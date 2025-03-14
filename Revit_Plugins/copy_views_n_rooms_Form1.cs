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
using Autodesk.Revit.DB.Architecture;
using static Revit_Plugins.copy_elements_bt_models_Form1;

namespace Revit_Plugins
{
    public partial class copy_views_n_rooms_Form1 : System.Windows.Forms.Form
    {
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
        public copy_views_n_rooms_Form1(Autodesk.Revit.DB.Document doc, Autodesk.Revit.UI.UIApplication uiapp)
        {
            InitializeComponent();
            Doc = doc;
            Uiapp = uiapp;
        }
        class ListViewItemComparer : IComparer
        {
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }
        public List<Autodesk.Revit.DB.View> al_views { get; set; }
        public List<Element> al_rms { get; set; }
        public List<Element> al_shts { get; set; } 
        public Document sel_doc { get; set; }
        public class doc_info
        {
            public Document l_doc { get; set; }
            public string l_title { get; set; }
        }
        public List<doc_info> lnk_docs { get; set; }

        private void copy_views_n_rooms_Form1_Load(object sender, EventArgs e)
        {
            DocumentSet l_docs_set = Doc.Application.Documents;
            List<doc_info> lst_docs = new List<doc_info>();
            foreach (Document nl_doc in l_docs_set)
            {
                if(nl_doc != Doc)
                {
                    doc_info dc_info = new doc_info();

                    dc_info.l_doc = nl_doc;
                    dc_info.l_title = nl_doc.Title;
                    lst_docs.Add(dc_info);
                }
                

            }
            /*
            List<Element> rvt_lnks = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_RvtLinks).WhereElementIsNotElementType().ToElements().ToList();

            List<doc_info> lst_docs = new List<doc_info>();
            foreach (Element rvt_lnk in rvt_lnks)
            {
                RevitLinkInstance r_lnk = rvt_lnk as RevitLinkInstance;
                Document n_l_doc = r_lnk.GetLinkDocument();
                if (n_l_doc != null)
                {
                    
                    doc_info dc_info = new doc_info();
                    
                    dc_info.l_doc = n_l_doc;
                    dc_info.l_title = n_l_doc.Title;
                    lst_docs.Add(dc_info);
                    //cmbx_doc_from01.Items.Add( rvt_lnk ); 

                }
            }
            */
            lnk_docs = lst_docs;
            cmbx_doc_from01.DataSource = lnk_docs;
            cmbx_doc_from01.DisplayMember = "l_title";

            
        }


        private void lstv_viws_01_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstv_viws_01_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lstv_viws_01.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        private void lstv_rms_02_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void lstv_rms_02_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lstv_rms_02.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }
        

        private void cmbx_doc_from01_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstv_viws_01.Items.Clear();
            lstv_rms_02.Items.Clear();
            lstv_shts_03.Items.Clear();
            doc_info l_doc_info = cmbx_doc_from01.SelectedItem as doc_info;
            Document l_doc = l_doc_info.l_doc;
            sel_doc = l_doc;

            List<Element> viws_elem = new FilteredElementCollector(l_doc).OfClass(typeof(Autodesk.Revit.DB.View)).WhereElementIsNotElementType().ToElements().ToList();
            List<Element> viws_schd_elem = new FilteredElementCollector(l_doc).OfClass(typeof(ViewSchedule)).WhereElementIsNotElementType().ToElements().ToList();

            List<Autodesk.Revit.DB.View> viws_0 = new List<Autodesk.Revit.DB.View>();
            if (viws_elem != null)
            {
                foreach (Element v_elem in viws_elem)
                {
                    Autodesk.Revit.DB.View v0 = v_elem as Autodesk.Revit.DB.View;
                    
                    if (!v0.IsTemplate && !v0.ViewSpecific)
                    {
                        viws_0.Add(v0);
                        string[] viws_0_info = { v0.Id.ToString(), v0.Name.ToString(), v0.ViewType.ToString()};
                        ListViewItem viws_0_item = new ListViewItem(viws_0_info);
                        viws_0_item.Tag = v0;
                        lstv_viws_01.Items.Add(viws_0_item);
                    }
                    
                }
            }

