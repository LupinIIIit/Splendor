using System;
using System.Collections.Generic;

namespace Splendor.Backup.Models {
    public class FolderModel {
        public List<string> Path { get; set; }
        public long MaxFileSize { get; set; }
        public NetworkCredential Credential { get; set; }
    }
}