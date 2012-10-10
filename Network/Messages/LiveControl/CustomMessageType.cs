using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network.Messages.LiveControl
{
    /// <summary>
    /// Defines the Nova protocol message types, for the File Explorer module.
    /// </summary>
    public enum CustomMessageType : ushort
    {
        RequestScreenshotMessage = 5000,
        ResponseBeginScreenshotMessage,
        ResponseScreenshotMessage,
        ResponseEndScreenshotMessage,
        ResponseEmptyScreenshotMessage,
        MouseMoveMessage,
        MouseClickMessage,
        MouseScrollMessage,
        KeyDownMessage,
        KeyPressMessage,
        KeyUpMessage,
    }
}
