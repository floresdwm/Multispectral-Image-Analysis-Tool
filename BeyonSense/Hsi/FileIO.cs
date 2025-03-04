﻿using HyperLib;
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
        public static void openHSImage(string folderPath)
        {
            try
            {               

                string SampleName = Path.GetFileName(folderPath);
                string SampleImagePath = Path.Combine(folderPath, "capture", SampleName + ".raw");

                using (Stream SampleStream = File.OpenRead(@SampleImagePath))
                {
                    int value = 0;
                    List<int> imgData = new List<int>();
                    while (true)
                    {
                        value = SampleStream.ReadByte();                        

                        if (value != 0)
                        {
                            imgData.Add(value);
                        }
                        else if( value == -1)
                        {
                            break;
                        }
                    }
                    
                }
                Console.WriteLine("done");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static byte[] PadLines(byte[] bytes, int rows, int columns)
        {
            int currentStride = columns; // 3
            int newStride = columns;  // 4
            byte[] newBytes = new byte[newStride * rows];
            for (int i = 0; i < rows; i++)
                Buffer.BlockCopy(bytes, currentStride * i, newBytes, newStride * i, currentStride);
            return newBytes;
        }

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
