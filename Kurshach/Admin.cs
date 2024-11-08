using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurshach
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Autho Auth = new Autho();
            Auth.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Users Admin = new Users();
            Admin.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SchludeApproval Admin = new SchludeApproval();
            Admin.Show();
            this.Hide();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            label1.Text = Dannie.FirstName;
            label2.Text = Dannie.LastName;
        }
    }
}
