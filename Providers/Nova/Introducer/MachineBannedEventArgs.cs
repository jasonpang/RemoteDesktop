using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Nova;

namespace Providers.Nova.Modules
{
    public class MachineBannedEventArgs : EventArgs
    {
        public Machine BannedMachine { get; set; }
        public uint NumConnectionAttempts { get; set; }
    }
}
