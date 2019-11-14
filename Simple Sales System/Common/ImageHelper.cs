using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Sales_System.Common
{
    public static class ImageHelper
    {
        private static Image _defaultImage = Image.FromFile("./image.png");
        public static async Task<Image> FromBytesAsync(byte[] bytes)
        {
            return await Task.Run(() =>
            {
                Image image = null;
                if (bytes != null && bytes.Length > 0)
                {
                    using (var stream = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(stream);
                    }
                }
                return image;
            });
        }

        public static Image DefaultImage
        {
            get
            {
                if (_defaultImage == null)
                    try
                    {
                        _defaultImage = Image.FromFile("./image.png");
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }

                return _defaultImage;
            }
        }
    }
}
