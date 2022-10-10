using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PhotoFilter
{
    class MatrixFilter : Filters
    {
        protected float[,] kernelX = null;
        protected float[,] kernelY = null;
        protected MatrixFilter() { }
        public MatrixFilter(float[,] kernelX, float[,] kernelY = null)
        {
            this.kernelX = kernelX;
            this.kernelY = kernelY;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            float[] gradX = new float[3] { 0, 0, 0 };
            float[] gradY = new float[3] { 0, 0, 0 };
            int[] result = new int[3];

            Thread thread = new Thread(() => calculateGradient(sourceImage, x, y, kernelY, gradY));
            if (kernelX != null)
                calculateGradient(sourceImage, x, y, kernelX, gradX);
            if (kernelY != null)
                calculateGradient(sourceImage, x, y, kernelY, gradY);

            for (int i = 0; i < 3; i++)
                result[i] = (int)Math.Sqrt(gradX[i] * gradX[i] + gradY[i] * gradY[i]);

            return Color.FromArgb(
                Clamp(result[0], 0, 255),
                Clamp(result[1], 0, 255),
                Clamp(result[2], 0, 255)
                );
        }
        public void calculateGradient(Bitmap sourceImage, int x, int y, float[,] kernel, float[] result)
        {
            int[] coef = new int[3] { 0, 0, 0 };

            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = x + k;
                    int idY = y + l;

                    if (Enumerable.Range(0, sourceImage.Width).Contains(idX) &&
                        Enumerable.Range(0, sourceImage.Height).Contains(idY))
                    {
                        Color neighborColor = sourceImage.GetPixel(idX, idY);
                        coef[0] = neighborColor.R;
                        coef[1] = neighborColor.G;
                        coef[2] = neighborColor.B;
                    }

                    for (int i = 0; i < 3; i++)
                        result[i] += coef[i] * kernel[k + radiusX, l + radiusY];
                }
        }
    }
}