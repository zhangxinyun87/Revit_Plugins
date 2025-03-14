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

namespace Revit_Plugins
{

    public partial class btn_chck_in_v : System.Windows.Forms.Form
    {

        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
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
        public btn_chck_in_v(Document doc,UIApplication uiapp)
        {
            InitializeComponent();
            this.Doc = doc;
            this.Uiapp = uiapp;

        }
        public class Clsh
        {
            public Element clsh_item_a { get; set; }
            public Element clsh_item_b { get; set; }
            public string clsh_it_nm_a {  get; set; }  
            public string clsh_it_nm_b { get; set; }
            public string clsh_it_id_a { get; set; }
            public string clsh_it_id_b { get; set; }
            public string clsh_it_cat_a { get; set; }
            public string clsh_it_cat_b { get; set; }
            public string clsh_name { get; set; }
        }
        public class selClsh
        {
            public ElementId clsh_id_a { get; set; }
            public ElementId clsh_id_b { get; set; }
            //public List<ElementId> pub_sel_ids {  get; set; } 
            
        }
        public List<Clsh> all_pub_clshs { get; set; }
        public List<selClsh> pub_clshs { get; set; }
        public ListView.SelectedListViewItemCollection sel_pub_clshs { get; set; }   
        //public Autodesk.Revit.DB.View c_view { get; set; }
        


        private void clash_visual_Form1_Load(object sender, EventArgs e)
        {
            List<Element> fec_all = new List<Element>();
            List<Element> fec_all_0 = new FilteredElementCollector(Doc).WhereElementIsNotElementType().ToElements().ToList();
            
            foreach(Element elem in fec_all_0)
            {
                if (elem.Category != null)
                {
                    if(elem.Category.HasMaterialQuantities == true)
                    {
                        fec_all.Add(elem);
                    }
                   
                }
            }
            

            List< ElementIntersectsElementFilter > fltr_s = new List<ElementIntersectsElementFilter>();
            foreach(Element elem in fec_all)
            {
                try
                {
                    ElementIntersectsElementFilter fltr = new ElementIntersectsElementFilter(elem);
                    fltr_s.Add(fltr);
                }

                catch(Exception ex)
                {

                }
            }
            //label1.Text = fltr_s.Count.ToString();  
            List<Clsh> clshs0 = new List<Clsh>();
            foreach(ElementIntersectsElementFilter f in fltr_s)
            {
                if(f != null)
                {
                    foreach(Element elem in fec_all)
                    {
                        if(elem != null)
                        {
                            if(f.PassesFilter(elem) == true)
                            {

                                Clsh clsh = new Clsh();
                                {
                                    clsh.clsh_item_a = elem;
                                    clsh.clsh_item_b = f.GetElement();
                                    clsh.clsh_it_nm_a = elem.Name;
                                    clsh.clsh_it_nm_b = f.GetElement().Name;
                                    clsh.clsh_it_id_a = elem.Id.ToString();
                                    clsh.clsh_it_id_b = f.GetElement().Id.ToString();
                                    clsh.clsh_it_cat_a = elem.Category.Name.ToString();
                                    clsh.clsh_it_cat_b = f.GetElement().Category.Name.ToString();
                                    clsh.clsh_name = "A:"+elem.Name.ToString() + "("+ elem.Id.ToString() + "-" + elem.Category.Name.ToString() + ")"+ "&" +"B:" + f.GetElement().Name.ToString() + "(" + f.GetElement().Id.ToString() + "-" + f.GetElement().Category.Name.ToString() + ")";
                                }
                                clshs0.Add(clsh);
                            }
                        }
                    }
                }
            }
            //lstBox_clash_itms.DataSource = clshs0;
            
            List<Clsh> clshs1 = new List<Clsh>();
            List<string> ids = new List<string>();
            for(int i = 0; i < clshs0.Count; i++)
            {
                List<int> sub_ids = new List<int>();
                List<int> ord_sub_ids = new List<int>();
                {
                    int id0 = clshs0[i].clsh_item_a.Id.IntegerValue;
                    int id1 = clshs0[i].clsh_item_b.Id.IntegerValue ;
                    sub_ids.Add(id0);
                    sub_ids.Add(id1);
                }
                ord_sub_ids = sub_ids.OrderBy(x => x).ToList();
                ids.Add(ord_sub_ids[0].ToString() + ord_sub_ids[1].ToString());
                
            }

            List<string> dist_ids = ids.Distinct().ToList();
            
            List<int> indxs = new List<int>();  
            foreach(string id0 in dist_ids)
            {
                List<int> sub_indx = new List<int>();
                {
                    sub_indx.Add(ids.IndexOf(id0));
                }
                
                indxs.Add(sub_indx.First());

            }
            

            foreach (int ind in indxs)
            {
                clshs1.Add(clshs0[ind]);
            }
            label1.Text = clshs1.Count.ToString() + " in file clashes found";
            all_pub_clshs = clshs1;
            //lstBox_clash_itms.DataSource = clshs1;
            //lstBox_clash_itms.DisplayMember = "clsh_name";
            /*
            string[] clsh_a_nms = {clshs1[0].clsh_it_nm_a.ToString(), clshs1[1].clsh_it_nm_a.ToString(), clshs1[2].clsh_it_nm_a.ToString()};
            string[] clsh_b_nms = { clshs1[0].clsh_it_nm_a.ToString(), clshs1[1].clsh_it_nm_a.ToString(), clshs1[2].clsh_it_nm_a.ToString()};
            ListViewItem clsh_na = new ListViewItem();
            ListViewItem clsh_nb = new ListViewItem();
            */
            List<ListViewItem> all_clshs = new List<ListViewItem>();
            int num_l = clshs1.Count.ToString().Length;
            for (int i = 1; i <= clshs1.Count; i++)
            {
                
                string num = i.ToString().PadLeft(num_l, '0');
                string[] clsh_a_nms = {num.ToString(), clshs1[i].clsh_it_nm_a.ToString(), clshs1[i].clsh_it_id_a.ToString(), clshs1[i].clsh_it_cat_a.ToString(), clshs1[i].clsh_it_nm_b.ToString(), clshs1[i].clsh_it_id_b.ToString(), clshs1[i].clsh_it_cat_b.ToString() };
                ListViewItem clsh = new ListViewItem(clsh_a_nms);
                lstV_clash_itms.Items.Add(clsh);
                all_clshs.Add(clsh);

            }
            
            
            
            /*
            List<Clsh> clshs2 = clshs1.GetRange(0, 4000)
            List<int> clsh_n0 = new List<int>();

            for (int i = 1; i < clshs2.Count; i++)
            {
            clsh_n0.Add(i);
            clsh_n.SubItems.Add(i.ToString());
            }

            ListViewItem clsh_a = new ListViewItem();
            List<int> clsh_a0 = new List<int>();

            foreach(Clsh cl in clshs2)
            {

            clsh_n.SubItems.Add(cl.clsh_item_a.Name.ToString());
            }
            */

           
        }

