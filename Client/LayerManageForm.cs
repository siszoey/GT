﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JXDL.ClientBusiness;
using JXDL.IntrefaceStruct;


namespace JXDL.Client
{
    public partial class LayerManageForm : Form
    {
        public LayerStruct[] Layers { get; set; }
        public LayerManageForm()
        {
            InitializeComponent();
        }

        private void LayerManageForm_Load(object sender, EventArgs e)
        {
            if (Layers != null)
            {
                foreach(LayerStruct vTempLayer in Layers)
                {
                    ListViewItem vNewItem = new ListViewItem();
                    vNewItem.UseItemStyleForSubItems = false;
                    vNewItem.Checked = vTempLayer.IsView;
                    vNewItem.SubItems.Add(vTempLayer.ID.ToString());
                    vNewItem.SubItems.Add( vTempLayer.Expository);
                    vNewItem.SubItems.Add( CommonUnit.ConvertLayerType(vTempLayer.Type.Value));
                    if (vTempLayer.Color != -1)
                    {
                        Color vColor = Color.FromArgb(vTempLayer.Color);
                        vNewItem.SubItems.Add("", vColor, vColor, new Font("宋体", 13));
                    }
                    else
                    {
                        vNewItem.SubItems.Add("");
                    }
                    //ListViewItem.ListViewSubItem vSubItem = new ListViewItem.ListViewSubItem()
                    //{
                    //    BackColor = Color.Red
                    //};
                    
                    listView_Layer.Items.Add(vNewItem);
                }
            }
        }

        //string convertLayerType( int type)
        //{
        //    string vResult = "";
        //    switch (type)
        //    {
        //        case 0:
        //            vResult = "点";
        //            break;
        //        case 1:
        //            vResult = "线";
        //            break;
        //        case 2:
        //            vResult = "面";
        //            break;
        //    }
        //    return vResult;
        //}

        private void button_Save_Click(object sender, EventArgs e)
        {
            foreach( ListViewItem vTempItem in listView_Layer.Items )
            {
                int vID = int.Parse( vTempItem.SubItems[1].Text );
                Layers.Where(m => m.ID == vID).FirstOrDefault().IsView = vTempItem.Checked;
                Layers.Where(m => m.ID == vID).FirstOrDefault().Color = vTempItem.SubItems[4].BackColor.ToArgb();
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void listView_Layer_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
          
        }

        private void listView_Layer_DoubleClick(object sender, EventArgs e)
        {
            if ( listView_Layer.SelectedItems.Count > 0 )
            {
                ColorDialog vColorDialog = new ColorDialog();
                if (vColorDialog.ShowDialog() == DialogResult.OK)
                {
                    listView_Layer.SelectedItems[0].SubItems[4].BackColor = vColorDialog.Color;
                    listView_Layer.SelectedItems[0].SubItems[4].ForeColor = vColorDialog.Color;
                }
            }
        }
    }
}