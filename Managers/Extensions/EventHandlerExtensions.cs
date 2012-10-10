using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Extensions
{
    public static class EventHandlerExtensions
    {
        public static void RemoveCompletedTaskEventHandlers<T>(this EventHandler<T> self, ref EventHandler<T> handler)
            where T : EventArgs
        {
            Delegate[] delegates = handler.GetInvocationList();

            if (delegates.Length > 1)
            {
                foreach (Delegate del in delegates)
                {
                    dynamic dynamicDelegateTarget = del.Target;
                    //if (((TaskCompletionSource<T>)d.Target).Task.Status == TaskStatus.RanToCompletion)                    
                    if (dynamicDelegateTarget.tcs.Task.Status == TaskStatus.RanToCompletion)
                    {
                        handler -= (EventHandler<T>)del;
                    }
                }
            }
        }
    }
}
