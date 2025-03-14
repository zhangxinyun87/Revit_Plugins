using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Revit_Plugins;
//using static Revit_Plugins.item_mappatura_Form1;

namespace Revit_Plugins
{
    public class ClashSet
    {
        public Element elemA { get; set; }
        public Element elemB { get; set; }
    }

    public class ListViewItemComparer : IComparer
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

    /// <summary>
    /// Logica di interazione per newClash_UserControl1.xaml
    /// </summary>
    public partial class newClash_UserControl1 : Window
    {
        UIDocument Uidoc;
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
        //doc_info is eliminatble
        public class doc_info
        {
            public Document s_doc { get; set; }
            public string s_doc_nm { get; set; }
            public bool is_lnkd { get; set; }
            public string rvt_pth { get; set; }
        }
        public List<doc_info> selDoc_a { get; set; }
        public List<doc_info> selDoc_b { get; set; }

        /*
        public class MyItem : INotifyPropertyChanged
        {
            private bool _isSelected;
            public bool IsSelected
            {
                get => _isSelected;
                set { _isSelected = value; OnPropertyChanged(); }
            }

            private bool _isRowSelected;
            public bool IsRowSelected
            {
                get => _isRowSelected;
                set { _isRowSelected = value; OnPropertyChanged(); }
            }
            public doc_info s_doc { get;set; }
            //public string Name { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null)
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
        public class MyItem 
        {
            
            public bool IsSelected {  get; set; }   
            
            public doc_info s_doc { get; set; }
            
            
        }
        */
        
        //Eliminate MyItem
        public class MyItem : INotifyPropertyChanged
        {
            private bool _isChecked;
            public bool IsChecked
            {
                get => _isChecked;
                set { _isChecked = value; OnPropertyChanged(); }
            }


            public doc_info s_doc { get; set; }
            //public string Name { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null)
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        /*
        private void MyListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedElement = e.OriginalSource as DependencyObject;
            // See if this is a checkbox click
            var checkbox = FindParent<CheckBox>(clickedElement);
            if (checkbox != null && checkbox.DataContext is MyItem clickedItem)
            {
                bool newState = !clickedItem.IsChecked;
                // Copy to all selected items
                var selectedItems = lstv_docsA.SelectedItems.Cast<MyItem>().ToList();
                foreach (var item in selectedItems)
                {
                    item.IsChecked = newState;
                }
                // Stop default toggle behavior
                e.Handled = true;
            }
        }
        */
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            while (child != null && !(child is T))
                child = VisualTreeHelper.GetParent(child);
            return child as T;
        }


        /*
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is CheckBox chck_bx)) return;
            if(!(chck_bx.DataContext is MyItem clck_itm)) return;

            //var chck_bx = sender as CheckBox;
            //var clck_itm = chck_bx.DataContext as MyItem;
            bool is_chck = chck_bx.IsChecked == true;

            var sel_itms = lstv_docsA.SelectedItems.Cast<MyItem>().ToList();    
            if(sel_itms.Count > 1)
            {
                foreach(var itm in sel_itms)
                {
                    itm.IsSelected = is_chck;
                }
                lstv_docsA.ItemsSource = null;
                lstv_docsA.ItemsSource = ItemsA;
            }
            else
            {
                clck_itm.IsSelected = is_chck;
                lstv_docsA.ItemsSource = null;
                lstv_docsA.ItemsSource = ItemsA;
            }

            /
            foreach(var itm in ItemsA.Where(i => i.IsRowSelected))
            {
                itm.IsSelected = is_chck;   
            }
            /
            
            
            
        }*/

        public System.Windows.Forms.ListView lstvA { get; set; }
        public System.Windows.Forms.ListView lstvB { get; set; }   

        /*
        int? _lastCheckIndexA = null;
        private void CheckBoxA_Click(object sender, RoutedEventArgs e)
        {
            var chck_bx = sender as CheckBox;
            var clck_itm = chck_bx.DataContext as MyItem;
            var crnt_indx = ItemsA.IndexOf(clck_itm);
            
            if(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                int min = Math.Min(_lastCheckIndexA.Value, crnt_indx);
                int max = Math.Max(_lastCheckIndexA.Value, crnt_indx);
                bool is_chck = chck_bx.IsChecked == true;

                for(int i = min; i <= max; i++)
                {
                    ItemsA[i].IsSelected = is_chck;
                }
            }
            

            _lastCheckIndexA = crnt_indx;

        }
        int? _lastCheckIndexB = null;
        private void CheckBoxB_Click(object sender, RoutedEventArgs e)
        {
            var chck_bx = sender as CheckBox;
            var clck_itm = chck_bx.DataContext as MyItem;
            var crnt_indx = ItemsB.IndexOf(clck_itm);
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                int min = Math.Min(_lastCheckIndexB.Value, crnt_indx);
                int max = Math.Max(_lastCheckIndexB.Value, crnt_indx);
                bool is_chck = chck_bx.IsChecked == true;

                for (int i = min; i <= max; i++)
                {
                    ItemsB[i].IsSelected = is_chck;
                }
            }

            _lastCheckIndexB = crnt_indx;

        }
        */
        public ObservableCollection<MyItem> ItemsA { get; set; }
        public ObservableCollection<MyItem> ItemsB { get; set; }
        public ObservableCollection<doc_info> DocsA = new ObservableCollection<doc_info>();
        public System.Windows.Forms.ListView create_n_Lstv()
        {
            var lstv = new System.Windows.Forms.ListView
            {
                CheckBoxes = true,
                View = System.Windows.Forms.View.Details,
                FullRowSelect = true,
                MultiSelect = true,
                Dock = System.Windows.Forms.DockStyle.Fill,

            };

            string[] col_headers = new string[] { "FileName", "IsLinkFile", "PathName" };
            int[] col_widths = new int[] { 100, 50, 100 };
            for (int i = 0; i < 3; i++)
            {
                lstv.Columns.Add(new System.Windows.Forms.ColumnHeader(i));
                lstv.Columns[i].Text = col_headers[i];
                lstv.Columns[i].Width = col_widths[i];
            }
            return lstv;

        }
        public class prmDoc
        {
            public ElementId elem_id { get; set; }
            public Document prm_doc { get; set; }
            public string doc_nm { get; set; }
            public string prm_nm { get; set; }
        }

