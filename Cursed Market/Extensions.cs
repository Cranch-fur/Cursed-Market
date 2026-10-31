using Avalonia.Controls;
using Avalonia.Media;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
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

        /* Absolute http(s) link. Anything else passed to Process.Start can open a local file or run a program. */
        public static bool IsWebUrl(this string input)
        {
            return Uri.TryCreate(input, UriKind.Absolute, out Uri uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
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




        /* Properties.Resources still hands out System.Drawing images / icons, Avalonia needs its own types for them. */
        public static Avalonia.Media.Imaging.Bitmap ToAvaloniaBitmap(this System.Drawing.Image image)
        {
            if (image == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;
                return new Avalonia.Media.Imaging.Bitmap(ms);
            }
        }
        public static WindowIcon ToWindowIcon(this System.Drawing.Icon icon)
        {
            if (icon == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                icon.Save(ms);
                ms.Position = 0;
                return new WindowIcon(ms);
            }
        }
        public static IBrush ToBrush(this Color color)
        {
            return new SolidColorBrush(color);
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