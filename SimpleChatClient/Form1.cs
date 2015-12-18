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
using SimpleChatCommon;
namespace SimpleChatClient
{
    public partial class Form1 : Form
    {
        System.Threading.Thread receiveThread;
		string server, nickname;

		public Form1()
        {
            InitializeComponent();
            receiveThread = new System.Threading.Thread(ReceiveThread);
            this.FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Disconnect();
            receiveThread.Abort();
            Application.Exit();
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
                    receiveThread.Start();
					statusSuccess("服务器连接成功。");
				}
				catch(SocketException)
				{
                    receiveThread.Abort();
					statusError("服务器连接失败:(");
				}
			}
		}
        private delegate void OnReceive(BaseMessage msg);
        void ReceiveThread()
        {
            while (true)
            {
                try
                {
                    BaseMessage msg = Program.Receive();
                    DoReceive(msg);
                }
                catch (SocketException)
                {
                    break;
                }
                catch (ChatException e)
                {
                    DoReceive(new PrivateMessage("System", nickname, e.Message));
                }
                catch (System.Threading.ThreadAbortException)
                {
                    break;
                }
            }
        }
        void DoReceive(BaseMessage msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new OnReceive(DoReceive), msg);
            }
            else
            {

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
