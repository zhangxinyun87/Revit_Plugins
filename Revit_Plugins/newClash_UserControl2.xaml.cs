using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Revit_Plugins;
using Autodesk.Revit.DB;
using System.Windows.Interop;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static Revit_Plugins.Test_UserControl1;
using System.Drawing;
using Autodesk.Revit.DB.DirectContext3D;

namespace Revit_Plugins
{
    /// <summary>
    /// Logica di interazione per newClash_UserControl2.xaml
    /// </summary>
    /// 

    
    

    public partial class newClash_UserControl2 : Window
    {
        UIDocument Uidoc;
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
        //List<ClashSet> GetNewSet;

        
        public List<ClashBind> clshbnd {  get; set; } 
        public List<testbind> tstbnd { get; set; }

        public List<ClashSet> GetNewSet { get; set; }
        

        public System.Windows.Forms.ListView create_n_clash_Lstv()
        {
            var lstv = new System.Windows.Forms.ListView
            {
                CheckBoxes = true,
                View = System.Windows.Forms.View.Details,
                FullRowSelect = true,
                MultiSelect = true,
                Dock = System.Windows.Forms.DockStyle.Fill,

            };

            string[] col_headers = new string[] { "Index", "ElementA", "A_Id", "ElementB", "B_Id", "ClashLocation", "DocumentA", "DocumentB", "ClashGuid" };
            int[] col_widths = new int[] { 20, 100, 50, 100, 50, 80, 100, 100, 120 };
            for (int i = 0; i < 9; i++)
            {
                lstv.Columns.Add(new System.Windows.Forms.ColumnHeader(i));
                lstv.Columns[i].Text = col_headers[i];
                lstv.Columns[i].Width = col_widths[i];
            }
            return lstv;

        }

        public System.Windows.Forms.ListView lstv_clsh { get; set; }

        public PerspectiveCamera publicCamera { get; set; }
        public MeshGeometry3D ConvertSolidToMeshGeometry3D(Solid solid)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            Dictionary<XYZ, int> vertexIndexMap = new Dictionary<XYZ, int>(new XYZEqualityComparer());
            foreach (Face face in solid.Faces)
            {
                Mesh faceMesh = face.Triangulate();
                for (int i = 0; i < faceMesh.NumTriangles; i++)
                {
                    MeshTriangle triangle = faceMesh.get_Triangle(i);
                    for (int j = 0; j < 3; j++)
                    {
                        XYZ pt = triangle.get_Vertex(j);
                        if (!vertexIndexMap.ContainsKey(pt))
                        {
                            vertexIndexMap[pt] = mesh.Positions.Count;
                            mesh.Positions.Add(new System.Windows.Media.Media3D.Point3D(pt.X * 0.1, pt.Y * 0.1, pt.Z * 0.1));
                            mesh.Normals.Add(new Vector3D(0, 0, 1));
                        }
                        mesh.TriangleIndices.Add(vertexIndexMap[pt]);
                        //mesh.TextureCoordinates.Add();

                    }
                }
            }
            return mesh;
        }
        public List<MeshGeometry3D> getElemMesh(Element elem)
        {
            Options opt = new Options
            {
                ComputeReferences = true,
                IncludeNonVisibleObjects = false,
                DetailLevel = ViewDetailLevel.Fine,
            };
            List<MeshGeometry3D> lst_msh_3d = new List<MeshGeometry3D>();
            GeometryElement geom_elem = elem.get_Geometry(opt);
            if (geom_elem != null)
            {
                foreach (GeometryObject g_obj in geom_elem)
                {
                    if (g_obj is Solid sld && sld.Faces.Size > 0)
                    {
                        Solid g_sld = (Solid)g_obj;
                        MeshGeometry3D msh_3d = ConvertSolidToMeshGeometry3D(g_sld);

                        lst_msh_3d.Add(msh_3d);

                    }

                }
            }
            return lst_msh_3d;
        }
        public XYZ ItemsTranslate(Element elem)
        {
            XYZ orgin = new XYZ(0,0,0);
            Options opt = new Options
            {
                ComputeReferences = true,
                IncludeNonVisibleObjects = false,
                DetailLevel = ViewDetailLevel.Fine,
            };
            List<MeshGeometry3D> lst_msh_3d = new List<MeshGeometry3D>();
            GeometryElement geom_elem = elem.get_Geometry(opt);
            List<XYZ> lst_xyz = new List<XYZ>();    
            if (geom_elem != null)
            {
                foreach (GeometryObject g_obj in geom_elem)
                {
                    if (g_obj is Solid sld && sld.Faces.Size > 0)
                    {
                        BoundingBoxXYZ sld_bx = sld.GetBoundingBox();
                        Autodesk.Revit.DB.Transform sld_trsfm = sld_bx.Transform;
                        XYZ sld_orgin = sld_trsfm.Origin;
                        lst_xyz.Add(sld_orgin);

                    }
                }
            }
            for (int i = 0;i<lst_xyz.Count;i++)
            {
                orgin.Add(lst_xyz[i]);
            }
            XYZ o_origin = orgin.Divide(lst_xyz.Count);
            return o_origin;
        }
        public class ClashBind
        {
            public string indx { get; set; }
            public int indx_int { get; set; }
            public string elA { get; set; }
            public Element elemA { get; set; }
            public string idA { get; set; }
            public int idA_int { get; set; }
            public string elB { get; set; }
            public Element elemB { get; set; }
            public string idB { get; set; }
            public int idB_int { get; set; }
            public string clsh_loc { get; set; }
            public string docA { get; set; }
            public Document doc_A { get; set; }
            public string docB { get; set; }
            public Document doc_B { get; set; }
            public Guid guid { get; set; }
            public string clsh_guid { get; set; }
        }

