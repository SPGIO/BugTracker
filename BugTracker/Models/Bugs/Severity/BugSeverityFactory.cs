using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Bugs.Severity
{
    public static partial class BugSeverityFactory
    {
        public static BugSeverity GetBugPriority(SeverityType priorityType)
        {
            return priorityType switch
            {
                SeverityType.Critical => new BugSeverity() {
                    Name = "Major",
                    Priority = 1
                },
                SeverityType.Normal => new BugSeverity()
                {
                    Name = "Minor", 
                    Priority = 2
                },
                _ => null,
            };
        }
        public enum SeverityType
        {
            Critical,
            Normal
        }
    }
}
