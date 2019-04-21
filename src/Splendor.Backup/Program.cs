using Serilog;
using System;
using System.IO;
using Splendor.Utility.IO;
namespace Splendor.Backup {
    class Program {
        static void Main(string[] args) {
            string path = CheckOrCreateAppFolder.HomeFolder();
            var logFileName = $"backup-{DateTime.Now.ToString("MMddyyyy")}.log";
            if (!CheckOrCreateAppFolder.CheckFolder()){
                if (!CheckOrCreateAppFolder.CreateHomeFolder()) {
                    throw new Exception("Impossibile creare la home directory.");
                }
            }
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole()
                .WriteTo.File(Path.Combine(path,logFileName), outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            Log.Information("Start Splendor Backup");
            try {
                new App().Run();
            } catch (Exception ex) {
                Log.Error(ex, "Splendor backup terminate in error: ");
            }
            Log.Information("End Splendor Backup");
        }
    }
}