        public List<prmDoc> findCommonItems(List<List<prmDoc>> list)
        {
            List<ElementId> out_lst = new List<ElementId>();
            List<prmDoc> out_prmdoc_lst = new List<prmDoc>();
            List<List<ElementId>> lst_lst_e_ids = new List<List<ElementId>>();

            foreach (List<prmDoc> lst_p_docs in list)
            {
                List<ElementId> lst_ids = lst_p_docs.ConvertAll(p_doc => p_doc.elem_id).ToList();
                lst_lst_e_ids.Add(lst_ids);
            }
            foreach (List<prmDoc> lst_p_docs in list)
            {
                if (lst_p_docs.Count > 0)
                {
                    foreach (prmDoc prm_d in lst_p_docs)
                    {
                        ElementId id = prm_d.elem_id;
                        List<bool> lst_bool = new List<bool>();
                        foreach (List<ElementId> a_lst in lst_lst_e_ids)
                        {
                            lst_bool.Add(a_lst.Contains(id));
                        }
                        if (lst_bool.All(i => i == true) && !out_lst.Contains(id))
                        {
                            out_lst.Add(id);
                            out_prmdoc_lst.Add(prm_d);

                        }
                    }
                }

            }
            return out_prmdoc_lst;
        }

        public List<prmDoc> prm_docsA { get; set; }
        public List<prmDoc> prm_docsB { get; set; }
        public List<Element> getAllModelElements(Document x_doc)
        {
            List<Element> out_lst = new List<Element>();
            BuiltInCategory[] esclud_cats = { BuiltInCategory.OST_Sheets, BuiltInCategory.OST_Rooms };

            List<Element> all_elems = new FilteredElementCollector(x_doc).WhereElementIsNotElementType().WhereElementIsViewIndependent().Where(e => e.Category != null).Where(e => e.Category.CategoryType == CategoryType.Model && !e.Category.IsTagCategory && !(e is Material) && !(e is AppearanceAssetElement)).ToList();
            foreach (Element elem in all_elems)
            {
                if (!esclud_cats.Contains(elem.Category.BuiltInCategory) && elem.GetType().FullName != "Autodesk.Revit.DB.ModelLine" && elem.GetType().FullName != "Autodesk.Revit.DB.CurtainGridLine")
                {
                    BoundingBoxXYZ bnd_box = elem.get_BoundingBox(null);
                    if (bnd_box != null)
                    {
                        double diff = bnd_box.Max.Z - bnd_box.Min.Z;
                        if (diff > 1e-6)
                        {
                            out_lst.Add(elem);

                        }
                    }

                }

            }
            return out_lst;
        }

        public class prmInfo
        {
            public Document prm_doc { get; set; }
            public Parameter prm { get; set; }
            public string prm_nm { get; set; }
            public int prm_id_val { get; set; }
        }
        public class prmValInfo
        {
            public string prm_key { get; set; }
            public string prm_str_val { get; set; }
            public int prm_int_val { get; set; }
            public double prm_double_val { get; set; }

        }

