using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    internal class PrewittFilter :MatrixFilter
    {
        public PrewittFilter()
        {
            const int size = 3;
            kernelX = new float[size, size] { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } };
            kernelY = new float[size, size] { { -1, -1, -1 }, { 0, 0, 0 }, { 1, 1, 1 } };
        }
    }
}
