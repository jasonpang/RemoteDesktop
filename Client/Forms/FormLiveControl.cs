using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Managers;
using Managers.LiveControl.Client;
using Managers.Nova.Client;
using Model.LiveControl;
using Network.Messages.LiveControl;

namespace Client.Forms
{
    public partial class FormLiveControl : Form
    {
        public LiveControlManager LiveControlManager { get { return NovaClient.Instance.LiveControlManager; } }

        public FormLiveControl()
        {
            LiveControlManager.OnScreenshotReceived += new EventHandler<Providers.LiveControl.Client.ScreenshotMessageEventArgs>(LiveControlManager_OnScreenshotReceived);

            InitializeComponent();
        }

        void LiveControlManager_OnScreenshotReceived(object sender, Providers.LiveControl.Client.ScreenshotMessageEventArgs e)
        {
            var screenshot = e.Screenshot;

            using (var stream = new MemoryStream(screenshot.Image))
            {
                Image image = Image.FromStream(stream);

                gdiScreen1.Draw(image, screenshot.Region);
            }

            LiveControlManager.RequestScreenshot();

            Trace.WriteLine(String.Format("Processed Image #{0}, Size: {1} KB", e.Screenshot.Number, GetKBFromBytes(e.Screenshot.Image.Length)));
        }

        private static float GetKBFromBytes(long bytes)
        {
            return (float)((float)bytes / (float)1024);
        }

        private static float GetTotalScreenshotsKB(List<Screenshot> screenshots)
        {
            float total = 0f;
            screenshots.ForEach((x) =>
            {
                total += GetKBFromBytes(x.Image.Length);
            });
            return total;
        }

        private void ButtonRequestScreenshot_Click(object sender, EventArgs e)
        {
            LiveControlManager.RequestScreenshot();
        }
    }
}
