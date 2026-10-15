using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Text;






namespace Cursed_Market
{
    public static class CrosshairGenerator
    {
        public struct S_CrosshairSettings
        {
            public float Length;           // Line length (0.0 - 20.0).
            public float Thickness;        // Line thickness (0.1 - 10.0).
            public float Gap;              // Center gap (-10.0 - 10.0).
            public bool HasOutline;        // Draw outer black outline.
            public float OutlineThickness; // Outline thickness (0.5 - 3.0).
            public bool CenterDot;         // Draw center dot.
            public float DotSize;          // Center dot size (0.5 - 10.0).
            public bool HasCircle;         // Draw center circle.
            public float CircleRadius;     // Center circle radius (1.0 - 30.0).
            public bool TStyle;            // Hide top crosshair arm.
            public byte Alpha;             // Crosshair alpha (10 - 255).
            public Color Color;            // Custom RGB or preset color.

            public static S_CrosshairSettings Default => new S_CrosshairSettings
            {
                Length = 2.5f,
                Thickness = 1.0f,
                Gap = -1.0f,
                HasOutline = true,
                OutlineThickness = 1.0f,
                CenterDot = false,
                DotSize = 1.0f,
                HasCircle = false,
                CircleRadius = 5.0f,
                TStyle = false,
                Alpha = 255,
                Color = Color.FromArgb(255, 0, 255, 0)
            };
        }

        private const string DictionaryChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefhijkmnopqrstuvwxyz23456789";

        /* Encodes current S_CrosshairSettings into a Share Code ("XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"). */
        public static string EncodeShareCode(S_CrosshairSettings cfg)
        {
            byte[] bytes = PackSettingsToBytes(cfg);

            /* Convert 18-byte big-endian array to unsigned little-endian byte array for BigInteger. */
            byte[] littleEndian = new byte[19];
            for (int i = 0; i < 18; i++)
            {
                littleEndian[i] = bytes[17 - i];
            }
            littleEndian[18] = 0; // Ensure unsigned positive BigInteger.

            BigInteger bigInt = new BigInteger(littleEndian);
            char[] codeChars = new char[25];

            for (int i = 0; i < 25; i++)
            {
                BigInteger remainder = bigInt % 57;
                bigInt /= 57;
                codeChars[i] = DictionaryChars[(int)remainder];
            }

            StringBuilder sb = new StringBuilder(29);
            for (int i = 0; i < 25; i++)
            {
                if (i > 0 && i % 5 == 0)
                    sb.Append('-');
                sb.Append(codeChars[i]);
            }

            return sb.ToString();
        }

        private static byte[] PackSettingsToBytes(S_CrosshairSettings cfg)
        {
            byte[] bytes = new byte[18];

            sbyte rawGap = (sbyte)Math.Max(-128, Math.Min(127, (int)Math.Round(cfg.Gap * 10.0f)));
            byte rawOutline = (byte)Math.Max(0, Math.Min(6, (int)Math.Round(cfg.OutlineThickness * 2.0f)));
            byte rawThickness = (byte)Math.Max(1, Math.Min(255, (int)Math.Round(cfg.Thickness * 10.0f)));
            ushort rawLength = (ushort)Math.Max(0, Math.Min(8191, (int)Math.Round(cfg.Length * 10.0f)));
            byte rawDotSize = (byte)Math.Max(5, Math.Min(100, (int)Math.Round(cfg.DotSize * 10.0f)));
            ushort rawCircleRadius = (ushort)Math.Max(10, Math.Min(300, (int)Math.Round(cfg.CircleRadius * 10.0f)));

            byte colorId = ResolveShareCodeColorId(cfg.Color);

            bytes[1] = 1;
            bytes[2] = (byte)rawGap;
            bytes[3] = rawOutline;
            bytes[4] = cfg.Color.R;
            bytes[5] = cfg.Color.G;
            bytes[6] = cfg.Color.B;
            bytes[7] = cfg.Alpha;
            bytes[8] = rawDotSize;
            bytes[9] = (byte)(rawCircleRadius & 0xFF);

            /* Byte 10: upper nibble = 3, bit 3 = HasOutline, bit 2 = HasCircle. */
            bytes[10] = (byte)(0x30 | (cfg.HasOutline ? 0x08 : 0x00) | (cfg.HasCircle ? 0x04 : 0x00));
            bytes[11] = (byte)((rawCircleRadius >> 8) & 0xFF);
            bytes[12] = rawThickness;

            /* Byte 13: lower 3 bits = colorId, bit 3 = usealpha (1), bit 4 = CenterDot, bits 5..6 = static style (4 << 5), bit 7 = TStyle. */
            byte flags2 = (byte)((colorId & 0x07) | 0x08);
            if (cfg.CenterDot) flags2 |= 0x10;
            flags2 |= (4 << 5) & 0x60;
            if (cfg.TStyle) flags2 |= 0x80;
            bytes[13] = flags2;

            bytes[14] = (byte)(rawLength & 0xFF);
            bytes[15] = (byte)((rawLength >> 8) & 0x1F);
            bytes[16] = 1; // Extended format marker (DotSize & CircleRadius present).
            bytes[17] = 0;

            /* Calculate checksum (byte 0 is the sum of bytes 1..17 modulo 256). */
            byte sum = 0;
            for (int i = 1; i < 18; i++)
                sum = (byte)((sum + bytes[i]) & 0xFF);
            bytes[0] = sum;

            return bytes;
        }

