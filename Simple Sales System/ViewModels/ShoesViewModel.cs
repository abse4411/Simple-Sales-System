using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Sales_System.Data;

namespace Simple_Sales_System.ViewModels
{
    public class ShoesViewModel:ObservableObject
    {
        private byte[] _image;
        public byte[] Image
        {
            get => _image;
            set => Set(ref _image, value);
        }

        private string _model;
        public string Model
        {
            get => _model;
            set => Set(ref _model, value);
        }

        private string _origin;
        public string Origin
        {
            get => _origin;
            set => Set(ref _origin, value);
        }

        private double _price;
        public double Price
        {
            get => _price;
            set => Set(ref _price, value);
        }

        private int _stocks;
        public int Stocks
        {
            get => _stocks;
            set => Set(ref _stocks, value);
        }

        public override void Merge(ObservableObject source)
        {
            if (source is ShoesViewModel model)
            {
                _model = model._model;
                _origin = model._origin;
                _price = model._price;
                _stocks = model._stocks;
                _image = model._image;
            }
        }

        public Shoes ToShoes()
        {
            return new Shoes
            {
                Model = _model,
                Origin = _origin,
                Price = _price,
                Stocks = _stocks,
                Image = _image
            };
        }
    }
}
