namespace Neural_network
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.BrowseBox = new System.Windows.Forms.TextBox();
            this.RawFileData = new System.Windows.Forms.TextBox();
            this.NeuralData = new System.Windows.Forms.TextBox();
            this.KinematicData = new System.Windows.Forms.TextBox();
            this.Sort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 43);
            this.button1.TabIndex = 0;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BrowseBox
            // 
            this.BrowseBox.Location = new System.Drawing.Point(168, 12);
            this.BrowseBox.Multiline = true;
            this.BrowseBox.Name = "BrowseBox";
            this.BrowseBox.Size = new System.Drawing.Size(1525, 43);
            this.BrowseBox.TabIndex = 1;
            // 
            // RawFileData
            // 
            this.RawFileData.Location = new System.Drawing.Point(12, 61);
            this.RawFileData.Multiline = true;
            this.RawFileData.Name = "RawFileData";
            this.RawFileData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RawFileData.Size = new System.Drawing.Size(608, 1074);
            this.RawFileData.TabIndex = 2;
            // 
            // NeuralData
            // 
            this.NeuralData.Location = new System.Drawing.Point(637, 61);
            this.NeuralData.Multiline = true;
            this.NeuralData.Name = "NeuralData";
            this.NeuralData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NeuralData.Size = new System.Drawing.Size(608, 334);
            this.NeuralData.TabIndex = 3;
            this.NeuralData.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // KinematicData
            // 
            this.KinematicData.Location = new System.Drawing.Point(1251, 61);
            this.KinematicData.Multiline = true;
            this.KinematicData.Name = "KinematicData";
            this.KinematicData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.KinematicData.Size = new System.Drawing.Size(597, 334);
            this.KinematicData.TabIndex = 4;
            this.KinematicData.Text = " ";
            // 
            // Sort
            // 
            this.Sort.Location = new System.Drawing.Point(1699, 12);
            this.Sort.Name = "Sort";
            this.Sort.Size = new System.Drawing.Size(148, 43);
            this.Sort.TabIndex = 5;
            this.Sort.Text = "Sort";
            this.Sort.UseVisualStyleBackColor = true;
            this.Sort.Click += new System.EventHandler(this.Sort_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1859, 1147);
            this.Controls.Add(this.Sort);
            this.Controls.Add(this.KinematicData);
            this.Controls.Add(this.NeuralData);
            this.Controls.Add(this.RawFileData);
            this.Controls.Add(this.BrowseBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = " ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox BrowseBox;
        private System.Windows.Forms.TextBox RawFileData;
        private System.Windows.Forms.TextBox NeuralData;
        private System.Windows.Forms.TextBox KinematicData;
        private System.Windows.Forms.Button Sort;
    }
}

