using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class Erosion : MathematicalMorphology
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            bool pix = false;

            int radiusX = structElem.GetLength(0) / 2;
            int radiusY = structElem.GetLength(1) / 2;

            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = x + k;
                    int idY = y + l;

                    if (Enumerable.Range(0, sourceImage.Width).Contains(idX) &&
                        Enumerable.Range(0, sourceImage.Height).Contains(idY))
                        pix = calculateBinPixel(sourceImage, idX, idY);

                    if (!pix && Convert.ToBoolean(structElem[k + radiusX, l + radiusY]))
                        return Color.Black;
                }

            return Color.White;
        }
    }
}
