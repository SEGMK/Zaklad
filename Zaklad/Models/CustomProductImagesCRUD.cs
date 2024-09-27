using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaklad.Models
{
    public static class CustomProductImagesCRUD
    {
        private static string AppPath = FileSystem.Current.AppDataDirectory;
        private const string UserProductsFile = "CustomProductImage";
        private static readonly string FilePath = Path.Combine(AppPath + UserProductsFile);
        public static ImageSource SaveImage(Stream stream)
        {
            using FileStream localFileStream = File.OpenWrite(FilePath + stream.GetHashCode());
            stream.CopyTo(localFileStream);
            ImageSource img = ImageSource.FromFile(FilePath + stream.GetHashCode().ToString());
            return img;
        }
        public static void DeleteImage(FileImageSource imageSource)
        {
            if (imageSource == string.Empty)
                return;
            File.Delete(imageSource.File);
        }
    }
}
