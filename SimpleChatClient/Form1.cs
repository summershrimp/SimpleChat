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
		System.Timers.Timer Timers_Timer = new System.Timers.Timer();
		string server, nickname;

		public Form1()
        {
            InitializeComponent();
        }

		private void statusError(string msg)
		{
			lblStatus.ForeColor = Color.FromArgb(255, 50, 50);
			lblStatus.Text = msg;
        }

		private void statusSuccess(string msg = "")
		{
			if (msg == "")
			{
				msg = "就绪。";
			}
			lblStatus.ForeColor = Color.FromArgb(255, 255, 255);
			lblStatus.Text = msg;
		}

		private void btnNewChat_Click(object sender, EventArgs e)
		{
			frmInput input_form = new frmInput();
			if (input_form.ShowDialog(this) == DialogResult.OK)
			{
				try
				{
					server = input_form.serverInfo();
					nickname = input_form.nickname();
					Program.Connect(server);
					Program.setNickname(nickname);
					Program.SendLogin();
					lblServerInfo.Text = "当前聊天服务器：" + server;
					lblUserInfo.Text = "当前昵称：" + nickname;
					txtInput.ReadOnly = false;
					Timers_Timer.Interval = 500;
					Timers_Timer.Enabled = true;
					Timers_Timer.Elapsed += new System.Timers.ElapsedEventHandler(Timers_Timer_Elapsed);
					statusSuccess("服务器连接成功。");
				}
				catch
				{
					statusError("服务器连接失败:(");
				}
			}
		}

		void Timers_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				BaseMessage msg = Program.Receive();
				if (msg.MsgType == "error")
				{
					ErrorMessage emsg = (ErrorMessage)msg;
					MessageBox.Show(emsg.ErrorStr);
				}
				else
				{
					PublicMessage pbmsg;
					PrivateMessage prmsg;
					switch (msg.MsgType)
					{
						case "public":
							pbmsg = (PublicMessage)msg;
							if (txtMsg.Text != "")
							{
								txtMsg.Text += "\n";
							}
							txtMsg.Text += " -- " + pbmsg.FromNick + " 说：\n" + pbmsg.Content;
							break;
						case "private":
							prmsg = (PrivateMessage)msg;
							if (prmsg.ToNick == prmsg.FromNick)
							{
								return;
							}
							else if (prmsg.ToNick == nickname)
							{
								prmsg.ToNick = "你";
							}
							else
							{
								prmsg.ToNick = ' ' + prmsg.ToNick + ' ';
							}
							if (txtMsg.Text != "")
							{
								txtMsg.Text += "\n";
							}
							txtMsg.Text += " -- " + prmsg.FromNick + " 对" + prmsg.ToNick + "说：\n" + prmsg.Content;
							break;
					}
				}
			}
			catch { }
		}
		private void btnSend_Click(object sender, EventArgs e)
		{
			string text = txtInput.Text;
			if (text == "")
			{
				return;
			}
			try
			{
				if (txtMsg.Text != "")
				{
					txtMsg.Text += "\n";
				}
				if (lblMsgType.Text == "全体消息")
				{
					Program.SendPublic(text);
					txtMsg.Text += " -- " + lblUserInfo.Text.Substring("当前昵称：".Length) + " 说：\n" + text;
				}
				else
				{
					string user = lblMsgType.Text.Substring(2, lblMsgType.Text.Length - 3);
					Program.SendPrivate(user, text);
					string said;
					if (user == nickname)
					{
						said = "你自言自语：\n";
					}
					else
					{
						said = lblUserInfo.Text.Substring("当前昵称：".Length) + " 对 " + user + " 说：\n";
                    }
					txtMsg.Text += " -- " + said + text;
				}
				statusSuccess();
			}
			catch
			{
				string textEllipse = text.Substring(0, 20);
				if (text.Length > 20)
				{
					textEllipse += "...";
				}
                statusError("消息 [" + textEllipse + "] 发送失败:(");
			}
			txtInput.Text = "";
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
