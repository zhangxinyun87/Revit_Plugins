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
using win_cntr = System.Windows.Controls;
using static Revit_Plugins.copy_elements_bt_models_Form1;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Electrical;

namespace Revit_Plugins
{
    public partial class copy_elements_bt_models_Form1 : System.Windows.Forms.Form
    {
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;

        public copy_elements_bt_models_Form1(Document doc, UIApplication uiapp)
        {
            InitializeComponent();
            this.Doc = doc;
            this.Uiapp = uiapp;
            
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
        public class custm_cp_hnler: IDuplicateTypeNamesHandler
        {
            public DuplicateTypeAction OnDuplicateTypeNamesFound(DuplicateTypeNamesHandlerArgs args)
            {
                return DuplicateTypeAction.UseDestinationTypes;
            }
        }

        public class sel_docs
        {
            public Document sl_doc {  get; set; }
            public Document dst_doc { get; set; }   
            //public RevitLinkInstance dst_lnk {  get; set; }
            //public RevitLinkType dst_lnk_tp { get; set; }

        }
        public class docs_info
        {
            public Document s_doc { get; set; }
            public string s_title { get; set; } 
        }

        public sel_docs n_sel_docs { get; set; }
        public Document dst_opn_doc_n {  get; set; } 
        public docs_info all_docs { get; set; }
        public List<docs_info> lst_all_docs_frm { get; set; }
        public List<docs_info> lst_all_docs_to { get; set; }

        public ICollection<ElementId> elem_ids_cp_1 {  get; set; }
        public List<ElementId> elem_ids_cp_lst1 { get; set; }
        public List<ElementId> elem_ids_cp_lst2 { get; set; }
        public ICollection<ElementId> elem_ids_cp_2 { get; set; }
        public List<BuiltInCategory> first_group_cats { get; set; }
        public List<BuiltInCategory> secnd_group_cats { get; set; }
        public List<BuiltInCategory> secnd_group_cats_to_cp { get; set; }
        private void copy_elements_bt_models_Form1_Load(object sender, EventArgs e)
        {
            
            //List<Element> rvt_lnks = new FilteredElementCollector(Doc).OfClass(typeof(RevitLinkInstance)).ToElements().ToList();

            //int num_l = rvt_lnks.Count.ToString().Length;


            //BuiltInCategory n_cat = BuiltInCategory.OST_Walls;
           

            
            /*
            foreach (Element rvt_lnk_e in rvt_lnks)
            {
               
                RevitLinkInstance rvt_lnk = rvt_lnk_e as RevitLinkInstance;
                ElementId lnk_id = rvt_lnk.GetTypeId();
                Element lnk = Doc.GetElement(lnk_id);
                RevitLinkType lnk_tpe = lnk as RevitLinkType;

                Document l_doc = rvt_lnk.GetLinkDocument();
                if (l_doc != null)
                {
                    string l_doc_nm = l_doc.Title;

               


                    //if (l_doc_nm == "173.DE.AR.PRG.E1.XXX.M3D.000")
                    if (l_doc_nm == "UNI_BDA_GE000_MNS_0036_M3_ES_M000")
                    {
                        n_sel_docs.sl_doc = l_doc;

                    }
                }


            }
            

            
            //List<Document> opn_dst_doc_lst = opn_dst_doc.ToList();
            //DocumentSetIterator doc_itrs = opn_dst_docs.ForwardIterator();
            
            foreach (Document opn_dst_doc in opn_dst_docs)
            {
                
                string nm = opn_dst_doc.Title.ToString();
                if (nm.Contains("copy_file"))
                {
                    dst_opn_doc_n = opn_dst_doc;
                    n_sel_docs.dst_doc = opn_dst_doc;
                }
                
            }
            */

            DocumentSet opn_dst_docs = Uiapp.Application.Documents;
            List<docs_info> lst_all_docs0 = new List<docs_info>();
            List<docs_info> lst_all_docs1 = new List<docs_info>();
            foreach (Document opn_dst_doc in opn_dst_docs)
            {
                docs_info docs_Info = new docs_info();
                
                docs_Info.s_doc = opn_dst_doc;
                docs_Info.s_title = opn_dst_doc.Title;

                lst_all_docs0.Add(docs_Info);
                lst_all_docs1.Add(docs_Info);

            }
            lst_all_docs_frm = lst_all_docs0;
            lst_all_docs_to = lst_all_docs1;
            

            doc_from_cmbbx1.DataSource = lst_all_docs_frm;
            doc_from_cmbbx1.DisplayMember = "s_title";
            doc_to_cmbbx1.DataSource = lst_all_docs_to;
            doc_to_cmbbx1.DisplayMember = "s_title";
            sel_docs n_sel_docs0 = new sel_docs();
            n_sel_docs = n_sel_docs0;

        }

        private void rvt_lnk_inst_lstv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ElementId> elems_2_cp_2 = new List<ElementId>();
            foreach (ListViewItem ct_to_cp in rvt_lnk_inst_lstv1.CheckedItems)
            {
                BuiltInCategory b_ct_2 = (BuiltInCategory)ct_to_cp.Tag;
                List<ElementId> e_ids2 = new FilteredElementCollector(n_sel_docs.sl_doc).OfCategory(b_ct_2).WhereElementIsNotElementType().ToElementIds().ToList();
                if (e_ids2.Count != 0)
                {
                    foreach (ElementId e_id2 in e_ids2)
                    {
                        if (e_id2 != null)
                        {
                            if (e_id2.IntegerValue != -1)
                            {
                                Element elm_2 = n_sel_docs.sl_doc.GetElement(e_id2);
                                if (elm_2.ViewSpecific == false)
                                {
                                    elems_2_cp_2.Add(e_id2);
                                   
                                }
                                
                            }
                        }


                    }
                }

            }
            elem_ids_cp_lst1 = elems_2_cp_2;
            cnt_lb3.Text = string.Empty;
            cnt_lb3.Text = elems_2_cp_2.Count.ToString();

        }

