using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class BugNotFoundException : Exception
    {

        public override string Message => "Bug not found";
    }
}
