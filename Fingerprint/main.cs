using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fingerprint
{
    delegate void Function();
    public partial class main : Form
    {

        private DPFP.Template Template;

        public main()
        {
            InitializeComponent();
        }

        private void OnTemplate(DPFP.Template template)
        {
            this.Invoke(new Function(delegate ()
            {
                Template = template;

                if(Template != null)
                {
                    MessageBox.Show("The Fingerprint template is ready for scan", "Fingerprint Enrollment");
                }else
                {
                    MessageBox.Show("The fingerprint template is not valid. Repeat Fingerprint Scanning", "Fingerprint Enrollment");
                }
            }));
        }

        private void Enroll_btn_Click(object sender, EventArgs e)
        {
            // Create and configure the enroll form
            enroll EnFrm = new enroll();
            EnFrm.OnTemplate += this.OnTemplate;

            // Attach a handler to show the main form when the enroll form is closed
            EnFrm.FormClosed += (s, args) => this.Show();

            // Hide the main form and show the enroll form
            this.Hide();
            EnFrm.Show();
        }

        private void Verify_btn_Click(object sender, EventArgs e)
        {
            // Create and configure the verify form
            verify VeFrm = new verify();
            VeFrm.Verify(Template);

            // Attach a handler to show the main form when the verify form is closed
            VeFrm.FormClosed += (s, args) => this.Show();

        }

    }
}
