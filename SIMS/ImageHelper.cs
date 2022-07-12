using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SIMS
{
    public class ImageHelper
    {
        public byte[] ImageToByte(Image imageIn)
        {
            MemoryStream memoryStream = new MemoryStream();
            imageIn.Save((Stream)memoryStream, ImageFormat.Jpeg);
            return memoryStream.ToArray();
        }

        public Image ByteToImage(byte[] byteArrayIn, int w = 0, int h = 0)
        {
            Image image = Image.FromStream((Stream)new MemoryStream(byteArrayIn));
            return w <= 0 || h <= 0 ? image.GetThumbnailImage(130, 170, (Image.GetThumbnailImageAbort)null, new IntPtr()) : image.GetThumbnailImage(w, h, (Image.GetThumbnailImageAbort)null, new IntPtr());
        }

        public Image GenerateThumbnailImage(string imgUrl)
        {
            Image thumbnailImage = (Image)null;
            try
            {
                Image image = (Image)null;
                if (imgUrl != string.Empty)
                    image = Image.FromFile(imgUrl);
                if (image != null)
                    thumbnailImage = image.GetThumbnailImage(130, 170, (Image.GetThumbnailImageAbort)null, new IntPtr());
                return thumbnailImage;
            }
            catch
            {
                return thumbnailImage;
            }
        }
    }
}
