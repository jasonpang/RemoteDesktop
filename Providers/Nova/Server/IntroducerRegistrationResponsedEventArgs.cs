using System;

namespace Providers.Nova.Modules
{
    public class IntroducerRegistrationResponsedEventArgs : EventArgs
    {
        public enum Result
        {
            Denied,
            Succeeded,
        }

        /// <summary>
        /// The unformatted Nova ID returned from the Introducer.
        /// </summary>
        public string NovaId { get; set; }

        public Result RegistrationResult { get; set; }
    }
}