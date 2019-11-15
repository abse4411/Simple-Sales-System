using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Simple_Sales_System.Common;
using Simple_Sales_System.Services;

namespace Simple_Sales_System.ViewModels
{
    public class InitDbViewModel : ObservableObject
    {
        private readonly IShoesService _shoesService;
        private readonly IOrderService _orderService;
        private readonly IDialogService _dialogService;
        private string _connectionString;
        public string ConnectionString
        {
            get => _connectionString;
            set => Set(ref _connectionString, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set => Set(ref _progressValue, value);
        }

        public InitDbViewModel()
        {
            _shoesService = new ShoesService();
            _orderService = new OrderService();
            _dialogService = new DialogService();
            _connectionString = DbConnectionString.DevelopmentConnection;
        }

        public bool TestConnection()
        {
            DbConnectionString.DefaultConnection = ConnectionString;
            Message = "Testing connection";
            try
            {
                SqlHelper.ExecuteScalar(DbConnectionString.DefaultConnection, "select Count(*) from Shoes", CommandType.Text);
                SqlHelper.ExecuteScalar(DbConnectionString.DefaultConnection, "select Count(*) from Orders", CommandType.Text);
            }
            catch (Exception e)
            {
                Message = "Failed to connect to the database";
                return false;
            }
            Message = "Succeed in connecting to the database";
            _dialogService.ShowMessage("Congratulations !", "Connection string is valid");
            return true;
        }

        public async Task<bool> InitDbAsync()
        {
            DbConnectionString.DefaultConnection = ConnectionString;
            Message = "Preparing";
            ProgressValue = 0;
            Thread.Sleep(1000);
            try
            {

                Message = "Preparing seed data";
                ProgressValue = 10;
                Thread.Sleep(1000);
                var data = new SeedData();
                var shoesList = data.GetShoesList();
                var orderList = data.GetOrderList();
                Message = "Try to connect to the database";
                ProgressValue = 20;
                Thread.Sleep(1000);
                using (var connection = new SqlConnection(DbConnectionString.DefaultConnection))
                {
                    connection.Open();
                    Message = "Connected";
                    ProgressValue = 30;
                    Thread.Sleep(1000);
                }
                const string createTableSql = @"
CREATE TABLE Shoes (
Model VARCHAR(50)    NOT NULL,
Origin VARCHAR(50)    NOT NULL,
Price FLOAT(53)      NOT NULL,
Stocks INT NOT NULL,
Image VARBINARY(MAX) NULL,
PRIMARY KEY CLUSTERED([Model] ASC));
CREATE TABLE Orders (
Id           INT          NOT NULL,
Model        VARCHAR (50) NOT NULL,
CustomerName VARCHAR (50) NOT NULL,
PhoneNumber  VARCHAR (50) NOT NULL,
Quantity     INT          NOT NULL,
PRIMARY KEY CLUSTERED (Id ASC));";
                Message = "Creating tables";
                ProgressValue = 40;
                Thread.Sleep(1000);
                SqlHelper.ExecuteNonQuery(DbConnectionString.DefaultConnection, createTableSql,CommandType.Text);
                Message = "Adding seed data to tables";
                ProgressValue = 50;
                Thread.Sleep(1000);
                foreach (var shoes in shoesList)
                {
                    await _shoesService.AddShoesAsync(shoes);
                    if (ProgressValue < 75)
                        ProgressValue += 5;
                }
                foreach (var order in orderList)
                {
                    await _orderService.AddOrderAsync(order);
                    if (ProgressValue < 100)
                        ProgressValue += 1;
                }
                Message = "Done";
                ProgressValue = 100;
            }
            catch (Exception e)
            {
                _dialogService.ShowException(e);
                throw;
            }
            ProgressValue = 0;
            _dialogService.ShowMessage("All done", "Database initialization completed");
            ProgressValue = 0;
            return true;
        }
    }
}
