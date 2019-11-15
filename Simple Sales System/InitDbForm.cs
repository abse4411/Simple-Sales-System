using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simple_Sales_System.ViewModels;

namespace Simple_Sales_System
{
    public partial class InitDbForm : Form
    {
        private InitDbViewModel _viewModel;
        public InitDbForm()
        {
            InitializeComponent();
            _viewModel=new InitDbViewModel();
            SetBindings();
            this.DialogResult = DialogResult.No;
        }

        private void SetBindings()
        {
            StringTB.DataBindings.Add(new Binding(nameof(StringTB.Text), _viewModel,nameof(_viewModel.ConnectionString)
                , true, DataSourceUpdateMode.OnPropertyChanged)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            MessageTB.DataBindings.Add(new Binding(nameof(MessageTB.Text), _viewModel, nameof(_viewModel.Message)
                , true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            TaskProgress.DataBindings.Add(new Binding(nameof(TaskProgress.Value), _viewModel,nameof(_viewModel.ProgressValue)
                , true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            TestBtn.DataBindings.Add(new Binding(nameof(TestBtn.Enabled), _viewModel, nameof(_viewModel.IsEnable)
                , true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            InitBtn.DataBindings.Add(new Binding(nameof(InitBtn.Enabled), _viewModel, nameof(_viewModel.IsEnable)
                , true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });

        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            EnterBtn.Enabled = _viewModel.TestConnection();
        }

        private async void InitBtn_Click(object sender, EventArgs e)
        {
            EnterBtn.Enabled =await _viewModel.InitDbAsync();
        }

        private void EnterBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void CreditsBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Icons made by Smashicons from www.flaticon.com", "Credits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
