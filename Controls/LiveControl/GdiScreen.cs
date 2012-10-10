using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Controls.LiveControl
{
    public partial class GdiScreen : Control
    {
        private Bitmap Screen { get; set; }

        public GdiScreen()
        {
            InitializeComponent();

            Screen = new Bitmap(1, 1);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawImage(Screen, new Rectangle(Point.Empty, this.Size));
        }

        public void Draw(Image image, Rectangle region)
        {
            if (Screen.Width * Screen.Height < region.Width * region.Height)
            {
                Screen = new Bitmap(region.Width, region.Height);
            }

            var gfx = Graphics.FromImage(Screen);
            gfx.DrawImageUnscaled(image, region);

            this.Invalidate();
        }
    }
}