        private void rvt_lnk_inst_lstv1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.rvt_lnk_inst_lstv1.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        private void copy_fl_btn1_Click(object sender, EventArgs e)
        {

            List<ElementId> cp_final_lst = new List<ElementId>();
            if(elem_ids_cp_lst2.Count != 0)
            {
                foreach(ElementId id2 in elem_ids_cp_lst2)
                {
                    cp_final_lst.Add(id2);
                }
            }
            if (elem_ids_cp_lst1.Count != 0)
            {
                foreach (ElementId id1 in elem_ids_cp_lst1)
                {
                    if(cp_final_lst.Contains(id1) == false)
                    {
                        cp_final_lst.Add(id1);
                    }
                    
                }
            }
            elem_ids_cp_2 = cp_final_lst;
            using (Transaction trns1 = new Transaction(n_sel_docs.dst_doc))
            {
                trns1.Start("start_copy");
                Transform trsfm1 = Transform.Identity;
                CopyPasteOptions cp_pst_opt1 = new CopyPasteOptions();
                ICollection<ElementId> rslt_ids1 = ElementTransformUtils.CopyElements(n_sel_docs.sl_doc, elem_ids_cp_2, n_sel_docs.dst_doc, trsfm1, cp_pst_opt1);
                trns1.Commit();

            }
            

        }

        private void n_rvt_lb1_Click(object sender, EventArgs e)
        {

        }

        private void lnk_rvt_lb2_Click(object sender, EventArgs e)
        {

        }

        private void doc_from_cmbbx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            n_rvt_lb1.Text = string.Empty;
            //n_rvt_lb1.Text =num_l.ToString();
            docs_info frm_dc_Info = doc_from_cmbbx1.SelectedItem as docs_info;
            Document frm_dc = frm_dc_Info.s_doc;
            n_sel_docs.sl_doc = frm_dc;
            //n_sel_docs.sl_doc = dc_Info.s_doc;
            n_rvt_lb1.Text = "from rvt : " + frm_dc_Info.s_doc.Title.ToString();

           

        }

