using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProjectManagementApp.Data;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Services
{
    public interface IAccountService
    {
        AuthResult Login(string email, string password, bool rememberMe);
        AuthResult Register(string fullName, string email, string password);
        Task LogoutAsync();

        User GetUserByEmail(string email);

        int GetStartedProjectsCount(string userId);
        int GetCompletedProjectsCount(string userId);
        List<Models.Activity> GetRecentActivities(string userId);
    }

    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;


        //Denna metod har jag tagit hjälp av ChatGpt
        public AccountService(
            ApplicationDbContext context,
            IPasswordHasher<User> passwordHasher,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }


        //Denna metod har jag tagit hjälp av ChatGpt
        public AuthResult Login(string email, string password, bool rememberMe)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return AuthResult.FailureResult("Något gick fel, antingen mailadressen eller lösenordet");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result != PasswordVerificationResult.Success)
                return AuthResult.FailureResult("Fel mail eller lösenord");

            //Denna metod har jag tagit hjälp av ChatGpt
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

            return AuthResult.SuccessResult();
        }


        public AuthResult Register(string fullName, string email, string password)
        {
            if (_context.Users.Any(u => u.Email == email))
                return AuthResult.FailureResult("Finns redan en sådan mejladress");

            var user = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = _passwordHasher.HashPassword(null, password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return AuthResult.SuccessResult();
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }


        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public int GetStartedProjectsCount(string userId)
        {
            return _context.Projects.Count(p => p.Status == ProjectStatus.Started && p.UserId == userId);
        }

        public int GetCompletedProjectsCount(string userId)
        {
            return _context.Projects.Count(p => p.Status == ProjectStatus.Completed && p.UserId == userId);
        }

        public List<Models.Activity> GetRecentActivities(string userId)
        {
            return _context.Activities
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Date)
                .Take(10)
                .ToList();
        }

    }
}
