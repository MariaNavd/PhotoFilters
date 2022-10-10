using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class WavesFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            Random rnd = new Random();

            double xNew = x + 20 * Math.Sin(2 * Math.PI * y / 60);

            Color sourceColor = sourceImage.GetPixel(Clamp((int)xNew, 0, sourceImage.Width - 1), y);
            Color resultColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);
            return resultColor;
        }
    }
}
