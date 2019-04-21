using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MimeKit;
using Serilog;
namespace Splendor.BatchMailer {
    public class Startup {
        public Startup(string[] args) {
            //TODO: Read Args if is possible 
        }
        public void Run() {
            Log.Debug("Entering in Run.");
            Log.Debug("Launch mail reader");
            MailReader();
        }
        public void MailReader() {
            using (var client = new ImapClient(new ProtocolLogger(@"logs/server-imap.log"))) {
                client.ServerCertificateValidationCallback = (s, c, ch, e) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Connect("imap.gmail.com", 993, true);
                client.Authenticate("carrozzeria@splendorsrl.it", "At066bn1!");
                client.Inbox.Open(FolderAccess.ReadOnly);
                var query = SearchQuery.FromContains("carrozzeria@splendorsrl.it").Or(SearchQuery.FromContains("tony@fatture-online.com"));
                foreach (var uid in client.Inbox.Search(query)) {
                    var message = client.Inbox.GetMessage(uid);
                    if (IsValid(message)) { 
                        SendMessage(message);
                        Console.WriteLine("[match] {0}: {1}", uid, message.Subject);
                        return;
                    }
                }
            }
        }
        bool IsValid(MimeMessage message) {
            foreach(var x in message.To) {
                MailboxAddress mb =  (MailboxAddress)x;
                if (mb.Address.Equals("carrozzeria@splendorsrl.it", StringComparison.InvariantCultureIgnoreCase) && IsValiAttachment(message)) {
                    return true;
                }
            }
            return false;
        }
        bool IsValiAttachment (MimeMessage message) {
            if (message.Attachments.Any()) {
                var attachment = message.Attachments.FirstOrDefault();
                if (attachment.IsAttachment&& attachment.ContentDisposition.FileName.ToLower().Contains(".zip")) {
                    return true;
                }
            }
            return false;
        }
        public void SendMessage(MimeMessage message) {
            message.To.Clear();
            message.To.Add(new MailboxAddress("Tony Nara", "tony@fatture-online.com"));
            using (var client = new SmtpClient()) {
                client.ServerCertificateValidationCallback = (s, c, ch, e) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("carrozzeria@splendorsrl.it", "At066bn1!");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    
        public static void SaveAttachments(MimeMessage message) {
            foreach (var attachment in message.Attachments) {
                    var part = (MimePart)attachment;
                    var fileName = part.FileName;
                    using (var stream = File.Create(fileName))
                        part.Content.DecodeTo(stream);
            }
        }
    }
}
