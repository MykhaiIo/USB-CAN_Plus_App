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
using System.IO.Ports;

namespace USB_CAN_Plus_Ctrl
{
    public partial class USB_CAN_Plus_Ctrl : Form
    {
        private const int DevicesCnt = 2;
        private const int AdaptersCnt = 1;

        private enum DevicesStates
        {
            NoActive, ActiveFirst, ActiveSecond, ActiveBoth
        }
        private DevicesStates State { get; set; }

        public byte[][] CurntBWritten { get; private set; } = new byte[DevicesCnt][];
        public byte[][] VoltBWritten { get; private set; } = new byte[DevicesCnt][];
        public byte[][] VoltBRead { get; private set; } = new byte[DevicesCnt][];
        public byte[][] CurntBRead { get; private set; } = new byte[DevicesCnt][];
        public byte[][] AmbientTemp { get; private set; } = new byte[DevicesCnt][];
        public byte[][] VoltAB { get; private set; } = new byte[DevicesCnt][];
        public byte[][] VoltBC { get; private set; } = new byte[DevicesCnt][];
        public byte[][] VoltCA { get; private set; } = new byte[DevicesCnt][];
        public byte[][] Voltage { get; private set; } = new byte[DevicesCnt][];

        public GroupBox[] GrpsModules { get; private set; }
        public Button[] BtnsAdapter { get; private set; }
        public NumericUpDown[] NudsVoltSI { get; private set; }
        public NumericUpDown[] NudsCurntSI { get; private set; }
        public TextBox[] TxtsVoltINT { get; private set; }
        public TextBox[] TxtsCurntINT { get; private set; }
        public TextBox[] TxtsAmbTemperature { get; private set; }
        public TextBox[] TxtsVoltage { get; private set; }
        public TextBox[] TxtsVoltAB { get; private set; }
        public TextBox[] TxtsVoltBC { get; private set; }
        public TextBox[] TxtsVoltCA { get; private set; }
        public Label[] LblsSerialNo { get; private set; }

        internal VSCAN[] CanAdapters { get; set; } = new VSCAN[AdaptersCnt];
        // internal string[] Ports { get; set; }

        public USB_CAN_Plus_Ctrl()
        {
            InitializeComponent();
            grpModule1.Enabled = false;
            grpModule2.Enabled = false;
            btnConnect1.Enabled = false;

            GrpsModules = new GroupBox[] { grpModule1, grpModule2 };
            BtnsAdapter = new Button[] { btnConnect1, /*btnConnect2*/ };
            NudsVoltSI = new NumericUpDown[] { nudOutVoltSI1, nudOutVoltSI2 };
            NudsCurntSI = new NumericUpDown[] { nudOutCurntSI1, nudOutCurntSI2 };
            TxtsVoltINT = new TextBox[] { txtOutVoltINT1, txtOutVoltINT2 };
            TxtsCurntINT = new TextBox[] { txtOutCurntINT1, txtOutCurntINT2 };
            TxtsAmbTemperature = new TextBox[] { txtTemperature1, txtTemperature2 };
            TxtsVoltage = new TextBox[] { txtCurVolt1, txtCurVolt2 };
            TxtsVoltAB = new TextBox[] { txtPhaseABVolt1, txtPhaseABVolt2 };
            TxtsVoltBC = new TextBox[] { txtPhaseBCVolt1, txtPhaseBCVolt2 };
            TxtsVoltCA = new TextBox[] { txtPhaseCAVolt1, txtPhaseCAVolt2 };
            LblsSerialNo = new Label[] { lblSerialNo1, /*lblSerialNo2*/ };
        }

        private void DisplayDeviceParams(/*short DeviceNo*/)
        {
            VSCAN_HWPARAM hw = new VSCAN_HWPARAM();
            CanAdapters[0].GetHwParams(ref hw);

            // get HW Params
            LblsSerialNo[0].Text = $"Серійний номер: {hw.SerialNr}";
        }

