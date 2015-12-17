using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleChatServer
{
    public partial class ServerStatus : Form
    {
        private delegate void DoItem(string name);

        public ServerStatus()
        {
            InitializeComponent();
            Program.AnnounceOnline += Program_AnnounceOnline;
            Program.AnnounceOffline += Program_AnnounceOffline;
            this.FormClosed += ServerStatus_FormClosed;
        }

        private void ServerStatus_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.CloseSocket();
            Application.Exit();
        }

        private void Program_AnnounceOffline(string name)
        {
            if (UserList.InvokeRequired)
                this.Invoke(new DoItem(DoOffline), name);
            else
                DoOffline(name);
        }

        private void DoOffline(string name)
        {
            UserList.Items.Remove(name);
        }

        private void Program_AnnounceOnline(string name)
        {
            if (UserList.InvokeRequired)
                this.Invoke(new DoItem(DoOnline), name);
            else
                DoOnline(name);
        }

        private void DoOnline(string name)
        {
            UserList.Items.Add(name);
        }
    }
}
