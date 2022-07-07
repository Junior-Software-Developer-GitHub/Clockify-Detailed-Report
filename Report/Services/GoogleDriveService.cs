using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Microsoft.Extensions.Configuration;
using Report.Services.Interfaces;

namespace Report.Services
{
    public class GoogleDriveService : IGoogleDriveService
    { 
        private IConfiguration Configuration { get; }
        public GoogleDriveService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public GoogleDriveService() { }

        public async Task InsertReport()
        {
            try
            {
                var credential = GoogleCredential
                    .FromFile($"{Configuration.GetSection("GoogleCredential_location").Value}")
                    .CreateScoped(DriveService.ScopeConstants.Drive);

                var service = new DriveService(new BaseClientService.Initializer()
                    {HttpClientInitializer = credential});

                var fileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = "Detailed Report",
                    Parents = new List<string>() {$"{Configuration.GetSection("GoogleFolderId").Value}"} //Folder ID from URL
                };

                await using var fsSource = new FileStream("/home/codemancystudio/Desktop/report.csv", FileMode.Open,
                    FileAccess.Read);
                var request = service.Files.Create(fileMetaData, fsSource, "text/csv");
                request.Fields = "*";
                var results = await request.UploadAsync(CancellationToken.None);

                if (results.Status == UploadStatus.Failed) { Console.WriteLine("ERROR!"); }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}