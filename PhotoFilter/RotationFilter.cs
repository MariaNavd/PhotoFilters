using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class RotationFilter : Filters
    {
        protected double angle;
        public RotationFilter(double angle)
        {
            this.angle = angle;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            int x0 = sourceImage.Width / 2;
            int y0 = sourceImage.Height / 2;

            double xNew = (x - x0) * Math.Cos(angle) - (y - y0) * Math.Sin(angle) + x0;
            double yNew = (y - y0) * Math.Cos(angle) + (x - x0) * Math.Sin(angle) + y0;

            Color sourceColor = sourceImage.GetPixel(Clamp((int)xNew, 0, sourceImage.Width - 1),
                            Clamp((int)yNew, 0, sourceImage.Height - 1));
            Color resultColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);
            return resultColor;
        }
    }
}
