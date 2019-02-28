using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Splendor.Backup.Helpers;
namespace Splendor.Backup {
    public class App {
        public void Run() {
            Log.Information($"Current date: {Config.Instance.CurrentDate}");
            Log.Information($"Current work path: {Config.Instance.Path}");
            Log.Information($"Current Os: {Config.Instance.CurrentOs}");
            var check = new CheckFoldersBackup();
            if (check.Run()) {

            }
        }
    }   
}