        public class testbind
        {
            public string ind { get; set; }
            public string eA { get; set; }
            public string eB { get; set; }
            public string guid { get; set; }
        }

        public List<ModelVisual3D> add3DToViewport(List<MeshGeometry3D> lst_msh, System.Windows.Media.Color colr, System.Windows.Controls.Viewport3D vprt)
        {
            List<ModelVisual3D> lst_modls = new List<ModelVisual3D>();
            for (int i = 0; i < lst_msh.Count; i++)
            {
                MeshGeometry3D n_msh_3d = lst_msh[i];
                System.Windows.Media.Brush n_brsh = new SolidColorBrush(colr);
                n_brsh.Opacity = 1;
                var mat = new DiffuseMaterial(n_brsh);
                GeometryModel3D n_geom_3d = new GeometryModel3D();
                n_geom_3d.Geometry = n_msh_3d;
                n_geom_3d.Material = mat;
                string modlvis_nm = "n_mod_vs_3d_n_" + i.ToString();
                ModelVisual3D n_mdl_vs_3d = new ModelVisual3D
                {
                    Content = n_geom_3d
                };
                lst_modls.Add(n_mdl_vs_3d);
                vprt.Children.Add(n_mdl_vs_3d);

            }
            return lst_modls;
        }

        public static Rect3D getModelsBoundingBox(List<ModelVisual3D> lst_model)
        {
            Rect3D bbox = Rect3D.Empty;
            foreach(var modl in lst_model)
            {
                Rect3D rect3D = modl.Content.Bounds;
                if(modl.Transform != null && modl.Transform.Value.IsIdentity)
                {
                    Rect3D trsfm_rect3D = modl.Transform.TransformBounds(rect3D);
                    bbox.Union(trsfm_rect3D);

                }
                else
                {
                    bbox.Union(rect3D);
                }
            }
            return bbox;

        }
        public static void zoomToFit(PerspectiveCamera camra, List<ModelVisual3D> lst_model, double marginFctor = 0.025)
        {
            Rect3D bnds = getModelsBoundingBox(lst_model);
            Point3D cntr_pt = new Point3D(bnds.X + bnds.SizeX /2, bnds.Y + bnds.SizeY / 2, bnds.Z + bnds.SizeZ / 2);
            camra.LookDirection.Normalize();
            Vector3D lookDir = camra.LookDirection; 
            double max = Math.Max(bnds.SizeX,Math.Max(bnds.SizeY,bnds.SizeZ));
            double dist = max * marginFctor / Math.Tan(camra.FieldOfView * 0.5 * Math.PI / 180);
            camra.Position = cntr_pt- (lookDir * dist);    
            camra.LookDirection = lookDir*dist;

        }

