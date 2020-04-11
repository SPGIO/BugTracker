using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Bugs.Status
{
    public static class BugStatusFactory
    {
        public static BugStatus GetBugPriority(StatusType statusType)
        {
            return statusType switch
            {
                StatusType.Closed => new BugStatus()
                {
                    Name = "Major"
                },
                StatusType.InProgress => new BugStatus()
                {
                    Name = "Minor"
                },
                StatusType.Open => new BugStatus()
                {
                    Name = "Open"
                },
                StatusType.Reopen => new BugStatus()
                {
                    Name = "Reopen"
                },
                _ => null,
            };
        }
        public enum StatusType
        {
            Open,
            Closed,
            Reopen,
            InProgress 
        }
    }
}
