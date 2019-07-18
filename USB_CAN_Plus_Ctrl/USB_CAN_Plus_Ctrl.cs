using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using VSCom.CanApi;

namespace USB_CAN_Plus_Ctrl
{
    public partial class USB_CAN_Plus_Ctrl : Form
    {

        // ------------------------------------------- //
        // Volt   == voltage(V)                        //
        // Curnt  == current(I)                        //
        // Cur == current(actual)                      //
        // Devices == charger modules                  //
        // Adapters == USB-CAN Plus by VSCOm devices   //
        // ------------------------------------------- //


        private const int DevicesCnt = 2;
        private const int AdaptersCnt = 1;

        private enum DevicesStates
        {
            NoActive, ActiveFirst, ActiveSecond, ActiveBoth
        }

        private enum MessagesIDs
        {
            TxTempDevice1 = 0x028401F0,                 RxTempDevice1 = 0x0284F001,     
            TxTempDevice2 = 0x028402F0,                 RxTempDevice2 = 0x0284F002,
            TxGetParamsDevice1 = 0x02C101F0,            RxGetParamsDevice1 = 0x02C1F001,
            TxGetParamsDevice2 = 0x02C102F0,            RxGetParamsDevice2 = 0x02C1F002,
            TxGetParamsBoth = 0x02813FF0,               RxGetParamsBoth = 0x0281F03F,
            TxPhaseVoltDevice1 = 0x028601F0,            RxPhaseVoltDevice1 = 0x0286F001,
            TxPhaseVoltDevice2 = 0x028602F0,            RxPhaseVoltDevice2 = 0x0286F002,
            TxPhaseVoltBoth = 0x02863FF0,               RxPhaseVoltBoth = 0x0286F03F,
            TxSetParamsDevice1 = 0x02DB01F0,            RxSetParamsDevice1 = 0x02DBF001,
            TxSetParamsDevice2 = 0x02DB02F0,            RxSetParamsDevice2 = 0x02DBF002,
            TxSetParamsBoth = 0x029B3FF0,               RxSetParamsBoth = 0x029F03F
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
        public TextBox[] TxtsVoltInt { get; private set; }
        public TextBox[] TxtsCurntInt { get; private set; }
        public TextBox[] TxtsAmbTemperature { get; private set; }
        public TextBox[] TxtsVoltage { get; private set; }
        public TextBox[] TxtsVoltAB { get; private set; }
        public TextBox[] TxtsVoltBC { get; private set; }
        public TextBox[] TxtsVoltCA { get; private set; }
        public Label[] LblsSerialNo { get; private set; }

        internal VSCAN[] CanAdapters { get; set; } = new VSCAN[AdaptersCnt];

        private readonly BackgroundWorker bgw = new BackgroundWorker();

        public USB_CAN_Plus_Ctrl()
        {
            InitializeComponent();
            grpModule1.Enabled = false;
            grpModule2.Enabled = false;
            btnConnect1.Enabled = false;
            metroProgressBar1.Visible = false;
            progressBar1.Visible = false;
            bgw.DoWork += BgwOnDoWork;
            bgw.RunWorkerCompleted += BgwOnRunWorkerCompleted;

            GrpsModules = new[] { grpModule1, grpModule2 };
            BtnsAdapter = new[] { btnConnect1 /*btnConnect2*/ };
            NudsVoltSI = new[] { nudOutVoltSI1, nudOutVoltSI2 };
            NudsCurntSI = new[] { nudOutCurntSI1, nudOutCurntSI2 };
            TxtsVoltInt = new[] { txtOutVoltINT1, txtOutVoltINT2 };
            TxtsCurntInt = new[] { txtOutCurntINT1, txtOutCurntINT2 };
            TxtsAmbTemperature = new[] { txtTemperature1, txtTemperature2 };
            TxtsVoltage = new[] { txtCurVolt1, txtCurVolt2 };
            TxtsVoltAB = new[] { txtPhaseABVolt1, txtPhaseABVolt2 };
            TxtsVoltBC = new[] { txtPhaseBCVolt1, txtPhaseBCVolt2 };
            TxtsVoltCA = new[] { txtPhaseCAVolt1, txtPhaseCAVolt2 };
            LblsSerialNo = new[] { lblSerialNo1 /*lblSerialNo2*/ };
        }

