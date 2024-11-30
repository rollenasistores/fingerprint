namespace Fingerprint
{
    partial class capture
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
            this.components = new System.ComponentModel.Container();
            this.fImage = new System.Windows.Forms.PictureBox();
            this.Prompt = new System.Windows.Forms.TextBox();
            this.statusText = new System.Windows.Forms.TextBox();
            this.status_label = new System.Windows.Forms.Label();
            this.StartScan = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.staff_box = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.fImage)).BeginInit();
            this.SuspendLayout();
            // 
            // fImage
            // 
            this.fImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fImage.Location = new System.Drawing.Point(39, 55);
            this.fImage.Name = "fImage";
            this.fImage.Size = new System.Drawing.Size(246, 252);
            this.fImage.TabIndex = 0;
            this.fImage.TabStop = false;
            // 
            // Prompt
            // 
            this.Prompt.Location = new System.Drawing.Point(302, 55);
            this.Prompt.Name = "Prompt";
            this.Prompt.Size = new System.Drawing.Size(307, 20);
            this.Prompt.TabIndex = 1;
            // 
            // statusText
            // 
            this.statusText.Location = new System.Drawing.Point(302, 81);
            this.statusText.Multiline = true;
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(307, 226);
            this.statusText.TabIndex = 2;
            // 
            // status_label
            // 
            this.status_label.AutoSize = true;
            this.status_label.Location = new System.Drawing.Point(36, 360);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(56, 13);
            this.status_label.TabIndex = 3;
            this.status_label.Text = "[STATUS]";
            // 
            // StartScan
            // 
            this.StartScan.Location = new System.Drawing.Point(302, 350);
            this.StartScan.Name = "StartScan";
            this.StartScan.Size = new System.Drawing.Size(307, 44);
            this.StartScan.TabIndex = 5;
            this.StartScan.Text = "Start Scan";
            this.StartScan.UseVisualStyleBackColor = true;
            this.StartScan.Click += new System.EventHandler(this.StartScan_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(630, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // staff_box
            // 
            this.staff_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.staff_box.FormattingEnabled = true;
            this.staff_box.Location = new System.Drawing.Point(302, 313);
            this.staff_box.Name = "staff_box";
            this.staff_box.Size = new System.Drawing.Size(307, 21);
            this.staff_box.TabIndex = 7;
            this.staff_box.SelectedIndexChanged += new System.EventHandler(this.Staff_box_SelectedIndexChanged);
            // 
            // capture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 407);
            this.Controls.Add(this.staff_box);
            this.Controls.Add(this.StartScan);
            this.Controls.Add(this.status_label);
            this.Controls.Add(this.statusText);
            this.Controls.Add(this.Prompt);
            this.Controls.Add(this.fImage);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "capture";
            this.Text = "Barangay Navarro Fingerprint Scanner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Capture_FormClosing);
            this.Load += new System.EventHandler(this.Capture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox fImage;
        private System.Windows.Forms.TextBox Prompt;
        private System.Windows.Forms.TextBox statusText;
        private System.Windows.Forms.Label status_label;
        private System.Windows.Forms.Button StartScan;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ComboBox staff_box;
    }
}