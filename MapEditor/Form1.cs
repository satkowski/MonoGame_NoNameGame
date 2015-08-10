﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Input;

using Microsoft.Xna.Framework;

using MapEditor.Managers;
using MapEditor.Maps;
using MapEditor.Windows;
using MapEditor.Extension;

namespace MapEditor
{
    public partial class Form1 : Form
    {
        private string savePath;

        public Form1 ()
        {
            InitializeComponent();
            savePath = String.Empty;
            layerCheckedListBox.GotFocus += layerCheckedListBox_GotFocus;
        }

        private void enableLayerProperties ()
        {
            resetChangesButton_Click(null, null);

            offsetXTextBox.Enabled = true;
            offsetYTextBox.Enabled = true;
            tileSheetTextBox.Enabled = true;
            saveChangesButton.Enabled = true;
            resetChangesButton.Enabled = true;

        }
        private void disableLayerProperties ()
        {
            offsetXTextBox.Text = String.Empty;
            offsetYTextBox.Text = String.Empty;
            tileSheetTextBox.Text = String.Empty;

            offsetXTextBox.Enabled = false;
            offsetYTextBox.Enabled = false;
            tileSheetTextBox.Enabled = false;
            saveChangesButton.Enabled = false;
            resetChangesButton.Enabled = false;
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
                {
                    layerCheckedListBox.Items.Add(c);
                    layerCheckedListBox.SetItemChecked(c, true);
                }
                layerCheckedListBox.SetSelected(0, true);
                editor1.DrawingAllowed = true;
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

            for (int c = 0; c < editor1.Map.Layers.Count; c++)
            {
                if (layerCheckedListBox.CheckedIndices.Contains(c))
                    editor1.Map.Layers[c].Active = true;
                else
                    editor1.Map.Layers[c].Active = false;
            }

            layerCheckedListBox_SelectedIndexChanged(null, null);
        }

        private void layerCheckedListBox_SelectedIndexChanged (object sender, EventArgs e)
        {
            if (layerCheckedListBox.CheckedIndices.Contains(layerCheckedListBox.SelectedIndex))
            {
                editor1.DrawingAllowed = true;
                editor1.CurrentLayerNumber = layerCheckedListBox.SelectedIndex;
                editor1.ResetSelector();

                enableLayerProperties();
            }
            else
            {
                editor1.DrawingAllowed = false;

                disableLayerProperties();
            }

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

        private void neuesLayerToolStripMenuItem_Click (object sender, EventArgs e)
        {
            CreateLayer newLayer = new CreateLayer(editor1, tileDisplay1);
            DialogResult dialogResult = newLayer.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Abort || dialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                if (editor1.CurrentLayer == null)
                    editor1.Map = null;
                return;
            }

            layerCheckedListBox.Items.Add(editor1.Map.Layers.Count - 1);
        }

        private void saveChangesButton_Click (object sender, EventArgs e)
        {
            int offsetX = int.Parse(offsetXTextBox.Text);
            int offsetY = int.Parse(offsetYTextBox.Text);

            editor1.CurrentLayer.Offset = new Vector2(offsetX, offsetY);
            editor1.CurrentLayer.TileSheet.Path = tileSheetTextBox.Text;

            editor1.Invalidate();
            tileDisplay1.Invalidate();
            tile1.Invalidate();
        }

        private void resetChangesButton_Click (object sender, EventArgs e)
        {
            offsetXTextBox.Text = editor1.CurrentLayer.Offset.X.ToString();
            offsetYTextBox.Text = editor1.CurrentLayer.Offset.Y.ToString();
            tileSheetTextBox.Text = editor1.CurrentLayer.TileSheet.Path;
        }

        private void upButton_Click (object sender, EventArgs e)
        {
            if (layerCheckedListBox.SelectedIndices.Count == 1)
            {
                if (layerCheckedListBox.SelectedIndex > 0)
                {
                    editor1.Map.Layers = (List<Layer>)editor1.Map.Layers.Swap<Layer>(layerCheckedListBox.SelectedIndex, layerCheckedListBox.SelectedIndex - 1);

                    int selectedIndex = layerCheckedListBox.SelectedIndex;
                    CheckState thisCheckState = layerCheckedListBox.GetItemCheckState(selectedIndex);
                    CheckState upperCheckState = layerCheckedListBox.GetItemCheckState(selectedIndex - 1);

                    layerCheckedListBox.SetItemCheckState(selectedIndex, upperCheckState);
                    layerCheckedListBox.SetSelected(selectedIndex, false);
                    layerCheckedListBox.SetItemCheckState(selectedIndex - 1, thisCheckState);
                    layerCheckedListBox.SetSelected(selectedIndex - 1, true);

                    editor1.Invalidate();
                    tileDisplay1.Invalidate();
                    tile1.Invalidate();
                }
            }
        }

