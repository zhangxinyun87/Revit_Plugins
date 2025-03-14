
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
using Autodesk.Revit.DB.Events;


namespace Revit_Plugins
{

    public partial class temp_test_Form1 : System.Windows.Forms.Form
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
        
        public class FailUI : IFailuresPreprocessor
        {

            public FailureProcessingResult PreprocessFailures(FailuresAccessor fail_acc)
            {
                
                IList<FailureMessageAccessor> fail_lst = new List<FailureMessageAccessor>();
                fail_lst = fail_acc.GetFailureMessages();
                bool has_fail = false;
                foreach (FailureMessageAccessor fail in fail_lst)
                {
                    if (fail.GetFailingElementIds().Count > 0)
                    {
                        has_fail = true;
                        fail_acc.DeleteAllWarnings();
                        fail_acc.DeleteElements(fail.GetFailingElementIds() as IList<ElementId>);
                    }

                }
                if (has_fail)
                {
                    return FailureProcessingResult.ProceedWithCommit;
                }
                
                return FailureProcessingResult.Continue;

            }
        }
        
        public temp_test_Form1(Document doc, UIApplication uiapp)
        {
            InitializeComponent();
            this.Doc = doc;
            this.Uiapp = uiapp;

        }
        class wal_cnct_pair
        {
            public Wall x_wal { get; set; }
            public List<Element> x_lst_cncts { get; set; }
        }

        class dim_data
        {
            public Wall x_wal { get; set; }
            public Line x_crv { get; set; }
            public List<Reference> x_refs { get; set; }
        }

        private List<dim_data> dim_to_create { get; set; }
        public Autodesk.Revit.DB.View act_v {  get; set; }  
        public DimensionType dim_type { get; set; }
        private wal_cnct_pair gt_al_cnct(wal_cnct_pair x, List<wal_cnct_pair> x_lst)
        {
            wal_cnct_pair o = x as wal_cnct_pair;
            
            foreach(wal_cnct_pair x_l in x_lst)
            {
                if (x_l.x_lst_cncts.Count > 0)
                { 
                    List<ElementId> lst_ids = new List<ElementId>();
                    foreach(Element x_xl in x_l.x_lst_cncts)
                    {
                        ElementId x_xl_id = x_xl.Id;
                        lst_ids.Add(x_xl_id);
                    }
                    if (lst_ids.Contains(x.x_wal.Id))
                    {
                        List<ElementId> o_ids = new List<ElementId>();
                        foreach( Element o1 in o.x_lst_cncts)
                        {
                            ElementId o1_id = o1.Id;
                            o_ids.Add(o1_id);
                        }
                        if(!o_ids.Contains(x_l.x_wal.Id))
                        {
                            o.x_lst_cncts.Add(x_l.x_wal);
                            
                        }

                    }
                
                }
            }
            return o;

        }

        private Boolean sm_vct(XYZ x1, XYZ x2, int n)
        {
            Boolean o = false;
            if(Math.Round(x1.X,n) == Math.Round(x2.X,n) && Math.Round(x1.Y,n) == Math.Round(x2.Y,n) && Math.Round(x1.Z, n) == Math.Round(x2.Z, n))
            {
                o = true;
            }
            else
            {
                o = false;  
            }
            return o;   
        }

        private List<Reference> gt_rfs(Wall m_w, Wall x_w)
        {
            Transform trs1 = Transform.CreateRotation(new XYZ(0,0,1),Math.PI/2);
            Transform trs2 = Transform.CreateRotation(new XYZ(1,0,0),Math.PI/2*3);
            XYZ m_w_dir = m_w.Orientation;
            XYZ dir1 = trs1.OfVector(m_w_dir);
            XYZ dir2 = trs2.OfVector(m_w_dir);
            Options opt = new Options();
            opt.DetailLevel = ViewDetailLevel.Fine;
            opt.ComputeReferences = true;
            GeometryElement geom = x_w.get_Geometry(opt);
            List<Reference> al_fcs = new List<Reference>();
            foreach(Solid sld in geom)
            {
                FaceArray fcs = sld.Faces;
                if(fcs.Size > 0 )
                {
                    for(int i = 0; i < fcs.Size; i++)
                    {
                        Face fc = fcs.get_Item(i);
                        Reference fc_rf = fc.Reference;
                        if(fc.GetType() == typeof(PlanarFace))
                        {
                            PlanarFace plnr_fc = fc as PlanarFace;
                            if(sm_vct(plnr_fc.FaceNormal, dir1, 5) || sm_vct(plnr_fc.FaceNormal, dir2, 5))
                            {
                                al_fcs.Add(fc_rf);  
                            }
                        } 
                    }
                }

            }
            return al_fcs;
        }

