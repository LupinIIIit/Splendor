using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace Splendor.BatchMailer {
    static internal  class Program {
        static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
               .MinimumLevel.Override("System", LogEventLevel.Warning)
               .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
               .Enrich.FromLogContext()
               .WriteTo.File(@"logs/mail-manager-.log", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
               .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
               .CreateLogger();
            new Startup(args).Run();
            Console.ReadLine();
        }
    }
}
