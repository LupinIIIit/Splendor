﻿using System;
using System.Linq;
using System.Threading;

using MailKit.Net.Imap;
using MailKit;
using MailKit.Search;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Splendor.MailManager.Utils;
using Splendor.MailManager.Helpers;

namespace Splendor.MailManager {
    class Program {
        public static void Main ( string[] args ) {
            Log.Logger = new LoggerConfiguration ()
               .MinimumLevel.Debug ()
               .MinimumLevel.Override ("Microsoft", LogEventLevel.Warning)
               .MinimumLevel.Override ("System", LogEventLevel.Warning)
               .MinimumLevel.Override ("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
               .Enrich.FromLogContext ()
               .WriteTo.File (@"logs/mail-manager-.log", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
               .WriteTo.Console (outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
               .CreateLogger ();
            ConfigReader cf = new ConfigReader ("mail-manager.json");
            EmailReader ();
            Log.Debug (cf.Configuration.ToString ());
            Console.ReadLine ();
        }
        static void EmailReader () {
            using (var client = new ImapClient (new ProtocolLogger (Console.OpenStandardError ()))) {
                client.ServerCertificateValidationCallback = ( s, c, ch, e ) => true;
                client.AuthenticationMechanisms.Remove ("XOAUTH2");
                client.Connect ("imap.gmail.com", 993, true);
                client.Authenticate ("carrozzeria@splendorsrl.it", "At066bn1!");
                client.Inbox.Open (FolderAccess.ReadOnly);
                // Get the summary information of all of the messages (suitable for displaying in a message list).
                var query = SearchQuery.FromContains ("carrozzeria@splendorsrl.it").Or (SearchQuery.FromContains ("tony@fatture-online.com"));
                foreach (var uid in client.Inbox.Search (query)) {
                    var message = client.Inbox.GetMessage (uid);
                    if (message.To.ToString()=="carrozzeria@splendorsrl.it"&& message.Attachments.Count()>0){
                        Console.WriteLine ("[match] {0}: {1}", uid, message.Subject);    
                    }

                }
                return;
                var messages = client.Inbox.Fetch (0, -1, MessageSummaryItems.Full | MessageSummaryItems.UniqueId).ToList ();
                                // Keep track of messages being expunged so that when the CountChanged event fires, we can tell if it's
                // because new messages have arrived vs messages being removed (or some combination of the two).
                client.Inbox.MessageExpunged += ( sender, e ) => {
                    var folder = (ImapFolder)sender;
                    if (e.Index < messages.Count) {
                        var message = messages[e.Index];
                        Console.WriteLine ("{0}: expunged message {1}: Subject: {2}", folder, e.Index, message.Envelope.Subject);
                        // Note: If you are keeping a local cache of message information
                        // (e.g. MessageSummary data) for the folder, then you'll need
                        // to remove the message at e.Index.
                        messages.RemoveAt (e.Index);
                    } else {
                        Console.WriteLine ("{0}: expunged message {1}: Unknown message.", folder, e.Index);
                    }
                };

                // Keep track of changes to the number of messages in the folder (this is how we'll tell if new messages have arrived).
                client.Inbox.CountChanged += ( sender, e ) => {
                    // Note: the CountChanged event will fire when new messages arrive in the folder and/or when messages are expunged.
                    var folder = (ImapFolder)sender;
                    Console.WriteLine ("The number of messages in {0} has changed.", folder);
                    // Note: because we are keeping track of the MessageExpunged event and updating our
                    // 'messages' list, we know that if we get a CountChanged event and folder.Count is
                    // larger than messages.Count, then it means that new messages have arrived.
                    if (folder.Count > messages.Count) {
                        Console.WriteLine ("{0} new messages have arrived.", folder.Count - messages.Count);
                        // Note: your first instict may be to fetch these new messages now, but you cannot do
                        // that in an event handler (the ImapFolder is not re-entrant).
                        //
                        // If this code had access to the 'done' CancellationTokenSource (see below), it could
                        // cancel that to cause the IDLE loop to end.
                    }
                };
                // Keep track of flag changes.
                client.Inbox.MessageFlagsChanged += ( sender, e ) => {
                    var folder = (ImapFolder)sender;
                    Console.WriteLine ("{0}: flags for message {1} have changed to: {2}.", folder, e.Index, e.Flags);
                };
                Console.WriteLine ("Hit any key to end the IDLE loop.");
                using (var done = new CancellationTokenSource ()) {
                    // Note: when the 'done' CancellationTokenSource is cancelled, it ends to IDLE loop.
                    var thread = new Thread (IdleLoop);
                    thread.Start (new IdleState (client, done.Token));
                    Console.ReadKey ();
                    done.Cancel ();
                    thread.Join ();
                }
                if (client.Inbox.Count > messages.Count) {
                    Console.WriteLine ("The new messages that arrived during IDLE are:");
                    foreach (var message in client.Inbox.Fetch (messages.Count, -1, MessageSummaryItems.Full | MessageSummaryItems.UniqueId))
                        Console.WriteLine ("Subject: {0}", message.Envelope.Subject);
                }
                client.Disconnect (true);
            }
        }
        static void IdleLoop ( object state ) {
            var idle = (IdleState)state;
            lock (idle.Client.SyncRoot) {
                // Note: since the IMAP server will drop the connection after 30 minutes, we must loop sending IDLE commands that
                // last ~29 minutes or until the user has requested that they do not want to IDLE anymore.
                //
                // For GMail, we use a 9 minute interval because they do not seem to keep the connection alive for more than ~10 minutes.
                while (!idle.IsCancellationRequested) {
                    // Note: Starting with .NET 4.5, you can make this simpler by using the CancellationTokenSource .ctor that
                    // takes a TimeSpan argument, thus eliminating the need to create a timer.
                    using (var timeout = new CancellationTokenSource ()) {
                        using (var timer = new System.Timers.Timer (9 * 60 * 1000)) {
                            // End the IDLE command when the timer expires.
                            timer.Elapsed += ( sender, e ) => timeout.Cancel ();
                            timer.AutoReset = false;
                            timer.Enabled = true;
                            try {
                                // We set the timeout source so that if the idle.DoneToken is cancelled, it can cancel the timeout
                                idle.SetTimeoutSource (timeout);
                                if (idle.Client.Capabilities.HasFlag (ImapCapabilities.Idle)) {
                                    // The Idle() method will not return until the timeout has elapsed or idle.CancellationToken is cancelled
                                    idle.Client.Idle (timeout.Token, idle.CancellationToken);
                                } else {
                                    // The IMAP server does not support IDLE, so send a NOOP command instead
                                    idle.Client.NoOp (idle.CancellationToken);
                                    // Wait for the timeout to elapse or the cancellation token to be cancelled
                                    WaitHandle.WaitAny (new[] { timeout.Token.WaitHandle, idle.CancellationToken.WaitHandle });
                                }
                            } catch (OperationCanceledException) {
                                // This means that idle.CancellationToken was cancelled, not the DoneToken nor the timeout.
                                break;
                            } catch (ImapProtocolException) {
                                // The IMAP server sent garbage in a response and the ImapClient was unable to deal with it.
                                // This should never happen in practice, but it's probably still a good idea to handle it.
                                //
                                // Note: an ImapProtocolException almost always results in the ImapClient getting disconnected.
                                break;
                            } catch (ImapCommandException) {
                                // The IMAP server responded with "NO" or "BAD" to either the IDLE command or the NOOP command.
                                // This should never happen... but again, we're catching it for the sake of completeness.
                                break;
                            } finally {
                                // We're about to Dispose() the timeout source, so set it to null.
                                idle.SetTimeoutSource (null);
                            }
                        }
                    }
                }
            }
        }
    }
}