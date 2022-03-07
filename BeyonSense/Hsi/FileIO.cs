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

                double[,,] img = sc.currentFormateObj.LoadImageCube();
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
    }
}