        public prmValInfo getParamVal(Parameter p)
        {
            prmValInfo prmVal = new prmValInfo();
            if (p.HasValue)
            {
                if (p.StorageType == StorageType.String)
                {
                    prmVal.prm_str_val = p.AsString();
                    prmVal.prm_key = p.AsString();

                }
                else if (p.StorageType == StorageType.Integer)
                {
                    prmVal.prm_int_val = p.AsInteger();
                    prmVal.prm_key = p.AsValueString();

                }
                else if (p.StorageType == StorageType.ElementId)
                {
                    prmVal.prm_int_val = p.AsElementId().IntegerValue;
                    prmVal.prm_key = p.AsValueString();

                }
                else if (p.StorageType == StorageType.Double)
                {
                    prmVal.prm_double_val = p.AsDouble();
                    prmVal.prm_key = p.AsValueString();

                }

            }
            return prmVal;
        }
        public List<Parameter> getAllElemPrms(Element x_e)
        {
            List<Parameter> out_lst = new List<Parameter>();
            ElementId x_id = x_e.GetTypeId();
            ElementType x_tp = x_e.Document.GetElement(x_id) as ElementType;
            ParameterSet x_params = x_e.Parameters;
            List<int> prm_ids_ints = new List<int>();   
            foreach (Parameter prm in x_params)
            {
                if (prm.HasValue && !prm_ids_ints.Contains(prm.Id.IntegerValue))
                {
                    prm_ids_ints.Add(prm.Id.IntegerValue);
                    out_lst.Add(prm);
                }
                
            }
            if (x_tp != null)
            {
                ParameterSet x_tp_params = x_tp.Parameters;
                foreach (Parameter tp_prm in x_tp_params)
                {
                    if (tp_prm.HasValue && !prm_ids_ints.Contains(tp_prm.Id.IntegerValue))
                    {
                        prm_ids_ints.Add(tp_prm.Id.IntegerValue);
                        out_lst.Add((Parameter)tp_prm);
                    }
                    
                }

            }
            return out_lst;

        }
        public class elemStrPrm
        {
            public Element elem { get; set; }
            public string strPrm { get; set; }
        }
        public List<string> getallPrmVals(List<Element> x_lst_elems, List<prmInfo> x_p_infos)
        {

            List<string> lst_strs = new List<string>();
            foreach (Element elem in x_lst_elems)
            {
                List<Parameter> lst_prms = getAllElemPrms((Element)elem);
                List<string> sub_lst_strs = new List<string>();
                foreach (prmInfo x_p_info in x_p_infos)
                {
                    foreach (Parameter prm in lst_prms)
                    {
                        prmValInfo p_v_info = getParamVal(prm);
                        if (p_v_info != null)
                        {
                            sub_lst_strs.Add(p_v_info.prm_key);

                        }
                    }

                }
                string prm_str = String.Join("_", sub_lst_strs);
                if (!lst_strs.Contains(prm_str))
                {
                    lst_strs.Add(prm_str);
                }


            }
            return lst_strs;

        }
        public List<prmInfo> elemGetPrmInfo(Element x_e)
        {
            List<int> prm_ids = new List<int>();
            List<prmInfo> out_prmInfo = new List<prmInfo>();

            ElementId x_id = x_e.GetTypeId();
            ElementType x_tp = x_e.Document.GetElement(x_id) as ElementType;
            ParameterSet x_params = x_e.Parameters;

            foreach (Parameter prm in x_params)
            {
                prmInfo p_info = new prmInfo
                {
                    prm_nm = prm.Definition.Name,
                    prm_id_val = prm.Id.IntegerValue
                };
                if (!prm_ids.Contains(p_info.prm_id_val))
                {
                    prm_ids.Add(p_info.prm_id_val);
                    out_prmInfo.Add(p_info);

                }
            }

            if (x_tp != null)
            {
                ParameterSet x_tp_params = x_tp.Parameters;
                foreach (Parameter prm in x_tp_params)
                {
                    prmInfo p_info = new prmInfo
                    {
                        prm_doc = x_e.Document,
                        prm_nm = prm.Definition.Name,
                        prm_id_val = prm.Id.IntegerValue
                    };
                    if (!prm_ids.Contains(p_info.prm_id_val))
                    {
                        prm_ids.Add(p_info.prm_id_val);
                        out_prmInfo.Add(p_info);

                    }
                }
            }
            return out_prmInfo;

        }
        public List<prmInfo> getAllParams(Document x_doc, List<Element> x_lst)
        {
            List<prmInfo> out_prmInfo = new List<prmInfo>();
            List<int> prm_ids = new List<int>();

            foreach (Element x_e in x_lst)
            {
                List<prmInfo> sub_prmInfo = elemGetPrmInfo(x_e);
                foreach (prmInfo pInfo in sub_prmInfo)
                {
                    if (!prm_ids.Contains(pInfo.prm_id_val))
                    {
                        prm_ids.Add(pInfo.prm_id_val);
                        out_prmInfo.Add(pInfo);
                    }
                }

            }
            return out_prmInfo;
        }

        public class elemPrms
        {
            public Element elem { get; set; }
            public List<int> prms_ids { get; set; }
        }
        public List<elemPrms> lst_elemPrms { get; set; }
        public List<int> sel_ids_a { get; set; }
        public string gt_n_name(string e_nm, string sp)
        {
            char c_sp = sp.ToCharArray().FirstOrDefault();
            string[] lst_nm = e_nm.Split(c_sp);
            string num_str = lst_nm[lst_nm.Length - 1];
            int num = new int();
            Int32.TryParse(num_str, out num);
            int n_num = num + 1;
            string n_num_str = n_num.ToString().PadLeft(2, '0');
            List<string> n_lst_nm = new List<string>();
            for (int i = 0; i < lst_nm.Length - 1; i++)
            {
                n_lst_nm.Add(lst_nm[i]);
            }
            n_lst_nm.Add(n_num_str);
            string n_nm_a = String.Join(sp, n_lst_nm);
            return n_nm_a;
        }
        