        private static byte ResolveShareCodeColorId(Color c)
        {
            if (c.R == 250 && c.G == 0 && c.B == 0) return 0;     // Red.
            if (c.R == 0 && c.G == 255 && c.B == 0) return 1;     // Green.
            if (c.R == 255 && c.G == 255 && c.B == 0) return 2;   // Yellow.
            if (c.R == 0 && c.G == 0 && c.B == 255) return 3;     // Blue.
            if (c.R == 0 && c.G == 255 && c.B == 255) return 4;   // Cyan.
            if (c.R == 255 && c.G == 255 && c.B == 255) return 5; // White.
            if (c.R == 255 && c.G == 0 && c.B == 255) return 6;   // Pink.
            return 7;                                             // Custom RGB.
        }

        /* Parses Share Code ("XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"). */
        public static bool TryParseShareCode(string shareCode, out S_CrosshairSettings settings)
        {
            settings = S_CrosshairSettings.Default;
            if (string.IsNullOrWhiteSpace(shareCode))
                return false;

            string cleanPayload = shareCode.Trim().Replace("-", string.Empty);
            if (cleanPayload.Length != 25)
                return false;

            BigInteger bigInt = BigInteger.Zero;
            for (int i = cleanPayload.Length - 1; i >= 0; i--)
            {
                int charIndex = DictionaryChars.IndexOf(cleanPayload[i]);
                if (charIndex < 0)
                    return false;

                bigInt = bigInt * 57 + charIndex;
            }

            byte[] decodedBytes = bigInt.ToByteArray();
            byte[] bytes = new byte[18];

            for (int i = 0; i < Math.Min(decodedBytes.Length, 18); i++)
            {
                bytes[17 - i] = decodedBytes[i];
            }

            /* Validate checksum. */
            byte sum = 0;
            for (int i = 1; i < 18; i++)
                sum = (byte)((sum + bytes[i]) & 0xFF);

            if (bytes[0] != sum)
                return false;

            sbyte rawGap = (sbyte)bytes[2];
            byte rawOutline = bytes[3];
            byte r = bytes[4];
            byte g = bytes[5];
            byte b = bytes[6];
            byte alpha = bytes[7];
            byte rawDotSize = bytes[8];
            byte flags1 = bytes[10];
            byte rawThickness = bytes[12];
            byte flags2 = bytes[13];
            ushort rawLength = (ushort)(((bytes[15] & 0x1F) << 8) | bytes[14]);
            ushort rawCircleRadius = (ushort)((bytes[11] << 8) | bytes[9]);
            bool hasExtendedFields = bytes[16] == 1;

            settings.Gap = Math.Max(-10.0f, Math.Min(10.0f, rawGap / 10.0f));
            settings.OutlineThickness = Math.Max(0.5f, Math.Min(3.0f, rawOutline / 2.0f));
            settings.Alpha = Math.Max((byte)10, alpha);
            settings.HasOutline = (flags1 & 0x08) != 0;
            settings.HasCircle = hasExtendedFields && (flags1 & 0x04) != 0;
            settings.Thickness = Math.Max(0.1f, Math.Min(10.0f, rawThickness / 10.0f));
            settings.CenterDot = ((flags2 >> 4) & 1) != 0;
            settings.TStyle = ((flags2 >> 7) & 1) != 0;
            settings.Length = Math.Max(0.0f, Math.Min(20.0f, rawLength / 10.0f));

            if (hasExtendedFields)
            {
                settings.DotSize = Math.Max(0.5f, Math.Min(10.0f, rawDotSize / 10.0f));
                settings.CircleRadius = Math.Max(1.0f, Math.Min(30.0f, rawCircleRadius / 10.0f));
            }
            else
            {
                settings.DotSize = Math.Max(0.5f, Math.Min(10.0f, settings.Thickness));
                settings.CircleRadius = 5.0f;
            }

            byte colorId = (byte)(flags2 & 0x07);
            switch (colorId)
            {
                case 0: settings.Color = Color.FromArgb(255, 250, 0, 0); break;
                case 1: settings.Color = Color.FromArgb(255, 0, 255, 0); break;
                case 2: settings.Color = Color.FromArgb(255, 255, 255, 0); break;
                case 3: settings.Color = Color.FromArgb(255, 0, 0, 255); break;
                case 4: settings.Color = Color.FromArgb(255, 0, 255, 255); break;
                case 5:
                    settings.Color = (r == 0 && g == 0 && b == 0)
                        ? Color.FromArgb(255, 255, 255, 255)
                        : Color.FromArgb(255, r, g, b);
                    break;
                case 6: settings.Color = Color.FromArgb(255, 255, 0, 255); break;
                default: settings.Color = Color.FromArgb(255, r, g, b); break;
            }

            return true;
        }