        private void HandleDeviceBtn(/*short DeviceNo*/)
        {
            if (BtnsAdapter[0].Text == "Підключити")
            {
                try
                {
                    CanAdapters[0] = DataFromCAN.InitCAN();
                    DisplayDeviceParams();
                    if (State == DevicesStates.ActiveFirst)
                    {
                        UpdateChargeParams(0);
                    }
                    else if (State == DevicesStates.ActiveSecond)
                    {
                        UpdateChargeParams(1);
                    }
                    else if (State == DevicesStates.ActiveBoth)
                    {
                        UpdateChargeParams(0);
                        UpdateChargeParams(1);
                    }
                    BtnsAdapter[0].Text = "Відключити";
                }
                catch (Exception)
                {
                    MessageBox.Show($"Не вдалося пдключити адаптер USB-CAN Plus",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }               
            }
            else if (BtnsAdapter[0].Text == "Відключити")
            {
                DataFromCAN.DeinitCAN(CanAdapters[0]);
                CanAdapters[0] = null;
                LblsSerialNo[0].Text = "Серійний номер:";
                BtnsAdapter[0].Text = "Підключити";
                foreach (var nud in NudsVoltSI)
                {
                    nud.Value = nud.Minimum;
                }
                foreach(var nud in NudsCurntSI)
                {
                    nud.Value = nud.Minimum;
                }

                foreach(var txt in TxtsVoltINT)
                {
                    txt.Text = "";
                }
                foreach(var txt in TxtsCurntINT)
                {
                    txt.Text = "";
                }
            }
        }

        private void BtnConnect1_Click(object sender, EventArgs e) => HandleDeviceBtn();

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

        private void GetAmbientDeviceTemp(short DeviceNo)
        {
            if (State == DevicesStates.ActiveFirst)
            {
                DataFromCAN.SendData(CanAdapters[0], 0x028401F0, new byte[8]);
            }
            else if (State == DevicesStates.ActiveSecond)
            {
                DataFromCAN.SendData(CanAdapters[0], 0x028402F0, new byte[8]);
            }
            else if (State == DevicesStates.ActiveBoth)
            {
                DataFromCAN.SendData(CanAdapters[0], 0x028401F0, new byte[8]);
                DataFromCAN.SendData(CanAdapters[0], 0x028402F0, new byte[8]);
            }

            try
            {
                AmbientTemp[DeviceNo] = new byte[4];
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanAdapters[0]);
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

        private void GetVoltage(short DeviceNo)
        {
            if (State == DevicesStates.ActiveFirst)
            {
                DataFromCAN.SendData(CanAdapters[0], 0x02C101F0, new byte[8]);
            }
            else if (State == DevicesStates.ActiveSecond)
            {
                DataFromCAN.SendData(CanAdapters[0], 0x02C102F0, new byte[8]);
            }
            else if (State == DevicesStates.ActiveBoth)
            {
                DataFromCAN.SendData(CanAdapters[0], 0x02813FF0, new byte[8]);
            }

            try
            {
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanAdapters[0]);
                Voltage[DeviceNo] = new byte[4];

                for (int i = 0; i < 4; i++)
                {
                    Voltage[DeviceNo][i] = msgs[0].Data[i];
                }

                // handle endianness
                Array.Reverse(Voltage[DeviceNo]);
            }
            catch (Exception)
            {
                MessageBox.Show("Помилка при передачі даних",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void GetPhaseVoltage(short DeviceNo)
        {
            if (State == DevicesStates.ActiveFirst)
            {
                DataFromCAN.SendData(CanAdapters[0], 0x028601F0, new byte[8]);
            }
            else if (State == DevicesStates.ActiveSecond)
            {
                DataFromCAN.SendData(CanAdapters[0], 0x028602F0, new byte[8]);
            }
            else if (State == DevicesStates.ActiveBoth)
            {               
                DataFromCAN.SendData(CanAdapters[0], 0x02863FF0, new byte[8]);
            }

            try
            {
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanAdapters[0]);
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
            UpdateVoltValue(DeviceNo);
            UpdateCurntValue(DeviceNo);
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

            if (State == DevicesStates.ActiveFirst)
            {
                DataFromCAN.SendData(CanAdapters[0], 0x02DB01F0, Data);
            }
            else if (State == DevicesStates.ActiveSecond)
            {
                DataFromCAN.SendData(CanAdapters[0], 0x02DB02F0, Data);
            }
            else if (State == DevicesStates.ActiveBoth)
            {
                DataFromCAN.SendData(CanAdapters[0], 0x029B3FF0, Data);
            }
        }

        private void UpdateVoltValue(short DeviceNo)
        {
            SendChargeParams(DeviceNo);
            try
            {
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanAdapters[0]);
                VoltBRead[DeviceNo] = new byte[4];

                // form volt value to be recieved
                for (int i = 0; i < 4; i++)
                {
                    VoltBRead[DeviceNo][i] = msgs[0].Data[i];
                }

                // handle endianness
                Array.Reverse(VoltBRead[DeviceNo]);
                TxtsVoltINT[DeviceNo].Text = $"{NumRepresentations.BYTEtoUINT(VoltBRead[DeviceNo])} мВ";
            }
            catch (Exception)
            {
                MessageBox.Show("Помилка при передачі даних",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void UpdateCurntValue(short DeviceNo)
        {
            SendChargeParams(DeviceNo);
            try
            {
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanAdapters[0]);
                CurntBRead[DeviceNo] = new byte[4];

                // form current value to be recieved
                for (int i = 4; i < 8; i++)
                {
                    CurntBRead[DeviceNo][i - 4] = msgs[0].Data[i];
                }

                // handle endianness
                Array.Reverse(CurntBRead[DeviceNo]);
                TxtsCurntINT[DeviceNo].Text = $"{NumRepresentations.BYTEtoUINT(CurntBRead[DeviceNo])} мА";
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
            if (BtnsAdapter[0].Text == "Відключити")
                UpdateVoltValue(0);
        }

        private void NudOutCurntSI1_ValueChanged(object sender, EventArgs e)
        {
            if (BtnsAdapter[0].Text == "Відключити")
                UpdateCurntValue(0);
        }

        private void NudOutVoltSI2_ValueChanged(object sender, EventArgs e)
        {
            if (BtnsAdapter[0].Text == "Відключити")
                UpdateVoltValue(1);
        }

        private void NudOutCurntSI2_ValueChanged(object sender, EventArgs e)
        {
            if (BtnsAdapter[0].Text == "Відключити")
                UpdateCurntValue(1);
        }

        private void GetDeviceParams(short DeviceNo)
        {
            if (BtnsAdapter[0].Text == "Відключити" && GrpsModules[DeviceNo].Enabled)
            {
                GetAmbientDeviceTemp(DeviceNo);
                GetPhaseVoltage(DeviceNo);
                GetVoltage(DeviceNo);
                TxtsVoltage[DeviceNo].Text = NumRepresentations.BYTEtoFP(Voltage[DeviceNo]).ToString();
                TxtsVoltAB[DeviceNo].Text = NumRepresentations.BYTEtoFP(VoltAB[DeviceNo]).ToString();
                TxtsVoltBC[DeviceNo].Text = NumRepresentations.BYTEtoFP(VoltBC[DeviceNo]).ToString();
                TxtsVoltCA[DeviceNo].Text = NumRepresentations.BYTEtoFP(VoltCA[DeviceNo]).ToString();
            }
        }

        private void TmrDeviceParams_Tick(object sender, EventArgs e)
        {
            GetDeviceParams(0);
            GetDeviceParams(1);
        }

        private void CmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbDevices.SelectedItem.ToString())
            {
                case "Активний перший модуль":
                    GrpsModules[0].Enabled = true;
                    GrpsModules[1].Enabled = false;
                    btnConnect1.Enabled = true;
                    NudsVoltSI[1].Value = NudsVoltSI[1].Minimum;
                    NudsCurntSI[1].Value = NudsCurntSI[1].Minimum;
                    TxtsVoltINT[1].Text = "";
                    TxtsCurntINT[1].Text = "";
                    State = DevicesStates.ActiveFirst;
                    break;

                case "Активний другий модуль":
                    GrpsModules[0].Enabled = false;
                    GrpsModules[1].Enabled = true;
                    btnConnect1.Enabled = true;
                    LblsSerialNo[0].Text = "Серійний номер:";
                    NudsVoltSI[0].Value = NudsVoltSI[0].Minimum;
                    NudsCurntSI[0].Value = NudsCurntSI[0].Minimum;
                    TxtsVoltINT[0].Text = "";
                    TxtsCurntINT[0].Text = "";
                    State = DevicesStates.ActiveSecond;
                    break;

                case "Активні обидва модулі":
                    GrpsModules[0].Enabled = true;
                    GrpsModules[1].Enabled = true;
                    btnConnect1.Enabled = true;
                    State = DevicesStates.ActiveBoth;
                    break;

                default:
                    State = DevicesStates.NoActive;
                    break;
            }
        }
    }
}
