using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Providers;
using Network;
using Nini.Config;
using System.Threading.Tasks;
using Providers.Nova.Modules;
using Managers.Extensions;

namespace Managers
{
    /// <summary>
    /// Sits under the actual business logic layer, on top of the Provider layer, and wraps the semi-functional events into a functional event (i.e. OnFileTransferPartiallyCompleted, OnFileChecksumVerified into OnFileTransferCompleted). The manager layer is also responsible for exposing usable functions, such as DownloadFileAsync, and DownloadFileAsTask.
    /// </summary>
    /// <remarks>The XEventArgs belong to this layer, not to the Provider layer. The Manager class should have no modules, because there shouldn't be that much functionality to break up.</remarks>
    public abstract class Manager<T>
        where T : Provider
    {
        /// <summary>
        /// Gets the underlying provider this manager abstracts functionality from.
        /// </summary>
        public virtual T Provider { get; private set; }

        /// <summary>
        /// Gets the underlying network transport.
        /// </summary>
        public NetworkPeer Network { get { return Provider.Network; } }

        /// <summary>
        /// Gets the underlying configuration exposed by the provider.
        /// </summary>
        public IConfig Config { get { return Provider.Config; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// </summary>
        /// <param name="provider">The underlying provider this manager abstracts functionality from.</param>
        public Manager(T provider)
        {
            Provider = provider;
            Provider.RegisterMessageHandlers();
        }

        public TaskCompletionSource<T> RegisterAsTask<T>(ref EventHandler<T> eventMethod)
            where T : EventArgs
        {
            TaskCompletionSource<T> tcs = null;
            try
            {
                // Create a new TaskCompletionSource to represent the finished event 
                tcs = new TaskCompletionSource<T>();

                // Add a custom handler for this event that completes TaskCompletionSource
                EventHandler<T> eventHandler = null;
                eventHandler = (s, e) => tcs.TrySetResult(e);
                eventMethod += eventHandler;

                // Remove all completed event handlers
                Delegate[] delegates = eventMethod.GetInvocationList();

                if (delegates.Length > 1)
                {
                    for (int i = 0; i < delegates.Length; i++)
                    {
                        try
                        {
                            dynamic dynamicDelegateTarget = delegates[i].Target;
                            if (dynamicDelegateTarget.tcs.Task.Status == TaskStatus.RanToCompletion)
                            {
                                eventMethod -= (EventHandler<T>) delegates[i];
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch { }

            // Return the uncompleted TaskCompletionSource; using async, the completed TaskCompletionSource will be returned after T event fires
            return tcs;
        }
    }
}
