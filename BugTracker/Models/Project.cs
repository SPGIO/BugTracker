using BugTracker.Models.Bugs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Bug> Bugs { get; set; }
        public DateTime DateCreated { get; set; }
        public IEnumerable<ApplicationUser> Team { get; set; }
    }
}
