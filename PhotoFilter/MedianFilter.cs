using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class MedianFilter : Filters
    {
        protected int windowSize;
        public MedianFilter(int windowSize)
        {
            this.windowSize = windowSize;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            int radius = windowSize / 2;
            Color[] samples = new Color[windowSize * windowSize];
            int count = 0;

            for (int l = -radius; l <= radius; l++)
                for (int k = -radius; k <= radius; k++)
                {
                    int idX = x + k;
                    int idY = y + l;

                    if (Enumerable.Range(0, sourceImage.Width).Contains(idX) &&
                        Enumerable.Range(0, sourceImage.Height).Contains(idY))
                    {
                        Color neighborColor = sourceImage.GetPixel(idX, idY);
                        samples[count] = neighborColor;
                        count++;
                    }
                }
            if (count < samples.Length)
                Array.Resize(ref samples, count);

            double[] intensities = new double[samples.Length];
            for (int i = 0; i < samples.Length; i++)
                intensities[i] = samples[i].R * 0.36 + 0.53 * samples[i].G + 0.11 * samples[i].B;

            Array.Sort(intensities, samples);
            return samples[count / 2];
        }
    }
}
