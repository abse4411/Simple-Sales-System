using Simple_Sales_System.Common;
using Simple_Sales_System.Data;
using Simple_Sales_System.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Sales_System.ViewModels
{
    public class ShoesListViewModel:ObservableObject
    {
        private readonly IShoesService _shoesService;
        private readonly IFilePickerService _filePickerService;
        private readonly IOrderService _orderService;
        private readonly IDialogService _dialogService;
        private IList<Shoes> _shoesList;
        public ShoesDetailsViewModel DetailsViewModel { get; }
        public OrderListViewModel OrderListViewModel { get; }
        public ListView ShoesList { get; }

        public ShoesListViewModel(ListView shoesList, ListView orderList)
        {
            _shoesService = new ShoesService();
            _filePickerService = new FilePickerService();
            _orderService=new OrderService();
            _dialogService = new DialogService();
            DetailsViewModel = new ShoesDetailsViewModel(_shoesService, _filePickerService, _dialogService);
            OrderListViewModel=new OrderListViewModel(orderList, _orderService,_dialogService);
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
            ImageList imageList = new ImageList {ImageSize = new Size(128, 128)};
            foreach (var shoes in _shoesList)
            {
                if (shoes.Image != null)
                    imageList.Images.Add(await ImageHelper.FromBytesAsync(shoes.Image));
                else
                    imageList.Images.Add(ImageHelper.DefaultImage);
            }
            ShoesList.LargeImageList = imageList;
            foreach (var item in items)
            {
                ShoesList.Items.Add(item);
            }
            ShoesList.EndUpdate();
            DetailsViewModel.ClearDetail();
            OrderListViewModel.ClearList();
        }

        public async Task SelectItemAsync(int index)
        {
            await DetailsViewModel.LoadAsync(_shoesList[index]);
            await OrderListViewModel.LoadAsync(_shoesList[index].Model);
        }

        private static async Task<IList<ListViewItem>> CreateListViewItemFrom(IList<Shoes> list)
        {
            return await Task.Run(() =>
            {
                var result = new List<ListViewItem>();
                int imageIndex = 0;
                foreach (var shoes in list)
                {
                    ListViewItem item = new ListViewItem(shoes.Model)
                    {
                        ImageIndex = imageIndex++
                    };
                    item.SubItems.Add(shoes.Origin);
                    item.SubItems.Add(shoes.Price.ToString());
                    item.SubItems.Add(shoes.Stock.ToString());
                    item.ToolTipText = shoes.Model;
                    result.Add(item);
                }
                return result;
            });
        }
    }
}
