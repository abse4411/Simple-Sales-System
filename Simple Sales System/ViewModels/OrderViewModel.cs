using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Sales_System.Data;

namespace Simple_Sales_System.ViewModels
{
    public class OrderViewModel : ObservableObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        private string _model;
        public string Model
        {
            get => _model;
            set => Set(ref _model, value);
        }

        private string _customerName;
        public string CustomerName
        {
            get => _customerName;
            set => Set(ref _customerName, value);
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => Set(ref _phoneNumber, value);
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => Set(ref _quantity, value);
        }

        public override void Merge(ObservableObject source)
        {
            if (source is OrderViewModel model)
            {
                Id = model._id;
                Model = model._model;
                CustomerName = model._customerName;
                PhoneNumber = model._phoneNumber;
                Quantity = model._quantity;
            }
        }

        public Order ToOrder()
        {
            return new Order
            {
                Id = _id,
                Model = _model,
                CustomerName = _customerName,
                PhoneNumber = _phoneNumber,
                Quantity = _quantity
            };
        }
    }
}
