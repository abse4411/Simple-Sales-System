using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Sales_System.Services
{
    public interface IDialogService
    {
        void ShowException(Exception e);
        void ShowWarning(string title, string content);
        void ShowMessage(string title, string content);
    }
}
