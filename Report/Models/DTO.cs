using System;

namespace Report.Models
{
    public class DTO
    {
        public DateTime dateRangeStart { get; set; }
        public DateTime dateRangeEnd { get; set; }
        public Filter detailedFilter { get; set; }
    }
}