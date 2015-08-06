namespace MapEditor.Windows
{
    partial class CreateMap
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
            this.label1 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.notSaveButton = new System.Windows.Forms.Button();
            this.abortButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(518, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Eine neue Map wird erstellt. Was soll mit der alten Map gemacht werden.";
            // 
            // saveButton
            // 
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(12, 53);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(141, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Änderungen speichern";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // notSaveButton
            // 
            this.notSaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.notSaveButton.Location = new System.Drawing.Point(224, 53);
            this.notSaveButton.Name = "notSaveButton";
            this.notSaveButton.Size = new System.Drawing.Size(138, 23);
            this.notSaveButton.TabIndex = 2;
            this.notSaveButton.Text = "Änderungen verwerfen";
            this.notSaveButton.UseVisualStyleBackColor = true;
            this.notSaveButton.Click += new System.EventHandler(this.notSaveButton_Click);
            // 
            // abortButton
            // 
            this.abortButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.abortButton.Location = new System.Drawing.Point(488, 53);
            this.abortButton.Name = "abortButton";
            this.abortButton.Size = new System.Drawing.Size(75, 23);
            this.abortButton.TabIndex = 3;
            this.abortButton.Text = "Abrechen";
            this.abortButton.UseVisualStyleBackColor = true;
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
            // 
            // CreateMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 84);
            this.Controls.Add(this.abortButton);
            this.Controls.Add(this.notSaveButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label1);
            this.Name = "CreateMap";
            this.Text = "Neue Map";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button notSaveButton;
        private System.Windows.Forms.Button abortButton;
    }
}