        private void doc_to_cmbbx1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
           
           
            docs_info to_dc_Info = doc_to_cmbbx1.SelectedItem as docs_info;
            Document to_dc = to_dc_Info.s_doc;
            n_sel_docs.dst_doc = to_dc;
            lnk_rvt_lb2.Text = string.Empty;
            lnk_rvt_lb2.Text = "to rvt: " + to_dc_Info.s_doc.Title.ToString(); 
            
            List<ElementId> elems_2_cp_1 = new List<ElementId>();


            ElementClassFilter fltr0 = new ElementClassFilter(typeof(FamilyInstance));
            List<Element> al_elems = new FilteredElementCollector(n_sel_docs.sl_doc).WhereElementIsNotElementType().ToElements().ToList();
            //
            List<ElementId> al_elems_ids = new FilteredElementCollector(n_sel_docs.sl_doc).WhereElementIsNotElementType().ToElementIds().ToList();
            List<ElementId> al_elems_a = new FilteredElementCollector(n_sel_docs.sl_doc).WhereElementIsNotElementType().WherePasses(fltr0).ToElementIds().ToList();
            List<ElementId> al_elems_b = new List<ElementId>();
            foreach (ElementId el in al_elems_ids)
            {
                if (al_elems_a.Contains(el) == false)
                {
                    al_elems_b.Add(el);
                }
            }
            //

            List<BuiltInCategory> b_cat_set_id = new List<BuiltInCategory>();
            List<ElementId> elem_tp_set_id = new List<ElementId>();
            List<Category> cat_set = new List<Category>();  

            //List<BuiltInCategory> lst_2nd_cp = new List<BuiltInCategory>();
            foreach (Element el in al_elems)
            { 
                ElementClassFilter fltr = new ElementClassFilter(typeof(FamilyInstance));

                if (fltr.PassesFilter(el))
                {
                    FamilyInstance el_fam = el as FamilyInstance;
                    FamilySymbol tp = el_fam.Symbol;
                    if (tp != null)
                    {
                        
                        ElementId tp_id = tp.Id;
                        Family fam = tp.Family;
                        Category fam_cat = fam.FamilyCategory;
                        if (fam_cat != null)
                        {
                            if (b_cat_set_id.Contains(fam_cat.BuiltInCategory) == false && elem_tp_set_id.Contains(tp_id) == false && tp.CanBeCopied == true)
                            {
                                b_cat_set_id.Add(fam_cat.BuiltInCategory);
                                elem_tp_set_id.Add((ElementId)tp_id);
                                cat_set.Add(fam_cat);
                            }
                        }
                    }
                    
                }
                
                else
                {
                    Category el_ct = el.Category;
                    ElementId tp_id = el.GetTypeId();
                    if(tp_id != null && tp_id.IntegerValue != -1)
                    {
                        ElementType tp = n_sel_docs.sl_doc.GetElement(tp_id) as ElementType;
                        if (el_ct != null)
                        {
                            if (b_cat_set_id.Contains(el_ct.BuiltInCategory) == false && elem_tp_set_id.Contains(tp_id) == false && tp.CanBeCopied == true)
                            {
                                b_cat_set_id.Add(el_ct.BuiltInCategory);
                                elem_tp_set_id.Add((ElementId)tp_id);
                                cat_set.Add(el_ct);
                            }
                        }
                    }
                    
                   
                    
                }
                   
              

            }
            List<BuiltInCategory> lst_2nd_cp = new List<BuiltInCategory>();

