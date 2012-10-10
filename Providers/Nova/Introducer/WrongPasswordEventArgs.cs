using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Nova;

namespace Providers.Nova.Modules
{
    public class WrongPasswordEventArgs : EventArgs
    {
        /// <summary>
        /// The reference to the offending Machine, which provides the IPEndPoint and hardware ID (and more).
        /// </summary>
        public Machine OffendingMachine { get; set; }

        /// <summary>
        /// The total number of times a connection was attempted.
        /// </summary>
        /// <remarks>Resets on each successful connection.</remarks>
        public uint NumConnectionAttempts { get; set; }

        /// <summary>
        /// The machine the OffendingMachine was trying to connect to.
        /// </summary>
        public Machine TargetMachine { get; set; }
    }
}
