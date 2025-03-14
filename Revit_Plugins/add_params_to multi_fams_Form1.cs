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

namespace Revit_Plugins
{
    public partial class add_params_to_multi_fams_Form1 : System.Windows.Forms.Form
    {
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
        public add_params_to_multi_fams_Form1(Autodesk.Revit.DB.Document doc, Autodesk.Revit.UI.UIApplication uiapp)
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

        class FamilyLoad_opt: IFamilyLoadOptions
        {
            public bool OnFamilyFound(
                bool familyInUse,
                out bool overwriteParameterValues)
            {
                overwriteParameterValues = true;
                return true;

            }
            public bool OnSharedFamilyFound(
                Family sharedFamily,
                bool familyInUse,
                out FamilySource source,
                out bool overwriteParameterValues)
            {
                overwriteParameterValues = true;
                source = FamilySource.Family;
                //source = default(FamilySource);
                //return FamilySource.Family;
                return true;
            }
     
        }

        private void add_params_to_multi_fams__Form1_Load(object sender, EventArgs e)
        {
            Autodesk.Revit.ApplicationServices.Application n_app = Uiapp.Application;
            DefinitionFile shd_prm_fl = n_app.OpenSharedParameterFile();
            string shd_fl_nm = shd_prm_fl.Filename;
            shd_fl_pth_labl1.Text = shd_fl_nm;

            DefinitionGroups prms_grps = shd_prm_fl.Groups;
            foreach (DefinitionGroup grp in prms_grps)
            {
                string grp_nm = grp.Name;
                Definitions dfs = grp.Definitions;
                string[] grp_info = { grp_nm, grp.ToString() };
                ListViewItem lstv_grp_info = new ListViewItem(grp_info);
                lstv_grp_info.Tag = grp;
                prm_grps_lstv01.Items.Add(lstv_grp_info);

            }
            foreach (string j in System.Enum.GetNames(typeof(BuiltInParameterGroup)))
            {
                BuiltInParameterGroup bilt_grp = (BuiltInParameterGroup)System.Enum.Parse(typeof(BuiltInParameterGroup), j, true);
                string grp_nm = LabelUtils.GetLabelFor(bilt_grp);
                string[] grp_itms = { grp_nm, j};
                ListViewItem biltin_grp_lstv = new ListViewItem(grp_itms);
                biltin_grp_lstv.Tag = j;
                bltin_grp_lstv02.Items.Add(biltin_grp_lstv);

            }
            //temp later add combo box for category;
            BuiltInCategory ct = BuiltInCategory.OST_Windows;
            //List<Element> ct_elem = new FilteredElementCollector(Doc).OfCategory
            List<Element> fam_symb_elems = new FilteredElementCollector(Doc).OfClass(typeof(FamilySymbol)).ToElements().ToList();
            List<Family> fam_elems = new List<Family>();
            List<ElementId> fam_symb_ids = new List<ElementId>();   
            foreach(Element f_sybl_elem in fam_symb_elems)
            {
                FamilySymbol f_sybl = f_sybl_elem as FamilySymbol;  
                Family symb_fam = f_sybl.Family;
                if(!fam_symb_ids.Contains(symb_fam.Id))
                {
                    fam_symb_ids.Add(symb_fam.Id);
                    fam_elems.Add(symb_fam);
                }
            }
            foreach (Family fam in fam_elems)
            {
                ElementClassFilter f_fltr = new ElementClassFilter(typeof(FamilyInstance));
                //ElementId f_inst_id = fam.GetDependentElements(f_fltr).First();
                //Element f_inst_e = Doc.GetElement(f_inst_id);
                //FamilyInstance f_inst = f_inst_e as FamilyInstance; 
                //Document f_doc = Doc.EditFamily(fam);
                //FamilyManager f_mngr = f_doc.FamilyManager;
                //string[] fam_info = { fam.Name, fam.Id.ToString(), f_inst_id.ToString() };
                Category f_cat = fam.FamilyCategory;
                string[] fam_info = { fam.Name, fam.Id.ToString(), f_cat.Name };
                ListViewItem fam_lstv = new ListViewItem(fam_info);
                fam_lstv.Tag = fam;
                fams_lstv03.Items.Add(fam_lstv);    
            }
        }

        private void shd_fl_pth_labl1_Click(object sender, EventArgs e)
        {

        }

