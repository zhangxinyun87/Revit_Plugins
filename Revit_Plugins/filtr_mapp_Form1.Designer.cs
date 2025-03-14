namespace Revit_Plugins
{
    public partial class filtr_mapp_Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

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
        public void InitializeComponent()
        {
            this.lstv_cats = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.trv_view_01 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.lstv_prms = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.lstv_tmp = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dt_grd_color = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbx_param = new System.Windows.Forms.ComboBox();
            this.cmbx_fill_pttrn = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dt_grd_color)).BeginInit();
            this.SuspendLayout();
            // 
            // lstv_cats
            // 
            this.lstv_cats.CheckBoxes = true;
            this.lstv_cats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstv_cats.HideSelection = false;
            this.lstv_cats.Location = new System.Drawing.Point(3, 12);
            this.lstv_cats.Name = "lstv_cats";
            this.lstv_cats.Size = new System.Drawing.Size(198, 184);
            this.lstv_cats.TabIndex = 0;
            this.lstv_cats.UseCompatibleStateImageBehavior = false;
            this.lstv_cats.View = System.Windows.Forms.View.Details;
            this.lstv_cats.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstv_cats_ColumnClick);
            this.lstv_cats.SelectedIndexChanged += new System.EventHandler(this.lstv_cats_SelectedIndexChanged);
            // 
            // trv_view_01
            // 
            this.trv_view_01.CheckBoxes = true;
            this.trv_view_01.Location = new System.Drawing.Point(207, 12);
            this.trv_view_01.Name = "trv_view_01";
            this.trv_view_01.Size = new System.Drawing.Size(343, 524);
            this.trv_view_01.TabIndex = 1;
            this.trv_view_01.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_view_01_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 437);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lstv_prms
            // 
            this.lstv_prms.CheckBoxes = true;
            this.lstv_prms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lstv_prms.HideSelection = false;
            this.lstv_prms.Location = new System.Drawing.Point(3, 226);
            this.lstv_prms.Name = "lstv_prms";
            this.lstv_prms.Size = new System.Drawing.Size(198, 184);
            this.lstv_prms.TabIndex = 3;
            this.lstv_prms.UseCompatibleStateImageBehavior = false;
            this.lstv_prms.View = System.Windows.Forms.View.Details;
            this.lstv_prms.SelectedIndexChanged += new System.EventHandler(this.lstv_prms_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 539);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lstv_tmp
            // 
            this.lstv_tmp.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.lstv_tmp.HideSelection = false;
            this.lstv_tmp.Location = new System.Drawing.Point(556, 12);
            this.lstv_tmp.Name = "lstv_tmp";
            this.lstv_tmp.Size = new System.Drawing.Size(119, 462);
            this.lstv_tmp.TabIndex = 5;
            this.lstv_tmp.UseCompatibleStateImageBehavior = false;
            this.lstv_tmp.View = System.Windows.Forms.View.Details;
            this.lstv_tmp.SelectedIndexChanged += new System.EventHandler(this.lstv_tmp_SelectedIndexChanged);
            // 
            // dt_grd_color
            // 
            this.dt_grd_color.AllowUserToAddRows = false;
            this.dt_grd_color.AllowUserToDeleteRows = false;
            this.dt_grd_color.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dt_grd_color.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dt_grd_color.Location = new System.Drawing.Point(690, 28);
            this.dt_grd_color.Name = "dt_grd_color";
            this.dt_grd_color.Size = new System.Drawing.Size(362, 508);
            this.dt_grd_color.TabIndex = 6;
            this.dt_grd_color.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dt_grd_color_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // cmbx_param
            // 
            this.cmbx_param.FormattingEnabled = true;
            this.cmbx_param.Location = new System.Drawing.Point(12, 453);
            this.cmbx_param.Name = "cmbx_param";
            this.cmbx_param.Size = new System.Drawing.Size(189, 21);
            this.cmbx_param.TabIndex = 7;
            this.cmbx_param.SelectedIndexChanged += new System.EventHandler(this.cmbx_param_SelectedIndexChanged);
            // 
            // cmbx_fill_pttrn
            // 
            this.cmbx_fill_pttrn.FormattingEnabled = true;
            this.cmbx_fill_pttrn.Location = new System.Drawing.Point(12, 492);
            this.cmbx_fill_pttrn.Name = "cmbx_fill_pttrn";
            this.cmbx_fill_pttrn.Size = new System.Drawing.Size(189, 21);
            this.cmbx_fill_pttrn.TabIndex = 8;
            this.cmbx_fill_pttrn.SelectedIndexChanged += new System.EventHandler(this.cmbx_fill_pttrn_SelectedIndexChanged);
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(693, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "label3";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(786, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 19);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(880, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 19);
            this.button2.TabIndex = 11;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 567);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "label4";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // filtr_mapp_Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1084, 602);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbx_fill_pttrn);
            this.Controls.Add(this.cmbx_param);
            this.Controls.Add(this.dt_grd_color);
            this.Controls.Add(this.lstv_tmp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstv_prms);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trv_view_01);
            this.Controls.Add(this.lstv_cats);
            this.Name = "filtr_mapp_Form1";
            this.Text = "filtr_mapp_Form1";
            this.Load += new System.EventHandler(this.filtr_mapp_Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_grd_color)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstv_cats;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TreeView trv_view_01;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstv_prms;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lstv_tmp;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.DataGridView dt_grd_color;
        private System.Windows.Forms.ComboBox cmbx_param;
        private System.Windows.Forms.ComboBox cmbx_fill_pttrn;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        private System.Windows.Forms.DataGridViewButtonColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.Label label4;
    }
}