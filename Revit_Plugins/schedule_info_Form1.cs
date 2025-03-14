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
using Autodesk.Revit.DB.Electrical;

namespace Revit_Plugins
{
    public partial class schedule_info_Form1 : System.Windows.Forms.Form
    {
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
        public schedule_info_Form1(Autodesk.Revit.DB.Document doc, Autodesk.Revit.UI.UIApplication uiapp)
        {
            InitializeComponent();
            Doc = doc;
            Uiapp = uiapp;
        }

        private void schedule_info_Form1_Load(object sender, EventArgs e)
        {
            List<ViewSchedule> extnl_lst = new List<ViewSchedule>();
            List<ViewSchedule> intnl_lst = new List<ViewSchedule>();

            List<Element> list = new FilteredElementCollector(Doc).OfClass(typeof(ViewSchedule)).ToElements().ToList();
            /*
            List<ViewSchedule> n_elems = new List<ViewSchedule>();
            ElementId n_id1 = new ElementId(424524);
            Element elem1 = Doc.GetElement(n_id1);
            ViewSchedule v3 = elem1 as ViewSchedule;
            ElementId n_id2 = new ElementId(424488);
            Element elem2 = Doc.GetElement(n_id2);
            ViewSchedule v2 = elem2 as ViewSchedule;

            n_elems.Add(v3);
            n_elems.Add(v2);

            foreach(ViewSchedule v1 in n_elems)
            {
                TableData tb = v1.GetTableData();
                TableSectionData tb_sd = tb.GetSectionData(SectionType.Body);
                for (int i = 0; i < 3; i++)
                {
                    int row = 0;
                    int col = i;
                    TableCellStyle sd_st = tb_sd.GetTableCellStyle(row, col);
                    Autodesk.Revit.DB.Color n_col = new Autodesk.Revit.DB.Color(181, 196, 221);
                    //try new table cell style
                    TableCellStyle n_sd_st = new TableCellStyle();
                    TableCellStyleOverrideOptions opt = new TableCellStyleOverrideOptions();
                    opt.BackgroundColor = true;
                    n_sd_st.SetCellStyleOverrideOptions(opt);
                    n_sd_st.BackgroundColor = n_col;
                    using (Transaction trns = new Transaction(Doc))
                    {
                        trns.Start("set_color");
                        //tb_sd.ResetCellOverride(0);
                        tb_sd.AllowOverrideCellStyle(0, col);

                        
                        TableCellStyleOverrideOptions sd_ovv = sd_st.GetCellStyleOverrideOptions();
                        if (sd_ovv.BackgroundColor == true)
                        {
                            sd_st.BackgroundColor = n_col;
                        }
                        else
                        {
                            sd_ovv.BackgroundColor = true;
                            sd_st.BackgroundColor = n_col;
                        }
                        

                        tb_sd.SetCellStyle(0, col, n_sd_st);

                        trns.Commit();
                        Autodesk.Revit.DB.Color n1_col = sd_st.BackgroundColor;
                        string str = n1_col.Red.ToString() + "," + n1_col.Green.ToString() + "," + n1_col.Blue.ToString();
                        lstb_test1.Items.Add(v1.Name + str);
                    }
                }

            }
            */
            
            foreach (Element l in list)
            {
                ViewSchedule v1 = l as ViewSchedule;
                string prm_cat = v1.LookupParameter("#CategoriaVista").AsValueString();
                lstb_test1.Items.Add(v1.Name + v1.Id.ToString());
                //v1.IsTemplate == false && v1.GetPlacementOnSheetStatus() == ViewPlacementOnSheetStatus.CompletelyPlaced && prm_cat == "02-Tavola"
                if (v1.Title.Contains("Interno"))
                {
                    //extnl_lst.Add(v1 as ViewSchedule);
                    
                    TableData tb = v1.GetTableData();
                    TableSectionData tb_sd = tb.GetSectionData(SectionType.Body);
                    
                    /*
                    for(int i = 0; i<3; i++)
                    {
                        int row = 0;
                        int col = i;
                        TableCellStyle sd_st = tb_sd.GetTableCellStyle(row, col);
                        Autodesk.Revit.DB.Color n_col = new Autodesk.Revit.DB.Color(181, 196, 221);
                        //try new table cell style
                        TableCellStyle n_sd_st = new TableCellStyle();
                        TableCellStyleOverrideOptions opt = new TableCellStyleOverrideOptions();
                        opt.BackgroundColor = true;
                        n_sd_st.SetCellStyleOverrideOptions(opt);
                        n_sd_st.BackgroundColor = n_col;
                        using (Transaction trns = new Transaction(Doc))
                        {
                            trns.Start("set_color");
                            //tb_sd.ResetCellOverride(0);
                            tb_sd.AllowOverrideCellStyle(0, col);

                            
                            TableCellStyleOverrideOptions sd_ovv = sd_st.GetCellStyleOverrideOptions();
                            if (sd_ovv.BackgroundColor == true)
                            {
                                sd_st.BackgroundColor = n_col;
                            }
                            else
                            {
                                sd_ovv.BackgroundColor = true;
                                sd_st.BackgroundColor = n_col;
                            }
                            
                            tb_sd.SetCellStyle(0, col, n_sd_st);

                            trns.Commit();
                            Autodesk.Revit.DB.Color n1_col = sd_st.BackgroundColor;
                            string str = n1_col.Red.ToString() + "," + n1_col.Green.ToString() + "," + n1_col.Blue.ToString();
                            //lstb_test1.Items.Add(v1.Name + "--" + str);


                        }
                    }
                    */



                    //lstb_test1.Items.Add(v1.Name.ToString());
                }
                
                
                
                
               
                
                //lstb_test1.Items.Add(v1.Name.ToString());
                /*
                ScheduleDefinition sch_def = v1.Definition;
                List<ScheduleSortGroupField> lst_grp_f = (List<ScheduleSortGroupField>)sch_def.GetSortGroupFields();
                using (Transaction trns0 = new Transaction(Doc))
                {
                    foreach(ScheduleSortGroupField grp in lst_grp_f)
                    {
                        lstb_test1.Items.Add(v1.Name.ToString() + grp.ShowHeader.ToString());
                        trns0.Start("set_header");
                        if(grp.ShowHeader == true)
                        {
                            grp.ShowHeader = false;
                        }
                        trns0.Commit(); 
                    }
                }
                */
                
            }
            /*
            foreach(ViewSchedule extnal_v in extnl_lst)
            {

               
                foreach (Element l in list)
                {
                    ViewSchedule v1 = l as ViewSchedule;
                    if (extnal_v.Name == v1.Name)
                    {
                        intnl_lst.Add(v1);
                        
                    }
                }
            }

            
            foreach (ViewSchedule v1 in intnl_lst)
            {
                
                
                TableData tb = v1.GetTableData();
                TableSectionData tb_sd = tb.GetSectionData(SectionType.Body);
                for (int i = 0; i < 3; i++)
                {
                    int row = 0;
                    int col = i;
                    TableCellStyle sd_st = tb_sd.GetTableCellStyle(row, col);
                    Autodesk.Revit.DB.Color n_col = new Autodesk.Revit.DB.Color(181, 196, 221);
                    //try new table cell style
                    TableCellStyle n_sd_st = new TableCellStyle();
                    TableCellStyleOverrideOptions opt = new TableCellStyleOverrideOptions();
                    opt.BackgroundColor = true;
                    n_sd_st.SetCellStyleOverrideOptions(opt);
                    n_sd_st.BackgroundColor = n_col;
                    using (Transaction trns = new Transaction(Doc))
                    {
                        trns.Start("set_color");
                        //tb_sd.ResetCellOverride(0);
                        tb_sd.AllowOverrideCellStyle(0, col);


                        tb_sd.SetCellStyle(0, col, n_sd_st);

                        trns.Commit();
                        Autodesk.Revit.DB.Color n1_col = sd_st.BackgroundColor;
                        string str = n1_col.Red.ToString() + "," + n1_col.Green.ToString() + "," + n1_col.Blue.ToString();
                        lstb_test1.Items.Add(v1.Name + "--" + str);


                    }
                }
                
            }
            */
        }

        private void lstb_test1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
