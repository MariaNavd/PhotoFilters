using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class FindDiff : Filters
    {
        Bitmap initImage;
        int coef;
        public FindDiff(Bitmap initImage, int coef)
        {
            this.initImage = initImage;
            this.coef = coef;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color initColor = initImage.GetPixel(x, y);
            return Color.FromArgb(coef * (initColor.R - sourceColor.R),
                coef * (initColor.G - sourceColor.G),
                coef * (initColor.B - sourceColor.B));
        }
    }
}
