using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models.DomainModels
{
    public class Project
    {

        public Project()
        {
            Team = new List<UserProjects>();
            Bugs = new List<BugReport>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<BugReport> Bugs { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public virtual ICollection<UserProjects> Team { get; set; }
    }
}