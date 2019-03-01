﻿using System;
using System.Runtime.InteropServices;
using Splendor.Utility.IO;
using Splendor.Backup.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Collections.Generic;
using Splendor.Costants;
namespace Splendor.Backup.Helpers {
    public sealed class Config {
        public string Path { get; set; }
        public string CurrentDate { get; set; }
        public OS CurrentOs { get; set; }
        public FolderModel OutFolder { get; set; }
        public List<ApplicationModel> Apps { get; set; }
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n").Append(Costants.Backup.LOG_BAR);
            sb.Append("\n").Append(Costants.Backup.SINGLE_SHARP).Append(CenterString(Costants.Backup.CONFIG_LOADED,Costants.Backup.LOG_BAR.Length -2)).Append(Costants.Backup.SINGLE_SHARP);
            sb.Append("\n").Append(Costants.Backup.LOG_BAR);
            sb.Append($"\nCurrent date: {Config.Instance.CurrentDate}");
            sb.Append($"\nCurrent work path: {Config.Instance.Path}");
            sb.Append($"\nCurrent Os: {Config.Instance.CurrentOs}");
            sb.Append($"OutputFolder: {OutFolder.ToString()}" );
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
            var outFolder = config.GetSection(Costants.Backup.SECT_OUT_FOLDER);
            OutFolder = PopulateFolderModel(outFolder);
            var appSect = config.GetSection(Costants.Backup.LIST_APPS);
            Apps = new List<ApplicationModel>();
            var appList = appSect.GetChildren();
            foreach(var app in appList) {
                Apps.Add(PopulateAppModel(app));
            }
        }
        private FolderModel PopulateFolderModel(IConfigurationSection FolderSect) {
            FolderModel fm = new FolderModel();
            var pathsSect = FolderSect.GetSection(Costants.Backup.LIST_PATH);
            var paths = pathsSect.GetChildren();
            foreach (var path in paths) {
                fm.Paths.Add(path.Value);
            }
            var credSect = FolderSect.GetSection(Costants.Backup.SECT_CREDENTIAL);
            fm.Credential = new NetworkCredential(credSect[Costants.Backup.USER_NAME], credSect[Costants.Backup.PASSWORD], credSect[Costants.Backup.DOMAIN]);
            var s = FolderSect[Costants.Backup.MAX_FILE_SIZE];
            long.TryParse(s, out long mxfs);
            fm.MaxFileSize = mxfs;
            return fm;
        }
        private ApplicationModel PopulateAppModel(IConfigurationSection FolderSect) {
            ApplicationModel fm = new ApplicationModel();
            var pathsSect = FolderSect.GetSection(Costants.Backup.LIST_PATH);
            var paths = pathsSect.GetChildren();
            foreach (var path in paths) {
                fm.Paths.Add(path.Value);
            }
            var credSect = FolderSect.GetSection(Costants.Backup.SECT_CREDENTIAL);
            fm.Credential = new NetworkCredential(credSect[Costants.Backup.USER_NAME], credSect[Costants.Backup.PASSWORD], credSect[Costants.Backup.DOMAIN]);
            var s = FolderSect[Costants.Backup.MAX_FILE_SIZE];
            long.TryParse(s, out long mxfs);
            fm.MaxFileSize = mxfs;
            fm.ApplicationName = FolderSect[Costants.Backup.APP_NAME];
            var sid = FolderSect[Costants.Backup.APP_ID];
            int.TryParse(sid, out int id);
            fm.ApplicationId = id;
            return fm;
        }

        private OS GetOS() {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                return OS.Windows;
            }
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? OS.OSX : OS.Linux;
        }
        private string CenterString(string stringToCenter, int totalLength) {
            return stringToCenter.PadLeft(((totalLength - stringToCenter.Length) / 2)+ stringToCenter.Length).PadRight(totalLength);
        }
    }
}