using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Sales_System.Data;
using Simple_Sales_System.Services;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestGetShoes();
            TestGetOrders();
            Console.WriteLine("============");
            Console.ReadKey();
        }
        static async void TestGetOrders()
        {
            IOrderService service = new OrderService();
            var list = await service.GetOrderListAsync();
            foreach (var o in list)
            {
                Console.WriteLine($"{o.Model}\t{o.CustomerName}\t{o.CustomerName}\t{o.Quantity}");
            }
            Order order = new Order
            {
                Id=0,
                Model = "OALY",
                CustomerName = "US",
                PhoneNumber = "13244536789",
                Quantity = 2,
            };
            Console.WriteLine("============");
            var result = await service.AddOrderAsync(order);
            if (result == 1)
            {
                list = await service.GetOrderListAsync();
                foreach (var o in list)
                {
                    Console.WriteLine($"{o.Model}\t{o.CustomerName}\t{o.CustomerName}\t{o.Quantity}");
                }
            }
            else
                Console.WriteLine("Failed to insert");
            //Console.WriteLine("============");
            //order = await service.GetOrderAsync(0);
            //if (order != null)
            //    Console.WriteLine($"{order.Model}\t{order.CustomerName}\t{order.CustomerName}\t{order.Quantity}");
        }
        static async void TestGetShoes()
        {
            IShoesService service=new ShoesService();
            var list=await service.GetShoesListAsync();
            foreach (var shoes in list)
            {
                Console.WriteLine($"{shoes.Model}\t{shoes.Origin}\t{shoes.Price}\t{shoes.Stocks}");
            }
            Shoes s = new Shoes
            {
                Model = "DSA-123",
                Origin = "Japan",
                Price = 3122.3d,
                Stocks = 32,
                Image = null
            };
            Console.WriteLine("============");
            //var result=await service.UpdateShoesAsync(s);
            //if(result==1)
            //{
            //    list = await service.GetShoesListAsync();
            //    foreach (var shoes in list)
            //    {
            //        Console.WriteLine($"{shoes.Model}\t{shoes.Origin}\t{shoes.Price}\t{shoes.Stocks}");
            //    }
            //}
            //else
            //    Console.WriteLine("Failed to insert");
            Console.WriteLine("============");
            s=await service.GetShoesAsync("DSA-13");
            if(s!=null)
                Console.WriteLine($"{s.Model}\t{s.Origin}\t{s.Price}\t{s.Stocks}");
        }
    }
}
