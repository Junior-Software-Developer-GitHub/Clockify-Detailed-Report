using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.Json;
using CsvHelper;
using Report.Models;
using Report.Services.Interfaces;


namespace Report.Services
{
    public class ClockifyService : IClockifyService
    {
        public void GetReport()
        {
            try
            {
                string FilePath = "/home/codemancystudio/Desktop/report.csv";

                DTO dto = new DTO();
                Filter filter = new Filter();
                filter.page = 1;
                filter.pageSize = 200;

                DateTime now = DateTime.Now.AddDays(-1);

                dto.dateRangeStart =
                    new DateTime(now.Year, now.Month, now.Day, 0, 0, 0); // Beginning of the previous day 

                dto.dateRangeEnd = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59); // End of the previous day
                dto.detailedFilter = filter;


                var httpWebRequest =
                    (HttpWebRequest) WebRequest.Create(
                        "https://reports.api.clockify.me/v1/workspaces/{WORKSPACE ID}/reports/detailed");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("X-Api-Key", "{API KEY}");

                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonSerializer.Serialize(dto);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
                using (var streamReader =
                    new StreamReader(httpResponse.GetResponseStream() ?? throw new NullReferenceException()))
                {
                    Root myDeserializedClass = JsonSerializer.Deserialize<Root>(streamReader.ReadToEnd());
                    foreach (var obj in myDeserializedClass.timeentries)
                    {
                        obj.timeInterval!.durationInMinutes = TimeSpan.FromSeconds(obj.timeInterval.duration);
                        obj.timeInterval.durationInDecimal =
                            Convert.ToDecimal(TimeSpan.Parse(obj.timeInterval!.durationInMinutes.ToString())
                                .TotalHours);


                        if (obj.amount != null && obj.rate != null)
                        {
                            obj.amount = obj.amount / 100;
                            obj.rate = obj.rate / 100;
                        }
                    }

                    using (var writer = new StreamWriter(FilePath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<FooMap>();
                        csv.WriteRecords(myDeserializedClass.timeentries);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}