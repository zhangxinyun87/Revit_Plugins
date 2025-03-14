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

namespace Revit_Plugins
{
    public partial class check_fam_param_Form1 : System.Windows.Forms.Form
    {
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
        public check_fam_param_Form1(Autodesk.Revit.DB.Document doc, Autodesk.Revit.UI.UIApplication uiapp)
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

        class FamilyLoad_opt : IFamilyLoadOptions
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
        //private object f_prm_g;
        //public Guid f_prm_g { get; set; }
        public List<string> shrd_prm_nms { get; set; }
        public List<Guid> shrd_prm_ids { get; set; }
        public Category selcat { get; set; }
        public List<Family> sel_fams_pub { get; set; }
        public List<ExternalDefinition> lst_ext_def_pub { get; set; }
        public IEnumerable<int> lst_fam_ids { get; set; }
        public class FamilyLoadOptions : IFamilyLoadOptions
        {
            public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
            {
                overwriteParameterValues = true;
                return true;
            }

            public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source, out bool overwriteParameterValues)
            {
                source = FamilySource.Project;
                overwriteParameterValues = true;
                return true;
            }
        }

        public class Cate_gry
        {
            public string cat_nm { get; set; }
            public Category cat { get; set; }
            public ElementId cat_id { get; set; }
            public BuiltInCategory built_Cat { get; set; }
        }
        

        private void check_room_info_Form1_Load(object sender, EventArgs e)
        {
            Categories all_cats = Doc.Settings.Categories;

            //List<Element> tst = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_Windows).WhereElementIsNotElementType().ToElements().ToList();
            List<Cate_gry> sel_cats = new List<Cate_gry>();
            foreach (Category cat in all_cats)
            {
                Cate_gry sel_cat0 = new Cate_gry();
                {
                    sel_cat0.cat = cat;
                    sel_cat0.cat_id = cat.Id;
                    sel_cat0.cat_nm = cat.Name;
                    sel_cat0.built_Cat = cat.BuiltInCategory;
                }
                sel_cats.Add(sel_cat0);
                //cbox_sel_cat1.Items.Add(cat.Name);
            }
            //List<Cate_gry> sel_cats0 = sel_cats.OrderByDescending();
            //sel_cats.Sort();
            cbox_sel_cat1.DataSource = sel_cats;
            cbox_sel_cat1.DisplayMember = "cat_nm";

            DefinitionFile sh_params_file = Doc.Application.OpenSharedParameterFile();
            List<DefinitionGroup> lst_def_grp = sh_params_file.Groups.ToList();
            List<ExternalDefinition> lst_ext_def = new List<ExternalDefinition>();
            List<Guid> lst_gids = new List<Guid>();
            List<string> lst_names = new List<string>();    
            
            foreach(DefinitionGroup grp in lst_def_grp)
            {
                //add definition group to data to listview
                string[] df_grps = {grp.Name, grp.Name};
                ListViewItem lstv_df_grps = new ListViewItem(df_grps);
                Definitions dfs = grp.Definitions;  
                lstv_df_grps.Tag = dfs;
                lstv_shrd_prm_grps.Items.Add(lstv_df_grps);

                List<Definition> lst_def = grp.Definitions.ToList();
                
                foreach(Definition def in lst_def)
                {
                    ExternalDefinition ex_def = def as ExternalDefinition; 
                    lst_ext_def.Add(def as ExternalDefinition);
                    Guid df_gid = ex_def.GUID;
                    lst_gids.Add(df_gid);
                    string df_nm = ex_def.Name; 
                    lst_names.Add(df_nm);   


                }
            }
            lst_ext_def_pub = lst_ext_def;
            shrd_prm_nms = lst_names;
            shrd_prm_ids = lst_gids;

            //tst_labl.Text = sh_params_file.Filename.ToString();


        }

        private void lstv_fam_loading_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstv_prm_tr_fam1.Items.Clear();

            List<int> lst_sel_fam = new List<int>();
            foreach(ListViewItem lvs in lstv_fam_loading.SelectedItems)
            {
                Element s_f = lvs.Tag as Element;
                int s_f_id = s_f.Id.IntegerValue;
                lst_sel_fam.Add(s_f_id);
            }
            foreach (int id1 in lst_fam_ids)
            {
               
                if (!lst_sel_fam.Contains(id1))
                {
                    ElementId id = new ElementId(id1);

                    Family elem = (Family)Doc.GetElement(id);

                    ///string[] lstv_itm = {elem.ToString()};

                    string[] lstv_itm1 = { elem.Name.ToString(), elem.Id.ToString() };
                    
                    ListViewItem lst_v_itm1 = new ListViewItem(lstv_itm1);
                    
                    lst_v_itm1.Tag = elem;

                    
                    lstv_prm_tr_fam1.Items.Add(lst_v_itm1);

                }
                
            }//Category s_cat = cbox_sel_cat1.SelectedItem as Category;

