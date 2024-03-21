using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gui
{
    public partial class notepadDevelopers : Form
    {
        public notepadDevelopers()
        {
            InitializeComponent();
        }

        private void notepadDevelopers_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            string url = "https://www.linkedin.com/in/monal-gajanan-sutar-13a9a6235/";
            Process.Start(url);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/monalsutar";
            Process.Start(url);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            string url = "https://www.linkedin.com/in/rupeshkumar-bhosale-681b63255/";
            Process.Start(url);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/rupesh2004/";
            Process.Start(url);

        }
    }
}