        private void downButton_Click (object sender, EventArgs e)
        {
            if (layerCheckedListBox.SelectedIndices.Count == 1)
            {
                if (layerCheckedListBox.SelectedIndex < editor1.Map.Layers.Count - 1)
                {
                    editor1.Map.Layers = (List<Layer>)editor1.Map.Layers.Swap<Layer>(layerCheckedListBox.SelectedIndex, layerCheckedListBox.SelectedIndex + 1);

                    int selectedIndex = layerCheckedListBox.SelectedIndex;
                    CheckState thisCheckState = layerCheckedListBox.GetItemCheckState(selectedIndex);
                    CheckState lowerCheckState = layerCheckedListBox.GetItemCheckState(selectedIndex + 1);

                    layerCheckedListBox.SetItemCheckState(selectedIndex, lowerCheckState);
                    layerCheckedListBox.SetSelected(selectedIndex, false);
                    layerCheckedListBox.SetItemCheckState(selectedIndex + 1, thisCheckState);
                    layerCheckedListBox.SetSelected(selectedIndex + 1, true);

                    editor1.Invalidate();
                    tileDisplay1.Invalidate();
                    tile1.Invalidate();
                }
            }
        }

        private void Form1_KeyDown (object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.Alt &&
                    e.KeyCode == Keys.U) // U
                    upButton_Click(null, null);
                else if (e.Alt &&
                         e.KeyCode == Keys.D) // D
                    downButton_Click(null, null);
                else if (e.KeyCode == Keys.L) // Left
                    rotateLeftButton_Click(null, null);
                else if (e.KeyCode == Keys.R) // Right
                    rotateRightButton_Click(null, null);
                else if (e.KeyCode == Keys.U) // Up
                {
                    if (layerCheckedListBox.SelectedIndex > 0)
                    {
                        layerCheckedListBox.SetSelected(layerCheckedListBox.SelectedIndex, false);
                        layerCheckedListBox.SetSelected(layerCheckedListBox.SelectedIndex - 1, true);
                    }
                }
                else if (e.KeyCode == Keys.D) // Down
                {
                    if (layerCheckedListBox.SelectedIndex < editor1.Map.Layers.Count - 1)
                    {
                        layerCheckedListBox.SetSelected(layerCheckedListBox.SelectedIndex, false);
                        layerCheckedListBox.SetSelected(layerCheckedListBox.SelectedIndex + 1, true);
                    }
                }
                else
                    return;

                editor1.Invalidate();
                tileDisplay1.Invalidate();
                tile1.Invalidate();

                return;
            }

            if (e.KeyCode == Keys.Space) // Space
            {
                if (layerCheckedListBox.GetItemCheckState(layerCheckedListBox.SelectedIndex) == CheckState.Checked)
                    layerCheckedListBox.SetItemCheckState(layerCheckedListBox.SelectedIndex, CheckState.Unchecked);
                else if (layerCheckedListBox.GetItemCheckState(layerCheckedListBox.SelectedIndex) == CheckState.Unchecked)
                    layerCheckedListBox.SetItemCheckState(layerCheckedListBox.SelectedIndex, CheckState.Checked);
            }
        }

        void layerCheckedListBox_GotFocus (object sender, EventArgs e)
        {
            this.Focus();
        }

        private void HandleScrollingTileDisplay (object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                tileDisplay1.WindowPosition.X = e.NewValue;
            else // e.ScrollOrientation == ScrollOrientation.VerticalScroll 
                tileDisplay1.WindowPosition.Y = e.NewValue;

            tileDisplay1.Invalidate();
        }

        private void HandleScrollingEditor (object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                editor1.WindowPosition.X = e.NewValue;
            else // e.ScrollOrientation == ScrollOrientation.VerticalScroll 
                editor1.WindowPosition.Y = e.NewValue;

            editor1.Invalidate();
        }
    }
}

            //this.tile1 = new WindowParts.Tile(editor1);
            //this.tileDisplay1 = new WindowParts.TileDisplay(editor1, tile1);