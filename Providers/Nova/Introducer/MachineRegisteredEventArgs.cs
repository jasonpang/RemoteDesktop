using System;
using Model.Nova;

namespace Providers.Nova.Modules
{
    public class MachineRegisteredEventArgs : EventArgs
    {
        public Machine Machine { get; set; }
    }
}