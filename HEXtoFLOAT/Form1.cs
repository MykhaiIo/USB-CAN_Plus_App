using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HEXtoFLOAT
{
    public partial class HEXtoFLOAT : Form
    {
        public HEXtoFLOAT()
        {
            InitializeComponent();
        }

        private bool IsValid(string s)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(s, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        private void HEXtoFLOAT_Click(object sender, EventArgs e)
        {
            var cur = HEXCurTextBox.Text;
            var volt = HEXVoltTextBox.Text;


            if (IsValid(cur) && IsValid(volt))
            {
                FLOATCurTextBox.Text = ToFormattedDouble(HEXSTRtoFP(cur));
                FLOATVoltTextBox.Text = ToFormattedDouble(HEXSTRtoFP(volt));
                SICurVal.Text = ToEngineering(HEXSTRtoFP(cur), "A");
                SIVoltVal.Text = ToEngineering(HEXSTRtoFP(volt), "V");
            }

            else if (cur != "")
            {
                MessageBox.Show("Tension value is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (volt != "")
            {
                MessageBox.Show("Current value is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private float HEXSTRtoFP(string hex)
        {
            bool success = Int32.TryParse(hex, System.Globalization.NumberStyles.AllowHexSpecifier, null, out int hexval);
            float fval = BitConverter.ToSingle(BitConverter.GetBytes(hexval), 0);

            /*
             
             // Hexadecimal Representation of 0.0500
            string HexRep = "3D4CCCCD";
            // Converting to integer
            Int32 IntRep = Int32.Parse(HexRep, NumberStyles.AllowHexSpecifier);
            // Integer to Byte[] and presenting it for float conversion
            float f = BitConverter.ToSingle(BitConverter.GetBytes(IntRep), 0);
            // There you go
            Console.WriteLine("{0}", f);

             */

            if (!success)
            {
                MessageBox.Show("Hexadecimal value is out of range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return float.MaxValue;
            }
            else
                return fval;
        }

        private string ToFormattedDouble(double value)
        {
            return string.Format("{0:##0.00}", value);
        }

        private string ToEngineering(double value, string unitName)
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

        private void GetDataFromCAN_Click(object sender, EventArgs e)
        {
            HEXCurTextBox.Text = DataFromCAN.GetStrHEXCurValue();
        }
    }
}
