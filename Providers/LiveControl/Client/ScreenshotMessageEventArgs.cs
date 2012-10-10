using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Model.LiveControl;

namespace Providers.LiveControl.Client
{
    public class ScreenshotMessageEventArgs : EventArgs
    {
        public Screenshot Screenshot { get; set; }
    }
}
