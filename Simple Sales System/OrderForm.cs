using System;
using System.Windows.Forms;
using Simple_Sales_System.Data;
using Simple_Sales_System.Services;
using Simple_Sales_System.ViewModels;

namespace Simple_Sales_System
{
    public partial class OrderForm : Form
    {
        private ShoesOrderViewModel _viewModel;
        public OrderForm(Shoes shoes)
        {
            InitializeComponent();
            _viewModel = new ShoesOrderViewModel(shoes);
            SetBindings();
            _viewModel.LoadShoesDetailsAsync();
        }

        private void SetBindings()
        {
            pictureBox1.DataBindings.Add(new Binding(nameof(pictureBox1.Image), _viewModel.ShoesDetails, nameof(_viewModel.ShoesDetails.ImageSource),
                true, DataSourceUpdateMode.OnPropertyChanged)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox1.DataBindings.Add(new Binding(nameof(textBox1.Text), _viewModel.ShoesDetails.EditableItem,
                nameof(_viewModel.ShoesDetails.EditableItem.Model),
                true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox2.DataBindings.Add(new Binding(nameof(textBox2.Text), _viewModel.ShoesDetails.EditableItem,
                nameof(_viewModel.ShoesDetails.EditableItem.Origin),
                true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox3.DataBindings.Add(new Binding(nameof(textBox3.Text), _viewModel.ShoesDetails.EditableItem,
                nameof(_viewModel.ShoesDetails.EditableItem.Price),
                true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox4.DataBindings.Add(new Binding(nameof(textBox4.Text), _viewModel.ShoesDetails.EditableItem,
                nameof(_viewModel.ShoesDetails.EditableItem.Stock),
                true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });

            textBox5.DataBindings.Add(new Binding(nameof(textBox5.Text), _viewModel.EditableItem,
                nameof(_viewModel.EditableItem.CustomerName),
                true, DataSourceUpdateMode.OnPropertyChanged)
            {
                ControlUpdateMode = ControlUpdateMode.Never
            });
            textBox6.DataBindings.Add(new Binding(nameof(textBox6.Text), _viewModel.EditableItem,
                nameof(_viewModel.EditableItem.PhoneNumber),
                true, DataSourceUpdateMode.OnPropertyChanged)
            {
                ControlUpdateMode = ControlUpdateMode.Never
            });
            textBox7.DataBindings.Add(new Binding(nameof(textBox7.Text), _viewModel.EditableItem,
                nameof(_viewModel.EditableItem.Quantity),
                true, DataSourceUpdateMode.OnPropertyChanged)
            {
                ControlUpdateMode = ControlUpdateMode.Never
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (_viewModel.Validate())
            {
                await _viewModel.SaveAsync();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