        public List<Element> getAllLstvElems(System.Windows.Forms.ListView x_lstv)
        {
            List<Element> all_docs_elems = new List<Element>();
            foreach (System.Windows.Forms.ListViewItem lstv_itm in x_lstv.CheckedItems)
            {
                Document sel_doc = lstv_itm.Tag as Document;

                List<Element> sel_doc_elems = getAllModelElements(sel_doc);
                all_docs_elems.AddRange(sel_doc_elems);
            }
            return all_docs_elems;
        }

        public List<prmInfo> getDocParams(System.Windows.Forms.ListView x_lstv, System.Windows.Controls.TreeView x_trv)
        {

            List<prmInfo> lst_sel_prminfos = new List<prmInfo>();
            List<int> lst_sel_prminfo_ids = new List<int>();
            List<Element> all_docs_elems = new List<Element>();
            foreach (System.Windows.Forms.ListViewItem lstv_itm in x_lstv.CheckedItems)
            {
                Document sel_doc_a = lstv_itm.Tag as Document;

                List<Element> sel_doc_elems_a = getAllModelElements(sel_doc_a);

                List<prmInfo> sel_doc_pinfo = getAllParams(sel_doc_a, sel_doc_elems_a);

                foreach (prmInfo sel_prm_info in sel_doc_pinfo)
                {
                    if (!lst_sel_prminfo_ids.Contains(sel_prm_info.prm_id_val))
                    {
                        lst_sel_prminfo_ids.Add(sel_prm_info.prm_id_val);
                        lst_sel_prminfos.Add(sel_prm_info);
                    }
                }

            }

            return lst_sel_prminfos;

        }
        public void generateTreeVieww(System.Windows.Forms.ListView x_lstv, System.Windows.Controls.TreeView x_trv)
        {
            x_trv.Items.Clear();
            foreach (System.Windows.Forms.ListViewItem lstv_itm in x_lstv.CheckedItems)
            {
                Document sel_doc_a = lstv_itm.Tag as Document;

                List<Element> sel_doc_elems_a = getAllModelElements(sel_doc_a);
                TreeViewItem trv_01 = new TreeViewItem
                {
                    Header = new System.Windows.Controls.CheckBox
                    {
                        Content = sel_doc_a.Title + "(" + sel_doc_elems_a.Count().ToString() + ")",
                    },
                    Tag = sel_doc_elems_a
                };
                x_trv.Items.Add(trv_01);
                var grpd_elems_a = sel_doc_elems_a.GroupBy(el => el.Category.Id).Select(grp => grp.ToList()).ToList();
                for (int i = 0; i < grpd_elems_a.Count; i++)
                {
                    string nm = grpd_elems_a[i][0].Category.Name;
                    TreeViewItem n_Tr_v_0 = new TreeViewItem
                    {
                        Header = new System.Windows.Controls.CheckBox
                        {
                            Content = nm + "(" + grpd_elems_a[i].Count().ToString() + ")",
                        },

                        Tag = grpd_elems_a[i]
                    };
                    trv_01.Items.Add(n_Tr_v_0);
                    for (int j = 0; j < grpd_elems_a[i].Count; j++)
                    {
                        string nm_1 = grpd_elems_a[i][j].Name;
                        List<Element> tag_elems = new List<Element>();

                        tag_elems.Add(grpd_elems_a[i][j]);
                        TreeViewItem n_Tr_v_1 = new TreeViewItem
                        {
                            Header = new System.Windows.Controls.CheckBox
                            {
                                Content = nm_1 + "(" + grpd_elems_a[i][j].Id.ToString() + ")",

                            },
                            Tag = tag_elems
                        };
                        n_Tr_v_0.Items.Add(n_Tr_v_1);
                       
                    }

                };

            }
        }
        
        public void treeViewCheckedItems(System.Windows.Controls.TreeViewItem x_trv, List<System.Windows.Controls.TreeViewItem> result)
        {
            //List<object> result = new List<object>();   
            foreach(System.Windows.Controls.TreeViewItem trv_itm in x_trv.Items)
            {
                if(trv_itm.Header is System.Windows.Controls.CheckBox CheckBox && CheckBox.IsChecked == true)
                {
                    result.Add(trv_itm);
                    
                }
                foreach(var child in trv_itm.Items)
                {
                    if(child is System.Windows.Controls.TreeViewItem sub_trv_itm)
                    {
                        treeViewCheckedItems(sub_trv_itm, result);
                    }
                }
            }
            //return result;
        }
       
