﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HyperLib
{
    public class MapInfo
    {

        public MapInfo(string mapInfoLine)
        {
            string[] lineParts = mapInfoLine.Split(',');
            Projection = lineParts[0];
            ImageCoordinats = new double[2] { double.Parse(lineParts[1]), double.Parse(lineParts[2]) };
            MapX = double.Parse(lineParts[3]);
            MapY = double.Parse(lineParts[4]);
            DX = double.Parse(lineParts[5]);
            DY = double.Parse(lineParts[6]);
            if (lineParts.Length == 9)
            {
                DatUM = lineParts[7];
                Units = lineParts[8].Substring(6);
            }
            else if (lineParts.Length == 11)
            {
                Zone = int.Parse(lineParts[7]);
                hemi = lineParts[8];
                DatUM = lineParts[9];
                Units = lineParts[10].Substring(6);
            }
        }



        public string DatUM { get; set; }

        public double DX { get; set; }

        public double DY { get; set; }

        public string hemi { get; set; }

        public double[] ImageCoordinats { get; set; }

        public double? MapX { get; set; }

        public double? MapY { get; set; }

        public string Projection { get; set; }

        public string Units { get; set; }

        public int Zone { get; set; }
    }
}
