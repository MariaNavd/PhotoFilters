using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class BrightnessFilter : Filters
    {
        protected int k;
        protected BrightnessFilter() { }
        public BrightnessFilter(int k)
        {
            this.k = k;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double intensity = sourceColor.R * 0.36 + 0.53 * sourceColor.G + 0.11 * sourceColor.B;
            Color resultColor = Color.FromArgb(Clamp(sourceColor.R + k, 0, 255),
                                                Clamp(sourceColor.G + k, 0, 255),
                                                Clamp(sourceColor.B + k, 0, 255));
            return resultColor;
        }
    }
}
