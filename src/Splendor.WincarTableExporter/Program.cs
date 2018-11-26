using System.Data;
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Splendor.Utility.Network;
using System.IO;
using System.IO.Compression;
using Serilog;

namespace Splendor.WincarTableExporter {
    class Program {
        static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole()
                .WriteTo.File("log.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            Log.Information("Start WincarTableExplorer");
            App app = new App();
            app.Start();
            // Create SQL and Connection strings
            //String WincarBaseFolder = @"\\splendornas";
            //NetworkCredential NCredentials = new NetworkCredential("tony", "at066bn");
            //using (new NetworkConnection(WincarBaseFolder, NCredentials)) {
            //    string sDir = @"\\splendornas\video\backup\20181123";
            //    foreach (string d in Directory.GetFiles(sDir)) {
            //        if (d.ToLower().Contains("winmec")) {
            //            if(Directory.Exists(Path.Combine(sDir, "output"))) {
            //                Directory.Delete(Path.Combine(sDir, "output"), true);
            //            }
            //            Console.WriteLine("Trovato Winmec");
            //            ZipFile.ExtractToDirectory(d, Path.Combine(sDir,"output"));
            //            if (Directory.Exists(Path.Combine(sDir, @"output\winmec"))) {
            //                foreach (string z in Directory.GetFiles(Path.Combine(sDir, @"output\winmec"))) {
            //                    if (z.ToLower().Contains("archivi")) {
            //                        ZipFile.ExtractToDirectory(z, Path.Combine(sDir, @"output\winmec\archivi"));
            //                        if (Directory.Exists(Path.Combine(sDir, @"output\winmec\archivi"))) {
            //                            foreach (string k in Directory.GetFiles(Path.Combine(sDir, @"output\winmec\archivi"))) {
            //                                if (k.ToLower().Contains(".mdb")){
            //                                    ReadDb(k);
            //                                    Console.WriteLine(String.Format("Trovato db : {0}",k));
            //                                }
            //                                Console.WriteLine(k);
            //                            }
            //                        }
            //                    }
            //                    Console.WriteLine(z);
            //                }
            //            }
            //        }
            //        Console.WriteLine(d);
            //    }
            //}


            //    //Pause
                Console.ReadLine();
        }
        static void ReadDb(string dbName) {
            string ConnectionString = String.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = {0};Persist Security Info = False;", dbName);
            try {
                OleDbConnection cn = new OleDbConnection();
                DataTable schemaTable;

                //Connect to the Northwind database in SQL Server.
                //Be sure to use an account that has permission to list tables.
                cn.ConnectionString = ConnectionString;
                cn.Open();
                //Retrieve schema information about tables.
                //Because tables include tables, views, and other objects,
                //restrict to just TABLE in the Object array of restrictions.
                schemaTable = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });
                //List the table name from each row in the schema table.
                for (int i = 0; i < schemaTable.Rows.Count; i++) {
                    Console.WriteLine(schemaTable.Rows[i].ItemArray[2].ToString());
                }
                //Explicitly close - don't wait on garbage collection.
                cn.Close();
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }
    }

}