            if(viws_schd_elem.Count() > 0)
            {
                foreach(Element v_schd_v in viws_schd_elem)
                {
                    ViewSchedule v_schd = v_schd_v as ViewSchedule;
                    if (!v_schd.IsTemplate && !v_schd.ViewSpecific)
                    {
                        viws_0.Add(v_schd);
                        string[] v_schd_info = {v_schd.Id.ToString(), v_schd.Name.ToString(), v_schd.ViewType.ToString()};
                        ListViewItem v_schd_itm = new ListViewItem(v_schd_info);
                        v_schd_itm.Tag = v_schd_info;
                        lstv_viws_01.Items.Add(v_schd_itm);
                    }

                }
                
            }
            al_views = viws_0;

            List<Element> rms_elem = new FilteredElementCollector(l_doc).OfCategory(BuiltInCategory.OST_Rooms).WhereElementIsNotElementType().ToElements().ToList();
            List<Element> rms_0 = new List<Element>();
            if (rms_elem != null)
            {
                foreach (Element rm_elem in rms_elem)
                {
                    Room rm = rm_elem as Room;
                    if (!rm_elem.ViewSpecific && rm.Area != 0)
                    {
                        rms_0.Add(rm_elem);
                        Location rm_loc = rm_elem.Location;
                        LocationPoint rm_loc_pt = rm_loc as LocationPoint;
                        XYZ rm_pt = rm_loc_pt.Point;
                        ElementId rm_lv_id = rm.LevelId;
                        Level rm_lv = l_doc.GetElement(rm_lv_id) as Level;
                        double rm_lv_elev_abs = rm_lv.Elevation;
                        double rm_lv_elev_prj = rm_lv.ProjectElevation;
                        string[] rms_0_info = { rm_elem.Id.ToString(), rm_elem.Name.ToString(), rm_pt.ToString(), rm_lv.Name.ToString(),rm_lv_elev_abs.ToString(), rm_lv_elev_prj.ToString() };
                        ListViewItem rms_0_item = new ListViewItem(rms_0_info);
                        rms_0_item.Tag = rm_elem;
                        lstv_rms_02.Items.Add(rms_0_item);
                        rms_0.Add((Element)rm_elem);

                    }
                }
            }
            al_rms = rms_0;

