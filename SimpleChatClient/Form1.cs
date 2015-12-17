using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
					String server = input_form.serverInfo();
					String nickname = input_form.nickname();
					// 这里链接服务器
					//
					lblServerInfo.Text = "当前聊天服务器：" + server;
					lblUserInfo.Text = "当前昵称：" + nickname;
				}
				catch
				{
					MessageBox.Show("设置失败:(");
				}
			}
		}
	}
}
