using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Splendor.Backup {
    // To product EXE use a command like this: dotnet publish -c Release -r win7-x64
    // See: https://docs.microsoft.com/en-us/dotnet/core/deploying/deploy-with-cli
    static class Program {
        static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
              .MinimumLevel.Override("System", LogEventLevel.Warning)
              .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
              .Enrich.FromLogContext()
              .WriteTo.File(@"logs\mail-manager-.log", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
              .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
              .CreateLogger();
            // Adding JSON file into IConfiguration.
            IConfiguration config = new ConfigurationBuilder()
                 .AddJsonFile("Splendor.Libs.dll", true, true)
                 .Build();

            // Read configuration
            string FirstName = config["FirstName"];
            string LastName = config["LastName"];
            Console.WriteLine($"Hello {FirstName} {LastName}!");

            var address = config.GetSection("Address");
            string street = address["Street"];
            string city = address["City"];
            string zip = address["Zip"];
            string state = address["State"];
            Console.WriteLine($"Your address is: {street}, {city}, {state} {zip}");
        }
    }
}