namespace Cursed_Market
{
    partial class Form_Wait
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Wait));
            this.label_Wait = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Wait
            // 
            this.label_Wait.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Wait.Location = new System.Drawing.Point(0, 0);
            this.label_Wait.Name = "label_Wait";
            this.label_Wait.Size = new System.Drawing.Size(300, 60);
            this.label_Wait.TabIndex = 0;
            this.label_Wait.Text = "Please wait...";
            this.label_Wait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_Wait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 60);
            this.Controls.Add(this.label_Wait);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::Cursed_Market.Properties.Resources.Icon_Other;
            this.MaximumSize = new System.Drawing.Size(300, 60);
            this.MinimumSize = new System.Drawing.Size(300, 60);
            this.Name = "Form_Wait";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Startup";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Wait_FormClosing);
            this.Load += new System.EventHandler(this.Form_Startup_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Wait;
    }
}