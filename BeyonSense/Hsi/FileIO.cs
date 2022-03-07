using HyperLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BeyonSense.Hsi
{
    public static class FileIO
    {        

        public static void HyperLibOpenFIle(string path)
        {
            try
            {
                string SampleName = Path.GetFileName(path);
                string SampleImagePath = Path.Combine(path, "capture", SampleName);

                string PictureFilePath = Path.Combine(path, "capture", SampleName);

                //How to get the file header info
                HeaderInfo header = HyperLib.HeaderInfo.LoadFromFile(SampleImagePath);
                Console.WriteLine("File Header Info ok");

                SpectralCube sc = new SpectralCube(PictureFilePath);
                Console.WriteLine("SpectralCube ok");

                float[,,] img = sc.currentFormateObj.LoadImageCube();
                Console.WriteLine("Imagecube ok");

                Bitmap bmp_img = ImageCube.ToBitmap(img, 129, 82, 38);
                Console.WriteLine("Bitmap img ok");

                if(bmp_img != null)
                {
                    saveBmpFile(bmp_img);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void saveBmpFile(Bitmap image)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image.Save(dialog.FileName, ImageFormat.Jpeg);
            }
        }

        public static BitmapSource OpenHSIrawToBMP(string path)
        {
            try
            {
                string SampleName = Path.GetFileName(path);
                string SampleImagePath = path.Replace(".raw", "");

                string PictureFilePath = path;

                //How to get the file header info
                HeaderInfo header = HyperLib.HeaderInfo.LoadFromFile(SampleImagePath);
                Console.WriteLine("File Header Info ok");

                SpectralCube sc = new SpectralCube(PictureFilePath);
                Console.WriteLine("SpectralCube ok");
                
                float[,,] img = sc.currentFormateObj.LoadImageCube();

                Bitmap bmp_img = ImageCube.ToBitmap(img, 129, 82, 38);
                Console.WriteLine("Bitmap img ok");

                if (bmp_img != null)
                {
                    return Convert(bmp_img);
                }
                else
                {
                    return null;
                }
                                 

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        private static BitmapSource Convert(System.Drawing.Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                PixelFormats.Pbgra32, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);

            return bitmapSource;
        }
    }
}