        public newClash_UserControl2(Autodesk.Revit.DB.Document doc, Autodesk.Revit.UI.UIApplication uiapp, List<ClashSet> getNewSet)
        {
            GetNewSet = getNewSet;
            
            Doc = doc;
            Uiapp = uiapp;
            InitializeComponent();

            
            lstv_clsh = create_n_clash_Lstv();
            lstv_clsh.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(lstv_view_clsh_ColumnClick);
            lstv_clsh.SelectedIndexChanged += new System.EventHandler(lstv_view_clsh_ItemChecked);
            clshResult.Child = lstv_clsh;

            PerspectiveCamera myCmera_a = new PerspectiveCamera
            {
                Position = new Point3D(50, 50, 50),
                LookDirection = new Vector3D(-50, -50, -50),
                UpDirection = new Vector3D(0, 0, 1),

                FieldOfView = 45
            };
            publicCamera = myCmera_a;
            vprt3d.Camera = publicCamera;

            if (GetNewSet.Count > 0)
            {
                TaskDialog.Show("count", GetNewSet.FirstOrDefault().elemA.Name + "&" + GetNewSet.FirstOrDefault().elemB.Name);
                
                clshbnd = new List<ClashBind>();
                tstbnd = new List<testbind>();

                for (int i = 0; i < GetNewSet.Count(); i++)
                {
                    ClashSet clsh_set = GetNewSet[i];
                    Element e_a = clsh_set.elemA;
                    Element e_b = clsh_set.elemB;
                    if (e_a != null && e_b != null)
                    {
                        Guid guid_val = new Guid();
                        ClashBind n_bind = new ClashBind
                        {
                            indx_int = i,
                            indx = i.ToString(),
                            elemA = e_a,
                            elA = e_a.Name.ToString(),
                            elemB = e_b,
                            elB = e_b.Name.ToString(),
                            idA_int = e_a.Id.IntegerValue,
                            idB_int = e_b.Id.IntegerValue,
                            idA = e_a.Id.IntegerValue.ToString(),
                            idB = e_b.Id.IntegerValue.ToString(),
                            clsh_loc = "(0,0,0)",
                            docA = e_a.Document.Title,
                            doc_A = e_a.Document,
                            docB = e_b.Document.Title,
                            doc_B = e_b.Document,
                            guid = guid_val,
                            clsh_guid = guid_val.ToString(),
                        };
                        clshbnd.Add(n_bind);
                        
                        string[] clsh_info = { n_bind.indx, n_bind.elA,n_bind.idA,n_bind.elB, n_bind.idB, n_bind.clsh_loc, n_bind.docA, n_bind.docB, n_bind.clsh_guid};
                        System.Windows.Forms.ListViewItem lstv_itm = new System.Windows.Forms.ListViewItem(clsh_info);
                        lstv_itm.Tag = n_bind;
                        lstv_clsh.Items.Add(lstv_itm);
                        
                    }

                }
            
            }
            dtgrd_clshSet.DataContext = clshbnd;

        }
        


