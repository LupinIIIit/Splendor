using System.Collections.Generic;
using System.Text;

namespace Splendor.Backup.Models {
    public class ConfigModel {
        public string HomeFolder {get;set;}
        public FolderModel OutFolder {get;set;}
        public List<ApplicationModel> Apps {get;set;}
        public override string ToString(){
            StringBuilder sb = new StringBuilder();
            sb.Append("\n Here list of configs");

            return sb.ToString();
        }
/*set home-folder=C:\Users\Administrator\Documents\backup-service\
set home-backup=\\splendornas\backup
set home-contab=\\splendornas\Contabilita
set home-foto=\\splendornas\foto
set home-wgescar=\\splendornas\WGESTCAR
set home-outlook=\\splendornas\outlook 
set home-video=\\splendornas\VideoTV 
set home-wincar=\\reception-pc\wincar
set home-winmec=\\silvia-pc\winmec
set username=microsoftlocal\silvia
set username2=microsoftlocal\admin
set password=Splendor1!
set wincar-filename=wincar-%formatdate%.zip
set wincar-archivi=wincar-archivi.zip
set wincar-contab=wincar-contab.zip
set winmec-filename=winmec%formatdate%.zip
set winmec-archivi=winmec-archivi.zip
set winmec-contab=winmec-contab.zip
set contab=contab%formatdate%.zip
set foto=foto%formatdate%.zip
set wgescar=wgescar%formatdate%.zip
set outlook=outlook%formatdate%.zip
set video=video%formatdate%.zip*/
    }
}