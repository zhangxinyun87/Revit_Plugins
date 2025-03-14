namespace Revit_Plugins
{
    partial class schedule_info_Form1
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
            this.lstb_test1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstb_test1
            // 
            this.lstb_test1.FormattingEnabled = true;
            this.lstb_test1.Location = new System.Drawing.Point(50, 43);
            this.lstb_test1.Name = "lstb_test1";
            this.lstb_test1.Size = new System.Drawing.Size(187, 329);
            this.lstb_test1.TabIndex = 0;
            this.lstb_test1.SelectedIndexChanged += new System.EventHandler(this.lstb_test1_SelectedIndexChanged);
            // 
            // schedule_info_Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstb_test1);
            this.Name = "schedule_info_Form1";
            this.Text = "schedule_info_Form1";
            this.Load += new System.EventHandler(this.schedule_info_Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstb_test1;
    }
}