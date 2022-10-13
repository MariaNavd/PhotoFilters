using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class LinearStretchFilter : Filters
    {
        protected int iMax = 0, iMin = 255;
        public LinearStretchFilter(Bitmap sourceImage)
        {
            for (int i = 0; i < sourceImage.Width; i++)
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color pix = sourceImage.GetPixel(i, j);
                    int intensity = (int)(pix.R * 0.36 + 0.53 * pix.G + 0.11 * pix.B);
                    if (intensity > iMax)
                        iMax = intensity;
                    if (intensity < iMin)
                        iMin = intensity;
                }
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int iSource = (int)(sourceColor.R * 0.36 + 0.53 * sourceColor.G + 0.11 * sourceColor.B);
            int iNew = (iSource - iMin) * 255 / (iMax - iMin);

            return Color.FromArgb(Clamp(iNew,0,255), Clamp(iNew, 0, 255), Clamp(iNew, 0, 255));
        }
    }
}
