using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Managers;
using Managers.LiveControl.Server;
using Managers.Nova.Server;
using Model.Extensions;
using Providers.Nova.Modules;
using System.Diagnostics;

namespace Server.Forms
{
    public partial class FormMain : Form
    {
        public NovaManager NovaManager { get { return NovaServer.Instance.NovaManager; } }
        public LiveControlManager LiveControlManager { get { return NovaServer.Instance.LiveControlManager; } }

        public FormMain()
        {
            NovaManager.OnIntroducerRegistrationResponded += NovaManager_OnIntroducerRegistrationResponded;
            NovaManager.OnNewPasswordGenerated += new EventHandler<PasswordGeneratedEventArgs>(ServerManager_OnNewPasswordGenerated);
            NovaManager.Network.OnConnected += new EventHandler<Network.ConnectedEventArgs>(Network_OnConnected);

            InitializeComponent();
        }

        private async void FormMain_Shown(object sender, EventArgs e)
        {
            PasswordGeneratedEventArgs passArgs = await NovaManager.GeneratePassword();
            LabelPassword.Text = passArgs.NewPassword;
            IntroducerRegistrationResponsedEventArgs regArgs = await NovaManager.RegisterWithIntroducerAsTask();
            LabelNovaId.Text = regArgs.NovaId;

            CheckMirrorDriverExists();
        }

        private void CheckMirrorDriverExists()
        {
            if (!LiveControlManager.Provider.DoesMirrorDriverExist())
            {
                /* This error is referring to:
                 
            while (deviceFound = EnumDisplayDevices(null, deviceIndex, ref device, 0))
            {
                if (device.DeviceString == driverName)
                    break;
                deviceIndex++;
            }

            if (!deviceFound) return false;

                 * in MirrorDriver.DesktopMirror.cs. Basically, it enumerates through all of your graphic providers, and it's looking for "DF Mirage Driver", and it can't find it. Check Device Manager to verify that it's been installed (it's under Device Manager -> Display Adapters -> Mirage Driver). If you see it there, most likely you simply have to restart your computer.
                 */

                var dialogResult = MessageBox.Show("You either don't have the DemoForge mirror driver installed, or you haven't restarted your computer after the installation of the mirror driver. Without a mirror driver, this application will not work. The mirror driver is responsible for notifying the application of any changed screen regions and passing the application bitmaps of those changed screen regions. Press 'Yes' to directly download the driver (you'll still have to install it after). You can visit the homepage if you'd like too: http://www.demoforge.com/dfmirage.htm", "Mirror Driver Not Installed", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (dialogResult == DialogResult.Yes)
                {
                    Process.Start("http://www.demoforge.com/tightvnc/dfmirage-setup-2.0.301.exe");
                }
            }
        }

        private void Timer_KeepAlive_Tick(object sender, EventArgs e)
        {
            NovaManager.SendKeepAliveMessage();
        }  

        void Network_OnConnected(object sender, Network.ConnectedEventArgs e)
        {
            LabelStatus.Set(() => LabelStatus.Text, "Connected");
            Timer_KeepAlive.Enabled = false;
        }

        void ServerManager_OnNewPasswordGenerated(object sender, PasswordGeneratedEventArgs e)
        {
            LabelPassword.Set(() => LabelPassword.Text, e.NewPassword);
        }

        private void NovaManager_OnIntroducerRegistrationResponded(object sender, IntroducerRegistrationResponsedEventArgs e)
        {
            LabelNovaId.Set(() => LabelNovaId.Text, e.NovaId);
        }
    }
}
