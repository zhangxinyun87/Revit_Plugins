
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.DB ;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using System.Windows.Documents;
using System.Windows.Controls;

namespace Revit_Plugins
{
    public partial class refresh_views_Form1 : System.Windows.Forms.Form
    {
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
        
        public refresh_views_Form1(Autodesk.Revit.DB.Document doc, Autodesk.Revit.UI.UIApplication uiapp)
        {
            InitializeComponent();
            Doc = doc;
            Uiapp = uiapp;

        }


        private void refresh_views__Form1_Load(object sender, EventArgs e)
        {
            List<Element> all_tgs = new List<Element>();
            List<ICollection<ElementId>> check_ids = new List<ICollection<ElementId>>();
            UIDocument uidoc = Uiapp.ActiveUIDocument;
            List<Element> all_v1shts0 = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_Sheets).ToElements().ToList();
            List<Element> all_v1shts = new List<Element>();
            foreach(Element v in all_v1shts0)
            {
                Parameter prm = v.LookupParameter("#CategoriaVista");
                if (prm != null)
                {
                    string val = prm.AsValueString();
                    if(val == "SRV")
                    {
                        all_v1shts.Add(v);
                    }
                }
            }
            XYZ v1 = new XYZ(0.01,0,0);
            XYZ v2 = new XYZ(-0.01,0,0);
            List<ElementId> no_ids = new List<ElementId>();
            foreach(Element elem in all_v1shts)
            {
                ViewSheet v1_sht = elem as ViewSheet;
                uidoc.RequestViewChange(v1_sht);
                Autodesk.Revit.DB.View v_sh0 = uidoc.ActiveView;
                v_sh0 = v1_sht;
                //List<Viewport> v1_sht_vpts = (List <Viewport>)v1_sht.GetAllViewports();
                List<Autodesk.Revit.DB.View> v1_sht_vs = new List<Autodesk.Revit.DB.View>();
                HashSet <ElementId> v_sht_vids = (HashSet<ElementId>)v1_sht.GetAllPlacedViews();
                foreach (ElementId id in v_sht_vids)
                {
                        
                    Element elemt = Doc.GetElement(id);
                    Autodesk.Revit.DB.View v0 = elemt as Autodesk.Revit.DB.View;
                    using (Transaction trns0 = new Transaction(Doc))
                    {
                        //uidoc.RequestViewChange(v0);
                        Autodesk.Revit.DB.View v00 = uidoc.ActiveView;
                        uidoc.ActiveView = v0;
                        
                       
                        List<ElementId> al_wals_id = new List<ElementId>();
                        
                        if (v0.IsTemplate == false)
                        {
                            
                            FilteredElementCollector fltrdoc0 = new FilteredElementCollector(Doc, v0.Id);
                            List<Element> al_tgs = (List<Element>)fltrdoc0.OfCategory(BuiltInCategory.OST_MaterialTags).WhereElementIsNotElementType().ToElements();
                            List<Element> al_wals = fltrdoc0.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType().ToElements().ToList();
                            //ICollection<ElementId> show_ids = new HashSet<ElementId>();
                            
                            if(al_wals.Count > 0)
                            {
                                foreach(Element wal in al_wals)
                                {
                                    ElementId w_id = wal.Id;
                                    al_wals_id .Add(w_id);  
                                }
                                
                            }

                            //uidoc.Selection.SetElementIds(al_wals_id);
                            List<ElementId> show_ids = new List<ElementId>();
                            foreach (Element el in al_tgs)
                            {
                                IndependentTag tg = el as IndependentTag;
                                
                                ElementId tg_id = tg.Id;
                               
                                show_ids.Add(tg_id);
                                trns0.Start("move tags");
                                if (tg.Pinned == true)
                                {
                                    tg.Pinned = false;
                                    tg.Location.Move(v1);
                                    tg.Location.Move(v2);
                                }
                                else
                                {
                                    tg.Location.Move(v1);
                                    tg.Location.Move(v2);
                                }
                                trns0.Commit();


                                all_tgs.Add(el);
                            }
                            uidoc.Selection.SetElementIds(show_ids);

                            
                            
                            //uidoc.Selection.SetElementIds(no_ids);

                            //v0.DisableTemporaryViewMode(TemporaryViewMode.TemporaryHideIsolate);
                            //v0.DisableTemporaryViewMode(TemporaryViewMode.TemporaryHideIsolate);


                            check_ids.Add(show_ids);
                            
                        }
                        //uidoc.Selection.SetElementIds(show_ids);
                        uidoc.ActiveView = v1_sht;

                        /*
                        using (Transaction trns1 = new Transaction(Doc))
                        {
                            trns1.Start("select items");
                           
                            uidoc.RefreshActiveView();
                            uidoc.Selection.SetElementIds(no_ids);
                            trns1.Commit(); 
                        }
                        */



                    }


                }


            }
                
 
            uidoc.UpdateAllOpenViews();
            List<Element> start_v_elem = new FilteredElementCollector(Doc).OfClass(typeof(StartingViewSettings)).WhereElementIsNotElementType().ToElements().ToList(); 
            StartingViewSettings start_v_stt = start_v_elem.First() as StartingViewSettings;
            
