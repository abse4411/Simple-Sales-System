using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simple_Sales_System.Data;
using Simple_Sales_System.Services;

namespace Simple_Sales_System.ViewModels
{
    public class OrderListViewModel:ObservableObject
    {
        private ListView OrderList { get; }
        private readonly IOrderService _orderService;
        private readonly IDialogService _dialogService;

        public OrderListViewModel(ListView orderList, IOrderService orderService,IDialogService dialogService)
        {
            OrderList = orderList;
            _orderService = orderService;
            _dialogService = dialogService;
        }

        public async Task LoadAsync(string model)
        {
            IList<Order> orderList;
            try
            {
                orderList =await _orderService.GetOrderListByModel(model);
            }
            catch (Exception e)
            {
                _dialogService.ShowException(e);
                return;
            }
            OrderList.Items.Clear();
            OrderList.BeginUpdate();
            var items = await CreateListViewItemFrom(orderList);
            foreach (var item in items)
            {
                OrderList.Items.Add(item);
            }
            OrderList.EndUpdate();
            OrderList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        public void ClearList()
        {
            OrderList.Items.Clear();
        }

        private static async Task<IList<ListViewItem>> CreateListViewItemFrom(IList<Order> list)
        {
            return await Task.Run(() =>
            {
                var result = new List<ListViewItem>();
                foreach (var order in list)
                {
                    ListViewItem item = new ListViewItem(order.Id.ToString());
                    item.SubItems.Add(order.CustomerName);
                    item.SubItems.Add(order.PhoneNumber);
                    item.SubItems.Add(order.Quantity.ToString());
                    item.ToolTipText = item.Text;
                    result.Add(item);
                }
                return result;
            });
        }

    }
}
