namespace SimpleChatClient
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.btnNewChat = new System.Windows.Forms.Button();
			this.txtInput = new System.Windows.Forms.TextBox();
			this.txtMsg = new System.Windows.Forms.RichTextBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.lblServerInfo = new System.Windows.Forms.Label();
			this.lblUserInfo = new System.Windows.Forms.Label();
			this.lblStatus = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnClear = new System.Windows.Forms.Button();
			this.lblMsgType = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnNewChat
			// 
			this.btnNewChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
			this.btnNewChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnNewChat.Location = new System.Drawing.Point(9, 397);
			this.btnNewChat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnNewChat.Name = "btnNewChat";
			this.btnNewChat.Size = new System.Drawing.Size(87, 33);
			this.btnNewChat.TabIndex = 0;
			this.btnNewChat.TabStop = false;
			this.btnNewChat.Text = "新聊天(&N)";
			this.btnNewChat.UseVisualStyleBackColor = false;
			this.btnNewChat.Click += new System.EventHandler(this.btnNewChat_Click);
			// 
			// txtInput
			// 
			this.txtInput.AcceptsReturn = true;
			this.txtInput.AcceptsTab = true;
			this.txtInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
			this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtInput.ForeColor = System.Drawing.Color.White;
			this.txtInput.HideSelection = false;
			this.txtInput.Location = new System.Drawing.Point(15, 290);
			this.txtInput.Multiline = true;
			this.txtInput.Name = "txtInput";
			this.txtInput.ReadOnly = true;
			this.txtInput.Size = new System.Drawing.Size(595, 96);
			this.txtInput.TabIndex = 1;
			this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
			// 
			// txtMsg
			// 
			this.txtMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
			this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMsg.ForeColor = System.Drawing.Color.White;
			this.txtMsg.Location = new System.Drawing.Point(15, 33);
			this.txtMsg.Name = "txtMsg";
			this.txtMsg.ReadOnly = true;
			this.txtMsg.Size = new System.Drawing.Size(595, 240);
			this.txtMsg.TabIndex = 2;
			this.txtMsg.Text = "";
			// 
			// btnSend
			// 
			this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
			this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSend.Location = new System.Drawing.Point(540, 397);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(75, 34);
			this.btnSend.TabIndex = 3;
			this.btnSend.TabStop = false;
			this.btnSend.Text = "发送(&S)";
			this.btnSend.UseVisualStyleBackColor = false;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// lblServerInfo
			// 
			this.lblServerInfo.Location = new System.Drawing.Point(9, 7);
			this.lblServerInfo.Name = "lblServerInfo";
			this.lblServerInfo.Size = new System.Drawing.Size(314, 17);
			this.lblServerInfo.TabIndex = 5;
			this.lblServerInfo.Text = "当前聊天服务器：无";
			this.lblServerInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblUserInfo
			// 
			this.lblUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblUserInfo.Location = new System.Drawing.Point(329, 7);
			this.lblUserInfo.Name = "lblUserInfo";
			this.lblUserInfo.Size = new System.Drawing.Size(286, 17);
			this.lblUserInfo.TabIndex = 6;
			this.lblUserInfo.Text = "当前昵称：空";
			this.lblUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(12, 6);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(44, 17);
			this.lblStatus.TabIndex = 7;
			this.lblStatus.Text = "就绪。";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(26)))));
			this.panel1.Controls.Add(this.lblStatus);
			this.panel1.Location = new System.Drawing.Point(-3, 437);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(630, 36);
			this.panel1.TabIndex = 8;
			// 
			// btnClear
			// 
			this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
			this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClear.Location = new System.Drawing.Point(459, 397);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 34);
			this.btnClear.TabIndex = 4;
			this.btnClear.TabStop = false;
			this.btnClear.Text = "清空(&C)";
			this.btnClear.UseVisualStyleBackColor = false;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// lblMsgType
			// 
			this.lblMsgType.Location = new System.Drawing.Point(102, 397);
			this.lblMsgType.Name = "lblMsgType";
			this.lblMsgType.Size = new System.Drawing.Size(351, 33);
			this.lblMsgType.TabIndex = 9;
			this.lblMsgType.Text = "全体消息";
			this.lblMsgType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(624, 467);
			this.Controls.Add(this.lblMsgType);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.lblUserInfo);
			this.Controls.Add(this.lblServerInfo);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.txtMsg);
			this.Controls.Add(this.txtInput);
			this.Controls.Add(this.btnNewChat);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
			this.Text = "简易聊天室";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private System.Windows.Forms.Button btnNewChat;
		private System.Windows.Forms.TextBox txtInput;
		private System.Windows.Forms.RichTextBox txtMsg;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.Label lblServerInfo;
		private System.Windows.Forms.Label lblUserInfo;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Label lblMsgType;
	}
}

