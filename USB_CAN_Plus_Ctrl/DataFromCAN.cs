using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VSCom.CanApi;

namespace USB_CAN_Plus_Ctrl
{
    static class DataFromCAN
    {
        public static VSCAN CanDevice { get; internal set; }
        // private static byte Flags = 0x0;

        public static VSCAN InitCAN()
        {
            VSCAN CanDevice = new VSCAN();
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
            }

            catch (Exception e)
            {
                Console.WriteLine("CAN not opened " + e.Message);
            }

            return CanDevice;
        }

        public static void DeinitCAN(VSCAN CanDevice)
        {
            try
            {
                CanDevice.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("CAN not closed " + e.Message);
            }

        }

        public static bool SendData(byte CmdNo, UInt32 ID, byte[] Data)
        {
            VSCAN_MSG[] msgs = new VSCAN_MSG[1];
            UInt32 Written = 0;
            try
            {
                msgs[0].Data = Data;
                msgs[0].Id = ID;
                msgs[0].Size = 8;
                msgs[0].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;

                CanDevice.Write(msgs, 1, ref Written);
                CanDevice.Flush();
                Thread.Sleep(500);
                Console.WriteLine("");
                Console.WriteLine("Send CAN frames: " + Written);
                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine("Message not transmitted " + e.Message);
                return false;
            }
        }

        public static VSCAN_MSG[] GetData()
        {
            VSCAN_MSG[] msgs = new VSCAN_MSG[1];
            UInt32 Read = 0;
            try
            {
                CanDevice.Read(ref msgs, 1, ref Read);
                Console.WriteLine("");
                Console.WriteLine("Read CAN frames: " + Read);
                return msgs;
            }

            catch (Exception e)
            {
                Console.WriteLine("Message not recieved " + e.Message);
                return msgs;
            }
        }

