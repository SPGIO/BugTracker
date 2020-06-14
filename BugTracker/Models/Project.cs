using BugTracker.Models.Bugs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class Project : IProject
    {
        private ICollection<Bug> bugs;
        private ICollection<UserProjects> team;

        public Project()
        {
            Team = new List<UserProjects>();
            Bugs = new List<Bug>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Bug> Bugs
        {
            get { return bugs; }
            set
            {
                if(value == null) bugs = new List<Bug>();
                else bugs = value;
            }
        }
        [Required]
        public DateTime DateCreated { get; set; }
        public virtual ICollection<UserProjects> Team
        {
            get
            {
                return team;
            }
            set
            {
                if (team == null) team = new List<UserProjects>();
                else team = value;
            }
        }
    }
}
