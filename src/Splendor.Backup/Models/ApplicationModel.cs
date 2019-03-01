using System;
using System.Collections.Generic;

namespace Splendor.Backup.Models {
    public class ApplicationModel :FolderModel {
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public override string ToString () {
            return $"\n-> ApplicationModel\n--> ApplicationId: {ApplicationId}\n--> ApplicationName: {ApplicationName}" + base.ToString ();
        }
    }
}