        private void prm_grps_lstv01_SelectedIndexChanged(object sender, EventArgs e)
        {
            ex_df_lstv04.Items.Clear(); 
            List<DefinitionGroup> lst_df_grp = new List<DefinitionGroup>(); 
            foreach(ListViewItem grp_lstv in prm_grps_lstv01.SelectedItems)
            {
                DefinitionGroup l_sel_df_grp = grp_lstv.Tag as DefinitionGroup;   
                lst_df_grp.Add(l_sel_df_grp);
            }
            DefinitionGroup sel_df_grp = lst_df_grp.First();
            Definitions sel_dfs = sel_df_grp.Definitions;
            foreach(Definition sel_df in sel_dfs)
            {
                ExternalDefinition ex_df_a = sel_df as ExternalDefinition;
                ForgeTypeId dt_tp = ex_df_a.GetDataType();
                string dt_tp_nm = LabelUtils.GetLabelForSpec(dt_tp);
                string[] ex_dfs_info = { ex_df_a.Name, dt_tp_nm };
                ListViewItem ex_df_lstv = new ListViewItem(ex_dfs_info);
                ex_df_lstv.Tag = ex_df_a;
                ex_df_lstv04.Items.Add(ex_df_lstv);


            }


        }

        private void prm_grp_nm_sort(object sender, ColumnClickEventArgs e)
        {
            this.prm_grps_lstv01.ListViewItemSorter = new ListViewItemComparer(e.Column);

        }

        private void bltin_grp_lstv02_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = bltin_grp_lstv02.SelectedItems.Count.ToString();
        }

        private void bltin_grp_lstv02_column_click(object sender, ColumnClickEventArgs e)
        {
            this.bltin_grp_lstv02.ListViewItemSorter = new ListViewItemComparer(e.Column);

        }

        private void fams_lstv03_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text =  fams_lstv03.SelectedItems.Count.ToString();

        }

        private void fams_lstv03_column_click(object sender, ColumnClickEventArgs e)
        {
            this.fams_lstv03.ListViewItemSorter = new ListViewItemComparer(e.Column);

        }

        private void add_prm_btn01_Click(object sender, EventArgs e)
        {
            List<ExternalDefinition> lst_ex_dfs = new List<ExternalDefinition>();
            if (ex_df_lstv04.SelectedItems.Count > 0) 
            {
                foreach (ListViewItem prm_grp_lstv in ex_df_lstv04.SelectedItems)
                {
                    ExternalDefinition ex_df = prm_grp_lstv.Tag as ExternalDefinition;
                    lst_ex_dfs.Add(ex_df);

                }
                    
            }
            List<BuiltInParameterGroup> lst_biltin_grps = new List<BuiltInParameterGroup>();
            if (bltin_grp_lstv02.SelectedItems.Count > 0)
            {
                foreach (ListViewItem blt_grp_lstv in bltin_grp_lstv02.SelectedItems)
                {
                    if(blt_grp_lstv.Tag != null)
                    {
                        string blt_grp_nm_a = blt_grp_lstv.Tag as string;
                        BuiltInParameterGroup blt_grp = (BuiltInParameterGroup)System.Enum.Parse(typeof(BuiltInParameterGroup), blt_grp_nm_a, true);
                        lst_biltin_grps.Add(blt_grp);
                    }
                    

                }

            }
            BuiltInParameterGroup bltin_grp = lst_biltin_grps.First();
            
            //List<Family> lst_fams = new List<Family>();
            if (fams_lstv03.SelectedItems.Count > 0)
            {
                foreach (ListViewItem fams_lstv in fams_lstv03.SelectedItems)
                {
                    Family sel_fam = fams_lstv.Tag as Family;
                    Document f_doc = Doc.EditFamily(sel_fam);
                    FamilyManager f_mngr = f_doc.FamilyManager;
                    bool is_inst = isInstance_rd_btn.Checked;
                    //lst_fams.Add(sel_fams);
                    //bool true = isInstance_rd_btn.Checked = true;
                    //bool false = isInstance_rd_btn.IsDisposed;
                    //check_is_inst.Text = is_inst.ToString();
                    foreach (ExternalDefinition sel_ex_df in lst_ex_dfs)
                    {
                        Transaction trns = new Transaction(f_doc, "add_param");
                        trns.Start();
                        try
                        {
                            FamilyParameter n_fam_prm = f_mngr.AddParameter(sel_ex_df, bltin_grp, is_inst);
                        }
                        catch (Exception ex)
                        {

                        }
                        trns.Commit();
                    }
                    Family n_fam = f_doc.LoadFamily(Doc, new FamilyLoad_opt());
                }

            }




        }

        private void isInstance_rd_btn_CheckedChanged(object sender, EventArgs e)
        {
            //bool is_int = isInstance_rd_btn.Checked 
            check_is_inst.Text = isInstance_rd_btn.Checked.ToString();
        }

        private void check_is_inst_Click(object sender, EventArgs e)
        {

        }

        private void ex_df_lstv04_column_click(object sender, ColumnClickEventArgs e)
        {
            this.ex_df_lstv04.ListViewItemSorter = new ListViewItemComparer(e.Column);

        }

        private void ex_df_lstv04_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = ex_df_lstv04.SelectedItems.Count.ToString();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void isType_rd_btn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void grp_bx01_Enter(object sender, EventArgs e)
        {

        }
    }
}
