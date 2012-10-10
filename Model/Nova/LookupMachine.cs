using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Nova
{
    public class LookupMachine
    {
        /// <summary>
        /// The total number of connection attempts to this machine.
        /// </summary>
        /// <remarks>Resets on each successful connection.</remarks>
        public uint NumConnectionAttempts { get; set; }

        public Machine Machine { get; private set; }

        public LookupMachine(Machine machine)
        {
            Machine = machine;
        }
    }
}
