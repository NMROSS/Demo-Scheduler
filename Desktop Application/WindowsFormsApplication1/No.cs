using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class No : Form
    {

        /*
         * This form is just here to show that something in the system went wrong
         * The code should not allow for this form to appear, as the buttons are unclickable in the cases that this could be triggered
         */

        public Main mainForm;

        public No(Form MainForm)
        {
            InitializeComponent();
            mainForm = MainForm as Main;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void No_Load(object sender, EventArgs e)
        {
            this.BackColor = mainForm.form_colour;
        }
    }
}
