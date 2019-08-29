using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using Serilog;
using Splendor.MailManager.Models;

namespace Splendor.MailManager.Utils
{
    public class ConfigReader {
        private readonly Configuration config;
        public ConfigReader (string file) {
            if (string.IsNullOrEmpty(file)) {
                throw new Exception("Errrore impossibile caricare il file di configurazione");
            }
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(file, optional: true, reloadOnChange: true);
            IConfigurationRoot cfg = builder.Build();
            config = new Configuration {
                Server = Crypto.Decrypt(cfg["m"]),
                Port = int.Parse(cfg["p"]),
                UserName = Crypto.Decrypt(cfg["k"]),
                RemoveOAuth = Crypto.Decrypt(cfg["l"]),
                Password = Crypto.Decrypt(cfg["z"]),
                Filters = new List<string>(),
                Senders = new List<string>()                
            };
            foreach (var a in cfg.GetSection("filters").GetChildren()) {
                config.Filters.Add(Crypto.Decrypt(a.Value));
            }
            foreach (var a in cfg.GetSection("senders").GetChildren()) {
                config.Senders.Add(Crypto.Decrypt(a.Value));
            }
            Log.Debug("file di configurazione caricato");
        }
        public Configuration Configuration => config;
    }
}
