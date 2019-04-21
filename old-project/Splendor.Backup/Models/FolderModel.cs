using System.Collections.Generic;

namespace Splendor.Backup.Models
{
    public class FolderModel
    {
        public List<string> Path { get; set; }
        public string UserName { get; set; }
        public string Domain { get; set; }
        public string Passord { get; set; }
        public long MaxFileSie { get; set; }
    }
}