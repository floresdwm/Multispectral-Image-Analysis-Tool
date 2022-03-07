using HyperLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LabToolsAnalytics
{
    public static class PixelServices
    {
        public static List<List<double>> getBandsData(ObservableCollection<double[]> selectedPixels, SpectralCube dataCube)
        {
            try
            {

                List<List<double>> bandsData = new List<List<double>>();

                for (int i = 0; i < selectedPixels.Count; i++)
                {
                    List<double> signal = new List<double>();

                    for (int l = 0; l < dataCube.ImageVector.GetUpperBound(2); l++)
                    {
                        signal.Add(dataCube.ImageVector[(int)selectedPixels[i][0], (int)selectedPixels[i][1], l]);
                    }

                    bandsData.Add(signal);

                }

                if(bandsData != null)
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
