using System;
using Model.Nova;

namespace Providers.Nova.Modules
{
    public class IntroductionCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Machine A.
        /// </summary>
        public Machine ClientMachine { get; set; }

        /// <summary>
        /// Machine B.
        /// </summary>
        public Machine ServerMachine { get; set; }
    }
}