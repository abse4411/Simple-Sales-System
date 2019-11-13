using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Sales_System.Common;
using Simple_Sales_System.Data;

namespace Simple_Sales_System.Services
{
    public class OrderService : IOrderService
    {
        public Task<Order> GetOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddOrderAsync(Order order)
        {
            return await Task.Run(() =>
            {
                string findMaxSql = "select Max(Id) from Orders";
                object maxId = SqlHelper.ExecuteScalar(DbConstants.ConnectionString, findMaxSql, CommandType.Text);
                int id=0;
                if(maxId!=null && Int32.TryParse(maxId.ToString(), out id))
                {
                    id= id+1;
                }
                order.Id = id;
                string sql = "insert into Orders values ( @id,@model,@customerName,@phoneNumber,@quantity )";
                var parameters = CreateParmsFromOrder(order);
                return SqlHelper.ExecuteNonQuery(DbConstants.ConnectionString, sql, CommandType.Text, parameters);
            });
        }

        public Task<int> DeleteOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Order>> GetOrderListAsync()
        {
            return await Task.Run(() =>
            {
                string sql = "select * from Orders";
                IList<Order> list = new List<Order>();
                using (SqlConnection connection = new SqlConnection(DbConstants.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(CreateShoesFromOrder(reader));
                            }
                        }
                    }
                }
                return list;
            });
        }

        public Task<int> UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        private static SqlParameter[] CreateParmsFromOrder(Order order)
        {
            const int n = 5;
            SqlParameter[] parameters = new SqlParameter[n];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int) { Value = order.Id };
            parameters[1] = new SqlParameter("@model", SqlDbType.VarChar) { Value = order.Model };
            parameters[2] = new SqlParameter("@customerName", SqlDbType.VarChar) { Value = order.CustomerName };
            parameters[3] = new SqlParameter("@phoneNumber", SqlDbType.VarChar) { Value = order.PhoneNumber };
            parameters[4] = new SqlParameter("@quantity", SqlDbType.Int) { Value = order.Quantity };
            return parameters;
        }

        private static Order CreateShoesFromOrder(SqlDataReader reader)
        {
            return new Order
            {
                Id= reader.GetInt32(0),
                Model = reader.GetString(1),
                CustomerName = reader.GetString(2),
                PhoneNumber = reader.GetString(3),
                Quantity = reader.GetInt32(4),
            };
        }
    }
}
