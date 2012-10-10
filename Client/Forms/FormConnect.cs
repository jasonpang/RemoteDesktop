using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Managers;
using Managers.Nova.Client;
using Model.Extensions;
using Network;
using Network.Messages.Nova;
using Providers.Nova.Modules;

namespace Client.Forms
{
    public partial class FormConnect : Form
    {
        public NovaManager NovaManager { get { return NovaClient.Instance.NovaManager; } }

        public FormConnect()
        {
            NovaManager.OnIntroductionCompleted += new EventHandler<IntroducerIntroductionCompletedEventArgs>(ClientManager_OnIntroductionCompleted);
            NovaManager.OnNatTraversalSucceeded += new EventHandler<NatTraversedEventArgs>(ClientManager_OnNatTraversalSucceeded);
            NovaManager.OnConnected += new EventHandler<ConnectedEventArgs>(ClientManager_OnConnected);

            InitializeComponent();
        }

        private async void ButtonConnect_Click(object sender, EventArgs e)
        {
            await NovaManager.RequestIntroductionAsTask(TextBox_Id.Text, TextBox_Password.Text);
        }

        void ClientManager_OnConnected(object sender, ConnectedEventArgs e)
        {
            ButtonConnect.Set(() => ButtonConnect.Text, "Connected.");

            this.Dispose();
        }

        private void ClientManager_OnIntroductionCompleted(object sender, IntroducerIntroductionCompletedEventArgs e)
        {
            switch (e.Result)
            {
                case Network.Messages.Nova.ResponseIntroducerIntroductionCompletedMessage.Result.Allowed:
                    // Do nothing, expect OnNatTraversalSucceeded() to be raised shortly
                    break;

                case Network.Messages.Nova.ResponseIntroducerIntroductionCompletedMessage.Result.Denied:
                    switch (e.DenyReason)
                    {
                        case ResponseIntroducerIntroductionCompletedMessage.Reason.WrongPassword:
                            TextBox_Password.Set(() => TextBox_Password.Text, String.Empty); // clear the password box for re-entry
                            MessageBox.Show("Please enter the correct password.");
                            break;
                        case ResponseIntroducerIntroductionCompletedMessage.Reason.Banned:
                            MessageBox.Show("You have been banned for trying to connect too many times.");
                            break;
                    }
                    break;
            }
        }

        private void ClientManager_OnNatTraversalSucceeded(object sender, NatTraversedEventArgs e)
        {
            ButtonConnect.Set(() => ButtonConnect.Text, "Connecting to " + TextBox_Id.Text + " ...");
        }
    }
}
