using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace PhotoFilter
{
    abstract class Filters
    {
        protected abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null);
        public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker, int[,] structElem = null)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    if (structElem == null)
                        resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j, null));
                    else
                        resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j, structElem));
                }
            }
            if (worker != null && worker.WorkerReportsProgress)
            {
                worker.ReportProgress(100);
            }
            return resultImage;
        }

        public int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}