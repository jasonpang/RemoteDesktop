using System;
using System.Security.Cryptography;

namespace Model
{
    // Nova ID:   f2h   1,594,323 Permutations
    // Password:  
    public enum GeneratorOptions
    {
        Alpha = 1,
        Numeric = 2,
        AlphaNumeric = Alpha + Numeric,
        AlphaNumericSpecial = 4
    }

    /// <remarks>Copied from http://eyeung003.blogspot.com/2010/09/c-random-password-generator.html </remarks>
    public class AlphaNumericGenerator
    {
        // Define default password length.
        private static int DEFAULT_PASSWORD_LENGTH = 3;

        // No characters that are confusing: i, I, l, L, o, O, 0, 1, u, v
        // No characters that are hard to pronounce over the phone (rhyming letters): a-j-h, b-c-d-e-g-z, m-n...etc

        private static string PASSWORD_CHARS_Alpha =
            "fhpqrw";

        private static string PASSWORD_CHARS_NUMERIC = "2456789";
        private static string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";

        #region Overloads

        /// <summary>
        /// Generates a random password with the default length.
        /// </summary>
        /// <returns>Randomly generated password.</returns>
        public static string Generate()
        {
            return Generate(DEFAULT_PASSWORD_LENGTH,
                            GeneratorOptions.Numeric);
        }

        /// <summary>
        /// Generates a random password with the default length.
        /// </summary>
        /// <returns>Randomly generated password.</returns>
        public static string Generate(GeneratorOptions option)
        {
            return Generate(DEFAULT_PASSWORD_LENGTH, option);
        }

        /// <summary>
        /// Generates a random password with the default length.
        /// </summary>
        /// <returns>Randomly generated password.</returns>
        public static string Generate(int passwordLength)
        {
            return Generate(DEFAULT_PASSWORD_LENGTH,
                            GeneratorOptions.Numeric);
        }

        /// <summary>
        /// Generates a random password.
        /// </summary>
        /// <returns>Randomly generated password.</returns>
        public static string Generate(int passwordLength,
                                      GeneratorOptions option)
        {
            return GeneratePassword(passwordLength, option);
        }

        #endregion

        /// <summary>
        /// Generates the password.
        /// </summary>
        /// <returns></returns>
        private static string GeneratePassword(int passwordLength,
                                               GeneratorOptions option)
        {
            if (passwordLength < 0) return null;

            var passwordChars = GetCharacters(option);

            if (string.IsNullOrEmpty(passwordChars)) return null;

            var password = new char[passwordLength];

            var random = GetRandom();

            for (int i = 0; i < passwordLength; i++)
            {
                var index = random.Next(passwordChars.Length);
                var passwordChar = passwordChars[index];

                password[i] = passwordChar;
            }

            return new string(password);
        }


        /// <summary>
        /// Gets the characters selected by the option
        /// </summary>
        /// <returns></returns>
        private static string GetCharacters(GeneratorOptions option)
        {
            switch (option)
            {
                case GeneratorOptions.Alpha:
                    return PASSWORD_CHARS_Alpha;
                case GeneratorOptions.Numeric:
                    return PASSWORD_CHARS_NUMERIC;
                case GeneratorOptions.AlphaNumeric:
                    return PASSWORD_CHARS_Alpha + PASSWORD_CHARS_NUMERIC;
                case GeneratorOptions.AlphaNumericSpecial:
                    return PASSWORD_CHARS_Alpha + PASSWORD_CHARS_NUMERIC +
                           PASSWORD_CHARS_SPECIAL;
                default:
                    break;
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets a random object with a real random seed
        /// </summary>
        /// <returns></returns>
        private static Random GetRandom()
        {
            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            var randomBytes = new byte[4];

            // Generate 4 random bytes.
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 |
                       randomBytes[1] << 16 |
                       randomBytes[2] << 8 |
                       randomBytes[3];

            // Now, this is real randomization.
            return new Random(seed);
        }
    }
}