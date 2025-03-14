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

namespace Revit_Plugins
{
    public partial class Form_template : System.Windows.Forms.Form
    {
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
        public Form_template(Autodesk.Revit.DB.Document doc, Autodesk.Revit.UI.UIApplication uiapp)
        {
            InitializeComponent();
            Doc = doc;
            Uiapp = uiapp;
        }

        private void Form1_template_Load(object sender, EventArgs e)
        {

        }
    }
}
