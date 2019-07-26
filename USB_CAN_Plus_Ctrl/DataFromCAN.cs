using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VSCom.CanApi;
using System.Diagnostics;

namespace USB_CAN_Plus_Ctrl
{
    internal static class DataFromCAN
    {
        private static byte _flags = 0x0;
        private static VSCAN_CODE_MASK mask;

        public static VSCAN InitCAN()
        {
            var canAdapter = new VSCAN();
            try
            {
                // set debugging options

                canAdapter.SetDebug(VSCAN.VSCAN_DEBUG_NONE);
                canAdapter.SetDebugMode(VSCAN.VSCAN_DEBUG_MODE_FILE);

                // open CAN channel: please specify the name of your device according to User Manual

                canAdapter.Open(VSCAN.VSCAN_FIRST_FOUND,
                               VSCAN.VSCAN_MODE_NORMAL);

                // set some options

                canAdapter.SetSpeed(VSCAN.VSCAN_SPEED_125K);
                canAdapter.SetTimestamp(VSCAN.VSCAN_TIMESTAMP_ON);
                canAdapter.SetBlockingRead(VSCAN.VSCAN_IOCTL_ON);
                /*canAdapter.SetFilterMode(VSCAN.VSCAN_FILTER_MODE_DUAL);
                // refer to http://www.si-kwadraat.nl/sja1000/codemask.php to establish acceptance code and mask
                // for the range of Rx IDs in USB_CAN_Plus_Ctrl.MessagesIDs
                // (http://www.si-kwadraat.nl/sja1000/codemask.php?en=dcia&mo=0&cd=00000000&ma=FFFFFFFF&fm=2&id=0.1&rn=0.5)
                mask.Code = 0x582F5007;
                mask.Mask = 0x000000E8;
                canAdapter.SetACCCodeMask(mask);*/

            }

            catch (Exception e)
            {
                Console.WriteLine($"CAN not opened {e.Message}");
            }

            return canAdapter;
        }

        public static void DeinitCAN(VSCAN canAdapter)
        {
            try
            {
                canAdapter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"CAN not closed {e.Message}");
            }
        }

        public static bool SendData(VSCAN canAdapter, UInt32 id, byte[] data)
        {
            var msgs = new VSCAN_MSG[1];
            UInt32 written = 0;
            try
            {
                msgs[0].Data = data;
                msgs[0].Id = id;
                msgs[0].Size = 8;
                msgs[0].Flags = VSCAN.VSCAN_FLAGS_EXTENDED;

                canAdapter.Write(msgs, 1, ref written);
                canAdapter.Flush();
                Console.WriteLine("");
                Console.WriteLine($"Send CAN frames: {written}");
                for (var i = 0; i < written; i++)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"CAN frame {i}");
                    Console.WriteLine($"ID: {msgs[i].Id.ToString("X")}");
                    Console.WriteLine($"Size: {msgs[i].Size}");
                    Console.Write("Data: ");

                    for (var j = 0; j < msgs[i].Size; j++)
                    {
                        Console.Write($"{msgs[i].Data[j].ToString("X")} ");
                    }

                    Console.WriteLine("");
                }

                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine($"Message not transmitted {e.Message} {canAdapter.ToString()}");
                return false;
            }
        }

        public static VSCAN_MSG[] GetData(VSCAN canAdapter)
        {
            var msgs = new VSCAN_MSG[1];
            UInt32 read = 0;
            try
            {
                canAdapter.Read(ref msgs, 1, ref read);
                Console.WriteLine("");
                Console.WriteLine($"Read CAN frames: {read}");
                for (var i = 0; i < read; i++)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"CAN frame {i}");
                    Console.WriteLine($"ID: {msgs[i].Id.ToString("X")}");
                    Console.WriteLine($"Size: {msgs[i].Size}");
                    Console.Write("Data: ");

                    for (var j = 0; j < msgs[i].Size; j++)
                    {
                        Console.Write(msgs[i].Data[j].ToString("X") + " ");
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

                canAdapter.GetFlags(ref _flags);

                Console.WriteLine("");

                Console.WriteLine($"Extended status and error flags: {_flags}");

                DecodeFlags(_flags);

                return msgs;
            }

            catch (Exception e)
            {
                Console.WriteLine($"Message not recieved {e.Message}");
                return msgs;
            }
        }


        public static void DecodeFlags(int flags)
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
























































/*
        public static void Test()
        {
            VSCAN_MSG[] msgs = new VSCAN_MSG[10];
            UInt32 written = 0;
            UInt32 read = 0;

            byte flags = 0x0;

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

                canAdapter.Write(msgs, 10, ref written);

                // send immediately 

                canAdapter.Flush();
                Console.WriteLine("");
                Console.WriteLine("Send CAN frames: " + written);

                msgs = new VSCAN_MSG[10];

                Thread.Sleep(1000);

                // read CAN frames

                canAdapter.Read(ref msgs, 10, ref read);

                Console.WriteLine("");
                Console.WriteLine("Read CAN frames: " + read);



                for (int i = 0; i < read; i++)

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

                canAdapter.GetFlags(ref flags);

                Console.WriteLine("");

                Console.WriteLine("Extended status and error flags: " + flags);

                DecodeFlags(flags);
            }

            catch (Exception e)
            {
                Console.WriteLine("CAN opened and " + e.Message);
            }
        }
*/
    }
}
