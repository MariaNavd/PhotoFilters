using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PhotoFilter
{
    class BlackAndWhiteFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int intensity = (int)(sourceColor.R * 0.36 + 0.53 * sourceColor.G + 0.11 * sourceColor.B);
            if (intensity < 128)
                intensity = 0;
            else
                intensity = 255;
            Color resultColor = Color.FromArgb(intensity, intensity, intensity);
            return resultColor;
        }
    }
}
