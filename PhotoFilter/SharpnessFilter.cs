using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class SharpnessFilter : MatrixFilter
    {
        public SharpnessFilter()
        {
            kernelX = new float[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
        }
    }
}
