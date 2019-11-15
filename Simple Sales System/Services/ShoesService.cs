using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Simple_Sales_System.Common;
using Simple_Sales_System.Data;

namespace Simple_Sales_System.Services
{
    public class ShoesService: IShoesService
    {
        public async Task<Shoes> GetShoesAsync(string id)
        {
            return await Task.Run(() =>
            {
                string sql = "select * from Shoes where Model=@model";
                SqlParameter model = new SqlParameter("@model", SqlDbType.VarChar) { Value = id };
                Shoes shoes = null;
                using (SqlConnection connection = new SqlConnection(DbConnectionString.DefaultConnection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(model);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                shoes = CreateShoesFromReader(reader);
                                break;
                            }
                        }
                    }
                }
                return shoes;
            });
        }

        public async Task<IList<Shoes>> GetShoesListAsync()
        {
            return await Task.Run(() =>
            {
                string sql = "select * from Shoes";
                IList<Shoes> list = new List<Shoes>();
                using (SqlConnection connection = new SqlConnection(DbConnectionString.DefaultConnection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(CreateShoesFromReader(reader));
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
                string sql = "insert into Shoes values ( @model,@origin,@price,@stocks,@image )";
                var parameters = CreateParmsFromShoes(shoes);
                return SqlHelper.ExecuteNonQuery(DbConnectionString.DefaultConnection,sql,CommandType.Text, parameters);
            });
        }

        public async Task<int> DeleteShoesAsync(string id)
        {
            return await Task.Run(() =>
            {
                string sql = "delete from Shoes where Model=@model";
                SqlParameter model = new SqlParameter("@model", SqlDbType.VarChar) { Value = id };
                return SqlHelper.ExecuteNonQuery(DbConnectionString.DefaultConnection, sql, CommandType.Text, model);
            });
        }

        public async Task<int> UpdateShoesAsync(Shoes shoes)
        {
            return await Task.Run(() =>
            {
                string sql = "update Shoes set Origin=@origin,Price=@price,Stocks=@stocks,Image=@image where Model=@model";
                var parameters = CreateParmsFromShoes(shoes);
                return SqlHelper.ExecuteNonQuery(DbConnectionString.DefaultConnection, sql, CommandType.Text, parameters);
            });
        }

        private static SqlParameter[] CreateParmsFromShoes(Shoes shoes)
        {
            const int n=5;
            SqlParameter[] parameters = new SqlParameter[n];
            parameters[0] = new SqlParameter("@model", SqlDbType.VarChar) { Value = shoes.Model };
            parameters[1] = new SqlParameter("@origin", SqlDbType.VarChar) { Value = shoes.Origin };
            parameters[2] = new SqlParameter("@price", SqlDbType.Float) { Value = shoes.Price };
            parameters[3] = new SqlParameter("@stocks", SqlDbType.Int) { Value = shoes.Stocks };
            parameters[4] = new SqlParameter("@image", SqlDbType.VarBinary) { Value = shoes.Image ?? (object)DBNull.Value };
            return parameters;
        }

        private static Shoes CreateShoesFromReader(SqlDataReader reader)
        {
            return new Shoes
            {
                Model = reader.GetString(0),
                Origin = reader.GetString(1),
                Price = reader.GetDouble(2),
                Stocks = reader.GetInt32(3),
                Image = reader.GetSqlBinary(4).IsNull ? null : reader.GetSqlBinary(4).Value
            };
        }
    }
}
