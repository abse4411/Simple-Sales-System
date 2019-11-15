using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Sales_System.Data;

namespace Simple_Sales_System.Common
{
    public class SeedData
    {
        private const string Root = "./Assets/";
        private readonly string[] _model = { "ASD-421", "QWE-113", "ZXC-533", "TYU-512", "HJK-131" };
        private readonly string[] _origin = { "China", "US", "UK", "Japan", "South Korea" };
        private readonly double[] _price = { 99d, 1688d, 3216d, 952d, 2123d };
        private readonly int[] _stocks = { 213, 43, 56, 45, 23 };
        private readonly string[] _path = { Root + "1.jpg", Root + "2.jpg", Root + "3.jpg", Root + "4.jpg", Root + "5.jpg" };
        private readonly string[] _customerName = { "Alma", "Adela ", "Lily", "Jane", "Elva" };
        private readonly string[] _phoneNumber = { "123456878901", "162456878901", "166658778901", "19758778901", "174752177581" };
        private readonly int[] _quantity = { 2, 3, 1, 6, 5 };

        public IList<Shoes> GetShoesList()
        {
            IList<Shoes> list = new List<Shoes>();
            try
            {
                for (int i = 0; i < _model.Length; i++)
                {
                    var shoes = new Shoes
                    {
                        Model = _model[i],
                        Origin = _origin[i],
                        Price = _price[i],
                        Stocks = _stocks[i],
                        Image = File.ReadAllBytes(_path[i])
                    };
                    list.Add(shoes);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return list;
        }

        public IList<Order> GetOrderList()
        {
            IList<Order> list = new List<Order>();
            for (int i = 0; i < _customerName.Length; i++)
            {
                for (int j = 0; j < _quantity.Length; j++)
                {
                    var order = new Order
                    {
                        Model = _model[j],
                        CustomerName = _customerName[i],
                        PhoneNumber = _phoneNumber[i],
                        Quantity = _quantity[j]+i+j
                    };
                    list.Add(order);
                }
            }
            return list;
        }
    }
}
