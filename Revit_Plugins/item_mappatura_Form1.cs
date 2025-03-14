using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.Runtime.Remoting.Contexts;
using System.Windows.Markup;
using Autodesk.Revit.DB.ExtensibleStorage;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public DataTable Populate(string str_connct, string sqlCommand)
        {
            SqlConnection northwindConnection = new SqlConnection(str_connct);
            northwindConnection.Open();

            SqlCommand command = new SqlCommand(sqlCommand, northwindConnection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            adapter.Fill(table);

            return table;
        }
        // temp set sql connection
        public DataGridViewRow new_row(DataGridView v, int ind)
        {
            try
            {
                DataGridViewRow rw = v.Rows[ind];
                return rw;
            }
            catch (Exception ex)
            {
                int ind_n = v.Rows.Add();
                DataGridViewRow rw = v.Rows[ind_n];
                return rw;
            }
        }

      

        public class set_cbx_btn
        {
            public System.Windows.Forms.Button xn_btn { get; set; }
            public System.Windows.Forms.Button xn_btn_min { get; set; }
            public System.Windows.Forms.ComboBox xn_cbx { get; set; }
        }
        public Element gt_elem0(string nm)
        {
            List<Element> elems = new FilteredElementCollector(Doc).OfClass(typeof(ParameterFilterElement)).ToElements().ToList();
            List<Element> out_e = new List<Element>();
            foreach (Element el in elems)
            {
                if (el.Name == nm)
                {
                    out_e.Add(el);
                }
            }
            if (out_e.Count == 0) { return null; }
            else
            {
                return out_e.FirstOrDefault();
            }

        }

        public ParameterFilterElement gt_prm_fltr_elem(string nm, List<prm_val_pair> x_lst_prs, ICollection<ElementId> ct_ids)
        {
            List<Element> elems = new FilteredElementCollector(Doc).OfClass(typeof(ParameterFilterElement)).ToElements().ToList();
            List<Element> out_e = new List<Element>();
            foreach (Element el in elems)
            {
                if (el.Name == nm)
                {
                    out_e.Add(el);
                }
            }
            if (out_e.Count == 0) 
            {
                List<ElementFilter> x_fltr_rls = new List<ElementFilter>();
                foreach (prm_val_pair pv_pair in x_lst_prs)
                {
                    // here need implement other type of equalruls
                    FilterRule fltr_rl = ParameterFilterRuleFactory.CreateEqualsRule(new ElementId(pv_pair.prm_id), pv_pair.prm_val.ToString());
                    ElementFilter fltr_prm = new ElementParameterFilter(fltr_rl);
                    x_fltr_rls.Add(fltr_prm);

                }
                try
                {
                    ElementLogicalFilter x_elem_fltr = new LogicalAndFilter(x_fltr_rls);

                    try
                    {
                        ParameterFilterElement n_fltr_e = ParameterFilterElement.Create(Doc, nm, ct_ids); 
                        try
                        {
                            n_fltr_e.SetElementFilter(x_elem_fltr);
                            return n_fltr_e;
                        }
                        catch(Exception ex)
                        {
                            return n_fltr_e ;
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;

                    }
                }
                catch(Exception ex)
                {
                    return null ;
                }
                         
            }
            else
            {
                ParameterFilterElement out_ea = out_e.FirstOrDefault()as ParameterFilterElement ;
                return out_ea;
            }

        }

        // cbm_box_itm cbm_box_itm_b choose one to use
        public class cbm_box_itm
        {
            public string key { get; set; }
            public object value { get; set; }
        }
        public class cbm_box_itm_b
        {
            public string key { get; set; }
            public int value { get; set; }
        }
        // cbm_box_itm cbm_box_itm_b choose one to use
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
        public string sett_file_path { get; set; }
        
        // defined class(later use directly the json de-serialize object or make an function to transform from low-code to class)
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
        // defined class(later use directly the json de-serialize object or make an function to transform from low-code to class)

        //json de-serialize object(for sextensible storage)
        public class json_prm_val_pair
        {
            public int num { get; set; }
            public int prm_id { get; set; }
            public object prm_val { get; set; }
        }
        public class json_prm_fltr_elem_set
        {
            public string set_nm { get; set; }
            public List<json_prm_val_pair> lst_prm_val_pair { get; set; }
        }
        
        public class json_color
        {
            public byte R { get; set; }
            public byte G { get; set; }
            public byte B { get; set; }
        }

        public class json_prm_vals_color_pair
        {
            public json_prm_fltr_elem_set prm_fltr_elem_set { get; set; }
            public List<json_prm_val_pair> lst_prm_val_pair { get; set; }
            public json_color color { get; set; }
            public int FillPatternElement { get; set; }
        }

        public class json_saved_list
        {
            public string saved_name { get; set; }
            public string doc_path { get; set; }
            public List<json_prm_vals_color_pair> lst_prm_vals_color_pair { get; set; }
        }

        public class json_all_saved_list
        {
            public List<json_saved_list> saved_list { get; set; }
        }
        public class json_itm_pair
        {
            public int guid { get; set; }
            public json_all_saved_list json_lst { get; set; }
            public saved_pair_list saved_pair_lst { get; set; }
        }
        //json de-serialize object

        public List<saved_pair_list> lst_sved_pair_lst { get; set; }    
        public List<prm_fltr_elem_set> lst_pair_to_create { get; set; }
        public List<prm_vals_color_pair> lst_pair_col_to_create { get; set; }
        public List<fltr_elem_color> lst_pair_elem_color { get; set; }
       
        public ICollection<ElementId> sel_cats_id_col { get; set; }
        public DataTable sql_data_tbl { get; set; }
        public DataGridViewCell _active_cell;
        
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

        //public schema
        public Schema Saved_grp_schema { get; set; }
        public Schema Saved_itm_schema { get;set;}
        public Schema Pair_schema { get; set;}
        public Schema Prm_val_schema { get; set; }
        public Schema Colr_schema { get; set; }
        public Entity Saved_grp_ent {  get; set; }  
        //public schema

        //transform entity to internal defintionitem
        public  saved_pair_list transform_entity(Entity a_ent, DataGridView dt_grd_v)
        {
            string sved_itm_nm = a_ent.Get<string>("SavedItemName");
            IList<Entity> sved_prm_set_lst_ent = a_ent.Get<IList<Entity>>("SavedParamSet");
            List<prm_vals_color_pair> a_prm_vals_colr_pair_lst = new List<prm_vals_color_pair>();
            for (int i = 0; i < sved_prm_set_lst_ent.Count(); i++)
            {
                Entity sved_prm_set_ent = sved_prm_set_lst_ent[i];
                string prm_set_nm_str = sved_prm_set_ent.Get<string>("ParamSetName");
                Entity colorset_ent = sved_prm_set_ent.Get<Entity>("Color");
                byte r_int = colorset_ent.Get<byte>("R");
                byte g_int = colorset_ent.Get<byte>("G");
                byte b_int = colorset_ent.Get<byte>("B");
                System.Drawing.Color n_colr = System.Drawing.Color.FromArgb(r_int, g_int, b_int);
                int fillptnid_int = sved_prm_set_ent.Get<int>("FillPattern");
                IList<Entity> prm_val_pairs_ent_lst = sved_prm_set_ent.Get<IList<Entity>>("PrmValPairs");
                List<prm_val_pair> lst_a_prm_val_pair = new List<prm_val_pair>();
                foreach (Entity prm_val_pairs_ent in prm_val_pairs_ent_lst)
                {
                    int prm_id_int = prm_val_pairs_ent.Get<int>("ParamId");
                    string prm_val = prm_val_pairs_ent.Get<string>("ParamVal");

                    prm_val_pair a_prm_val_pair = new prm_val_pair();
                    a_prm_val_pair.prm_id = prm_id_int;
                    a_prm_val_pair.prm_val = prm_val;
                    lst_a_prm_val_pair.Add(a_prm_val_pair);

                }
                /*
                int r_ind = dt_grdv_color.Rows.Add();
                DataGridViewRow row = dt_grd_v.Rows[r_ind];
                DataGridViewCell cel_nm_0 = row.Cells[0];
                cel_nm_0.Value = prm_set_nm_str;

                //
                DataGridViewCell cel_colr_1 = row.Cells[1];
                cel_colr_1.Value = "RGB(" + r_int.ToString() + "," + g_int.ToString() + "," + b_int.ToString() + "," + ")";
                cel_colr_1.Style.BackColor = n_colr;
                cel_colr_1.Style.SelectionBackColor = n_colr;
                cel_colr_1.Style.SelectionForeColor = n_colr;

                DataGridViewComboBoxCell cel_fil_pat_2 = row.Cells[2] as DataGridViewComboBoxCell;
                cel_fil_pat_2.Value = fillptnid_int;
                */
                //create the selfdefined items:
                prm_fltr_elem_set a_prm_fltr_elem_set = new prm_fltr_elem_set();
                a_prm_fltr_elem_set.set_nm = prm_set_nm_str;
                a_prm_fltr_elem_set.p_v_pairs = lst_a_prm_val_pair;

                FillPatternElement fill_ptn_elem = (FillPatternElement)Doc.GetElement(new ElementId(fillptnid_int));
                prm_vals_color_pair a_prm_vals_color_pair = new prm_vals_color_pair();
                a_prm_vals_color_pair.prm_f_elem_set = a_prm_fltr_elem_set;
                a_prm_vals_color_pair.rvt_col = new Autodesk.Revit.DB.Color(r_int, g_int, b_int);
                a_prm_vals_color_pair.win_col = System.Drawing.Color.FromArgb(r_int, g_int, b_int);
                a_prm_vals_color_pair.fill_pattrn = fill_ptn_elem;
                a_prm_vals_colr_pair_lst.Add(a_prm_vals_color_pair);
                

            }
            saved_pair_list a_saved_pair_list = new saved_pair_list();
            a_saved_pair_list.lst_pairs = a_prm_vals_colr_pair_lst;
            a_saved_pair_list.n_lst_nm = sved_itm_nm;

            return a_saved_pair_list;   

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

        private void dt_grd_color_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                {
                    if (e.ColumnIndex == 1)
                    {
                        _active_cell = dt_grdv_color.Rows[e.RowIndex].Cells[1];
                        ColorDialog n_color_dia = new ColorDialog();
                        n_color_dia.FullOpen = true;
                        n_color_dia.ShowHelp = true;
                        n_color_dia.Color = dt_grdv_color.Rows[e.RowIndex].Cells[2].Style.BackColor;

                        //n_color_dia.ShowDialog();
                        if (n_color_dia.ShowDialog() == DialogResult.OK)
                        {
                            dt_grdv_color.Rows[e.RowIndex].Cells[1].Style.BackColor = n_color_dia.Color;
                            dt_grdv_color.Rows[e.RowIndex].Cells[1].Style.SelectionBackColor = n_color_dia.Color;
                            string n_color = "RGB(" + n_color_dia.Color.R.ToString() + "," + n_color_dia.Color.G.ToString() + "," + n_color_dia.Color.B.ToString() + ")";
                            dt_grdv_color.Rows[e.RowIndex].Cells[1].Value = n_color;
                        }
                       

                    }
                }
            }
        }

        private void dt_grdv_color_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dt_grdv_color.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
                {
                    object selectedValue = dt_grdv_color.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    //MessageBox.Show($"Selected Value: {selectedValue}");
                }
            }
        }

        private void item_mappatura_Form1_Load(object sender, EventArgs e)
        {
            // Get the Main Schema
            IList<Schema> lst_schema = Schema.ListSchemas();
            List<Schema> exist_schm = new List<Schema>();
            foreach (Schema schm in lst_schema)
            {
                if (schm.SchemaName == "SavedGroup")
                {
                    Entity ent = Doc.ProjectInformation.GetEntity(schm);
                    if (ent.IsValid())
                    {
                        exist_schm.Add(schm);
                    }
                }

            }

            //Create new Schema
            if (exist_schm.Count > 0)
            {
                Saved_grp_schema = exist_schm.FirstOrDefault();
                Entity saved_grp_ent = Doc.ProjectInformation.GetEntity(Saved_grp_schema);
                
                if (saved_grp_ent != null)
                {
                    Entity a_saved_itm_ent = saved_grp_ent.Get<IList<Entity>> ("SavedItemList").FirstOrDefault();
                    //Schema a_savedgroup = a_saved_itm_ent.Schema;
                    //Field saved_item_fld = a_savedgroup.GetField("SavedItem");
                    Schema a_saveditem = a_saved_itm_ent.Schema;
                    Saved_itm_schema = a_saveditem;

                    Field a_savedparamset_fld = a_saveditem.GetField("SavedParamSet");
                    Schema a_savedparam = a_savedparamset_fld.SubSchema;
                    Pair_schema = a_savedparam;

                    Field a_colr_fld = a_savedparam.GetField("Color");
                    Schema a_colr = a_colr_fld.SubSchema;
                    Colr_schema = a_colr;

                    Field a_prmvalpairs_fld = a_savedparam.GetField("PrmValPairs");
                    Schema a_prmvalpairs = a_prmvalpairs_fld.SubSchema;
                    Prm_val_schema = a_prmvalpairs;


                }
                
                
                Saved_grp_ent = saved_grp_ent;
                
            }
            else
            {
                //Create param value schema:
                Guid sub_col_guid = Guid.NewGuid();
                SchemaBuilder colr_schema_bildr = new SchemaBuilder(sub_col_guid);
                colr_schema_bildr.SetSchemaName("SetColor");
                colr_schema_bildr.SetReadAccessLevel(AccessLevel.Public);
                colr_schema_bildr.SetWriteAccessLevel(AccessLevel.Public);
                colr_schema_bildr.AddSimpleField("R", typeof(byte));
                colr_schema_bildr.AddSimpleField("G", typeof(byte));
                colr_schema_bildr.AddSimpleField("B", typeof(byte));
                Autodesk.Revit.DB.ExtensibleStorage.Schema colr_schema = colr_schema_bildr.Finish();
                Autodesk.Revit.DB.ExtensibleStorage.Entity colr_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(colr_schema);
                Colr_schema = colr_schema;

                //Create Color schema:
                Guid sub_prm_val_guid = Guid.NewGuid();
                SchemaBuilder prm_val_schema_bildr = new SchemaBuilder(sub_prm_val_guid);
                prm_val_schema_bildr.SetSchemaName("ParamValPair");
                prm_val_schema_bildr.SetReadAccessLevel(AccessLevel.Public);
                prm_val_schema_bildr.SetWriteAccessLevel(AccessLevel.Public);
                prm_val_schema_bildr.AddSimpleField("ParamId", typeof(int));
                prm_val_schema_bildr.AddSimpleField("ParamVal", typeof(string));
                Autodesk.Revit.DB.ExtensibleStorage.Schema prm_val_schema = prm_val_schema_bildr.Finish();
                Autodesk.Revit.DB.ExtensibleStorage.Entity prm_val_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(prm_val_schema);
                Prm_val_schema = prm_val_schema;

                //Create pair schema:
                Guid main_guid = Guid.NewGuid();
                SchemaBuilder pair_schema_bildr = new SchemaBuilder(main_guid);
                pair_schema_bildr.SetSchemaName("ParamSet");
                pair_schema_bildr.SetReadAccessLevel(AccessLevel.Public);
                pair_schema_bildr.SetWriteAccessLevel(AccessLevel.Public);
                pair_schema_bildr.AddSimpleField("ParamSetName", typeof(string));
                pair_schema_bildr.AddSimpleField("FillPattern", typeof(int));
                FieldBuilder sub_colr_fbildr = pair_schema_bildr.AddSimpleField("Color", typeof(Autodesk.Revit.DB.ExtensibleStorage.Entity));
                FieldBuilder sub_prm_val_fbildr = pair_schema_bildr.AddArrayField("PrmValPairs", typeof(Autodesk.Revit.DB.ExtensibleStorage.Entity));
                //FieldBuilder sub_colr_fbildr = pair_schema_bildr.AddArrayField("Color", typeof(Autodesk.Revit.DB.ExtensibleStorage.Entity));
                sub_colr_fbildr.SetSubSchemaGUID(sub_col_guid);
                sub_prm_val_fbildr.SetSubSchemaGUID(sub_prm_val_guid);
                Autodesk.Revit.DB.ExtensibleStorage.Schema pair_schema = pair_schema_bildr.Finish();
                IList<Entity> prm_val_lst = new List<Entity>();
                prm_val_lst.Add(prm_val_ent);
                Autodesk.Revit.DB.ExtensibleStorage.Entity pair_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(pair_schema);
                pair_ent.Set<IList<Entity>>("PrmValPairs",prm_val_lst);
                pair_ent.Set<Entity>("Color",colr_ent);
                Pair_schema = pair_schema;

                //Greate saved item schema:
                Guid saved_itm_guid = Guid.NewGuid();
                SchemaBuilder saved_itm_bildr = new SchemaBuilder(saved_itm_guid);
                saved_itm_bildr.SetSchemaName("SavedItem");
                saved_itm_bildr.SetReadAccessLevel(AccessLevel.Public);
                saved_itm_bildr.SetWriteAccessLevel(AccessLevel.Public);
                saved_itm_bildr.AddSimpleField("SavedItemName", typeof(string));
                FieldBuilder saved_itm_fbildr = saved_itm_bildr.AddArrayField("SavedParamSet", typeof(Autodesk.Revit.DB.ExtensibleStorage.Entity));
                saved_itm_fbildr.SetSubSchemaGUID(main_guid);
                Autodesk.Revit.DB.ExtensibleStorage.Schema saved_itm_schema = saved_itm_bildr.Finish();
                IList<Entity> saved_prm_set_lst = new List<Entity>();
                saved_prm_set_lst.Add(pair_ent);
                Autodesk.Revit.DB.ExtensibleStorage.Entity saved_itm_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(saved_itm_schema);
                saved_itm_ent.Set<IList<Entity>>("SavedParamSet", saved_prm_set_lst);
                Saved_itm_schema = saved_itm_schema;

                //Greate saved group schema:
                Guid saved_grp_guid = Guid.NewGuid();
                SchemaBuilder saved_grp_bildr = new SchemaBuilder(saved_grp_guid);
                saved_grp_bildr.SetSchemaName("SavedGroup");
                saved_grp_bildr.SetReadAccessLevel(AccessLevel.Public);
                saved_grp_bildr.SetWriteAccessLevel(AccessLevel.Public);
                //saved_itm_bildr.AddSimpleField("SavedGroupName", typeof(string));
                FieldBuilder saved_grp_fbildr = saved_grp_bildr.AddArrayField("SavedItemList", typeof(Autodesk.Revit.DB.ExtensibleStorage.Entity));
                saved_grp_fbildr.SetSubSchemaGUID(saved_itm_guid);
                Autodesk.Revit.DB.ExtensibleStorage.Schema saved_grp_schema = saved_grp_bildr.Finish();
                IList<Entity> saved_itm_lst = new List<Entity>();
                saved_itm_lst.Add(saved_itm_ent);
                Autodesk.Revit.DB.ExtensibleStorage.Entity saved_grp_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(saved_grp_schema);
                saved_grp_ent.Set<IList<Entity>>("SavedItemList",saved_itm_lst);

                Saved_grp_schema = saved_grp_schema;
                Saved_grp_ent = saved_grp_ent;
                using (Transaction trns_schema = new Transaction(Doc, "Add new schema group"))
                {
                    trns_schema.Start();
                    Doc.ProjectInformation.SetEntity(Saved_grp_ent);
                    trns_schema.Commit();
                }

            }
            //load the schema entities:
            Entity saved_Grp_ent = Doc.ProjectInformation.GetEntity(Saved_grp_schema);
            IList<Entity> saved_Itms_lst = saved_Grp_ent.Get<IList<Entity>>("SavedItemList");
            foreach (Entity svd_itm_ent in saved_Itms_lst)
            {
                try
                {
                    string svd_itm_nm = svd_itm_ent.Get<string>("SavedItemName");
                    IList<Entity> svd_prm_set_lst = svd_itm_ent.Get<IList<Entity>>("SavedParamSet");
                    string[] str_svd_itm_ent = { svd_itm_nm, svd_prm_set_lst.Count().ToString() };
                    System.Windows.Forms.ListViewItem lstv_itm_svd_itm_ent = new System.Windows.Forms.ListViewItem(str_svd_itm_ent);
                    lstv_itm_svd_itm_ent.Tag = svd_itm_ent;
                    lstv_saved_grp_2.Items.Add(lstv_itm_svd_itm_ent);


                }
                catch (Exception ex)
                {

                }


            }

            //load all opened documents
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

            //load all views and legend views
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
                if (v1.ViewTemplateId == new ElementId(-1))
                {
                    lstv_views.Items.Add(lst_v_view);
                }
                
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
            //load all category
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
            
            //load all fill pattern
            List<FillPatternElement> al_pttrns = new List<FillPatternElement>();
            foreach(Element fill_pttrn_tp_elem in al_fill_pattrn_tps)
            {
                FillPatternElement fill_pttn_tp = fill_pttrn_tp_elem as FillPatternElement;
                FillPattern fil_ptn = fill_pttn_tp.GetFillPattern();
                if(fil_ptn.Target == FillPatternTarget.Drafting)
                {
                    al_pttrns.Add(fill_pttn_tp);
                    FillPattern fill_pttn = fill_pttn_tp.GetFillPattern();
                    string f_pttn_nm = fill_pttn.Name;
                    cbm_box_itm f_pttn_tp_itm = new cbm_box_itm();
                    f_pttn_tp_itm.key = f_pttn_nm;
                    f_pttn_tp_itm.value = fill_pttn_tp as object;
                    lst_fill_pttrn_itms.Add(f_pttn_tp_itm);

                }
               
            }
            al_f_pttrn_elems = al_pttrns;
            cmbx_fillpattern.DataSource = lst_fill_pttrn_itms;
            cmbx_fillpattern.DisplayMember = "key";
            cmbx_fillpattern.ValueMember = "value";

            //setup the datagridview
            dt_grdv_color.CellValueChanged += new DataGridViewCellEventHandler(dt_grdv_color_CellValueChanged);
            dt_grdv_color.CellClick += new DataGridViewCellEventHandler(dt_grd_color_CellClick);

            List<cbm_box_itm_b> fil_ptn_cell = new List<cbm_box_itm_b>();
            foreach (FillPatternElement fil_ptn in al_f_pttrn_elems)
            {
                cbm_box_itm_b fil_ptn_itm = new cbm_box_itm_b();
                fil_ptn_itm.key = fil_ptn.Name;
                fil_ptn_itm.value = fil_ptn.Id.IntegerValue;
                fil_ptn_cell.Add(fil_ptn_itm);

            }

            DataGridViewComboBoxColumn dt_col_2 = dt_grdv_color.Columns[2] as DataGridViewComboBoxColumn;
            dt_col_2.DataSource = fil_ptn_cell;
            dt_col_2.DisplayMember = "key";
            dt_col_2.ValueMember = "value";
        

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
            
            // json save methods
            string app_data = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fl_pth = app_data + "/Autodesk/Revit/Addins/settings.json";
            object[] saved_list = new object[0];
            var json_str_origin = new Dictionary<string, object>
            {
                {"saved_list", saved_list }
            };

            string json_str_save_origin = System.Text.Json.JsonSerializer.Serialize(json_str_origin);
            if (!File.Exists(fl_pth))
            {
                FileStream fl_Stream = File.Create(fl_pth);
                fl_pth = fl_Stream.Name;
                File.WriteAllText(fl_pth,json_str_save_origin);

            }
            else
            {
                string json_file_read = File.ReadAllText(fl_pth);
                if(json_file_read != "")
                {
                    JsonSerializerOptions json_opt = new JsonSerializerOptions();
                    dynamic json_file = Newtonsoft.Json.JsonConvert.DeserializeObject(json_file_read);
                    Newtonsoft.Json.Linq.JArray sved_list = json_file["saved_list"];
                    label5.Text = sved_list.Count().ToString();
                    if (sved_list != null && sved_list.Count > 0)
                    {
                        for (int j = 0; j < sved_list.Count(); j++)
                        {
                            dynamic sved_lst = sved_list[j];
                            string doc_nm = sved_lst["doc_path"];
                            if (Doc.PathName == doc_nm)
                            {
                                Newtonsoft.Json.Linq.JArray lst_opn_sva_color_pair = sved_lst["lst_prm_vals_color_pair"];

                                List<prm_vals_color_pair> lst_opn_p_v_col_pr_a = new List<prm_vals_color_pair>();
                                for (int i = 0; i < lst_opn_sva_color_pair.Count(); i++)
                                {

                                    dynamic lst_opn_p_v_c_pair = lst_opn_sva_color_pair[i];
                                    try
                                    {
                                        dynamic lst_opn_p_f_e_set = lst_opn_p_v_c_pair["prm_fltr_elem_set"];
                                        string l_o_set_nm = lst_opn_p_f_e_set["set_nm"];

                                        List<prm_val_pair> lst_opn_p_v_lst = new List<prm_val_pair>();
                                        //object[] l_o_prm_val = lst_opn_p_f_e_set["lst_prm_val_pair"];
                                        Newtonsoft.Json.Linq.JArray l_o_prm_val = lst_opn_p_f_e_set["lst_prm_val_pair"];

                                        for (int k = 0; k < l_o_prm_val.Count(); k++)
                                        {
                                            dynamic l_o_p_val = l_o_prm_val[k];
                                            int l_o_prm_id = l_o_p_val["prm_id"];
                                            object l_o_val = l_o_p_val["prm_val"];
                                            prm_val_pair l_o_prm_val_p = new prm_val_pair();
                                            l_o_prm_val_p.prm_id = l_o_prm_id;
                                            l_o_prm_val_p.prm_val = l_o_val;
                                            lst_opn_p_v_lst.Add(l_o_prm_val_p);
                                        }
                                        prm_fltr_elem_set lst_opn_p_f_elem_set = new prm_fltr_elem_set();
                                        lst_opn_p_f_elem_set.set_nm = l_o_set_nm;
                                        lst_opn_p_f_elem_set.p_v_pairs = lst_opn_p_v_lst;

                                        dynamic lst_opn_colr_val = lst_opn_p_v_c_pair["color"];

                                        Autodesk.Revit.DB.Color l_o_rvt_col = new Autodesk.Revit.DB.Color((byte)lst_opn_colr_val["R"], (byte)lst_opn_colr_val["G"], (byte)lst_opn_colr_val["B"]);

                                        System.Drawing.Color l_o_win_col = System.Drawing.Color.FromArgb(Convert.ToInt32(lst_opn_colr_val["R"]), Convert.ToInt32(lst_opn_colr_val["G"]), Convert.ToInt32(lst_opn_colr_val["B"]));
                                        int fill_p_elem_id_int = lst_opn_p_v_c_pair["FillPatternElement"];
                                        ElementId fill_p_elem_id = new ElementId(fill_p_elem_id_int);

                                        Element fill_p_e = Doc.GetElement(fill_p_elem_id);
                                        FillPatternElement fill_p_elem = fill_p_e as FillPatternElement;

                                        prm_vals_color_pair l_o_pvcol_pr = new prm_vals_color_pair();
                                        l_o_pvcol_pr.prm_f_elem_set = lst_opn_p_f_elem_set;
                                        l_o_pvcol_pr.rvt_col = l_o_rvt_col;
                                        l_o_pvcol_pr.win_col = l_o_win_col;
                                        l_o_pvcol_pr.fill_pattrn = fill_p_elem;

                                        lst_opn_p_v_col_pr_a.Add(l_o_pvcol_pr);

                                    }
                                    catch (Exception ex)
                                    {

                                    }


                                }
                                saved_pair_list lst_opn_sved_lst_a = new saved_pair_list();

                                string lst_opn_svd_nm = sved_lst["saved_name"];


                                lst_opn_sved_lst_a.lst_pairs = lst_opn_p_v_col_pr_a;
                                lst_opn_sved_lst_a.n_lst_nm = lst_opn_svd_nm;

                                //label5.Text = sved_lst;

                                string[] lst_v_itm_str_l_o = { lstv_saved_grp.Items.Count.ToString(), lst_opn_svd_nm };
                                System.Windows.Forms.ListViewItem lst_v_itm_l_o = new System.Windows.Forms.ListViewItem(lst_v_itm_str_l_o);
                                lst_v_itm_l_o.Tag = lst_opn_sved_lst_a;

                                lstv_saved_grp.Items.Add(lst_v_itm_l_o);


                            }


                        }

                    }

                }
                bool is_empt = json_file_read == "";
                label5.Text = json_file_read + "&" + is_empt.ToString() ;
                

            }
            sett_file_path = fl_pth;
            // json save methods


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

            //saved_pair_list finl_saved_pair0 = lstv_saved_grp.CheckedItems[0].Tag as saved_pair_list;
            Entity checked_itm_ent = lstv_saved_grp_2.CheckedItems[0].Tag as Entity;
            saved_pair_list finl_saved_pair = transform_entity(checked_itm_ent, dt_grdv_color);

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

                        ParameterFilterElement fltr_e = gt_prm_fltr_elem(p_fltr_e_nm, p_v_prs, sel_cats_id_col);
                        prm_fltr_elem_created.Add(fltr_e);
                        fltr_elem_color finl_itm_pair = new fltr_elem_color();
                        finl_itm_pair.p_fltr_elem = fltr_e;
                        finl_itm_pair.p_colr = p_fltr_col;
                        finl_itm_pair.fill_pattrn = fl_ptn;
                        finl_itm_pair.p_nm = p_fltr_e_nm;
                        finl_pf_elem_pair.Add(finl_itm_pair);
                       
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
                        try
                        {
                            FilledRegionType n_f_tp = f_tp0.Duplicate(fltr_nm) as FilledRegionType;
                            n_f_tp.ForegroundPatternId = sel_pttrn.Id;
                            n_f_tp.ForegroundPatternColor = fltr_col;
                            lst_f_regns_tps.Add(n_f_tp);

                        }
                        catch(Exception ex)
                        {

                        }
                        

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
            
            
            FillPatternElement sel_patrn = cmbx_fillpattern.SelectedValue as FillPatternElement;
            int deflt_val = sel_patrn.Id.IntegerValue;
            dt_grdv_color.Rows.Clear();
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
                
                
                string[] val_colr_info = { lst_fltr_elem[i].set_nm, color };
                System.Windows.Forms.ListViewItem itm_val_colr = new System.Windows.Forms.ListViewItem(val_colr_info);
                lst_itm_pvc_pair.Add(fltr_elem_col);
                lstv_val_color.Items.Add(itm_val_colr);

                DataGridViewCellStyle cel_sty = new DataGridViewCellStyle();
                cel_sty.BackColor = System.Drawing.Color.FromArgb(itm_rd, itm_grn, itm_blu);
                cel_sty.SelectionBackColor = System.Drawing.Color.FromArgb(itm_rd, itm_grn, itm_blu);

                DataGridViewRow dt_grd_row = new_row(dt_grdv_color, i);
                
                DataGridViewCellCollection dt_v0_cel_col = dt_grd_row.Cells;
                DataGridViewCell cel0 = dt_v0_cel_col[0];
                cel0.Value = lst_fltr_elem[i].set_nm.ToString();
                DataGridViewCell cel2 = dt_v0_cel_col[1];
                cel2.Value = color;
                cel2.Style = cel_sty;


                DataGridViewComboBoxCell dt_v2_cel = dt_v0_cel_col[2] as DataGridViewComboBoxCell;

                dt_v2_cel.Value = deflt_val;
                //string default_itm = fil_ptn_cell[0].key;
               
                fltr_elem_col.fill_pattrn = cmbx_fillpattern.SelectedValue as FillPatternElement;

                System.Drawing.Color sel_colr = cel2.Style.BackColor;

                fltr_elem_col.rvt_col = new Autodesk.Revit.DB.Color(sel_colr.R,sel_colr.G,sel_colr.B);
                fltr_elem_col.win_col = sel_colr;
                
                dt_grd_row.Tag = fltr_elem_col; 
                
               
            }
                
            lst_pair_elem_color = lst_fltr_elem_color_set;

        }
        //datagridcomboboxcell event handler
        /*
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

        private void dt_grdv_color_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dt_grdv_color.CurrentCell is DataGridViewComboBoxCell)
            {
                System.Windows.Forms.ComboBox comboBox = e.Control as System.Windows.Forms.ComboBox;
                if (comboBox != null)
                {
                    // Remove previous event handler to avoid multiple subscriptions
                    comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
                    // Add the event handler
                    comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
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
            

        }
        */
       
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
            System.Windows.Forms.ListViewItem sel_grp = lstv_saved_grp.SelectedItems.Cast<System.Windows.Forms.ListViewItem>().FirstOrDefault();
            try
            {
                saved_pair_list chcked_pair = sel_grp.Tag as saved_pair_list;
                string chcked_pair_nm = chcked_pair.n_lst_nm;
                List<prm_vals_color_pair> chcked_pair_prm_lst = chcked_pair.lst_pairs;

                dt_grdv_color.Rows.Clear();
                DataGridViewRowCollection chcked_pair_itms = dt_grdv_color.Rows;
                for (int i = 0; i < chcked_pair_prm_lst.Count; i++)
                {
                    prm_vals_color_pair chcked_color_pair = chcked_pair_prm_lst[i];

                    if (chcked_color_pair != null)
                    {
                        prm_fltr_elem_set chcked_prm_fltr_elem = chcked_color_pair.prm_f_elem_set;
                        string set_nm = chcked_prm_fltr_elem.set_nm;
                        System.Drawing.Color set_color = chcked_color_pair.win_col;
                        byte s_itm_rd = (byte)set_color.R;
                        byte s_itm_grn = (byte)set_color.G;
                        byte s_itm_blu = (byte)set_color.B;
                        string clr_nm = "RGB(" + s_itm_rd.ToString() + "," + s_itm_grn.ToString() + "," + s_itm_blu.ToString() + ")";
                        FillPatternElement set_pat = chcked_color_pair.fill_pattrn;

                        DataGridViewRow set_dt_grd_r0 = chcked_pair_itms[0] as DataGridViewRow;
                        DataGridViewRow set_dt_grd_r = (DataGridViewRow)set_dt_grd_r0.Clone();
                        DataGridViewCellCollection set_cel_col = set_dt_grd_r.Cells;
                        DataGridViewCell set_cel0 = set_cel_col[0];
                        set_cel0.Value = set_nm;
                        DataGridViewCell set_cel1 = set_cel_col[1];
                        set_cel1.Value = clr_nm;
                        DataGridViewCellStyle set_cel_sty = new DataGridViewCellStyle();
                        set_cel_sty.BackColor = set_color;
                        set_cel1.Style = set_cel_sty;

                        DataGridViewComboBoxCell dt_v2_cel2 = set_cel_col[2] as DataGridViewComboBoxCell;

                        dt_v2_cel2.DataSource = cmbx_fillpattern.DataSource;
                        dt_v2_cel2.DisplayMember = "key";
                        dt_v2_cel2.ValueMember = "value";

                        dt_v2_cel2.Value = set_pat;
                        set_dt_grd_r.Tag = chcked_color_pair;
                        
                        chcked_pair_itms.Add(set_dt_grd_r);

                    }

                }
                
            }
            catch(Exception ex)
            {

            }
            

        }

        private void tbx_itm_prefx_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbx_grp_nm_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_add_grp_Click(object sender, EventArgs e)
        {
            string doc_pth_nm = Doc.PathName.ToString();
            List<prm_vals_color_pair> n_lst_pvc_pair = new List<prm_vals_color_pair>();
            foreach (DataGridViewRow rw in dt_grdv_color.Rows)
            {
                try 
                {
                    DataGridViewCell rw_cel_1 = rw.Cells[1];
                    DataGridViewCell rw_cel_2 = rw.Cells[2];
                    prm_vals_color_pair n_pair = rw.Tag as prm_vals_color_pair;
                    System.Drawing.Color rw_col = rw_cel_1.Style.BackColor;
                    n_pair.win_col = rw_col;
                    n_pair.rvt_col = new Autodesk.Revit.DB.Color(rw_col.R, rw_col.G, rw_col.B);
                    try 
                    {
                        int fill_patn_id = Int32.Parse(rw_cel_2.Value.ToString());
                        try
                        {
                            Element fil_elem = Doc.GetElement(new ElementId(fill_patn_id));
                            FillPatternElement fil_ptn_elem = fil_elem as FillPatternElement;
                            n_pair.fill_pattrn = fil_ptn_elem;
                            n_lst_pvc_pair.Add(n_pair);

                        }
                        catch (Exception ex)
                        {
                            n_pair.fill_pattrn = cmbx_fillpattern.SelectedItem as FillPatternElement;
                            n_lst_pvc_pair.Add(n_pair);
                        }


                    }
                    catch(Exception ex1)
                    {
                        n_pair.fill_pattrn = cmbx_fillpattern.SelectedItem as FillPatternElement;
                        n_lst_pvc_pair.Add(n_pair);
                    }

                }
                catch(Exception ex)
                {

                }
                
            }

            saved_pair_list svd_pair_lst = new saved_pair_list();
            svd_pair_lst.lst_pairs = n_lst_pvc_pair;

            List<string> lst_nms = new List<string>();
            foreach (set_cbx_btn lst in lst_set)
            {
                cbm_box_itm l_itm = lst.xn_cbx.SelectedItem as cbm_box_itm;
                string sub_nm = l_itm.key.ToString();
                lst_nms.Add(sub_nm);
            }
            string lst_prms_nms = String.Join("_", lst_nms);
            
            string n_nm = lstv_saved_grp.Items.Count.ToString() + "_" + tbx_grp_nm.Text + lst_prms_nms;
            svd_pair_lst.n_lst_nm = n_nm;

            DataGridViewRow x_rw = dt_grdv_color.Rows[1] as DataGridViewRow;
            prm_vals_color_pair x_col = x_rw.Tag as prm_vals_color_pair;

            string[] lst_v_itm_str = { lstv_saved_grp.Items.Count.ToString(), n_nm };
            System.Windows.Forms.ListViewItem lst_v_itm = new System.Windows.Forms.ListViewItem(lst_v_itm_str);
            lst_v_itm.Tag = svd_pair_lst;

            lstv_saved_grp.Items.Add(lst_v_itm);

            List<saved_pair_list> saved_lst = new List<saved_pair_list>();
            if (lstv_saved_grp.Items.Count > 0)
            {
                foreach (System.Windows.Forms.ListViewItem lst_itm in lstv_saved_grp.Items)
                {
                    saved_lst.Add(lst_itm.Tag as saved_pair_list);

                }

            }
            saved_lst.Add(svd_pair_lst);

            string json_n_lst_nm =  svd_pair_lst.n_lst_nm;
            List<prm_vals_color_pair> json_lst_pair = svd_pair_lst.lst_pairs;

            //var json_list_prm_vals_color_pair = new Dictionary<string, object>();
            object[] json_list_prm_vals_color_pair = new object[json_lst_pair.Count()];
            for (int j = 0; j < json_lst_pair.Count(); j++)
            {
                prm_vals_color_pair js_prm_vals_color_pair = json_lst_pair[j];
                if(js_prm_vals_color_pair != null)
                {
                    //string x_rvt_col = js_prm_vals_color_pair.rvt_col.ToString();
                    byte col_r = js_prm_vals_color_pair.rvt_col.Red;
                    byte col_g = js_prm_vals_color_pair.rvt_col.Green;
                    byte col_b = js_prm_vals_color_pair.rvt_col.Blue;
                    FillPatternElement sved_pttrn = js_prm_vals_color_pair.fill_pattrn;
                    List<prm_val_pair> sved_pv_pair = js_prm_vals_color_pair.prm_f_elem_set.p_v_pairs;
                    string sved_set_name = js_prm_vals_color_pair.prm_f_elem_set.set_nm;

                    var json_str_prm_val = new Dictionary<string, object>();

                    object[] prm_val_l_pair = new object[sved_pv_pair.Count()];
                    // temp json dictionary
                    for (int i = 0; i < sved_pv_pair.Count(); i++)
                    {
                        prm_val_pair pair = sved_pv_pair[i];

                        var prm_info = new Dictionary<string, object>
                        {

                            {"prm_val_pair", i },
                            {"prm_id",pair.prm_id},
                            {"prm_val",pair.prm_val}
                        };
                        prm_val_l_pair[i] = prm_info;
                        //json_str_prm_val.Add(dic_nm, prm_info);

                    };

                    var json_str_p_f_elem_set = new Dictionary<string, object>
                    {
                        {"set_nm",sved_set_name},
                        {"lst_prm_val_pair",prm_val_l_pair}
                    };
                    var json_str_colr = new Dictionary<string, object>
                    {
                        {"R", col_r},
                        {"G", col_g},
                        {"B", col_b},
                    };
                    var json_str_col_pair = new Dictionary<string, object>
                    {
                        {"prm_fltr_elem_set",json_str_p_f_elem_set},
                        {"color", json_str_colr},
                        {"FillPatternElement", sved_pttrn.Id.IntegerValue }
                    };
                    json_list_prm_vals_color_pair[j] = json_str_col_pair;
                    // temp json dictionary
                }


            }
            // temp json dictionary
            var json_str = new Dictionary<string, object>
            {
                {"saved_name",  json_n_lst_nm},
                {"doc_path", doc_pth_nm},
                {"lst_prm_vals_color_pair", json_list_prm_vals_color_pair}

            };
            // temp json dictionary

            JsonSerializerOptions json_ser_opt = new JsonSerializerOptions();
            json_ser_opt.WriteIndented = true;  
            //json_ser_opt.IgnoreNullValues = true;   
            string json_str_str = System.Text.Json.JsonSerializer.Serialize(json_str);

            object[] saved_list = new object[1];
            saved_list[0] = json_str;
            var json_str_origin = new Dictionary<string, object>
            {
                {"saved_list", saved_list }
            };

            string json_file_read_0 = File.ReadAllText(sett_file_path);

            if(json_file_read_0 != "" && json_file_read_0 != null)
            {
                json_all_saved_list json_file_0 = Newtonsoft.Json.JsonConvert.DeserializeObject<json_all_saved_list>(json_file_read_0);
                List<json_saved_list> lst_j_saved_list = json_file_0.saved_list;

                json_saved_list j_saved_list = Newtonsoft.Json.JsonConvert.DeserializeObject<json_saved_list>(json_str_str);
                lst_j_saved_list.Add(j_saved_list);
                

                object[] saved_list_n = new object[1];
                saved_list_n[0] = json_str;
                var json_str_origin_n = new Dictionary<string, object>
                {
                    {"saved_list", lst_j_saved_list }
                };

                //string json_lst_j_saved_list_n = System.Text.Json.JsonSerializer.Serialize(json_str_origin_n);
                string json_lst_j_saved_list_n = Newtonsoft.Json.JsonConvert.SerializeObject(json_str_origin_n);
                File.WriteAllText(sett_file_path, json_lst_j_saved_list_n);

                label5.Text = json_file_0.ToString();
                
            }
            else
            {
                string json_str_save_3 = System.Text.Json.JsonSerializer.Serialize(json_str_origin);
                File.WriteAllText(sett_file_path, json_str_save_3);

            }
            
           

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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            using(Transaction trans_del = new Transaction(Doc,"Delete Group"))
            {
                trans_del.Start();
                IList<Entity> a_saved_itms_lst = Saved_grp_ent.Get<IList<Entity>>("SavedItemList");
                System.Windows.Forms.ListViewItem lstv_itm_chcked_grp = lstv_saved_grp_2.CheckedItems.Cast<System.Windows.Forms.ListViewItem>().FirstOrDefault();
                Entity sved_itm_ent = lstv_itm_chcked_grp.Tag as Entity;
                int l_ind = lstv_saved_grp_2.CheckedItems[0].Index;
                lstv_saved_grp_2.CheckedItems[0].Remove();
                a_saved_itms_lst.Remove(sved_itm_ent);
                Saved_grp_ent.Set<IList<Entity>>("SavedItemList", a_saved_itms_lst);
                Doc.ProjectInformation.SetEntity(Saved_grp_ent);
                trans_del.Commit();

            }
            

        }

        private void btn_add_grp_tst_Click(object sender, EventArgs e)
        {
            List<string> lst_nms = new List<string>();
            foreach (set_cbx_btn lst in lst_set)
            {
                cbm_box_itm l_itm = lst.xn_cbx.SelectedItem as cbm_box_itm;
                string sub_nm = l_itm.key.ToString();
                lst_nms.Add(sub_nm);
            }
            string lst_prms_nms = String.Join("_", lst_nms);

            string sved_itm_nm = lstv_saved_grp_2.Items.Count.ToString() + "_" + tbx_grp_nm.Text + lst_prms_nms;

            using (Transaction trans_add_entity = new Transaction(Doc, "Add new item list"))
            {
                trans_add_entity.Start();
                //Create saved item entity:
                IList<Entity> saved_itms_lst = Saved_grp_ent.Get<IList<Entity>>("SavedItemList");
                label6.Text = saved_itms_lst.Count.ToString();  

                //Autodesk.Revit.DB.ExtensibleStorage.Entity saved_itm_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(Saved_itm_schema);
                //saved_itm_ent.Set<string>("SavedItemName", "temp_tst(get_the_prm_from_set_name)");

                IList<Entity> saved_entis = new List<Entity>();
                for (int i = 0; i < dt_grdv_color.Rows.Count; i++)
                {
                    DataGridViewRow r = dt_grdv_color.Rows[i] as DataGridViewRow;

                    DataGridViewCell r_cel_1 = r.Cells[1];
                    DataGridViewCell r_cel_2 = r.Cells[2];
                    prm_vals_color_pair n_pair = r.Tag as prm_vals_color_pair;

                    System.Drawing.Color rw_col = r_cel_1.Style.BackColor;
                    Autodesk.Revit.DB.Color rvt_colr = new Autodesk.Revit.DB.Color(rw_col.R, rw_col.G, rw_col.B);
                    int fill_patrn_elem_id = (int)r_cel_2.Value;
                    prm_fltr_elem_set prm_set = n_pair.prm_f_elem_set;
                    string prm_set_nm = prm_set.set_nm;
                    List<prm_val_pair> prm_set_p_vals = prm_set.p_v_pairs;
                    //prm_val_pair tst_pair = prm_set_p_vals[0];

                    //Create param value entity:
                    IList<Entity> sub_prm_entis = new List<Entity>();
                    foreach (prm_val_pair p_val in prm_set_p_vals)
                    {
                        //Schema prm_val_schema = saved_itm_ent.Schema;
                        Autodesk.Revit.DB.ExtensibleStorage.Entity prm_val_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(Prm_val_schema);
                        Field prm_val_prmid_fld = Prm_val_schema.GetField("ParamId");
                        prm_val_ent.Set<int>("ParamId", p_val.prm_id);
                        Field prm_val_prmval_fld = Prm_val_schema.GetField("ParamVal");
                        string prm_val = "";
                        if(p_val.prm_val != null)
                        {
                            prm_val = p_val.prm_val.ToString();
                        }
                        prm_val_ent.Set<string>("ParamVal", prm_val);
                        sub_prm_entis.Add(prm_val_ent);

                    }
                    //Create color entity:
                    Autodesk.Revit.DB.ExtensibleStorage.Entity colr_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(Colr_schema);
                    colr_ent.Set<byte>("R", rvt_colr.Red);
                    colr_ent.Set<byte>("G", rvt_colr.Green);
                    colr_ent.Set<byte>("B", rvt_colr.Blue);

                    //Create pair entity:
                    Autodesk.Revit.DB.ExtensibleStorage.Entity pair_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(Pair_schema);
                    Field pair_set_nm_fld = Pair_schema.GetField("ParamSetName");
                    pair_ent.Set<string>(pair_set_nm_fld, prm_set_nm.ToString());
                    Field pair_filpat_fld = Pair_schema.GetField("FillPattern");
                    pair_ent.Set<int>(pair_filpat_fld, fill_patrn_elem_id);
                    Field pair_colr_fld = Pair_schema.GetField("Color");
                    pair_ent.Set<Autodesk.Revit.DB.ExtensibleStorage.Entity>(pair_colr_fld, colr_ent);
                    Field pair_prm_val_lst_fld = Pair_schema.GetField("PrmValPairs");
                    pair_ent.Set<IList<Entity>>("PrmValPairs", sub_prm_entis);
                    saved_entis.Add(pair_ent);


                }


                //Create saved item entity:
                Autodesk.Revit.DB.ExtensibleStorage.Entity saved_itm_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(Saved_itm_schema);
                saved_itm_ent.Set<string>("SavedItemName", sved_itm_nm);
                saved_itm_ent.Set<IList<Entity>>("SavedParamSet", saved_entis);

                saved_itms_lst.Add(saved_itm_ent);
                Saved_grp_ent.Set<IList<Entity>>("SavedItemList",saved_itms_lst);
                Doc.ProjectInformation.SetEntity(Saved_grp_ent);

                string a_svd_itm_nm = sved_itm_nm;
                IList<Entity> a_svd_prm_set_lst = saved_entis;
                string[] a_str_svd_itm_ent = { a_svd_itm_nm, a_svd_prm_set_lst.Count().ToString() };
                System.Windows.Forms.ListViewItem a_lstv_itm_svd_itm_ent = new System.Windows.Forms.ListViewItem(a_str_svd_itm_ent);
                a_lstv_itm_svd_itm_ent.Tag = saved_itm_ent;
                lstv_saved_grp_2.Items.Add(a_lstv_itm_svd_itm_ent);

                trans_add_entity.Commit();
            }

            /*
            Entity saved_Grp_ent = Doc.ProjectInformation.GetEntity(Saved_grp_schema);
            IList<Entity> saved_Itms_lst = saved_Grp_ent.Get<IList<Entity>>("SavedItemList");
            foreach(Entity svd_itm_ent in saved_Itms_lst)
            {
                try
                {
                    string svd_itm_nm = svd_itm_ent.Get<string>("SavedItemName");
                    IList<Entity> svd_prm_set_lst = svd_itm_ent.Get<IList<Entity>>("SavedParamSet");
                    string[] str_svd_itm_ent = { svd_itm_nm, svd_prm_set_lst.Count().ToString() };
                    System.Windows.Forms.ListViewItem lstv_itm_svd_itm_ent = new System.Windows.Forms.ListViewItem(str_svd_itm_ent);
                    lstv_itm_svd_itm_ent.Tag = svd_itm_ent;
                    lstv_saved_grp_2.Items.Add(lstv_itm_svd_itm_ent);


                }
                catch(Exception ex)
                {

                }
                

            }
            */
            

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_grp_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ListViewItem lstv_itm_chcked_grp = lstv_saved_grp_2.CheckedItems.Cast<System.Windows.Forms.ListViewItem>().FirstOrDefault();
            Entity sved_itm_ent = lstv_itm_chcked_grp.Tag as Entity;
            string sved_itm_nm = sved_itm_ent.Get<string>("SavedItemName");
            IList<Entity> sved_prm_set_lst_ent = sved_itm_ent.Get<IList<Entity>>("SavedParamSet");

            sved_prm_set_lst_ent.Clear();
            using (Transaction trans_modify_entity = new Transaction(Doc, "Save the modification"))
            {
                trans_modify_entity.Start();
                //Create saved item entity:
                IList<Entity> saved_itms_lst = Saved_grp_ent.Get<IList<Entity>>("SavedItemList");
                //label6.Text = saved_itms_lst.Count.ToString();

                //Autodesk.Revit.DB.ExtensibleStorage.Entity saved_itm_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(Saved_itm_schema);
                //saved_itm_ent.Set<string>("SavedItemName", "temp_tst(get_the_prm_from_set_name)");

                //IList<Entity> saved_entis = new List<Entity>();

                for (int i = 0; i < dt_grdv_color.Rows.Count; i++)
                {
                    DataGridViewRow r = dt_grdv_color.Rows[i] as DataGridViewRow;

                    DataGridViewCell r_cel_1 = r.Cells[1];
                    DataGridViewCell r_cel_2 = r.Cells[2];
                    prm_vals_color_pair n_pair = r.Tag as prm_vals_color_pair;

                    System.Drawing.Color rw_col = r_cel_1.Style.BackColor;
                    Autodesk.Revit.DB.Color rvt_colr = new Autodesk.Revit.DB.Color(rw_col.R, rw_col.G, rw_col.B);
                    int fill_patrn_elem_id = (int)r_cel_2.Value;
                    prm_fltr_elem_set prm_set = n_pair.prm_f_elem_set;
                    string prm_set_nm = prm_set.set_nm;
                    List<prm_val_pair> prm_set_p_vals = prm_set.p_v_pairs;
                    //prm_val_pair tst_pair = prm_set_p_vals[0];

                    //Create param value entity:
                    IList<Entity> sub_prm_entis = new List<Entity>();
                    foreach (prm_val_pair p_val in prm_set_p_vals)
                    {
                        //Schema prm_val_schema = saved_itm_ent.Schema;
                        Autodesk.Revit.DB.ExtensibleStorage.Entity prm_val_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(Prm_val_schema);
                        Field prm_val_prmid_fld = Prm_val_schema.GetField("ParamId");
                        prm_val_ent.Set<int>("ParamId", p_val.prm_id);
                        Field prm_val_prmval_fld = Prm_val_schema.GetField("ParamVal");
                        string prm_val = "";
                        if (p_val.prm_val != null)
                        {
                            prm_val = p_val.prm_val.ToString();
                        }
                        prm_val_ent.Set<string>("ParamVal", prm_val);
                        sub_prm_entis.Add(prm_val_ent);

                    }
                    //Create color entity:
                    Autodesk.Revit.DB.ExtensibleStorage.Entity colr_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(Colr_schema);
                    colr_ent.Set<byte>("R", rvt_colr.Red);
                    colr_ent.Set<byte>("G", rvt_colr.Green);
                    colr_ent.Set<byte>("B", rvt_colr.Blue);

                    //Create pair entity:
                    Autodesk.Revit.DB.ExtensibleStorage.Entity pair_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(Pair_schema);
                    Field pair_set_nm_fld = Pair_schema.GetField("ParamSetName");
                    pair_ent.Set<string>(pair_set_nm_fld, prm_set_nm.ToString());
                    Field pair_filpat_fld = Pair_schema.GetField("FillPattern");
                    pair_ent.Set<int>(pair_filpat_fld, fill_patrn_elem_id);
                    Field pair_colr_fld = Pair_schema.GetField("Color");
                    pair_ent.Set<Autodesk.Revit.DB.ExtensibleStorage.Entity>(pair_colr_fld, colr_ent);
                    Field pair_prm_val_lst_fld = Pair_schema.GetField("PrmValPairs");
                    pair_ent.Set<IList<Entity>>("PrmValPairs", sub_prm_entis);
                    sved_prm_set_lst_ent.Add(pair_ent);

                }


                //Create saved item entity:
                //Autodesk.Revit.DB.ExtensibleStorage.Entity saved_itm_ent = new Autodesk.Revit.DB.ExtensibleStorage.Entity(Saved_itm_schema);
                sved_itm_ent.Set<string>("SavedItemName", sved_itm_nm);
                sved_itm_ent.Set<IList<Entity>>("SavedParamSet", sved_prm_set_lst_ent);

                //saved_itms_lst.Add(sved_itm_ent);
                Saved_grp_ent.Set<IList<Entity>>("SavedItemList", saved_itms_lst);
                Doc.ProjectInformation.SetEntity(Saved_grp_ent);

                string a_svd_itm_nm = sved_itm_nm;
                IList<Entity> a_svd_prm_set_lst = sved_prm_set_lst_ent;
                string[] a_str_svd_itm_ent = { a_svd_itm_nm, a_svd_prm_set_lst.Count().ToString() };
                System.Windows.Forms.ListViewItem a_lstv_itm_svd_itm_ent = new System.Windows.Forms.ListViewItem(a_str_svd_itm_ent);
                a_lstv_itm_svd_itm_ent.Tag = sved_itm_ent;

                //lstv_saved_grp_2.Items.Add(a_lstv_itm_svd_itm_ent);

                trans_modify_entity.Commit();
            }


        }

        private void lstv_saved_grp_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt_grdv_color.Rows.Clear();
            System.Windows.Forms.ListViewItem lstv_itm_sel_grp = lstv_saved_grp_2.SelectedItems.Cast<System.Windows.Forms.ListViewItem>().FirstOrDefault();
            try
            {
                Entity sved_itm_ent = lstv_itm_sel_grp.Tag as Entity;
                string sved_itm_nm = sved_itm_ent.Get<string>("SavedItemName");
                IList<Entity> sved_prm_set_lst_ent = sved_itm_ent.Get<IList<Entity>>("SavedParamSet");
                List<prm_vals_color_pair> a_prm_vals_colr_pair_lst = new List<prm_vals_color_pair>();
                for(int i = 0; i < sved_prm_set_lst_ent.Count(); i ++)
                {
                    Entity sved_prm_set_ent = sved_prm_set_lst_ent[i];
                    string prm_set_nm_str = sved_prm_set_ent.Get<string>("ParamSetName");
                    Entity colorset_ent = sved_prm_set_ent.Get<Entity>("Color");
                    byte r_int = colorset_ent.Get<byte>("R");
                    byte g_int = colorset_ent.Get<byte>("G");
                    byte b_int = colorset_ent.Get<byte>("B");
                    System.Drawing.Color n_colr = System.Drawing.Color.FromArgb(r_int,g_int,b_int);
                    int fillptnid_int = sved_prm_set_ent.Get<int>("FillPattern");
                    IList<Entity> prm_val_pairs_ent_lst = sved_prm_set_ent.Get<IList<Entity>>("PrmValPairs");
                    List<prm_val_pair> lst_a_prm_val_pair = new List<prm_val_pair>();
                    foreach(Entity prm_val_pairs_ent in prm_val_pairs_ent_lst)
                    {
                        int prm_id_int = prm_val_pairs_ent.Get<int>("ParamId");
                        string prm_val = prm_val_pairs_ent.Get<string>("ParamVal");

                        prm_val_pair a_prm_val_pair = new prm_val_pair();
                        a_prm_val_pair.prm_id = prm_id_int;
                        a_prm_val_pair.prm_val = prm_val;
                        lst_a_prm_val_pair.Add(a_prm_val_pair);  

                    }
                    int r_ind = dt_grdv_color.Rows.Add();
                    DataGridViewRow row = dt_grdv_color.Rows[r_ind];
                    DataGridViewCell cel_nm_0 = row.Cells[0];
                    cel_nm_0.Value = prm_set_nm_str;

                    //
                    DataGridViewCell cel_colr_1 = row.Cells[1];
                    cel_colr_1.Value = "RGB(" + r_int.ToString() + "," + g_int.ToString() + "," + b_int.ToString() + "," + ")";
                    cel_colr_1.Style.BackColor = n_colr;
                    cel_colr_1.Style.SelectionBackColor = n_colr;
                    cel_colr_1.Style.SelectionForeColor = n_colr;

                    DataGridViewComboBoxCell cel_fil_pat_2 =  row.Cells[2] as DataGridViewComboBoxCell;
                    cel_fil_pat_2.Value = fillptnid_int;

                    //create the selfdefined items:
                    prm_fltr_elem_set a_prm_fltr_elem_set = new prm_fltr_elem_set();
                    a_prm_fltr_elem_set.set_nm = prm_set_nm_str;
                    a_prm_fltr_elem_set.p_v_pairs = lst_a_prm_val_pair;

                    FillPatternElement fill_ptn_elem = (FillPatternElement)Doc.GetElement(new ElementId(fillptnid_int));
                    prm_vals_color_pair a_prm_vals_color_pair = new prm_vals_color_pair();
                    a_prm_vals_color_pair.prm_f_elem_set = a_prm_fltr_elem_set;
                    a_prm_vals_color_pair.rvt_col = new Autodesk.Revit.DB.Color(r_int, g_int, b_int);
                    a_prm_vals_color_pair.win_col = System.Drawing.Color.FromArgb(r_int,g_int,b_int);
                    a_prm_vals_color_pair.fill_pattrn = fill_ptn_elem;
                    a_prm_vals_colr_pair_lst.Add(a_prm_vals_color_pair);
                    row.Tag = a_prm_vals_color_pair;

                }
                saved_pair_list a_saved_pair_list = new saved_pair_list();
                a_saved_pair_list.lst_pairs = a_prm_vals_colr_pair_lst;
                a_saved_pair_list.n_lst_nm = sved_itm_nm;


            }
            catch (Exception ex)
            {

            }
            
        }
    }
}
