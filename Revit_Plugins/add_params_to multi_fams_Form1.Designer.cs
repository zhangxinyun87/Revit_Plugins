namespace Revit_Plugins
{
    partial class add_params_to_multi_fams_Form1
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
            this.shd_fl_pth_labl1 = new System.Windows.Forms.Label();
            this.prm_grps_lstv01 = new System.Windows.Forms.ListView();
            this.ParameterDefGroupName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bltin_grp_lstv02 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fams_lstv03 = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.add_prm_btn01 = new System.Windows.Forms.Button();
            this.isInstance_rd_btn = new System.Windows.Forms.RadioButton();
            this.check_is_inst = new System.Windows.Forms.Label();
            this.ex_df_lstv04 = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.isType_rd_btn = new System.Windows.Forms.RadioButton();
            this.grp_bx01 = new System.Windows.Forms.GroupBox();
            this.grp_bx01.SuspendLayout();
            this.SuspendLayout();
            // 
            // shd_fl_pth_labl1
            // 
            this.shd_fl_pth_labl1.AutoSize = true;
            this.shd_fl_pth_labl1.Location = new System.Drawing.Point(21, 9);
            this.shd_fl_pth_labl1.Name = "shd_fl_pth_labl1";
            this.shd_fl_pth_labl1.Size = new System.Drawing.Size(105, 13);
            this.shd_fl_pth_labl1.TabIndex = 0;
            this.shd_fl_pth_labl1.Text = "shared parameter file";
            this.shd_fl_pth_labl1.Click += new System.EventHandler(this.shd_fl_pth_labl1_Click);
            // 
            // prm_grps_lstv01
            // 
            this.prm_grps_lstv01.CheckBoxes = true;
            this.prm_grps_lstv01.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ParameterDefGroupName,
            this.columnHeader2});
            this.prm_grps_lstv01.HideSelection = false;
            this.prm_grps_lstv01.Location = new System.Drawing.Point(24, 41);
            this.prm_grps_lstv01.Name = "prm_grps_lstv01";
            this.prm_grps_lstv01.Size = new System.Drawing.Size(236, 410);
            this.prm_grps_lstv01.TabIndex = 1;
            this.prm_grps_lstv01.UseCompatibleStateImageBehavior = false;
            this.prm_grps_lstv01.View = System.Windows.Forms.View.Details;
            this.prm_grps_lstv01.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.prm_grp_nm_sort);
            this.prm_grps_lstv01.SelectedIndexChanged += new System.EventHandler(this.prm_grps_lstv01_SelectedIndexChanged);
            // 
            // ParameterDefGroupName
            // 
            this.ParameterDefGroupName.Text = "Parameter Groups Name";
            this.ParameterDefGroupName.Width = 102;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 78;
            // 
            // bltin_grp_lstv02
            // 
            this.bltin_grp_lstv02.CheckBoxes = true;
            this.bltin_grp_lstv02.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3});
            this.bltin_grp_lstv02.HideSelection = false;
            this.bltin_grp_lstv02.Location = new System.Drawing.Point(682, 41);
            this.bltin_grp_lstv02.MultiSelect = false;
            this.bltin_grp_lstv02.Name = "bltin_grp_lstv02";
            this.bltin_grp_lstv02.Size = new System.Drawing.Size(305, 410);
            this.bltin_grp_lstv02.TabIndex = 2;
            this.bltin_grp_lstv02.UseCompatibleStateImageBehavior = false;
            this.bltin_grp_lstv02.View = System.Windows.Forms.View.Details;
            this.bltin_grp_lstv02.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.bltin_grp_lstv02_column_click);
            this.bltin_grp_lstv02.SelectedIndexChanged += new System.EventHandler(this.bltin_grp_lstv02_SelectedIndexChanged);
            // 
            // fams_lstv03
            // 
            this.fams_lstv03.CheckBoxes = true;
            this.fams_lstv03.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.fams_lstv03.HideSelection = false;
            this.fams_lstv03.Location = new System.Drawing.Point(1030, 41);
            this.fams_lstv03.Name = "fams_lstv03";
            this.fams_lstv03.Size = new System.Drawing.Size(300, 410);
            this.fams_lstv03.TabIndex = 3;
            this.fams_lstv03.UseCompatibleStateImageBehavior = false;
            this.fams_lstv03.View = System.Windows.Forms.View.Details;
            this.fams_lstv03.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.fams_lstv03_column_click);
            this.fams_lstv03.SelectedIndexChanged += new System.EventHandler(this.fams_lstv03_SelectedIndexChanged);
            // 
            // add_prm_btn01
            // 
            this.add_prm_btn01.Location = new System.Drawing.Point(855, 521);
            this.add_prm_btn01.Name = "add_prm_btn01";
            this.add_prm_btn01.Size = new System.Drawing.Size(154, 33);
            this.add_prm_btn01.TabIndex = 4;
            this.add_prm_btn01.Text = "Add Parameters";
            this.add_prm_btn01.UseVisualStyleBackColor = true;
            this.add_prm_btn01.Click += new System.EventHandler(this.add_prm_btn01_Click);
            // 
            // isInstance_rd_btn
            // 
            this.isInstance_rd_btn.AutoSize = true;
            this.isInstance_rd_btn.Location = new System.Drawing.Point(6, 19);
            this.isInstance_rd_btn.Name = "isInstance_rd_btn";
            this.isInstance_rd_btn.Size = new System.Drawing.Size(73, 17);
            this.isInstance_rd_btn.TabIndex = 5;
            this.isInstance_rd_btn.TabStop = true;
            this.isInstance_rd_btn.Text = "isInstance";
            this.isInstance_rd_btn.UseVisualStyleBackColor = true;
            this.isInstance_rd_btn.CheckedChanged += new System.EventHandler(this.isInstance_rd_btn_CheckedChanged);
            // 
            // check_is_inst
            // 
            this.check_is_inst.AutoSize = true;
            this.check_is_inst.Location = new System.Drawing.Point(679, 490);
            this.check_is_inst.Name = "check_is_inst";
            this.check_is_inst.Size = new System.Drawing.Size(27, 13);
            this.check_is_inst.TabIndex = 6;
            this.check_is_inst.Text = "bool";
            this.check_is_inst.Click += new System.EventHandler(this.check_is_inst_Click);
            // 
            // ex_df_lstv04
            // 
            this.ex_df_lstv04.CheckBoxes = true;
            this.ex_df_lstv04.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.ex_df_lstv04.HideSelection = false;
            this.ex_df_lstv04.Location = new System.Drawing.Point(318, 41);
            this.ex_df_lstv04.Name = "ex_df_lstv04";
            this.ex_df_lstv04.Size = new System.Drawing.Size(277, 410);
            this.ex_df_lstv04.TabIndex = 7;
            this.ex_df_lstv04.UseCompatibleStateImageBehavior = false;
            this.ex_df_lstv04.View = System.Windows.Forms.View.Details;
            this.ex_df_lstv04.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ex_df_lstv04_column_click);
            this.ex_df_lstv04.SelectedIndexChanged += new System.EventHandler(this.ex_df_lstv04_SelectedIndexChanged);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Parameter Groups Name";
            this.columnHeader7.Width = 102;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Width = 78;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(684, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1027, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "label3";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // isType_rd_btn
            // 
            this.isType_rd_btn.AutoSize = true;
            this.isType_rd_btn.Location = new System.Drawing.Point(90, 20);
            this.isType_rd_btn.Name = "isType_rd_btn";
            this.isType_rd_btn.Size = new System.Drawing.Size(56, 17);
            this.isType_rd_btn.TabIndex = 11;
            this.isType_rd_btn.TabStop = true;
            this.isType_rd_btn.Text = "isType";
            this.isType_rd_btn.UseVisualStyleBackColor = true;
            this.isType_rd_btn.CheckedChanged += new System.EventHandler(this.isType_rd_btn_CheckedChanged);
            // 
            // grp_bx01
            // 
            this.grp_bx01.Controls.Add(this.isType_rd_btn);
            this.grp_bx01.Controls.Add(this.isInstance_rd_btn);
            this.grp_bx01.Location = new System.Drawing.Point(682, 510);
            this.grp_bx01.Name = "grp_bx01";
            this.grp_bx01.Size = new System.Drawing.Size(155, 47);
            this.grp_bx01.TabIndex = 12;
            this.grp_bx01.TabStop = false;
            this.grp_bx01.Text = "select parameter type";
            this.grp_bx01.Enter += new System.EventHandler(this.grp_bx01_Enter);
            // 
            // add_params_to_multi_fams_Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1394, 591);
            this.Controls.Add(this.grp_bx01);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ex_df_lstv04);
            this.Controls.Add(this.check_is_inst);
            this.Controls.Add(this.add_prm_btn01);
            this.Controls.Add(this.fams_lstv03);
            this.Controls.Add(this.bltin_grp_lstv02);
            this.Controls.Add(this.prm_grps_lstv01);
            this.Controls.Add(this.shd_fl_pth_labl1);
            this.Name = "add_params_to_multi_fams_Form1";
            this.Text = "add_params_to_multi_fams__Form1";
            this.Load += new System.EventHandler(this.add_params_to_multi_fams__Form1_Load);
            this.grp_bx01.ResumeLayout(false);
            this.grp_bx01.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label shd_fl_pth_labl1;
        private System.Windows.Forms.ListView prm_grps_lstv01;
        private System.Windows.Forms.ColumnHeader ParameterDefGroupName;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView bltin_grp_lstv02;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView fams_lstv03;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button add_prm_btn01;
        private System.Windows.Forms.RadioButton isInstance_rd_btn;
        private System.Windows.Forms.Label check_is_inst;
        private System.Windows.Forms.ListView ex_df_lstv04;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton isType_rd_btn;
        private System.Windows.Forms.GroupBox grp_bx01;
    }
}