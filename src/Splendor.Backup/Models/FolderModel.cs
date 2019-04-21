using System.Collections.Generic;
using System.Text;

namespace Splendor.Backup.Models {
    public class FolderModel {
        public FolderModel() {
            Paths = new List<string>();
        }
        public List<string> Paths { get; set; }
        public long MaxFileSize { get; set; }
        public NetworkCredential Credential { get; set; }
        public override string ToString () {
            StringBuilder sb = new StringBuilder ();
            sb.Append ("\n--> FolderModel ");
            sb.Append ($"\n---> MaxFileSize: {MaxFileSize}");
            sb.Append ($"\n---> Lis of Paths");
            int i = 1;
            foreach(string val in Paths) {
                sb.Append ($"\n---> Path[{i}]: {val}" );
                i++;
            }
            sb.Append (Credential.ToString ());
            return sb.ToString();
        }
    }
}