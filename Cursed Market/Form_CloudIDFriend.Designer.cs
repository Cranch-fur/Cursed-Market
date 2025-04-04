
namespace Cursed_Market
{
    partial class Form_CloudIDFriend
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_CloudIDFriend));
            this.panel_WindowHeader = new System.Windows.Forms.Panel();
            this.button_WindowClose = new System.Windows.Forms.Button();
            this.textBox_MyCloudID = new System.Windows.Forms.TextBox();
            this.label_Title = new System.Windows.Forms.Label();
            this.label_MyCloudIDTitle = new System.Windows.Forms.Label();
            this.button_MyCloudIDCopy = new System.Windows.Forms.Button();
            this.button_FriendCloudIDSend = new System.Windows.Forms.Button();
            this.label_FriendCloudIDTitle = new System.Windows.Forms.Label();
            this.textBox_FriendCloudID = new System.Windows.Forms.TextBox();
            this.pictureBox_WindowBorder = new System.Windows.Forms.PictureBox();
            this.panel_WindowHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_WindowBorder)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_WindowHeader
            // 
            this.panel_WindowHeader.BackColor = System.Drawing.SystemColors.Control;
            this.panel_WindowHeader.Controls.Add(this.button_WindowClose);
            this.panel_WindowHeader.Location = new System.Drawing.Point(0, 0);
            this.panel_WindowHeader.Name = "panel_WindowHeader";
            this.panel_WindowHeader.Size = new System.Drawing.Size(500, 23);
            this.panel_WindowHeader.TabIndex = 4;
            this.panel_WindowHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_WindowHeader_MouseDown);
            // 
            // button_WindowClose
            // 
            this.button_WindowClose.BackColor = System.Drawing.Color.IndianRed;
            this.button_WindowClose.FlatAppearance.BorderSize = 0;
            this.button_WindowClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_WindowClose.ForeColor = System.Drawing.Color.White;
            this.button_WindowClose.Location = new System.Drawing.Point(476, 0);
            this.button_WindowClose.Name = "button_WindowClose";
            this.button_WindowClose.Size = new System.Drawing.Size(24, 23);
            this.button_WindowClose.TabIndex = 0;
            this.button_WindowClose.Text = "X";
            this.button_WindowClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_WindowClose.UseVisualStyleBackColor = false;
            this.button_WindowClose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_WindowClose_MouseClick);
            // 
            // textBox_MyCloudID
            // 
            this.textBox_MyCloudID.Font = new System.Drawing.Font("Roboto", 8.25F);
            this.textBox_MyCloudID.Location = new System.Drawing.Point(102, 89);
            this.textBox_MyCloudID.Name = "textBox_MyCloudID";
            this.textBox_MyCloudID.ReadOnly = true;
            this.textBox_MyCloudID.Size = new System.Drawing.Size(296, 21);
            this.textBox_MyCloudID.TabIndex = 5;
            // 
            // label_Title
            // 
            this.label_Title.BackColor = System.Drawing.Color.Transparent;
            this.label_Title.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Title.Location = new System.Drawing.Point(12, 37);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(477, 18);
            this.label_Title.TabIndex = 10;
            this.label_Title.Text = "CLOUD-ID FRIEND REQUEST";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_MyCloudIDTitle
            // 
            this.label_MyCloudIDTitle.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_MyCloudIDTitle.Location = new System.Drawing.Point(16, 66);
            this.label_MyCloudIDTitle.Name = "label_MyCloudIDTitle";
            this.label_MyCloudIDTitle.Size = new System.Drawing.Size(473, 20);
            this.label_MyCloudIDTitle.TabIndex = 11;
            this.label_MyCloudIDTitle.Text = "Your Cloud-ID";
            this.label_MyCloudIDTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_MyCloudIDTitle.UseMnemonic = false;
            // 
            // button_MyCloudIDCopy
            // 
            this.button_MyCloudIDCopy.BackColor = System.Drawing.Color.DimGray;
            this.button_MyCloudIDCopy.FlatAppearance.BorderSize = 0;
            this.button_MyCloudIDCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_MyCloudIDCopy.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_MyCloudIDCopy.ForeColor = System.Drawing.Color.White;
            this.button_MyCloudIDCopy.Location = new System.Drawing.Point(377, 89);
            this.button_MyCloudIDCopy.Name = "button_MyCloudIDCopy";
            this.button_MyCloudIDCopy.Size = new System.Drawing.Size(21, 21);
            this.button_MyCloudIDCopy.TabIndex = 34;
            this.button_MyCloudIDCopy.Text = "⧉";
            this.button_MyCloudIDCopy.UseVisualStyleBackColor = false;
            this.button_MyCloudIDCopy.Visible = false;
            this.button_MyCloudIDCopy.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_MyCloudIDCopy_MouseClick);
            // 
            // button_FriendCloudIDSend
            // 
            this.button_FriendCloudIDSend.BackColor = System.Drawing.Color.DimGray;
            this.button_FriendCloudIDSend.FlatAppearance.BorderSize = 0;
            this.button_FriendCloudIDSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_FriendCloudIDSend.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_FriendCloudIDSend.ForeColor = System.Drawing.Color.White;
            this.button_FriendCloudIDSend.Location = new System.Drawing.Point(377, 155);
            this.button_FriendCloudIDSend.Name = "button_FriendCloudIDSend";
            this.button_FriendCloudIDSend.Size = new System.Drawing.Size(21, 21);
            this.button_FriendCloudIDSend.TabIndex = 37;
            this.button_FriendCloudIDSend.Text = "📨";
            this.button_FriendCloudIDSend.UseVisualStyleBackColor = false;
            this.button_FriendCloudIDSend.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_FriendCloudIDSend_MouseClick);
            // 
            // label_FriendCloudIDTitle
            // 
            this.label_FriendCloudIDTitle.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_FriendCloudIDTitle.Location = new System.Drawing.Point(16, 132);
            this.label_FriendCloudIDTitle.Name = "label_FriendCloudIDTitle";
            this.label_FriendCloudIDTitle.Size = new System.Drawing.Size(473, 20);
            this.label_FriendCloudIDTitle.TabIndex = 36;
            this.label_FriendCloudIDTitle.Text = "Cloud-ID to Send a Friend Request to";
            this.label_FriendCloudIDTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_FriendCloudIDTitle.UseMnemonic = false;
            // 
            // textBox_FriendCloudID
            // 
            this.textBox_FriendCloudID.Font = new System.Drawing.Font("Roboto", 8.25F);
            this.textBox_FriendCloudID.Location = new System.Drawing.Point(102, 155);
            this.textBox_FriendCloudID.Name = "textBox_FriendCloudID";
            this.textBox_FriendCloudID.Size = new System.Drawing.Size(275, 21);
            this.textBox_FriendCloudID.TabIndex = 35;
            this.textBox_FriendCloudID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox_FriendCloudID_MouseClick);
            // 
            // pictureBox_WindowBorder
            // 
            this.pictureBox_WindowBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_WindowBorder.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_WindowBorder.Name = "pictureBox_WindowBorder";
            this.pictureBox_WindowBorder.Size = new System.Drawing.Size(500, 200);
            this.pictureBox_WindowBorder.TabIndex = 38;
            this.pictureBox_WindowBorder.TabStop = false;
            // 
            // Form_CloudIDFriend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 200);
            this.Controls.Add(this.button_FriendCloudIDSend);
            this.Controls.Add(this.label_FriendCloudIDTitle);
            this.Controls.Add(this.textBox_FriendCloudID);
            this.Controls.Add(this.button_MyCloudIDCopy);
            this.Controls.Add(this.label_MyCloudIDTitle);
            this.Controls.Add(this.label_Title);
            this.Controls.Add(this.textBox_MyCloudID);
            this.Controls.Add(this.panel_WindowHeader);
            this.Controls.Add(this.pictureBox_WindowBorder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_CloudIDFriend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cloud-ID Friend Request";
            this.Shown += new System.EventHandler(this.Form_AddFriend_Shown);
            this.panel_WindowHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_WindowBorder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_WindowHeader;
        private System.Windows.Forms.Button button_WindowClose;
        private System.Windows.Forms.TextBox textBox_MyCloudID;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Label label_MyCloudIDTitle;
        private System.Windows.Forms.Button button_MyCloudIDCopy;
        private System.Windows.Forms.Button button_FriendCloudIDSend;
        private System.Windows.Forms.Label label_FriendCloudIDTitle;
        private System.Windows.Forms.TextBox textBox_FriendCloudID;
        private System.Windows.Forms.PictureBox pictureBox_WindowBorder;
    }
}