            ElementClassFilter mecc_fltr = new ElementClassFilter(typeof(MechanicalSystem));
            ElementClassFilter plumb_fltr = new ElementClassFilter(typeof(PipingSystem));
            ElementClassFilter elect_fltr = new ElementClassFilter(typeof(ElectricalSystem));
            List<ElementClassFilter> mep_fltrs = new List<ElementClassFilter>();
            mep_fltrs.Add(mecc_fltr);
            mep_fltrs.Add(plumb_fltr);
            mep_fltrs.Add(elect_fltr);
            rvt_lnk_systm_lstv2.Items.Clear();
            foreach (ElementClassFilter fltr in mep_fltrs)
            {
                string[] str_m_fltr = { fltr.GetElementClass().ToString(), fltr.ToString() };
                ListViewItem lstv_fltr = new ListViewItem(str_m_fltr);
                lstv_fltr.Tag = fltr;
                rvt_lnk_systm_lstv2.Items.Add(lstv_fltr);

            }

            
            rvt_lnk_inst_lstv1.Items.Clear();
            List<ListViewItem> lst_elems_transf = new List<ListViewItem>();
            foreach(Category cat2_s in cat_set)
            {
                if(cat2_s != null)
                {
                    BuiltInCategory b_cat = cat2_s.BuiltInCategory;
                    lst_2nd_cp.Add(b_cat);
                    string[] b_cats_id = { b_cat.ToString(), cat2_s.Name, cat2_s.CategoryType.ToString() };
                    ListViewItem cat_info = new ListViewItem(b_cats_id);
                    cat_info.Tag = b_cat;
                    lst_elems_transf.Add(cat_info);
                    rvt_lnk_inst_lstv1.Items.Add(cat_info);
                }
                
                
            }
            secnd_group_cats = lst_2nd_cp;
           
