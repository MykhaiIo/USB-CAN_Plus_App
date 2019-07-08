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
            public static int inVoltOutSI;
            public static float inCurntOutSI;
            public static float outVoltOutFP;
            public static float outCurntOutFP;
        }

        public USB_CAN_Plus_Ctrl()
        {
            InitializeComponent();
        }

        

        /*private void HEXtoFLOAT_Click(object sender, EventArgs e)
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

        }*/

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

        

        /*private void GetDataFromCAN_Click(object sender, EventArgs e)
        {
            HEXCurTextBox.Text = DataFromCAN.GetStrHEXCurValue();
        }*/

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnConnect1_Click(object sender, EventArgs e)
        {
            DataFromCAN.CanDevice = DataFromCAN.InitCAN();
            CanDeviceParams.inVoltOutSI = (int)nudOutVoltSI1.Value*1000;
            CanDeviceParams.inCurntOutSI = (float)nudOutCurntSI1.Value*1000;
            VoltHEXWritten =  NumRepresentations.INTtoHEX(CanDeviceParams.inVoltOutSI);
            CurntHEXWritten = NumRepresentations.FPtoHEX(CanDeviceParams.inCurntOutSI);
        }

        private void BtnDisconnect1_Click(object sender, EventArgs e)
        {
            DataFromCAN.DeinitCAN(DataFromCAN.CanDevice);
        }
    }
}
