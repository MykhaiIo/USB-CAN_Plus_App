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
        public byte[][] CurntBWritten { get; private set; } = new byte[2][];
        public byte[][] VoltBWritten { get; private set; } = new byte[2][];
        public byte[][] VoltBRead { get; private set; } = new byte[2][];
        
        public byte[][] CurntBRead { get; private set; } = new byte[2][];
        public byte[][] AmbientTemp { get; private set; } = new byte[2][];
        public byte[][] VoltAB { get; private set; } = new byte[2][];
        public byte[][] VoltBC { get; private set; } = new byte[2][];
        public byte[][] VoltCA { get; private set; } = new byte[2][];


        private VSCAN[] CanDevice = new VSCAN[2];

        public USB_CAN_Plus_Ctrl()
        {
            InitializeComponent();
            grpModule1.Enabled = false;
            grpModule2.Enabled = false;
        }

        private void DisplayDeviceParams()
        {
            VSCAN_HWPARAM hw = new VSCAN_HWPARAM();
            CanDevice[0].GetHwParams(ref hw);

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
            ConnectDisconnectCAN(ref CanDevice[0]);
        }

        private void BtnConnect2_Click(object sender, EventArgs e)
        {
            ConnectDisconnectCAN(ref CanDevice[0]);
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
            VoltBWritten[0] = NumRepresentations.UINTtoBYTE((uint)nudOutVoltSI1.Value * 1000);
            CurntBWritten[0] = NumRepresentations.UINTtoBYTE((uint)nudOutCurntSI1.Value * 1000);

            // handle endianness
            Array.Reverse(VoltBWritten[0]);
            Array.Reverse(CurntBWritten[0]);

            byte[] Data = new byte[8];

            // form HEX volt value to be sent
            for (int i = 0; i < 4; i++)
            {
                Data[i] = VoltBWritten[0][i];
            }

            for (int i = 4; i < 8; i++)
            {
                Data[i] = CurntBWritten[0][i - 4];
            }

            if (!DataFromCAN.SendData(CanDevice[0], 0x029B3FF0, Data))
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
                AmbientTemp[0] = new byte[4];
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice);
                AmbientTemp[0][0] = msgs[0].Data[4];
                Array.Reverse(AmbientTemp[0]);
                txtTemperature1.Text = NumRepresentations.BYTEtoINT(AmbientTemp[0]).ToString();
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
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice);
                VoltAB[0] = new byte[4];
                VoltBC[0] = new byte[4];
                VoltCA[0] = new byte[4];


                for (int i = 0; i < 2; i++)
                {
                    VoltAB[0][i] = msgs[0].Data[i];
                }

                for (int i = 2; i < 4; i++)
                {
                    VoltBC[0][i - 2] = msgs[0].Data[i];
                }

                for (int i = 4; i < 6; i++)
                {
                    VoltCA[0][i - 4] = msgs[0].Data[i];
                }

                // handle endianness
                Array.Reverse(VoltAB[0]);
                Array.Reverse(VoltBC[0]);
                Array.Reverse(VoltCA[0]);
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

            VoltBRead[0] = new byte[4];
            CurntBRead[0] = new byte[4];

            SendChargeParams();
            try
            {
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice[0]);

                // form volt value to be recieved
                for (int i = 0; i < 4; i++)
                {
                    VoltBRead[0][i] = msgs[0].Data[i];
                }

                for (int i = 4; i < 8; i++)
                {
                    CurntBRead[0][i - 4] = msgs[0].Data[i];
                }

                // handle endianness
                Array.Reverse(VoltBRead[0]);
                Array.Reverse(CurntBRead[0]);

                txtOutVoltFP1.Text = NumRepresentations.BYTEtoUINT(VoltBRead[0]) + " мВ";
                txtOutCurntFP1.Text = NumRepresentations.BYTEtoUINT(CurntBRead[0]) + " мА";
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
                    VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice[0]);
                    VoltBRead[0] = new byte[4];

                    // form volt value to be recieved
                    for (int i = 0; i < 4; i++)
                    {
                        VoltBRead[0][i] = msgs[0].Data[i];
                    }

                    // handle endianness
                    Array.Reverse(VoltBRead[0]);
                    txtOutVoltFP1.Text = NumRepresentations.BYTEtoUINT(VoltBRead[0]) + " мВ";
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
                    VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice[0]);
                    CurntBRead[0] = new byte[4];

                    // form current value to be recieved
                    for (int i = 4; i < 8; i++)
                    {
                        CurntBRead[0][i - 4] = msgs[0].Data[i];
                    }

                    // handle endianness
                    Array.Reverse(CurntBRead[0]);
                    txtOutCurntFP1.Text = NumRepresentations.BYTEtoUINT(CurntBRead[0]) + " мА";
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
                    VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice[1]);
                    VoltBRead[1] = new byte[4];

                    // form volt value to be recieved
                    for (int i = 0; i < 4; i++)
                    {
                        VoltBRead[1][i] = msgs[0].Data[i];
                    }

                    // handle endianness
                    Array.Reverse(VoltBRead[1]);
                    txtOutVoltFP2.Text = NumRepresentations.BYTEtoUINT(VoltBRead[1]) + " мВ";
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
                    VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevice[1]);
                    CurntBRead[1] = new byte[4];

                    // form current value to be recieved
                    for (int i = 4; i < 8; i++)
                    {
                        CurntBRead[1][i - 4] = msgs[0].Data[i];
                    }

                    // handle endianness
                    Array.Reverse(CurntBRead[1]);
                    txtOutCurntFP2.Text = NumRepresentations.BYTEtoUINT(CurntBRead[1]) + " мА";
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
                GetAmbientDeviceTemp(CanDevice[0]);
                GetCurrentVoltage(CanDevice[0]);
                txtPhaseABVolt1.Text = NumRepresentations.BYTEtoFP(VoltAB[0]).ToString();
                txtPhaseBCVolt1.Text = NumRepresentations.BYTEtoFP(VoltBC[0]).ToString();
                txtPhaseCAVolt1.Text = NumRepresentations.BYTEtoFP(VoltCA[0]).ToString();
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
