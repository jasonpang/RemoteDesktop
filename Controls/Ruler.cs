using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Controls
{
    public partial class Ruler : Control
    {
        private const int SS_ETCHEDHORZ = 0x00000010;
        private const int SS_ETCHEDVERT = 0x00000011;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;

        public Ruler()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                p.ClassName = "STATIC";
                p.Style = WS_CHILD | WS_VISIBLE | SS_ETCHEDHORZ;
                return p;
            }
        }
    }
}
