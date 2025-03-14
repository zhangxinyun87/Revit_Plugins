using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Revit_Plugins
{
    public partial class set_associate_param_Form1 : System.Windows.Forms.Form
    {
        Autodesk.Revit.DB.Document Doc;
        Autodesk.Revit.UI.UIApplication Uiapp;
        public set_associate_param_Form1(Autodesk.Revit.DB.Document doc, Autodesk.Revit.UI.UIApplication uiapp)
        {
            InitializeComponent();
            Doc = doc;
            Uiapp = uiapp;
        }
        public ElementId input_id { get;set;}
        public ElementId set_id { get; set; }
        public class group_prams
        {
            public FamilyParameter fam_pram {get; set; }
            public Parameter pram { get; set;}
            public string info { get; set;}    
        }
        public List<group_prams> group_bind {  get; set; }  


        private void set_associate_param_Form1_Load(object sender, EventArgs e)
        {
            


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string tx1 = textBox1.Text;
            int tx_id1= Int32.Parse(tx1);
            ElementId id_in = new ElementId(tx_id1);
            input_id = id_in;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string tx2 = textBox1.Text;
            int tx_id2 = Int32.Parse(tx2);
            ElementId id_out = new ElementId(tx_id2);
            set_id = id_out;    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Element dst_elem = Doc.GetElement(set_id);
            ParameterSet dst_elem_prms = dst_elem.Parameters;
            FamilyManager dc_fam_mngr = Doc.FamilyManager;
            FamilyParameterSet dc_prms_set = dc_fam_mngr.Parameters;
            List<group_prams> bind_list = new List<group_prams>();  
            
            
            foreach (FamilyParameter fam_prms in dc_prms_set)
            {
                ParameterSet a_prms = fam_prms.AssociatedParameters;
                
                foreach(Parameter prm in a_prms)
                {
                    Element elem0 = prm.Element;
                    ElementId id0 = elem0.Id;
                    foreach(Parameter dst_prm in dst_elem_prms)
                    {
                        if(dst_prm.Id == prm.Id && id0 == input_id)
                        {
                            group_prams g_prm = new group_prams();
                            g_prm.pram = dst_prm;
                            g_prm.fam_pram = fam_prms;
                            g_prm.info = dst_prm.Definition.Name + "fam_parameter: " + fam_prms.Definition.Name;
                            bool can_b_ass = dc_fam_mngr.CanElementParameterBeAssociated(dst_prm);
                            bind_list.Add(g_prm);
                            listBox1.Items.Add(can_b_ass);

                        }
                    }
                    
                        
                }
                

                /*
                if (a_prms != null)
                {
                    group_prams g_prms = new group_prams();
                    foreach (Parameter prm0 in a_prms)
                    {

                        Element elem0 = prm0.Element;
                        ElementId id0 = elem0.Id;
                        if (id0 == input_id)
                        {
                            ElementId dst_insrt_id = prm0.Id;
                            foreach (Parameter dst_p in dst_elem_prms)
                            {
                                ElementId dst_p_id = dst_p.Id;
                                List<ElementId> a_prms_id = new List<ElementId>();
                                foreach (Parameter p in a_prms)
                                {
                                    a_prms_id.Add(p.Id);
                                }
                                if (dst_p_id == id0)
                                {
                                    //&& a_prms_id.Contains(id0) == false
                                    //dc_fam_mngr.AssociateElementParameterToFamilyParameter(dst_p,fam_prms);
                                    g_prms.pram = dst_p;
                                    g_prms.fam_pram = fam_prms;
                                    g_prms.info = dst_p.Definition.Name + "_" + fam_prms.Definition.Name;
                                    //

                                    //a_prms.Insert(dst_p);


                                }
                            }

                        }
                    }
                    bind_list.Add(g_prms);
                    listBox1.Items.Add(g_prms.info);
                
                }
                */



            }
            group_bind = bind_list;
            
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            FamilyManager dc_fam_mngr_a = Doc.FamilyManager;
            foreach (group_prams g_prm in group_bind)
            {
                if (dc_fam_mngr_a.CanElementParameterBeAssociated(g_prm.pram))
                {
                    using (Transaction trns = new Transaction(Doc, "associate_pram"))
                    {

                        if(trns.Start() == TransactionStatus.Started)
                        {
                            dc_fam_mngr_a.AssociateElementParameterToFamilyParameter(g_prm.pram, g_prm.fam_pram);

                        }
                        

                        trns.Commit();


                    }
                }
                
                


            }
            /*
            using (Transaction trns = new Transaction(Doc, "associate_pram"))
            {

                trns.Start();
                
                trns.Commit();


            }
            */
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
