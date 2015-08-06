using System;
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
            editor.Map = null;
            Close();
        }

        private void createButton_Click (object sender, EventArgs e)
        {
            int tDimesionX = int.Parse(tileDimensionX.Text);
            int tDimesionY = int.Parse(tileDimensionY.Text);

            editor.Map.CreateNewLayer(editor.Content, new Vector2(tDimesionX, tDimesionY), sheetPathTextBox.Text);

            editor.Map.Initialize(editor.Content);
            editor.ResetSelector();
            tileDisplay.SetNewTileImage();

            Close();
        }
    }
}