        /*
        private void lstBox_clash_itms_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        */

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Autodesk.Revit.DB.View view = Doc.ActiveView;
            ListView.SelectedListViewItemCollection selectedItems = this.lstV_clash_itms.SelectedItems;
            sel_pub_clshs = selectedItems;
            //List<string[]> clsh_sel_str = selectedItems;
            List<selClsh> clsh_sel = new List<selClsh>();   
            foreach (ListViewItem clsh in selectedItems)
            {
                selClsh n_clsh = new selClsh();
                {
                    int n_a = Int32.Parse(clsh.SubItems[2].Text);
                    int n_b = Int32.Parse(clsh.SubItems[5].Text);
                    ElementId id_a = new ElementId(n_a);
                    ElementId id_b = new ElementId(n_b);
                    n_clsh.clsh_id_a = id_a;
                    n_clsh.clsh_id_b = id_b;

                }
                clsh_sel.Add(n_clsh);
            }
            //List<Clsh> clsh_sel = lstBox_clash_itms.SelectedItems.Cast<Clsh>().ToList();

            label1.Text = clsh_sel.Count.ToString() + " clash sets selected";
            pub_clshs = clsh_sel;
            List<ElementId> sel_ids = new List<ElementId>();
            FillPatternElement pattn = FillPatternElement.GetFillPatternElementByName(Doc, FillPatternTarget.Drafting, "<Riempimento solido>");
            /* //select by isSolid
            List<FillPatternElement> fill_pt = new FilteredElementCollector(Doc).OfClass(FillPatternElement).WhereElementIsElementType().ToElements().ToList();
            foreach(Element pt in fill_pt)
            {
                if(pt != null)
                {
                    if(pt.GetFillPattern().IsSolidFill == true)
                    {

                    }
                }
            }
            */
            UIDocument uidoc = Uiapp.ActiveUIDocument;
            
            using (Transaction trans0 = new Transaction(Doc))
            {
                trans0.Start("select clash elements in view");
                Autodesk.Revit.DB.Color colr01 = new Autodesk.Revit.DB.Color(250, 0, 0);
                Autodesk.Revit.DB.Color colr02 = new Autodesk.Revit.DB.Color(0, 250, 0);
                OverrideGraphicSettings grph_01 = new OverrideGraphicSettings();
                grph_01.SetSurfaceForegroundPatternId(pattn.Id);
                grph_01.SetSurfaceForegroundPatternColor(colr01);
                OverrideGraphicSettings grph_02 = new OverrideGraphicSettings();
                grph_02.SetSurfaceForegroundPatternId(pattn.Id);
                grph_02.SetSurfaceForegroundPatternColor(colr02);
                //sel_ids = null;
                foreach (selClsh clsh in clsh_sel)
                {
                    sel_ids.Add(clsh.clsh_id_a);
                    sel_ids.Add(clsh.clsh_id_b);
                    view.GetElementOverrides(clsh.clsh_id_a);
                    view.SetElementOverrides(clsh.clsh_id_a, grph_01);
                    view.GetElementOverrides(clsh.clsh_id_a);
                    view.SetElementOverrides(clsh.clsh_id_b, grph_02);
                }
                uidoc.ShowElements(sel_ids);
                view.IsolateElementsTemporary(sel_ids);
                trans0.Commit();
            }
            //pub_sel_ids = sel_ids;
            
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            Autodesk.Revit.DB.View view = Doc.ActiveView;
            label1.Text = null;
            label1.Text = all_pub_clshs.Count.ToString() + " in file clashes found";
            OverrideGraphicSettings grph_00 = new OverrideGraphicSettings();
            using (Transaction trans1 = new Transaction(Doc))
            {
                trans1.Start("clear override and unisolate elements");
                foreach (selClsh clsh in pub_clshs)
                {
                    view.SetElementOverrides(clsh.clsh_id_a, grph_00);
                    view.SetElementOverrides(clsh.clsh_id_b, grph_00);

                }
                view.DisableTemporaryViewMode(TemporaryViewMode.TemporaryHideIsolate);
                trans1.Commit();

            }
            
            
        }

        private void lstV_clash_itms_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstV_clash_itms_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lstV_clash_itms.ListViewItemSorter =  new ListViewItemComparer(e.Column);

            /*
            lvwColumnSorter = new ListViewColumnSorter();
            ListViewItemSorter lvwColumnSorter
            
            this.lstV_clash_itms.ListViewItemSorter = lvwColumnSorter;
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView1.Sort();
            */
        }
    }
}
