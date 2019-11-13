using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simple_Sales_System.Services;

namespace Simple_Sales_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var result =await new FilePickerService().OpenImagePickerAsync();
            pictureBox1.Image = result?.ImageSource as Image;
        }
    }
}