        private void BgwOnDoWork(object sender, DoWorkEventArgs e)
        {
            HandleAdapterBtn();
        }

        private void BgwOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            metroProgressBar1.Hide();
        }

        private void DisplayAdapterParams(/*short AdapterNo*/)
        {
            VSCAN_HWPARAM hw = new VSCAN_HWPARAM();
            CanAdapters[0].GetHwParams(ref hw);

            // display adapter's serial number
            //textBox.Invoke(new Action(() => textBox.Text = epoch.ToString()));
            LblsSerialNo[0].Invoke(new Action(() => LblsSerialNo[0].Text = $"Серійний номер: {hw.SerialNr}"));
        }

        private void HandleAdapterBtn(/*short AdapterNo*/)
        {
            switch (BtnsAdapter[0].Text)
            {
                case "Підключити":
                    {
                        try
                        {
                            CanAdapters[0] = DataFromCAN.InitCAN();
                            DisplayAdapterParams();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Не вдалося підключити адаптер USB-CAN Plus",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }

                        try
                        {
                            switch (State)
                            {
                                case DevicesStates.ActiveFirst:
                                    UpdateChargeParams(0);
                                    break;
                                case DevicesStates.ActiveSecond:
                                    UpdateChargeParams(1);
                                    break;
                                case DevicesStates.ActiveBoth:
                                    UpdateChargeParams(0);
                                    UpdateChargeParams(1);
                                    break;
                            }

                            BtnsAdapter[0].Invoke(new Action(() => BtnsAdapter[0].Text = "Відключити"));
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Не вдалося надіслати параметри зарядки",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    break;

                case "Відключити":
                {
                    DataFromCAN.DeinitCAN(CanAdapters[0]);
                    CanAdapters[0] = null;
                    LblsSerialNo[0].Invoke(new Action(() => LblsSerialNo[0].Text = "Серійний номер:"));
                    BtnsAdapter[0].Invoke(new Action(() => BtnsAdapter[0].Text = "Підключити"));

                    foreach (var nud in NudsVoltSI)
                        nud.Invoke(new Action(() => nud.Value = nud.Minimum));

                    foreach (var nud in NudsCurntSI)
                        nud.Invoke(new Action(() => nud.Value = nud.Minimum));

                    foreach (var txt in TxtsVoltInt)
                        txt.Invoke(new Action(() => txt.Text = ""));

                    foreach (var txt in TxtsCurntInt)
                        txt.Invoke(new Action(() =>txt.Text = ""));
                    break;
                }
            }
        }

        private void BtnConnect1_Click(object sender, EventArgs e)
        {
            metroProgressBar1.Show();
            bgw.RunWorkerAsync();
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
                strID.Append(str);

            strID.Replace(" ", "");

            return 
                UInt32.TryParse(strID.ToString(), NumberStyles.AllowHexSpecifier, null, out UInt32 ID) ? 
                    ID : 0;
        }

        private char GetErrorCodeFromID(UInt32 ID)
        {
            string strID = ID.ToString();
            char strEC = strID[0];
            return strEC;
        }

        private void GetAmbientDeviceTemp(short DeviceNo)
        {
            //byte[] data = {0x00, 0x00, 0x02, 0x00, 0x1B, 0x00, 0x40, 0x00};
            switch (State)
            {
                case DevicesStates.ActiveFirst:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxTempDevice1, new byte[8]);
                    break;
                case DevicesStates.ActiveSecond:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxTempDevice2, new byte[8]);
                    break;
                case DevicesStates.ActiveBoth:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxTempDevice1, new byte[8]);
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxTempDevice2, new byte[8]);
                    break;
                case DevicesStates.NoActive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            try
            {
                AmbientTemp[DeviceNo] = new byte[4];
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanAdapters[0]);
                if ((msgs[0].Id != (UInt32) MessagesIDs.RxTempDevice1 || State != DevicesStates.ActiveFirst) &&
                    (msgs[0].Id != (UInt32) MessagesIDs.RxTempDevice2 || State != DevicesStates.ActiveSecond)) return;
                AmbientTemp[DeviceNo][0] = msgs[0].Data[4];
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
            switch (State)
            {
                case DevicesStates.ActiveFirst:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxGetParamsDevice1, new byte[8]);
                    break;
                case DevicesStates.ActiveSecond:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxGetParamsDevice2, new byte[8]);
                    break;
                case DevicesStates.ActiveBoth:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxGetParamsBoth,    new byte[8]);
                    break;
                case DevicesStates.NoActive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            try
            {
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanAdapters[0]);

