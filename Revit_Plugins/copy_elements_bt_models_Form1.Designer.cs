namespace Revit_Plugins
{
    partial class copy_elements_bt_models_Form1
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
            this.rvt_lnk_inst_lstv1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.n_rvt_lb1 = new System.Windows.Forms.Label();
            this.lnk_rvt_lb2 = new System.Windows.Forms.Label();
            this.copy_fl_btn1 = new System.Windows.Forms.Button();
            this.doc_from_cmbbx1 = new System.Windows.Forms.ComboBox();
            this.doc_to_cmbbx1 = new System.Windows.Forms.ComboBox();
            this.copy_grp_btn1 = new System.Windows.Forms.Button();
            this.cnt_lb3 = new System.Windows.Forms.Label();
            this.rvt_lnk_systm_lstv2 = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cnt_lb4 = new System.Windows.Forms.Label();
            this.btn_cp_f_tps = new System.Windows.Forms.Button();
            this.lstv_cp_fam_tps = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cnt_lbl5 = new System.Windows.Forms.Label();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // rvt_lnk_inst_lstv1
            // 
            this.rvt_lnk_inst_lstv1.CheckBoxes = true;
            this.rvt_lnk_inst_lstv1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.rvt_lnk_inst_lstv1.HideSelection = false;
            this.rvt_lnk_inst_lstv1.Location = new System.Drawing.Point(31, 172);
            this.rvt_lnk_inst_lstv1.Name = "rvt_lnk_inst_lstv1";
            this.rvt_lnk_inst_lstv1.Size = new System.Drawing.Size(360, 249);
            this.rvt_lnk_inst_lstv1.TabIndex = 0;
            this.rvt_lnk_inst_lstv1.UseCompatibleStateImageBehavior = false;
            this.rvt_lnk_inst_lstv1.View = System.Windows.Forms.View.Details;
            this.rvt_lnk_inst_lstv1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.rvt_lnk_inst_lstv1_ColumnClick);
            this.rvt_lnk_inst_lstv1.SelectedIndexChanged += new System.EventHandler(this.rvt_lnk_inst_lstv1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 98;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ColumnHeader2";
            this.columnHeader2.Width = 79;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 72;
            // 
            // n_rvt_lb1
            // 
            this.n_rvt_lb1.AutoSize = true;
            this.n_rvt_lb1.Location = new System.Drawing.Point(28, 41);
            this.n_rvt_lb1.Name = "n_rvt_lb1";
            this.n_rvt_lb1.Size = new System.Drawing.Size(35, 13);
            this.n_rvt_lb1.TabIndex = 1;
            this.n_rvt_lb1.Text = "label1";
            this.n_rvt_lb1.Click += new System.EventHandler(this.n_rvt_lb1_Click);
            // 
            // lnk_rvt_lb2
            // 
            this.lnk_rvt_lb2.AutoSize = true;
            this.lnk_rvt_lb2.Location = new System.Drawing.Point(28, 94);
            this.lnk_rvt_lb2.Name = "lnk_rvt_lb2";
            this.lnk_rvt_lb2.Size = new System.Drawing.Size(35, 13);
            this.lnk_rvt_lb2.TabIndex = 2;
            this.lnk_rvt_lb2.Text = "label2";
            this.lnk_rvt_lb2.Click += new System.EventHandler(this.lnk_rvt_lb2_Click);
            // 
            // copy_fl_btn1
            // 
            this.copy_fl_btn1.Location = new System.Drawing.Point(665, 442);
            this.copy_fl_btn1.Name = "copy_fl_btn1";
            this.copy_fl_btn1.Size = new System.Drawing.Size(94, 33);
            this.copy_fl_btn1.TabIndex = 3;
            this.copy_fl_btn1.Text = "copy file";
            this.copy_fl_btn1.UseVisualStyleBackColor = true;
            this.copy_fl_btn1.Click += new System.EventHandler(this.copy_fl_btn1_Click);
            // 
            // doc_from_cmbbx1
            // 
            this.doc_from_cmbbx1.FormattingEnabled = true;
            this.doc_from_cmbbx1.Location = new System.Drawing.Point(31, 57);
            this.doc_from_cmbbx1.Name = "doc_from_cmbbx1";
            this.doc_from_cmbbx1.Size = new System.Drawing.Size(233, 21);
            this.doc_from_cmbbx1.TabIndex = 4;
            this.doc_from_cmbbx1.SelectedIndexChanged += new System.EventHandler(this.doc_from_cmbbx1_SelectedIndexChanged);
            // 
            // doc_to_cmbbx1
            // 
            this.doc_to_cmbbx1.FormattingEnabled = true;
            this.doc_to_cmbbx1.Location = new System.Drawing.Point(31, 110);
            this.doc_to_cmbbx1.Name = "doc_to_cmbbx1";
            this.doc_to_cmbbx1.Size = new System.Drawing.Size(233, 21);
            this.doc_to_cmbbx1.TabIndex = 5;
            this.doc_to_cmbbx1.SelectedIndexChanged += new System.EventHandler(this.doc_to_cmbbx1_SelectedIndexChanged);
            // 
            // copy_grp_btn1
            // 
            this.copy_grp_btn1.Location = new System.Drawing.Point(43, 442);
            this.copy_grp_btn1.Name = "copy_grp_btn1";
            this.copy_grp_btn1.Size = new System.Drawing.Size(94, 33);
            this.copy_grp_btn1.TabIndex = 6;
            this.copy_grp_btn1.Text = "copy first group";
            this.copy_grp_btn1.UseVisualStyleBackColor = true;
            this.copy_grp_btn1.Click += new System.EventHandler(this.copy_grp_btn1_Click);
            // 
            // cnt_lb3
            // 
            this.cnt_lb3.AutoSize = true;
            this.cnt_lb3.Location = new System.Drawing.Point(29, 155);
            this.cnt_lb3.Name = "cnt_lb3";
            this.cnt_lb3.Size = new System.Drawing.Size(82, 13);
            this.cnt_lb3.TabIndex = 7;
            this.cnt_lb3.Text = "elements_count";
            this.cnt_lb3.Click += new System.EventHandler(this.cnt_lb3_Click);
            // 
            // rvt_lnk_systm_lstv2
            // 
            this.rvt_lnk_systm_lstv2.CheckBoxes = true;
            this.rvt_lnk_systm_lstv2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.rvt_lnk_systm_lstv2.HideSelection = false;
            this.rvt_lnk_systm_lstv2.Location = new System.Drawing.Point(408, 172);
            this.rvt_lnk_systm_lstv2.Name = "rvt_lnk_systm_lstv2";
            this.rvt_lnk_systm_lstv2.Size = new System.Drawing.Size(351, 249);
            this.rvt_lnk_systm_lstv2.TabIndex = 8;
            this.rvt_lnk_systm_lstv2.UseCompatibleStateImageBehavior = false;
            this.rvt_lnk_systm_lstv2.View = System.Windows.Forms.View.Details;
            this.rvt_lnk_systm_lstv2.SelectedIndexChanged += new System.EventHandler(this.rvt_lnk_systm_lstv2_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 115;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Width = 122;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Width = 200;
            // 
            // cnt_lb4
            // 
            this.cnt_lb4.AutoSize = true;
            this.cnt_lb4.Location = new System.Drawing.Point(405, 155);
            this.cnt_lb4.Name = "cnt_lb4";
            this.cnt_lb4.Size = new System.Drawing.Size(82, 13);
            this.cnt_lb4.TabIndex = 9;
            this.cnt_lb4.Text = "elements_count";
            // 
            // btn_cp_f_tps
            // 
            this.btn_cp_f_tps.Location = new System.Drawing.Point(665, 767);
            this.btn_cp_f_tps.Name = "btn_cp_f_tps";
            this.btn_cp_f_tps.Size = new System.Drawing.Size(94, 33);
            this.btn_cp_f_tps.TabIndex = 10;
            this.btn_cp_f_tps.Text = "copy fam types";
            this.btn_cp_f_tps.UseVisualStyleBackColor = true;
            this.btn_cp_f_tps.Click += new System.EventHandler(this.btn_cp_f_tps_Click);
            // 
            // lstv_cp_fam_tps
            // 
            this.lstv_cp_fam_tps.CheckBoxes = true;
            this.lstv_cp_fam_tps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.lstv_cp_fam_tps.HideSelection = false;
            this.lstv_cp_fam_tps.Location = new System.Drawing.Point(32, 512);
            this.lstv_cp_fam_tps.Name = "lstv_cp_fam_tps";
            this.lstv_cp_fam_tps.Size = new System.Drawing.Size(727, 249);
            this.lstv_cp_fam_tps.TabIndex = 11;
            this.lstv_cp_fam_tps.UseCompatibleStateImageBehavior = false;
            this.lstv_cp_fam_tps.View = System.Windows.Forms.View.Details;
            this.lstv_cp_fam_tps.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstv_cp_fam_tps_ColumnClick);
            this.lstv_cp_fam_tps.SelectedIndexChanged += new System.EventHandler(this.lstv_cp_fam_tps_SelectedIndexChanged);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Width = 115;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Width = 122;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Width = 200;
            // 
            // cnt_lbl5
            // 
            this.cnt_lbl5.AutoSize = true;
            this.cnt_lbl5.Location = new System.Drawing.Point(29, 496);
            this.cnt_lbl5.Name = "cnt_lbl5";
            this.cnt_lbl5.Size = new System.Drawing.Size(82, 13);
            this.cnt_lbl5.TabIndex = 12;
            this.cnt_lbl5.Text = "elements_count";
            this.cnt_lbl5.Click += new System.EventHandler(this.cnt_lbl5_Click);
            // 
            // copy_elements_bt_models_Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 824);
            this.Controls.Add(this.cnt_lbl5);
            this.Controls.Add(this.lstv_cp_fam_tps);
            this.Controls.Add(this.btn_cp_f_tps);
            this.Controls.Add(this.cnt_lb4);
            this.Controls.Add(this.rvt_lnk_systm_lstv2);
            this.Controls.Add(this.cnt_lb3);
            this.Controls.Add(this.copy_grp_btn1);
            this.Controls.Add(this.doc_to_cmbbx1);
            this.Controls.Add(this.doc_from_cmbbx1);
            this.Controls.Add(this.copy_fl_btn1);
            this.Controls.Add(this.lnk_rvt_lb2);
            this.Controls.Add(this.n_rvt_lb1);
            this.Controls.Add(this.rvt_lnk_inst_lstv1);
            this.Name = "copy_elements_bt_models_Form1";
            this.Text = "copy_elements_bt_models_Form1";
            this.Load += new System.EventHandler(this.copy_elements_bt_models_Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView rvt_lnk_inst_lstv1;
        private System.Windows.Forms.Label n_rvt_lb1;
        private System.Windows.Forms.Label lnk_rvt_lb2;
        private System.Windows.Forms.Button copy_fl_btn1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ComboBox doc_from_cmbbx1;
        private System.Windows.Forms.ComboBox doc_to_cmbbx1;
        private System.Windows.Forms.Button copy_grp_btn1;
        private System.Windows.Forms.Label cnt_lb3;
        private System.Windows.Forms.ListView rvt_lnk_systm_lstv2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label cnt_lb4;
        private System.Windows.Forms.Button btn_cp_f_tps;
        private System.Windows.Forms.ListView lstv_cp_fam_tps;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Label cnt_lbl5;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
    }
}