using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    class SobelFilter : MatrixFilter
    {
        public SobelFilter()
        {
            const int size = 3;
            kernelX = new float[size, size] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            kernelY = new float[size, size] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
        }
    }
}