                if ((msgs[0].Id != (UInt32)MessagesIDs.RxGetParamsDevice1 || State != DevicesStates.ActiveFirst) &&
                    (msgs[0].Id != (UInt32)MessagesIDs.RxGetParamsDevice2 || State != DevicesStates.ActiveSecond) &&
                    (msgs[0].Id != (UInt32)MessagesIDs.RxGetParamsBoth    || State != DevicesStates.ActiveBoth)) return;

                Voltage[DeviceNo] = new byte[4];

                for (int i = 0; i < 4; i++)
                {
                    Voltage[DeviceNo][i] = msgs[0].Data[i];
                }
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
            switch (State)
            {
                case DevicesStates.ActiveFirst:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxPhaseVoltDevice1, new byte[8]);
                    break;
                case DevicesStates.ActiveSecond:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxPhaseVoltDevice2, new byte[8]);
                    break;
                case DevicesStates.ActiveBoth:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxPhaseVoltBoth,    new byte[8]);
                    break;
                case DevicesStates.NoActive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            try
            {
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanAdapters[0]);

                if ((msgs[0].Id != (UInt32)MessagesIDs.RxPhaseVoltDevice1 || State != DevicesStates.ActiveFirst) &&
                    (msgs[0].Id != (UInt32)MessagesIDs.RxPhaseVoltDevice2 || State != DevicesStates.ActiveSecond) &&
                    (msgs[0].Id != (UInt32)MessagesIDs.RxPhaseVoltBoth    || State != DevicesStates.ActiveBoth)) return;

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
            VoltBWritten[DeviceNo]  = NumRepresentations.UINTtoBYTE((uint)NudsVoltSI [DeviceNo].Value * 1000);
            CurntBWritten[DeviceNo] = NumRepresentations.UINTtoBYTE((uint)NudsCurntSI[DeviceNo].Value * 1000);

            byte[] Data = new byte[8];

            // form BYTE volt value to be sent
            for (int i = 0; i < 4; i++)
            {
                Data[i] = VoltBWritten[DeviceNo][i];
            }
            for (int i = 4; i < 8; i++)
            {
                Data[i] = CurntBWritten[DeviceNo][i - 4];
            }

            switch (State)
            {
                case DevicesStates.ActiveFirst:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxSetParamsDevice1, Data);
                    break;
                case DevicesStates.ActiveSecond:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxSetParamsDevice2, Data);
                    break;
                case DevicesStates.ActiveBoth:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxSetParamsBoth,    Data);
                    break;
                case DevicesStates.NoActive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateVoltValue(short DeviceNo)
        {
            SendChargeParams(DeviceNo);
            try
            {
                VSCAN_MSG[] msgs = DataFromCAN.GetData(CanAdapters[0]);

                if ((msgs[0].Id != (UInt32)MessagesIDs.RxSetParamsDevice1 || State != DevicesStates.ActiveFirst) &&
                    (msgs[0].Id != (UInt32)MessagesIDs.RxSetParamsDevice2 || State != DevicesStates.ActiveSecond) &&
                    (msgs[0].Id != (UInt32)MessagesIDs.RxSetParamsBoth    || State != DevicesStates.ActiveBoth)) return;

                VoltBRead[DeviceNo] = new byte[4];

                // form volt value to be recieved
                for (int i = 0; i < 4; i++)
                {
                    VoltBRead[DeviceNo][i] = msgs[0].Data[i];
                }

                TxtsVoltInt[DeviceNo].Invoke(new Action(() => 
                TxtsVoltInt[DeviceNo].Text = $"{NumRepresentations.BYTEtoUINT(VoltBRead[DeviceNo])} мВ"));
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

                if ((msgs[0].Id != (UInt32)MessagesIDs.RxSetParamsDevice1 || State != DevicesStates.ActiveFirst) &&
                    (msgs[0].Id != (UInt32)MessagesIDs.RxSetParamsDevice2 || State != DevicesStates.ActiveSecond) &&
                    (msgs[0].Id != (UInt32)MessagesIDs.RxSetParamsBoth    || State != DevicesStates.ActiveBoth)) return;

                CurntBRead[DeviceNo] = new byte[4];

                // form current value to be received
                for (int i = 4; i < 8; i++)
                {
                    CurntBRead[DeviceNo][i - 4] = msgs[0].Data[i];
                }

                TxtsCurntInt[DeviceNo].Invoke(new Action(() =>
                TxtsCurntInt[DeviceNo].Text = $"{NumRepresentations.BYTEtoUINT(CurntBRead[DeviceNo])} мА"));
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
            if (BtnsAdapter[0].Text != "Відключити") return;
            UpdateVoltValue(0);
        }

        private void NudOutCurntSI1_ValueChanged(object sender, EventArgs e)
        {
            if (BtnsAdapter[0].Text != "Відключити") return;
            UpdateCurntValue(0);
        }

        private void NudOutVoltSI2_ValueChanged(object sender, EventArgs e)
        {
            if (BtnsAdapter[0].Text != "Відключити") return;
            UpdateVoltValue(1);
        }

        private void NudOutCurntSI2_ValueChanged(object sender, EventArgs e)
        {
            if (BtnsAdapter[0].Text != "Відключити") return;
            UpdateCurntValue(1);
        }

        private void GetDeviceParams(short DeviceNo)
        {
            if (BtnsAdapter[0].Text != "Відключити" || !GrpsModules[DeviceNo].Enabled) return;
            GetAmbientDeviceTemp(DeviceNo);
            GetPhaseVoltage(DeviceNo);
            GetVoltage(DeviceNo);
            if (Voltage[DeviceNo] == null || VoltAB[DeviceNo] == null || VoltBC[DeviceNo] == null || VoltCA[DeviceNo] == null) return;
            TxtsVoltage[DeviceNo].Text = NumRepresentations.BYTEtoFP(Voltage[DeviceNo]).ToString();
            TxtsVoltAB[DeviceNo].Text =  NumRepresentations.BYTEtoFP(VoltAB [DeviceNo]).ToString();
            TxtsVoltBC[DeviceNo].Text =  NumRepresentations.BYTEtoFP(VoltBC [DeviceNo]).ToString();
            TxtsVoltCA[DeviceNo].Text =  NumRepresentations.BYTEtoFP(VoltCA [DeviceNo]).ToString();
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
                    NudsVoltSI[1].Value =  NudsVoltSI [1].Minimum;
                    NudsCurntSI[1].Value = NudsCurntSI[1].Minimum;
                    TxtsVoltInt[1].Text = "";
                    TxtsCurntInt[1].Text = "";
                    State = DevicesStates.ActiveFirst;
                    break;

                case "Активний другий модуль":
                    GrpsModules[0].Enabled = false;
                    GrpsModules[1].Enabled = true;
                    btnConnect1.Enabled = true;
                    NudsVoltSI[0].Value =  NudsVoltSI [0].Minimum;
                    NudsCurntSI[0].Value = NudsCurntSI[0].Minimum;
                    TxtsVoltInt[0].Text = "";
                    TxtsCurntInt[0].Text = "";
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
