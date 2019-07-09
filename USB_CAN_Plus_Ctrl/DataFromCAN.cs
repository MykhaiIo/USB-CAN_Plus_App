using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSCom.CanApi;

namespace USB_CAN_Plus_Ctrl
{
    static class DataFromCAN
    {
        public static VSCAN CanDevice { get; internal set; }
        private static VSCAN_MSG[] msgs = new VSCAN_MSG[1];
        private static UInt32 Read = 0, Written = 0;
        private static byte Flags = 0x0;

        public static VSCAN InitCAN()
        {
            VSCAN CanDevice = new VSCAN();
            VSCAN_HWPARAM hw = new VSCAN_HWPARAM();
            VSCAN_API_VERSION api_ver = new VSCAN_API_VERSION();

            try
            {
                // set debugging options

                CanDevice.SetDebug(VSCAN.VSCAN_DEBUG_NONE);
                CanDevice.SetDebugMode(VSCAN.VSCAN_DEBUG_MODE_FILE);

                // open CAN channel: please specify the name of your device according to User Manual

                CanDevice.Open(VSCAN.VSCAN_FIRST_FOUND, VSCAN.VSCAN_MODE_SELF_RECEPTION);
                
                // set some options

                CanDevice.SetSpeed(VSCAN.VSCAN_SPEED_20K);
                CanDevice.SetTimestamp(VSCAN.VSCAN_TIMESTAMP_ON);
                CanDevice.SetBlockingRead(VSCAN.VSCAN_IOCTL_ON);

                // get HW Params

                CanDevice.GetHwParams(ref hw);
                Console.WriteLine("Get hardware paramter:");
                Console.WriteLine("HwVersion:" + hw.HwVersion + " SwVersion:" + (hw.SwVersion >> 4) + "." + (hw.SwVersion & 0x0f));
                Console.WriteLine("SerNr:" + hw.SerialNr + " HwType:" + hw.HwType);

                // get API version

                CanDevice.GetApiVersion(ref api_ver);
                Console.WriteLine("");
                Console.WriteLine("API version: " + api_ver.Major + "." + api_ver.Minor + "." + api_ver.SubMinor);
            }

            catch (Exception e)
            {
                Console.WriteLine("CAN opened " + e.Message);
            }

            return CanDevice;
        }

        public static void DeinitCAN(VSCAN CanDevice)
        {
            CanDevice.Close();
        }

        public static bool SendData(byte CmdNo, UInt32 ID, byte[] Data)
        {
            /*try
            {*/
                msgs[0].Data = Data;
                msgs[0].Id = ID;
                msgs[0].Size = 8;
                CanDevice.Write(msgs, 1, ref Written);
                CanDevice.Flush();
                Console.WriteLine("");
                Console.WriteLine("Send CAN frames: " + Written);
                return true;
            /*}

            catch(Exception e)
            {
                Console.WriteLine("Message transmitted " + e.Message);
                return false;
            }*/
        }   

        public static VSCAN_MSG[] GetData()
        {
            try
            {
                CanDevice.Read(ref msgs, 1, ref Read);
                Console.WriteLine("");
                Console.WriteLine("Read CAN frames: " + Read);
                return msgs;
            }
            catch (Exception e)
            {
                Console.WriteLine("Message recieved " + e.Message);
                return msgs;
            }
        }

