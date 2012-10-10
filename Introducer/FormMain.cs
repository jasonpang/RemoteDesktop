using System;
using System.Windows.Forms;
using Managers.Nova;
using Managers.Nova.Introducer;
using Providers.Nova.Modules;
using Network;
using Managers;

namespace Introducer
{
    public partial class FormMain : Form
    {
        private NovaManager NovaManager { get { return NovaIntroducer.Instance.NovaManager; } }

        public FormMain()
        {
            NovaManager.OnMachineRegistered += new EventHandler<MachineRegisteredEventArgs>(OnMachineRegistered);
            NovaManager.OnIntroductionCompleted += new EventHandler<IntroductionCompletedEventArgs>(OnIntroductionCompleted);
            NovaManager.OnWrongPassword += new EventHandler<WrongPasswordEventArgs>(IntroducerManager_OnWrongPassword);
            NovaManager.OnMachineBanned += new EventHandler<MachineBannedEventArgs>(IntroducerManager_OnMachineBanned);
            NovaManager.OnKeepAliveReceived += new EventHandler<EventArgs>(IntroducerManager_OnKeepAliveReceived);

            InitializeComponent();
        }

        void IntroducerManager_OnKeepAliveReceived(object sender, EventArgs e)
        {
            //LogText(String.Format("Received KeepAlive message."));
        }

        void IntroducerManager_OnMachineBanned(object sender, MachineBannedEventArgs e)
        {
            LabelNumMachinesBanned.Text = NovaManager.Provider.MachineBanTable.Count.ToString();

            LogText(String.Format("{0} ({1}) has been banned for {2} wrong connection attempts.", e.BannedMachine.PublicEndPoint, e.BannedMachine.Identity, e.NumConnectionAttempts));
        }

        void IntroducerManager_OnWrongPassword(object sender, WrongPasswordEventArgs e)
        {
            LogText(String.Format("{0} has provided an incorrect password {1} times to {2} ({3}).", e.OffendingMachine.PublicEndPoint, e.NumConnectionAttempts, e.TargetMachine.NovaId, e.TargetMachine.PublicEndPoint));
        }

        private void OnIntroductionCompleted(object sender, IntroductionCompletedEventArgs e)
        {
            LabelNumMachinesIntroduced.Text = NovaManager.Provider.NumMachinesIntroduced.ToString();

            LogText(String.Format("Introduced {0} to {1} ({2}).", e.ClientMachine.PublicEndPoint, e.ServerMachine.NovaId,
            e.ServerMachine.PublicEndPoint));
        }

        private void OnMachineRegistered(object sender, MachineRegisteredEventArgs e)
        {
            LabelNumMachinesRegistered.Text = NovaManager.Provider.NumMachinesRegistered.ToString();

            LogText(String.Format("Registered {0} as {1}.", e.Machine.PublicEndPoint, e.Machine.NovaId));
        }

        private void LogText(string text)
        {
            RichTextBoxLog.AppendText(String.Format(DateTime.Now.ToString() + " {0}" + Environment.NewLine, text));
        }

        private void MenuFile_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuIntroducer_Start_Click(object sender, EventArgs e)
        {
            if (NovaManager.Network.Status == NetworkStatus.Running)
            {
                MessageBox.Show("Already running.", "Unable to Start", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                NovaManager.Restart();
                LogText("Restarted Introducer service.");
            }
        }

        private void MenuIntroducer_Stop_Click(object sender, EventArgs e)
        {
            if (NovaManager.Network.Status != NetworkStatus.Running)
            {
                MessageBox.Show("Not running.", "Unable to Stop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (NovaManager.Network.Status == NetworkStatus.ShutdownRequested)
            {
                MessageBox.Show("Already shutting down.", "Unable to Stop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                NovaManager.Stop();
                LogText("Stopped Introducer service.");
            }
        }

        private void MenuIntroducer_Status_Click(object sender, EventArgs e)
        {
            LogText("Network Status: " + NovaManager.Network.Status.ToString());
        }
    }
}
