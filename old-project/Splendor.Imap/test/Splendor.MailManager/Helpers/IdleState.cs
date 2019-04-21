using System;
using System.Threading;
using MailKit.Net.Imap;

namespace Splendor.MailManager.Helpers {
    public class IdleState {
        public IdleState () {
        }
 
        readonly object mutex = new object ();
        CancellationTokenSource timeout;
        /// <summary>
        /// Get the cancellation token.
        /// </summary>
        /// <remarks>
        /// <para>The cancellation token is the brute-force approach to cancelling the IDLE and/or NOOP command.</para>
        /// <para>Using the cancellation token will typically drop the connection to the server and so should
        /// not be used unless the client is in the process of shutting down or otherwise needs to
        /// immediately abort communication with the server.</para>
        /// </remarks>
        /// <value>The cancellation token.</value>
        public CancellationToken CancellationToken { get; private set; }
        /// <summary>
        /// Get the done token.
        /// </summary>
        /// <remarks>
        /// <para>The done token tells the <see cref="Program.IdleLoop"/> that the user has requested to end the loop.</para>
        /// <para>When the done token is cancelled, the <see cref="Program.IdleLoop"/> will gracefully come to an end by
        /// cancelling the timeout and then breaking out of the loop.</para>
        /// </remarks>
        /// <value>The done token.</value>
        public CancellationToken DoneToken { get; private set; }
        /// <summary>
        /// Get the IMAP client.
        /// </summary>
        /// <value>The IMAP client.</value>
        public ImapClient Client { get; private set; }
        /// <summary>
        /// Check whether or not either of the CancellationToken's have been cancelled.
        /// </summary>
        /// <value><c>true</c> if cancellation was requested; otherwise, <c>false</c>.</value>
        public bool IsCancellationRequested {
            get {
                return CancellationToken.IsCancellationRequested || DoneToken.IsCancellationRequested;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="IdleState"/> class.
        /// </summary>
        /// <param name="client">The IMAP client.</param>
        /// <param name="doneToken">The user-controlled 'done' token.</param>
        /// <param name="cancellationToken">The brute-force cancellation token.</param>
        public IdleState ( ImapClient client, CancellationToken doneToken, CancellationToken cancellationToken = default (CancellationToken) ) {
            CancellationToken = cancellationToken;
            DoneToken = doneToken;
            Client = client;
            // When the user hits a key, end the current timeout as well
            doneToken.Register (CancelTimeout);
        }
        /// <summary>
        /// Cancel the timeout token source, forcing ImapClient.Idle() to gracefully exit.
        /// </summary>
        void CancelTimeout () {
            lock (mutex) {
                if (timeout != null) {
                    timeout.Cancel ();
                }
            }
        }
        /// <summary>
        /// Set the timeout source.
        /// </summary>
        /// <param name="source">The timeout source.</param>
        public void SetTimeoutSource ( CancellationTokenSource source ) {
            lock (mutex) {
                timeout = source;
                if (timeout != null && IsCancellationRequested) {
                    timeout.Cancel ();
                }
            }
        }
    }
}