using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models
{
    public class UserProfileViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Initials { get; set; }
        public DateTime JoinDate { get; set; }
        public int StartedProjects { get; set; }
        public int CompletedTasks { get; set; }
        public List<Activity> RecentActivities { get; set; }

        public UserProfileViewModel()
        {
            RecentActivities = new List<Activity>();
        }

        public static string GetInitials(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return "?";

            var parts = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
                return parts[0].Substring(0, 1).ToUpper();

            return (parts[0].Substring(0, 1) + parts[parts.Length - 1].Substring(0, 1)).ToUpper();
        }
    }

    public class UserActivity
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}