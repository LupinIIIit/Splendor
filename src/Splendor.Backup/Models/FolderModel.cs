using System;
using System.Collections.Generic;

namespace Splendor.Backup.Models {
    public class FolderModel {
        public FolderModel() {
            Paths = new List<string>();
        }
        public List<string> Paths { get; set; }
        public long MaxFileSize { get; set; }
        public NetworkCredential Credential { get; set; }
    }
}