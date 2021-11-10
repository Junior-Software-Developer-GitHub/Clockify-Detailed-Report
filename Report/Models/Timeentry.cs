#nullable enable
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Report.Models
{
    public class Timeentry
    {
        public  string? _id { get; set; }
        public string? description { get; set; }
        public string? userId { get; set; }
        public bool? billable { get; set; }
        public string? taskId { get; set; }
        public string? projectId { get; set; }
        public TimeInterval? timeInterval { get; set; }
        public string? taskName { get; set; }
        public List<object>? tags { get; set; }
        public bool? isLocked { get; set; }
        public object? customFields { get; set; }
        public decimal? amount { get; set; }
        public double? rate { get; set; }
        public string? userName { get; set; }
        public string? userEmail { get; set; }
        public string? projectName { get; set; }
        public string? projectColor { get; set; }
        public string? clientName { get; set; }
        public string? clientId { get; set; }
    }
}