using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class GlassEffectFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y, int[,] structElem = null)
        {
            Random rnd = new Random();

            //Получить случайное число (в диапазоне от 0 до 10)
            //int value = rnd.Next(0, 10);

            int xNew = x + rnd.Next(0, 10) - 5;
            //rnd = new Random();
            int yNew = y + rnd.Next(0, 10) - 5;

            //double xNew = x + (rnd.NextDouble() - 0.5) * 10.0;
            //double yNew = y + (rnd.NextDouble() - 0.5) * 10.0;

            xNew = Clamp((int)Math.Abs(xNew), 0, sourceImage.Width - 1);
            yNew = Clamp((int)Math.Abs(yNew), 0, sourceImage.Height - 1);

            Color sourceColor = sourceImage.GetPixel((int)xNew, (int)yNew);
            Color resultColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);
            return resultColor;
        }
    }
}
