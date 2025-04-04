
namespace Cursed_Market
{
    partial class Form_Queue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Queue));
            this.label_QueueStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_QueueStatus
            // 
            this.label_QueueStatus.AutoSize = true;
            this.label_QueueStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label_QueueStatus.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_QueueStatus.Location = new System.Drawing.Point(12, 9);
            this.label_QueueStatus.Name = "label_QueueStatus";
            this.label_QueueStatus.Size = new System.Drawing.Size(395, 25);
            this.label_QueueStatus.TabIndex = 0;
            this.label_QueueStatus.Text = "QUEUE POSITION WILL BE SHOWN HERE";
            // 
            // Form_Queue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_QueueStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Queue";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Queue";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Queue_FormClosing);
            this.Load += new System.EventHandler(this.Form_Queue_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_QueueStatus;
    }
}