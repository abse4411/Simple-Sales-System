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
            else
                ClearDetail();
        }

        public async Task LoadAsync(string id)
        {
            try
            {
                var shoes = await _shoesService.GetShoesAsync(id);
                await LoadAsync(shoes);
            }
            catch (Exception e)
            {
                _dialogService.ShowException(e);
            }
        }

        public void ClearDetail()
        {
            EditableItem.Merge(new ShoesViewModel());
            ImageSource = null;
        }

        public async Task PickPictureAsync()
        {
            var result = await _filePickerService.OpenImagePickerAsync();
            if (result != null)
            {
                ImageSource = result?.ImageSource as Image;
                EditableItem.Image = result.ImageBytes;
            }
        }

        public void ResetPicture()
        {
            EditableItem.Image = null;
            ImageSource = null;
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
                return false;
            }
            if (isSuccessful)
            {
                ImageSource = await ImageHelper.FromBytesAsync(shoes.Image);
                _dialogService.ShowMessage("Congratulations", "Save successfully");
            }
            else
            {
                _dialogService.ShowMessage("Failed", "Unknown error");
            }

            return isSuccessful;
        }

        private bool Validate()
        {
            const string title = "Warning";
            if (string.IsNullOrWhiteSpace(EditableItem.Model))
            {
                _dialogService.ShowWarning(title, "Model can not be null");
                return false;
            }
            if (string.IsNullOrWhiteSpace(EditableItem.Origin))
            {
                _dialogService.ShowWarning(title, "Origin can not be null");
                return false;
            }
            if (EditableItem.Price <= 0d)
            {
                _dialogService.ShowWarning(title, "Price must be great than 0");
                return false;
            }
            if (EditableItem.Stocks <= 0)
            {
                _dialogService.ShowWarning(title, "Stocks must be great than 0");
                return false;
            }
            return true;
        }
    }
}
