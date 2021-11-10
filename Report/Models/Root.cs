using System.Collections.Generic;

namespace Report.Models
{
    public class Root
    {
        public List<Total> totals { get; set; }
        public List<Timeentry> timeentries { get; set; }
    }
}