using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Sales_System.Data;

namespace Simple_Sales_System.Services
{
    public interface IShoesService
    {
        Task<IList<Shoes>> GetShoesListAsync();
        Task<int> AddShoesAsync(Shoes shoes);
        Task<int> DeleteShoesAsync(int id);
        Task<int> UpdateShoesAsync(Shoes shoes);
    }
}
