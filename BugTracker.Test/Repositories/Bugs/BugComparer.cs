using BugTracker.Models.Bugs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BugTracker.Test.Repositories.Bugs
{
    public class BugComparer : Comparer<Bug>
    {
        public override int Compare([AllowNull] Bug x, [AllowNull] Bug y)
        {
            if (x == null || y == null) return 0;
            bool sameId = x.Id.CompareTo(y.Id) != 0;
            bool sameSteps = false;
            if (x.HowToReproduceBug == null && y.HowToReproduceBug == null)
            {
                sameSteps = true;
            }
            else
            {
                sameSteps = x.HowToReproduceBug.CompareTo(y.HowToReproduceBug) != 0;
            }

            return (sameId &&
                   sameSteps)
                ? 1 : 0;
        }
    }
}
