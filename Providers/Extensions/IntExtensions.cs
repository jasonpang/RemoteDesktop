using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Providers.Extensions
{
    public static class IntExtensions
    {
        public static float ToKilobytes(this int i)
        {
            return (float)((float)i / (float)1024);
        }
    }
}
