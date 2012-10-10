using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network.Utilities
{
    public static class ArrayUtil
    {
        public static T[] GetSequence<T>(T[] array, int startingIndex, int length)
        {
            T[] newArray = new T[length];
            Array.ConstrainedCopy(array, startingIndex, newArray, 0, length);
            return newArray;
        }
    }
}