            //List<Element> all_fam_insts = new FilteredElementCollector(Doc).OfCategory(s_cat).WhereElementIsNotElementType().ToElements().ToList();
        }

        private void cbox_sel_cat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cate_gry s_cat0 = cbox_sel_cat1.SelectedItem as Cate_gry;
            ElementClassFilter fltr0 = new ElementClassFilter(typeof(FamilyInstance));
            List<Element> all_fam_insts = new FilteredElementCollector(Doc).OfCategory(s_cat0.built_Cat).WhereElementIsNotElementType().ToElements().ToList();

            lstv_fam_loading.Items.Clear();
            lstv_prm_tr_fam1.Items.Clear();
            List<ElementId> fams_ids = new List<ElementId>();
            List<int> fam_id_int = new List<int>();
            foreach (Element elem in all_fam_insts)
            {
                if (fltr0.PassesFilter(elem))
                {
                    FamilyInstance f_inst = elem as FamilyInstance;
                    Family fam1 = f_inst.Symbol.Family;
                    ElementId fam1_id = fam1.Id;

                    fam_id_int.Add(fam1_id.IntegerValue);

                }


            }
            IEnumerable<int> fams_unique_ids = fam_id_int.Distinct();
            lst_fam_ids = fams_unique_ids.ToList();
            foreach (int id0 in fams_unique_ids)
            {
                ElementId id = new ElementId(id0);
                Family elem = (Family)Doc.GetElement(id);
                ///string[] lstv_itm = {elem.ToString()};
                string[] lstv_itm = { elem.Name.ToString(), elem.Id.ToString() };
               
                ListViewItem lst_v_itm = new ListViewItem(lstv_itm);
                
                lst_v_itm.Tag = elem;
                

                lstv_fam_loading.Items.Add(lst_v_itm);
                
            }
        }

        private void lstv_prm1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tst_labl.Text = lstv_prm1.SelectedItems.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ListView.SelectedListViewItemCollection sel_itms = lstv_fam_loading.SelectedItems;
            lstv_prm1.Items.Clear();
            List<Family> fams = new List<Family>();
            foreach (ListViewItem sel_itm in sel_itms)
            {

                Family fam0 = sel_itm.Tag as Family;
                

                if (fam0 != null)
                {
                    Document f_doc = Doc.EditFamily(fam0 as Family);
                    FamilyManager f_mnger = f_doc.FamilyManager;

                    List<FamilyParameter> f_params = (List<FamilyParameter>)f_mnger.GetParameters();


                    foreach (FamilyParameter f_prm in f_params)
                    {
                        ElementId f_prm_id = f_prm.Id;
                        Guid f_g = f_prm.IsShared ? f_prm.GUID : Guid.Empty;

                        string f_prm_inst = f_prm.IsInstance.ToString();
                        string f_prm_frmula = f_prm.Formula == null ? " " : f_prm.Formula.ToString();
                        BuiltInParameterGroup f_prm_grp = f_prm.Definition.ParameterGroup;

                        string[] lstv_prm_str = { fam0.Id.ToString(), f_prm_grp.ToString(), f_prm.Definition.Name.ToString(), f_prm_inst, f_prm_frmula ,f_prm_id.ToString(), f_g.ToString()};
                        ListViewItem lstv_prm_itm = new ListViewItem(lstv_prm_str);
                        lstv_prm_itm.Tag = f_prm;
                        lstv_prm1.Items.Add(lstv_prm_itm);
                    }

                    //fams.Add(fam0);

                }


            }

            //sel_fams_pub = fams;
        }

        private void lstv_prm1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lstv_prm1.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        private void lstv_fam_loading_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lstv_fam_loading.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        private void lstv_prm_tr_fam1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lstv_prm_tr_fam1.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        private void lstv_prm_tr_fam1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_fam_trnsfr.Text = lstv_prm_tr_fam1.SelectedItems.Count.ToString() + "-" + lstv_prm_tr_fam1.CheckedItems.Count.ToString();
        }

        private void btn_tr_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection sel_iprms = lstv_prm1.SelectedItems;
            List<FamilyParameter> sel_prms = new List<FamilyParameter>();
            
            foreach (ListViewItem sel_iprm in sel_iprms)
            {
                FamilyParameter s_prm_fam = sel_iprm.Tag as FamilyParameter;
                sel_prms.Add(s_prm_fam);

            }

            //tst_labl.Text = sb.ToString();



            //ListView.SelectedListViewItemCollection sel_ifams = lstv_prm_tr_fam1.CheckedItems;

            List<Family> sel_fams = new List<Family>();
            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem sel_f in lstv_prm_tr_fam1.CheckedItems)
            {
                //int sel_f_id1 = int.Parse(sel_f.SubItems[1].Text);
                //ElementId sel_itm_id = new ElementId(sel_f_id1);
                Family sel_fam0 = sel_f.Tag as Family;
               
                if (sel_fam0 != null)
                {
                    //Family sel_fam0 = sel_fam1 as Family;
                    Document sel_f_doc = Doc.EditFamily(sel_fam0 as Family);
                    FamilyManager sel_f_mnger = sel_f_doc.FamilyManager;
                    string sel_fam_file_nm = sel_f_doc.Title;
                    //List<FamilyParameter> sel_f_prms = (List<FamilyParameter>)sel_f_mnger.GetParameters();

                    
                    foreach(ListViewItem lvt_f_prm in lstv_prm1.CheckedItems)
                    {
                        FamilyParameter s_f_prm = lvt_f_prm .Tag as FamilyParameter;    

                        if (s_f_prm.IsShared)
                        {
                            string sf_prm_nm = s_f_prm.Definition.Name;
                            Guid s1_prm_guid = (Guid)s_f_prm.GUID;
                            if (shrd_prm_ids.Contains(s1_prm_guid))
                            {
                                string s1_prm_fmla0 = s_f_prm.Formula;
                                bool s2_prm_inst0 = s_f_prm.IsInstance;
                                StorageType s1_prm_strg_tp = s_f_prm.StorageType;

                                Definition s1_df = s_f_prm.Definition;
                                BuiltInParameterGroup sel_grp0 = s1_df.ParameterGroup;

                                foreach (ExternalDefinition ex_def in lst_ext_def_pub)
                                {
                                    Guid guid0 = s1_prm_guid;


                                    using (Transaction trns0 = new Transaction(sel_f_doc, "add_parameter"))
                                    {
                                        trns0.Start();
                                        if (ex_def.GUID == guid0)
                                        {
                                            try
                                            {
                                                FamilyParameter n_fam_prm = sel_f_mnger.AddParameter(ex_def, sel_grp0, s2_prm_inst0);

                                                //sb.Append(n_fam_prm.Definition.Name);
                                                //sb.Append(' ');

                                                if (s1_prm_fmla0 != null && s1_prm_fmla0 != "")
                                                {
                                                    try
                                                    {
                                                        sel_f_mnger.SetFormula(n_fam_prm, s1_prm_fmla0);

                                                    }
                                                    catch (Exception ex)
                                                    {

                                                    }
                                                }

                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                            trns0.Commit();



                                        }
                                        //bool rsult = Doc.LoadFamily(sel_fam_file_nm, new FamilyLoadOptions(), out sel_fam0);
                                        //sb.Append(rsult.ToString());


                                    }

                                }

                            }
                            else if (!shrd_prm_ids.Contains(s1_prm_guid) && shrd_prm_nms.Contains(sf_prm_nm))
                            {
                                string s1_prm_fmla1 = s_f_prm.Formula;
                                bool s2_prm_inst1 = s_f_prm.IsInstance;
                                StorageType s1_prm_strg_tp = s_f_prm.StorageType;

                                Definition s1_df1 = s_f_prm.Definition;
                                BuiltInParameterGroup sel_grp1 = s1_df1.ParameterGroup;

                                foreach (ExternalDefinition ex_def1 in lst_ext_def_pub)
                                {
                                    Guid guid1 = s1_prm_guid;


                                    using (Transaction trns1 = new Transaction(sel_f_doc, "add_parameter"))
                                    {
                                        trns1.Start();
                                        if (ex_def1.Name == sf_prm_nm)
                                        {
                                            try
                                            {
                                                FamilyParameter n_fam_prm1 = sel_f_mnger.AddParameter(ex_def1, sel_grp1, s2_prm_inst1);

                                                //sb.Append(n_fam_prm.Definition.Name);
                                                //sb.Append(' ');

                                                if (s1_prm_fmla1 != null && s1_prm_fmla1 != "")
                                                {
                                                    try
                                                    {
                                                        sel_f_mnger.SetFormula(n_fam_prm1, s1_prm_fmla1);

                                                    }
                                                    catch (Exception ex)
                                                    {

                                                    }
                                                }

                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                            trns1.Commit();



                                        }
                                        //bool rsult = Doc.LoadFamily(sel_fam_file_nm, new FamilyLoadOptions(), out sel_fam0);
                                        //sb.Append(rsult.ToString());

                                        


                                    }

                                }









                            }
                            else
                            {
                                string s1_prm_fmla2 = s_f_prm.Formula;
                                bool s2_prm_inst2 = s_f_prm.IsInstance;

                                StorageType s1_prm_strg_tp2 = s_f_prm.StorageType;

                                Definition s1_df2 = s_f_prm.Definition;
                                ForgeTypeId dt_tp2 = s1_df2.GetDataType();
                                BuiltInParameterGroup sel_grp2 = s1_df2.ParameterGroup;
                                try
                                {
                                    ExternalDefinitionCreationOptions extrnal_df_opt = new ExternalDefinitionCreationOptions(sf_prm_nm, dt_tp2);
                                    extrnal_df_opt.GUID = s1_prm_guid;
                                    foreach (ListViewItem lsv_prm_grp in lstv_shrd_prm_grps.SelectedItems)
                                    {
                                        Definitions n_dfs = lsv_prm_grp.Tag as Definitions;
                                        Definition n_prm_df = n_dfs.Create(extrnal_df_opt);
                                        ExternalDefinition n_ext_prm_df = n_prm_df as ExternalDefinition;
                                        using (Transaction trns2 = new Transaction(sel_f_doc, "add_parameter"))
                                        {
                                            trns2.Start();

                                            try
                                            {
                                                FamilyParameter n_fam_prm = sel_f_mnger.AddParameter(n_ext_prm_df, sel_grp2, s2_prm_inst2);

                                                //sb.Append(n_fam_prm.Definition.Name);
                                                //sb.Append(' ');

                                                if (s1_prm_fmla2 != null && s1_prm_fmla2 != "")
                                                {
                                                    try
                                                    {
                                                        sel_f_mnger.SetFormula(n_fam_prm, s1_prm_fmla2);

                                                    }
                                                    catch (Exception ex)
                                                    {

                                                    }
                                                }

                                            }
                                            catch (Exception ex)
                                            {

                                            }




                                            //bool rsult = Doc.LoadFamily(sel_fam_file_nm, new FamilyLoadOptions(), out sel_fam0);
                                            //sb.Append(rsult.ToString());

                                            trns2.Commit();



                                        }


                                    }

                                }

                                catch (Exception ex)
                                {

                                }
                            }
                        }
                        else if (!s_f_prm.IsShared)
                        {
                            string sf_prm_nm0 = s_f_prm.Definition.Name;
                            //Guid s1_prm_guid = (Guid)s_f_prm.GUID;
                            string s1_prm_fmla = s_f_prm.Formula;
                            bool s2_prm_inst = s_f_prm.IsInstance;

                            StorageType s1_prm_strg_tp = s_f_prm.StorageType;
                            Category f_ct = sel_fam0.FamilyCategory;

                            Definition s1_df_a = s_f_prm.Definition;
                            ForgeTypeId dt_tp_a = s1_df_a.GetDataType();
                            ForgeTypeId grp_tp_a = s1_df_a.GetGroupTypeId();
                            BuiltInParameterGroup sel_grp = s1_df_a.ParameterGroup;

                            using (Transaction trns3 = new Transaction(sel_f_doc, "add_parameter"))
                            {
                                trns3.Start();

                                try
                                {
                                    FamilyParameter n_fam_prm_a = sel_f_mnger.AddParameter(sf_prm_nm0, grp_tp_a, dt_tp_a, s2_prm_inst);

                                    //sb.Append(n_fam_prm.Definition.Name);
                                    //sb.Append(' ');

                                    if (s1_prm_fmla != null && s1_prm_fmla != "")
                                    {
                                        try
                                        {
                                            sel_f_mnger.SetFormula(n_fam_prm_a, s1_prm_fmla);

                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                    }

                                }
                                catch (Exception ex)
                                {

                                }




                                //bool rsult = Doc.LoadFamily(sel_fam_file_nm, new FamilyLoadOptions(), out sel_fam0);
                                //sb.Append(rsult.ToString());

                                trns3.Commit();

                            }
                        }


                    }
                    Family n_fam = sel_f_doc.LoadFamily(Doc, new FamilyLoad_opt());

                }
                
                
                

            }
            tst_labl.Text = sb.ToString();
        }

        private void tst_labl_Click(object sender, EventArgs e)
        {

        }

        private void lbl_shd_fl_nm_Click(object sender, EventArgs e)
        {

        }

        private void lstv_shrd_prm_grps_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbl_fam_trnsfr_Click(object sender, EventArgs e)
        {

        }
    }
}
