using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Revit_Plugins
{
    [Transaction(TransactionMode.Manual)]
    //[Regeneration(RegenerationOption.Manual)]
    public class filtr_mapp_Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;
            using (filtr_mapp_Form1 form_n = new filtr_mapp_Form1(doc, uiapp))
            //using(System.Windows.Forms.Form form_n2 = new Form_check_subcat_2(doc))
            {
                try
                {
                    System.Windows.Forms.Application.Run(form_n);
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Error", ex.ToString());
                    return Result.Failed;
                }

                return Result.Succeeded;


            }


        }
    }
}