        public List<Element> getTreeViewElems(System.Windows.Controls.TreeView x_Trv)
        {
            List<Element> x_lst = new List<Element>();
            List<System.Windows.Controls.TreeViewItem> lst_trv_itms = new List<TreeViewItem>();
            foreach (System.Windows.Controls.TreeViewItem trv_itm in x_Trv.Items)
            {
                List<System.Windows.Controls.TreeViewItem> sub_lst_trv_itms = new List<System.Windows.Controls.TreeViewItem>();
                if(trv_itm.Header is System.Windows.Controls.CheckBox chckbox && chckbox.IsChecked == true)
                {
                    sub_lst_trv_itms.Add(trv_itm);
                }

                treeViewCheckedItems(trv_itm, sub_lst_trv_itms);
                foreach(TreeViewItem trv_itm_a in sub_lst_trv_itms)
                {
                    List<Element> lst_elems = trv_itm_a.Tag as List<Element>;
                    if (lst_elems.Count > 0)
                    {
                        foreach (Element elem in lst_elems)
                        {
                            x_lst.Add(elem);
                        }
                    }
                 
                
                }

            }
            
            return x_lst;
        }

        public List<Element> all_sel_elems_a { get; set; }
        public List<Element> all_sel_elems_b { get; set; }
        public void reRangeTreeView(System.Windows.Controls.TreeView x_trv,List<Element> x_lst, List<set_cbx_btn> x_lst_cmbox_set)
        {
            x_trv.Items.Clear();
            List<int> sel_ids = x_lst_cmbox_set.Select(a => (prmInfo)a.xn_cbx.SelectedItem).Select(n => (int)n.prm_id_val).Distinct().ToList();

            string tmp = String.Join("-", sel_ids.Select(m => (string)m.ToString()).ToArray());
            
            List<elemStrPrm> lst_elem_str_prms_a = new List<elemStrPrm>();
            foreach (Element elem_a in x_lst)
            {
                List<Parameter> e_aPrm = getAllElemPrms(elem_a);
                //List<Parameter> lst_prms = new List<Parameter>();
                List<Parameter> lst_prms = e_aPrm.Where(n => sel_ids.Contains(n.Id.IntegerValue)).ToList();
                List<string> lst_vals = lst_prms.Select(m => m.HasValue ? m.AsValueString() : "").ToList();

                string val = String.Join("$", lst_vals);
                lst_elem_str_prms_a.Add(new elemStrPrm
                {
                    elem = elem_a,
                    strPrm = val,
                });

            }
            var new_lst = lst_elem_str_prms_a.GroupBy(x => x.strPrm).Select(grp => grp.ToList()).ToList();
            
            for (int i = 0; i < new_lst.Count; i++)
            {
                string nm = new_lst[i][0].strPrm;
                List<Element> n_lst_i = new_lst[i].Select(a => a.elem).ToList();

                TreeViewItem n_Tr_v_0 = new TreeViewItem
                {
                    Header = new System.Windows.Controls.CheckBox
                    {
                        Content = nm + "(" + n_lst_i.Count().ToString() + ")",
                    },
                            
                    Tag = n_lst_i
                };

                x_trv.Items.Add(n_Tr_v_0);
                for (int j = 0; j < new_lst[i].Count; j++)
                {
                    string nm_1 = new_lst[i][j].elem.Name + "%" + new_lst[i][j].strPrm;
                    string id_1 = new_lst[i][j].elem.Id.ToString();
                    List<Element> n_lst_elems = new List<Element>();
                    n_lst_elems.Add(new_lst[i][j].elem);
                    TreeViewItem n_Tr_v_1 = new TreeViewItem
                    {
                        Header = new System.Windows.Controls.CheckBox
                        {
                            Content = nm_1 + "(id: " +id_1 + "&" + n_lst_elems.Count.ToString()+")",
                        },
                        Tag = n_lst_elems
                    };
                    n_Tr_v_0.Items.Add(n_Tr_v_1);
                }

            }
        }
        public List<Element> SelectedElemsA { get; set; }
        public List<Element> SelectedElemsB { get; set; }

       

        public static bool bBoxIntersct(BoundingBoxXYZ b1, BoundingBoxXYZ b2)
        {
            return !(
                b1.Max.X < b2.Min.X || b1.Min.X > b2.Max.X ||
                b1.Max.Y < b2.Min.Y || b1.Min.Y > b2.Max.Y ||
                b1.Max.Z < b2.Min.Z || b1.Min.Z > b2.Max.Z
                );
        }
        public static bool mshMightIntersct(Element e1,Element e2)
        {
            BoundingBoxXYZ bbx1 = e1.get_BoundingBox(null);
            BoundingBoxXYZ bbx2 = e2.get_BoundingBox(null);
            return bBoxIntersct(bbx1 , bbx2 );

        }
       

        public class set_cbx_btn
        {
            public System.Windows.Controls.Button xn_btn { get; set; }
            public System.Windows.Controls.Button xn_btn_min { get; set; }
            public System.Windows.Controls.ComboBox xn_cbx { get; set; }


        }

        private set_cbx_btn origin_set_A { get; set; }
        public List<set_cbx_btn> lst_set_A { get; set; }

        private set_cbx_btn origin_set_B { get; set; }
        public List<set_cbx_btn> lst_set_B { get; set; }
        public void btn_a_Click(object sender, RoutedEventArgs e)
        {
            set_cbx_btn nn_pr_set = create_n_set(lst_set_A.Last());
            nn_pr_set.xn_btn.Click += new System.Windows.RoutedEventHandler(btn_a_Click);
            nn_pr_set.xn_btn_min.Click += new System.Windows.RoutedEventHandler(btn_min_a_CLick);
            lst_set_A.Add(nn_pr_set);

        }
        public void btn_min_a_CLick(object sender, RoutedEventArgs e)
        {
            remove_n_set(lst_set_A.Last());
            if (lst_set_A.Count > 1)
            {
                lst_set_A.Remove(lst_set_A.Last());
            }

        }

