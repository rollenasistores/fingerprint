namespace Fingerprint
{
    partial class main
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
            this.enroll_btn = new System.Windows.Forms.Button();
            this.verify_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // enroll_btn
            // 
            this.enroll_btn.Location = new System.Drawing.Point(12, 11);
            this.enroll_btn.Name = "enroll_btn";
            this.enroll_btn.Size = new System.Drawing.Size(269, 90);
            this.enroll_btn.TabIndex = 0;
            this.enroll_btn.Text = "Enroll";
            this.enroll_btn.UseVisualStyleBackColor = true;
            this.enroll_btn.Click += new System.EventHandler(this.Enroll_btn_Click);
            // 
            // verify_btn
            // 
            this.verify_btn.Location = new System.Drawing.Point(12, 107);
            this.verify_btn.Name = "verify_btn";
            this.verify_btn.Size = new System.Drawing.Size(269, 90);
            this.verify_btn.TabIndex = 1;
            this.verify_btn.Text = "Attendance";
            this.verify_btn.UseVisualStyleBackColor = true;
            this.verify_btn.Click += new System.EventHandler(this.Verify_btn_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 210);
            this.Controls.Add(this.verify_btn);
            this.Controls.Add(this.enroll_btn);
            this.Name = "main";
            this.Text = "Barangay Navarro";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button enroll_btn;
        private System.Windows.Forms.Button verify_btn;
    }
}

