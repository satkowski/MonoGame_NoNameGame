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

namespace MapEditor
{
    public partial class Form1 : Form
    {
        public Form1 ()
        {
            InitializeComponent();
        }

        private void speichernAlsToolStripMenuItem_Click (object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Xml File (.xml)|*.xml";
            sfd.Title = "Speicher Map";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                editor1.Map.Save(sfd.FileName);
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
    }
}

            //this.tile1 = new WindowParts.Tile(editor1);
            //this.tileDisplay1 = new WindowParts.TileDisplay(editor1, tile1);