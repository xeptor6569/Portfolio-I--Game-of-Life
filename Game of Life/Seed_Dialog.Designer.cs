namespace Game_of_Life
{
    partial class Seed_Dialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
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
        private void InitializeComponent()
        {
            this.seedLabel = new System.Windows.Forms.Label();
            this.seedUpDown = new System.Windows.Forms.NumericUpDown();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.randomSeedButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.seedUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // seedLabel
            // 
            this.seedLabel.AutoSize = true;
            this.seedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seedLabel.Location = new System.Drawing.Point(71, 56);
            this.seedLabel.Name = "seedLabel";
            this.seedLabel.Size = new System.Drawing.Size(41, 16);
            this.seedLabel.TabIndex = 0;
            this.seedLabel.Text = "Seed";
            // 
            // seedUpDown
            // 
            this.seedUpDown.Location = new System.Drawing.Point(118, 56);
            this.seedUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.seedUpDown.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.seedUpDown.Name = "seedUpDown";
            this.seedUpDown.Size = new System.Drawing.Size(120, 20);
            this.seedUpDown.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(74, 117);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(199, 117);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // randomSeedButton
            // 
            this.randomSeedButton.Location = new System.Drawing.Point(245, 52);
            this.randomSeedButton.Name = "randomSeedButton";
            this.randomSeedButton.Size = new System.Drawing.Size(75, 23);
            this.randomSeedButton.TabIndex = 4;
            this.randomSeedButton.Text = "Randomize";
            this.randomSeedButton.UseVisualStyleBackColor = true;
            this.randomSeedButton.Click += new System.EventHandler(this.randomSeedButton_Click);
            // 
            // Seed_Dialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(357, 156);
            this.Controls.Add(this.randomSeedButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.seedUpDown);
            this.Controls.Add(this.seedLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Seed_Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seed_Dialog";
            ((System.ComponentModel.ISupportInitialize)(this.seedUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label seedLabel;
        private System.Windows.Forms.NumericUpDown seedUpDown;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button randomSeedButton;
    }
}