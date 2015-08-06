using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MapEditor.Managers;
using MapEditor.Maps;
using MapEditor.Windows;

namespace MapEditor
{
    public partial class Form1 : Form
    {
        private string savePath;

        public Form1 ()
        {
            InitializeComponent();
            savePath = String.Empty;
        }

        private void speichernAlsToolStripMenuItem_Click (object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Xml File (.xml)|*.xml";
            sfd.Title = "Speicher Map";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                editor1.Map.Save(sfd.FileName);
                savePath = sfd.FileName;
            }
        }

        private void mapÖffnenToolStripMenuItem_Click (object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Xml File (.xml)|*.xml";
            ofd.Title = "Öffne Map";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                XmlManager<Map> mapXML = new XmlManager<Map>();
                editor1.Map = mapXML.Load(ofd.FileName);
                editor1.Map.Initialize(editor1.Content);
                editor1.ResetSelector();
                tileDisplay1.SetNewTileImage();

                savePath = ofd.FileName;

                layerCheckedListBox.Items.Clear();
                for (int c = 0; c < editor1.Map.Layers.Count; c++)
                    layerCheckedListBox.Items.Add(c);
                editor1.OneLayerActive = false;
            }
        }

        private void rotateLeftButton_Click (object sender, EventArgs e)
        {
            tile1.RotateTile(-1);
        }

        private void rotateRightButton_Click (object sender, EventArgs e)
        {
            tile1.RotateTile(1);
        }

        private void layerCheckedListBox_ItemCheck (object sender, ItemCheckEventArgs e)
        {
            CheckedListBox clb = (CheckedListBox)sender;
            // Switch off event handler
            clb.ItemCheck -= layerCheckedListBox_ItemCheck;
            clb.SetItemCheckState(e.Index, e.NewValue);
            // Switch on event handler
            clb.ItemCheck += layerCheckedListBox_ItemCheck;

            int layerCount = 0;
            for (int c = 0; c < editor1.Map.Layers.Count; c++)
            {
                if (layerCheckedListBox.CheckedIndices.Contains(c))
                    editor1.Map.Layers[c].Active = true;
                else
                    editor1.Map.Layers[c].Active = false;

                if (editor1.Map.Layers[c].Active)
                    layerCount++;
            }
            if (layerCount != 1)
                editor1.OneLayerActive = false;
            else
                editor1.OneLayerActive = true;

            editor1.Invalidate();
            tile1.Invalidate();
            tileDisplay1.Invalidate();
        }

        private void neueMapToolStripMenuItem_Click (object sender, EventArgs e)
        {
            DialogResult dialogResult;

            CreateMap newMap = new CreateMap(editor1);
            editor1.CreateNewMap = true;
            dialogResult = newMap.ShowDialog();
            editor1.CreateNewMap = false;
            if (dialogResult == System.Windows.Forms.DialogResult.Abort || dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;

            CreateLayer newLayer = new CreateLayer(editor1, tileDisplay1);
            dialogResult = newLayer.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Abort || dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;

            layerCheckedListBox.Items.Clear();
            for (int c = 0; c < editor1.Map.Layers.Count; c++)
                layerCheckedListBox.Items.Add(c);
        }

        private void speichernToolStripMenuItem_Click (object sender, EventArgs e)
        {
            if (savePath != String.Empty)
                editor1.Map.Save(savePath);
            else
                speichernAlsToolStripMenuItem_Click(null, null);
        }
    }
}

            //this.tile1 = new WindowParts.Tile(editor1);
            //this.tileDisplay1 = new WindowParts.TileDisplay(editor1, tile1);