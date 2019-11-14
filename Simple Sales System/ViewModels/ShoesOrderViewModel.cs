﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Sales_System.Common;
using Simple_Sales_System.Data;
using Simple_Sales_System.Services;

namespace Simple_Sales_System.ViewModels
{
    public class ShoesOrderViewModel:ObservableObject
    {
        private readonly Shoes _shoes;
        private readonly IShoesService _shoesService;
        private readonly IOrderService _orderService;
        private readonly IDialogService _dialogService;
        public ShoesDetailsViewModel ShoesDetails { get; }
        private OrderViewModel _editableItem;
        public OrderViewModel EditableItem
        {
            get => _editableItem;
            set => Set(ref _editableItem, value);
        }

        public ShoesOrderViewModel(Shoes shoes)
        {
            _shoes = shoes;
            _shoesService = new ShoesService();
            _orderService = new OrderService();
            _dialogService = new DialogService();
            ShoesDetails = new ShoesDetailsViewModel(_shoesService, new FilePickerService(), _dialogService);
            EditableItem=new OrderViewModel();
            EditableItem.Model = _shoes.Model;
        }

        public async Task LoadShoesDetailsAsync()
        {
            await ShoesDetails.LoadAsync(_shoes);
        }

        public async Task<bool> SaveAsync()
        {
            if (!Validate())
            {
                return false;
            }
            var order = EditableItem.ToOrder();
            bool isSuccessful = false;
            try
            {
                isSuccessful = await _orderService.AddOrderAsync(order) > 0;
                if (!isSuccessful)
                    return false;
                if (order.Quantity == _shoes.Stocks)
                    isSuccessful = await _shoesService.DeleteShoesAsync(_shoes.Model)>0;
                else
                {
                    _shoes.Stocks -= order.Quantity;
                    isSuccessful = await _shoesService.UpdateShoesAsync(_shoes) > 0;
                }
            }
            catch (Exception e)
            {
                _dialogService.ShowException(e);
                return false;
            }
            if (isSuccessful)
            {
                _dialogService.ShowMessage("Congratulations", "You have purchased");
            }
            else
            {
                _dialogService.ShowMessage("Failed", "Unknown error");
            }

            return isSuccessful;
        }

        public bool Validate()
        {
            const string title = "Warning";
            if (string.IsNullOrWhiteSpace(EditableItem.CustomerName))
            {
                _dialogService.ShowWarning(title, "CustomerName can not be null");
                return false;
            }
            if (string.IsNullOrWhiteSpace(EditableItem.PhoneNumber))
            {
                _dialogService.ShowWarning(title, "PhoneNumber can not be null");
                return false;
            }
            if (EditableItem.Quantity <=0)
            {
                _dialogService.ShowWarning(title, "Quantity must be great than zero");
                return false;
            }
            if (EditableItem.Quantity > _shoes.Stocks)
            {
                _dialogService.ShowWarning(title, "Quantity must be not great than stocks");
                return false;
            }

            return true;
        }
    }
}