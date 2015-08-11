﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Xna.Framework;

using MapEditor.WindowParts;

namespace MapEditor.Windows
{
    public partial class CreateLayer : Form
    {
        Editor editor;
        TileDisplay tileDisplay;

        public CreateLayer (Editor editor, TileDisplay tileDisplay)
        {
            InitializeComponent();

            this.editor = editor;
            this.tileDisplay = tileDisplay;
        }

        private void abortButton_Click (object sender, EventArgs e)
        {
            Close();
        }

        private void createButton_Click (object sender, EventArgs e)
        {
            int tDimesionX = int.Parse(tileDimensionX.Text);
            int tDimesionY = int.Parse(tileDimensionY.Text);
            int offsetX = int.Parse(offsetXTextBox.Text);
            int offsetY = int.Parse(offsetYTextBox.Text);
            int collisionLevel = int.Parse(collisionLevelTextBox.Text);

            editor.Map.CreateNewLayer(editor.Content, new Vector2(tDimesionX, tDimesionY), new Vector2(offsetX, offsetY), collisionLevel, sheetPathTextBox.Text);

            editor.ResetSelector();
            tileDisplay.SetNewTileImage();

            Close();
        }
    }
}
