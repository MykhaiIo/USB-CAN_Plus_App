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
        private byte[] m_VoltHEXWritten;
        private byte[] m_CurntHEXWritten;
        private byte[] m_VoltHEXRead = new byte[4];
        private byte[] m_CurntHEXRead = new byte[4];

        public byte[] CurntBWritten { get => m_CurntHEXWritten; set => m_CurntHEXWritten = value; }
        public byte[] VoltBWritten { get => m_VoltHEXWritten; set => m_VoltHEXWritten = value; }
        public byte[] VoltBRead { get => m_VoltHEXRead; set => m_VoltHEXRead = value; }
        public byte[] CurntBRead { get => m_CurntHEXRead; set => m_CurntHEXRead = value; }

        public USB_CAN_Plus_Ctrl()
        {
            InitializeComponent();
            /*int num = 0;
            TimerCallback tm = new TimerCallback(DisplayParams);       
            System.Threading.Timer timer = new System.Threading.Timer(tm, num, 0, 2000);*/
        }

        private void SendCurntVal()
        {
            byte[] Data = new byte[8];

            // form HEX current value to be sent
            for (int i = 4; i < 8; i++)
            {
                Data[i] = CurntBWritten[i - 4];
            }
            DataFromCAN.SendData(Convert.ToByte(0x1B), 0x029B3FF0, Data);
        }

        private void SendVoltVal()
        {
            byte[] Data = new byte[8];

            // form HEX volt value to be sent
            for (int i = 0; i < 4; i++)
            {
                Data[i] = VoltBWritten[i];
            }
            DataFromCAN.SendData(Convert.ToByte(0x1B), 0x029B3FF0, Data);
        }

        private void RecieveCurntVal()
        {
            VSCAN_MSG[] msg = DataFromCAN.GetData();

            // form current value to be recieved
            for (int i = 4; i < 8; i++)
            {
                CurntBRead[i - 4] = msg[0].Data[i];
            }
        }

        private void RecieveVoltVal()
        {
            VSCAN_MSG[] msg = DataFromCAN.GetData();

            // form volt value to be recieved
            for (int i = 0; i < 4; i++)
            {
                VoltBRead[i] = msg[0].Data[i];
            }
        }

        private void DisplayDeviceParams()
        {
            VSCAN_HWPARAM hw = new VSCAN_HWPARAM();
            VSCAN_API_VERSION api_ver = new VSCAN_API_VERSION();
            DataFromCAN.CanDevice.GetHwParams(ref hw);
            DataFromCAN.CanDevice.GetApiVersion(ref api_ver);

            // get HW Params

            lblHwVer.Text = "Версія модуля: " + hw.HwVersion;
            lblSwVer.Text = "Версія ПЗ: " + (hw.SwVersion >> 4) + "." + (hw.SwVersion & 0x0f);
            lblSerialNo.Text = "Серійний номер: " + hw.SerialNr;
            lblHwType.Text = "Тип модуля: " + hw.HwType;

            lblAPIVer.Text = "Версія API: " + api_ver.Major + "." + api_ver.Minor + "." + api_ver.SubMinor;
        }

        private void BtnConnect1_Click(object sender, EventArgs e)
        {
            if (btnConnect1.Text == "Підключити")
            {
                DataFromCAN.CanDevice = DataFromCAN.InitCAN();
                DisplayDeviceParams();
                btnConnect1.Text = "Відключити";
            }
            else if (btnConnect1.Text == "Відключити")
            {
                DataFromCAN.DeinitCAN(DataFromCAN.CanDevice);
                lblHwVer.Text = "Версія модуля: ";
                lblSwVer.Text = "Версія ПЗ: ";
                lblSerialNo.Text = "Серійний номер: ";
                lblHwType.Text = "Тип модуля: ";

                lblAPIVer.Text = "Версія API: ";
                btnConnect1.Text = "Підключити";
            }          
        }

        private void NudOutVoltSI1_ValueChanged(object sender, EventArgs e)
        {
            VoltBWritten = NumRepresentations.FPtoBYTE((float)nudOutVoltSI1.Value / 1000);
            Array.Reverse(VoltBWritten);
            SendVoltVal();
            RecieveVoltVal();
            Array.Reverse(VoltBRead);
            txtOutVoltFP1.Text = NumRepresentations.BYTEtoFP(VoltBRead).ToString();
        }

        private void NudOutCurntSI1_ValueChanged(object sender, EventArgs e)
        {
            CurntBWritten = NumRepresentations.FPtoBYTE((float)nudOutCurntSI1.Value / 1000);
            Array.Reverse(CurntBWritten);
            SendCurntVal();
            RecieveCurntVal();
            Array.Reverse(CurntBRead);
            txtOutCurntFP1.Text = NumRepresentations.BYTEtoFP(CurntBRead).ToString();

        }
    }
}
