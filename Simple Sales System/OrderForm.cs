using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simple_Sales_System.Data;
using Simple_Sales_System.Services;
using Simple_Sales_System.ViewModels;

namespace Simple_Sales_System
{
    public partial class OrderForm : Form
    {
        private Shoes _shoes;
        public ShoesDetailsViewModel ViewModel { get; }
        public OrderForm(Shoes shoes)
        {
            _shoes = shoes;
            InitializeComponent();
            ViewModel=new ShoesDetailsViewModel(new ShoesService(), new FilePickerService(), new DialogService());
            SetBindings();
        }

        private void SetBindings()
        {
            pictureBox1.DataBindings.Add(new Binding(nameof(pictureBox1.Image), ViewModel, nameof(ViewModel.ImageSource),
                true, DataSourceUpdateMode.OnPropertyChanged)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox1.DataBindings.Add(new Binding(nameof(textBox1.Text), ViewModel.EditableItem,
                nameof(ViewModel.EditableItem.Model),
                true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox2.DataBindings.Add(new Binding(nameof(textBox2.Text), ViewModel.EditableItem,
                nameof(ViewModel.EditableItem.Origin),
                true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox3.DataBindings.Add(new Binding(nameof(textBox3.Text), ViewModel.EditableItem,
                nameof(ViewModel.EditableItem.Price),
                true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox4.DataBindings.Add(new Binding(nameof(textBox4.Text), ViewModel.EditableItem,
                nameof(ViewModel.EditableItem.Stocks),
                true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await ViewModel.LoadAsync(_shoes);
        }
    }
}
