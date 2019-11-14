using Simple_Sales_System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Sales_System.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderAsync(int id);
        Task<IList<Order>> GetOrderListAsync();
        Task<int> AddOrderAsync(Order order);
        Task<IList<Order>> GetOrderListByModel(string model);
        Task<int> DeleteOrderAsync(int id);
        Task<int> UpdateOrderAsync(Order order);
    }
}
