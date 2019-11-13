using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Sales_System.Services
{
    public class DialogService:IDialogService
    {
        public void ShowException(Exception e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowWarning(string title, string content)
        {
            MessageBox.Show(content, title,MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void ShowMessage(string title, string content)
        {
            MessageBox.Show(content, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
