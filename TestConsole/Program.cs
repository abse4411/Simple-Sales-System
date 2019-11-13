using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Sales_System.Services;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TestGetShoes();
            Console.WriteLine("============");
            Console.ReadKey();
        }

        static async void TestGetShoes()
        {
            IShoesService service=new ShoesService();
            var list=await service.GetShoesListAsync();
            foreach (var shoes in list)
            {
                Console.WriteLine($"{shoes.Model}\t{shoes.Origin}\t{shoes.Price}\t{shoes.Stocks}");
            }
        }
    }
}
