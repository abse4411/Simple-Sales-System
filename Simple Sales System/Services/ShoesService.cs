using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Sales_System.Data;

namespace Simple_Sales_System.Services
{
    public class ShoesService: IShoesService
    {
        public async Task<IList<Shoes>> GetShoesListAsync()
        {
            return await Task.Run(() =>
            {
                string sql = "select * from Shoes";
                IList<Shoes> list = new List<Shoes>();
                using (SqlConnection connection = new SqlConnection(DbConstants.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Shoes
                                {
                                    Model = reader.GetString(0),
                                    Origin = reader.GetString(1),
                                    Price = reader.GetDouble(2),
                                    Stocks = reader.GetInt32(3),
                                    Image = reader.GetSqlBinary(4).IsNull ? null : reader.GetSqlBinary(4).Value
                                });
                            }
                        }
                    }
                }
                return list;
            });
        }

        public async Task<int> AddShoesAsync(Shoes shoes)
        {
            return await Task.Run(() =>
            {
                string sql = "insert Shoes(Model,Origin,Price,Stocks,Image) values(@model,@origin,@price,@stocks,@image)";
                SqlParameter model = new SqlParameter("@model", SqlDbType.VarChar) {Value = shoes.Model};
                SqlParameter origin = new SqlParameter("@origin", SqlDbType.VarChar) {Value = shoes.Origin};
                SqlParameter price =new SqlParameter("@price", SqlDbType.VarChar) {Value = shoes.Price};
                SqlParameter stocks = new SqlParameter("@stocks", SqlDbType.VarChar) {Value = shoes.Stocks};
                SqlParameter image = new SqlParameter("@image", SqlDbType.VarChar) {Value = shoes.Image};
                IList<Shoes> list = new List<Shoes>();
                using (SqlConnection connection = new SqlConnection(DbConstants.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Shoes
                                {
                                    Model = reader.GetString(0),
                                    Origin = reader.GetString(1),
                                    Price = reader.GetDouble(2),
                                    Stocks = reader.GetInt32(3),
                                    Image = reader.GetSqlBinary(4).IsNull ? null : reader.GetSqlBinary(4).Value
                                });
                            }
                        }
                    }
                }
                return 1;
            });
        }

        public Task<int> DeleteShoesAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateShoesAsync(Shoes shoes)
        {
            throw new NotImplementedException();
        }
    }
}
