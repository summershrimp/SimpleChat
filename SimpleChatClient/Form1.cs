using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using SimpleChatCommon.Messages;

namespace SimpleChatClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

		private void btnNewChat_Click(object sender, EventArgs e)
		{
			frmInput input_form = new frmInput();
			if (input_form.ShowDialog(this) == DialogResult.OK)
			{
				try
				{
					string server = input_form.serverInfo();
					string nickname = input_form.nickname();
					Program.Connect(server);
					Program.setNickname(nickname);
					Program.SendLogin();
					lblServerInfo.Text = "当前聊天服务器：" + server;
					lblUserInfo.Text = "当前昵称：" + nickname;
					txtInput.ReadOnly = false;
					timer1.Enabled = true;
				}
				catch
				{
					lblStatus.Text = "服务器连接失败:(";
				}
			}
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			string text = txtInput.Text;
			try
			{
				if (lblMsgType.Text == "全体消息")
				{
					Program.SendPublic(text);
				}
				else
				{
					string user = lblMsgType.Text.Substring(2, lblMsgType.Text.Length - 3);
					Program.SendPrivate(user, text);
				}
				if (txtMsg.Text != "")
				{
					txtMsg.Text += "\n";
				}
				txtMsg.Text += lblUserInfo.Text.Substring("当前昵称：".Length) + "：\n" + text;
			}
			catch
			{
				string textEllipse = text.Substring(0, 20);
				if (text.Length > 20)
				{
					textEllipse += "...";
				}
                lblStatus.Text = "消息 [" + textEllipse + "] 发送失败:(";
			}
			txtInput.Text = "";
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			BaseMessage pmsg = Program.Receive();
			Console.Out.WriteLine(pmsg.MsgType);
			if (txtMsg.Text != "")
			{
				txtMsg.Text += "\n";
			}
			txtMsg.Text += pmsg;
		}

		private void txtInput_TextChanged(object sender, EventArgs e)
		{
			string text = txtInput.Text;
			if (text.Length == 0)
			{
				lblMsgType.Text = "全体消息";
				return;
			}
			if (text[0] == '@')
			{
				int i;
				for (i = 1; i < text.Length; ++i)
				{
					if (text[i] == ' ' || text[i] == '\t' || text[i] == '\r')
					{
						break;
					}
				}
				string user = text.Substring(1, i - 1);
				lblMsgType.Text = "@[" + user + "]";
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			if (txtInput.Text == "")
			{
				txtMsg.Text = "";
			}
			else
			{
				txtInput.Text = "";
			}
		}
	}
}
