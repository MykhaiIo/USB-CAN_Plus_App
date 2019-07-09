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
        }

        private void BtnConnect1_Click(object sender, EventArgs e)
        {
            DataFromCAN.CanDevice = DataFromCAN.InitCAN();
            VoltBWritten = NumRepresentations.FPtoBYTE((float)nudOutVoltSI1.Value / 1000);
            CurntBWritten = NumRepresentations.FPtoBYTE((float)nudOutCurntSI1.Value / 1000);

            Array.Reverse(VoltBWritten);
            Array.Reverse(CurntBWritten);

            byte[] Data = new byte[8];

            // form HEX volt value to be sent
            for (int i = 0; i < 3; i++)
            {
                Data[i] = VoltBWritten[i];
            }

            // form HEX current value to be sent
            for (int i = 3; i < 7; i++)
            {
                Data[i] = CurntBWritten[i - 3];
            }
            DataFromCAN.SendData(Convert.ToByte(0x1B), 0x029B3FF0, Data);
            VSCAN_MSG[] msg = DataFromCAN.GetData();

            // form volt value to be recieved
            for (int i = 0; i < 3; i++)
            {
                VoltBRead[i] = msg[0].Data[i];
            }

            // form current value to be recieved
            for (int i = 3; i < 7; i++)
            {
                CurntBRead[i - 3] = msg[0].Data[i];
            }

            Array.Reverse(VoltBRead);
            Array.Reverse(CurntBRead);

            txtOutVoltFP1.Text = NumRepresentations.BYTEtoFP(VoltBRead).ToString();
            txtOutCurntFP1.Text = NumRepresentations.BYTEtoFP(CurntBRead).ToString();
            }

        private void BtnDisconnect1_Click(object sender, EventArgs e)
        {
            DataFromCAN.DeinitCAN(DataFromCAN.CanDevice);
        }
    }
}
