using System;
using System.Collections;
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
using static Revit_Plugins.copy_elements_bt_models_Form1;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using static Revit_Plugins.Color_picker_Form1;
using System.Windows.Shapes;
using System.Xml.Linq;


namespace Revit_Plugins
{
    public partial class filtr_mapp_Form1 : System.Windows.Forms.Form
    {
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
        public filtr_mapp_Form1(Autodesk.Revit.DB.Document doc, Autodesk.Revit.UI.UIApplication uiapp)
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

        private DataGridViewCell _active_cell;
        private void dt_grd_color_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dt_grd_color.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
                {
                    object selectedValue = dt_grd_color.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    
                    label3.Text = selectedValue.ToString();
                    
                    //MessageBox.Show($"Selected Value: {selectedValue}");
                }
               
            }
        }
        
        private void dt_grd_color_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                {
                    if (e.ColumnIndex == 2)
                    {
                        _active_cell = dt_grd_color.Rows[e.RowIndex].Cells[2];
                        ColorDialog n_color_dia = new ColorDialog();
                        n_color_dia.FullOpen = true;
                        n_color_dia.ShowHelp = true;
                        n_color_dia.Color = dt_grd_color.Rows[e.RowIndex].Cells[2].Style.BackColor;

                        //n_color_dia.ShowDialog();
                        if(n_color_dia.ShowDialog() == DialogResult.OK)
                        {
                            dt_grd_color.Rows[e.RowIndex].Cells[2].Style.BackColor = n_color_dia.Color;
                            dt_grd_color.Rows[e.RowIndex].Cells[2].Style.SelectionBackColor = n_color_dia.Color;
                        }
                        /*
                        Color_picker_Form1 n_colr_form = new Color_picker_Form1();
                        n_colr_form.ColorSelected += Color_picker_Form1_ColorSelected;
                        n_colr_form.Show();
                        */


                    }
                }
            }
        }
        

        public bool is_color_in_lst(Autodesk.Revit.DB.Color x_col, List<Autodesk.Revit.DB.Color> x_lst_col)
        {
            bool x_rslt = false;
            List<bool> x_bools = new List<bool>();
            for (int i = 0; i < x_lst_col.Count; i++)
            {
                List<bool> sub_bools = new List<bool>();
                sub_bools.Add(x_col.Red == x_lst_col[i].Red);
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
            while (is_color_in_lst(x_col, x_lst_col) == true);

            return x_col;
        }


        class cmb_box_itm
        {
            public string key { get; set; }
            public int value { get; set; }
        }

        //method create tree nodes
        public class TreeItem
        {
            public string nm { get; set; }
            public List<TreeItem> sub_itms { get; set; }
        }

        public TreeNode CreateTreeNode(TreeItem itm)
        {
            TreeNode nod = new TreeNode(itm.nm);
            foreach(var chld in itm.sub_itms)
            {
                nod.Nodes.Add(CreateTreeNode(chld));
            }
            return nod;
        }
        public void PopulateTreeView(List<TreeItem> itms, TreeView tr_view)
        {
            tr_view.Nodes.Clear();
            foreach(var itm in itms)
            {
                TreeNode nod = CreateTreeNode(itm);
                tr_view.Nodes.Add(nod);
            }
        }

        public void AddParent(string path, string node)
        {
            foreach (TreeNode tnode in trv_view_01.Nodes)
            {
                if (tnode.FullPath == path)
                {
                    tnode.Nodes.Add(node);
                    break;
                }
                checkChildren(tnode, path, node);
            }
            trv_view_01.ExpandAll();
        }
        //method create tree nodes

        public void checkChildren(TreeNode original, string path, string node)
        {
            foreach (TreeNode tnode in original.Nodes)
            {
                if (tnode.FullPath == path)
                {
                    tnode.Nodes.Add(node);
                    break;
                }
                checkChildren(tnode, path, node);
            }
        }

        public Color_picker_Form1.colr_vals select_colr {  get; set; }  
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
        public bool in_lst(string[] str, List<string[]> lst_str)
        {
            bool[] lst_bol = new bool[lst_str.Count];
            for(int j = 0; j < lst_str.Count; j++)
            {
                string[] str2 = lst_str[j];
                bool[] sub_lst_bol = new bool[str.Length];
                for (int i = 0; i < str.Length; i++)
                {
                    if (str2[i] == str[i])
                    {
                        sub_lst_bol[i] = true;
                    }
                    else
                    {
                        sub_lst_bol[i] = false;
                    }

                }
                if (sub_lst_bol.All(x => x))
                {
                    lst_bol[j] = true;
                }
                else
                {
                    lst_bol[j] = false;
                }
            }
            if(lst_bol.Any(x => x))
            {
                return true;

            }
            else
            {
                return false;
            }
           

        }
        private void filtr_mapp_Form1_Load(object sender, EventArgs e)
        {
            //all model categories
            //string pth_color_json = "src/tmp_color_json1.json";
            
            string pth_color_json = Environment.CurrentDirectory;
            //label4.Text = pth_color_json;

            Categories al_cats = Doc.Settings.Categories;
            foreach (Category cat in al_cats)
            {
                List<Element> lst_elems_x_cat = new FilteredElementCollector(Doc).OfCategoryId(cat.Id).WhereElementIsNotElementType().ToElements().ToList();
                if (lst_elems_x_cat.Count > 0)
                {
                    if (cat.CategoryType == CategoryType.Model)
                    {
                        string[] cat_info = { cat.Name, cat.Id.ToString() };
                        System.Windows.Forms.ListViewItem lstv_itm_cat = new ListViewItem(cat_info);
                        lstv_cats.Items.Add(lstv_itm_cat);
                        lstv_itm_cat.Tag = cat;
                    }
                }
            }

            List<Element> lst_al_view_elems = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_Views).WhereElementIsNotElementType().ToElements().ToList();
            BrowserOrganization brsr_org = BrowserOrganization.GetCurrentBrowserOrganizationForViews(Doc);


            List<string[]> lst_fldrs_nms = new List<string[]>();

            List<Autodesk.Revit.DB.View> lst_al_views = new List<Autodesk.Revit.DB.View>();

            //get the folders
            foreach (Element view_elem in lst_al_view_elems)
            {
                Autodesk.Revit.DB.View view = view_elem as Autodesk.Revit.DB.View;
                if (view != null && view.ViewType != ViewType.Legend && view.IsTemplate == false)
                {
                    lst_al_views.Add(view);
                    if (brsr_org.AreFiltersSatisfied(view.Id))
                    {
                        IList<FolderItemInfo> fldr_items = brsr_org.GetFolderItems(view.Id);
                        string[] fldrs_nms = new string[fldr_items.Count];

                        for (int i = 0; i < fldr_items.Count; i++)
                        {
                            string fldr_nm = fldr_items[i].Name;
                            fldrs_nms[i] = fldr_nm;
                        }

                        if (in_lst(fldrs_nms, lst_fldrs_nms) == false)
                        {
                            lst_fldrs_nms.Add(fldrs_nms);
                        }


                    }
                }
            }

           
            TreeNodeCollection nod_collct = trv_view_01.Nodes;
            //TreeNode[] tr_nod_a = new TreeNode[lst_fldrs_nms.Count];
            int cnt = lst_fldrs_nms.FirstOrDefault().Count();
            

            List<string> list_str_buildr = new List<string>();
            List<List<string>> lst_fldr_lst = new List<List<string>>();
            foreach (string[] fldrs_nms in lst_fldrs_nms)
            {
                for (int i = 0; i < cnt; i++)
                {
                    StringBuilder str_fldr_lst = new StringBuilder();
                    List<string> fldr_lst = new List<string>();
                    for (int j = 0; j <= i; j++)
                    {
                        string sub_nod_fldr = fldrs_nms[j];
                        str_fldr_lst.Append(sub_nod_fldr);
                        fldr_lst.Add(sub_nod_fldr);

                    }
                    if (!list_str_buildr.Contains(str_fldr_lst.ToString()))
                    {
                        list_str_buildr.Add(str_fldr_lst.ToString());
                        lst_fldr_lst.Add(fldr_lst);

                    }

                }


            }

            label2.Text = lst_fldr_lst.Count.ToString();
            /*
            for (int i = 0; i < lst_fldr_lst.Count; i++)
            {
                List<string> fldr_lst = lst_fldr_lst[i];
               
                AddParent(fldr_lst[0], fldr_lst[0]);

                   
            }
            */
            /*
            for (int i = 0; i < lst_fldr_lst.Count; i++)
            {
                List<string> fldr_lst = lst_fldr_lst[i];    
                for(int j = 0;j < fldr_lst.Count; j++)
                {
                    List<string> ful_pth_list = new List<string>();
                    for (int k = 0; k <= j; k++)
                    {
                        
                        ful_pth_list.Add(fldr_lst[k]);

                    }
                    string ful_pth = String.Join("/", ful_pth_list);
                    TreeNode[] tr_nod_arr = trv_view_01.Nodes.Find(ful_pth, true);
                    AddParent(ful_pth, fldr_lst[j]);
                    foreach (TreeNode node in tr_nod_arr)
                    {
                        checkChildren(node, ful_pth, fldr_lst[j]);
                    }
                    


                }
            }
            */

            
            // subfolder max count 3
            List<string> fldrs_0 = new List<string>();
            foreach (List<string> fldr_lst_a in lst_fldr_lst)
            {
                if (fldr_lst_a.Count == 3)
                {
                    TreeNode tr_nod0 = trv_view_01.Nodes[fldr_lst_a[0]];
                    if (!fldrs_0.Contains(fldr_lst_a[0]))
                    {
                        fldrs_0.Add(fldr_lst_a[0]);
                        if (tr_nod0 == null)
                        {
                            tr_nod0 = trv_view_01.Nodes.Add(fldr_lst_a[0]);
                        }

                    }

                }

            }
            TreeNodeCollection tr_nods_0 = trv_view_01.Nodes;

            List<string> fldrs_1 = new List<string>();
            foreach (List<string> fldr_lst_a in lst_fldr_lst)
            {

                if (fldr_lst_a.Count == 3)
                {

                    foreach (TreeNode tr_nod0 in tr_nods_0)
                    {
                        if (fldr_lst_a[0] == tr_nod0.Text)
                        {
                            TreeNode tr_nod1 = tr_nod0.Nodes[fldr_lst_a[1]];
                            if (!fldrs_1.Contains(tr_nod0.Text + fldr_lst_a[1]))
                            {
                                fldrs_1.Add(tr_nod0.Text + fldr_lst_a[1]);
                                if (tr_nod1 == null)
                                {
                                    tr_nod1 = tr_nod0.Nodes.Add(fldr_lst_a[1]);
                                }

                            }


                        }

                    }
                }
            }
            List<string> fldrs_2 = new List<string>();
            foreach (List<string> fldr_lst_a in lst_fldr_lst)
            {

                if (fldr_lst_a.Count == 3)
                {

                    foreach (TreeNode tr_nod0 in tr_nods_0)
                    {


                        foreach (TreeNode tr_nod1 in tr_nod0.Nodes)
                        {

                            if (fldr_lst_a[0] == tr_nod0.Text && fldr_lst_a[1] == tr_nod1.Text)
                            {
                                TreeNode tr_nod2 = tr_nod1.Nodes[fldr_lst_a[2]];
                                if (!fldrs_2.Contains(tr_nod0.Text + tr_nod1.Text + fldr_lst_a[2]))
                                {
                                    fldrs_2.Add(tr_nod0.Text + tr_nod1.Text + fldr_lst_a[2]);
                                    if (tr_nod2 == null)
                                    {
                                        tr_nod2 = tr_nod1.Nodes.Add(fldr_lst_a[2]);
                                    }

                                }

                            }

                        }


                    }
                }
            }

            // subfolder max count 3
            string tmp_str = trv_view_01.Nodes[0].Nodes[0].Nodes[0].FullPath.ToString();
            //label2.Text = 
            
            foreach (Autodesk.Revit.DB.View v1 in lst_al_views)
            {
                if (brsr_org.AreFiltersSatisfied(v1.Id))
                {
                    IList<FolderItemInfo> fldr_items = brsr_org.GetFolderItems(v1.Id);
                    StringBuilder sb_fldr = new StringBuilder();
                    for (int i = 0; i < fldr_items.Count; i++)
                    {
                        sb_fldr.Append(fldr_items[i].Name);
                    }
                    string sb_fldr_nm = sb_fldr.ToString();
                    foreach (TreeNode tr_n0 in trv_view_01.Nodes)
                    {
                        foreach (TreeNode tr_n1 in tr_n0.Nodes)
                        {
                            foreach (TreeNode tr_n2 in tr_n1.Nodes)
                            {
                                string tr_nms = tr_n0.Text + tr_n1.Text + tr_n2.Text;

                                if (sb_fldr_nm == tr_nms)
                                {
                                    TreeNode tr_n3 = tr_n2.Nodes.Add(v1.Name.ToString());
                                    List<Autodesk.Revit.DB.View> lst_v3 = new List<Autodesk.Revit.DB.View>();
                                    tr_n3.Tag = lst_v3;
                                }

                            }
                        }
                    }

                }
            }
            foreach (TreeNode tr_n0 in trv_view_01.Nodes)
            {
                List<Autodesk.Revit.DB.View> lst_v0 = new List<Autodesk.Revit.DB.View>();
                foreach (TreeNode tr_n1 in tr_n0.Nodes)
                {
                    List<Autodesk.Revit.DB.View> lst_v1 = new List<Autodesk.Revit.DB.View>();
                    foreach (TreeNode tr_n2 in tr_n1.Nodes)
                    {
                        List<Autodesk.Revit.DB.View> lst_v2 = new List<Autodesk.Revit.DB.View>();
                        foreach (TreeNode tr_n3 in tr_n2.Nodes)
                        {
                            List<Autodesk.Revit.DB.View> v3 = tr_n3.Tag as List<Autodesk.Revit.DB.View>;
                            foreach (Autodesk.Revit.DB.View vv3 in v3)
                            {
                                lst_v2.Add(vv3);
                            }
                        }
                        tr_n2.Tag = lst_v2;
                        foreach (Autodesk.Revit.DB.View v2 in lst_v2)
                        {
                            lst_v1.Add(v2);
                        }


                    }
                    tr_n1.Tag = lst_v1;
                    foreach (Autodesk.Revit.DB.View v1 in lst_v1)
                    {
                        lst_v0.Add(v1);
                    }
                }
                tr_n0.Tag = lst_v0;

            }

            //temp_datagridview test
            List<Element> al_fill_pattrn_tps_elems = new FilteredElementCollector(Doc).OfClass(typeof(FillPatternElement)).ToElements().ToList();
            //label3.Text = al_fill_pattrn_tps_elems.Count.ToString();
            //dt_grd_color.EditingControlShowing += new DataGridViewEditingControlShowingEventArgs(dt_grd_color_EditingControlShowing);
            
            List<FillPatternElement> al_fill_pattrn = new List<FillPatternElement>();
            List<cmb_box_itm> lst_cmb_box_fil_pattrn = new List<cmb_box_itm>();
            for(int i = 0; i < 10; i++)
            {
                FillPatternElement fil_pattrn = al_fill_pattrn_tps_elems[i] as FillPatternElement;
                cmb_box_itm cmb_box_fil_pattrn = new cmb_box_itm();
                cmb_box_fil_pattrn.key = fil_pattrn.Name.ToString();
                cmb_box_fil_pattrn.value = fil_pattrn.Id.IntegerValue;
                lst_cmb_box_fil_pattrn.Add(cmb_box_fil_pattrn);
                
            }
            int default_val = lst_cmb_box_fil_pattrn[2].value;
            dt_grd_color.Rows.Clear();
            dt_grd_color.CellValueChanged += new DataGridViewCellEventHandler(dt_grd_color_CellValueChanged);
            dt_grd_color.CellClick += new DataGridViewCellEventHandler(dt_grd_color_CellClick);


            List<Autodesk.Revit.DB.Color> lst_color = new List<Autodesk.Revit.DB.Color> ();
            DataGridViewComboBoxColumn dt_col = dt_grd_color.Columns[0] as DataGridViewComboBoxColumn;
            dt_col.DataSource = lst_cmb_box_fil_pattrn;
            dt_col.DisplayMember = "key";
            dt_col.ValueMember = "value";

            for (int j = 0; j < lst_cmb_box_fil_pattrn.Count; j++)
            {

                DataGridViewRow dt_grd_row = new_row(dt_grd_color, j);

                DataGridViewComboBoxCell dt_grd_cel = dt_grd_row.Cells[0] as DataGridViewComboBoxCell;
                dt_grd_cel.Value = default_val;
            
                //dt_grd_cel.DataSource = lst_cmb_box_fil_pattrn;
                //dt_grd_cel.DisplayMember = "key";
                //dt_grd_cel.ValueMember = "value";

                Autodesk.Revit.DB.Color rnd_color = create_rnd_col(lst_color);
                lst_color.Add(rnd_color);

                DataGridViewCell dt_grd_cel_1 = dt_grd_row.Cells[2];
                System.Drawing.Color win_col = System.Drawing.Color.FromArgb(rnd_color.Red, rnd_color.Green, rnd_color. Blue);
                dt_grd_cel_1.Value = "R: " + rnd_color.Red.ToString() + "," + "G: " + rnd_color.Green.ToString() + ","+"B: " + rnd_color.Blue.ToString();
                DataGridViewCellStyle  styl =  new DataGridViewCellStyle();
                styl.BackColor = win_col;
                styl.SelectionBackColor = win_col;
                dt_grd_cel_1.Style = styl;

                DataGridViewButtonCell dt_grd_cel_2 = dt_grd_row.Cells[1] as DataGridViewButtonCell;
                DataGridViewCellStyle styl_a = new DataGridViewCellStyle();
                styl_a.ForeColor = win_col;
                dt_grd_cel_2.Style = styl_a;



            }
            

            DataGridViewComboBoxCell tmp = dt_grd_color.Rows[2].Cells[0] as DataGridViewComboBoxCell;


        }

        private void lstv_cats_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ElementId> lst_ct_ids = new List<ElementId>();
            lst_ct_ids.Clear();
            foreach(System.Windows.Forms.ListViewItem lstv_ct in lstv_cats.CheckedItems)
            {
                Category sel_cat = lstv_ct.Tag as Category;
                lst_ct_ids.Add(sel_cat.Id);
            }
            ICollection<ElementId> lst_cat_id = lst_ct_ids;
            ICollection<ElementId> lst_fltr_prms_ids = ParameterFilterUtilities.GetFilterableParametersInCommon(Doc,lst_ct_ids);
            foreach (ElementId prm_id in lst_fltr_prms_ids)
            {
                Element prm = Doc.GetElement(prm_id);
                if (prm != null)
                {
                    string[] lst_v_prm = { prm.Name, prm.Id.ToString() };
                    System.Windows.Forms.ListViewItem lstv_prm = new System.Windows.Forms.ListViewItem(lst_v_prm);
                    lstv_prms.Items.Add(lstv_prm);
                    lstv_prm.Tag = prm;

                }
            }

        }

        private void lstv_cats_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lstv_cats.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        private void trv_view_01_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lstv_tmp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbx_fill_pttrn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbx_param_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dt_grd_color_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lstv_prms_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dt_grd_color.Rows.Count; i++)
            {
                DataGridViewRow row = dt_grd_color.Rows[i];
                try
                {
                    DataGridViewComboBoxCell cell = row.Cells[0] as DataGridViewComboBoxCell;
                    string[] tst_str = { i.ToString(), cell.Value.ToString() };
                    ListViewItem lsvt_itm = new ListViewItem(tst_str);
                    lstv_tmp.Items.Add(lsvt_itm);
                }
                catch(Exception ex)
                {

                }
                

            }

        }
        /*
        private void Color_picker_Form1_ColorSelected(object sender, Color_picker_Form1.colr_vals sel_colr)
        {
            
            System.Drawing.Color select_col = System.Drawing.Color.FromArgb(sel_colr.colr_r, sel_colr.colr_g, sel_colr.colr_b);
            string select_col_nm = "R: " + sel_colr.colr_r.ToString() + "," + "G: " + sel_colr.colr_g.ToString() + "," + "B: " + sel_colr.colr_b.ToString();
            DataGridViewCellStyle sel_styl = new DataGridViewCellStyle();
            sel_styl.BackColor = select_col;
            _active_cell.Style = sel_styl;
            _active_cell.Value = select_col_nm;
           
        }
        */

        private void a_Color_picker_Form1_ColorSelected(object sender, Color_picker_Form1.colr_vals sel_colr)
        {
            //label4.Text = sel_colr.colr_r.ToString() + "," + sel_colr.colr_g.ToString() + "," + sel_colr.colr_b.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Color_picker_Form1 colr_picker_form = new Color_picker_Form1;
            //colr_picker_form.ColorSelected += a_Color_picker_Form1_ColorSelected;
            //colr_picker_form.Show();
            ColorDialog colr_dia = new ColorDialog();
            colr_dia.ShowDialog();
 

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