            List<Element> shts_elem = new FilteredElementCollector(l_doc).OfCategory(BuiltInCategory.OST_Sheets).WhereElementIsNotElementType().ToElements().ToList();
            List<Element> shts_0 = new List<Element>();
            if (shts_elem.Count > 0)
            {
                foreach (Element sht_elem in shts_elem)
                {
                    ViewSheet sht = sht_elem as ViewSheet;
                    string sht_nm = sht.Name;
                    string sht_num = sht.SheetNumber.ToString();
                    string sht_id = sht.Id.ToString();
                    string[] sht_info = { sht_id, sht_num, sht_nm };
                    ListViewItem sht0_itm = new ListViewItem(sht_info);
                    sht0_itm.Tag = sht_elem;
                    lstv_shts_03.Items.Add(sht0_itm);

                }
            }
            al_shts = shts_0;

        }

        private void btn_cp_rm_02_Click(object sender, EventArgs e)
        {
            List<Element> slct_rms = new List<Element>();
            List<ElementId> slct_rms_ids = new List<ElementId>();
            foreach (ListViewItem viws in lstv_rms_02.CheckedItems)
            {
                Element slct_rm = viws.Tag as Element;
                slct_rms_ids.Add(slct_rm.Id);
            }

            using (Transaction trns1 = new Transaction(Doc))
            {
                trns1.Start("start_copy");
                Transform trsfm1 = Transform.Identity;
                CopyPasteOptions cp_pst_opt1 = new CopyPasteOptions();
                ICollection<ElementId> rslt_ids1 = ElementTransformUtils.CopyElements(sel_doc, slct_rms_ids, Doc, trsfm1, cp_pst_opt1);
                trns1.Commit();
            }

        }

        private void btn_cp_v_01_Click(object sender, EventArgs e)
        {
            List<Autodesk.Revit.DB.View> slct_vs = new List<Autodesk.Revit.DB.View> ();
            List<ElementId> slct_vs_ids = new List<ElementId> ();   
            foreach(ListViewItem viws in lstv_viws_01.CheckedItems)
            {
                Element slct_v = viws.Tag as Element; 
                slct_vs_ids.Add(slct_v.Id);
                
            }
            try
            {
                using (Transaction trns1 = new Transaction(Doc))
                {
                    trns1.Start("start_copy");
                    Transform trsfm1 = Transform.Identity;
                    CopyPasteOptions cp_pst_opt1 = new CopyPasteOptions();
                    ICollection<ElementId> rslt_ids1 = ElementTransformUtils.CopyElements(sel_doc, slct_vs_ids, Doc, trsfm1, cp_pst_opt1);
                    trns1.Commit();
                }

            }
            catch (Exception ex) 
            {

            }
            

        }

        private void lstv_shts_03_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstv_shts_03_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lstv_shts_03.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        private void btn_cp_shts_03_Click(object sender, EventArgs e)
        {
            List<Element> slct_shts = new List<Element>();
            List<ElementId> slct_shts_ids = new List<ElementId>();
            foreach (ListViewItem shts in lstv_shts_03.CheckedItems)
            {
                Element slct_elem = shts.Tag as Element;
                using (Transaction trns1  = new Transaction(Doc))
                {
                    trns1.Start("start_to_copy");
                    if (slct_elem != null)
                    {
                        ViewSheet slct_sht = slct_elem as ViewSheet;
                        Autodesk.Revit.DB.View slct_sht_v = slct_sht as Autodesk.Revit.DB.View; 
                        List<ElementId> al_vprts = slct_sht.GetAllViewports().ToList();
                        ElementClassFilter fam_inst_fltr = new ElementClassFilter(typeof(FamilyInstance));
                        List<ElementId> titl_blcs = slct_sht.GetDependentElements(fam_inst_fltr).ToList();
                        List<ElementId> titl_blc = new List<ElementId>();
                        if (titl_blc.Count > 0)
                        {
                            foreach (ElementId blcs_id in titl_blcs)
                            {
                                Element fam_inst_elem = sel_doc.GetElement(blcs_id);
                                Category fam_cat = fam_inst_elem.Category;
                                if (fam_cat != null && fam_cat.BuiltInCategory == BuiltInCategory.OST_TitleBlocks) 
                                { 
                                    titl_blc.Add(blcs_id);
                                
                                }
                            }
                        }
                        


                        ElementId slct_id = slct_sht.Id;
                        List<ElementId> slct_sht_id = new List<ElementId>();
                        slct_sht_id.Add(slct_id);
                        Transform trsfm1 = Transform.Identity;
                        CopyPasteOptions cp_pst_opt1 = new CopyPasteOptions();
                        try
                        {
                            ICollection<ElementId> rslt_ids1 = ElementTransformUtils.CopyElements(sel_doc, slct_sht_id, Doc, trsfm1, cp_pst_opt1);
                            ElementId rslt_id = rslt_ids1.ToList().First();
                            Element elem = Doc.GetElement(rslt_id);
                            ViewSheet new_sht = elem as ViewSheet;
                            Autodesk.Revit.DB.View new_sht_v = new_sht as Autodesk.Revit.DB.View;

                            ICollection<ElementId> new_vprts = ElementTransformUtils.CopyElements(slct_sht, al_vprts, new_sht, trsfm1, cp_pst_opt1);
                            if (titl_blc.Count > 0) 
                            {
                                ICollection<ElementId> new_titlblcs = ElementTransformUtils.CopyElements(slct_sht, titl_blc, new_sht, trsfm1, cp_pst_opt1);
                                foreach(ElementId blcs_id in new_titlblcs)
                                {
                                    Element n_titl_blc = Doc.GetElement(blcs_id);
                                    ElementId owner_v_id = n_titl_blc.OwnerViewId;
                                    Autodesk.Revit.DB.View owner_v = Doc.GetElement(owner_v_id) as Autodesk.Revit.DB.View;
                                    string[] n_t_blc_info = {n_titl_blc.Id.ToString(), n_titl_blc.Name.ToString(), owner_v.Name};
                                    ListViewItem chck_itm = new ListViewItem(n_t_blc_info);
                                    chck_lstv_shts_04.Items.Add(chck_itm);
                                }
                            }

                        }
                        catch (Exception ex) 
                        {
                        
                        }
                        
                        trns1.Commit();

                        //slct_shts_ids.Add(slct_sht.Id);
                    }
                }
                
            }
            /*
            using (Transaction trns1 = new Transaction(Doc))
            {

                trns1.Start("start_copy");
                Transform trsfm1 = Transform.Identity;
                CopyPasteOptions cp_pst_opt1 = new CopyPasteOptions();
                ICollection<ElementId> rslt_ids1 = ElementTransformUtils.CopyElements(sel_doc, slct_shts_ids, Doc, trsfm1, cp_pst_opt1);
                trns1.Commit();
            }
            */
        }

        private void chck_lstv_shts_04_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