        public static string GetStrHEXCurValue()
        {
            VSCAN_MSG[] msgs = new VSCAN_MSG[2];
            UInt32 Written = 0;
            UInt32 Read = 0;

            byte Flags = 0x0;
            string[] strByte = new string[4];
            StringBuilder strCur = new StringBuilder("", 8);


            CanDevice = InitCAN();

            msgs[0].Id = 0x029A3FF0;
            msgs[0].Size = 8;
            msgs[0].Data = new byte[8];
            msgs[0].Data[0] = 0x01;
            msgs[0].Data[1] = 0x00;
            msgs[0].Data[2] = 0x00;
            msgs[0].Data[3] = 0x00;
            msgs[0].Data[4] = 0x00;
            msgs[0].Data[5] = 0x00;
            msgs[0].Data[6] = 0x00;
            msgs[0].Data[7] = 0x00;
            msgs[0].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;


            msgs[1].Id = 0x029C3FF0;
            msgs[1].Size = 8;
            msgs[1].Data = new byte[8];
            msgs[1].Data[0] = 0x00;
            msgs[1].Data[1] = 0x0b;
            msgs[1].Data[2] = 0x71;
            msgs[1].Data[3] = 0xb0;
            msgs[1].Data[4] = 0x00;
            msgs[1].Data[5] = 0x00;
            msgs[1].Data[6] = 0x3a;
            msgs[1].Data[7] = 0x98;
            msgs[1].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;

            try
            {

            // send CAN frames

            CanDevice.Write(msgs, 2, ref Written);

            // send immediately 

            CanDevice.Flush();
            Console.WriteLine("");
            Console.WriteLine("Send CAN frames: " + Written);

            // read CAN frames

            CanDevice.Read(ref msgs, 2, ref Read);

            Console.WriteLine("");
            Console.WriteLine("Read CAN frames: " + Read);

            for (int k = 0; k < msgs[1].Data.Length / 2; k++)
            {
                strByte[k] = msgs[1].Data[k].ToString();
                strCur.Append(strByte[k]);
            }

            for (int i = 0; i < Read; i++)

            {
                Console.WriteLine("");
                Console.WriteLine("CAN frame " + i);
                Console.WriteLine("ID: " + msgs[i].Id);
                Console.WriteLine("Size: " + msgs[i].Size);
                Console.Write("Data: ");

                for (int j = 0; j < msgs[i].Size; j++)
                {
                    Console.Write(msgs[i].Data[j] + " ");
                }



                Console.WriteLine("");

                if ((msgs[i].Flags & VSCAN.VSCAN_FLAGS_STANDARD) != 0)

                    Console.WriteLine("VSCAN_FLAGS_STANDARD");

                if ((msgs[i].Flags & VSCAN.VSCAN_FLAGS_EXTENDED) != 0)

                    Console.WriteLine("VSCAN_FLAGS_EXTENDED");

                if ((msgs[i].Flags & VSCAN.VSCAN_FLAGS_REMOTE) != 0)

                    Console.WriteLine("VSCAN_FLAGS_REMOTE");

                if ((msgs[i].Flags & VSCAN.VSCAN_FLAGS_TIMESTAMP) != 0)

                    Console.WriteLine("VSCAN_FLAGS_TIMESTAMP");

                Console.WriteLine("TS: " + msgs[i].TimeStamp);

            }



            // get extended status and error flags

            CanDevice.GetFlags(ref Flags);

            Console.WriteLine("");

            Console.WriteLine("Extended status and error flags: " + Flags);

            DecodeFlags(Flags);



            // close CAN channel

            DeinitCAN(CanDevice);
        }


            catch (Exception e)
            {
                Console.WriteLine("CAN opened and " + e.Message);
            }

            return strCur.ToString();

        }

        private static void DecodeFlags(int flags)
        {
            if ((flags & VSCAN.VSCAN_IOCTL_FLAG_API_RX_FIFO_FULL) > 0)
            {
                Console.WriteLine("API RX FIFO full");
            }

            if ((flags & VSCAN.VSCAN_IOCTL_FLAG_ARBIT_LOST) > 0)
            {
                Console.WriteLine("Arbitration lost");
            }

            if ((flags & VSCAN.VSCAN_IOCTL_FLAG_BUS_ERROR) > 0)
            {
                Console.WriteLine("Bus error");
            }

            if ((flags & VSCAN.VSCAN_IOCTL_FLAG_DATA_OVERRUN) > 0)
            {
                Console.WriteLine("Data overrun");
            }

            if ((flags & VSCAN.VSCAN_IOCTL_FLAG_ERR_PASSIVE) > 0)
            {
                Console.WriteLine("Passive error");
            }

            if ((flags & VSCAN.VSCAN_IOCTL_FLAG_ERR_WARNING) > 0)
            {
                Console.WriteLine("Error warning");
            }

            if ((flags & VSCAN.VSCAN_IOCTL_FLAG_RX_FIFO_FULL) > 0)
            {
                Console.WriteLine("RX FIFO full");
            }

            if ((flags & VSCAN.VSCAN_IOCTL_FLAG_TX_FIFO_FULL) > 0)
            {
                Console.WriteLine("TX FIFO full");
            }

        }
    }
}
