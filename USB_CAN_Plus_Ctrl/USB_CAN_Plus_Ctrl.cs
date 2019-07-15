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

        public GroupBox[] GrpsModules { get; private set; }
        public Button[] BtnsDevice { get; private set; }
        public NumericUpDown[] NudsVoltSI { get; private set; }
        public NumericUpDown[] NudsCurntSI { get; private set; }
        public TextBox[] TxtsVoltFP { get; private set; }
        public TextBox[] TxtsCurntFP { get; private set; }
        public TextBox[] TxtsAmbTemperature { get; private set; }
        public TextBox[] TxtsVoltage { get; private set; }
        public TextBox[] TxtsVoltAB { get; private set; }
        public TextBox[] TxtsVoltBC { get; private set; }
        public TextBox[] TxtsVoltCA { get; private set; }
        public Label[] LblsSerialNo { get; private set; }
        internal VSCAN[] CanDevices { get; set; } = new VSCAN[2];

        public USB_CAN_Plus_Ctrl()
        {
            InitializeComponent();
            grpModule1.Enabled = false;
            grpModule2.Enabled = false;
            GrpsModules = new GroupBox[] { grpModule1, grpModule2 };
            BtnsDevice = new Button[] { btnConnect1, btnConnect2 };
            NudsVoltSI = new NumericUpDown[] { nudOutVoltSI1, nudOutVoltSI2 };
            NudsCurntSI = new NumericUpDown[] { nudOutCurntSI1, nudOutCurntSI2 };
            TxtsVoltFP = new TextBox[] { txtOutVoltFP1, txtOutVoltFP2 };
            TxtsCurntFP = new TextBox[] { txtOutCurntFP1, txtOutCurntFP2 };
            TxtsAmbTemperature = new TextBox[] { txtTemperature1, txtTemperature2 };
            TxtsVoltage = new TextBox[] { txtCurVolt1, txtCurVolt2 };
            TxtsVoltAB = new TextBox[] { txtPhaseABVolt1, txtPhaseABVolt2 };
            TxtsVoltBC = new TextBox[] { txtPhaseBCVolt1, txtPhaseBCVolt2 };
            TxtsVoltCA = new TextBox[] { txtPhaseCAVolt1, txtPhaseCAVolt2 };
            LblsSerialNo = new Label[] { lblSerialNo1, lblSerialNo2 };
        }

        private void DisplayDeviceParams(short DeviceNo)
        {
            VSCAN_HWPARAM hw = new VSCAN_HWPARAM();
            CanDevices[DeviceNo].GetHwParams(ref hw);

            // get HW Params
            LblsSerialNo[DeviceNo].Text = $"Серійний номер: {hw.SerialNr}";
        }

        private void ConnectDisconnectCAN(short DeviceNo)
        {
            if (GrpsModules[DeviceNo].Enabled)
            {
                if (BtnsDevice[DeviceNo].Text == "Підключити")
                {
                    try
                    {
                        CanDevices[DeviceNo] = DataFromCAN.InitCAN();
                        DisplayDeviceParams(DeviceNo);
                        UpdateChargeParams(DeviceNo);
                        BtnsDevice[DeviceNo].Text = "Відключити";
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Не вдалося пдключити модуль USB-CAN Plus",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                }
                else if (BtnsDevice[DeviceNo].Text == "Відключити" && GrpsModules[DeviceNo].Enabled)
                {
                    DataFromCAN.DeinitCAN(CanDevices[DeviceNo]);

                    LblsSerialNo[DeviceNo].Text = "Серійний номер:";
                    BtnsDevice[DeviceNo].Text = "Підключити";
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
            ConnectDisconnectCAN(0);
        }

        private void BtnConnect2_Click(object sender, EventArgs e)
        {
            ConnectDisconnectCAN(1);
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

        private void SendChargeParams(short DeviceNo)
        {
            VoltBWritten[DeviceNo] = NumRepresentations.UINTtoBYTE((uint)NudsVoltSI[DeviceNo].Value * 1000);
            CurntBWritten[DeviceNo] = NumRepresentations.UINTtoBYTE((uint)NudsCurntSI[DeviceNo].Value * 1000);

            // handle endianness
            Array.Reverse(VoltBWritten[DeviceNo]);
            Array.Reverse(CurntBWritten[DeviceNo]);

            byte[] Data = new byte[8];

            // form HEX volt value to be sent
            for (int i = 0; i < 4; i++)
            {
                Data[i] = VoltBWritten[DeviceNo][i];
            }

            for (int i = 4; i < 8; i++)
            {
                Data[i] = CurntBWritten[DeviceNo][i - 4];
            }

            if (!DataFromCAN.SendData(CanDevices[DeviceNo], 0x029B3FF0, Data))
                MessageBox.Show("Помилка при передачі даних",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
        }



        private void GetAmbientDeviceTemp(short DeviceNo)
        {
            DataFromCAN.SendData(CanDevices[DeviceNo], 0x028401F0, new byte[8]);
            try
            {
                AmbientTemp[DeviceNo] = new byte[4];
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevices[DeviceNo]);
                AmbientTemp[DeviceNo][0] = msgs[0].Data[4];
                Array.Reverse(AmbientTemp[DeviceNo]);
                TxtsAmbTemperature[DeviceNo].Text = NumRepresentations.BYTEtoINT(AmbientTemp[DeviceNo]).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Помилка при передачі даних",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void GetCurrentVoltage(short DeviceNo)
        {
            DataFromCAN.SendData(CanDevices[DeviceNo], 0x028601F0, new byte[8]);
            try
            {
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevices[DeviceNo]);
                VoltAB[DeviceNo] = new byte[4];
                VoltBC[DeviceNo] = new byte[4];
                VoltCA[DeviceNo] = new byte[4];


                for (int i = 0; i < 2; i++)
                {
                    VoltAB[DeviceNo][i] = msgs[0].Data[i];
                }

                for (int i = 2; i < 4; i++)
                {
                    VoltBC[DeviceNo][i - 2] = msgs[0].Data[i];
                }

                for (int i = 4; i < 6; i++)
                {
                    VoltCA[DeviceNo][i - 4] = msgs[0].Data[i];
                }

                // handle endianness
                Array.Reverse(VoltAB[DeviceNo]);
                Array.Reverse(VoltBC[DeviceNo]);
                Array.Reverse(VoltCA[DeviceNo]);
            }
            catch (Exception)
            {
                MessageBox.Show("Помилка при передачі даних",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void UpdateChargeParams(short DeviceNo)
        {

            VoltBRead[DeviceNo] = new byte[4];
            CurntBRead[DeviceNo] = new byte[4];

            SendChargeParams(DeviceNo);
            try
            {
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevices[DeviceNo]);

                // form volt value to be recieved
                for (int i = 0; i < 4; i++)
                {
                    VoltBRead[DeviceNo][i] = msgs[0].Data[i];
                }

                for (int i = 4; i < 8; i++)
                {
                    CurntBRead[DeviceNo][i - 4] = msgs[0].Data[i];
                }

                // handle endianness
                Array.Reverse(VoltBRead[DeviceNo]);
                Array.Reverse(CurntBRead[DeviceNo]);

                TxtsVoltFP[DeviceNo].Text = NumRepresentations.BYTEtoUINT(VoltBRead[DeviceNo]) + " мВ";
                TxtsCurntFP[DeviceNo].Text = NumRepresentations.BYTEtoUINT(CurntBRead[DeviceNo]) + " мА";
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
            if (BtnsDevice[0].Text == "Відключити")
            {
                SendChargeParams(0);
                try
                {
                    VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevices[0]);
                    VoltBRead[0] = new byte[4];

                    // form volt value to be recieved
                    for (int i = 0; i < 4; i++)
                    {
                        VoltBRead[0][i] = msgs[0].Data[i];
                    }

                    // handle endianness
                    Array.Reverse(VoltBRead[0]);
                    TxtsVoltFP[0].Text = NumRepresentations.BYTEtoUINT(VoltBRead[0]) + " мВ";
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
            if (BtnsDevice[0].Text == "Відключити")
            {
                SendChargeParams(0);
                try
                {
                    VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevices[0]);
                    CurntBRead[0] = new byte[4];

                    // form current value to be recieved
                    for (int i = 4; i < 8; i++)
                    {
                        CurntBRead[0][i - 4] = msgs[0].Data[i];
                    }

                    // handle endianness
                    Array.Reverse(CurntBRead[0]);
                    TxtsCurntFP[0].Text = NumRepresentations.BYTEtoUINT(CurntBRead[0]) + " мА";
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
            if (BtnsDevice[1].Text == "Відключити")
            {
                SendChargeParams(1);
                try
                {
                    VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevices[1]);
                    VoltBRead[1] = new byte[4];

                    // form volt value to be recieved
                    for (int i = 0; i < 4; i++)
                    {
                        VoltBRead[1][i] = msgs[0].Data[i];
                    }

                    // handle endianness
                    Array.Reverse(VoltBRead[1]);
                    TxtsVoltFP[1].Text = NumRepresentations.BYTEtoUINT(VoltBRead[1]) + " мВ";
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
            if (BtnsDevice[1].Text == "Відключити")
            {
                SendChargeParams(1);
                try
                {
                    VSCAN_MSG[] msgs = DataFromCAN.GetData(CanDevices[1]);
                    CurntBRead[1] = new byte[4];

                    // form current value to be recieved
                    for (int i = 4; i < 8; i++)
                    {
                        CurntBRead[1][i - 4] = msgs[0].Data[i];
                    }

                    // handle endianness
                    Array.Reverse(CurntBRead[1]);
                    TxtsCurntFP[1].Text = NumRepresentations.BYTEtoUINT(CurntBRead[1]) + " мА";
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

        private void TmrDeviceParams_Tick(object sender, EventArgs e)
        {
            if (BtnsDevice[0].Text == "Відключити")
            {
                GetAmbientDeviceTemp(0);
                GetCurrentVoltage(0);
                TxtsVoltAB[0].Text = NumRepresentations.BYTEtoFP(VoltAB[0]).ToString();
                TxtsVoltBC[0].Text = NumRepresentations.BYTEtoFP(VoltBC[0]).ToString();
                TxtsVoltCA[0].Text = NumRepresentations.BYTEtoFP(VoltCA[0]).ToString();
            }
        }

        private void CmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = cmbDevices.SelectedItem.ToString();
            switch (selectedState)
            {
                case "Активний перший модуль":
                    GrpsModules[0].Enabled = true;
                    GrpsModules[1].Enabled = false;
                    BtnsDevice[1].Text = "Підключити";
                    break;
                case "Активний другий модуль":
                    GrpsModules[0].Enabled = false;
                    GrpsModules[1].Enabled = true;
                    BtnsDevice[0].Text = "Підключити";
                    break;
                case "Активні обидва модулі":
                    GrpsModules[0].Enabled = true;
                    GrpsModules[1].Enabled = true;
                    BtnsDevice[0].Text = "Підключити";
                    BtnsDevice[1].Text = "Підключити";
                    break;
                
            }
        }
    }
}
