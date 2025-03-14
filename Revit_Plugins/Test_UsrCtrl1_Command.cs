using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Events;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;


namespace Revit_Plugins
{
    [Transaction(TransactionMode.Manual)]
    //[Regeneration(RegenerationOption.Manual)]
    internal class Test_UsrCtrl1_Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            try
            {
                //System.Windows.Forms.Application.Run(ctrl_n);

                Test_UserControl1 n_window = new Test_UserControl1(doc, uiapp);
                WindowInteropHelper n_helper = new WindowInteropHelper(n_window);
                n_helper.Owner = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
                n_window.Show();
                return Result.Succeeded;

            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.ToString());
                return Result.Failed;
            }


        }



    }
}

