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

        private bool _isEnable;
        public bool IsEnable
        {
            get => _isEnable;
            set => Set(ref _isEnable, value);
        }

        public InitDbViewModel()
        {
            _shoesService = new ShoesService();
            _orderService = new OrderService();
            _dialogService = new DialogService();
            _connectionString = DbConnectionString.DevelopmentConnection;
            _isEnable = true;
        }

        public bool TestConnection()
        {
            IsEnable = false;
            if (!CheckNull())
                return false;
            DbConnectionString.DefaultConnection = ConnectionString;
            Message = "Testing connection";
            Thread.Sleep(1000);
            try
            {
                using (var connection = new SqlConnection(DbConnectionString.DefaultConnection))
                {
                    connection.Open();
                    Message = "Connected";
                }
            }
            catch (Exception e)
            {
                _dialogService.ShowException(e);
                Message = "The connection string is invalid";
                IsEnable = true;
                return false;
            }
            try
            {
                SqlHelper.ExecuteScalar(DbConnectionString.DefaultConnection, "select Count(*) from Shoes", CommandType.Text);
                SqlHelper.ExecuteScalar(DbConnectionString.DefaultConnection, "select Count(*) from Orders", CommandType.Text);
            }
            catch (Exception e)
            {
                _dialogService.ShowException(e);
                Message = "if the database has not tables, please init the database first";
                IsEnable = true;
                return false;
            }
            Message = "Succeed in connecting to the database";
            IsEnable = true;
            return true;
        }

        public async Task<bool> InitDbAsync()
        {
            IsEnable = false;
            if (!CheckNull())
                return false;
            DbConnectionString.DefaultConnection = ConnectionString;
            Message = "Preparing";
            ProgressValue = 0;
            await Sleep(1000);
            try
            {
                Message = "Preparing seed data";
                ProgressValue = 10;
                await Sleep(1000);
                var data = new SeedData();
                var shoesList = data.GetShoesList();
                var orderList = data.GetOrderList();
                Message = "Try to connect to the database";
                ProgressValue = 20;
                await Sleep(1000);
                using (var connection = new SqlConnection(DbConnectionString.DefaultConnection))
                {
                    connection.Open();
                    Message = "Connected";
                    ProgressValue = 30;
                    await Sleep(1000);
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
                await Sleep(1000);
                SqlHelper.ExecuteNonQuery(DbConnectionString.DefaultConnection, createTableSql,CommandType.Text);
                Message = "Adding seed data into tables";
                ProgressValue = 50;
                await Sleep(1000);
                Message = "Adding shoes";
                foreach (var shoes in shoesList)
                {
                    await Sleep(500);
                    await _shoesService.AddShoesAsync(shoes);
                    if (ProgressValue < 75)
                        ProgressValue += 5;
                }
                await Sleep(1000);
                Message = "Adding orders";
                foreach (var order in orderList)
                {
                    await Sleep(100);
                    await _orderService.AddOrderAsync(order);
                    if (ProgressValue < 100)
                        ProgressValue += 1;
                }
                ProgressValue = 100;
                Message = "Done";
                await Sleep(1000);
            }
            catch (Exception e)
            {
                _dialogService.ShowException(e);
                ProgressValue = 0;
                Message = "Database initialization failed";
                IsEnable = true;
                return false;
            }
            ProgressValue = 0;
            Message = "Database initialization completed";
            IsEnable = true;
            return true;
        }

        private bool CheckNull()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                _dialogService.ShowWarning("Warning", "ConnectionString can not be null");
                Message = "ConnectionString can not be null";
                IsEnable = true;
                return false;
            }
            return true;
        }

        private async Task Sleep(int millisecondsTimeout)
        {
            await Task.Run(() => Thread.Sleep(millisecondsTimeout));
        }
    }
}