            lstv_cp_fam_tps.Items.Clear();
            List<Element> lst_fam_tps_elems =  new FilteredElementCollector(n_sel_docs.sl_doc).OfClass(typeof(FamilySymbol)).ToElements().ToList();
            foreach (Element fam_tp_elem in lst_fam_tps_elems)
            {
                FamilySymbol fam_tp = fam_tp_elem as FamilySymbol;
                if (fam_tp.CanBeCopied)
                {
                    Family fam = fam_tp.Family;
                    Category f_cat = fam.FamilyCategory;
                    FamilyPlacementType plc_tp = fam.FamilyPlacementType;
                    string plc_tp_nm = System.Enum.GetName(typeof(FamilyPlacementType), plc_tp);
                    string[] fam_tps_info = { f_cat.Name, fam_tp.Name, fam_tp.Id.ToString(), fam.Name, fam.Id.ToString(), plc_tp_nm };
                    ListViewItem lstv_f_tp_info = new ListViewItem(fam_tps_info);
                    lstv_f_tp_info.Tag = fam_tp;
                    lstv_cp_fam_tps.Items.Add(lstv_f_tp_info);

                }
                
            }

        }

        private void copy_grp_btn1_Click(object sender, EventArgs e)
        {
           

        }

        private void cnt_lb3_Click(object sender, EventArgs e)
        {

        }

        private void rvt_lnk_systm_lstv2_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Element> mep_systm = new FilteredElementCollector(n_sel_docs.sl_doc).WhereElementIsNotElementType().ToElements().ToList();
            List<ElementId> lstv_2_cp = new List<ElementId>();  
            foreach (ListViewItem fltr_to_cp in rvt_lnk_systm_lstv2.CheckedItems)
            {
                ElementClassFilter mp_fltr = fltr_to_cp.Tag as ElementClassFilter;
                foreach(Element mep_sys in mep_systm)
                {
                    if (mp_fltr != null)
                    {
                        if (mp_fltr.GetElementClass() == typeof(MechanicalSystem))
                        {
                            if (mp_fltr.PassesFilter(mep_sys))
                            {
                                MechanicalSystem mcc_sys = mep_sys as MechanicalSystem;
                                ElementSet mecc_elems = mcc_sys.DuctNetwork;

                                foreach (Element m_e in mecc_elems)
                                {
                                    ElementId e0_id = m_e.Id;
                                    lstv_2_cp.Add(e0_id);
                                }

                            }

                        }
                        else if (mp_fltr.GetElementClass() == typeof(PipingSystem))
                        {
                            if (mp_fltr.PassesFilter(mep_sys))
                            {
                                PipingSystem plm_sys = mep_sys as PipingSystem;
                                ElementSet plumb_elems = plm_sys.PipingNetwork;

                                foreach (Element p_e in plumb_elems)
                                {
                                    ElementId e1_id = p_e.Id;
                                    lstv_2_cp.Add(e1_id);
                                }

                            }

                        }
                        else if (mp_fltr.GetElementClass() == typeof(ElectricalSystem))
                        {
                            if (mp_fltr.PassesFilter(mep_sys))
                            {
                                ElectricalSystem ele_sys = mep_sys as ElectricalSystem;
                                ElementSet elect_elems = ele_sys.Elements;

                                foreach (Element e_e in elect_elems)
                                {
                                    ElementId e2_id = e_e.Id;
                                    lstv_2_cp.Add(e2_id);
                                }

                            }

                        }
                    }
                }
                
            }
           
            elem_ids_cp_lst2 = lstv_2_cp;
            
            cnt_lb4.Text = string.Empty;
            //int cnt_3 = Int32.Parse(cnt_lb3.Text);
            //int tot = cnt_3 + lstv_2_cp.Count;
            cnt_lb4.Text = lstv_2_cp.Count.ToString();
        }

        private void cnt_lbl5_Click(object sender, EventArgs e)
        {

        }

        private void lstv_cp_fam_tps_SelectedIndexChanged(object sender, EventArgs e)
        {
            cnt_lbl5.Text = lstv_cp_fam_tps.CheckedItems.Count.ToString();   

        }

        private void btn_cp_f_tps_Click(object sender, EventArgs e)
        {   List<ElementId> cp_ct_ids = new List<ElementId>();
            foreach(ListViewItem itm_fam_tp in lstv_cp_fam_tps.CheckedItems)
            {
                FamilySymbol fam_tp_syb_0 = itm_fam_tp.Tag as FamilySymbol;
                Family fam_0 = fam_tp_syb_0.Family;
                Category fam_ct_0 = fam_0.FamilyCategory;
                ElementId fam_ct_id = fam_ct_0.Id;
                if (!cp_ct_ids.Contains(fam_ct_id))
                {
                    cp_ct_ids.Add(fam_ct_id);
                }
            }
            foreach(ElementId ct_id in cp_ct_ids)
            {
                Category ct_0 = Category.GetCategory(n_sel_docs.sl_doc,ct_id);
                string ct_nm = ct_0.Name;
                List<ElementId> cp_f_tp_lst = new List<ElementId>();
                foreach (ListViewItem itm_fam_tp in lstv_cp_fam_tps.CheckedItems)
                {
                    FamilySymbol fam_tp_syb = itm_fam_tp.Tag as FamilySymbol;
                    ElementId fam_tp_id = fam_tp_syb.Id;
                    Family fam = fam_tp_syb.Family;
                    Category f_cat = fam.FamilyCategory;
                    ElementId f_cat_id = f_cat.Id;
                    if(f_cat_id == ct_id)
                    {
                        cp_f_tp_lst.Add(fam_tp_id);
                    }

                    
                }
                ICollection<ElementId> cp_f_tp_col = cp_f_tp_lst;

                using (Transaction trns2 = new Transaction(n_sel_docs.dst_doc))
                {
                    string trns2_str = "start_copy_familytypes_" + ct_nm.ToString();
                    trns2.Start(trns2_str);
                    Transform trsfm1 = Transform.Identity;
                    CopyPasteOptions cp_pst_opt1 = new CopyPasteOptions();
                    cp_pst_opt1.SetDuplicateTypeNamesHandler(new custm_cp_hnler());
                    ICollection<ElementId> rslt_ids1 = ElementTransformUtils.CopyElements(n_sel_docs.sl_doc, cp_f_tp_col, n_sel_docs.dst_doc, trsfm1, cp_pst_opt1);
                    trns2.Commit();

                }

            }

            
        }

        private void lstv_cp_fam_tps_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lstv_cp_fam_tps.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }
    }
}
