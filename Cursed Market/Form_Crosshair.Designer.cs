
namespace Cursed_Market
{
    partial class Form_Crosshair
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Crosshair));
            this.pictureBox_Crosshair = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Crosshair)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Crosshair
            // 
            this.pictureBox_Crosshair.Image = global::Cursed_Market.Properties.Resources.IMG_CROSSHAIR_DEFAULT_CIRCLE_AQUA;
            this.pictureBox_Crosshair.Location = new System.Drawing.Point(640, 176);
            this.pictureBox_Crosshair.Name = "pictureBox_Crosshair";
            this.pictureBox_Crosshair.Size = new System.Drawing.Size(512, 512);
            this.pictureBox_Crosshair.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_Crosshair.TabIndex = 0;
            this.pictureBox_Crosshair.TabStop = false;
            // 
            // Form_Crosshair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.pictureBox_Crosshair);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Crosshair";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crosshair";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Crosshair_FormClosing);
            this.Load += new System.EventHandler(this.Form_Crosshair_Load);
            this.Shown += new System.EventHandler(this.Form_Crosshair_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Crosshair)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Crosshair;
    }
}