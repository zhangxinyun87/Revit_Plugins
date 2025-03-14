namespace Revit_Plugins
{
    partial class copy_views_n_rooms_Form1
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
            this.lstv_viws_01 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstv_rms_02 = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmbx_doc_from01 = new System.Windows.Forms.ComboBox();
            this.btn_cp_v_01 = new System.Windows.Forms.Button();
            this.btn_cp_rm_02 = new System.Windows.Forms.Button();
            this.lstv_shts_03 = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_cp_shts_03 = new System.Windows.Forms.Button();
            this.chck_lstv_shts_04 = new System.Windows.Forms.ListView();
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstv_viws_01
            // 
            this.lstv_viws_01.CheckBoxes = true;
            this.lstv_viws_01.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader6});
            this.lstv_viws_01.HideSelection = false;
            this.lstv_viws_01.Location = new System.Drawing.Point(47, 112);
            this.lstv_viws_01.Name = "lstv_viws_01";
            this.lstv_viws_01.Size = new System.Drawing.Size(286, 245);
            this.lstv_viws_01.TabIndex = 0;
            this.lstv_viws_01.UseCompatibleStateImageBehavior = false;
            this.lstv_viws_01.View = System.Windows.Forms.View.Details;
            this.lstv_viws_01.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstv_viws_01_ColumnClick);
            this.lstv_viws_01.SelectedIndexChanged += new System.EventHandler(this.lstv_viws_01_SelectedIndexChanged);
            // 
            // lstv_rms_02
            // 
            this.lstv_rms_02.CheckBoxes = true;
            this.lstv_rms_02.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.lstv_rms_02.HideSelection = false;
            this.lstv_rms_02.Location = new System.Drawing.Point(352, 112);
            this.lstv_rms_02.Name = "lstv_rms_02";
            this.lstv_rms_02.Size = new System.Drawing.Size(373, 245);
            this.lstv_rms_02.TabIndex = 1;
            this.lstv_rms_02.UseCompatibleStateImageBehavior = false;
            this.lstv_rms_02.View = System.Windows.Forms.View.Details;
            this.lstv_rms_02.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstv_rms_02_ColumnClick);
            this.lstv_rms_02.SelectedIndexChanged += new System.EventHandler(this.lstv_rms_02_SelectedIndexChanged);
            // 
            // cmbx_doc_from01
            // 
            this.cmbx_doc_from01.FormattingEnabled = true;
            this.cmbx_doc_from01.Location = new System.Drawing.Point(46, 31);
            this.cmbx_doc_from01.Name = "cmbx_doc_from01";
            this.cmbx_doc_from01.Size = new System.Drawing.Size(287, 21);
            this.cmbx_doc_from01.TabIndex = 2;
            this.cmbx_doc_from01.SelectedIndexChanged += new System.EventHandler(this.cmbx_doc_from01_SelectedIndexChanged);
            // 
            // btn_cp_v_01
            // 
            this.btn_cp_v_01.Location = new System.Drawing.Point(241, 377);
            this.btn_cp_v_01.Name = "btn_cp_v_01";
            this.btn_cp_v_01.Size = new System.Drawing.Size(92, 28);
            this.btn_cp_v_01.TabIndex = 3;
            this.btn_cp_v_01.Text = "copy views";
            this.btn_cp_v_01.UseVisualStyleBackColor = true;
            this.btn_cp_v_01.Click += new System.EventHandler(this.btn_cp_v_01_Click);
            // 
            // btn_cp_rm_02
            // 
            this.btn_cp_rm_02.Location = new System.Drawing.Point(546, 377);
            this.btn_cp_rm_02.Name = "btn_cp_rm_02";
            this.btn_cp_rm_02.Size = new System.Drawing.Size(92, 28);
            this.btn_cp_rm_02.TabIndex = 4;
            this.btn_cp_rm_02.Text = "copy rooms";
            this.btn_cp_rm_02.UseVisualStyleBackColor = true;
            this.btn_cp_rm_02.Click += new System.EventHandler(this.btn_cp_rm_02_Click);
            // 
            // lstv_shts_03
            // 
            this.lstv_shts_03.CheckBoxes = true;
            this.lstv_shts_03.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.lstv_shts_03.HideSelection = false;
            this.lstv_shts_03.Location = new System.Drawing.Point(745, 112);
            this.lstv_shts_03.Name = "lstv_shts_03";
            this.lstv_shts_03.Size = new System.Drawing.Size(331, 245);
            this.lstv_shts_03.TabIndex = 5;
            this.lstv_shts_03.UseCompatibleStateImageBehavior = false;
            this.lstv_shts_03.View = System.Windows.Forms.View.Details;
            this.lstv_shts_03.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstv_shts_03_ColumnClick);
            this.lstv_shts_03.SelectedIndexChanged += new System.EventHandler(this.lstv_shts_03_SelectedIndexChanged);
            // 
            // btn_cp_shts_03
            // 
            this.btn_cp_shts_03.Location = new System.Drawing.Point(975, 377);
            this.btn_cp_shts_03.Name = "btn_cp_shts_03";
            this.btn_cp_shts_03.Size = new System.Drawing.Size(92, 28);
            this.btn_cp_shts_03.TabIndex = 6;
            this.btn_cp_shts_03.Text = "copy sheets";
            this.btn_cp_shts_03.UseVisualStyleBackColor = true;
            this.btn_cp_shts_03.Click += new System.EventHandler(this.btn_cp_shts_03_Click);
            // 
            // chck_lstv_shts_04
            // 
            this.chck_lstv_shts_04.CheckBoxes = true;
            this.chck_lstv_shts_04.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.chck_lstv_shts_04.HideSelection = false;
            this.chck_lstv_shts_04.Location = new System.Drawing.Point(745, 411);
            this.chck_lstv_shts_04.Name = "chck_lstv_shts_04";
            this.chck_lstv_shts_04.Size = new System.Drawing.Size(331, 187);
            this.chck_lstv_shts_04.TabIndex = 7;
            this.chck_lstv_shts_04.UseCompatibleStateImageBehavior = false;
            this.chck_lstv_shts_04.View = System.Windows.Forms.View.Details;
            this.chck_lstv_shts_04.SelectedIndexChanged += new System.EventHandler(this.chck_lstv_shts_04_SelectedIndexChanged);
            // 
            // copy_views_n_rooms_Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 636);
            this.Controls.Add(this.chck_lstv_shts_04);
            this.Controls.Add(this.btn_cp_shts_03);
            this.Controls.Add(this.lstv_shts_03);
            this.Controls.Add(this.btn_cp_rm_02);
            this.Controls.Add(this.btn_cp_v_01);
            this.Controls.Add(this.cmbx_doc_from01);
            this.Controls.Add(this.lstv_rms_02);
            this.Controls.Add(this.lstv_viws_01);
            this.Name = "copy_views_n_rooms_Form1";
            this.Text = "copy_views_n_rooms_Form1";
            this.Load += new System.EventHandler(this.copy_views_n_rooms_Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstv_viws_01;
        private System.Windows.Forms.ListView lstv_rms_02;
        private System.Windows.Forms.ComboBox cmbx_doc_from01;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btn_cp_v_01;
        private System.Windows.Forms.Button btn_cp_rm_02;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ListView lstv_shts_03;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Button btn_cp_shts_03;
        private System.Windows.Forms.ListView chck_lstv_shts_04;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
    }
}