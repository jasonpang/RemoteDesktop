using System;

namespace Providers.Nova.Modules
{
    public class PasswordGeneratedEventArgs : EventArgs
    {
        /// <summary>
        /// The newly generated password.
        /// </summary>
        public string NewPassword { get; set; }
    }
}