            ElementId start_v_id = start_v_stt.ViewId;
            Autodesk.Revit.DB.View start_v = Doc.GetElement(start_v_id) as Autodesk.Revit.DB.View;
            uidoc.RequestViewChange(start_v);
            uidoc.ActiveView = start_v;
            List<Element> all_v1s = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_Views).ToElements().ToList();

            /*
            foreach (var v1 in all_v1s)
            {

                Autodesk.Revit.DB.View v0 = v1 as Autodesk.Revit.DB.View;
                if(v0.IsTemplate == false)
                {
                    uidoc.RequestViewChange(v0);
                    FilteredElementCollector fltrdoc0 = new FilteredElementCollector(Doc, v0.Id);
                    List<Element> al_tgs = (List<Element>)fltrdoc0.OfClass(typeof(IndependentTag)).WhereElementIsNotElementType().ToElements();
                    //ICollection<ElementId> show_ids = new HashSet<ElementId>();
                    List<ElementId> show_ids = new List<ElementId>();
                    foreach (Element el in al_tgs)
                    {
                        IndependentTag tg = el as IndependentTag;   
                        if(tg.IsMaterialTag == true)
                        {
                            ElementId tg_id = tg.Id;
                            show_ids.Add(tg_id);
                        }
                        
                        

                        all_tgs.Add(el);
                    }
                    uidoc.Selection.SetElementIds(show_ids);
                    //v0.DisableTemporaryViewMode(TemporaryViewMode.TemporaryHideIsolate);
                    //v0.DisableTemporaryViewMode(TemporaryViewMode.TemporaryHideIsolate);
                    lstb_test1.Items.Add(show_ids.ToString());
                    check_ids.Add(show_ids);
                }
                
                
                
            }
            */
            
            UIView uiview = null;
            IList<UIView> ui_v1s = uidoc.GetOpenUIViews();
            List<UIView> ui_v2s = new List<UIView>();
            //Autodesk.Revit.DB.View crnt_v = Doc.ActiveView; 
            foreach (UIView ui_v in ui_v1s)
            {
                if(ui_v.ViewId != start_v.Id)
                {
                    ui_v2s.Add(ui_v);
                    lstb_test1.Items.Add(ui_v.ViewId.ToString());
                    ui_v.Close();
                    //ElementId v_id = ui_v.ViewId;
                    //Element v_01 = Doc.GetElement(v_id);
                    //Autodesk.Revit.DB.View v_02 = v_01 as Autodesk.Revit.DB.View;
                    

                }

                
            }
            labl_tst1.Text = ui_v2s.Count.ToString() + "-" + all_v1shts.Count.ToString();


        }

        private void labl_tst1_Click(object sender, EventArgs e)
        {

        }

        private void lstb_test1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
