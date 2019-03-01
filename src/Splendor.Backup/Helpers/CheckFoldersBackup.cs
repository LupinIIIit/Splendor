using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splendor.Utility.Network;
namespace Splendor.Backup.Helpers {
    public class CheckFoldersBackup {
        public bool Run() {
            Log.Information("Start Check Folder backup");
            try {
                Log.Information ($"Try to connect nas out folder");
                var k = Config.Instance.OutFolder;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential (k.Credential.UserName, k.Credential.Password, k.Credential.Domain);
                using (NetworkConnection nc = new NetworkConnection (k.Paths[0], credentials)) {
                    
                }
            } catch(Exception ex) {
                Log.Error(ex, "Check folder Backup error:");
                return false;
            }
            Log.Information("End Check Folder backup");
            return true;
        }
    }
}
