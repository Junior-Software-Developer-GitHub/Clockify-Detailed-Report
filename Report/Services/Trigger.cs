using System.Threading.Tasks;
using Report.Services.Interfaces;

namespace Report.Services
{
    public class Trigger : ITrigger
    {
        private IGoogleDriveService _googleDriveService;
        private IClockifyService _clockifyService;
        private static Trigger _instance;

        
        public static Trigger Instance => _instance ??= new Trigger(new GoogleDriveService(), new ClockifyService());

        private Trigger(IGoogleDriveService googleDriveService, IClockifyService clockifyService)
        {
            _googleDriveService = googleDriveService;
            _clockifyService = clockifyService;
        }
        
        public async Task Start()
        {
            _clockifyService.GetReport();
            await _googleDriveService.InsertReport();
        }
            
    }
}