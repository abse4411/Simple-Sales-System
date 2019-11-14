using System;
using System.Diagnostics;
using System.Windows.Forms;
using Simple_Sales_System.ViewModels;

namespace Simple_Sales_System
{
    public partial class Form1 : Form
    {
        //private readonly ShoesDetailsViewModel _viewModel;
        //private FilePickerService _filePickerService;
        //private ShoesService _shoesService;
        //private DialogService dialogService;
        private ShoesListViewModel _viewModel;
        public Form1()
        {
            InitializeComponent();
            _viewModel = new ShoesListViewModel(ShoesList,OrderList);
            SetBindings();
            _viewModel.RefreshAsync();
        }

        private void SetBindings()
        {
            pictureBox1.DataBindings.Add(new Binding(nameof(pictureBox1.Image), _viewModel.DetailsViewModel, nameof(_viewModel.DetailsViewModel.ImageSource),
                true, DataSourceUpdateMode.OnPropertyChanged)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox1.DataBindings.Add(new Binding(nameof(textBox1.Text), _viewModel.DetailsViewModel.EditableItem,
                nameof(_viewModel.DetailsViewModel.EditableItem.Model),
                true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox2.DataBindings.Add(new Binding(nameof(textBox2.Text), _viewModel.DetailsViewModel.EditableItem,
                nameof(_viewModel.DetailsViewModel.EditableItem.Origin),
                true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox3.DataBindings.Add(new Binding(nameof(textBox3.Text), _viewModel.DetailsViewModel.EditableItem,
                nameof(_viewModel.DetailsViewModel.EditableItem.Price),
                true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            textBox4.DataBindings.Add(new Binding(nameof(textBox4.Text), _viewModel.DetailsViewModel.EditableItem,
                nameof(_viewModel.DetailsViewModel.EditableItem.Stocks),
                true, DataSourceUpdateMode.Never)
            {
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (ShoesList.SelectedItems.Count > 0)
                if (await _viewModel.DetailsViewModel.SaveAsync())
                {
                    ShoesList.SelectedItems.Clear();
                    await _viewModel.RefreshAsync();
                }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (ShoesList.SelectedItems.Count > 0)
                await _viewModel.DetailsViewModel.PickPictureAsync();
        }

        private async void Refresh_Click(object sender, EventArgs e)
        {
            await _viewModel.RefreshAsync();
        }

        private async void ShoesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ShoesList.SelectedItems.Count > 0)
            {
                await _viewModel.SelectItemAsync(ShoesList.SelectedIndices[0]);
                ChooseBtn.Enabled = true;
                ClearBtn.Enabled = true;
                SaveBtn.Enabled = true;
                BuyBtn.Enabled = true;
            }
            else
            {
                _viewModel.DetailsViewModel.ClearDetail();
                _viewModel.OrderListViewModel.ClearList();
                ChooseBtn.Enabled = false;
                ClearBtn.Enabled = false;
                SaveBtn.Enabled = false;
                BuyBtn.Enabled = false;
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            _viewModel.DetailsViewModel.ResetPicture();
        }

        private async void Buy_Click(object sender, EventArgs e)
        {
            using (OrderForm form = new OrderForm( _viewModel.DetailsViewModel.EditableItem.ToShoes()))
            {
                form.ShowDialog(this);
            }

            await _viewModel.RefreshAsync();
        }

        //<div>Icons made by <a href="https://www.flaticon.com/authors/smashicons" title="Smashicons">Smashicons</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a></div>
    }

}
