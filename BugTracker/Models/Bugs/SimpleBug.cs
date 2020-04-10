﻿using System;

namespace BugTracker.Models.Bugs
{
    public class SimpleBug : IBug
    {
        public int Id { get; set; }
        public string HowToReproduceBug { get; set; }
        public DateTime DateReported { get; set; }
        public DateTime DateFixed { get; set; }
        public IBugStatus Status { get; set; }
        public IBugPriorty Priorty { get; set; }
    }
}