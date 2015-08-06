namespace MapEditor.Windows
{
    partial class CreateLayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.abortButton = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tileDimensionX = new System.Windows.Forms.TextBox();
            this.tileDimensionY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sheetPathTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // abortButton
            // 
            this.abortButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.abortButton.Location = new System.Drawing.Point(586, 23);
            this.abortButton.Name = "abortButton";
            this.abortButton.Size = new System.Drawing.Size(75, 23);
            this.abortButton.TabIndex = 18;
            this.abortButton.Text = "Abbrechen";
            this.abortButton.UseVisualStyleBackColor = true;
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
            // 
            // createButton
            // 
            this.createButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.createButton.Location = new System.Drawing.Point(505, 23);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(75, 23);
            this.createButton.TabIndex = 17;
            this.createButton.Text = "Erstellen";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tile Dimension";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(261, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Y";
            // 
            // tileDimensionX
            // 
            this.tileDimensionX.Location = new System.Drawing.Point(153, 18);
            this.tileDimensionX.Name = "tileDimensionX";
            this.tileDimensionX.Size = new System.Drawing.Size(100, 20);
            this.tileDimensionX.TabIndex = 9;
            this.tileDimensionX.Text = "32";
            // 
            // tileDimensionY
            // 
            this.tileDimensionY.Location = new System.Drawing.Point(282, 18);
            this.tileDimensionY.Name = "tileDimensionY";
            this.tileDimensionY.Size = new System.Drawing.Size(100, 20);
            this.tileDimensionY.TabIndex = 10;
            this.tileDimensionY.Text = "32";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Tile Sheets";
            // 
            // sheetPathTextBox
            // 
            this.sheetPathTextBox.Location = new System.Drawing.Point(134, 47);
            this.sheetPathTextBox.Name = "sheetPathTextBox";
            this.sheetPathTextBox.Size = new System.Drawing.Size(260, 20);
            this.sheetPathTextBox.TabIndex = 13;
            // 
            // CreateLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 76);
            this.Controls.Add(this.sheetPathTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tileDimensionY);
            this.Controls.Add(this.tileDimensionX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.abortButton);
            this.Name = "CreateLayer";
            this.Text = "Neues Layer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button abortButton;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tileDimensionX;
        private System.Windows.Forms.TextBox tileDimensionY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sheetPathTextBox;
    }
}