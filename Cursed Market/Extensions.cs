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
            return random.NextLong(long.MinValue, long.MaxValue);
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

            byte[] buffer = new byte[8];
            random.NextBytes(buffer);

            long longRand = BitConverter.ToInt64(buffer, 0);
            longRand = Math.Abs(longRand);

            return minValue + (longRand % (maxValue - minValue));
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
            byte[] hashBytes = null;
            switch (hashType)
            {
                case HashType.MD5:
                    hashBytes = MD5.Create().ComputeHash(input);
                    break;
                case HashType.SHA1:
                    hashBytes = SHA1.Create().ComputeHash(input);
                    break;
                case HashType.SHA256:
                    hashBytes = SHA256.Create().ComputeHash(input);
                    break;
                case HashType.SHA384:
                    hashBytes = SHA384.Create().ComputeHash(input);
                    break;
                case HashType.SHA512:
                    hashBytes = SHA512.Create().ComputeHash(input);
                    break;
            }

            StringBuilder stringBuilder = new StringBuilder();
            int hashBytesLength = hashBytes.Length;
            for (int i = 0; i < hashBytesLength; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
