using Splendor.Costants.Backup;
namespace Splendor.Backup.Models {
    public class NetworkCredential {
        public NetworkCredential(string un, string pw): this(un, pw, null) {}
        public NetworkCredential(string un,string pw,string dm) {
            UserName = un;
            Password = pw;
            Domain = dm ?? Costant.DEFAULT_DOMAIN;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
    }
}
