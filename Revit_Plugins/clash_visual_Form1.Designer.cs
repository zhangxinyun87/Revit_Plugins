namespace Revit_Plugins
{
    partial class btn_chck_in_v
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.lstV_clash_itms = new System.Windows.Forms.ListView();
            this.clashes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.item_A_nm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.item_A_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.item_A_cat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.item_B_nm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.item_B_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.item_B_cat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(361, 693);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 26);
            this.button1.TabIndex = 2;
            this.button1.Text = "check in view";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(504, 693);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(137, 26);
            this.btn_clear.TabIndex = 3;
            this.btn_clear.Text = "clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // lstV_clash_itms
            // 
            this.lstV_clash_itms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clashes,
            this.item_A_nm,
            this.item_A_id,
            this.item_A_cat,
            this.item_B_nm,
            this.item_B_id,
            this.item_B_cat});
            this.lstV_clash_itms.FullRowSelect = true;
            this.lstV_clash_itms.HideSelection = false;
            this.lstV_clash_itms.Location = new System.Drawing.Point(20, 42);
            this.lstV_clash_itms.Name = "lstV_clash_itms";
            this.lstV_clash_itms.Size = new System.Drawing.Size(620, 621);
            this.lstV_clash_itms.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lstV_clash_itms.TabIndex = 4;
            this.lstV_clash_itms.UseCompatibleStateImageBehavior = false;
            this.lstV_clash_itms.View = System.Windows.Forms.View.Details;
            this.lstV_clash_itms.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstV_clash_itms_ColumnClick);
            this.lstV_clash_itms.SelectedIndexChanged += new System.EventHandler(this.lstV_clash_itms_SelectedIndexChanged);
            // 
            // clashes
            // 
            this.clashes.Text = "clash_num";
            this.clashes.Width = 64;
            // 
            // item_A_nm
            // 
            this.item_A_nm.Text = "item_a_name";
            this.item_A_nm.Width = 99;
            // 
            // item_A_id
            // 
            this.item_A_id.Text = "item_a_id";
            this.item_A_id.Width = 78;
            // 
            // item_A_cat
            // 
            this.item_A_cat.Text = "item_a_category";
            this.item_A_cat.Width = 82;
            // 
            // item_B_nm
            // 
            this.item_B_nm.Text = "item_b_name";
            this.item_B_nm.Width = 103;
            // 
            // item_B_id
            // 
            this.item_B_id.Text = "item_b_id";
            // 
            // item_B_cat
            // 
            this.item_B_cat.Text = "item_b_category";
            // 
            // btn_chck_in_v
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 773);
            this.Controls.Add(this.lstV_clash_itms);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "btn_chck_in_v";
            this.Text = "clash_visual_Form1";
            this.Load += new System.EventHandler(this.clash_visual_Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.ListView lstV_clash_itms;
        private System.Windows.Forms.ColumnHeader item_A_nm;
        private System.Windows.Forms.ColumnHeader item_B_nm;
        public System.Windows.Forms.ColumnHeader clashes;
        private System.Windows.Forms.ColumnHeader item_A_id;
        private System.Windows.Forms.ColumnHeader item_A_cat;
        private System.Windows.Forms.ColumnHeader item_B_id;
        private System.Windows.Forms.ColumnHeader item_B_cat;
    }
}