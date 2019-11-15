using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Sales_System
{
    public partial class InitDbForm : Form
    {
        public InitDbForm()
        {
            InitializeComponent();
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            this.TaskProgress.Value = 0;

        }

        private void InitBtn_Click(object sender, EventArgs e)
        {
            this.TaskProgress.Value += 10;
        }

        private void EnterBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
