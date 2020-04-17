using BugTracker.Models.Bugs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class Project : IProject
    {
        public Project() => Team = new HashSet<UserProjects>();
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Bug> Bugs { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public virtual ICollection<UserProjects> Team { get; set; }
    }
}
