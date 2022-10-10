using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFilter
{
    class SepiaFilter : ShadesOfGreyFilter
    {
        public SepiaFilter(int k)
        {
            this.k = k;
        }
    }
}
