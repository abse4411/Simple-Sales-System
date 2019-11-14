using Simple_Sales_System.Common;
using Simple_Sales_System.Data;
using Simple_Sales_System.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Sales_System.ViewModels
{
    public class ShoesListViewModel:ObservableObject
    {
        private readonly IShoesService _shoesService;
        private readonly IFilePickerService _filePickerService;
        private readonly IDialogService _dialogService;
        private IList<Shoes> _shoesList;
        public ShoesDetailsViewModel DetailsViewModel { get; }
        public ListView ShoesList { get; }

        public ShoesListViewModel(ListView shoesList)
        {
            _shoesService = new ShoesService();
            _filePickerService = new FilePickerService();
            _dialogService = new DialogService();
            DetailsViewModel = new ShoesDetailsViewModel(_shoesService, _filePickerService, _dialogService);
            ShoesList = shoesList;
        }

        public async Task RefreshAsync()
        {
            try
            {
                _shoesList = await _shoesService.GetShoesListAsync();
            }
            catch (Exception e)
            {
                _dialogService.ShowException(e);
                return;
            }
            ShoesList.Items.Clear();
            ShoesList.BeginUpdate();
            var items = await CreateListViewItemFrom(_shoesList);
            ImageList images = new ImageList();
            images.ImageSize = new Size(128,128);
            foreach (var shoes in _shoesList)
            {
                if(shoes.Image != null)
                    images.Images.Add(await ImageHelper.FromBytesAsync(shoes.Image));
            }
            ShoesList.LargeImageList = images;
            foreach (var item in items)
            {
                ShoesList.Items.Add(item);
            }
            ShoesList.EndUpdate();
        }

        public async Task SelectItemAsync(int index)
        {
            await DetailsViewModel.LoadAsync(_shoesList[index]);
        }

        private static async Task<IList<ListViewItem>> CreateListViewItemFrom(IList<Shoes> list)
        {
            return await Task.Run(() =>
            {
                var result = new List<ListViewItem>();
                int imageIndex = 0;
                foreach (var shoes in list)
                {
                    ListViewItem item = new ListViewItem(shoes.Model);
                    if (shoes.Image != null)
                        item.ImageIndex = imageIndex++;
                    item.SubItems.Add(shoes.Origin);
                    item.SubItems.Add(shoes.Price.ToString());
                    item.SubItems.Add(shoes.Stocks.ToString());
                    item.ToolTipText = shoes.Model;
                    result.Add(item);
                }
                return result;
            });
        }
    }
}
