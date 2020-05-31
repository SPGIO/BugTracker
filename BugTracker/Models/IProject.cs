using BugTracker.Models.Bugs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BugTracker.Models
{
    public interface IProject
    {
        ICollection<Bug> Bugs { get; set; }
        DateTime DateCreated { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        ICollection<UserProjects> Team { get; set; }
    }
}