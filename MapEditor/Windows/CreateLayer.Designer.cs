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
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.offsetXTextBox = new System.Windows.Forms.TextBox();
            this.offsetYTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.collisionLevelTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // abortButton
            // 
            this.abortButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.abortButton.Location = new System.Drawing.Point(449, 128);
            this.abortButton.Name = "abortButton";
            this.abortButton.Size = new System.Drawing.Size(75, 23);
            this.abortButton.TabIndex = 15;
            this.abortButton.Text = "Abbrechen";
            this.abortButton.UseVisualStyleBackColor = true;
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
            // 
            // createButton
            // 
            this.createButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.createButton.Location = new System.Drawing.Point(449, 91);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(75, 23);
            this.createButton.TabIndex = 14;
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
            this.label1.Location = new System.Drawing.Point(13, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Tile Sheets";
            // 
            // sheetPathTextBox
            // 
            this.sheetPathTextBox.Location = new System.Drawing.Point(135, 133);
            this.sheetPathTextBox.Name = "sheetPathTextBox";
            this.sheetPathTextBox.Size = new System.Drawing.Size(260, 20);
            this.sheetPathTextBox.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Offset";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(132, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(261, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Y";
            // 
            // offsetXTextBox
            // 
            this.offsetXTextBox.Location = new System.Drawing.Point(153, 57);
            this.offsetXTextBox.Name = "offsetXTextBox";
            this.offsetXTextBox.Size = new System.Drawing.Size(100, 20);
            this.offsetXTextBox.TabIndex = 11;
            this.offsetXTextBox.Text = "0";
            // 
            // offsetYTextBox
            // 
            this.offsetYTextBox.Location = new System.Drawing.Point(282, 56);
            this.offsetYTextBox.Name = "offsetYTextBox";
            this.offsetYTextBox.Size = new System.Drawing.Size(100, 20);
            this.offsetYTextBox.TabIndex = 12;
            this.offsetYTextBox.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Kollisions Level";
            // 
            // collisionLevelTextBox
            // 
            this.collisionLevelTextBox.Location = new System.Drawing.Point(136, 93);
            this.collisionLevelTextBox.Name = "collisionLevelTextBox";
            this.collisionLevelTextBox.Size = new System.Drawing.Size(259, 20);
            this.collisionLevelTextBox.TabIndex = 23;
            this.collisionLevelTextBox.Text = "-1";
            // 
            // CreateLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 161);
            this.Controls.Add(this.collisionLevelTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.offsetYTextBox);
            this.Controls.Add(this.offsetXTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox offsetXTextBox;
        private System.Windows.Forms.TextBox offsetYTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox collisionLevelTextBox;
    }
}