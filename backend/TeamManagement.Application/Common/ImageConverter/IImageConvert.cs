using System;

namespace TeamManagementSystem.Application.Common.ImageConverter;

public interface IImageConvert
{
    string convertImageToBase64(string imagePath);
    string convertBase64ToImage(string ImagePath);
}
