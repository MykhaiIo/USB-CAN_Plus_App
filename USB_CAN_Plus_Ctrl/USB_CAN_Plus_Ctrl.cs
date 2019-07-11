using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using VSCom.CanApi;

namespace USB_CAN_Plus_Ctrl
{
    public partial class USB_CAN_Plus_Ctrl : Form
    {
        public byte[] CurntBWritten { get; private set; }
        public byte[] VoltBWritten { get; private set; }
        public byte[] VoltBRead { get; private set; } = new byte[4];
        public byte[] CurntBRead { get; private set; } = new byte[4];
        public byte[] AmbientTemp { get; private set; } = new byte[4];
        public byte[] VoltAB { get; private set; } = new byte[4];
        public byte[] VoltBC { get; private set; } = new byte[4];
        public byte[] VoltCA { get; private set; } = new byte[4];

        public USB_CAN_Plus_Ctrl()
        {
            InitializeComponent();
        }

        private void DisplayDeviceParams()
        {
            VSCAN_HWPARAM hw = new VSCAN_HWPARAM();
            DataFromCAN.CanDevice.GetHwParams(ref hw);

            // get HW Params
            lblSerialNo.Text = "Серійний номер: " + hw.SerialNr;
        }

        private void BtnConnect1_Click(object sender, EventArgs e)
        {
            if (btnConnect1.Text == "Підключити")
            {
                try
                {
                    DataFromCAN.CanDevice = DataFromCAN.InitCAN();
                    DisplayDeviceParams();
                    UpdateChargeParams();
                    btnConnect1.Text = "Відключити";
                }
                catch (Exception)
                {
                    MessageBox.Show("Не вдалося пдключити модуль USB-CAN Plus",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            else if (btnConnect1.Text == "Відключити")
            {
                DataFromCAN.DeinitCAN(DataFromCAN.CanDevice);
                lblSerialNo.Text = "Серійний номер: ";

                btnConnect1.Text = "Підключити";
            }
        }


        private UInt32 FormIDField(byte ErrorCode,
                                   byte DeviceNo,
                                   byte CommandNo,
                                   byte DestAddress,
                                   byte SourceAddress)
        {
            string[] idParams = new string[5];
            idParams[0] = ErrorCode.ToString("X1"); // 00000111 -> 111 or 07 -> 7
            idParams[1] = DeviceNo.ToString("X1");  // 00001111 -> 1111 or 0F -> F
            idParams[2] = CommandNo.ToString("X2");
            idParams[3] = DestAddress.ToString("X2");
            idParams[4] = SourceAddress.ToString("X2");
            StringBuilder strID = new StringBuilder("", 8);

            foreach (string str in idParams)
            {
                strID.Append(str);
            }

            strID.Replace(" ", "");

            if (UInt32.TryParse(strID.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier, null, out uint ID))
                return ID; // 29 bits -> 32 bits
            else
                return 0x00000000;
        }

        private void SendChargeParams()
        {
            VoltBWritten = NumRepresentations.UINTtoBYTE((uint)nudOutVoltSI1.Value * 1000);
            CurntBWritten = NumRepresentations.UINTtoBYTE((uint)nudOutCurntSI1.Value * 1000);

            // handle endianness
            Array.Reverse(VoltBWritten);
            Array.Reverse(CurntBWritten);

            byte[] Data = new byte[8];

            // form HEX volt value to be sent
            for (int i = 0; i < 4; i++)
            {
                Data[i] = VoltBWritten[i];
            }

            for (int i = 4; i < 8; i++)
            {
                Data[i] = CurntBWritten[i - 4];
            }

            if (!DataFromCAN.SendData(0x029B3FF0, Data))
                MessageBox.Show("Помилка при передачі даних",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
        }

        private void GetAmbientDeviceTemp()
        {
            DataFromCAN.SendData(0x028401F0, new byte[8]);
            VSCAN_MSG[] msgs = DataFromCAN.GetData();
            AmbientTemp[0] = msgs[0].Data[4];
            txtTemperature1.Text = NumRepresentations.BYTEtoINT(AmbientTemp).ToString();
        }

        private void GetCurrentVoltage()
        {
            DataFromCAN.SendData(0x028601F0, new byte[8]);
            VSCAN_MSG[] msgs  = DataFromCAN.GetData();

            for (int i = 0; i < 2; i++)
            {
                VoltAB[i]     = msgs[0].Data[i];
            }
            
            for (int i = 2; i < 4; i++)
            {
                VoltBC[i - 2] = msgs[0].Data[i];
            }        

            for (int i = 4; i < 6; i++)
            {
                VoltCA[i - 4] = msgs[0].Data[i];
            }

            // handle endianness
            Array.Reverse(VoltAB);
            Array.Reverse(VoltBC);
            Array.Reverse(VoltCA);
        }

        private void UpdateChargeParams()
        {
            SendChargeParams();
            VSCAN_MSG[] msgs = DataFromCAN.GetData();

            // form volt value to be recieved
            for (int i = 0; i < 4; i++)
            {
                VoltBRead[i] = msgs[0].Data[i];
            }

            for (int i = 4; i < 8; i++)
            {
                CurntBRead[i - 4] = msgs[0].Data[i];
            }

            // handle endianness
            Array.Reverse(VoltBRead);
            Array.Reverse(CurntBRead);

            txtOutVoltFP1.Text = NumRepresentations.BYTEtoUINT(VoltBRead) + " мВ";
            txtOutCurntFP1.Text = NumRepresentations.BYTEtoUINT(CurntBRead) + " мА";
        }

        private void NudOutVoltSI1_ValueChanged(object sender, EventArgs e)
        {
            if (btnConnect1.Text == "Відключити")
            {
                SendChargeParams();
                VSCAN_MSG[] msgs = DataFromCAN.GetData();

                // form volt value to be recieved
                for (int i = 0; i < 4; i++)
                {
                    VoltBRead[i] = msgs[0].Data[i];
                }

                // handle endianness
                Array.Reverse(VoltBRead);
                txtOutVoltFP1.Text = NumRepresentations.BYTEtoUINT(VoltBRead) + " мВ";
            }
        }

        private void NudOutCurntSI1_ValueChanged(object sender, EventArgs e)
        {
            if (btnConnect1.Text == "Відключити")
            {
                SendChargeParams();
                VSCAN_MSG[] msgs = DataFromCAN.GetData();

                // form current value to be recieved
                for (int i = 4; i < 8; i++)
                {
                    CurntBRead[i - 4] = msgs[0].Data[i];
                }

                // handle endianness
                Array.Reverse(CurntBRead);
                txtOutCurntFP1.Text = NumRepresentations.BYTEtoUINT(CurntBRead) + " мА";
            }
        }

        private void tmrDeviceParams_Tick(object sender, EventArgs e)
        {
            if (btnConnect1.Text == "Відключити")
            {
                GetAmbientDeviceTemp();
                GetCurrentVoltage();
                txtPhaseABVolt1.Text = NumRepresentations.BYTEtoFP(VoltAB).ToString();
                txtPhaseBCVolt1.Text = NumRepresentations.BYTEtoFP(VoltBC).ToString();
                txtPhaseCAVolt1.Text = NumRepresentations.BYTEtoFP(VoltCA).ToString();
            }
        }

        private void NudOutVoltSI2_ValueChanged(object sender, EventArgs e)
        {
            txtOutVoltFP2.Text = (nudOutVoltSI2.Value * 1000).ToString();
            txtOutCurntFP2.Text = (nudOutCurntSI2.Value * 1000).ToString();
        }

        private void NudOutCurntSI2_ValueChanged(object sender, EventArgs e)
        {
            txtOutVoltFP2.Text = ((float)nudOutVoltSI2.Value * 1000).ToString();
            txtOutCurntFP2.Text = ((float)nudOutCurntSI2.Value * 1000).ToString();
        }
    }
}
