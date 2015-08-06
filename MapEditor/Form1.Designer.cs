﻿namespace MapEditor
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
            this.layerComboBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hinzufügenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neueMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapÖffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.speichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernAlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // editor1
            // 
            this.editor1.Location = new System.Drawing.Point(12, 35);
            this.editor1.Name = "editor1";
            this.editor1.Size = new System.Drawing.Size(640, 480);
            this.editor1.TabIndex = 0;
            this.editor1.Text = "editor1";
            // 
            // tile1
            // 
            this.tile1.Location = new System.Drawing.Point(659, 522);
            this.tile1.Name = "tile1";
            this.tile1.Size = new System.Drawing.Size(64, 64);
            this.tile1.TabIndex = 2;
            this.tile1.Text = "tile1";
            // 
            // tileDisplay1
            // 
            this.tileDisplay1.Location = new System.Drawing.Point(659, 35);
            this.tileDisplay1.Name = "tileDisplay1";
            this.tileDisplay1.Size = new System.Drawing.Size(256, 480);
            this.tileDisplay1.TabIndex = 1;
            this.tileDisplay1.Text = "tileDisplay1";
            // 
            // rotateLeftButton
            // 
            this.rotateLeftButton.Location = new System.Drawing.Point(730, 522);
            this.rotateLeftButton.Name = "rotateLeftButton";
            this.rotateLeftButton.Size = new System.Drawing.Size(97, 23);
            this.rotateLeftButton.TabIndex = 3;
            this.rotateLeftButton.Text = "Links drehen";
            this.rotateLeftButton.UseVisualStyleBackColor = true;
            // 
            // rotateRightButton
            // 
            this.rotateRightButton.Location = new System.Drawing.Point(730, 559);
            this.rotateRightButton.Name = "rotateRightButton";
            this.rotateRightButton.Size = new System.Drawing.Size(97, 23);
            this.rotateRightButton.TabIndex = 4;
            this.rotateRightButton.Text = "Rechts drehen";
            this.rotateRightButton.UseVisualStyleBackColor = true;
            // 
            // layerComboBox
            // 
            this.layerComboBox.FormattingEnabled = true;
            this.layerComboBox.Location = new System.Drawing.Point(12, 522);
            this.layerComboBox.Name = "layerComboBox";
            this.layerComboBox.Size = new System.Drawing.Size(121, 21);
            this.layerComboBox.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.hinzufügenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(931, 24);
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
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // hinzufügenToolStripMenuItem
            // 
            this.hinzufügenToolStripMenuItem.Name = "hinzufügenToolStripMenuItem";
            this.hinzufügenToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.hinzufügenToolStripMenuItem.Text = "Hinzufügen";
            // 
            // neueMapToolStripMenuItem
            // 
            this.neueMapToolStripMenuItem.Name = "neueMapToolStripMenuItem";
            this.neueMapToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.neueMapToolStripMenuItem.Text = "Neue Map";
            // 
            // mapÖffnenToolStripMenuItem
            // 
            this.mapÖffnenToolStripMenuItem.Name = "mapÖffnenToolStripMenuItem";
            this.mapÖffnenToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.mapÖffnenToolStripMenuItem.Text = "Map öffnen";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 6);
            // 
            // speichernToolStripMenuItem
            // 
            this.speichernToolStripMenuItem.Name = "speichernToolStripMenuItem";
            this.speichernToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.speichernToolStripMenuItem.Text = "Speichern";
            // 
            // speichernAlsToolStripMenuItem
            // 
            this.speichernAlsToolStripMenuItem.Name = "speichernAlsToolStripMenuItem";
            this.speichernAlsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.speichernAlsToolStripMenuItem.Text = "Speichern als ...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 601);
            this.Controls.Add(this.layerComboBox);
            this.Controls.Add(this.rotateRightButton);
            this.Controls.Add(this.rotateLeftButton);
            this.Controls.Add(this.tile1);
            this.Controls.Add(this.tileDisplay1);
            this.Controls.Add(this.editor1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
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
        private System.Windows.Forms.ComboBox layerComboBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neueMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapÖffnenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem speichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speichernAlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hinzufügenToolStripMenuItem;
    }
}

