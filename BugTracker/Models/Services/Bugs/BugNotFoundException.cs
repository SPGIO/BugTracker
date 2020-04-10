using System;

namespace BugTracker.Models.Services.Bugs
{
    public class BugNotFoundException : Exception
    {

        public override string Message => "Bug not found";
    }
}
