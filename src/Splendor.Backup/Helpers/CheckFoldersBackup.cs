using Serilog;
using Splendor.Utility.Network;
using System;
using System.IO;
using System.Net;
namespace Splendor.Backup.Helpers {
    public class CheckFoldersBackup {
        public bool Run() {
            Log.Information("Start Check Folder backup");
            try {
                Log.Information($"Try to connect nas out folder");
                var outFolder = Config.Instance.OutFolder;
                var outPath = outFolder.Paths[0];
                NetworkCredential credentials = new NetworkCredential(outFolder.Credential.UserName, outFolder.Credential.Password);
                using (NetworkConnection nc = new NetworkConnection(outPath, credentials)) {
                    Log.Information("try to clean output folder i take only the last five days with today");
                    foreach (var g in Directory.GetDirectories(outPath)) {
                        Log.Information($"Analisi della directory: {g}");
                        var b = g.Substring(g.LastIndexOf("\\")+1);
                        if (!Config.Instance.ActiveBackupFolders.Contains(b)) {
                            Log.Information($"--> La directory{g} è troppo vecchia la cancello");
                            Directory.Delete(g, true);
                            Log.Information($"--> La directory{g} stata cancellata.");
                        }
                    }
                }
            } catch (Exception ex) {
                Log.Error(ex, "Check folder Backup error:");
                return false;
            }
            Log.Information("End Check Folder backup");
            return true;
        }
    }
}
