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

namespace Revit_Plugins
{
    [Transaction(TransactionMode.Manual)]
    //[Regeneration(RegenerationOption.Manual)]
    internal class clash_visual_Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;
            
        
            using (System.Windows.Forms.Form form_n = new btn_chck_in_v(doc, uiapp))
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
                uiapp.Application.FailuresProcessing += new EventHandler<FailuresProcessingEventArgs>(OnFailureProcessing);
                return Result.Succeeded;

                //return Result.Succeeded;


            }


        }

        private void OnFailureProcessing(object sender, FailuresProcessingEventArgs e)
        {
            FailuresAccessor failure_acc = e.GetFailuresAccessor();
            string trns_nm = failure_acc.GetTransactionName();
            IList<FailureMessageAccessor> fmsms = failure_acc.GetFailureMessages();
            if (trns_nm != null) 
            { 
                foreach (FailureMessageAccessor fmsm in fmsms)
                {
                    failure_acc.ResolveFailure(fmsm);
                }
                e.SetProcessingResult(FailureProcessingResult.ProceedWithCommit);
                return;
            }
            e.SetProcessingResult(FailureProcessingResult.Continue);
        }

    }
}

