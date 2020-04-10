using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Bugs
{
    public interface IBug
    {
        int Id { get; set; }
        string HowToReproduceBug { get; set; }
        DateTime DateReported { get; set; }
        DateTime DateFixed { get; set; }
        IBugStatus Status { get; set; }
        IBugPriorty Priorty { get; set; }
    }
}
