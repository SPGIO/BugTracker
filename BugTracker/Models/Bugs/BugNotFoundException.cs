using System;

namespace BugTracker.Models.Bugs
{
    public class BugNotFoundException : Exception
    {

        public override string Message => "Bug not found";
    }
}
