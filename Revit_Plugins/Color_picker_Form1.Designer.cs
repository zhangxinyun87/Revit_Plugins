namespace Revit_Plugins
{
    public partial class Color_picker_Form1
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
            this.pic_bx1 = new System.Windows.Forms.PictureBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lbl_r = new System.Windows.Forms.Label();
            this.lbl_g = new System.Windows.Forms.Label();
            this.lbl_b = new System.Windows.Forms.Label();
            this.txt_r = new System.Windows.Forms.TextBox();
            this.txt_g = new System.Windows.Forms.TextBox();
            this.txt_b = new System.Windows.Forms.TextBox();
            this.pnl_sel_colr = new System.Windows.Forms.Panel();
            this.btn_ok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pic_bx1)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_bx1
            // 
            this.pic_bx1.Image = global::Revit_Plugins.Properties.Resources.ma9l52m4;
            this.pic_bx1.Location = new System.Drawing.Point(89, 26);
            this.pic_bx1.Name = "pic_bx1";
            this.pic_bx1.Size = new System.Drawing.Size(326, 217);
            this.pic_bx1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_bx1.TabIndex = 0;
            this.pic_bx1.TabStop = false;
            this.pic_bx1.Click += new System.EventHandler(this.pic_bx1_Click);
            this.pic_bx1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_bx1_MouseClick);
            this.pic_bx1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_bx1_2MouseHover);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 394);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // lbl_r
            // 
            this.lbl_r.AutoSize = true;
            this.lbl_r.Location = new System.Drawing.Point(12, 39);
            this.lbl_r.Name = "lbl_r";
            this.lbl_r.Size = new System.Drawing.Size(18, 13);
            this.lbl_r.TabIndex = 2;
            this.lbl_r.Text = "R:";
            this.lbl_r.Click += new System.EventHandler(this.lbl_r_Click);
            // 
            // lbl_g
            // 
            this.lbl_g.AutoSize = true;
            this.lbl_g.Location = new System.Drawing.Point(12, 91);
            this.lbl_g.Name = "lbl_g";
            this.lbl_g.Size = new System.Drawing.Size(18, 13);
            this.lbl_g.TabIndex = 3;
            this.lbl_g.Text = "G:";
            this.lbl_g.Click += new System.EventHandler(this.lbl_g_Click);
            // 
            // lbl_b
            // 
            this.lbl_b.AutoSize = true;
            this.lbl_b.Location = new System.Drawing.Point(12, 150);
            this.lbl_b.Name = "lbl_b";
            this.lbl_b.Size = new System.Drawing.Size(17, 13);
            this.lbl_b.TabIndex = 4;
            this.lbl_b.Text = "B:";
            this.lbl_b.Click += new System.EventHandler(this.lbl_b_Click);
            // 
            // txt_r
            // 
            this.txt_r.Location = new System.Drawing.Point(89, 270);
            this.txt_r.Name = "txt_r";
            this.txt_r.Size = new System.Drawing.Size(210, 20);
            this.txt_r.TabIndex = 5;
            this.txt_r.TextChanged += new System.EventHandler(this.txt_r_TextChanged);
            // 
            // txt_g
            // 
            this.txt_g.Location = new System.Drawing.Point(89, 307);
            this.txt_g.Name = "txt_g";
            this.txt_g.Size = new System.Drawing.Size(210, 20);
            this.txt_g.TabIndex = 6;
            this.txt_g.TextChanged += new System.EventHandler(this.txt_g_TextChanged);
            // 
            // txt_b
            // 
            this.txt_b.Location = new System.Drawing.Point(89, 347);
            this.txt_b.Name = "txt_b";
            this.txt_b.Size = new System.Drawing.Size(210, 20);
            this.txt_b.TabIndex = 7;
            this.txt_b.TextChanged += new System.EventHandler(this.txt_b_TextChanged);
            // 
            // pnl_sel_colr
            // 
            this.pnl_sel_colr.Location = new System.Drawing.Point(314, 270);
            this.pnl_sel_colr.Name = "pnl_sel_colr";
            this.pnl_sel_colr.Size = new System.Drawing.Size(100, 96);
            this.pnl_sel_colr.TabIndex = 8;
            this.pnl_sel_colr.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_sel_colr_Paint);
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(314, 388);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(101, 26);
            this.btn_ok.TabIndex = 9;
            this.btn_ok.Text = "ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // Color_picker_Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 426);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.pnl_sel_colr);
            this.Controls.Add(this.txt_b);
            this.Controls.Add(this.txt_g);
            this.Controls.Add(this.txt_r);
            this.Controls.Add(this.lbl_b);
            this.Controls.Add(this.lbl_g);
            this.Controls.Add(this.lbl_r);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.pic_bx1);
            this.Name = "Color_picker_Form1";
            this.Text = "Color_picker_Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Color_picker_Form1_OnClosed);
            this.Load += new System.EventHandler(this.Color_picker_Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_bx1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_bx1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.Label lbl_r;
        public System.Windows.Forms.Label lbl_g;
        public System.Windows.Forms.Label lbl_b;
        public System.Windows.Forms.TextBox txt_r;
        public System.Windows.Forms.TextBox txt_g;
        public System.Windows.Forms.TextBox txt_b;
        private System.Windows.Forms.Panel pnl_sel_colr;
        private System.Windows.Forms.Button btn_ok;
    }
}