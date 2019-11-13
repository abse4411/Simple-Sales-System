using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Sales_System.Common
{
    public static class ImageHelper
    {
        public static async Task<Image> FromBytesAsync(byte[] bytes)
        {
            return await Task.Run(() =>
            {
                Image image = null;
                if (bytes != null && bytes.Length > 0)
                {
                    using (var stream = new MemoryStream(bytes))
                    {
                        image= Image.FromStream(stream);
                    }
                }
                return image;
            });
        }
    }
}
