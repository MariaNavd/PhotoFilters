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
        protected string type;
        protected MathematicalMorphology() { }
        public MathematicalMorphology(string type)
        {
            this.type = type;
        }
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

                    switch(this.type)
                    {
                        case "dilation":
                            if (pix && Convert.ToBoolean(structElem[k + radiusX, l + radiusY]))
                            {
                                //result = true;
                                //break;
                                return Color.White;
                            }
                            break;
                        case "erosion":
                            if (!pix && Convert.ToBoolean(structElem[k + radiusX, l + radiusY]))
                            {
                                //result = true;
                                //break;
                                return Color.Black;
                            }
                            break;
                    }
                }

            return Color.Black;
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
