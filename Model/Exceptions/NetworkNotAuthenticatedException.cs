using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Exceptions
{
    /// <summary>
    /// Occurs when attempting to send connected messages before a Network has been authenticated.
    /// </summary>
    public class NetworkNotAuthenticatedException : Exception
    {
        public NetworkNotAuthenticatedException()
        {
            
        }
        public NetworkNotAuthenticatedException(string message)
            : base(message)
        {
            
        }
        public NetworkNotAuthenticatedException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}
