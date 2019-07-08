using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VSCom.CanApi;

namespace USB_CAN_Plus_Ctrl
{
    public partial class USB_CAN_Plus_Ctrl : Form
    {
        private int VoltHEXWritten;
        private int CurntHEXWritten;
        private int VoltHEXRead;
        private int CurntHEXRead;

        private struct CanDeviceParams
        {
            public static VSCAN CanDevice;
            public static int inVoltOutSI;
            public static float inCurntOutSI;
            public static float outVoltOutFP;
            public static float outCurntOutFP;
        }

        public USB_CAN_Plus_Ctrl()
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

        private int INTtoHEX(int i)
        {
            int hex = BitConverter.ToInt32(BitConverter.GetBytes(i), 0);
            return hex;
        }

        private int FPtoHEX(float fp)
        {
            int hex = BitConverter.ToInt32(BitConverter.GetBytes(fp), 0);
            return hex;
        }

        private float HEXSTRtoFP(string hex)
        {
            bool success = Int32.TryParse(hex, System.Globalization.NumberStyles.AllowHexSpecifier, null, out int hexval);
            float fval = BitConverter.ToSingle(BitConverter.GetBytes(hexval), 0);

            

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

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnConnect1_Click(object sender, EventArgs e)
        {
            CanDeviceParams.CanDevice = DataFromCAN.InitCAN();
            CanDeviceParams.inVoltOutSI = (int)nudOutVoltSI1.Value*1000;
            CanDeviceParams.inCurntOutSI = (float)nudOutCurntSI1.Value*1000;
            VoltHEXWritten = INTtoHEX(CanDeviceParams.inVoltOutSI);
            CurntHEXWritten = FPtoHEX(CanDeviceParams.inCurntOutSI);
        }

        private void BtnDisconnect1_Click(object sender, EventArgs e)
        {
            DataFromCAN.DeinitCAN(ref CanDeviceParams.CanDevice);
        }
    }
}
