using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Revit_Plugins
{
    public partial class Color_picker_Form1 : System.Windows.Forms.Form
    {
        public class colr_vals
        {
            public byte colr_r { get; set; }
            public byte colr_g { get; set; }
            public byte colr_b { get; set; }
        }
        public colr_vals sel_color { get; set; }
        public string tmp {  get; set; }
        public event EventHandler<colr_vals> ColorSelected;
        public Color_picker_Form1()
        {
            InitializeComponent();
            
            
        }
        public Bitmap colr { get; set; }    

        private void Color_picker_Form1_Load(object sender, EventArgs e)
        {
            Bitmap colr_bit = new Bitmap(pic_bx1.Image);
            colr = colr_bit;
        }


        private void pic_bx1_Click(object sender, EventArgs e)
        {
            

        }

        private void pic_bx1_2MouseHover(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            Color px0_colr = colr.GetPixel(x, y);
            lbl_r.Text = "R:" + px0_colr.R.ToString();
            lbl_g.Text = "G:" + px0_colr.R.ToString();
            lbl_b.Text = "B:" + px0_colr.R.ToString();

        }
        
        private void pic_bx1_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void lbl_r_Click(object sender, EventArgs e)
        {

        }

        private void lbl_g_Click(object sender, EventArgs e)
        {

        }

        private void lbl_b_Click(object sender, EventArgs e)
        {

        }

        private void txt_r_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_g_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_b_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnl_sel_colr_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pic_bx1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            Color px_colr = colr.GetPixel(x, y);
            txt_r.Text = px_colr.R.ToString();
            txt_g.Text = px_colr.G.ToString();
            txt_b.Text = px_colr.B.ToString();
            Color n_colr = new Color();
            byte cl_r = Convert.ToByte(txt_r.Text);
            byte cl_g = Convert.ToByte(txt_g.Text);
            byte cl_b = Convert.ToByte(txt_b.Text);
            n_colr = Color.FromArgb(cl_r,cl_g,cl_b);
            
            pnl_sel_colr.BackColor = n_colr;
            
            

        }

        public void btn_ok_Click(object sender, EventArgs e)
        {
            byte cl_r = Convert.ToByte(txt_r.Text);
            byte cl_g = Convert.ToByte(txt_g.Text);
            byte cl_b = Convert.ToByte(txt_b.Text);
            colr_vals colr_val = new colr_vals();
            colr_val.colr_r = cl_r;
            colr_val.colr_g = cl_g;
            colr_val.colr_b = cl_b;
            
            sel_color = colr_val;
            tmp = cl_r.ToString() +","+ cl_g.ToString() + "," + cl_b.ToString();
            ColorSelected.Invoke(this,colr_val);
            
            this.Close();
           
        }
        
        private void Color_picker_Form1_OnClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                MessageBox.Show(sel_color.colr_r.ToString() + "," + sel_color.colr_g.ToString() + "," + sel_color.colr_b.ToString());
            }
            catch(Exception ex)
            {

            }
            
        }
    }
}
