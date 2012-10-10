using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Model.Extensions
{
    public static class BitmapExtensions
    {
        public static DisposableImageData LockBitsDisposable(this Bitmap bitmap, Rectangle rect, ImageLockMode flags, PixelFormat format)
        {
            return new DisposableImageData(bitmap, rect, flags, format);
        }

        public class DisposableImageData : IDisposable
        {
            private readonly Bitmap _bitmap;
            private readonly BitmapData _data;

            internal DisposableImageData(Bitmap bitmap, Rectangle rect, ImageLockMode flags, PixelFormat format)
            {
                if (bitmap == null)
                    throw new ArgumentNullException("bitmap");
                _bitmap = bitmap;
                _data = bitmap.LockBits(rect, flags, format);
            }

            public void Dispose()
            {
                _bitmap.UnlockBits(_data);
            }

            public IntPtr Scan0
            {
                get { return _data.Scan0; }
            }

            public int Stride
            {
                get { return _data.Stride; }
            }

            public int Width
            {
                get { return _data.Width; }
            }

            public int Height
            {
                get { return _data.Height; }
            }

            public PixelFormat PixelFormat
            {
                get { return _data.PixelFormat; }
            }

            public int Reserved
            {
                get { return _data.Reserved; }
            }
        }   
    }
}
