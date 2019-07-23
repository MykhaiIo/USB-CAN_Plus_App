using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using USB_CAN_Plus_Ctrl.Properties;
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
                                                                                            // CommandNo
            TxGetParamsDevice1 = 0x02C101F0,            RxGetParamsDevice1 = 0x02C1F001,    //    0x01
            TxGetParamsDevice2 = 0x02C102F0,            RxGetParamsDevice2 = 0x02C1F002,    //    0x01
            TxGetParamsBoth = 0x02813FF0,               RxGetParamsBoth = 0x0281F03F,       //    0x01
                                                                                                  
            TxTempDevice1 = 0x028401F0,                 RxTempDevice1 = 0x0284F001,         //    0x04
            TxTempDevice2 = 0x028402F0,                 RxTempDevice2 = 0x0284F002,         //    0x04
                                                                                                  
            TxPhaseVoltDevice1 = 0x028601F0,            RxPhaseVoltDevice1 = 0x0286F001,    //    0x06
            TxPhaseVoltDevice2 = 0x028602F0,            RxPhaseVoltDevice2 = 0x0286F002,    //    0x06
            TxPhaseVoltBoth = 0x02863FF0,               RxPhaseVoltBoth = 0x0286F03F,       //    0x06
                                                                                                  
            TxPowerDevice1 = 0x02DA01F0,                                                    //    0x1A
            TxPowerDevice2 = 0x02DA02F0,                                                    //    0x1A
            TxPowerBoth = 0x029A3FF0,                                                       //    0x1A
                                                                                                  
            TxSetParamsDevice1 = 0x02DB01F0,                                                //    0x1B
            TxSetParamsDevice2 = 0x02DB02F0,                                                //    0x1B
            TxSetParamsBoth = 0x029B3FF0,                                                   //    0x1B
        }

        private DevicesStates State { get; set; }

        // properties storing read or written devices' params
        public byte[][] CurntWritten { get; } = new byte[DevicesCnt][];
        public byte[][] VoltWritten { get; } = new byte[DevicesCnt][];
        public byte[][] VoltRead { get; } = new byte[DevicesCnt][];
        public byte[][] CurntRead { get; } = new byte[DevicesCnt][];
        public byte[][] AmbientTemp { get; } = new byte[DevicesCnt][];
        public byte[][] VoltAB { get; } = new byte[DevicesCnt][];
        public byte[][] VoltBC { get; } = new byte[DevicesCnt][];
        public byte[][] VoltCA { get; } = new byte[DevicesCnt][];

        // group form's controls related to both devices 
        public GroupBox[] GrpsModules { get; }
        public Button[] BtnsAdapter { get; }
        public NumericUpDown[] NudsVoltSI { get; }
        public NumericUpDown[] NudsCurntSI { get; }
        public TextBox[] TxtsAmbTemperature { get; }
        public TextBox[] TxtsVoltage { get; }
        public TextBox[] TxtsCurrent { get; }
        public TextBox[] TxtsVoltAB { get; }
        public TextBox[] TxtsVoltBC { get; }
        public TextBox[] TxtsVoltCA { get; }
        public Label[] LblsSerialNo { get; }

        // counter used to perform continuous devices' params actualization
        public ushort Cnt { get; private set; } = 0;

        // stopwatches used to make textboxes red if it's values hasn't been updated during last second
        // TODO: Convert Stopwatch Sw properties to Stopwatch[] Sw 
        public Stopwatch SwVoltRead { get; } = new Stopwatch();
        public Stopwatch SwCurntRead { get; } = new Stopwatch();
        public Stopwatch SwPhaseVoltAB { get; } = new Stopwatch();
        public Stopwatch SwPhaseVoltBC { get; } = new Stopwatch();
        public Stopwatch SwPhaseVoltCA { get; } = new Stopwatch();
        public Stopwatch SwAmbientTemp { get; } = new Stopwatch();


        // property storing all received messages from USB-CAN Plus adapter separately for both devices
        public VSCAN_MSG[][] Msgs { get; private set; } = new VSCAN_MSG[DevicesCnt][];
        internal VSCAN[] CanAdapters { get; set; } = new VSCAN[AdaptersCnt];

        private readonly BackgroundWorker _bgw = new BackgroundWorker();

        public USB_CAN_Plus_Ctrl()
        {
            InitializeComponent();
            grpModule1.Enabled = false;
            grpModule2.Enabled = false;
            btnConnect1.Enabled = false;
            metroProgressBar1.Visible = false;
            _bgw.DoWork += BgwOnDoWork;
            _bgw.RunWorkerCompleted += BgwOnRunWorkerCompleted;

            GrpsModules = new[] { grpModule1, grpModule2 };
            BtnsAdapter = new[] { btnConnect1 /*btnConnect2*/ };
            NudsVoltSI = new[] { nudOutVoltSI1, nudOutVoltSI2 };
            NudsCurntSI = new[] { nudOutCurntSI1, nudOutCurntSI2 };
            TxtsAmbTemperature = new[] { txtTemperature1, txtTemperature2 };
            TxtsVoltage = new[] { txtCurVolt1, txtCurVolt2 };
            TxtsCurrent = new[] { txtCurCurnt1, txtCurCurnt2 };
            TxtsVoltAB = new[] { txtPhaseABVolt1, txtPhaseABVolt2 };
            TxtsVoltBC = new[] { txtPhaseBCVolt1, txtPhaseBCVolt2 };
            TxtsVoltCA = new[] { txtPhaseCAVolt1, txtPhaseCAVolt2 };
            LblsSerialNo = new[] { lblSerialNo1 /*lblSerialNo2*/ };
        }

        private void BgwOnDoWork(object sender, DoWorkEventArgs e) => HandleAdapterBtnClick();

        private void BgwOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => metroProgressBar1.Hide();

        private void DisplayAdapterParams(/*short AdapterNo*/)
        {
            VSCAN_HWPARAM hw = new VSCAN_HWPARAM();
            CanAdapters[0].GetHwParams(ref hw);

            // display adapter's serial number
            LblsSerialNo[0].Invoke(new Action(() => 
                LblsSerialNo[0].Text = string.Format(Resources.SerialNmbActive, hw.SerialNr)
                )
            );
        }

        private void HandleAdapterBtnClick(/*short AdapterNo*/)
        {
            switch (BtnsAdapter[0].Text)
            {
                case "Підключити":
                {
                    try
                    {
                        CanAdapters[0] = DataFromCAN.InitCAN();
                        Msgs[0] = DataFromCAN.GetData(CanAdapters[0]);
                        Msgs[1] = DataFromCAN.GetData(CanAdapters[0]);
                        DisplayAdapterParams();

                        BtnsAdapter[0].Invoke(new Action(() => BtnsAdapter[0].Text = Resources.Disconnect));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(Resources.AdapterConnectionError,
                            Resources.ErrorMsg,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    break;
                }
                
                case "Відключити":
                {
                    DataFromCAN.DeinitCAN(CanAdapters[0]);
                    LblsSerialNo[0].Invoke(new Action(() => LblsSerialNo[0].Text = Resources.SerialNmbInactive));
                    BtnsAdapter[0].Invoke(new Action(() => BtnsAdapter[0].Text = Resources.Connect));

                    foreach (var nud in NudsVoltSI)
                        nud.Invoke(new Action(() => nud.Value = nud.Minimum));

                    foreach (var nud in NudsCurntSI)
                        nud.Invoke(new Action(() => nud.Value = nud.Minimum));

                    foreach (var txt in TxtsVoltage)
                        txt.Invoke(new Action(() => txt.Text = ""));

                    foreach (var txt in TxtsCurrent)
                        txt.Invoke(new Action(() => txt.Text = ""));

                    foreach (var txt in TxtsAmbTemperature)
                        txt.Invoke(new Action(() => txt.Text = ""));

                    foreach (var txt in TxtsVoltAB)
                        txt.Invoke(new Action(() => txt.Text = ""));

                    foreach (var txt in TxtsVoltBC)
                        txt.Invoke(new Action(() => txt.Text = ""));

                    foreach (var txt in TxtsVoltCA)
                        txt.Invoke(new Action(() => txt.Text = ""));

                    break;
                }
            }
        }

        private void BtnConnect1_Click(object sender, EventArgs e)
        {
            metroProgressBar1.Show();
            _bgw.RunWorkerAsync();

            /*foreach (var sw in SwVoltRead)
            {
                sw.Start();
            }*/

            SwVoltRead.Start();
            SwCurntRead.Start();
            SwAmbientTemp.Start();
            SwPhaseVoltAB.Start();
            SwPhaseVoltBC.Start();
            SwPhaseVoltCA.Start();
        }

        private void DisplayDeviceParams(short deviceNo)
        {
            if (BtnsAdapter[0].Text != Resources.Disconnect || !GrpsModules[deviceNo].Enabled) return;

            TxtsVoltage[deviceNo].ForeColor = Color.Black;
            TxtsCurrent[deviceNo].ForeColor = Color.Black;
            TxtsAmbTemperature[deviceNo].ForeColor = Color.Black;
            TxtsVoltAB[deviceNo].ForeColor = Color.Black;
            TxtsVoltBC[deviceNo].ForeColor = Color.Black;
            TxtsVoltCA[deviceNo].ForeColor = Color.Black;

            if (VoltRead[deviceNo] == null || CurntRead[deviceNo] == null ||
                VoltAB[deviceNo]   == null || VoltBC[deviceNo]    == null || VoltCA[deviceNo] == null) return;

            TxtsVoltage[deviceNo].Text =
                string.Format(Resources.Vpostfix, NumRepresentations.ToFormattedFloat(NumRepresentations.BYTEtoFP(VoltRead[deviceNo])));
            TxtsCurrent[deviceNo].Text =
                string.Format(Resources.Apostfix, NumRepresentations.ToFormattedFloat(NumRepresentations.BYTEtoFP(CurntRead[deviceNo])));

            if (SwVoltRead.ElapsedMilliseconds > 2000)
                TxtsVoltage[deviceNo].ForeColor = Color.Red;
            if (SwCurntRead.ElapsedMilliseconds > 2000)
                TxtsCurrent[deviceNo].ForeColor = Color.Red;



            TxtsVoltAB[deviceNo].Text =
                string.Format(Resources.Vpostfix, NumRepresentations.BYTEtoUSHORT(VoltAB[deviceNo]) / 10);
            TxtsVoltBC[deviceNo].Text =
                string.Format(Resources.Vpostfix, NumRepresentations.BYTEtoUSHORT(VoltBC[deviceNo]) / 10);
            TxtsVoltCA[deviceNo].Text =
                string.Format(Resources.Vpostfix, NumRepresentations.BYTEtoUSHORT(VoltCA[deviceNo]) / 10);
            if (SwPhaseVoltAB.ElapsedMilliseconds > 2000)
                TxtsVoltAB[deviceNo].ForeColor = Color.Red;
            if (SwPhaseVoltBC.ElapsedMilliseconds > 2000)
                TxtsVoltBC[deviceNo].ForeColor = Color.Red;
            if (SwPhaseVoltCA.ElapsedMilliseconds > 2000)
                TxtsVoltCA[deviceNo].ForeColor = Color.Red;

            TxtsAmbTemperature[deviceNo].Text = $"{NumRepresentations.BYTEtoINT(AmbientTemp[deviceNo])} °C";
            SwAmbientTemp.Stop();
            if (SwAmbientTemp.ElapsedMilliseconds > 2000)
                TxtsAmbTemperature[deviceNo].ForeColor = Color.Red;
        }

        private void TmrDisplayDeviceParams_Tick(object sender, EventArgs e)
        {
            switch (State)
            {
                case DevicesStates.ActiveFirst:
                    DisplayDeviceParams(0);
                    break;
                case DevicesStates.ActiveSecond:
                    DisplayDeviceParams(1);
                    break;
                case DevicesStates.ActiveBoth:
                    DisplayDeviceParams(0);
                    DisplayDeviceParams(1);
                    break;
                case DevicesStates.NoActive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TmrRefreshDeviceParams_Tick(object sender, EventArgs e)
        {
            switch (State)
            {
                case DevicesStates.ActiveFirst when BtnsAdapter[0].Text != Resources.Disconnect || !GrpsModules[0].Enabled:
                    return;
                case DevicesStates.ActiveSecond when BtnsAdapter[0].Text != Resources.Disconnect || !GrpsModules[1].Enabled:
                    return;
                case DevicesStates.ActiveBoth when BtnsAdapter[0].Text != Resources.Disconnect ||
                                                   !GrpsModules[0].Enabled || !GrpsModules[1].Enabled:
                    return;
                case DevicesStates.ActiveFirst:
                    break;
                case DevicesStates.ActiveSecond:
                    break;
                case DevicesStates.ActiveBoth:
                    break;
                case DevicesStates.NoActive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (Cnt)
            {
                case 0:
                    switch (State)
                    {
                        case DevicesStates.ActiveFirst:
                            SendChargeParams(0);
                            Thread.Sleep(5);
                            Msgs[0] = DataFromCAN.GetData(CanAdapters[0]);
                            break;
                        case DevicesStates.ActiveSecond:
                            SendChargeParams(1);
                            Thread.Sleep(5);
                            Msgs[1] = DataFromCAN.GetData(CanAdapters[0]);
                            break;
                        case DevicesStates.ActiveBoth:
                            SendChargeParams(0);
                            SendChargeParams(1);
                            Thread.Sleep(5);
                            Msgs[0] = DataFromCAN.GetData(CanAdapters[0]);
                            Msgs[1] = DataFromCAN.GetData(CanAdapters[0]);
                            break;
                        case DevicesStates.NoActive:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    Cnt++;
                    break;
                case 1:
                    switch (State)
                    {
                        case DevicesStates.ActiveFirst:
                            AskDeviceParams();
                            Thread.Sleep(5);
                            GetDeviceParams(0);
                            break;
                        case DevicesStates.ActiveSecond:
                            AskDeviceParams();
                            Thread.Sleep(5);
                            GetDeviceParams(1);
                            break;
                        case DevicesStates.ActiveBoth:
                            AskDeviceParams();
                            Thread.Sleep(5);
                            GetDeviceParams(0);
                            GetDeviceParams(1);
                            break;
                        case DevicesStates.NoActive:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    Cnt++;
                    break;

                case 2:
                    switch (State)
                    {
                        case DevicesStates.ActiveFirst:
                            AskAmbientTemp();
                            Thread.Sleep(5);
                            GetAmbientDeviceTemp(0);
                            break;
                        case DevicesStates.ActiveSecond:
                            AskAmbientTemp();
                            Thread.Sleep(5);
                            GetAmbientDeviceTemp(1);
                            break;
                        case DevicesStates.ActiveBoth:
                            AskAmbientTemp();
                            Thread.Sleep(5);
                            GetAmbientDeviceTemp(0);
                            GetAmbientDeviceTemp(1);
                            break;
                        case DevicesStates.NoActive:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    Cnt++;
                    break;

                case 3:
                    switch (State)
                    {
                        case DevicesStates.ActiveFirst:
                            AskPhaseVoltage();
                            Thread.Sleep(5);
                            GetPhaseVoltage(0);
                            break;
                        case DevicesStates.ActiveSecond:
                            AskPhaseVoltage();
                            Thread.Sleep(5);
                            GetPhaseVoltage(1);
                            break;
                        case DevicesStates.ActiveBoth:
                            AskPhaseVoltage();
                            Thread.Sleep(5);
                            GetPhaseVoltage(0);
                            GetPhaseVoltage(1);
                            break;
                        case DevicesStates.NoActive:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    Cnt = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private UInt32 GetId(byte errorCode,
                             byte deviceNo,
                             byte commandNo,
                             byte destAddress,
                             byte sourceAddress)
        {
            string[] idParams = new string[5];
            idParams[0] = errorCode.ToString("X").Remove(0); // 00000111 -> 111  or 07 -> 7
            idParams[1] = deviceNo.ToString("X").Remove(0);  // 00001111 -> 1111 or 0F -> F
            idParams[2] = commandNo.ToString("X2");
            idParams[3] = destAddress.ToString("X2");
            idParams[4] = sourceAddress.ToString("X2");
            StringBuilder strId = new StringBuilder("", 8);

            foreach (string str in idParams)
                strId.Append(str);

            strId.Replace(" ", "");

            return 
                UInt32.TryParse(strId.ToString(), NumberStyles.AllowHexSpecifier, null, out UInt32 id) ? 
                    id : 0;
        }

        private char GetErrorCodeFromId(UInt32 id)
        {
            string strId = id.ToString();
            char strEC = strId[0];
            return strEC;
        }

        private void SendChargeParams(short deviceNo)
        {
            VoltWritten[deviceNo] =  NumRepresentations.UINTtoBYTE((uint)NudsVoltSI[deviceNo].Value * 1000);
            CurntWritten[deviceNo] = NumRepresentations.UINTtoBYTE((uint)NudsCurntSI[deviceNo].Value * 1000);

            Array.Reverse(VoltWritten[deviceNo]);
            Array.Reverse(CurntWritten[deviceNo]);

            byte[] data = new byte[8];

            // form BYTE charge params values to be sent
            for (int i = 0; i < 4; i++)
            {
                data[i] = VoltWritten[deviceNo][i];
            }
            for (int i = 4; i < 8; i++)
            {
                data[i] = CurntWritten[deviceNo][i - 4];
            }

            switch (State)
            {
                case DevicesStates.ActiveFirst:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxSetParamsDevice1, data);
                    break;
                case DevicesStates.ActiveSecond:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxSetParamsDevice2, data);
                    break;
                case DevicesStates.ActiveBoth:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxSetParamsBoth, data);
                    break;
                case DevicesStates.NoActive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected void GetDeviceParams(short deviceNo)
        {
            try
            {
                Msgs[deviceNo] = DataFromCAN.GetData(CanAdapters[0]);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Resources.ReceivingDeviceParamsError, deviceNo + 1),
                    Resources.ErrorMsg,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                Console.WriteLine(Resources.ReceivingDeviceParamsError, deviceNo + 1);
            }

            try
            {
                if (Msgs[deviceNo][0].Id == (UInt32) MessagesIDs.RxGetParamsDevice1 && State == DevicesStates.ActiveFirst ||
                    Msgs[deviceNo][0].Id == (UInt32) MessagesIDs.RxGetParamsDevice2 && State == DevicesStates.ActiveSecond ||
                    Msgs[deviceNo][0].Id == (UInt32) MessagesIDs.RxGetParamsBoth && State == DevicesStates.ActiveBoth)
                {
                    VoltRead[deviceNo] = new byte[4];
                    CurntRead[deviceNo] = new byte[4];

                    for (int i = 0; i < 4; i++)
                    {
                        VoltRead[deviceNo][i] = Msgs[deviceNo][0].Data[i];
                    }

                    for (int i = 4; i < 8; i++)
                    {
                        CurntRead[deviceNo][i - 4] = Msgs[deviceNo][0].Data[i];
                    }

                    Array.Reverse(VoltRead[deviceNo]);
                    Array.Reverse(CurntRead[deviceNo]);
                }
            }
            
            catch (Exception)
            {
                MessageBox.Show( string.Format(Resources.HandlingDeviceParamsError, deviceNo + 1),
                    Resources.ErrorMsg,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                Console.WriteLine(Resources.HandlingDeviceParamsError, deviceNo + 1);
            }
        }

        private void AskDeviceParams()
        {
            try
            {
                switch (State)
                {
                case DevicesStates.ActiveFirst:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32) MessagesIDs.TxGetParamsDevice1, new byte[8]);
                    break;
                case DevicesStates.ActiveSecond:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32) MessagesIDs.TxGetParamsDevice2, new byte[8]);
                    break;
                case DevicesStates.ActiveBoth:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32) MessagesIDs.TxGetParamsBoth, new byte[8]);
                    break;
                case DevicesStates.NoActive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Resources.AskingDeviceParamsError),
                    Resources.ErrorMsg,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                Console.WriteLine(Resources.AskingDeviceParamsError);
            }
        }

        private void GetAmbientDeviceTemp(short deviceNo)
        {
            try
            {
                Msgs[deviceNo] = DataFromCAN.GetData(CanAdapters[0]);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Resources.ReceivingAmbientTempError, deviceNo +1),
                    Resources.ErrorMsg,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                Console.WriteLine(Resources.ReceivingAmbientTempError, deviceNo + 1);
            }

            try
            {
                if (Msgs[deviceNo][0].Id == (UInt32) MessagesIDs.RxTempDevice1 && State == DevicesStates.ActiveFirst ||
                    Msgs[deviceNo][0].Id == (UInt32) MessagesIDs.RxTempDevice2 && State == DevicesStates.ActiveSecond)
                {
                    AmbientTemp[deviceNo] = new byte[4];
                    AmbientTemp[deviceNo][0] = Msgs[deviceNo][0].Data[4];
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Помилка при обробці даних температури модуля",
                                Resources.ErrorMsg,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                Console.WriteLine("Помилка при обробці даних температури модуля");
            }
        }

        private void AskAmbientTemp()
        {
            try
            {
                //byte[] data = {0x00, 0x00, 0x02, 0x00, 0x1B, 0x00, 0x40, 0x00};
                switch (State)
                {
                case DevicesStates.ActiveFirst:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32) MessagesIDs.TxTempDevice1, new byte[8]);
                    break;
                case DevicesStates.ActiveSecond:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32) MessagesIDs.TxTempDevice2, new byte[8]);
                    break;
                case DevicesStates.ActiveBoth:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32) MessagesIDs.TxTempDevice1, new byte[8]);
                    DataFromCAN.SendData(CanAdapters[0], (UInt32) MessagesIDs.TxTempDevice2, new byte[8]);
                    break;
                case DevicesStates.NoActive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.AskingAmbTempError,
                    Resources.ErrorMsg,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                Console.WriteLine(Resources.AskingAmbTempError);
            }
        }

        private void GetPhaseVoltage(short deviceNo)
        {
            try
            {
                Msgs[deviceNo] = DataFromCAN.GetData(CanAdapters[0]);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Resources.ReceivingPhaseVoltagesError, deviceNo +1),
                    Resources.ErrorMsg,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                Console.WriteLine(Resources.ReceivingPhaseVoltagesError, deviceNo + 1);
            }
            try
            {
                if (Msgs[deviceNo][0].Id == (UInt32)MessagesIDs.RxPhaseVoltDevice1 && State == DevicesStates.ActiveFirst ||
                    Msgs[deviceNo][0].Id == (UInt32)MessagesIDs.RxPhaseVoltDevice2 && State == DevicesStates.ActiveSecond ||
                    Msgs[deviceNo][0].Id == (UInt32)MessagesIDs.RxPhaseVoltBoth && State == DevicesStates.ActiveBoth)
                {
                    VoltAB[deviceNo] = new byte[2];
                    VoltBC[deviceNo] = new byte[2];
                    VoltCA[deviceNo] = new byte[2];

                    for (int i = 0; i < 2; i++)
                    {
                        VoltAB[deviceNo][i] = Msgs[deviceNo][0].Data[i];
                    }

                    for (int i = 2; i < 4; i++)
                    {
                        VoltBC[deviceNo][i - 2] = Msgs[deviceNo][0].Data[i];
                    }

                    for (int i = 4; i < 6; i++)
                    {
                        VoltCA[deviceNo][i - 4] = Msgs[deviceNo][0].Data[i];
                    }

                    // handle endianness

                    Array.Reverse(VoltAB[deviceNo]);
                    Array.Reverse(VoltBC[deviceNo]);
                    Array.Reverse(VoltCA[deviceNo]);
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(Resources.HandlingPhaseVoltagesError, deviceNo + 1),
                                Resources.ErrorMsg,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                Console.WriteLine(Resources.HandlingPhaseVoltagesError, deviceNo + 1);
            }
        }

        private void AskPhaseVoltage()
        {
            try
            {
                switch (State)
                {
                case DevicesStates.ActiveFirst:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32) MessagesIDs.TxPhaseVoltDevice1,
                        new byte[8]); // { 0xF, 0x23, 0xE, 0xF0, 0xE, 0xCC, 0x0, 0x0 }
                    break;
                case DevicesStates.ActiveSecond:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32) MessagesIDs.TxPhaseVoltDevice2, new byte[8]);
                    break;
                case DevicesStates.ActiveBoth:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32) MessagesIDs.TxPhaseVoltBoth, new byte[8]);
                    break;
                case DevicesStates.NoActive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.AskingPhaseVoltagesError,
                    Resources.ErrorMsg,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                Console.WriteLine(Resources.AskingPhaseVoltagesError);
            }
        }

        private void SetPowerDevice(byte isOff)
        {
            try
            {
                switch (State)
                {
                case DevicesStates.ActiveFirst:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxPowerDevice1, new byte[] { isOff, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;
                case DevicesStates.ActiveSecond:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxPowerDevice2, new byte[] { isOff, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;
                case DevicesStates.ActiveBoth:
                    DataFromCAN.SendData(CanAdapters[0], (UInt32)MessagesIDs.TxPowerBoth, new byte[] { isOff, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;
                case DevicesStates.NoActive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.PoweringDeviceError,
                    Resources.ErrorMsg,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                Console.WriteLine(Resources.PoweringDeviceError);
            }
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
                    TxtsVoltage[1].Text = "";
                    TxtsCurrent[1].Text = "";
                    State = DevicesStates.ActiveFirst;
                    break;

                case "Активний другий модуль":
                    GrpsModules[0].Enabled = false;
                    GrpsModules[1].Enabled = true;
                    btnConnect1.Enabled = true;
                    NudsVoltSI[0].Value =  NudsVoltSI [0].Minimum;
                    NudsCurntSI[0].Value = NudsCurntSI[0].Minimum;
                    TxtsVoltage[0].Text = "";
                    TxtsCurrent[0].Text = "";
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

        private void ChkPowerDevice1_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnsAdapter[0].Text != Resources.Disconnect) return;
            CheckBox chk = (CheckBox)sender;
            switch (chk.Checked)
            {
                case true:
                    SetPowerDevice(0x0);
                    break;
                case false:
                    SetPowerDevice(0x1);
                    break;
            }
        }

        private void ChkPowerDevice2_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnsAdapter[0].Text != Resources.Disconnect) return;
            CheckBox chk = (CheckBox)sender;
            switch (chk.Checked)
            {
                case true:
                    SetPowerDevice(0x0);
                    break;
                case false:
                    SetPowerDevice(0x1);
                    break;
            }
        }

        private void TxtCurVolt1_TextChanged(object sender, EventArgs e)
        {
            SwVoltRead.Restart();
        }

        private void TxtCurCurnt1_TextChanged(object sender, EventArgs e)
        {
            SwCurntRead.Restart();
        }

        private void TxtTemperature1_TextChanged(object sender, EventArgs e)
        {
            SwAmbientTemp.Restart();
        }

        private void TxtPhaseABVolt1_TextChanged(object sender, EventArgs e)
        {
            SwPhaseVoltAB.Restart();
        }

        private void TxtPhaseBCVolt1_TextChanged(object sender, EventArgs e)
        {
            SwPhaseVoltBC.Restart();
        }

        private void TxtPhaseCAVolt1_TextChanged(object sender, EventArgs e)
        {
            SwPhaseVoltCA.Restart();
        }
    }
}
