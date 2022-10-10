using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class ScharrFilter : MatrixFilter
    {
        public ScharrFilter()
        {
            const int size = 3;
            kernelX = new float[size, size] { { 3, 0, -3 }, { 10, 0, -10 }, { 3, 0, -3 } };
            kernelY = new float[size, size] { { 3, 10, 3 }, { 0, 0, 0 }, { -3, -10, -3 } };
        }
    }
}
