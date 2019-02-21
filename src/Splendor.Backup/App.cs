using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Splendor.Backup {
    public class App {
        public void Run() {
            var currentDate = DateTime.Now.ToString("MMddyyyy");
            Log.Information("Current date {0}",currentDate);
            
        }

        private static string GetFileName() {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
                return "Win";
            }
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "OSX" : "Linux";
        }
    }
    
}
