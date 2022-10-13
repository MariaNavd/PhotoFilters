using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    class MathematicalMorphology : Filters
    {
        protected MathematicalMorphology() { }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            throw new NotImplementedException();
        }
        public bool calculateBinPixel(Bitmap sourceImage, int x, int y)
        {
            bool pix;
            Color sourceColor = sourceImage.GetPixel(x, y);

            int intensity = (int)(sourceColor.R * 0.36 + 0.53 * sourceColor.G + 0.11 * sourceColor.B);
            if (intensity < 128)
                pix = false;
            else
                pix = true;

            return pix;
        }
    }
}