        public void btn_b_Click(object sender, RoutedEventArgs e)
        {
            set_cbx_btn nn_pr_set = create_n_set(lst_set_B.Last());
            nn_pr_set.xn_btn.Click += new System.Windows.RoutedEventHandler(btn_b_Click);
            nn_pr_set.xn_btn_min.Click += new System.Windows.RoutedEventHandler(btn_min_b_CLick);
            lst_set_B.Add(nn_pr_set);

        }
        public void btn_min_b_CLick(object sender, RoutedEventArgs e)
        {
            remove_n_set(lst_set_B.Last());
            if (lst_set_B.Count > 1)
            {
                lst_set_B.Remove(lst_set_B.Last());
            }

        }
        public void remove_n_set(set_cbx_btn e_set)
        {
            System.Windows.Controls.Button e_btn = e_set.xn_btn;
            System.Windows.Controls.Button e_btn_m = e_set.xn_btn_min;
            System.Windows.Controls.ComboBox e_cbx = e_set.xn_cbx;
            MainGrid.Children.Remove(e_btn);
            MainGrid.Children.Remove(e_btn_m);
            MainGrid.Children.Remove(e_cbx);
        }
        public class initGroup
        {
            public set_cbx_btn initSet { get; set; }
            public List<set_cbx_btn> initList {get;set;}
        }
        public initGroup create_init_set(System.Windows.Controls.ComboBox x_cmbox, System.Windows.Controls.Button x_btn, System.Windows.Controls.Button x_btn_m)
        {

            set_cbx_btn set_orgin = new set_cbx_btn
            {
                xn_cbx = x_cmbox,
                xn_btn = x_btn,
                xn_btn_min = x_btn_m
            };
            List<set_cbx_btn> set_orgin_grp = new List<set_cbx_btn>();
            set_orgin_grp.Add(set_orgin);
            initGroup n_grp = new initGroup
            {
                initSet = set_orgin,
                initList = set_orgin_grp
            };
            return n_grp;
        }

        public set_cbx_btn create_n_set(set_cbx_btn e_set)
        {
            System.Windows.Controls.Button e_btn = e_set.xn_btn;
            System.Windows.Controls.Button e_btn_m = e_set.xn_btn_min;
            System.Windows.Controls.ComboBox e_cbx = e_set.xn_cbx;

            string e_btn_nm = e_btn.Name;
            string e_btn_m_nm = e_btn_m.Name;
            string e_btn_m_txt = "-";
            string e_btn_txt = e_btn.Content as string;
            string e_cbx_nm = e_cbx.Name;

            string chr_sp = '_'.ToString();
            string n_btn_nm = gt_n_name(e_btn_nm, chr_sp);
            string n_btn_m_nm = gt_n_name(e_btn_m_nm, chr_sp);
            string n_cbx_nm = gt_n_name(e_cbx_nm, chr_sp);

            //create new set
            System.Windows.Controls.Button n_btn = new System.Windows.Controls.Button();
            System.Windows.Controls.Button n_btn_m = new System.Windows.Controls.Button();
            System.Windows.Controls.ComboBox n_cbx = new System.Windows.Controls.ComboBox();

            n_btn.Name = n_btn_nm;
            n_btn.Content = e_btn_txt;
            n_btn_m.Name = n_btn_m_nm;
            n_btn_m.Content= e_btn_m_txt;
            n_cbx.Name = n_cbx_nm;

            double dlt = 25;

            n_btn.Margin = new Thickness(e_btn.Margin.Left, e_btn.Margin.Top + dlt, e_btn.Margin.Right, e_btn.Margin.Bottom-dlt);
            n_btn_m.Margin = new Thickness(e_btn_m.Margin.Left, e_btn_m.Margin.Top + dlt, e_btn_m.Margin.Right, e_btn_m.Margin.Bottom - dlt);
            n_cbx.Margin = new Thickness(e_cbx.Margin.Left, e_cbx.Margin.Top + dlt , e_cbx.Margin.Right, e_cbx.Margin.Bottom - dlt);

            n_btn.Height = e_btn.Height;
            n_btn.Width = e_btn.Width;
            n_btn_m.Height = e_btn_m.Height;
            n_btn_m.Width = e_btn_m.Width;
            n_cbx.Height = e_cbx.Height;
            n_cbx.Width = e_cbx.Width;

            n_btn.VerticalAlignment = VerticalAlignment.Top;
            n_btn.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            n_btn_m.VerticalAlignment = VerticalAlignment.Top;
            n_btn_m.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            n_cbx.VerticalAlignment = VerticalAlignment.Top;

            //n_btn.Click += new System.Windows.RoutedEventHandler(btn_a_Click);
            //n_btn_m.Click += new System.Windows.RoutedEventHandler(btn_min_a_CLick);

            List<prmInfo> lst_n_prms = new List<prmInfo>();
            foreach (object itm in e_cbx.Items)
            {
                if (itm != e_cbx.SelectedItem)
                {
                    prmInfo n_itm = itm as prmInfo;

                    lst_n_prms.Add(n_itm);
                }
            }
            
            n_cbx.ItemsSource = lst_n_prms;
            n_cbx.DisplayMemberPath = "prm_nm";

            MainGrid.Children.Add(n_btn);
            MainGrid.Children.Add(n_btn_m);
            MainGrid.Children.Add(n_cbx);


            int coln_indx = System.Windows.Controls.Grid.GetColumn(e_btn);
            System.Windows.Controls.Grid.SetColumn(n_cbx, coln_indx);
            System.Windows.Controls.Grid.SetColumn(n_btn, coln_indx);
            System.Windows.Controls.Grid.SetColumn(n_btn_m, coln_indx);

            System.Windows.Controls.Grid.SetRow(n_cbx, 3);
            System.Windows.Controls.Grid.SetRow(n_btn, 3);
            System.Windows.Controls.Grid.SetRow(n_btn_m, 3);

            set_cbx_btn n_set = new set_cbx_btn
            {
                xn_btn = n_btn,
                xn_btn_min = n_btn_m,
                xn_cbx = n_cbx
            };

            return n_set;

        }
        public List<ClashSet> clashSets { get; set; }
        public newClash_UserControl1(Autodesk.Revit.DB.Document doc, Autodesk.Revit.UI.UIApplication uiapp)
        {
            InitializeComponent();
            Doc = doc;
            Uiapp = uiapp;
            lstvA = create_n_Lstv();
            lstvA.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstv_view_a_ColumnClick);
            lstvA.SelectedIndexChanged += new System.EventHandler(lstv_docsA_ItemChecked);

