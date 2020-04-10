using System;

namespace BugTracker.Services.Bugs
{
    public class BugNotFoundException : Exception
    {

        public override string Message => "Bug not found";
    }
}
