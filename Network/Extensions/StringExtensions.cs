using System;
using System.Linq;
using System.Text;

namespace Network.Extensions
{
    public static class StringExtensions
    {
        public enum UnformatOptions
        {
            /// <summary>
            /// Keep only the digits.
            /// </summary>
            DigitsOnly,

            /// <summary>
            /// Keep only the letters.
            /// </summary>
            LettersOnly,

            /// <summary>
            /// Keep letters and digits only.
            /// </summary>
            DigitsAndLettersOnly,
        }

        public static String FormatId(this String str)
        {
            int insertIndex = (str.Length/2) - 1;
            return str.Insert(insertIndex, "-");
        }

        public static String Unformat(this String str, UnformatOptions options)
        {
            var stringBuilder = new StringBuilder();

            switch (options)
            {
                case UnformatOptions.DigitsOnly:
                    foreach (char c in str.Where(e => Char.IsDigit(e)))
                    {
                        stringBuilder.Append(c);
                    }
                    break;
                case UnformatOptions.LettersOnly:
                    foreach (char c in str.Where(e => Char.IsLetter(e)))
                    {
                        stringBuilder.Append(c);
                    }
                    break;
                case UnformatOptions.DigitsAndLettersOnly:
                    foreach (char c in str.Where(e => Char.IsLetterOrDigit(e)))
                    {
                        stringBuilder.Append(c);
                    }
                    break;
            }

            return stringBuilder.ToString();
        }
    }
}