using System.Windows.Forms;

namespace Revit_Plugins
{
    partial class check_fam_param_Form1
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
            this.lstv_fam_loading = new System.Windows.Forms.ListView();
            this.family_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.family_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbox_sel_cat1 = new System.Windows.Forms.ComboBox();
            this.lstv_prm1 = new System.Windows.Forms.ListView();
            this.sample_family_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pram_group = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pram_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pram_is_intance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pram_formula = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pram_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pram_GUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.lstv_prm_tr_fam1 = new System.Windows.Forms.ListView();
            this.fam_nm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fam_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_tr = new System.Windows.Forms.Button();
            this.tst_labl = new System.Windows.Forms.Label();
            this.lstv_shrd_prm_grps = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbl_shd_fl_nm = new System.Windows.Forms.Label();
            this.lbl_fam_trnsfr = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstv_fam_loading
            // 
            this.lstv_fam_loading.CheckBoxes = true;
            this.lstv_fam_loading.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.family_name,
            this.family_id});
            this.lstv_fam_loading.HideSelection = false;
            this.lstv_fam_loading.Location = new System.Drawing.Point(12, 67);
            this.lstv_fam_loading.Name = "lstv_fam_loading";
            this.lstv_fam_loading.ShowItemToolTips = true;
            this.lstv_fam_loading.Size = new System.Drawing.Size(220, 284);
            this.lstv_fam_loading.TabIndex = 0;
            this.lstv_fam_loading.UseCompatibleStateImageBehavior = false;
            this.lstv_fam_loading.View = System.Windows.Forms.View.Details;
            this.lstv_fam_loading.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstv_fam_loading_ColumnClick);
            this.lstv_fam_loading.SelectedIndexChanged += new System.EventHandler(this.lstv_fam_loading_SelectedIndexChanged);
            // 
            // family_name
            // 
            this.family_name.Text = "Family_Name";
            this.family_name.Width = 119;
            // 
            // family_id
            // 
            this.family_id.Text = "Family_Id";
            // 
            // cbox_sel_cat1
            // 
            this.cbox_sel_cat1.FormattingEnabled = true;
            this.cbox_sel_cat1.Location = new System.Drawing.Point(12, 28);
            this.cbox_sel_cat1.Name = "cbox_sel_cat1";
            this.cbox_sel_cat1.Size = new System.Drawing.Size(220, 21);
            this.cbox_sel_cat1.TabIndex = 1;
            this.cbox_sel_cat1.SelectedIndexChanged += new System.EventHandler(this.cbox_sel_cat1_SelectedIndexChanged);
            // 
            // lstv_prm1
            // 
            this.lstv_prm1.CheckBoxes = true;
            this.lstv_prm1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sample_family_id,
            this.pram_group,
            this.pram_name,
            this.pram_is_intance,
            this.pram_formula,
            this.pram_id,
            this.pram_GUID});
            this.lstv_prm1.FullRowSelect = true;
            this.lstv_prm1.HideSelection = false;
            this.lstv_prm1.Location = new System.Drawing.Point(348, 67);
            this.lstv_prm1.Name = "lstv_prm1";
            this.lstv_prm1.Size = new System.Drawing.Size(551, 520);
            this.lstv_prm1.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lstv_prm1.TabIndex = 2;
            this.lstv_prm1.UseCompatibleStateImageBehavior = false;
            this.lstv_prm1.View = System.Windows.Forms.View.Details;
            this.lstv_prm1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstv_prm1_ColumnClick);
            this.lstv_prm1.SelectedIndexChanged += new System.EventHandler(this.lstv_prm1_SelectedIndexChanged);
            // 
            // sample_family_id
            // 
            this.sample_family_id.Text = "family_id";
            this.sample_family_id.Width = 49;
            // 
            // pram_group
            // 
            this.pram_group.Text = "Param_Group";
            // 
            // pram_name
            // 
            this.pram_name.Text = "Parameter_Name";
            // 
            // pram_is_intance
            // 
            this.pram_is_intance.Text = "Param_Is_Inst";
            this.pram_is_intance.Width = 67;
            // 
            // pram_formula
            // 
            this.pram_formula.Text = "Param_Formula";
            this.pram_formula.Width = 88;
            // 
            // pram_id
            // 
            this.pram_id.Text = "Param_Id";
            // 
            // pram_GUID
            // 
            this.pram_GUID.Text = "Param_GUID";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(238, 261);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 50);
            this.button1.TabIndex = 3;
            this.button1.Text = "Sample Families";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstv_prm_tr_fam1
            // 
            this.lstv_prm_tr_fam1.CheckBoxes = true;
            this.lstv_prm_tr_fam1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fam_nm,
            this.fam_id});
            this.lstv_prm_tr_fam1.HideSelection = false;
            this.lstv_prm_tr_fam1.Location = new System.Drawing.Point(1006, 67);
            this.lstv_prm_tr_fam1.Name = "lstv_prm_tr_fam1";
            this.lstv_prm_tr_fam1.Size = new System.Drawing.Size(242, 520);
            this.lstv_prm_tr_fam1.TabIndex = 4;
            this.lstv_prm_tr_fam1.UseCompatibleStateImageBehavior = false;
            this.lstv_prm_tr_fam1.View = System.Windows.Forms.View.Details;
            this.lstv_prm_tr_fam1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstv_prm_tr_fam1_ColumnClick);
            this.lstv_prm_tr_fam1.SelectedIndexChanged += new System.EventHandler(this.lstv_prm_tr_fam1_SelectedIndexChanged);
            // 
            // fam_nm
            // 
            this.fam_nm.Text = "Des_Family_Name";
            this.fam_nm.Width = 100;
            // 
            // fam_id
            // 
            this.fam_id.Text = "Des_Family_Id";
            this.fam_id.Width = 98;
            // 
            // btn_tr
            // 
            this.btn_tr.Location = new System.Drawing.Point(905, 261);
            this.btn_tr.Name = "btn_tr";
            this.btn_tr.Size = new System.Drawing.Size(86, 50);
            this.btn_tr.TabIndex = 5;
            this.btn_tr.Text = "Transfer";
            this.btn_tr.UseVisualStyleBackColor = true;
            this.btn_tr.Click += new System.EventHandler(this.btn_tr_Click);
            // 
            // tst_labl
            // 
            this.tst_labl.AutoSize = true;
            this.tst_labl.Location = new System.Drawing.Point(345, 9);
            this.tst_labl.Name = "tst_labl";
            this.tst_labl.Size = new System.Drawing.Size(40, 13);
            this.tst_labl.TabIndex = 6;
            this.tst_labl.Text = "tst_labl";
            this.tst_labl.Click += new System.EventHandler(this.tst_labl_Click);
            // 
            // lstv_shrd_prm_grps
            // 
            this.lstv_shrd_prm_grps.CheckBoxes = true;
            this.lstv_shrd_prm_grps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstv_shrd_prm_grps.HideSelection = false;
            this.lstv_shrd_prm_grps.Location = new System.Drawing.Point(12, 372);
            this.lstv_shrd_prm_grps.Name = "lstv_shrd_prm_grps";
            this.lstv_shrd_prm_grps.ShowItemToolTips = true;
            this.lstv_shrd_prm_grps.Size = new System.Drawing.Size(220, 283);
            this.lstv_shrd_prm_grps.TabIndex = 7;
            this.lstv_shrd_prm_grps.UseCompatibleStateImageBehavior = false;
            this.lstv_shrd_prm_grps.View = System.Windows.Forms.View.Details;
            this.lstv_shrd_prm_grps.SelectedIndexChanged += new System.EventHandler(this.lstv_shrd_prm_grps_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "parameter_group";
            this.columnHeader1.Width = 119;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "column2";
            // 
            // lbl_shd_fl_nm
            // 
            this.lbl_shd_fl_nm.AutoSize = true;
            this.lbl_shd_fl_nm.Location = new System.Drawing.Point(350, 36);
            this.lbl_shd_fl_nm.Name = "lbl_shd_fl_nm";
            this.lbl_shd_fl_nm.Size = new System.Drawing.Size(35, 13);
            this.lbl_shd_fl_nm.TabIndex = 8;
            this.lbl_shd_fl_nm.Text = "label1";
            this.lbl_shd_fl_nm.Click += new System.EventHandler(this.lbl_shd_fl_nm_Click);
            // 
            // lbl_fam_trnsfr
            // 
            this.lbl_fam_trnsfr.AutoSize = true;
            this.lbl_fam_trnsfr.Location = new System.Drawing.Point(1012, 36);
            this.lbl_fam_trnsfr.Name = "lbl_fam_trnsfr";
            this.lbl_fam_trnsfr.Size = new System.Drawing.Size(35, 13);
            this.lbl_fam_trnsfr.TabIndex = 9;
            this.lbl_fam_trnsfr.Text = "label1";
            this.lbl_fam_trnsfr.Click += new System.EventHandler(this.lbl_fam_trnsfr_Click);
            // 
            // check_fam_param_Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1283, 684);
            this.Controls.Add(this.lbl_fam_trnsfr);
            this.Controls.Add(this.lbl_shd_fl_nm);
            this.Controls.Add(this.lstv_shrd_prm_grps);
            this.Controls.Add(this.tst_labl);
            this.Controls.Add(this.btn_tr);
            this.Controls.Add(this.lstv_prm_tr_fam1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lstv_prm1);
            this.Controls.Add(this.cbox_sel_cat1);
            this.Controls.Add(this.lstv_fam_loading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "check_fam_param_Form1";
            this.Text = "check_fam_param_Form1";
            this.Load += new System.EventHandler(this.check_room_info_Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstv_fam_loading;
        private System.Windows.Forms.ComboBox cbox_sel_cat1;
        private System.Windows.Forms.ColumnHeader family_name;
        private System.Windows.Forms.ColumnHeader family_id;
        private System.Windows.Forms.ListView lstv_prm1;
        private System.Windows.Forms.ColumnHeader pram_name;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader sample_family_id;
        private System.Windows.Forms.ColumnHeader pram_group;
        private System.Windows.Forms.ColumnHeader pram_id;
        private System.Windows.Forms.ColumnHeader pram_GUID;
        private System.Windows.Forms.ColumnHeader pram_is_intance;
        private System.Windows.Forms.ColumnHeader pram_formula;
        private ListView lstv_prm_tr_fam1;
        private ColumnHeader fam_nm;
        private ColumnHeader fam_id;
        private Button btn_tr;
        private Label tst_labl;
        private ListView lstv_shrd_prm_grps;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Label lbl_shd_fl_nm;
        private Label lbl_fam_trnsfr;
    }
}