        public Dimension create_dim(Autodesk.Revit.DB.Document x_dc, Autodesk.Revit.DB.View x_v, Line x_crv, List<Reference> x_rfs, DimensionType x_d_tp)
        {
           
            //Line x_ln = x_crv as Line;
            ReferenceArray x_rf_arr = new ReferenceArray();
            if(x_rfs.Count > 0)
            {
                foreach (Reference x_rf in x_rfs)
                {
                    x_rf_arr.Append(x_rf);
                }
            }

            Dimension x_dim = x_dc.Create.NewDimension(x_v, x_crv, x_rf_arr,x_d_tp);
             
            return x_dim;

        }

        private void temp_test_Form1_Load(object sender, EventArgs e)
        {
            //ElementId es_v_id = new ElementId(17190396);
            //Element es_v_elem = Doc.GetElement(es_v_id);
            //Autodesk.Revit.DB.View es_v =  es_v_elem as Autodesk.Revit.DB.View;
            Autodesk.Revit.DB.View es_v = Doc.ActiveView;
            act_v = es_v;
            List<Element> dim_tp_lst = new FilteredElementCollector(Doc).OfClass(typeof(DimensionType)).ToElements().ToList();
            List<DimensionType> ln_dim_lst = new List<DimensionType>();
            foreach(Element dim_elem in dim_tp_lst)
            {
                DimensionType d_tp = dim_elem as DimensionType;
                if(d_tp.StyleType == DimensionStyleType.Linear)
                {
                    ln_dim_lst.Add(d_tp);
                } 
                
            }
            //DimensionType dim_tp = ln_dim_lst.FirstOrDefault();
            //Element dim_tp_elem = Doc.GetElement(new ElementId(7691969));
            //Element dim_tp_elem = Doc.GetElement(new ElementId(140332));
            Element dim_tp_elem = Doc.GetElement(new ElementId(7092));
            DimensionType dim_tp = dim_tp_elem as DimensionType;
            label3.Text = dim_tp.Name.ToString() + "-" + dim_tp.Id.ToString();
            dim_type = dim_tp;
            List<Element> al_wals = new FilteredElementCollector(Doc,es_v.Id).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType().ToElements().ToList();
            ElementCategoryFilter wal_fltr = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            //List<Element> al_cncts = new List<Element>();
            List<wal_cnct_pair> al_cncts = new List<wal_cnct_pair>();

            foreach (Element el in al_wals) 
            {
                Wall wal = el as Wall;
                ElementId w_tp_id = el.GetTypeId();
                Element w_tp_elem = Doc.GetElement(w_tp_id); 
                WallType w_tp = w_tp_elem as WallType;
                //List<ElementArray> al_cncts = new List<ElementArray>();
                List<Element> cncts = new List<Element>();
                if (w_tp.Kind == WallKind.Basic)
                {
                    LocationCurve l_crv = wal.Location as LocationCurve;
                    ElementArray cnctcs_a = l_crv.get_ElementsAtJoin(0);
                    ElementArray cnctcs_b = l_crv.get_ElementsAtJoin(1);
                    if(cnctcs_a != null && cnctcs_a.Size > 0)
                    {
                        for (int i = 0; i < cnctcs_a.Size; i++)
                        {
                            Element cnct_a = cnctcs_a.get_Item(i) as Element;
                            if (wal_fltr.PassesFilter(cnct_a))
                            {
                                cncts.Add(cnct_a);
                            }
                            

                        }
                    }

                    if (cnctcs_b != null && cnctcs_b.Size > 0)
                    {
                        for (int i = 0; i < cnctcs_b.Size; i++)
                        {
                            Element cnct_b = cnctcs_b.get_Item(i) as Element;
                            if (wal_fltr.PassesFilter(cnct_b))
                            {
                                cncts.Add(cnct_b);
                            }


                        }
                    }

                }
                wal_cnct_pair n_wal_cnct = new wal_cnct_pair(); 
                n_wal_cnct.x_wal = wal;
                n_wal_cnct.x_lst_cncts = cncts;
                al_cncts.Add(n_wal_cnct);


            }

            List<wal_cnct_pair> al_cnct_out = new List<wal_cnct_pair>();
            foreach(wal_cnct_pair w in al_cncts)
            {
                wal_cnct_pair n_w = gt_al_cnct(w,al_cncts) as wal_cnct_pair;
                al_cnct_out.Add(n_w);
            }
            label1.Text = al_cnct_out.Count.ToString();
            double ft_cm = 30.48;
            double dst = 45 / ft_cm;
            List<dim_data> dim_Datas = new List<dim_data>();
            foreach(wal_cnct_pair lst in al_cnct_out)
            {
                Wall wal = lst.x_wal;
                LocationCurve loc = wal.Location as LocationCurve;
                Curve loc_crv = loc.Curve as Curve;
                Line crv0 = Line.CreateBound(loc_crv.GetEndPoint(0), loc_crv.GetEndPoint(1));
                Transform trsfm_vct = Transform.CreateRotation(new XYZ(0,0,1), System.Math.PI);
                XYZ vct = trsfm_vct.OfVector(loc_crv.GetEndPoint(1).Subtract(loc_crv.GetEndPoint(0)).Divide(crv0.Length));
                Transform trsfm_a = Transform.CreateTranslation(vct.Multiply(dst));
                Line crv = crv0.CreateTransformed(trsfm_a) as Line;
                
                List<Element> othrs = lst.x_lst_cncts;
                IList<ElementId> insrts_ids = wal.FindInserts(true,false,true,false);

                List<Reference> refs = new List<Reference>();
                List<Reference> wal_rfs = gt_rfs(wal, wal);

                foreach( Reference w_rf in wal_rfs)
                {
                    if (w_rf != null)
                    {
                        refs.Add(w_rf);

                    }
                }

                if(othrs != null && othrs.Count > 0)
                {
                    foreach(Element othr in othrs)
                    {
                        Wall othr_w = othr as Wall;
                        List<Reference> oth_rfs = gt_rfs(wal, othr_w);
                        if(oth_rfs != null)
                        {
                            foreach(Reference oth_rf in oth_rfs)
                            {
                                if(oth_rf != null)
                                {
                                    refs.Add((Reference) oth_rf);
                                }
                            }
                        }

                    }
                }

                if(insrts_ids != null &&  insrts_ids.Count > 0)
                {
                    foreach(ElementId in_id in insrts_ids)
                    {
                        Element itm = Doc.GetElement(in_id);
                        ElementClassFilter fam_inst_fltr = new ElementClassFilter(typeof(FamilyInstance));
                        if (fam_inst_fltr.PassesFilter(itm))
                        {
                            FamilyInstance fam_itm = (FamilyInstance)itm;   
                            IList<Reference> itm_rfs = fam_itm.GetReferences(FamilyInstanceReferenceType.CenterLeftRight);
                            foreach(Reference itm_rf in itm_rfs)
                            {
                                refs.Add(itm_rf);
                            }
                            
                        }
                    }
                }

                dim_data dim_dt = new dim_data();
                dim_dt.x_wal = wal;
                dim_dt.x_crv = crv;
                dim_dt.x_refs = refs;
                dim_Datas.Add(dim_dt);

            }
            dim_to_create = dim_Datas;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            List<Dimension> new_dims = new List<Dimension>();
            Transaction trns = new Transaction(Doc, "create_new_linear_dimension");
            FailureHandlingOptions fail_opt = trns.GetFailureHandlingOptions();
            IFailuresPreprocessor failures = new FailUI();
            fail_opt.SetFailuresPreprocessor(failures);
            trns.SetFailureHandlingOptions(fail_opt);
            trns.Start();

            foreach (dim_data dim_dt in dim_to_create)
            {
                Wall n_w = dim_dt.x_wal;
                Line n_crv = dim_dt.x_crv;
                List<Reference> n_rfs = dim_dt.x_refs;
                
                //Dimension n_dim = create_dim(Doc, act_v, n_crv, n_rfs, dim_type);
                //new_dims.Add(n_dim);
                if(n_rfs.Count > 1)
                {
                    ReferenceArray n_rf_arr = new ReferenceArray();
                    foreach (Reference n_rf in n_rfs)
                    {
                        n_rf_arr.Append(n_rf);
                        string[] refs_info = { n_w.Id.ToString(), n_rf.ElementReferenceType.ToString(), Doc.GetElement(n_rf.ElementId).Name.ToString() };
                        ListViewItem lstv_chck1 = new ListViewItem(refs_info);
                        lstv_chck1.Tag = n_rf;
                        listView1.Items.Add(lstv_chck1);
                    }
                    Dimension n_dim = Doc.Create.NewDimension(act_v, n_crv, n_rf_arr);
                    //Dimension n_dim = create_dim(Doc, act_v, n_crv, n_rfs, dim_type);
                    new_dims.Add(n_dim);

                    /*
                    try
                    {
                        Dimension n_dim = Doc.Create.NewDimension(act_v, n_crv, n_rf_arr);
                        //Dimension n_dim = create_dim(Doc, act_v, n_crv, n_rfs, dim_type);
                        new_dims.Add(n_dim);
                    }

                    catch (Exception ex)
                    {

                    }
                    */

                }


            }

            Doc.Regenerate();
            trns.Commit();

            
            label2.Text = new_dims.Count.ToString();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstv1_columnclick(object sender, ColumnClickEventArgs e)
        {
            this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        
    }
}