using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Customization_Toolbox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            var quitchoice = MessageBox.Show("Are you sure you want to quit WCT?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (quitchoice == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main_UI frm = new Main_UI();
            frm.Show();
        }
    }
}
