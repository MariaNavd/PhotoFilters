using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class GreyWorldFilter : Filters
    {
        protected int rAvg = 0, gAvg = 0, bAvg = 0, avg = 0;
        public GreyWorldFilter(Bitmap sourceImage)
        {
            for (int i = 0; i < sourceImage.Width; i++)
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color pix = sourceImage.GetPixel(i, j);
                    rAvg += pix.R;
                    gAvg += pix.G;
                    bAvg += pix.B;
                }
            rAvg /= sourceImage.Width * sourceImage.Height;
            gAvg /= sourceImage.Width * sourceImage.Height;
            bAvg /= sourceImage.Width * sourceImage.Height;

            avg = (rAvg + gAvg + bAvg) / 3;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(
                Clamp(sourceColor.R * avg / rAvg, 0, 255),
                Clamp(sourceColor.G * avg / gAvg, 0, 255),
                Clamp(sourceColor.B * avg / bAvg, 0, 255));
            return resultColor;
        }
    }
}
