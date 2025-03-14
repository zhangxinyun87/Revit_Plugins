namespace Revit_Plugins
{
    partial class refresh_views_Form1
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
            this.labl_tst1 = new System.Windows.Forms.Label();
            this.lstb_test1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // labl_tst1
            // 
            this.labl_tst1.AutoSize = true;
            this.labl_tst1.Location = new System.Drawing.Point(37, 28);
            this.labl_tst1.Name = "labl_tst1";
            this.labl_tst1.Size = new System.Drawing.Size(46, 13);
            this.labl_tst1.TabIndex = 0;
            this.labl_tst1.Text = "labl_tst1";
            this.labl_tst1.Click += new System.EventHandler(this.labl_tst1_Click);
            // 
            // lstb_test1
            // 
            this.lstb_test1.FormattingEnabled = true;
            this.lstb_test1.Location = new System.Drawing.Point(295, 48);
            this.lstb_test1.Name = "lstb_test1";
            this.lstb_test1.Size = new System.Drawing.Size(363, 238);
            this.lstb_test1.TabIndex = 1;
            this.lstb_test1.SelectedIndexChanged += new System.EventHandler(this.lstb_test1_SelectedIndexChanged);
            // 
            // refresh_views__Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstb_test1);
            this.Controls.Add(this.labl_tst1);
            this.Name = "refresh_views__Form1";
            this.Text = "refresh_views__Form1";
            this.Load += new System.EventHandler(this.refresh_views__Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labl_tst1;
        private System.Windows.Forms.ListBox lstb_test1;
    }
}