        private void dtgrd_clshSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
        }

        private void lstv_view_clsh_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            System.Windows.Forms.ListView lstv_v = clshResult.Child as System.Windows.Forms.ListView;
            lstv_v.ListViewItemSorter = new ListViewItemComparer(e.Column);

        }
        private void lstv_view_clsh_ItemChecked(object sender, EventArgs e)
        {

        }
       
        private void checkClash_Click(object sender, RoutedEventArgs e)
        {
            
            

            Options opt = new Options
            {
                ComputeReferences = true,
                IncludeNonVisibleObjects = false,
                DetailLevel = ViewDetailLevel.Fine,
            };
            XYZ medium_camera_pos_translate = new XYZ(0, 0, 0);
            List<XYZ> lst_translate = new List<XYZ>();  
            List<ModelVisual3D> lst_mdls = new List<ModelVisual3D>();   
            foreach (System.Windows.Forms.ListViewItem sel_itm in lstv_clsh.CheckedItems)
            {
                ClashBind sel_clash_bind = sel_itm.Tag as ClashBind;

                Element elem_A = sel_clash_bind.elemA;
                Element elem_B = sel_clash_bind.elemB;
                List<MeshGeometry3D> lst_msh_A = getElemMesh(elem_A);
                List<MeshGeometry3D> lst_msh_B = getElemMesh(elem_B);
                lst_translate.Add(ItemsTranslate(elem_A));
                lst_translate.Add(ItemsTranslate(elem_B));

                List<ModelVisual3D> lst_mdl_A = add3DToViewport(lst_msh_A, Colors.Red, vprt3d);
                List<ModelVisual3D> lst_mdl_B = add3DToViewport(lst_msh_B, Colors.Green, vprt3d);
                lst_mdls.AddRange(lst_mdl_A);
                lst_mdls.AddRange(lst_mdl_B);

            }

            PerspectiveCamera myCmera_b = new PerspectiveCamera
            {
                Position = new Point3D(50, 50, 50),
                LookDirection = new Vector3D(-50, -50, -50),
                UpDirection = new Vector3D(0, 0, 1),

                FieldOfView = 45
            };

            zoomToFit(myCmera_b, lst_mdls);
            publicCamera = myCmera_b;
            vprt3d.Camera = publicCamera;
            /*
            for (int i = 0; i < lst_translate.Count; i++)
            {
                medium_camera_pos_translate.Add(lst_translate[i]);
            }
            XYZ translt = medium_camera_pos_translate.Divide(lst_translate.Count);
            */

        }

        public RotateTransform3D rot_trsfm_x { get; set; }
        public RotateTransform3D rot_trsfm_y { get; set; }
        public PerspectiveCamera Cmra { get; set; }

        private bool isRotating = false;
        private System.Windows.Point last_pt;
        private void vprt3d_mousedown(object sender, MouseButtonEventArgs e)
        {
            isRotating = true;
            last_pt = e.GetPosition(this);
            Mouse.Capture(vprt3d);

        }
        private void vprt3d_mouseup(object sender, MouseButtonEventArgs e)
        {
            isRotating = false;
            Mouse.Capture(null);
        }
        private void vprt3d_mousewheel(object sender, MouseWheelEventArgs e)
        {
            double dlt = e.Delta > 0 ? 0.9 : 1.1;
            publicCamera.Position = new Point3D(publicCamera.Position.X * dlt, publicCamera.Position.Y * dlt, publicCamera.Position.Z * dlt);


        }
        private void vprt3d_mousemove(object sender,System.Windows.Input.MouseEventArgs e)
        {
            if (isRotating == false) return;
            System.Windows.Point n_pt = e.GetPosition(this);
            double dx = n_pt.X - last_pt.X;
            double dy = n_pt.Y - last_pt.Y;
            ((AxisAngleRotation3D)rot_trsfm_x.Rotation).Angle += dx + 0.5;
            ((AxisAngleRotation3D)rot_trsfm_y.Rotation).Angle += dy + 0.5;
            last_pt = n_pt;

        }

        private void WindowsFormsHost_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
