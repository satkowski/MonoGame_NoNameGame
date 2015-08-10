namespace MapEditor
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent ()
        {
            this.editor1 = new MapEditor.WindowParts.Editor();
            this.tile1 = new WindowParts.Tile(editor1);
            this.tileDisplay1 = new WindowParts.TileDisplay(editor1, tile1);
            this.rotateLeftButton = new System.Windows.Forms.Button();
            this.rotateRightButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neueMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapÖffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.speichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernAlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hinzufügenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuesLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.offsetYTextBox = new System.Windows.Forms.TextBox();
            this.offsetXTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tileSheetTextBox = new System.Windows.Forms.TextBox();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.resetChangesButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.editorHScrollBar = new System.Windows.Forms.HScrollBar();
            this.tileDisplayHScrollBar = new System.Windows.Forms.HScrollBar();
            this.editorVScrollBar = new System.Windows.Forms.VScrollBar();
            this.tileDisplayVScrollBar = new System.Windows.Forms.VScrollBar();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // editor1
            // 
            this.editor1.ActualLayerSizeX = 0;
            this.editor1.ActualLayerSizeY = 0;
            this.editor1.Location = new System.Drawing.Point(12, 35);
            this.editor1.Name = "editor1";
            this.editor1.Size = new System.Drawing.Size(640, 480);
            this.editor1.TabIndex = 0;
            this.editor1.Text = "editor1";
            // 
            // tile1
            // 
            this.tile1.Location = new System.Drawing.Point(804, 550);
            this.tile1.Name = "tile1";
            this.tile1.Size = new System.Drawing.Size(64, 64);
            this.tile1.TabIndex = 2;
            this.tile1.Text = "tile1";
            // 
            // tileDisplay1
            // 
            this.tileDisplay1.Location = new System.Drawing.Point(687, 35);
            this.tileDisplay1.Name = "tileDisplay1";
            this.tileDisplay1.Size = new System.Drawing.Size(320, 480);
            this.tileDisplay1.TabIndex = 1;
            this.tileDisplay1.Text = "tileDisplay1";
            // 
            // rotateLeftButton
            // 
            this.rotateLeftButton.Location = new System.Drawing.Point(875, 550);
            this.rotateLeftButton.Name = "rotateLeftButton";
            this.rotateLeftButton.Size = new System.Drawing.Size(97, 23);
            this.rotateLeftButton.TabIndex = 3;
            this.rotateLeftButton.Text = "Links drehen";
            this.rotateLeftButton.UseVisualStyleBackColor = true;
            this.rotateLeftButton.Click += new System.EventHandler(this.rotateLeftButton_Click);
            // 
            // rotateRightButton
            // 
            this.rotateRightButton.Location = new System.Drawing.Point(875, 587);
            this.rotateRightButton.Name = "rotateRightButton";
            this.rotateRightButton.Size = new System.Drawing.Size(97, 23);
            this.rotateRightButton.TabIndex = 4;
            this.rotateRightButton.Text = "Rechts drehen";
            this.rotateRightButton.UseVisualStyleBackColor = true;
            this.rotateRightButton.Click += new System.EventHandler(this.rotateRightButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.hinzufügenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1038, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neueMapToolStripMenuItem,
            this.mapÖffnenToolStripMenuItem,
            this.toolStripMenuItem1,
            this.speichernToolStripMenuItem,
            this.speichernAlsToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // neueMapToolStripMenuItem
            // 
            this.neueMapToolStripMenuItem.Name = "neueMapToolStripMenuItem";
            this.neueMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.neueMapToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.neueMapToolStripMenuItem.Text = "Neue Map";
            this.neueMapToolStripMenuItem.Click += new System.EventHandler(this.neueMapToolStripMenuItem_Click);
            // 
            // mapÖffnenToolStripMenuItem
            // 
            this.mapÖffnenToolStripMenuItem.Name = "mapÖffnenToolStripMenuItem";
            this.mapÖffnenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mapÖffnenToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.mapÖffnenToolStripMenuItem.Text = "Map öffnen";
            this.mapÖffnenToolStripMenuItem.Click += new System.EventHandler(this.mapÖffnenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(277, 6);
            // 
            // speichernToolStripMenuItem
            // 
            this.speichernToolStripMenuItem.Name = "speichernToolStripMenuItem";
            this.speichernToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.speichernToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.speichernToolStripMenuItem.Text = "Speichern";
            this.speichernToolStripMenuItem.Click += new System.EventHandler(this.speichernToolStripMenuItem_Click);
            // 
            // speichernAlsToolStripMenuItem
            // 
            this.speichernAlsToolStripMenuItem.Name = "speichernAlsToolStripMenuItem";
            this.speichernAlsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.speichernAlsToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.speichernAlsToolStripMenuItem.Text = "Speichern als ...";
            this.speichernAlsToolStripMenuItem.Click += new System.EventHandler(this.speichernAlsToolStripMenuItem_Click);
            // 
            // hinzufügenToolStripMenuItem
            // 
            this.hinzufügenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neuesLayerToolStripMenuItem});
            this.hinzufügenToolStripMenuItem.Name = "hinzufügenToolStripMenuItem";
            this.hinzufügenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
            this.hinzufügenToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.hinzufügenToolStripMenuItem.Text = "Hinzufügen";
            // 
            // neuesLayerToolStripMenuItem
            // 
            this.neuesLayerToolStripMenuItem.Name = "neuesLayerToolStripMenuItem";
            this.neuesLayerToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.neuesLayerToolStripMenuItem.Text = "Neues Layer ...";
            this.neuesLayerToolStripMenuItem.Click += new System.EventHandler(this.neuesLayerToolStripMenuItem_Click);
            // 
            // layerCheckedListBox
            // 
            this.layerCheckedListBox.FormattingEnabled = true;
            this.layerCheckedListBox.Location = new System.Drawing.Point(12, 550);
            this.layerCheckedListBox.Name = "layerCheckedListBox";
            this.layerCheckedListBox.Size = new System.Drawing.Size(120, 79);
            this.layerCheckedListBox.TabIndex = 7;
            this.layerCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.layerCheckedListBox_ItemCheck);
            this.layerCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.layerCheckedListBox_SelectedIndexChanged);
            // 
            // offsetYTextBox
            // 
            this.offsetYTextBox.Enabled = false;
            this.offsetYTextBox.Location = new System.Drawing.Point(457, 579);
            this.offsetYTextBox.Name = "offsetYTextBox";
            this.offsetYTextBox.Size = new System.Drawing.Size(100, 20);
            this.offsetYTextBox.TabIndex = 23;
            // 
            // offsetXTextBox
            // 
            this.offsetXTextBox.Enabled = false;
            this.offsetXTextBox.Location = new System.Drawing.Point(328, 580);
            this.offsetXTextBox.Name = "offsetXTextBox";
            this.offsetXTextBox.Size = new System.Drawing.Size(100, 20);
            this.offsetXTextBox.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(436, 580);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(307, 583);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(238, 582);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Offset";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(238, 608);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Tile Sheet";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(213, 554);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Layer Eigentschaften";
            // 
            // tileSheetTextBox
            // 
            this.tileSheetTextBox.Enabled = false;
            this.tileSheetTextBox.Location = new System.Drawing.Point(310, 606);
            this.tileSheetTextBox.Name = "tileSheetTextBox";
            this.tileSheetTextBox.Size = new System.Drawing.Size(247, 20);
            this.tileSheetTextBox.TabIndex = 29;
            // 
            // saveChangesButton
            // 
            this.saveChangesButton.Enabled = false;
            this.saveChangesButton.Location = new System.Drawing.Point(582, 576);
            this.saveChangesButton.Name = "saveChangesButton";
            this.saveChangesButton.Size = new System.Drawing.Size(130, 23);
            this.saveChangesButton.TabIndex = 30;
            this.saveChangesButton.Text = "Änderung speichern";
            this.saveChangesButton.UseVisualStyleBackColor = true;
            this.saveChangesButton.Click += new System.EventHandler(this.saveChangesButton_Click);
            // 
            // resetChangesButton
            // 
            this.resetChangesButton.Enabled = false;
            this.resetChangesButton.Location = new System.Drawing.Point(582, 606);
            this.resetChangesButton.Name = "resetChangesButton";
            this.resetChangesButton.Size = new System.Drawing.Size(130, 23);
            this.resetChangesButton.TabIndex = 31;
            this.resetChangesButton.Text = "Änderung zurücksetzen";
            this.resetChangesButton.UseVisualStyleBackColor = true;
            this.resetChangesButton.Click += new System.EventHandler(this.resetChangesButton_Click);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(138, 577);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(45, 23);
            this.upButton.TabIndex = 32;
            this.upButton.Text = "Up";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(138, 606);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(45, 23);
            this.downButton.TabIndex = 33;
            this.downButton.Text = "Down";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // editorHScrollBar
            // 
            this.editorHScrollBar.LargeChange = 1;
            this.editorHScrollBar.Location = new System.Drawing.Point(12, 515);
            this.editorHScrollBar.Maximum = 0;
            this.editorHScrollBar.Name = "editorHScrollBar";
            this.editorHScrollBar.Size = new System.Drawing.Size(640, 20);
            this.editorHScrollBar.TabIndex = 34;
            this.editorHScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HandleScrollingEditor);
            // 
            // tileDisplayHScrollBar
            // 
            this.tileDisplayHScrollBar.Enabled = false;
            this.tileDisplayHScrollBar.Location = new System.Drawing.Point(687, 515);
            this.tileDisplayHScrollBar.Name = "tileDisplayHScrollBar";
            this.tileDisplayHScrollBar.Size = new System.Drawing.Size(320, 20);
            this.tileDisplayHScrollBar.TabIndex = 35;
            this.tileDisplayHScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HandleScrollingTileDisplay);
            // 
            // editorVScrollBar
            // 
            this.editorVScrollBar.LargeChange = 1;
            this.editorVScrollBar.Location = new System.Drawing.Point(652, 35);
            this.editorVScrollBar.Maximum = 0;
            this.editorVScrollBar.Name = "editorVScrollBar";
            this.editorVScrollBar.Size = new System.Drawing.Size(20, 480);
            this.editorVScrollBar.TabIndex = 36;
            this.editorVScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HandleScrollingEditor);
            // 
            // tileDisplayVScrollBar
            // 
            this.tileDisplayVScrollBar.Enabled = false;
            this.tileDisplayVScrollBar.Location = new System.Drawing.Point(1007, 35);
            this.tileDisplayVScrollBar.Name = "tileDisplayVScrollBar";
            this.tileDisplayVScrollBar.Size = new System.Drawing.Size(20, 480);
            this.tileDisplayVScrollBar.TabIndex = 37;
            this.tileDisplayVScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HandleScrollingTileDisplay);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 644);
            this.Controls.Add(this.tileDisplayVScrollBar);
            this.Controls.Add(this.editorVScrollBar);
            this.Controls.Add(this.tileDisplayHScrollBar);
            this.Controls.Add(this.editorHScrollBar);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.resetChangesButton);
            this.Controls.Add(this.saveChangesButton);
            this.Controls.Add(this.tileSheetTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.offsetYTextBox);
            this.Controls.Add(this.offsetXTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.layerCheckedListBox);
            this.Controls.Add(this.rotateRightButton);
            this.Controls.Add(this.rotateLeftButton);
            this.Controls.Add(this.tile1);
            this.Controls.Add(this.tileDisplay1);
            this.Controls.Add(this.editor1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Map Editor - NoNameGame";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WindowParts.Editor editor1;
        private WindowParts.TileDisplay tileDisplay1;
        private WindowParts.Tile tile1;
        private System.Windows.Forms.Button rotateLeftButton;
        private System.Windows.Forms.Button rotateRightButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neueMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapÖffnenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem speichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speichernAlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hinzufügenToolStripMenuItem;
        private System.Windows.Forms.CheckedListBox layerCheckedListBox;
        private System.Windows.Forms.ToolStripMenuItem neuesLayerToolStripMenuItem;
        private System.Windows.Forms.TextBox offsetYTextBox;
        private System.Windows.Forms.TextBox offsetXTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tileSheetTextBox;
        private System.Windows.Forms.Button saveChangesButton;
        private System.Windows.Forms.Button resetChangesButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.HScrollBar editorHScrollBar;
        private System.Windows.Forms.HScrollBar tileDisplayHScrollBar;
        private System.Windows.Forms.VScrollBar editorVScrollBar;
        private System.Windows.Forms.VScrollBar tileDisplayVScrollBar;
    }
}