        public static void Test()
        {
            VSCAN_MSG[] msgs = new VSCAN_MSG[10];
            UInt32 Written = 0;
            UInt32 Read = 0;

            byte Flags = 0x0;
            
            msgs[0].Id = 0x029A3FF0;
            msgs[0].Size = 8;
            msgs[0].Data = new byte[8];
            msgs[0].Data[0] = 0x00;
            msgs[0].Data[1] = 0x0b;
            msgs[0].Data[2] = 0x71;
            msgs[0].Data[3] = 0xb0;
            msgs[0].Data[4] = 0x00;
            msgs[0].Data[5] = 0x00;
            msgs[0].Data[6] = 0x3a;
            msgs[0].Data[7] = 0x98;
            msgs[0].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;


            msgs[1].Id = 0x029C3FF0;
            msgs[1].Size = 8;
            msgs[1].Data = new byte[8];
            msgs[1].Data[0] = 0x00;
            msgs[1].Data[1] = 0x00;
            msgs[1].Data[2] = 0x00;
            msgs[1].Data[3] = 0x00;
            msgs[1].Data[4] = 0x00;
            msgs[1].Data[5] = 0x00;
            msgs[1].Data[6] = 0x00;
            msgs[1].Data[7] = 0x01;
            msgs[1].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;

            msgs[2].Id = 0x029A3FF0;
            msgs[2].Size = 8;
            msgs[2].Data = new byte[8];
            msgs[2].Data[0] = 0x00;
            msgs[2].Data[1] = 0x0b;
            msgs[2].Data[2] = 0x71;
            msgs[2].Data[3] = 0xb0;
            msgs[2].Data[4] = 0x00;
            msgs[2].Data[5] = 0x00;
            msgs[2].Data[6] = 0x3a;
            msgs[2].Data[7] = 0x98;
            msgs[2].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;


            msgs[3].Id = 0x029C3FF0;
            msgs[3].Size = 8;
            msgs[3].Data = new byte[8];
            msgs[3].Data[0] = 0x00;
            msgs[3].Data[1] = 0x00;
            msgs[3].Data[2] = 0x00;
            msgs[3].Data[3] = 0x00;
            msgs[3].Data[4] = 0x00;
            msgs[3].Data[5] = 0x00;
            msgs[3].Data[6] = 0x00;
            msgs[3].Data[7] = 0x01;
            msgs[3].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;

            msgs[4].Id = 0x029A3FF0;
            msgs[4].Size = 8;
            msgs[4].Data = new byte[8];
            msgs[4].Data[0] = 0x00;
            msgs[4].Data[1] = 0x0b;
            msgs[4].Data[2] = 0x71;
            msgs[4].Data[3] = 0xb0;
            msgs[4].Data[4] = 0x00;
            msgs[4].Data[5] = 0x00;
            msgs[4].Data[6] = 0x3a;
            msgs[4].Data[7] = 0x98;
            msgs[4].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;


            msgs[5].Id = 0x029C3FF0;
            msgs[5].Size = 8;
            msgs[5].Data = new byte[8];
            msgs[5].Data[0] = 0x00;
            msgs[5].Data[1] = 0x00;
            msgs[5].Data[2] = 0x00;
            msgs[5].Data[3] = 0x00;
            msgs[5].Data[4] = 0x00;
            msgs[5].Data[5] = 0x00;
            msgs[5].Data[6] = 0x00;
            msgs[5].Data[7] = 0x01;
            msgs[5].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;

            msgs[6].Id = 0x029A3FF0;
            msgs[6].Size = 8;
            msgs[6].Data = new byte[8];
            msgs[6].Data[0] = 0x00;
            msgs[6].Data[1] = 0x0b;
            msgs[6].Data[2] = 0x71;
            msgs[6].Data[3] = 0xb0;
            msgs[6].Data[4] = 0x00;
            msgs[6].Data[5] = 0x00;
            msgs[6].Data[6] = 0x3a;
            msgs[6].Data[7] = 0x98;
            msgs[6].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;


            msgs[7].Id = 0x029C3FF0;
            msgs[7].Size = 8;
            msgs[7].Data = new byte[8];
            msgs[7].Data[0] = 0x00;
            msgs[7].Data[1] = 0x00;
            msgs[7].Data[2] = 0x00;
            msgs[7].Data[3] = 0x00;
            msgs[7].Data[4] = 0x00;
            msgs[7].Data[5] = 0x00;
            msgs[7].Data[6] = 0x00;
            msgs[7].Data[7] = 0x01;
            msgs[7].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;

            msgs[8].Id = 0x029A3FF0;
            msgs[8].Size = 8;
            msgs[8].Data = new byte[8];
            msgs[8].Data[0] = 0x00;
            msgs[8].Data[1] = 0x0b;
            msgs[8].Data[2] = 0x71;
            msgs[8].Data[3] = 0xb0;
            msgs[8].Data[4] = 0x00;
            msgs[8].Data[5] = 0x00;
            msgs[8].Data[6] = 0x3a;
            msgs[8].Data[7] = 0x98;
            msgs[8].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;


            msgs[9].Id = 0x029C3FF0;
            msgs[9].Size = 8;
            msgs[9].Data = new byte[8];
            msgs[9].Data[0] = 0x00;
            msgs[9].Data[1] = 0x00;
            msgs[9].Data[2] = 0x00;
            msgs[9].Data[3] = 0x00;
            msgs[9].Data[4] = 0x00;
            msgs[9].Data[5] = 0x00;
            msgs[9].Data[6] = 0x00;
            msgs[9].Data[7] = 0x01;
            msgs[9].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;

            try
            {

                // send CAN frames

                CanDevice.Write(msgs, 10, ref Written);

                // send immediately 

                CanDevice.Flush();
                Console.WriteLine("");
                Console.WriteLine("Send CAN frames: " + Written);

                msgs = new VSCAN_MSG[10];

                Thread.Sleep(1000);

                // read CAN frames

                CanDevice.Read(ref msgs, 10, ref Read);

                Console.WriteLine("");
                Console.WriteLine("Read CAN frames: " + Read);



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
            }


            catch (Exception e)
            {
                Console.WriteLine("CAN opened and " + e.Message);
            }
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
