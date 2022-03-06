using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using MiscUtil;
using System.Drawing;
using System.Threading.Tasks;
using BitmapProcessing;
namespace HyperLib
{
    
    public class ImageCube:SpectralCube
    {
		#region Constructors (1) 

        public ImageCube(string p) : base( p)
        {
            ;
        }

		#endregion Constructors 

		#region Methods (3) 

		// Public Methods (2) 

        //..........................................................................
        public  Bitmap ToBitmap(int redIndex, int greenIndex, int blueIndex)
        {          
            int[, ,] triBandImageVector = NormalizeImageVectorForDisplay(redIndex, greenIndex, blueIndex);
            Bitmap inputImage = new Bitmap(ImageVector.GetLength(0), ImageVector.GetLength(1));
            FastBitmap currentTRIBandImage = new FastBitmap(inputImage);
            currentTRIBandImage.LockImage();
            for (int i = 0; i < inputImage.Width; i++)
            {
                for (int j = 0; j < inputImage.Height; j++)
                {
                    currentTRIBandImage.SetPixel(i, j, Color.FromArgb(triBandImageVector[i, j, 0], triBandImageVector[i, j, 1], triBandImageVector[i, j, 2]));
                }
            }
            currentTRIBandImage.UnlockImage();
            return inputImage;
        }

        public static Bitmap ToBitmap(double[,,] imageVector, int redIndex, int greenIndex, int blueIndex)
        {
            ImageCube image = new ImageCube("");
            image.ImageVector = imageVector;
            return image.ToBitmap(redIndex, greenIndex, blueIndex);
        }
		// Private Methods (1) 

        private double[] RangeValuesFor(int colorBandIndex)
        {
            double[] maxValue = new double[2];
            Parallel.For(0, ImageVector.GetLength(0), i =>
            //for (int i = 0; i < ImageVector.GetLength(0); i++)
            {
                for (int j = 0; j < ImageVector.GetLength(1); j++)
                {
                    if (maxValue[0] < ImageVector[i, j, colorBandIndex])
                        maxValue[0] = ImageVector[i, j, colorBandIndex];
                    //...........................................................
                    if (maxValue[1] > ImageVector[i, j, colorBandIndex])
                        maxValue[1] = ImageVector[i, j, colorBandIndex];
                }
            });
            return maxValue;
        }

		#endregion Methods 

        public double[, ,] LoadImageCube()
        {
            ImageVector = currentFormateObj.LoadImageCube();
            return ImageVector;
        }
        public double[, ,] LoadImageCube_withSubWindow(ImageSubWindow currentWindowToLoad)
        {
            ImageVector = currentFormateObj.LoadImageCube_withSubWindow(currentWindowToLoad);
            return ImageVector;
        }
        public double[, ,] LoadImageSingleBand(int bandIndex, ImageSubWindow currentWindowToLoad)
        {
            ImageVector = currentFormateObj.LoadImageSingleBand(bandIndex,currentWindowToLoad);
            return ImageVector;
        }
        private int[, ,] NormalizeImageVectorForDisplay(int redIndex, int greenIndex, int blueIndex)
        {
            int[, ,] triBandImageVector = new int[ImageVector.GetLength(0), ImageVector.GetLength(1), 3];
            //.................................................................
            double[] redmaxValue = RangeValuesFor(redIndex);
            double[] greenmaxValue = RangeValuesFor(greenIndex);
            double[] bluemaxValue = RangeValuesFor(blueIndex);
            //.................................................................
            int redRange = (int)(redmaxValue[0] - redmaxValue[1]);
            int greenRange = (int)(greenmaxValue[0] - greenmaxValue[1]);
            int blueRange = (int)(bluemaxValue[0] - bluemaxValue[1]);
            for (int i = 0; i < ImageVector.GetLength(0); i++)
            {
                for (int j = 0; j < ImageVector.GetLength(1); j++)
                {
                    triBandImageVector[i, j, 0] = (int)(ImageVector[i, j, redIndex] * 255d / redmaxValue[0]);
                    triBandImageVector[i, j, 1] = (int)(ImageVector[i, j, greenIndex] * 255d / greenmaxValue[0]);
                    triBandImageVector[i, j, 2] = (int)(ImageVector[i, j, blueIndex] * 255d / bluemaxValue[0]);
                }
            }
            return triBandImageVector;
        }
        //.....................................................................................


       
    }
}
