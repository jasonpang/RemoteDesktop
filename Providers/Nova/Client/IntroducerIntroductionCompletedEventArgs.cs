using System;
using Network.Messages.Nova;

namespace Providers.Nova.Modules
{
    public class IntroducerIntroductionCompletedEventArgs : EventArgs
    {
        public ResponseIntroducerIntroductionCompletedMessage.Reason DenyReason { get; set; }
        public ResponseIntroducerIntroductionCompletedMessage.Result Result { get; set; }
    }
}