            //lstvA.che
            lstvB = create_n_Lstv();
            lstvB.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstv_view_b_ColumnClick);
            lstvB.SelectedIndexChanged += new System.EventHandler(lstv_docsB_SelectedIndexChanged);
            winformHostA.Child = lstvA;
            winformHostB.Child = lstvB;

            ItemsA = new ObservableCollection<MyItem>();
            ItemsB = new ObservableCollection<MyItem>();
            //Application n_app = uiapp.Application;
            DocumentSet all_docs = uiapp.Application.Documents;
            foreach(Document l_doc in all_docs)
            {
                doc_info l_doc_info = new doc_info();
                l_doc_info.s_doc = l_doc;
                l_doc_info.s_doc_nm = l_doc.Title;
                l_doc_info.is_lnkd = l_doc.IsLinked;
                l_doc_info.rvt_pth = l_doc.PathName;

                string[] lstv_str_a = new string[] {l_doc.Title.ToString(), l_doc.IsLinked.ToString(), l_doc.PathName.ToString()};
                System.Windows.Forms.ListViewItem lstv_itm_a = new System.Windows.Forms.ListViewItem(lstv_str_a);
                lstv_itm_a.Tag = l_doc;
                lstvA.Items.Add(lstv_itm_a);

                string[] lstv_str_b = new string[] {l_doc.Title.ToString(), l_doc.IsLinked.ToString(), l_doc.PathName.ToString() };
                System.Windows.Forms.ListViewItem lstv_itm_b = new System.Windows.Forms.ListViewItem(lstv_str_b);
                lstv_itm_b.Tag = l_doc;
                lstvB.Items.Add(lstv_itm_b);
   
            }
            /*
            set_cbx_btn origin_set_grp = new set_cbx_btn();
            origin_set_grp.xn_cbx = start_cmbox_a;
            origin_set_grp.xn_btn = btn_plus_a;
            origin_set_grp.xn_btn_min = btn_minus_a;
            origin_set_A = origin_set_grp;
            List<set_cbx_btn> set_cbx_orign = new List<set_cbx_btn>();
            set_cbx_orign.Add(origin_set_A);
            lst_set_A = set_cbx_orign;
            */

            initGroup init_Grp_A = create_init_set(start_cmbox_a, btn_plus_a, btn_minus_a);
            origin_set_A = init_Grp_A.initSet;
            lst_set_A = init_Grp_A.initList;

