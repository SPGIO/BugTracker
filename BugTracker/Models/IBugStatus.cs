using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public interface IBugStatus
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
