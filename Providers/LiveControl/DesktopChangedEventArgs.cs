using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Providers.LiveControl
{
    public class DesktopChangedEventArgs : EventArgs
    {
        public Image Screenshot { get; set; }
        public Rectangle Region { get; set; }
    }
}
