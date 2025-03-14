using System;
using System.Windows.Controls;

namespace Revit_Plugins
{
    partial class item_mappatura_Form1
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
            this.lstv_views = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstv_cats_01 = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmbx_prm_01 = new System.Windows.Forms.ComboBox();
            this.btn_add_01 = new System.Windows.Forms.Button();
            this.lstv_docs = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_create = new System.Windows.Forms.Button();
            this.btn_prev = new System.Windows.Forms.Button();
            this.btn_minus = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbx_legend_v = new System.Windows.Forms.ComboBox();
            this.cmbx_fillpattern = new System.Windows.Forms.ComboBox();
            this.cmbx_txt_type = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lstv_val_color = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstv_saved_grp = new System.Windows.Forms.ListView();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbx_grp_nm = new System.Windows.Forms.TextBox();
            this.tbx_itm_prefx = new System.Windows.Forms.TextBox();
            this.btn_add_grp = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_re_colr = new System.Windows.Forms.Button();
            this.dt_grdv_color = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btn_crt_fltrs = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dt_grdv_color)).BeginInit();
            this.SuspendLayout();
            // 
            // lstv_views
            // 
            this.lstv_views.CheckBoxes = true;
            this.lstv_views.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader8});
            this.lstv_views.HideSelection = false;
            this.lstv_views.Location = new System.Drawing.Point(12, 303);
            this.lstv_views.Name = "lstv_views";
            this.lstv_views.Size = new System.Drawing.Size(327, 142);
            this.lstv_views.TabIndex = 0;
            this.lstv_views.UseCompatibleStateImageBehavior = false;
            this.lstv_views.View = System.Windows.Forms.View.Details;
            this.lstv_views.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstv_views_ColumnClick);
            this.lstv_views.SelectedIndexChanged += new System.EventHandler(this.lstv_views_SelectedIndexChanged);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Width = 71;
            // 
            // lstv_cats_01
            // 
            this.lstv_cats_01.CheckBoxes = true;
            this.lstv_cats_01.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
            this.lstv_cats_01.HideSelection = false;
            this.lstv_cats_01.Location = new System.Drawing.Point(12, 145);
            this.lstv_cats_01.Name = "lstv_cats_01";
            this.lstv_cats_01.Size = new System.Drawing.Size(327, 142);
            this.lstv_cats_01.TabIndex = 1;
            this.lstv_cats_01.UseCompatibleStateImageBehavior = false;
            this.lstv_cats_01.View = System.Windows.Forms.View.Details;
            this.lstv_cats_01.SelectedIndexChanged += new System.EventHandler(this.lstv_cats_01_SelectedIndexChanged);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Width = 126;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Width = 102;
            // 
            // cmbx_prm_01
            // 
            this.cmbx_prm_01.FormattingEnabled = true;
            this.cmbx_prm_01.Location = new System.Drawing.Point(358, 56);
            this.cmbx_prm_01.Name = "cmbx_prm_01";
            this.cmbx_prm_01.Size = new System.Drawing.Size(239, 21);
            this.cmbx_prm_01.TabIndex = 2;
            this.cmbx_prm_01.SelectedIndexChanged += new System.EventHandler(this.cmbx_prm_01_SelectedIndexChanged);
            // 
            // btn_add_01
            // 
            this.btn_add_01.Location = new System.Drawing.Point(711, 56);
            this.btn_add_01.Name = "btn_add_01";
            this.btn_add_01.Size = new System.Drawing.Size(28, 28);
            this.btn_add_01.TabIndex = 3;
            this.btn_add_01.Text = "+";
            this.btn_add_01.UseVisualStyleBackColor = true;
            this.btn_add_01.Click += new System.EventHandler(this.btn_add_01_Click);
            // 
            // lstv_docs
            // 
            this.lstv_docs.CheckBoxes = true;
            this.lstv_docs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstv_docs.HideSelection = false;
            this.lstv_docs.Location = new System.Drawing.Point(12, 24);
            this.lstv_docs.Name = "lstv_docs";
            this.lstv_docs.Size = new System.Drawing.Size(327, 104);
            this.lstv_docs.TabIndex = 4;
            this.lstv_docs.UseCompatibleStateImageBehavior = false;
            this.lstv_docs.View = System.Windows.Forms.View.Details;
            this.lstv_docs.SelectedIndexChanged += new System.EventHandler(this.lstv_docs_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 189;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 100;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(253, 601);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(86, 28);
            this.btn_ok.TabIndex = 5;
            this.btn_ok.Text = "ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(355, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btn_create
            // 
            this.btn_create.Location = new System.Drawing.Point(1539, 605);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(92, 24);
            this.btn_create.TabIndex = 7;
            this.btn_create.Text = "Create";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // btn_prev
            // 
            this.btn_prev.Location = new System.Drawing.Point(855, 19);
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(102, 25);
            this.btn_prev.TabIndex = 8;
            this.btn_prev.Text = "Preview";
            this.btn_prev.UseVisualStyleBackColor = true;
            this.btn_prev.Click += new System.EventHandler(this.btn_prev_Click);
            // 
            // btn_minus
            // 
            this.btn_minus.Location = new System.Drawing.Point(677, 56);
            this.btn_minus.Name = "btn_minus";
            this.btn_minus.Size = new System.Drawing.Size(28, 28);
            this.btn_minus.TabIndex = 9;
            this.btn_minus.UseVisualStyleBackColor = true;
            this.btn_minus.Click += new System.EventHandler(this.btn_minus_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(743, 601);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // cmbx_legend_v
            // 
            this.cmbx_legend_v.FormattingEnabled = true;
            this.cmbx_legend_v.Location = new System.Drawing.Point(12, 471);
            this.cmbx_legend_v.Name = "cmbx_legend_v";
            this.cmbx_legend_v.Size = new System.Drawing.Size(327, 21);
            this.cmbx_legend_v.TabIndex = 11;
            this.cmbx_legend_v.SelectedIndexChanged += new System.EventHandler(this.cmbx_legend_v_SelectedIndexChanged);
            // 
            // cmbx_fillpattern
            // 
            this.cmbx_fillpattern.FormattingEnabled = true;
            this.cmbx_fillpattern.Location = new System.Drawing.Point(12, 513);
            this.cmbx_fillpattern.Name = "cmbx_fillpattern";
            this.cmbx_fillpattern.Size = new System.Drawing.Size(327, 21);
            this.cmbx_fillpattern.TabIndex = 12;
            this.cmbx_fillpattern.SelectedIndexChanged += new System.EventHandler(this.cmbx_fillpattern_SelectedIndexChanged);
            // 
            // cmbx_txt_type
            // 
            this.cmbx_txt_type.FormattingEnabled = true;
            this.cmbx_txt_type.Location = new System.Drawing.Point(12, 555);
            this.cmbx_txt_type.Name = "cmbx_txt_type";
            this.cmbx_txt_type.Size = new System.Drawing.Size(327, 21);
            this.cmbx_txt_type.TabIndex = 13;
            this.cmbx_txt_type.SelectedIndexChanged += new System.EventHandler(this.cmbx_txt_type_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(396, 635);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "label3";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lstv_val_color
            // 
            this.lstv_val_color.CheckBoxes = true;
            this.lstv_val_color.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10});
            this.lstv_val_color.HideSelection = false;
            this.lstv_val_color.Location = new System.Drawing.Point(1212, 51);
            this.lstv_val_color.Name = "lstv_val_color";
            this.lstv_val_color.OwnerDraw = true;
            this.lstv_val_color.Size = new System.Drawing.Size(155, 535);
            this.lstv_val_color.TabIndex = 15;
            this.lstv_val_color.UseCompatibleStateImageBehavior = false;
            this.lstv_val_color.View = System.Windows.Forms.View.Details;
            this.lstv_val_color.SelectedIndexChanged += new System.EventHandler(this.lstv_val_color_SelectedIndexChanged);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Width = 100;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Width = 73;
            // 
            // lstv_saved_grp
            // 
            this.lstv_saved_grp.CheckBoxes = true;
            this.lstv_saved_grp.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12});
            this.lstv_saved_grp.HideSelection = false;
            this.lstv_saved_grp.Location = new System.Drawing.Point(1388, 51);
            this.lstv_saved_grp.Name = "lstv_saved_grp";
            this.lstv_saved_grp.Size = new System.Drawing.Size(243, 535);
            this.lstv_saved_grp.TabIndex = 16;
            this.lstv_saved_grp.UseCompatibleStateImageBehavior = false;
            this.lstv_saved_grp.View = System.Windows.Forms.View.Details;
            this.lstv_saved_grp.SelectedIndexChanged += new System.EventHandler(this.lstv_saved_grp_SelectedIndexChanged);
            // 
            // columnHeader11
            // 
            this.columnHeader11.Width = 62;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Width = 73;
            // 
            // tbx_grp_nm
            // 
            this.tbx_grp_nm.Location = new System.Drawing.Point(1387, 24);
            this.tbx_grp_nm.Name = "tbx_grp_nm";
            this.tbx_grp_nm.Size = new System.Drawing.Size(103, 20);
            this.tbx_grp_nm.TabIndex = 17;
            this.tbx_grp_nm.TextChanged += new System.EventHandler(this.tbx_grp_nm_TextChanged);
            // 
            // tbx_itm_prefx
            // 
            this.tbx_itm_prefx.Location = new System.Drawing.Point(746, 22);
            this.tbx_itm_prefx.Name = "tbx_itm_prefx";
            this.tbx_itm_prefx.Size = new System.Drawing.Size(103, 20);
            this.tbx_itm_prefx.TabIndex = 18;
            this.tbx_itm_prefx.TextChanged += new System.EventHandler(this.tbx_itm_prefx_TextChanged);
            // 
            // btn_add_grp
            // 
            this.btn_add_grp.Location = new System.Drawing.Point(1496, 24);
            this.btn_add_grp.Name = "btn_add_grp";
            this.btn_add_grp.Size = new System.Drawing.Size(61, 21);
            this.btn_add_grp.TabIndex = 19;
            this.btn_add_grp.Text = "Add ";
            this.btn_add_grp.UseVisualStyleBackColor = true;
            this.btn_add_grp.Click += new System.EventHandler(this.btn_add_grp_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1385, 601);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "label4";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btn_re_colr
            // 
            this.btn_re_colr.Location = new System.Drawing.Point(963, 21);
            this.btn_re_colr.Name = "btn_re_colr";
            this.btn_re_colr.Size = new System.Drawing.Size(92, 24);
            this.btn_re_colr.TabIndex = 21;
            this.btn_re_colr.Text = "Refresh Color";
            this.btn_re_colr.UseVisualStyleBackColor = true;
            this.btn_re_colr.Click += new System.EventHandler(this.btn_re_colr_Click);
            // 
            // dt_grdv_color
            // 
            this.dt_grdv_color.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dt_grdv_color.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4,
            this.Column3});
            this.dt_grdv_color.Location = new System.Drawing.Point(745, 51);
            this.dt_grdv_color.Name = "dt_grdv_color";
            this.dt_grdv_color.Size = new System.Drawing.Size(449, 535);
            this.dt_grdv_color.TabIndex = 22;
            this.dt_grdv_color.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dt_grdv_color_CellContentClick);
            this.dt_grdv_color.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dt_grdv_color_ColumnHeaderMouseClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Color";
            this.Column4.Name = "Column4";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "FillPattern";
            this.Column3.Name = "Column3";
            // 
            // btn_crt_fltrs
            // 
            this.btn_crt_fltrs.Location = new System.Drawing.Point(1102, 20);
            this.btn_crt_fltrs.Name = "btn_crt_fltrs";
            this.btn_crt_fltrs.Size = new System.Drawing.Size(92, 24);
            this.btn_crt_fltrs.TabIndex = 23;
            this.btn_crt_fltrs.Text = "Create Filters";
            this.btn_crt_fltrs.UseVisualStyleBackColor = true;
            this.btn_crt_fltrs.Click += new System.EventHandler(this.btn_crt_fltrs_Click);
            // 
            // item_mappatura_Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1643, 657);
            this.Controls.Add(this.btn_crt_fltrs);
            this.Controls.Add(this.dt_grdv_color);
            this.Controls.Add(this.btn_re_colr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_add_grp);
            this.Controls.Add(this.tbx_itm_prefx);
            this.Controls.Add(this.tbx_grp_nm);
            this.Controls.Add(this.lstv_saved_grp);
            this.Controls.Add(this.lstv_val_color);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbx_txt_type);
            this.Controls.Add(this.cmbx_fillpattern);
            this.Controls.Add(this.cmbx_legend_v);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_minus);
            this.Controls.Add(this.btn_prev);
            this.Controls.Add(this.btn_create);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.lstv_docs);
            this.Controls.Add(this.btn_add_01);
            this.Controls.Add(this.cmbx_prm_01);
            this.Controls.Add(this.lstv_cats_01);
            this.Controls.Add(this.lstv_views);
            this.Name = "item_mappatura_Form1";
            this.Text = "item_mappatura_Form1";
            this.Load += new System.EventHandler(this.item_mappatura_Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_grdv_color)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        

        #endregion

        private System.Windows.Forms.ListView lstv_views;
        private System.Windows.Forms.ListView lstv_cats_01;
        private System.Windows.Forms.ComboBox cmbx_prm_01;
        private System.Windows.Forms.Button btn_add_01;
        private System.Windows.Forms.ListView lstv_docs;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.Button btn_prev;
        private System.Windows.Forms.Button btn_minus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbx_legend_v;
        private System.Windows.Forms.ComboBox cmbx_fillpattern;
        private System.Windows.Forms.ComboBox cmbx_txt_type;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lstv_val_color;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ListView lstv_saved_grp;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.TextBox tbx_grp_nm;
        private System.Windows.Forms.TextBox tbx_itm_prefx;
        private System.Windows.Forms.Button btn_add_grp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_re_colr;
        private System.Windows.Forms.DataGridView dt_grdv_color;
        private System.Windows.Forms.Button btn_crt_fltrs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column3;
    }
}