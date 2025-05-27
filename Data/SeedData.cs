using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    FullName = "Admin User",
                    Email = "admin@example.com",
                    PasswordHash = "hashedpassword",
                    CreatedAt = DateTime.UtcNow
                });
                context.SaveChanges();
            }

            var user = context.Users.First();

            if (context.Projects.Any())
            {
                return;
            }

            // Några av dessa exempel innehåller chatgpt genererad kod.
            context.Projects.AddRange(
                new Project
                {
                    ProjectName = "Website Redesign",
                    ClientName = "GitLab Inc.",
                    Description = "It is necessary to develop a website redesign in a corporate style.",
                    StartDate = DateTime.Parse("2025-02-01"),
                    EndDate = DateTime.Parse("2025-04-15"),
                    Budget = 12000,
                    Status = ProjectStatus.Started,
                    UserId = user.Id.ToString(),
                },
                new Project
                {
                    ProjectName = "Landing Page",
                    ClientName = "Bitbucket Inc.",
                    Description = "It is necessary to create a landing together with the development of design.",
                    StartDate = DateTime.Parse("2025-02-15"),
                    EndDate = DateTime.Parse("2025-03-30"),
                    Budget = 5000,
                    Status = ProjectStatus.Started,
                    UserId = user.Id.ToString()
                },
                new Project
                {
                    ProjectName = "Parser Development",
                    ClientName = "Driveway Inc.",
                    Description = "It is necessary to develop a ticket site parser in python.",
                    StartDate = DateTime.Parse("2025-01-10"),
                    EndDate = DateTime.Parse("2025-03-10"),
                    Budget = 8000,
                    Status = ProjectStatus.Completed,
                    UserId = user.Id.ToString()
                },
                new Project
                {
                    ProjectName = "App Development",
                    ClientName = "Slack Technologies Inc.",
                    Description = "Create a mobile application on iOS and Android devices.",
                    StartDate = DateTime.Parse("2025-03-01"),
                    EndDate = DateTime.Parse("2025-07-01"),
                    Budget = 25000,
                    Status = ProjectStatus.Started,
                    UserId = user.Id.ToString()
                },
                new Project
                {
                    ProjectName = "App Development",
                    ClientName = "Google Inc.",
                    Description = "Create a mobile application on iOS and Android devices.",
                    StartDate = DateTime.Parse("2025-01-15"),
                    EndDate = DateTime.Parse("2025-06-15"),
                    Budget = 30000,
                    Status = ProjectStatus.Started,
                    UserId = user.Id.ToString()
                },
                new Project
                {
                    ProjectName = "Admin Dashboard",
                    ClientName = "ArtTemplate Inc.",
                    Description = "Necessary to create Admin Dashboard on Angular 9.",
                    StartDate = DateTime.Parse("2025-02-01"),
                    EndDate = DateTime.Parse("2025-05-01"),
                    Budget = 15000,
                    Status = ProjectStatus.Completed,
                    UserId = user.Id.ToString()
                },
                new Project
                {
                    ProjectName = "Web App on Vue.js",
                    ClientName = "ArtTemplate Inc.",
                    Description = "It is necessary to develop a web app on the framework Vue.js",
                    StartDate = DateTime.Parse("2025-03-15"),
                    EndDate = DateTime.Parse("2025-06-15"),
                    Budget = 18000,
                    Status = ProjectStatus.Started,
                    UserId = user.Id.ToString()
                },
                new Project
                {
                    ProjectName = "App Development",
                    ClientName = "Facebook Inc.",
                    Description = "Create a mobile application on iOS and Android devices.",
                    StartDate = DateTime.Parse("2025-04-01"),
                    EndDate = DateTime.Parse("2025-07-15"),
                    Budget = 28000,
                    Status = ProjectStatus.Started,
                    UserId = user.Id.ToString()
                }
            );

            context.SaveChanges();
        }

    }
}