using HyperLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
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
                

                Bitmap bmp_img = ImageCube.ToBitmap(sc.currentFormateObj.LoadImageCube(), 129, 82, 38);
                Console.WriteLine("Bitmap img ok");                

                if (bmp_img != null)
                {
                    return GetBitmapSource(bmp_img);
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

        public static float[,,] getRawDataToDataCube(string path)
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

                return img;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static BitmapSource GetBitmapSource(Bitmap bitmap)
        {
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap
            (
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions()
            );

            return bitmapSource;
        }

        public static List<List<double>> getBandsData(ObservableCollection<double[]> selectedPixels, float[,,] dataCube)
        {
            try
            {

                List<List<double>> bandsData = new List<List<double>>();

                for (int i = 0; i < selectedPixels.Count; i++)
                {
                    List<double> signal = new List<double>();

                    for (int l = 0; l < dataCube.GetUpperBound(2) + 1; l++)
                    {
                        signal.Add(dataCube[(int)selectedPixels[i][0], (int)selectedPixels[i][1], l]);
                    }

                    bandsData.Add(signal);

                }

                if (bandsData != null)
                {
                    return bandsData;
                }
                else
                {
                    throw new Exception("getBandsData() generic Exception");
                }

            }
            catch (Exception)
            {
                throw new Exception("getBandsData() generic Exception");
            }
        }
    }
}
