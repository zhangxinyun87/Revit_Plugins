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
using System.Security.Cryptography.X509Certificates;
using System.Collections;

namespace Revit_Plugins
{
    public partial class copy_views_n_rooms_Form2 : UserControl
    {
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
        public copy_views_n_rooms_Form2(Autodesk.Revit.DB.Document doc, Autodesk.Revit.UI.UIApplication uiapp)
        {
            InitializeComponent();
            Doc = doc;
            Uiapp = uiapp;
        }

        private void copy_views_n_rooms_Form1_Load(object sender, EventArgs e)
        {
           

        }
    }
}
