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
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.Electrical;
using System.Windows.Controls;
using System.Data.Common;
using System.Data.SqlClient;

namespace Revit_Plugins
{
    public partial class item_mappatura_Form1 : System.Windows.Forms.Form
    {
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
        public item_mappatura_Form1(Autodesk.Revit.DB.Document doc, Autodesk.Revit.UI.UIApplication uiapp)
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
                return String.Compare(((System.Windows.Forms.ListViewItem)x).SubItems[col].Text, ((System.Windows.Forms.ListViewItem)y).SubItems[col].Text);
            }
        }
        // temp set sql connection
        private DataTable Populate(string sqlCommand)
        {
            SqlConnection northwindConnection = new SqlConnection("connectionString");
            northwindConnection.Open();

            SqlCommand command = new SqlCommand(sqlCommand, northwindConnection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            adapter.Fill(table);

            return table;
        }

        public class set_cbx_btn
        {
            public System.Windows.Forms.Button xn_btn { get; set; }
            public System.Windows.Forms.Button xn_btn_min { get; set; }
            public System.Windows.Forms.ComboBox xn_cbx { get; set; }
        }

        public class cbm_box_itm
        {
            public string key { get; set; }
            public object value { get; set; }
        }
        
        public bool is_color_in_lst(Autodesk.Revit.DB.Color x_col, List<Autodesk.Revit.DB.Color> x_lst_col)
        {
            bool x_rslt = false;
            List<bool> x_bools = new List<bool>();
            for (int i = 0; i < x_lst_col.Count; i++)
            {
                List<bool> sub_bools = new List<bool>();
                sub_bools.Add(x_col.Red ==  x_lst_col[i].Red);
                sub_bools.Add(x_col.Blue == x_lst_col[i].Blue);
                sub_bools.Add(x_col.Green == x_lst_col[i].Green);
                if (!sub_bools.Contains(false))
                {
                    x_bools.Add(true);
                }
                else
                {
                    x_bools.Add(false);
                }
            }
            if (!x_bools.Contains(true))
            {
                x_rslt = false;  
            }
            else
            {
                x_rslt = true;
            }
            return x_rslt;

        }
        public Autodesk.Revit.DB.Color rnd_col()
        {
            Random rnd = new Random();
            int rd0 = rnd.Next(0, 255);
            int grn0 = rnd.Next(0, 255);
            int blu0 = rnd.Next(0, 255);
            byte rd = (byte)rd0;
            byte grn = (byte)grn0;
            byte blu = (byte)blu0;
            Autodesk.Revit.DB.Color x_col = new Autodesk.Revit.DB.Color(rd, grn, blu);
            return x_col;
        }
        public Autodesk.Revit.DB.Color create_rnd_col(List<Autodesk.Revit.DB.Color> x_lst_col)
        {
            Autodesk.Revit.DB.Color x_col = rnd_col();
            do
            {
                x_col = rnd_col();
            }
            while(is_color_in_lst(x_col, x_lst_col) == true);   
            
            return x_col;
        }
        public OverrideGraphicSettings  set_ovri(OverrideGraphicSettings ov,FillPatternElement ptrn,Autodesk.Revit.DB.Color col)
        {
            ov.SetCutForegroundPatternVisible(true);
            ov.SetCutForegroundPatternId(ptrn.Id);
            ov.SetCutForegroundPatternColor(col);
            ov.SetSurfaceForegroundPatternVisible(true);
            ov.SetSurfaceForegroundPatternId(ptrn.Id);
            ov.SetSurfaceForegroundPatternColor(col);

            return ov;
        }
        public FilledRegionType gt_fild_rgin_tp(string x_nm)
        {
            List<Element> fl_rgin_tp_elems = new FilteredElementCollector(Doc).OfClass(typeof(FilledRegionType)).ToElements().ToList();
            FilledRegionType out_e = null; 
            foreach(Element e in  fl_rgin_tp_elems)
            {
                FilledRegionType fl_rgin_tp = e as FilledRegionType;
                if (fl_rgin_tp.Name == x_nm)
                {   
                    out_e = e as FilledRegionType;
                   
                }
            }
            return out_e;
            
        }

        public class prm_info
        {
            public int p_id { get; set; } 
            public bool shrd {  get; set; }
            public string nm { get; set; }
        }
        public List<FillPatternElement> al_f_pttrn_elems { get; set; }
        private set_cbx_btn origin_set { get; set; }
        public  List<set_cbx_btn> lst_set { get; set; }
        public List<List<Parameter>> lst_al_param { get; set; }
        public string params_nm { get; set; }
        public class fltr_elem_color
        {
            public ParameterFilterElement p_fltr_elem { get; set; }
            public Autodesk.Revit.DB.Color p_colr { get; set; }
            public FillPatternElement fill_pattrn { get; set; }
            public string p_nm { get; set; }    
        }
        public class prm_val_pair
        {
            public int prm_id { get; set; }
            public object prm_val { get; set; }
        }
        public class prm_fltr_elem_set
        {
            public string set_nm { get; set; }
            public List<prm_val_pair> p_v_pairs { get; set; }
        }
        public class prm_vals_color_pair
        {
            public prm_fltr_elem_set prm_f_elem_set { get; set; }
            public Autodesk.Revit.DB.Color rvt_col {  get; set; } 
            public System.Drawing.Color win_col { get; set; }
            public FillPatternElement fill_pattrn { get; set; }

        }
        public class saved_pair_list
        {
            public List<prm_vals_color_pair> lst_pairs { get; set; }
            public string n_lst_nm { get; set; }
        }
        public List<prm_fltr_elem_set> lst_pair_to_create { get; set; }
        public List<prm_vals_color_pair> lst_pair_col_to_create { get; set; }
        public List<fltr_elem_color> lst_pair_elem_color { get; set; }
       
        public ICollection<ElementId> sel_cats_id_col { get; set; }
        
        public string gt_n_name(string e_nm, string sp)
        {
            char c_sp = sp.ToCharArray().FirstOrDefault();
            string[] lst_nm = e_nm.Split(c_sp);
            string num_str = lst_nm[lst_nm.Length-1];
            int num = new int();
            Int32.TryParse(num_str, out num);
            int n_num = num + 1;
            string n_num_str = n_num.ToString().PadLeft(2,'0');
            List<string> n_lst_nm = new List<string>();
            for (int i = 0; i < lst_nm.Length-1; i++)
            {
                n_lst_nm.Add(lst_nm[i]);
            }
            n_lst_nm.Add(n_num_str);
            string n_nm_a = String.Join(sp, n_lst_nm);
            return n_nm_a;
        }
        //form function
        public void btn_Click(object sender, EventArgs e)
        {
            set_cbx_btn nn_pr_set = create_n_set(lst_set.Last(), this);
            lst_set.Add(nn_pr_set);
            label1.Text = lst_set.Last().xn_cbx.Name.ToString();
        }
        public void btn_min_CLick(object sender, EventArgs e)
        {
            remove_n_set(lst_set.Last(),this);
            if (lst_set.Count > 1)
            {
                lst_set.Remove(lst_set.Last());
            }
            label1.Text = lst_set.Last().xn_cbx.Name.ToString();
           
        }

        public void cbx_Indexchanged(object sender, EventArgs e)
        {

        }
        private void remove_n_set(set_cbx_btn e_set, System.Windows.Forms.Form frm)
        {
            System.Windows.Forms.Button e_btn = e_set.xn_btn;
            System.Windows.Forms.Button e_btn_m = e_set.xn_btn_min;
            System.Windows.Forms.ComboBox e_cbx = e_set.xn_cbx;
            frm.Controls.Remove(e_btn);
            frm.Controls.Remove(e_btn_m);
            frm.Controls.Remove(e_cbx);
        }

        public set_cbx_btn create_n_set(set_cbx_btn e_set,System.Windows.Forms.Form frm)
        {
            //here could add a new class for add set of buttons and combobox
            System.Windows.Forms.Button e_btn = e_set.xn_btn;
            System.Windows.Forms.Button e_btn_m = e_set.xn_btn_min;
            System.Windows.Forms.ComboBox e_cbx = e_set.xn_cbx;
            
            string e_btn_nm = e_btn.Name;
            string e_btn_m_nm = e_btn_m.Name;
            string e_btn_m_txt = "-";
            string e_btn_txt = e_btn.Text;
            string e_cbx_nm = e_cbx.Name;
            System.Drawing.Size btn_size = e_btn.Size;
            System.Drawing.Size btn_m_size = e_btn_m.Size;
            System.Drawing.Size cbx_size = e_cbx.Size;
            string chr_sp = '_'.ToString();
            string n_btn_nm = gt_n_name(e_btn_nm, chr_sp);
            string n_btn_m_nm = gt_n_name(e_btn_m_nm, chr_sp);
            string n_cbx_nm = gt_n_name(e_cbx_nm, chr_sp);
            System.Drawing.Point pt_cbx = new System.Drawing.Point(e_cbx.Location.X, e_cbx.Location.Y + 30);
            System.Drawing.Point pt_btn = new System.Drawing.Point(e_btn.Location.X, pt_cbx.Y);
            System.Drawing.Point pt_btn_m = new System.Drawing.Point(e_btn_m.Location.X, pt_cbx.Y);

            //create new set buttons n combobox
            System.Windows.Forms.ComboBox n_cbx = new System.Windows.Forms.ComboBox();
            System.Windows.Forms.Button n_btn = new System.Windows.Forms.Button();
            System.Windows.Forms.Button n_btn_m = new System.Windows.Forms.Button();
            n_cbx.Location = pt_cbx;
            n_btn.Location = pt_btn;
            n_btn_m.Location = pt_btn_m;
            n_cbx.Name = n_cbx_nm;
            n_btn.Name = n_btn_nm;
            n_btn_m.Name = n_btn_m_nm;
            n_btn.Text = e_btn_txt;
            n_btn_m.Text = e_btn_m_txt;
            n_cbx.Size = cbx_size;
            n_btn.Size = btn_size;
            n_btn_m.Size = btn_m_size;
            frm.Controls.Add(n_cbx);
            frm.Controls.Add(n_btn);
            frm.Controls.Add(n_btn_m);

            n_cbx.SelectedIndexChanged += new System.EventHandler(this.cbx_Indexchanged);
            
            List<cbm_box_itm> lst_n_prms = new List<cbm_box_itm>();
            foreach(object itm in e_cbx.Items)
            {
                if(itm != e_cbx.SelectedItem)
                {
                    cbm_box_itm n_itm = itm as cbm_box_itm;
                   
                    lst_n_prms.Add(n_itm);
                }
            }
            n_cbx.DataSource = lst_n_prms;
            n_cbx.DisplayMember = "key";
            n_cbx.ValueMember = "value";

            set_cbx_btn n_set = new set_cbx_btn();
            n_set.xn_btn = n_btn;
            n_set.xn_btn_min = n_btn_m;
            n_set.xn_cbx = n_cbx;
            n_btn.Click += new System.EventHandler(this.btn_Click);
            n_btn_m.Click += new System.EventHandler(this.btn_min_CLick);

;           return n_set;   
        }

        private void item_mappatura_Form1_Load(object sender, EventArgs e)
        {
            Autodesk.Revit.ApplicationServices.Application app = Uiapp.Application;
            DocumentSet al_docs = app.Documents;
            List<Element> al_views = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_Views).WhereElementIsNotElementType().ToElements().ToList();
            ICollection<ElementId> al_cat_ids = ParameterFilterUtilities.GetAllFilterableCategories();
            Categories al_cats = Doc.Settings.Categories;   
            foreach (Document l_doc in al_docs)
            {
                string dc_nm = l_doc.Title;
                string dc_pth = l_doc.PathName;
                string[] l_doc_info = { dc_nm.ToString(), dc_pth.ToString() };
                System.Windows.Forms.ListViewItem lst_v_doc = new System.Windows.Forms.ListViewItem(l_doc_info);
                lst_v_doc.Tag = l_doc;
                lstv_docs.Items.Add(lst_v_doc);
            }

            List<cbm_box_itm> lst_legend_v_itms = new List<cbm_box_itm>();
            foreach (Element v_elem in al_views)
            {
                Autodesk.Revit.DB.View v1 = v_elem as Autodesk.Revit.DB.View;
                string v_nm = v1.Name;
                ElementId v_id = v1.Id;
                ViewType v_tp = v1.ViewType;
                string v_tp_nm = System.Enum.GetName(typeof(ViewType), v_tp);
                string[] v_info = { v_nm, v_id.ToString(), v_tp_nm,v1.IsTemplate.ToString() };
                System.Windows.Forms.ListViewItem lst_v_view = new System.Windows.Forms.ListViewItem(v_info);
                lst_v_view.Tag = v1;
                lstv_views.Items.Add(lst_v_view);
                if(v_tp == ViewType.Legend)
                {
                    cbm_box_itm v_itm = new cbm_box_itm();
                    v_itm.key = v_nm;
                    v_itm.value = v1 as object;
                    lst_legend_v_itms.Add(v_itm);
                }

            }
            cmbx_legend_v.DataSource = lst_legend_v_itms;
            cmbx_legend_v.DisplayMember = "key";
            cmbx_legend_v.ValueMember = "value";    
            
            foreach(ElementId cat_id in al_cat_ids)
            {
                Category cat = Category.GetCategory(Doc, cat_id);
                if(cat != null)
                {
                    string nm = cat.Name;
                    string[] cat_info = { nm, cat_id.IntegerValue.ToString() };
                    System.Windows.Forms.ListViewItem lstv_itm_cats = new System.Windows.Forms.ListViewItem(cat_info);
                    lstv_itm_cats.Tag = cat;
                    lstv_cats_01.Items.Add(lstv_itm_cats);
                }
                
            }
            List<cbm_box_itm> lst_fill_pttrn_itms = new List<cbm_box_itm>();
            List<cbm_box_itm> lst_txt_type_itms = new List<cbm_box_itm>();
            List<Element> al_fill_pattrn_tps = new FilteredElementCollector(Doc).OfClass(typeof(FillPatternElement)).ToElements().ToList();
            List<Element> al_txt_tps = new FilteredElementCollector(Doc).OfClass(typeof(TextNoteType)).ToElements().ToList();
            //can create a class
            List<FillPatternElement> al_pttrns = new List<FillPatternElement>();
            foreach(Element fill_pttrn_tp_elem in al_fill_pattrn_tps)
            {
                FillPatternElement fill_pttn_tp = fill_pttrn_tp_elem as FillPatternElement;
                al_pttrns.Add(fill_pttn_tp);
                FillPattern fill_pttn = fill_pttn_tp.GetFillPattern();
                string f_pttn_nm = fill_pttn.Name;
                cbm_box_itm f_pttn_tp_itm = new cbm_box_itm();  
                f_pttn_tp_itm.key = f_pttn_nm;
                f_pttn_tp_itm.value = fill_pttn_tp as object;
                lst_fill_pttrn_itms.Add(f_pttn_tp_itm);
            }
            al_f_pttrn_elems = al_pttrns;
            cmbx_fillpattern.DataSource = lst_fill_pttrn_itms;
            cmbx_fillpattern.DisplayMember = "key";
            cmbx_fillpattern.ValueMember = "value";

            foreach (Element txt_tp_elem in al_txt_tps)
            {
                TextNoteType txt_tp = txt_tp_elem as TextNoteType;
                string txt_tp_nm = txt_tp.Name;
                cbm_box_itm txt_tp_itm = new cbm_box_itm();
                txt_tp_itm.key = txt_tp_nm;
                txt_tp_itm.value = txt_tp as object;
                lst_txt_type_itms.Add(txt_tp_itm);
            }
            cmbx_txt_type.DataSource = lst_txt_type_itms;
            cmbx_txt_type.DisplayMember = "key";
            cmbx_txt_type.ValueMember = "value";

        }

        private void lstv_views_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstv_cats_01_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbx_prm_01_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_add_01_Click(object sender, EventArgs e)
        {

            List<set_cbx_btn> lst_set_01 = lst_set;
            set_cbx_btn first_pr_set = create_n_set(origin_set, this);
            lst_set_01.Add(first_pr_set);
            
            lst_set = lst_set_01;
            label1.Text = lst_set.Last().xn_cbx.Name.ToString();    
            
        }
        
        private void lstv_docs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            List<ElementId> sel_cat_ids = new List<ElementId>();

            foreach (System.Windows.Forms.ListViewItem sel_cat in lstv_cats_01.CheckedItems)
            {
                Category sel_ct = (Category)sel_cat.Tag;
                ElementId s_ct_id = sel_ct.Id;
                sel_cat_ids.Add(s_ct_id);
            }
            ICollection<ElementId> lst_cats_ids = sel_cat_ids;
            sel_cats_id_col = lst_cats_ids;
            ICollection<ElementId> lst_fltr_prms_ids = ParameterFilterUtilities.GetFilterableParametersInCommon(Doc,lst_cats_ids);
            List<cbm_box_itm> lst_prm_itms = new List<cbm_box_itm>();
            foreach(ElementId prm_id in lst_fltr_prms_ids)
            {
                Element prm = Doc.GetElement(prm_id);
                if ( prm != null )
                {
                    cmbx_prm_01.Items.Add(prm.Name);
                    cbm_box_itm prm_itm = new cbm_box_itm();
                    prm_itm.key = prm.Name;
                    prm_itm.value = prm_id.IntegerValue;
                    lst_prm_itms.Add(prm_itm);

                }
                else
                {
                    BuiltInParameter bip = (BuiltInParameter)prm_id.IntegerValue;
                    string prm_nm = LabelUtils.GetLabelFor(bip);
                    cbm_box_itm prm_itm0 = new cbm_box_itm();
                    prm_itm0.key = prm_nm;
                    prm_itm0.value = prm_id.IntegerValue;
                    lst_prm_itms .Add(prm_itm0);
                    //cmbx_prm_01.Items.Add(prm_itm0);
                }
               
            }
            cmbx_prm_01.DataSource = lst_prm_itms;  
            cmbx_prm_01.DisplayMember = "key";
            cmbx_prm_01.ValueMember = "value";

            set_cbx_btn origin_set_grp = new set_cbx_btn();
            origin_set_grp.xn_cbx = cmbx_prm_01;
            origin_set_grp.xn_btn = btn_add_01;
            origin_set_grp.xn_btn_min = btn_minus;
            origin_set = origin_set_grp;
            List<set_cbx_btn> set_cbx_orign = new List<set_cbx_btn>();
            set_cbx_orign.Add(origin_set);
            lst_set = set_cbx_orign;

            //lst_set.Add(origin_set);

            List<List<Parameter>> al_lst_prms = new List<List<Parameter>>();   
            if (lstv_cats_01.CheckedItems.Count > 0)
            {
                foreach(System.Windows.Forms.ListViewItem lstv_cat in lstv_cats_01.CheckedItems)
                {
                    if (lstv_docs.CheckedItems.Count > 0)
                    {
                        foreach (System.Windows.Forms.ListViewItem lstv_doc in lstv_docs.CheckedItems)
                        {
                            Document l_doc = lstv_doc.Tag as Document;
                            Category cat = lstv_cat.Tag as Category;    
                            List<Element> lnk_elems = new FilteredElementCollector(l_doc).OfCategory(cat.BuiltInCategory).WhereElementIsNotElementType().ToElements().ToList();
                            foreach(Element l_elem in lnk_elems)
                            {
                                List<Parameter> lst_param = new List<Parameter>();
                                ElementId tp_id = l_elem.GetTypeId();
                                if (tp_id != null || tp_id != new ElementId(-1))
                                {
                                    ElementType l_elem_tp = l_doc.GetElement(l_elem.GetTypeId()) as ElementType;
                                    ParameterSet elemtp_prm_set = l_elem_tp.Parameters;
                                    foreach(Parameter etp_prm in  elemtp_prm_set)
                                    {
                                        lst_param.Add(etp_prm);
                                       
                                    } 
                                }
                                ParameterSet elem_prm_set = l_elem.Parameters;
                                foreach (Parameter e_prm in elem_prm_set)
                                {
                                    lst_param.Add(e_prm);
                                }
                                al_lst_prms.Add(lst_param);
                            }

                        }
                    }

                }
                    
            }
            lst_al_param = al_lst_prms;
            


            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            //FillPatternElement sel_pttrn = cmbx_fillpattern.SelectedValue as FillPatternElement;
            List<ParameterFilterElement> prm_fltr_elem_created = new List<ParameterFilterElement>();

            saved_pair_list finl_saved_pair = lstv_saved_grp.CheckedItems[0].Tag as saved_pair_list;
            List<prm_vals_color_pair> lst_finl_pvc_pair = finl_saved_pair.lst_pairs;
            label4.Text = lst_finl_pvc_pair.Count.ToString();   
            string finl_nm = finl_saved_pair.n_lst_nm;
            
            List<fltr_elem_color> finl_pf_elem_pair = new List<fltr_elem_color>();
            Transaction trns = new Transaction(Doc, "create_new_parameterfilterelement");
            trns.Start();
            if (trns.GetStatus() == TransactionStatus.Started)
            {
                
                List < Autodesk.Revit.DB.Color > col_lst = new List<Autodesk.Revit.DB.Color>();
                //foreach (prm_fltr_elem_set p_fltr_elem_set in lst_pair_to_create)
                foreach(prm_vals_color_pair finl_pvc_pair in lst_finl_pvc_pair)
                {
                    if(finl_pvc_pair != null)
                    {
                        prm_fltr_elem_set p_fltr_elem_set = finl_pvc_pair.prm_f_elem_set;
                        Autodesk.Revit.DB.Color p_fltr_col = finl_pvc_pair.rvt_col;
                        FillPatternElement fl_ptn = finl_pvc_pair.fill_pattrn;
                        string p_fltr_e_nm = p_fltr_elem_set.set_nm;
                        //here add if the name is already existed
                        List<prm_val_pair> p_v_prs = p_fltr_elem_set.p_v_pairs;
                        List<ElementFilter> fltr_rls = new List<ElementFilter>();
                        foreach (prm_val_pair pv_pair in p_v_prs)
                        {
                            // here need implement other type of equalruls
                            FilterRule fltr_rl = ParameterFilterRuleFactory.CreateEqualsRule(new ElementId(pv_pair.prm_id), pv_pair.prm_val.ToString());
                            ElementFilter fltr_prm = new ElementParameterFilter(fltr_rl);
                            fltr_rls.Add(fltr_prm);

                        }
                        try
                        {
                            ElementLogicalFilter elem_fltr = new LogicalAndFilter(fltr_rls);
                            
                            try
                            {
                                ParameterFilterElement fltr_e = ParameterFilterElement.Create(Doc, p_fltr_e_nm, sel_cats_id_col);
                                try
                                {
                                    fltr_e.SetElementFilter(elem_fltr);
                                    prm_fltr_elem_created.Add(fltr_e);
                                    fltr_elem_color finl_itm_pair = new fltr_elem_color();
                                    finl_itm_pair.p_fltr_elem = fltr_e;
                                    finl_itm_pair.p_colr = p_fltr_col;
                                    finl_itm_pair.fill_pattrn = fl_ptn;
                                    finl_itm_pair.p_nm = p_fltr_e_nm;
                                    finl_pf_elem_pair.Add(finl_itm_pair);


                                }
                                catch (Exception ex)
                                {

                                }

                            }
                            catch (Exception ex)
                            {

                            }
                            

                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    

                }
                trns.Commit();
            }
            
            
            TextNoteType sel_txt_tp = cmbx_txt_type.SelectedValue as TextNoteType;
            Transaction trns0 = new Transaction(Doc, "create_legend");
            trns0.Start();
            if (trns0.GetStatus() == TransactionStatus.Started)
            {

                List<FilledRegionType> lst_f_regns_tps = new List<FilledRegionType>();
                foreach (fltr_elem_color fltr_set in finl_pf_elem_pair)
                //foreach(prm_vals_color_pair finl_pvc_pair in lst_finl_pvc_pair)
                {
                    
                    ParameterFilterElement fltr_e = fltr_set.p_fltr_elem;
                    Autodesk.Revit.DB.Color fltr_col = fltr_set.p_colr;
                    //string fltr_nm = fltr_e.Name;
                    string fltr_nm = fltr_set.p_nm;
                    FillPatternElement sel_pttrn = fltr_set.fill_pattrn;
                    if (gt_fild_rgin_tp(fltr_nm) == null)
                    {
                        FilledRegionType f_tp0 = new FilteredElementCollector(Doc).OfClass(typeof(FilledRegionType)).ToElements().ToList().First() as FilledRegionType;
                        FilledRegionType n_f_tp = f_tp0.Duplicate(fltr_nm) as FilledRegionType;
                        n_f_tp.ForegroundPatternId = sel_pttrn.Id;
                        n_f_tp.ForegroundPatternColor = fltr_col;
                        lst_f_regns_tps.Add(n_f_tp);

                    }
                    else
                    {
                        FilledRegionType en_f_tp = gt_fild_rgin_tp(fltr_nm);
                        if (en_f_tp != null)
                        {
                            en_f_tp.ForegroundPatternId = sel_pttrn.Id;
                            en_f_tp.ForegroundPatternColor = fltr_col;
                            lst_f_regns_tps.Add(en_f_tp);

                        }

                    }
                }

                Autodesk.Revit.DB.View sel_legnd_v = cmbx_legend_v.SelectedValue as Autodesk.Revit.DB.View;
                sel_legnd_v.Name = finl_nm;
                for (int i = 0; i < lst_f_regns_tps.Count; i++)
                {
                    XYZ dlt_a0 = new XYZ(10, 0, 0);
                    XYZ dlt_b0 = new XYZ(0, 2.5, 0);
                    XYZ pt_0 = new XYZ(9, 3 * i, 0);
                    XYZ pt_1 = pt_0.Add(dlt_a0);
                    XYZ pt_2 = pt_1.Add(dlt_b0);
                    XYZ pt_3 = pt_0.Add(dlt_b0);

                    XYZ pt_txt = pt_2.Add(new XYZ(2, 0, 0));

                    Line ln0 = Line.CreateBound(pt_0, pt_1);
                    Line ln1 = Line.CreateBound(pt_1, pt_2);
                    Line ln2 = Line.CreateBound(pt_2, pt_3);
                    Line ln3 = Line.CreateBound(pt_3, pt_0);

                    CurveLoop crv_loop = new CurveLoop();
                    crv_loop.Append(ln0);
                    crv_loop.Append(ln1);
                    crv_loop.Append(ln2);
                    crv_loop.Append(ln3);
                    IList<CurveLoop> crv_loop_lst = new List<CurveLoop>();
                    crv_loop_lst.Add(crv_loop);
                    FilledRegion n_fl_rgin = FilledRegion.Create(Doc, lst_f_regns_tps[i].Id, sel_legnd_v.Id, crv_loop_lst);
                    string txt = lst_f_regns_tps[i].Name;
                    if (sel_txt_tp != null)
                    {
                        TextNote n_txt_nt = TextNote.Create(Doc, sel_legnd_v.Id, pt_txt, txt, sel_txt_tp.Id);
                    }
                }
                trns0.Commit();
                //label3.Text = lst_pair_elem_color.Count.ToString() + "_&colorset:" + lst_pair_elem_color.Count.ToString() + "_&legend_tp:" + lst_f_regns_tps.Count.ToString() + "_&legend:" + lst_f_regns_tps.Count.ToString();
            }
            

            
            Transaction trns1 = new Transaction(Doc, "create_filters");
            trns1.Start();
            if (trns1.GetStatus() == TransactionStatus.Started)
            {
                foreach (System.Windows.Forms.ListViewItem v0_itm in lstv_views.CheckedItems)
                {
                    Autodesk.Revit.DB.View sel_v = v0_itm.Tag as Autodesk.Revit.DB.View;
                    ICollection<ElementId> fltrs_ids = sel_v.GetFilters();
                    foreach (fltr_elem_color fltr_set0 in finl_pf_elem_pair)
                    {

                        //cbm_box_itm v1_itm = (cbm_box_itm)v0_itm;
                        ParameterFilterElement fltr_e0 = fltr_set0.p_fltr_elem;
                        Autodesk.Revit.DB.Color fltr_col0 = fltr_set0.p_colr;
                        //string fltr_nm = fltr_e0.Name;
                        string fltr_nm = fltr_set0.p_nm;
                        FillPatternElement sel_pttrn = fltr_set0.fill_pattrn;
                        if (fltrs_ids != null)
                        {
                            if (fltrs_ids.Contains(fltr_e0.Id))
                            {
                                OverrideGraphicSettings ovrid1 = sel_v.GetFilterOverrides(fltr_e0.Id) as OverrideGraphicSettings;
                                OverrideGraphicSettings ovrid2 = set_ovri(ovrid1, sel_pttrn, fltr_col0);
                                sel_v.SetFilterOverrides(fltr_e0.Id, ovrid2);
                            }
                            else
                            {
                                sel_v.AddFilter(fltr_e0.Id);
                                OverrideGraphicSettings ovrid0 = new OverrideGraphicSettings();
                                OverrideGraphicSettings ovrid = set_ovri(ovrid0, sel_pttrn, fltr_col0);
                                sel_v.SetFilterOverrides(fltr_e0.Id, ovrid);


                            }

                        }
                        else
                        {
                            sel_v.AddFilter(fltr_e0.Id);
                            OverrideGraphicSettings ovrid3 = new OverrideGraphicSettings();
                            OverrideGraphicSettings ovrid4 = set_ovri(ovrid3, sel_pttrn, fltr_col0);
                            sel_v.SetFilterOverrides(fltr_e0.Id, ovrid4);

                        }


                    }
                }
                trns1.Commit();

            }
            
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            dt_grdv_color.Rows.Clear();
            List<int> al_prms_ids = new List<int>();
            //List<set_cbx_btn> al_lst_set = new List<set_cbx_btn>();
            List<object> lst_val = new List<object>();
            List<List<prm_val_pair>> lst_pairs = new List<List<prm_val_pair>>();
            List<prm_fltr_elem_set> lst_fltr_elem = new List<prm_fltr_elem_set>();
            List<string> val_Set = new List<string>();
            List<string> tmp_str_lst = new List<string>();
            string pre_fx = tbx_itm_prefx.Text;
            for (int i = 0; i < lst_al_param.Count; i++)
            {
                List<Parameter> sub_lst_prm = lst_al_param[i];

                if (sub_lst_prm.Count > 0)
                {
                    StringBuilder sb_val = new StringBuilder();
                    List<prm_val_pair> sub_lst_val = new List<prm_val_pair>();
                    List<string> sb_name_lst = new List<string>();
                    
                    foreach (set_cbx_btn cbx_btn_itm in lst_set)
                    {
                        cbm_box_itm prm_id_itm = cbx_btn_itm.xn_cbx.SelectedItem as cbm_box_itm;

                        int prm_id = (int)prm_id_itm.value;
                        string prm_nm = (string)prm_id_itm.key;
                        
                        foreach (Parameter sub_prm in sub_lst_prm)
                        {
                            if (sub_prm.Id.IntegerValue == prm_id || sub_prm.Definition.Name == prm_nm)
                            {
                                if (sub_prm.StorageType == StorageType.String)
                                {
                                    tmp_str_lst.Add(sub_prm.Definition.Name);
                                    if (sub_prm.AsString() != null)
                                    {
                                        object p_val = (object)sub_prm.AsString();
                                        sb_val.Append(p_val.ToString().ToArray());
                                        prm_val_pair n_val_pair = new prm_val_pair();
                                        n_val_pair.prm_id = prm_id;
                                        n_val_pair.prm_val = p_val;
                                        sub_lst_val.Add(n_val_pair);
                                        sb_name_lst.Add(p_val.ToString());
                                    }

                                }

                            }

                        }

                    }
                    
                    if (!val_Set.Contains(sb_val.ToString()))
                    {
                        val_Set.Add(sb_val.ToString());
                        lst_pairs.Add(sub_lst_val);
                        string sp = "_";
                        sb_name_lst.Add(i.ToString().PadLeft(3, '0'));
                        prm_fltr_elem_set fltr_e_set = new prm_fltr_elem_set();
                        fltr_e_set.set_nm = pre_fx + String.Join(sp, sb_name_lst);
                        fltr_e_set.p_v_pairs = sub_lst_val;
                        lst_fltr_elem.Add(fltr_e_set);


                    }

                }


            }

            string prams_nms = String.Join("_",tmp_str_lst);
            params_nm = prams_nms;  
            lst_pair_to_create = lst_fltr_elem;

            label2.Text = lst_pair_to_create.Count.ToString();
           
            List<Autodesk.Revit.DB.Color> lst_color = new List<Autodesk.Revit.DB.Color>();
            List<fltr_elem_color> lst_fltr_elem_color_set = new List<fltr_elem_color>();
            List<prm_vals_color_pair> lst_itm_pvc_pair = new List<prm_vals_color_pair>();

            DataGridViewRowCollection dt_grd_row_col = dt_grdv_color.Rows;
            for (int i = 0; i < lst_fltr_elem.Count(); i++)
            {
                Autodesk.Revit.DB.Color col = create_rnd_col(lst_color);
                Int32 itm_rd = (Int32)col.Red;
                Int32 itm_grn = (Int32)col.Green;
                Int32 itm_blu = (Int32)col.Blue;
                System.Drawing.Color w_col = System.Drawing.Color.FromArgb(itm_rd, itm_grn, itm_blu);
                string color = "RGB(" + itm_rd.ToString() + "," + itm_grn.ToString() + "," + itm_blu.ToString() + ")";

                lst_color.Add(col);
                prm_vals_color_pair fltr_elem_col = new prm_vals_color_pair();
                fltr_elem_col.prm_f_elem_set = lst_fltr_elem[i];
                fltr_elem_col.rvt_col = col;
                fltr_elem_col.win_col = w_col;
                fltr_elem_col.fill_pattrn = cmbx_fillpattern.SelectedValue as FillPatternElement;

                string[] val_colr_info = { lst_fltr_elem[i].set_nm, color };
                System.Windows.Forms.ListViewItem itm_val_colr = new System.Windows.Forms.ListViewItem(val_colr_info);
                lst_itm_pvc_pair.Add(fltr_elem_col);
                lstv_val_color.Items.Add(itm_val_colr);

                DataGridViewCellStyle cel_sty = new DataGridViewCellStyle();
                cel_sty.BackColor = System.Drawing.Color.FromArgb(itm_rd, itm_grn, itm_blu);

                DataGridViewRow dt_grd_row0 = dt_grd_row_col[0] as DataGridViewRow;
                DataGridViewRow dt_grd_row = (DataGridViewRow)dt_grd_row0.Clone();
                DataGridViewCellCollection dt_v0_cel_col = dt_grd_row.Cells;
                DataGridViewCell cel0 = dt_v0_cel_col[0];
                cel0.Value = lst_fltr_elem[i].set_nm.ToString();
                DataGridViewCell cel2 = dt_v0_cel_col[1];
                cel2.Value = color;
                cel2.Style = cel_sty;

                DataGridViewComboBoxCell dt_v2_cel = dt_v0_cel_col[2] as DataGridViewComboBoxCell;
                /*
                List<cbm_box_itm> dt_v2_f_pt_itms = new List<cbm_box_itm>();
                foreach(FillPatternElement fp_e in al_f_pttrn_elems)
                {
                    cbm_box_itm dt_v2_itm = new cbm_box_itm();
                    dt_v2_itm.key = fp_e.Name;
                    dt_v2_itm.value = fp_e;
                    dt_v2_f_pt_itms.Add(dt_v2_itm);
                }
                */
                dt_v2_cel.DataSource = cmbx_fillpattern.DataSource;
                dt_v2_cel.DisplayMember = "key";
                dt_v2_cel.ValueMember = "value";
                
                dt_grdv_color.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dt_grdv_color_CellValueChanged);
                //dt_v2_cel.
                //dt_v2_cel.Displayed = true;
                dt_grd_row.Tag = fltr_elem_col; 
                
                /*
                for (int j = 0; j < dt_v2_cel.Items.Count; j++)
                {
                    cbm_box_itm itm = dt_v2_cel.Items[j] as cbm_box_itm;

                }
                */

                //dt_v2_cel.Selected = true;
                dt_grd_row_col.Add(dt_grd_row);
            }
                
            lst_pair_elem_color = lst_fltr_elem_color_set;

        }
        private void dt_grdv_color_CellValueChanged(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //DataGridViewComboBoxCell combo = (DataGridViewComboBoxCell)dt_grdv_color.Rows[e.RowIndex].Cells[2];
            if(dt_grdv_color.CurrentCell != null &&  dt_grdv_color.CurrentCell.ColumnIndex == 2)
            {
                System.Windows.Forms.ComboBox combo = e.Control as System.Windows.Forms.ComboBox;
                if (combo != null)
                {
                    // Remove an existing event-handler, if present, to avoid 
                    // adding multiple handlers when the editing control is reused.
                    combo.SelectedIndexChanged -=
                        new EventHandler(ComboBox_SelectedIndexChanged);

                    // Add the event handler. 
                    combo.SelectedIndexChanged +=
                        new EventHandler(ComboBox_SelectedIndexChanged);
                }
            }
           
            
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmbo_adrss = dt_grdv_color.CurrentCellAddress;
            DataGridViewCell dt_cell = dt_grdv_color.CurrentCell;
            if (dt_grdv_color.CurrentCell != null && dt_grdv_color.CurrentCell.ColumnIndex == 2)
            {
                DataGridViewComboBoxCell dt_cmb_cel = dt_cell as DataGridViewComboBoxCell;
                DataGridViewComboBoxEditingControl sndr = sender as DataGridViewComboBoxEditingControl;
                dt_cell.Value = sndr.EditingControlValueChanged;
            }
            
            //((System.Windows.Forms.ComboBox)sender). = (cbm_box_itm)((System.Windows.Forms.ComboBox)sender).SelectedItem;
        }
       
        private void btn_minus_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbx_legend_v_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbx_fillpattern_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbx_txt_type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lstv_views_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lstv_views.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        private void lstv_val_color_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lstv_saved_grp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbx_itm_prefx_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbx_grp_nm_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_add_grp_Click(object sender, EventArgs e)
        {
            List<prm_vals_color_pair> n_lst_pvc_pair = new List<prm_vals_color_pair>();
            foreach(DataGridViewRow rw in dt_grdv_color.Rows)
            {
               prm_vals_color_pair n_pair = rw.Tag as prm_vals_color_pair;
                n_lst_pvc_pair.Add(n_pair);

            }
            saved_pair_list svd_pair_lst = new saved_pair_list();
            svd_pair_lst.lst_pairs = n_lst_pvc_pair;
            List<string> lst_nms = new List<string>();
            foreach(set_cbx_btn lst in lst_set)
            {
                cbm_box_itm l_itm = lst.xn_cbx.SelectedItem as cbm_box_itm;
                string sub_nm = l_itm.key.ToString();
                lst_nms.Add(sub_nm);
            }
            string lst_prms_nms = String.Join("_",lst_nms);

            string n_nm = lstv_saved_grp.Items.Count.ToString() + "_" + tbx_grp_nm.Text + lst_prms_nms;
            DataGridViewRow x_rw = dt_grdv_color.Rows[1] as DataGridViewRow;
            prm_vals_color_pair x_col = x_rw.Tag as prm_vals_color_pair;
            string x_rvt_col = x_col.rvt_col.ToString();
            //string n_nm = lstv_saved_grp.Items.Count.ToString() + "_" + tbx_grp_nm.Text + "num:" + dt_grdv_color.RowCount.ToString() + "_" + x_rvt_col;

            svd_pair_lst.n_lst_nm = n_nm;
            string[] lst_v_itm_str = { lstv_saved_grp.Items.Count.ToString(), n_nm };
            System.Windows.Forms.ListViewItem lst_v_itm = new System.Windows.Forms.ListViewItem(lst_v_itm_str);
            lst_v_itm.Tag = svd_pair_lst;
            lstv_saved_grp.Items.Add(lst_v_itm);
            
        }

        private void dt_grdv_color_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dt_grdv_color_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dt_grdv_color.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dt_grdv_color.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dt_grdv_color.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            dt_grdv_color.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;

        }

        private void btn_crt_fltrs_Click(object sender, EventArgs e)
        {
            /*
            List <ParameterFilterElement> prm_fltr_elem_created = new List<ParameterFilterElement>();
            Transaction trns = new Transaction(Doc, "create_new_parameterfilterelement");
            trns.Start();
            if (trns.GetStatus() == TransactionStatus.Started)
            {
                /

                List<Autodesk.Revit.DB.Color> col_lst = new List<Autodesk.Revit.DB.Color>();
                foreach (prm_fltr_elem_set p_fltr_elem_set in lst_pair_to_create)
                {
                    string p_fltr_e_nm = p_fltr_elem_set.set_nm;
                    
                    byte[] bt_p_nm = Encoding.Default.GetBytes(p_fltr_e_nm);
                    var hx_string = BitConverter.ToString(bt_p_nm);
                    hx_string = hx_string.Replace('_', ' ');
                    string tmp_tst = 
                    
                    List<prm_val_pair> p_v_prs = p_fltr_elem_set.p_v_pairs;
                    List<ElementFilter> fltr_rls = new List<ElementFilter>();
                    foreach (prm_val_pair pv_pair in p_v_prs)
                    {
                        // here need implement other type of equalruls
                        FilterRule fltr_rl = ParameterFilterRuleFactory.CreateEqualsRule(new ElementId(pv_pair.prm_id), pv_pair.prm_val.ToString());
                        ElementFilter fltr_prm = new ElementParameterFilter(fltr_rl);
                        fltr_rls.Add(fltr_prm);

                    }
                    try
                    {
                        ElementLogicalFilter elem_fltr = new LogicalAndFilter(fltr_rls);
                        try
                        {
                            ParameterFilterElement fltr_e = ParameterFilterElement.Create(Doc, p_fltr_e_nm, sel_cats_id_col);
                            try
                            {
                                fltr_e.SetElementFilter(elem_fltr);
                                prm_fltr_elem_created.Add(fltr_e);

                            }
                            catch (Exception ex)
                            {

                            }

                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    catch (Exception ex)
                    {

                    }

                }
                trns.Commit();
            }
            */
            
            

        }

        private void btn_re_colr_Click(object sender, EventArgs e)
        {

        }
    }
}
