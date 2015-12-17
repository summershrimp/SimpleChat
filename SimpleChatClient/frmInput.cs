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
	public partial class frmInput : Form
	{
		public frmInput()
		{
			InitializeComponent();
		}

		private void frmInput_Load(object sender, EventArgs e)
		{

		}

		public String serverInfo()
		{
			if (txtServer.Text != "")
			{
				return txtServer.Text;
			}
			return "";
		}

		public String nickname()
		{
			if (txtNickname.Text != "")
			{
				return txtNickname.Text;
			}
			return "";
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtServer.Text != "")
				{
					if (txtNickname.Text != "")
					{
						this.DialogResult = DialogResult.OK;
					}
					else
					{
						MessageBox.Show("昵称不能为空！");
					}
				}
				else
				{
					MessageBox.Show("服务器地址不能为空！");
				}
			}
			catch
			{

			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
