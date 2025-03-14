namespace Revit_Plugins
{
    partial class copy_position_Form1
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
            this.loc_file_cmbx01 = new System.Windows.Forms.ComboBox();
            this.from_file_cmbx01 = new System.Windows.Forms.ComboBox();
            this.loc_file_dtgrd1 = new System.Windows.Forms.DataGridView();
            this.from_file_dtgrd2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.loc_file_dtgrd1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.from_file_dtgrd2)).BeginInit();
            this.SuspendLayout();
            // 
            // loc_file_cmbx01
            // 
            this.loc_file_cmbx01.FormattingEnabled = true;
            this.loc_file_cmbx01.Location = new System.Drawing.Point(35, 35);
            this.loc_file_cmbx01.Name = "loc_file_cmbx01";
            this.loc_file_cmbx01.Size = new System.Drawing.Size(291, 21);
            this.loc_file_cmbx01.TabIndex = 0;
            this.loc_file_cmbx01.SelectedIndexChanged += new System.EventHandler(this.loc_file_cmbx01_SelectedIndexChanged);
            // 
            // from_file_cmbx01
            // 
            this.from_file_cmbx01.FormattingEnabled = true;
            this.from_file_cmbx01.Location = new System.Drawing.Point(363, 35);
            this.from_file_cmbx01.Name = "from_file_cmbx01";
            this.from_file_cmbx01.Size = new System.Drawing.Size(291, 21);
            this.from_file_cmbx01.TabIndex = 1;
            this.from_file_cmbx01.SelectedIndexChanged += new System.EventHandler(this.from_file_cmbx01_SelectedIndexChanged);
            // 
            // loc_file_dtgrd1
            // 
            this.loc_file_dtgrd1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.loc_file_dtgrd1.Location = new System.Drawing.Point(35, 95);
            this.loc_file_dtgrd1.Name = "loc_file_dtgrd1";
            this.loc_file_dtgrd1.Size = new System.Drawing.Size(291, 173);
            this.loc_file_dtgrd1.TabIndex = 2;
            this.loc_file_dtgrd1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.loc_file_dtgrd1_CellContentClick);
            // 
            // from_file_dtgrd2
            // 
            this.from_file_dtgrd2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.from_file_dtgrd2.Location = new System.Drawing.Point(363, 95);
            this.from_file_dtgrd2.Name = "from_file_dtgrd2";
            this.from_file_dtgrd2.Size = new System.Drawing.Size(291, 173);
            this.from_file_dtgrd2.TabIndex = 3;
            this.from_file_dtgrd2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.from_file_dtgrd2_CellContentClick);
            // 
            // copy_position_Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.from_file_dtgrd2);
            this.Controls.Add(this.loc_file_dtgrd1);
            this.Controls.Add(this.from_file_cmbx01);
            this.Controls.Add(this.loc_file_cmbx01);
            this.Name = "copy_position_Form1";
            this.Text = "copy_position_Form1";
            this.Load += new System.EventHandler(this.copy_position_Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.loc_file_dtgrd1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.from_file_dtgrd2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox loc_file_cmbx01;
        private System.Windows.Forms.ComboBox from_file_cmbx01;
        private System.Windows.Forms.DataGridView loc_file_dtgrd1;
        private System.Windows.Forms.DataGridView from_file_dtgrd2;
    }
}