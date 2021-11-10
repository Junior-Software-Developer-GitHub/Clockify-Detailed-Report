using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Report.Services.Interfaces;

namespace Report.Services
{
    public class GoogleDriveService : IGoogleDriveService
    { 
        public async Task InsertReport()
        {
            try
            {
                var credential = GoogleCredential
                    .FromFile("/home/codemancystudio/Documents/ClockifySheets/Report/credentials.json")
                    .CreateScoped(DriveService.ScopeConstants.Drive);

                var service = new DriveService(new BaseClientService.Initializer()
                    {HttpClientInitializer = credential});

                var fileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = "Detailed Report",
                    Parents = new List<string>() {"1BLgFRYQfxSWCXbmsVOKCSmGksOunHy9N"} //Folder ID from URL
                };

                using (var fsSource = new FileStream("/home/codemancystudio/Desktop/report.csv", FileMode.Open,
                    FileAccess.Read))
                {
                    var request = service.Files.Create(fileMetaData, fsSource, "text/csv");
                    request.Fields = "*";
                    var results = await request.UploadAsync(CancellationToken.None);

                    if (results.Status == UploadStatus.Failed)
                    {
                        Console.WriteLine("ERROR!");
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