using System;
using System.Collections.Generic;
using System.Text;

namespace Splendor.MailManager.Models {
    public class Configuration {
        public string Server { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RemoveOAuth { get; set; }
        public List<string> Filters { get; set; } 
        public List<string> Senders { get; set; }
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("\nServer:[{0}]",Server);
            sb.AppendFormat("\nPort:[{0}]", Port);
            sb.AppendFormat("\nUserName:[{0}]", UserName);
            sb.AppendFormat("\nPassword:[{0}]", Password);
            sb.AppendFormat("\nRemoveOAuth:[{0}]", RemoveOAuth);
            if(Filters!=null && Filters.Count > 0) {
                int count = 1;
                foreach(var x in Filters) {
                    sb.AppendFormat("\nFiltro per[{0}]:[{1}]",count, x);
                    count++;
                }
            }
            if (Senders != null && Senders.Count > 0) {
                int count = 1;
                foreach (var x in Senders) {
                    sb.AppendFormat("\nInvia a [{0}]:[{1}]", count, x);
                    count++;
                }
            }
            return sb.ToString();
        }
    }
}
