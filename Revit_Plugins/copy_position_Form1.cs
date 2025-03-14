using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using static Revit_Plugins.btn_chck_in_v;
using System.Reflection.Emit;
using System.Collections;

namespace Revit_Plugins
{
    public partial class copy_position_Form1 : System.Windows.Forms.Form
    {
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
        public copy_position_Form1(Document doc, UIApplication uiapp)
        {
            
            InitializeComponent();
            this.Doc = doc;
            this.Uiapp = uiapp;
            

        }

        public class docs_info
        {
            public Document n_doc { get; set; }
            public string doc_name { get; set; }

        }

        public Document frm_doc { get; set; }
        public Document loc_doc { get; set; }
        private void copy_position_Form1_Load(object sender, EventArgs e)
        {

            DocumentSet all_docs = Uiapp.Application.Documents;
            List<docs_info> all_docs_info = new List<docs_info>();  
            foreach(Document a_doc in all_docs)
            {
                docs_info a_doc_info = new docs_info();
                a_doc_info.n_doc = a_doc;
                a_doc_info.doc_name = a_doc.Title;
                all_docs_info.Add(a_doc_info);
            }
            from_file_cmbx01.DataSource = all_docs_info;
            loc_file_cmbx01.DataSource = all_docs_info;
            from_file_cmbx01.DisplayMember = "doc_name";
            loc_file_cmbx01.DisplayMember = "doc_name";


            
        }

        private void from_file_cmbx01_SelectedIndexChanged(object sender, EventArgs e)
        {
            docs_info frm_dc_info = from_file_cmbx01.SelectedItem as docs_info;
            Document frm_dc = frm_dc_info.n_doc;
            frm_doc = frm_dc;

        }

        private void loc_file_cmbx01_SelectedIndexChanged(object sender, EventArgs e)
        {
            docs_info loc_dc_info = loc_file_cmbx01.SelectedItem as docs_info;
            Document loc_dc = loc_dc_info.n_doc;
            loc_doc = loc_dc;
        }

        private void from_file_dtgrd2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void loc_file_dtgrd1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
