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
        public byte[] CurntBWritten1 { get; private set; }
        public byte[] VoltBWritten1 { get; private set; }
        public byte[] VoltBRead1 { get; private set; } = new byte[4];
        public byte[] CurntBRead1 { get; private set; } = new byte[4];
        public byte[] AmbientTemp1 { get; private set; } = new byte[4];
        public byte[] VoltAB1 { get; private set; } = new byte[4];
        public byte[] VoltBC1 { get; private set; } = new byte[4];
        public byte[] VoltCA1 { get; private set; } = new byte[4];

        public byte[] CurntBWritten2 { get; private set; }
        public byte[] VoltBWritten2 { get; private set; }
        public byte[] VoltBRead2 { get; private set; } = new byte[4];
        public byte[] CurntBRead2 { get; private set; } = new byte[4];
        public byte[] AmbientTemp2 { get; private set; } = new byte[4];
        public byte[] VoltAB2 { get; private set; } = new byte[4];
        public byte[] VoltBC2 { get; private set; } = new byte[4];
        public byte[] VoltCA2 { get; private set; } = new byte[4];

        private VSCAN CanDevice1;
        private VSCAN CanDevice2;

        public USB_CAN_Plus_Ctrl()
        {
            InitializeComponent();
            grpModule1.Enabled = false;
            grpModule2.Enabled = false;
        }

        private void DisplayDeviceParams()
        {
            VSCAN_HWPARAM hw = new VSCAN_HWPARAM();
            CanDevice1.GetHwParams(ref hw);

            // get HW Params
            lblSerialNo1.Text = $"Серійний номер: {hw.SerialNr}";
        }

        private void ConnectDisconnectCAN(ref VSCAN CanDevice)
        {
            if (grpModule1.Enabled)
            {
                if (btnConnect1.Text == "Підключити")
                {
                    try
                    {
                        CanDevice = DataFromCAN.InitCAN();
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
                else if (btnConnect1.Text == "Відключити" && grpModule1.Enabled)
                {
                    DataFromCAN.DeinitCAN(CanDevice);

                    lblSerialNo1.Text = "Серійний номер:";
                    btnConnect1.Text = "Підключити";
                }
            }
            /*else if (grpModule2.Enabled)
            {

                if (btnConnect2.Text == "Підключити")
                {
                    try
                    {
                        CanDevice = DataFromCAN.InitCAN();
                        DisplayDeviceParams();
                        UpdateChargeParams();
                        btnConnect2.Text = "Відключити";
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Не вдалося пдключити модуль USB-CAN Plus",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                }
                else if (btnConnect2.Text == "Відключити")
                {
                    DataFromCAN.DeinitCAN(CanDevice);

                    lblSerialNo2.Text = "Серійний номер:";
                    btnConnect2.Text = "Підключити";
                }
            

            }*/
        }
        private void BtnConnect1_Click(object sender, EventArgs e)
        {
            ConnectDisconnectCAN(ref CanDevice1);
        }

        private void BtnConnect2_Click(object sender, EventArgs e)
        {
            ConnectDisconnectCAN(ref CanDevice2);
        }

        private UInt32 GetID(byte ErrorCode,
                             byte DeviceNo,
                             byte CommandNo,
                             byte DestAddress,
                             byte SourceAddress)
        {
            string[] idParams = new string[5];
            idParams[0] = ErrorCode.ToString("X").Remove(0); // 00000111 -> 111  or 07 -> 7
            idParams[1] = DeviceNo.ToString("X").Remove(0);  // 00001111 -> 1111 or 0F -> F
            idParams[2] = CommandNo.ToString("X2");
            idParams[3] = DestAddress.ToString("X2");
            idParams[4] = SourceAddress.ToString("X2");
            StringBuilder strID = new StringBuilder("", 8);

            foreach (string str in idParams)
            {
                strID.Append(str);
            }

            strID.Replace(" ", "");

            if (UInt32.TryParse(strID.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier, null, out UInt32 ID))
            {
                return ID; // 29 bits -> 32 bits
            }
            else
                return 0;
        }

        private char GetErrorCodeFromID(UInt32 ID)
        {
            string strID = ID.ToString();
            char strEC = strID[0];
            return strEC;
        }

        private void SendChargeParams()
        {
            VoltBWritten1 = NumRepresentations.UINTtoBYTE((uint)nudOutVoltSI1.Value * 1000);
            CurntBWritten1 = NumRepresentations.UINTtoBYTE((uint)nudOutCurntSI1.Value * 1000);

            // handle endianness
            Array.Reverse(VoltBWritten1);
            Array.Reverse(CurntBWritten1);

            byte[] Data = new byte[8];

            // form HEX volt value to be sent
            for (int i = 0; i < 4; i++)
            {
                Data[i] = VoltBWritten1[i];
            }

            for (int i = 4; i < 8; i++)
            {
                Data[i] = CurntBWritten1[i - 4];
            }

            if (!DataFromCAN.SendData(CanDevice1, 0x029B3FF0, Data))
                MessageBox.Show("Помилка при передачі даних",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
        }



        private void GetAmbientDeviceTemp(VSCAN CanDevice)
        {
            DataFromCAN.SendData(CanDevice, 0x028401F0, new byte[8]);
            try
            {
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice1);
                AmbientTemp1[0] = msgs[0].Data[4];
                Array.Reverse(AmbientTemp1);
                txtTemperature1.Text = NumRepresentations.BYTEtoINT(AmbientTemp1).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Помилка при передачі даних",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void GetCurrentVoltage(VSCAN CanDevice)
        {
            DataFromCAN.SendData(CanDevice, 0x028601F0, new byte[8]);
            try
            {
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice1);

                for (int i = 0; i < 2; i++)
                {
                    VoltAB1[i] = msgs[0].Data[i];
                }

                for (int i = 2; i < 4; i++)
                {
                    VoltBC1[i - 2] = msgs[0].Data[i];
                }

                for (int i = 4; i < 6; i++)
                {
                    VoltCA1[i - 4] = msgs[0].Data[i];
                }

                // handle endianness
                Array.Reverse(VoltAB1);
                Array.Reverse(VoltBC1);
                Array.Reverse(VoltCA1);
            }
            catch (Exception)
            {
                MessageBox.Show("Помилка при передачі даних",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void UpdateChargeParams()
        {
            SendChargeParams();
            try
            {
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice1);

                // form volt value to be recieved
                for (int i = 0; i < 4; i++)
                {
                    VoltBRead1[i] = msgs[0].Data[i];
                }

                for (int i = 4; i < 8; i++)
                {
                    CurntBRead1[i - 4] = msgs[0].Data[i];
                }

                // handle endianness
                Array.Reverse(VoltBRead1);
                Array.Reverse(CurntBRead1);

                txtOutVoltFP1.Text = NumRepresentations.BYTEtoUINT(VoltBRead1) + " мВ";
                txtOutCurntFP1.Text = NumRepresentations.BYTEtoUINT(CurntBRead1) + " мА";
            }
            catch (Exception)
            {
                MessageBox.Show("Помилка при передачі даних",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void NudOutVoltSI1_ValueChanged(object sender, EventArgs e)
        {
            if (btnConnect1.Text == "Відключити")
            {
                SendChargeParams();
                try
                {
                    VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice1);

                    // form volt value to be recieved
                    for (int i = 0; i < 4; i++)
                    {
                        VoltBRead1[i] = msgs[0].Data[i];
                    }

                    // handle endianness
                    Array.Reverse(VoltBRead1);
                    txtOutVoltFP1.Text = NumRepresentations.BYTEtoUINT(VoltBRead1) + " мВ";
                }
                catch (Exception)
                {
                    MessageBox.Show("Помилка при передачі даних",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
        }

        private void NudOutCurntSI1_ValueChanged(object sender, EventArgs e)
        {
            if (btnConnect1.Text == "Відключити")
            {
                SendChargeParams();
                try
                {
                    VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice1);

                    // form current value to be recieved
                    for (int i = 4; i < 8; i++)
                    {
                        CurntBRead1[i - 4] = msgs[0].Data[i];
                    }

                    // handle endianness
                    Array.Reverse(CurntBRead1);
                    txtOutCurntFP1.Text = NumRepresentations.BYTEtoUINT(CurntBRead1) + " мА";
                }
                catch (Exception)
                {
                    MessageBox.Show("Помилка при передачі даних",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
        }

        private void NudOutVoltSI2_ValueChanged(object sender, EventArgs e)
        {
            if (btnConnect2.Text == "Відключити")
            {
                SendChargeParams();
                try
                {
                    VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice2);

                    // form volt value to be recieved
                    for (int i = 0; i < 4; i++)
                    {
                        VoltBRead2[i] = msgs[0].Data[i];
                    }

                    // handle endianness
                    Array.Reverse(VoltBRead2);
                    txtOutVoltFP2.Text = NumRepresentations.BYTEtoUINT(VoltBRead2) + " мВ";
                }
                catch (Exception)
                {
                    MessageBox.Show("Помилка при передачі даних",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
        }

        private void NudOutCurntSI2_ValueChanged(object sender, EventArgs e)
        {
            if (btnConnect2.Text == "Відключити")
            {
                SendChargeParams();
                try
                {
                    VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice2);

                    // form current value to be recieved
                    for (int i = 4; i < 8; i++)
                    {
                        CurntBRead2[i - 4] = msgs[0].Data[i];
                    }

                    // handle endianness
                    Array.Reverse(CurntBRead2);
                    txtOutCurntFP2.Text = NumRepresentations.BYTEtoUINT(CurntBRead2) + " мА";
                }
                catch (Exception)
                {
                    MessageBox.Show("Помилка при передачі даних",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
        }

        private void tmrDeviceParams_Tick(object sender, EventArgs e)
        {
            if (btnConnect1.Text == "Відключити")
            {
                GetAmbientDeviceTemp(CanDevice1);
                GetCurrentVoltage(CanDevice1);
                txtPhaseABVolt1.Text = NumRepresentations.BYTEtoFP(VoltAB1).ToString();
                txtPhaseBCVolt1.Text = NumRepresentations.BYTEtoFP(VoltBC1).ToString();
                txtPhaseCAVolt1.Text = NumRepresentations.BYTEtoFP(VoltCA1).ToString();
            }
        }

        private void CmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = cmbDevices.SelectedItem.ToString();
            switch (selectedState)
            {
                case "Активний перший модуль":
                    grpModule1.Enabled = true;
                    grpModule2.Enabled = false;
                    btnConnect2.Text = "Підключити";
                    break;
                case "Активний другий модуль":
                    grpModule1.Enabled = false;
                    grpModule2.Enabled = true;
                    btnConnect1.Text = "Підключити";
                    break;
                case "Активні обидва модулі":
                    grpModule1.Enabled = true;
                    grpModule2.Enabled = true;
                    btnConnect1.Text = "Підключити";
                    btnConnect2.Text = "Підключити";
                    break;
                
            }
        }
    }
}
