using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USB_CAN_Plus_Ctrl
{
    static class NumRepresentations
    {
        private static bool IsValid(string s) =>
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            System.Text.RegularExpressions.Regex.IsMatch(s, @"\A\b[0-9a-fA-F]+\b\Z");

        public static byte[] FPtoBYTE(float fp) => BitConverter.GetBytes(fp);

        public static float BYTEtoFP(byte[] b) => BitConverter.ToSingle(b, 0);

        public static byte[] INTtoBYTE(int i) => BitConverter.GetBytes(i);

        public static int BYTEtoINT(byte[] b) => BitConverter.ToInt32(b, 0);

        public static byte[] UINTtoBYTE(uint ui) => BitConverter.GetBytes(ui);

        public static uint BYTEtoUINT(byte[] b) => BitConverter.ToUInt16(b, 0);

        public static string ToFormattedFloat(float value) => string.Format("{0:##0.00}", value);

        private static string ToEngineering(double value, string unitName)
        {
            int exp = (int)(Math.Floor(Math.Log10(value) / 3.0) * 3.0);
            double newValue = value * Math.Pow(10.0, -exp);
            if (newValue >= 1000.0)
            {
                newValue /= 1000.0;
                exp += 3;
            }
            var symbol = String.Empty;
            switch (exp)
            {
                case 0:
                    symbol = "";
                    break;
                case 3:
                    symbol = "k";
                    break;
                case 6:
                    symbol = "M";
                    break;
                case 9:
                    symbol = "G";
                    break;
                case 12:
                    symbol = "T";
                    break;
                case -3:
                    symbol = "m";
                    break;
                case -6:
                    symbol = "μ";
                    break;
                case -9:
                    symbol = "n";
                    break;
                case -12:
                    symbol = "p";
                    break;
                default:
                    symbol = "?";
                    break;
            }

            return string.Format("{0:##0.000} {1}{2}", newValue, symbol, unitName);
        }
    }
}
