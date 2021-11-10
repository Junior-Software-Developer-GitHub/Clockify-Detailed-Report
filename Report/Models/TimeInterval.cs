using System;

namespace Report.Models
{
    public class TimeInterval
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int duration { get; set; }
        public TimeSpan durationInMinutes { get; set; }
        public decimal durationInDecimal { get; set; }
    }
}