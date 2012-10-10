using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Providers.Extensions
{
    public static class ByteExtensions
    {
        public static float ToKilobytes(this byte b)
        {
            return (float)((float) b / (float)1024);
        }
    }
}
