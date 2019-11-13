using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simple_Sales_System.Common;

namespace Simple_Sales_System.Services
{
    public class FilePickerService : IFilePickerService
    {
        public async Task<ImagePickerResult> OpenImagePickerAsync()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.png;*.jpeg;*.bmp; *.jpg; *.gif)| *.png;*.jpeg;*.bmp; *.jpg; *.gif";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    return await Task.Run(async () =>
                    {
                        var bytes=File.ReadAllBytes(filePath);
                        Image img = await ImageHelper.FromBytesAsync(bytes);
                        return new ImagePickerResult
                        {
                            ImageBytes = bytes,
                            ImageSource = img
                        };
                    });
                }
            }
            return null;
        }
    }
}
