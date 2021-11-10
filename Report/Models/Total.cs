namespace Report.Models
{
    public class Total
    {
        public string _id { get; set; }
        public int totalTime { get; set; }
        public int totalBillableTime { get; set; }
        public int entriesCount { get; set; }
        public double totalAmount { get; set; }
    }
}