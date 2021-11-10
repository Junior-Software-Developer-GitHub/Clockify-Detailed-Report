using System.Globalization;
using CsvHelper.Configuration;

namespace Report.Models
{
    public sealed class FooMap : ClassMap<Timeentry>
    {
        public FooMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.timeInterval.duration).Ignore();
            Map(m => m.timeInterval.durationInMinutes).Ignore();
        }
    }
}