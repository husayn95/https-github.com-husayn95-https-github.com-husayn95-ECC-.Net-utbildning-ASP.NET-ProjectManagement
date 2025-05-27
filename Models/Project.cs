using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models
{
    public enum ProjectStatus
    {
        Started,
        Completed
    }

    public class Project
    {
        public int Id { get; set; }

        public required string ProjectName { get; set; }

        public string ClientName { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Budget { get; set; }

        public ProjectStatus Status { get; set; } = ProjectStatus.Started;

        public string UserId { get; set; }

    }
}