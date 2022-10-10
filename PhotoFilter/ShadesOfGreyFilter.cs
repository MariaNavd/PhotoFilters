using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PhotoFilter
{
    class ShadesOfGreyFilter : Filters
    {
        protected int k;
        public ShadesOfGreyFilter()
        {
            this.k = 0;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double intensity = sourceColor.R * 0.36 + 0.53 * sourceColor.G + 0.11 * sourceColor.B;
            Color resultColor = Color.FromArgb(Clamp((int)(intensity + 2 * k), 0, 255),
                                                Clamp((int)(intensity + 0.5 * k), 0, 255),
                                                Clamp((int)(intensity - k), 0, 255));
            return resultColor;
        }
    }
}
