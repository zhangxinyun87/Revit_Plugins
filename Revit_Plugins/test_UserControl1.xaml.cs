using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revit_Plugins.Properties;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Revit_Plugins
{
    /// <summary>
    /// Logica di interazione per Test_UserControl1.xaml
    /// </summary>
    /// 
    public class fillPatInfo
    {
        public string nm { get; set; }
        public FillPatternElement fillPat { get; set; }
    }
    
    public class FillPatternBrushConverter : IValueConverter

    {
        public DrawingBrush ConvertFillGridToBrush(FillPatternElement fillPat_e)
        {
            FillPattern fillPat = fillPat_e.GetFillPattern();
            IList<FillGrid> lst_fil_grds = fillPat.GetFillGrids(); 
            DrawingGroup dr_grp_A = new DrawingGroup();
            foreach(FillGrid fil_grd in lst_fil_grds)
            {
                UV orgin = fil_grd.Origin;
                double angl = fil_grd.Angle;
                double ofst = fil_grd.Offset;
                double shft = fil_grd.Shift;

                double wth = 20;
                double ht = 20;

                Vector dir = new Vector(Math.Cos(angl), Math.Sin(angl));
                Vector orth = new Vector(-dir.Y, dir.X);
                System.Windows.Point orig = new System.Windows.Point(0,0);
                var segmnts = fil_grd.GetSegments();
                GeometryGroup lns = new GeometryGroup();
                for (int i = -10; i < 10;  i++)
                {
                    Vector mv = orth *(i*ofst + ((i%2 == 0) ? 0 : shft));
                    System.Windows.Point strt = new System.Windows.Point(0,0) + mv;
                    //System.Windows.Point end = new System.Windows.Point(wth, ht) + mv;
                    System.Windows.Point end = strt+dir*40;

                    lns.Children.Add(new LineGeometry(strt, end));

                }
                dr_grp_A.Children.Add(new GeometryDrawing
                {
                    Brush = Brushes.Transparent,
                    Pen =  new Pen(Brushes.Black,0.5),
                    Geometry = lns, 

                });
                

                
            }
            DrawingBrush brush = new DrawingBrush(dr_grp_A)
            {
                TileMode = TileMode.Tile,
                Viewport = new Rect(0, 0, 10, 10),
                ViewportUnits = BrushMappingMode.Absolute,
                Stretch = Stretch.None
            };
            return brush;
        }

        public DrawingBrush GetFillGridToBrush(FillPatternElement fillPat_e)
        {
            FillPattern fillPat = fillPat_e.GetFillPattern();
            IList<FillGrid> lst_fil_grds = fillPat.GetFillGrids();
            DrawingGroup dr_grp_A = new DrawingGroup();
            foreach (FillGrid fil_grd in lst_fil_grds)
            {
                UV orgin = fil_grd.Origin;
                double angl = fil_grd.Angle;
                double ofst = fil_grd.Offset;
                double shft = fil_grd.Shift;

                double wth = 20;
                double ht = 20;

                Vector dir = new Vector(Math.Cos(angl), Math.Sin(angl));
                Vector orth = new Vector(-dir.Y, dir.X);
                System.Windows.Point orig = new System.Windows.Point(0, 0);
                var segmnts = fil_grd.GetSegments();
                GeometryGroup lns = new GeometryGroup();
                for (int i = -10; i < 10; i++)
                {
                    Vector mv = orth * (i * ofst + ((i % 2 == 0) ? 0 : shft));
                    System.Windows.Point strt = new System.Windows.Point(0, 0) + mv;
                    //System.Windows.Point end = new System.Windows.Point(wth, ht) + mv;
                    System.Windows.Point end = strt + dir * 40;
                    double start = 0;
                    for(int j = 0; j < segmnts.Count; j++)
                    {
                        double segmnt_lnth = segmnts[j];
                        System.Windows.Point pt1 = strt + dir * start;
                        System.Windows.Point pt2 = strt + dir * (start + segmnt_lnth);
                        dr_grp_A.Children.Add(new GeometryDrawing
                        {
                            Geometry = new LineGeometry(pt1,pt2),
                            Pen = new Pen(Brushes.Black,0.3)
                        });
                        start += segmnt_lnth;   

                    }
                    

                    lns.Children.Add(new LineGeometry(strt, end));

                }
                dr_grp_A.Children.Add(new GeometryDrawing
                {
                    Brush = Brushes.Transparent,
                    Pen = new Pen(Brushes.Black, 0.3),
                    Geometry = lns,

                });


            }

        
            DrawingBrush brush = new DrawingBrush(dr_grp_A)
            {
                TileMode = TileMode.Tile,
                Viewport = new Rect(0, 0, 10, 10),
                ViewportUnits = BrushMappingMode.Absolute,
                Stretch = Stretch.None
            };
            return brush;
        }



        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            fillPatInfo filPatInfo = (fillPatInfo)value;
            FillPatternElement fp = filPatInfo.fillPat as FillPatternElement;
            var pattern = fp.GetFillPattern();
            if (fp == null)
            {
                return System.Windows.Media.Brushes.Transparent;

            }


            else if (pattern.IsSolidFill)
            {
                return System.Windows.Media.Brushes.Black;

            }
            else
            {
                // Simulate a hatch with diagonal lines

                //DrawingBrush brush = new DrawingBrush();
                DrawingBrush brush_A = GetFillGridToBrush(fp);
                /*
                //GeometryGroup lns = new GeometryGroup();
                GeometryDrawing draw = new GeometryDrawing();

                GeometryDrawing drawing = new GeometryDrawing
                {

                    Brush = Brushes.Transparent,

                    Pen = new Pen(Brushes.Black, 1),

                    Geometry = new GeometryGroup
                    {

                        Children = new GeometryCollection

                        {

                            new LineGeometry(new System.Windows.Point(0, 10), new System.Windows.Point(10, 0)),

                            new LineGeometry(new  System.Windows.Point(0, 0), new  System.Windows.Point(10, 10)),

                        }

                    }

                };

                brush.Drawing = drawing;

                brush.TileMode = TileMode.Tile;

                brush.Viewport = new Rect(0, 0, 10, 10);

                brush.ViewportUnits = BrushMappingMode.Absolute;

                brush.Freeze();
                */

                return brush_A;

            }

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => throw new NotImplementedException();
    }
    /*
    public interface IFillPatternBrushConverter : IValueConverter
    {

    }
    */
    public partial class Test_UserControl1 : Window
    {
        UIDocument Uidoc;
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;

        /*
        public class DFillPatternBrushConverter : IFillPatternBrushConverter
        {

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return Brushes.Gray;
            }
            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => throw new NotImplementedException();
        }
        */
        

        public class eleminfo
        {
            public string nm { get; set; }
            public int id { get; set; }

        }
        



        public ObservableCollection<optItems> lst_Optitms { get; set; }
        public class optItems
        {
            public string key { get; set; }
            public int val_id { get; set; }
            public WallType select_Type {  get; set; }  
            public List<WallType> available_Types { get; set; }  
        }
        //public ObservableCollection<fillPatInfo> lst_Pats { get; set; }
        
        public List<fillPatInfo> n_fillPats { get; set; }
        public ObservableCollection<fillPatInfo> nn_fillPats { get; set; }
        public fillPatInfo sel_fillPat { get; set; }

        public class XYZEqualityComparer : IEqualityComparer<XYZ>
        {
            private const double Tolerance = 1e-6;
            public bool Equals(XYZ a, XYZ b)
            {
                return a.IsAlmostEqualTo(b);
            }
            public int GetHashCode(XYZ obj)
            {
                return obj.X.GetHashCode() ^ obj.Y.GetHashCode() ^ obj.Z.GetHashCode();
            }
        }

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
                            mesh.Positions.Add(new System.Windows.Media.Media3D.Point3D(pt.X*0.1, pt.Y*0.1, pt.Z*0.1));
                            mesh.Normals.Add(new Vector3D(0,0,1));  
                        }
                        mesh.TriangleIndices.Add(vertexIndexMap[pt]);
                        //mesh.TextureCoordinates.Add();

                    }
                }
            }
            return mesh;
        }


        public RotateTransform3D rot_trsfm_x { get;set; }
        public RotateTransform3D rot_trsfm_y { get; set; }
        public PerspectiveCamera Cmra { get;set; }
        //public OrthographicCamera Cmra { get; set; }


        public Test_UserControl1(Autodesk.Revit.DB.Document doc, Autodesk.Revit.UI.UIApplication uiapp)
        {
            Resources.Add("FillPatternBrushConverter", new FillPatternBrushConverter());    
           
            InitializeComponent();
            
            Doc = doc;
            Uiapp = uiapp;
            // test for walltypes;
            List<Element> test_elems = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType().ToElements().ToList();
            List<WallType> tst_wal_tps = new List<WallType>();
           
            lst_Optitms = new ObservableCollection<optItems>();
           
            string[] opt_arr = new string[test_elems.Count];
            foreach (Element tst_e in test_elems)
            {
                eleminfo n_info = new eleminfo();
                n_info.nm = tst_e.Name.ToString();
                n_info.id = tst_e.Id.IntegerValue;
                lstview1.Items.Add(n_info);
                WallType tst_w_tp = tst_e as WallType;
                tst_wal_tps.Add(tst_w_tp);

            }
            for (int i = 0; i < test_elems.Count; i++)
            {
                lst_Optitms.Add(new optItems
                {
                    key = $"Placeholder {i}",
                    val_id = i+100,
                    available_Types = tst_wal_tps,
                    select_Type = tst_wal_tps.FirstOrDefault()
                });
            }

            dt_grdv_01.ItemsSource = lst_Optitms;

            //test for fillpatterns
            List<Element> lst_elems = new FilteredElementCollector(Doc).OfClass(typeof(FillPatternElement)).ToElements().ToList();
            List<fillPatInfo> lst_fill_pats = new List<fillPatInfo>();
            List<FillPatternElement> fill_pats = new List<FillPatternElement>();
            foreach (Element t in lst_elems)
            {
                FillPatternElement e = t as FillPatternElement;
                if (e.GetFillPattern().Target == FillPatternTarget.Drafting)
                {
                    
                    fill_pats.Add(e);
                    fillPatInfo n_pat_info = new fillPatInfo
                    {
                        nm = e.GetFillPattern().Name,
                        fillPat = e,

                    };
                    lst_fill_pats.Add(n_pat_info);
                   

                }
                
            }
            n_fillPats = lst_fill_pats;
            fillPatCombx.ItemsSource = lst_fill_pats;
            fillPatCombx.SelectedIndex = 0;
            cmboxA.ItemsSource = lst_fill_pats;
            cmboxA.SelectedIndex = 0;
            
            sel_fillPat = lst_fill_pats.FirstOrDefault();
            //DataContext = this;
            
            //System.Windows.Controls.Viewport3D v_prt_3d = new System.Windows.Controls.Viewport3D();
            View actv_v = Doc.ActiveView as View;
            List<Element> crnt_v_elems = new FilteredElementCollector(Doc, actv_v.Id).WhereElementIsNotElementType().ToElements().ToList();

            Options opt = new Options 
            { 
                ComputeReferences = true,
                IncludeNonVisibleObjects = false,
                DetailLevel =  ViewDetailLevel.Fine,
            };

            List<MeshGeometry3D> lst_msh_3d = new List<MeshGeometry3D>();   
            foreach(Element elem in crnt_v_elems)
            {
                GeometryElement geom_elem = elem.get_Geometry(opt);
                if (geom_elem != null)
                {
                    foreach(GeometryObject g_obj in geom_elem)
                    {
                        if(g_obj is Solid sld && sld.Faces.Size > 0)
                        {
                            Solid g_sld = (Solid)g_obj; 
                            MeshGeometry3D msh_3d = ConvertSolidToMeshGeometry3D(g_sld);

                            lst_msh_3d.Add(msh_3d);
                            
                        }

                    }
                }
            }
            // new viewport3D transform
            Transform3DGroup trsfm_grp = new Transform3DGroup();
            TranslateTransform3D pan_trsfm = new TranslateTransform3D();
            RotateTransform3D rot_trsfm_X =  new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1,0,0),0));
            RotateTransform3D rot_trsfm_Y = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0),0));
            rot_trsfm_x = rot_trsfm_X;
            rot_trsfm_y = rot_trsfm_Y;
            trsfm_grp.Children.Add(pan_trsfm);
            trsfm_grp.Children.Add(rot_trsfm_X);
            trsfm_grp.Children.Add(rot_trsfm_Y);
            View3D actv_3d_v = (View3D)Doc.ActiveView;
            XYZ eye_pos = actv_3d_v.GetOrientation().EyePosition;
            XYZ forwd_dir = actv_3d_v.GetOrientation().ForwardDirection;
            XYZ up_dir = actv_3d_v.GetOrientation().UpDirection;
            XYZ trgt_dir = eye_pos.Add(forwd_dir.Multiply(10));

            double scle = 0.1;
            PerspectiveCamera cmra = new PerspectiveCamera
            {
                //Position = new Point3D(50,50,50), 
                //LookDirection = new Vector3D(-50,-50,-50),
                //UpDirection = new Vector3D(0,0,1),
                Position = new Point3D(eye_pos.X*scle,eye_pos.Y*scle,eye_pos.Z*scle),
                LookDirection = new Vector3D(forwd_dir.X*scle , forwd_dir.Y * scle, forwd_dir.Z * scle) ,
                UpDirection = new Vector3D(up_dir.X, up_dir.Y, up_dir.Z),
                //Width = 45
                FieldOfView = 45
            };
            Cmra = cmra;
            Cmra.Transform = trsfm_grp;
           
            vprt3d.Camera = Cmra;
           
            for (int i = 0; i < lst_msh_3d.Count; i++)
            {
                MeshGeometry3D n_msh_3d =lst_msh_3d[i];
                Brush n_brsh = new SolidColorBrush(Colors.Red);
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
                vprt3d.Children.Add(n_mdl_vs_3d);
                
            }

            //TaskDialog.Show("count 3dMshes:", lst_msh_3d.Count().ToString());

            ///test geoemtry--
            Brush tst_brsh = new SolidColorBrush(Colors.Azure);
            tst_brsh.Opacity = 1;
            var tst_mat = new DiffuseMaterial(tst_brsh);

            MeshGeometry3D tst_msh_geom = new MeshGeometry3D();
            tst_msh_geom.Positions.Add(new System.Windows.Media.Media3D.Point3D(-10,-10,0));
            tst_msh_geom.Positions.Add(new System.Windows.Media.Media3D.Point3D(10, -10, 0));
            tst_msh_geom.Positions.Add(new System.Windows.Media.Media3D.Point3D(-10, 10, 0));
            tst_msh_geom.Positions.Add(new System.Windows.Media.Media3D.Point3D(10, 10, 0));
            for(int i = 0; i<4; i++)
            {
                tst_msh_geom.Normals.Add(new Vector3D(0, 0, 1));
            }

            tst_msh_geom.TriangleIndices = new Int32Collection
            {
                0,1,2,
                1,3,2
            };

            tst_msh_geom.TextureCoordinates = new PointCollection
            {
                new System.Windows.Point(0,1),
                new System.Windows.Point(1,1),
                new System.Windows.Point(0,0),
                new System.Windows.Point(1,0),
            };
            TranslateTransform3D tst_trsnfom = new TranslateTransform3D(2,0,15);

            GeometryModel3D tst_geom = new GeometryModel3D();
            tst_geom.Geometry = tst_msh_geom;
            tst_geom.Material = tst_mat;
            tst_geom.Transform = tst_trsnfom;   
            ModelVisual3D tst_mdl_vs_3d = new ModelVisual3D
            {
                Content = tst_geom
            };
            vprt3d.Children.Add(tst_mdl_vs_3d);
            ///--test geoemtry


        }



        private void Test_UserControl1_Load(object sender, RoutedEventArgs e)
        {
            

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void Combox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox combo = sender as System.Windows.Controls.ComboBox;
            if(combo?.DataContext is optItems select)
            {
                WallType sel_tp = combo.SelectedItem as WallType;
                TaskDialog.Show("select_type",sel_tp.Name);
            }

        }
        public void cmboxA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox combo = sender as System.Windows.Controls.ComboBox;
            if (combo?.DataContext is optItems select)
            {
                fillPatInfo sel_tp = combo.SelectedItem as fillPatInfo;
                TaskDialog.Show("select_type", sel_tp.nm);
            }

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void windows_load(object sender, RoutedEventArgs e)
        {
            

        }

        private void Window_1_Activated(object sender, EventArgs e)
        {
            
        }

        private void fillPatCombx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox combo = sender as System.Windows.Controls.ComboBox;
            if (combo?.DataContext is optItems select)
            {
                fillPatInfo sel_tp = combo.SelectedItem as fillPatInfo;
                TaskDialog.Show("select_type", sel_tp.nm);
            }
        }
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
            Cmra.Position =  new Point3D(Cmra.Position.X * dlt, Cmra.Position.Y * dlt, Cmra.Position.Z * dlt);


        }
        private void vprt3d_mousemove(object sender, MouseEventArgs e)
        {
            if (isRotating == false) return;
            System.Windows.Point n_pt = e.GetPosition(this);
            double dx = n_pt.X - last_pt.X;
            double dy = n_pt.Y - last_pt.Y;
            ((AxisAngleRotation3D)rot_trsfm_x.Rotation).Angle += dx + 0.5;
            ((AxisAngleRotation3D)rot_trsfm_y.Rotation).Angle += dy + 0.5;
            last_pt = n_pt;



        }
    }
}
