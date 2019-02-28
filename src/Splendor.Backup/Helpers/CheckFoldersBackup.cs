using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splendor.Backup.Helpers {
    public class CheckFoldersBackup {
        public bool Run() {
            Log.Information("Start Check Folder backup");
            try {


            } catch(Exception ex) {
                Log.Error(ex, "Check folder Backup error:");
                return false;
            }
            Log.Information("End Check Folder backup");
            return true;
        }
    }
}
