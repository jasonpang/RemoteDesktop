using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network
{
    public class DebugMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the internal library's debug message.
        /// </summary>
        public string Message { get; set; }
    }
}
