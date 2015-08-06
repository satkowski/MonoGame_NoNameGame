using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MapEditor.WindowParts;
using MapEditor.Maps;

namespace MapEditor.Windows
{
    public partial class CreateMap : Form
    {
        Editor editor;

        public CreateMap (Editor editor)
        {
            InitializeComponent();

            this.editor = editor;
        }

        private void abortButton_Click (object sender, EventArgs e)
        {
            Close();
        }

        private void notSaveButton_Click (object sender, EventArgs e)
        {
            createNewMap();
            Close();
        }

        private void saveButton_Click (object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Xml File (.xml)|*.xml";
            sfd.Title = "Speicher Map";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                editor.Map.Save(sfd.FileName);
                createNewMap();
                Close();
            }
        }

        private void createNewMap ()
        {
            Map newMap = new Map();
            newMap.Initialize(editor.Content);
            editor.Map = newMap;

            Close();
        }
    }
}