        /* Renders crosshair settings into a 32-bit ARGB Bitmap. */
        public static Bitmap RenderCrosshair(S_CrosshairSettings cfg, int canvasSize = 256)
        {
            Bitmap bmp = new Bitmap(canvasSize, canvasSize, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.None;
                g.PixelOffsetMode = PixelOffsetMode.Half;

                float scale = 2.0f;
                float center = canvasSize / 2.0f;

                float thickness = Math.Max(1.0f, (float)Math.Round(cfg.Thickness * scale));
                float length = (float)Math.Round(cfg.Length * scale);
                float gap = Math.Max(-length, (float)Math.Round(cfg.Gap * scale) + 6.0f);
                float outline = cfg.HasOutline ? Math.Max(1.0f, (float)Math.Round(cfg.OutlineThickness)) : 0f;

                Color mainColor = Color.FromArgb(cfg.Alpha, cfg.Color.R, cfg.Color.G, cfg.Color.B);
                Color outlineColor = Color.FromArgb(cfg.Alpha, 0, 0, 0);

                if (cfg.HasCircle && cfg.CircleRadius > 0.0f)
                {
                    float radius = (float)Math.Round(cfg.CircleRadius * scale);
                    float diameter = radius * 2.0f;
                    float topLeft = center - radius;

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    if (cfg.HasOutline && outline > 0)
                    {
                        using (Pen outlinePen = new Pen(outlineColor, thickness + outline * 2.0f))
                        {
                            g.DrawEllipse(outlinePen, topLeft, topLeft, diameter, diameter);
                        }
                    }
                    using (Pen mainPen = new Pen(mainColor, thickness))
                    {
                        g.DrawEllipse(mainPen, topLeft, topLeft, diameter, diameter);
                    }
                    g.SmoothingMode = SmoothingMode.None;
                }

                using (SolidBrush mainBrush = new SolidBrush(mainColor))
                using (SolidBrush outlineBrush = new SolidBrush(outlineColor))
                {
                    void DrawBar(float x, float y, float w, float h)
                    {
                        if (w <= 0 || h <= 0) return;
                        if (cfg.HasOutline && outline > 0)
                        {
                            g.FillRectangle(outlineBrush, x - outline, y - outline, w + outline * 2, h + outline * 2);
                        }
                        g.FillRectangle(mainBrush, x, y, w, h);
                    }

                    float halfThick = (float)Math.Floor(thickness / 2.0f);

                    DrawBar(center - gap - length, center - halfThick, length, thickness);
                    DrawBar(center + gap, center - halfThick, length, thickness);
                    DrawBar(center - halfThick, center + gap, thickness, length);

                    if (!cfg.TStyle)
                    {
                        DrawBar(center - halfThick, center - gap - length, thickness, length);
                    }

                    if (cfg.CenterDot)
                    {
                        float dotSize = Math.Max(1.0f, (float)Math.Round(cfg.DotSize * scale));
                        float halfDot = (float)Math.Floor(dotSize / 2.0f);
                        DrawBar(center - halfDot, center - halfDot, dotSize, dotSize);
                    }
                }
            }
            return bmp;
        }
    }
}