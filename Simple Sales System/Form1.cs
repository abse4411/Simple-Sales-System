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
using Simple_Sales_System.ViewModels;

namespace Simple_Sales_System
{
    public partial class Form1 : Form
    {
        private readonly ShoesDetailsViewModel _viewModel;
        private FilePickerService _filePickerService;
        private ShoesService _shoesService;
        private DialogService dialogService;
        public Form1()
        {
            InitializeComponent();
            _filePickerService = new FilePickerService();
            _shoesService=new ShoesService();
            dialogService=new DialogService();
            _viewModel =new ShoesDetailsViewModel(_shoesService, _filePickerService, dialogService);
            pictureBox1.DataBindings.Add(new Binding(nameof(pictureBox1.Image), _viewModel, nameof(_viewModel.ImageSource),
                true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox1.DataBindings.Add(new Binding(nameof(textBox1.Text), _viewModel.EditableItem, nameof(_viewModel.EditableItem.Model),
                true, DataSourceUpdateMode.OnPropertyChanged)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox2.DataBindings.Add(new Binding(nameof(textBox2.Text), _viewModel.EditableItem, nameof(_viewModel.EditableItem.Origin),
                true, DataSourceUpdateMode.OnPropertyChanged)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox3.DataBindings.Add(new Binding(nameof(textBox3.Text), _viewModel.EditableItem, nameof(_viewModel.EditableItem.Price),
                true, DataSourceUpdateMode.OnPropertyChanged)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox4.DataBindings.Add(new Binding(nameof(textBox4.Text), _viewModel.EditableItem, nameof(_viewModel.EditableItem.Stocks),
                true, DataSourceUpdateMode.OnPropertyChanged)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await _viewModel.SaveAsync();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await _viewModel.EditPicture();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var shose =await _shoesService.GetShoesAsync("ABC-123");
                await _viewModel.LoadAsync(shose);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

    public class ViewModel : ObservableObject
    {
        private Image _image;
        public Image Image
        {
            get => _image;
            set => Set(ref _image, value);
        }
    }
}
