using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class PerfectReflectionFilter : Filters
    {
        protected int rMax = 0, gMax = 0, bMax = 0;
        public PerfectReflectionFilter(Bitmap sourceImage)
        {
            for (int i = 0; i < sourceImage.Width; i++)
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color pix = sourceImage.GetPixel(i, j);
                    if (pix.R > rMax)
                        rMax = pix.R;
                    if (pix.G > gMax)
                        gMax = pix.G;
                    if (pix.B > bMax)
                        bMax = pix.B;
                }
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(
                Clamp(sourceColor.R * 255 / rMax, 0, 255),
                Clamp(sourceColor.G * 255 / gMax, 0, 255),
                Clamp(sourceColor.B * 255 / bMax, 0, 255));
            return resultColor;
        }
    }
}