            initGroup init_Grp_B = create_init_set(start_cmbox_b,btn_plus_b,btn_minus_b);
            origin_set_B = init_Grp_B.initSet;
            lst_set_B = init_Grp_B.initList;



        }
        private void lstv_view_a_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            System.Windows.Forms.ListView lstv_v = winformHostA.Child as System.Windows.Forms.ListView;
            lstv_v.ListViewItemSorter = new ListViewItemComparer(e.Column);
           
        }
        private void lstv_view_b_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            System.Windows.Forms.ListView lstv_v = winformHostB.Child as System.Windows.Forms.ListView;
            lstv_v.ListViewItemSorter = new ListViewItemComparer(e.Column);
            
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {

        }

        private void btn_add_01_Click(object sender, RoutedEventArgs e)
        {
            List<set_cbx_btn> lst_set_01 = lst_set_A;
            set_cbx_btn first_pr_set = create_n_set(origin_set_A);
            first_pr_set.xn_btn.Click += new System.Windows.RoutedEventHandler(btn_a_Click);
            first_pr_set.xn_btn_min.Click += new System.Windows.RoutedEventHandler(btn_min_a_CLick);
            lst_set_01.Add(first_pr_set);
            lst_set_A = lst_set_01;
        }
        private void btn_add_02_Click(object sender, RoutedEventArgs e)
        {
            List<set_cbx_btn> lst_set_01 = lst_set_B;
            set_cbx_btn first_pr_set = create_n_set(origin_set_B);
            first_pr_set.xn_btn.Click += new System.Windows.RoutedEventHandler(btn_b_Click);
            first_pr_set.xn_btn_min.Click += new System.Windows.RoutedEventHandler(btn_min_b_CLick);
            lst_set_01.Add(first_pr_set);
            lst_set_B = lst_set_01;
        }
        private void btn_a_confirm_Click(object sender, RoutedEventArgs e)
        {
            //TaskDialog.Show("combo_box_count:",lst_set_A.Count.ToString());
            all_sel_elems_a = getAllLstvElems(lstvA);
            reRangeTreeView(trv_a, all_sel_elems_a, lst_set_A);
            

        }

        private void btn_b_confirm_Click(object sender, RoutedEventArgs e)
        {
            all_sel_elems_b = getAllLstvElems(lstvB);
            reRangeTreeView(trv_b, all_sel_elems_b, lst_set_B);
        }

        private void ButtonOk_A_Click(object sender, RoutedEventArgs e)
        {
            //trv_a.Items.Clear();
            generateTreeVieww(lstvA, trv_a);
            start_cmbox_a.Items.Clear();
            //List<List<ElementId>> lst_lst_prm_id = new List<List<ElementId>>();
            //List<List<prmDoc>> lst_lst_prm_doc = new List<List<prmDoc>>();
            List<prmInfo> lst_prm_info_a = getDocParams(lstvA, trv_a);

            start_cmbox_a.ItemsSource = lst_prm_info_a;
            start_cmbox_a.DisplayMemberPath = "prm_nm";

        }

        private void ButtonOk_B_Click(object sender, RoutedEventArgs e)
        {
            generateTreeVieww(lstvB, trv_b);
            //trv_b.Items.Clear();
            start_cmbox_b.Items.Clear();
            
            List<prmInfo> lst_prm_info_b = getDocParams(lstvB, trv_b);
            start_cmbox_b.ItemsSource = lst_prm_info_b;
            start_cmbox_b.DisplayMemberPath = "prm_nm";

        }

        private void WindowsFormsHost_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
        private void lstv_docsA_ItemChecked(object sender, EventArgs e)
        {

        }
        private void lstv_docsB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StartClash_Click(object sender, RoutedEventArgs e)
        {
            SelectedElemsA = getTreeViewElems(trv_a);
            SelectedElemsB = getTreeViewElems(trv_b);

            clashSets = new List<ClashSet>();
            List<ClashSet> pre_lst_clsh_set = new List<ClashSet>(); 
            foreach (Element eA in SelectedElemsA)
            {
                foreach(Element eB in SelectedElemsB)
                {
                    
                    
                    if(eA.Document.PathName != eB.Document.PathName)
                    {
                       
                        if (mshMightIntersct(eA, eB))
                        {
                            ClashSet n_clsh_set = new ClashSet
                            {
                                elemA = eA,
                                elemB = eB
                            };
                            pre_lst_clsh_set.Add(n_clsh_set);

                        }
                    }

                    else if (eA.Document.PathName == eB.Document.PathName && eA.Id != eB.Id)
                    {
                        
                        if (mshMightIntersct(eA, eB))
                        {
                            ClashSet n_clsh_set = new ClashSet
                            {
                                elemA = eA,
                                elemB = eB
                            };
                            pre_lst_clsh_set.Add(n_clsh_set);

                        }
                    }

                }
            }
            if(pre_lst_clsh_set.Count > 0)
            {
                foreach (ClashSet pre_set in pre_lst_clsh_set)
                {
                    ElementIntersectsElementFilter intrsct_filtr = new ElementIntersectsElementFilter(pre_set.elemA);
                    if (intrsct_filtr.PassesFilter(pre_set.elemB))
                    {
                        clashSets.Add(pre_set);

                    }
                }

            }
            TaskDialog.Show("might have clash: ", SelectedElemsA.Count().ToString() + "&" + SelectedElemsB.Count().ToString() + ":" + pre_lst_clsh_set.Count().ToString() + " VS " + clashSets.Count().ToString());


            try
            {
                newClash_UserControl2 n_window2 = new newClash_UserControl2(Doc, Uiapp, clashSets);
                //n_window2.GetNewSet = clashSets;
                
                WindowInteropHelper n_helper = new WindowInteropHelper(n_window2);
                
                n_helper.Owner = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
                
                n_window2.Show();
                
            }
            catch (Exception ex)
            {

                TaskDialog.Show("Error", ex.ToString());
                
            }


        }


    }
    
}
