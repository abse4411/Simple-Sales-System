﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Sales_System.Services
{
    public class ImagePickerResult
    {
        public byte[] ImageBytes { get; set; }
        public object ImageSource { get; set; }
    }
    public interface IFilePickerService
    {
        Task<ImagePickerResult> OpenImagePickerAsync();
    }
}
