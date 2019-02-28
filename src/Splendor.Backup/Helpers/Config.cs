using System;
using System.Runtime.InteropServices;
using Splendor.Utility.IO;
using Splendor.Backup.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Collections.Generic;
using Splendor.Costants.Backup;
namespace Splendor.Backup.Helpers {
    public sealed class Config {
        public string Path { get; set; }
        public string CurrentDate { get; set; }
        public OS CurrentOs { get; set; }
        public FolderModel OutFolder { get; set; }
        public List<ApplicationModel> Apps { get; set; }
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n Here list of configs");
            return sb.ToString();
        }
        private Config() {
            LoadConfig();
        }
        private static readonly Lazy<Config> lazy = new Lazy<Config>(() => new Config());
        public static Config Instance => lazy.Value;
        private void LoadConfig() {
            Path = CheckOrCreateAppFolder.HomeFolder();
            CurrentDate = DateTime.Now.ToString("MMddyyyy");
            CurrentOs = GetOS();
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            // Read configuration

            var outFolder = config.GetSection(Costant.SECT_OUT_FOLDER);
            var section = config.GetSection($"{Costant.SECT_OUT_FOLDER}:{Costant.SECT_PATH}");
            var folders = section.GetChildren();
            foreach(var folder in folders) {
                var x = folder.Value;
            }
            var cred = outFolder.GetSection(Costant.SECT_CREDENTIAL);
            string userName = cred[Costant.USER_NAME];
            string domain = cred[Costant.DOMAIN];
            string password = cred[Costant.PASSWORD];
            string maxFileSize = outFolder[Costant.MAX_FILE_SIZE];
        }
        private OS GetOS() {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                return OS.Windows;
            }
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? OS.OSX : OS.Linux;
        }

    }
}