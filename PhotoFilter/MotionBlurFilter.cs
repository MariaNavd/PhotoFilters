using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class MotionBlurFilter : MatrixFilter
    {
        public MotionBlurFilter(int size = 3)
        {
            kernelX = new float[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (i == j)
                        kernelX[i, j] = 1.0f / (float)size;
                    else
                        kernelX[i, j] = 0.0f;
        }
    }
}
