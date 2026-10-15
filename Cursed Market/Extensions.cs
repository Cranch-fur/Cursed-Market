using Newtonsoft.Json.Linq;
using System;
using System.Security.Cryptography;
using System.Text;






namespace Cursed_Market
{
    public static class Extensions
    {
        public static bool IsJson(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            try
            {
                JContainer.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }




        public static long NextLong(this Random random)
        {
            byte[] buffer = new byte[8];
            random.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }
        public static long NextLong(this Random random, long maxValue)
        {
            return random.NextLong(0, maxValue);
        }
        public static long NextLong(this Random random, long minValue, long maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue), "minValue must be less than or equal to maxValue");
            }

            if (minValue == maxValue)
                return minValue;

            ulong uRange = (ulong)(maxValue - minValue);
            byte[] buffer = new byte[8];
            ulong ulongRand;

            /* Reject values that would introduce modulo bias. */
            ulong limit = ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange;
            do
            {
                random.NextBytes(buffer);
                ulongRand = BitConverter.ToUInt64(buffer, 0);
            } while (ulongRand > limit);

            return (long)(ulongRand % uRange) + minValue;
        }




        public static int Clamp(this int value, int maxValue)
        {
            return Clamp(value, int.MinValue, maxValue);
        }
        public static int Clamp(this int value, int minValue, int maxValue)
        {
            if (value < minValue)
                return minValue;

            if (value > maxValue)
                return maxValue;

            return value;
        }




        public enum HashType
        {
            MD5,
            SHA1,
            SHA256,
            SHA384,
            SHA512
        }

        public static string ComputeHash(this byte[] input, HashType hashType)
        {
            if (input == null)
                return string.Empty;

            byte[] hashBytes = null;
            switch (hashType)
            {
                case HashType.MD5:
                    using (MD5 md5 = MD5.Create())
                        hashBytes = md5.ComputeHash(input);
                    break;
                case HashType.SHA1:
                    using (SHA1 sha1 = SHA1.Create())
                        hashBytes = sha1.ComputeHash(input);
                    break;
                case HashType.SHA256:
                    using (SHA256 sha256 = SHA256.Create())
                        hashBytes = sha256.ComputeHash(input);
                    break;
                case HashType.SHA384:
                    using (SHA384 sha384 = SHA384.Create())
                        hashBytes = sha384.ComputeHash(input);
                    break;
                case HashType.SHA512:
                    using (SHA512 sha512 = SHA512.Create())
                        hashBytes = sha512.ComputeHash(input);
                    break;
                default:
                    return string.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder(hashBytes.Length * 2);
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}