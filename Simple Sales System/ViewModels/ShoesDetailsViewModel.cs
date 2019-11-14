using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simple_Sales_System.Common;
using Simple_Sales_System.Data;
using Simple_Sales_System.Services;

namespace Simple_Sales_System.ViewModels
{
    public class ShoesDetailsViewModel : ObservableObject
    {
        private readonly IShoesService _shoesService;
        private readonly IFilePickerService _filePickerService;
        private readonly IDialogService _dialogService;

        private ShoesViewModel _editableItem;
        public ShoesViewModel EditableItem
        {
            get => _editableItem;
            set => Set(ref _editableItem, value);
        }

        private Image _imageSource;
        public Image ImageSource
        {
            get => _imageSource;
            set => Set(ref _imageSource, value);
        }

        public ShoesDetailsViewModel(IShoesService shoesService, IFilePickerService filePickerService, IDialogService dialogService)
        {
            _shoesService = shoesService;
            _filePickerService = filePickerService;
            _dialogService = dialogService;
            EditableItem = new ShoesViewModel();
        }

        public async Task LoadAsync(Shoes shoes)
        {
            if (shoes != null)
            {
                EditableItem.Model = shoes.Model;
                EditableItem.Origin = shoes.Origin;
                EditableItem.Price = shoes.Price;
                EditableItem.Stocks = shoes.Stocks;
                EditableItem.Image = shoes.Image;
                ImageSource = await ImageHelper.FromBytesAsync(shoes.Image);
            }
        }

        public async Task EditPicture()
        {
            var result = await _filePickerService.OpenImagePickerAsync();
            if (result != null)
            {
                ImageSource = result?.ImageSource as Image;
                EditableItem.Image = result.ImageBytes;
            }
        }

        public async Task<bool> SaveAsync()
        {
            if (!Validate())
                return false;
            var shoes = EditableItem.ToShoes();
            bool isSuccessful = false;
            try
            {
                isSuccessful = await _shoesService.UpdateShoesAsync(shoes) > 0;
            }
            catch (Exception e)
            {
                _dialogService.ShowException(e);
            }
            if (isSuccessful)
            {
                ImageSource = await ImageHelper.FromBytesAsync(shoes.Image);
                _dialogService.ShowMessage("Congratulations", "Save successfully");
            }

            return isSuccessful;
        }

        private bool Validate()
        {
            const string title = "Warning";
            if (string.IsNullOrWhiteSpace(EditableItem.Model))
            {
                _dialogService.ShowWarning(title, "Model can be null");
                return false;
            }
            if (string.IsNullOrWhiteSpace(EditableItem.Origin))
            {
                _dialogService.ShowWarning(title, "Origin can be null");
                return false;
            }
            if (EditableItem.Price <= 0d)
            {
                _dialogService.ShowWarning(title, "Price must be great than 0");
                return false;
            }
            if (EditableItem.Stocks <= 0)
            {
                _dialogService.ShowWarning(title, "Price must be great than 0");
                return false;
            }
            return true;
        }
    }
}
