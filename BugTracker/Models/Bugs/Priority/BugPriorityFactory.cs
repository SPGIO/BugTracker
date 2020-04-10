using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Bugs.Priority
{
    public static partial class BugPriorityFactory
    {
        public static BugPriority GetBugPriority(PriorityType priorityType)
        {
            return priorityType switch
            {
                PriorityType.Critical => new CriticalPriority() {
                    Name = "Critical",
                    Priority = 1
                },
                PriorityType.Normal => null,
                _ => null,
            };
        }
        public enum PriorityType
        {
            Critical,
            Normal
        }
    }
}
