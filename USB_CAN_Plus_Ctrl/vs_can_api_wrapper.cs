/**
 * @file vs_can_api_wrapper.cs
 * @brief VS CAN API wrapper class 
 * @version 1.6.0
 */

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VSCom.CanApi
{
    /**
     * @brief CAN message structure
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct VSCAN_MSG
    {
        public UInt32 Id; /**< CAN frame ID */
        public byte Size; /**< number of data bytes */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Data; /**< data bytes */
        /**
         * CAN frame flags:
         *  - VSCAN::VSCAN_FLAGS_STANDARD
         *  - VSCAN::VSCAN_FLAGS_EXTENDED
         *  - VSCAN::VSCAN_FLAGS_REMOTE
         *  - VSCAN::VSCAN_FLAGS_TIMESTAMP
         */
        public byte Flags;
        public UInt16 TimeStamp; /**< time stamp if configured */
    }

    /**
     * @brief API Version Structure
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct VSCAN_API_VERSION
    {
        public byte Major; /**< mojor number */
        public byte Minor; /**< minor number */
        public byte SubMinor; /**< subminor number */
    }

    /**
     * @brief Bit Timing Register Structure
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct VSCAN_BTR
    {
        public byte Btr0; /**< first bit-timning register */
        public byte Btr1; /**< second bit-timning register */
    }

    /**
     * @brief Acceptance Code and Mask Structure
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct VSCAN_CODE_MASK
    {
        public UInt32 Code; /**< acceptance code */
        public UInt32 Mask; /**< acceptance mask */
    }

    /**
     * @brief Hardware Parameter Structure
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct VSCAN_HWPARAM
    {
        public UInt32 SerialNr; /**< device serial number */
        public byte HwVersion;  /**< hardware version */
        public byte SwVersion;  /**< firmware version */
        /**
         * hardware type:
         *  - VSCAN::VSCAN_HWTYPE_UNKNOWN
         *  - VSCAN::VSCAN_HWTYPE_SERIAL
         *  - VSCAN::VSCAN_HWTYPE_USB
         *  - VSCAN::VSCAN_HWTYPE_NET
         *  - VSCAN::VSCAN_HWTYPE_BUS
         */
        public byte HwType;
    }

    class VSCANException : Exception
    {
        public VSCANException(string str)
        {
            Console.WriteLine(str);
        }
    }

    class VSCAN
    {
        /**
         * @name CAN device name
         * @{ */
        public const string VSCAN_FIRST_FOUND = ""; /**< use first found VSCAN device */
        /** @} */

        /**
         * @name CAN Channel Modes
         * @{ */
        /** the normal operation mode */
        public const Int32 VSCAN_MODE_NORMAL = 0;
        /**
         * the listen only mode, in which
         * no CAN interaction will be done from the controller
         */
        public const Int32 VSCAN_MODE_LISTEN_ONLY = 1;
        /**
         * the self reception mode, in which the device
         * receives also the frames that it sents.
         * The firmware version must be 1.4 or greater
         * and the DLL version 1.6 or greater
         */
        public const Int32 VSCAN_MODE_SELF_RECEPTION = 2;
        /** @} */

        /**
         * @name CAN Speed Constants
         * @{ */
        public const Int32 VSCAN_SPEED_1M = 8;      /**< 1Mb/s */
        public const Int32 VSCAN_SPEED_800K = 7;    /**< 800Kb/s */
        public const Int32 VSCAN_SPEED_500K = 6;    /**< 500Kb/s */
        public const Int32 VSCAN_SPEED_250K = 5;    /**< 250Kb/s */
        public const Int32 VSCAN_SPEED_125K = 4;    /**< 125Kb/s */
        public const Int32 VSCAN_SPEED_100K = 3;    /**< 100Kb/s */
        public const Int32 VSCAN_SPEED_50K = 2;     /**< 50Kb/s */
        public const Int32 VSCAN_SPEED_20K = 1;     /**< 20Kb/s */
        /** @} */

        /**
         * @name CAN IOCTLs
         * @{ */
        public const Int32 VSCAN_IOCTL_SET_DEBUG = 1;           /**< set debug verbosity level */
        public const Int32 VSCAN_IOCTL_GET_HWPARAM = 2;         /**< get hardware parameter */
        public const Int32 VSCAN_IOCTL_SET_SPEED = 3;           /**< set CAN speed */
        public const Int32 VSCAN_IOCTL_SET_BTR = 4;             /**< set CAN bus timing registers */
        public const Int32 VSCAN_IOCTL_GET_FLAGS = 5;           /**< get CAN status flags */
        public const Int32 VSCAN_IOCTL_SET_ACC_CODE_MASK = 6;   /**< setup CAN frame filter */
        public const Int32 VSCAN_IOCTL_SET_TIMESTAMP = 7;       /**< enable/disable timestamp */
        public const Int32 VSCAN_IOCTL_SET_DEBUG_MODE = 8;      /**< set debugging mode */
        public const Int32 VSCAN_IOCTL_SET_BLOCKING_READ = 9;   /**< enable/disable blocking read */
        public const Int32 VSCAN_IOCTL_SET_FILTER_MODE = 10;    /**< set filter mode */
        public const Int32 VSCAN_IOCTL_GET_API_VERSION = 11;    /**< get API version */
        /** @} */

        /**
         * @name CAN Debug Level
         * @{ */
        public const Int32 VSCAN_DEBUG_NONE = 0;    /**< (no debug information */
        public const Int32 VSCAN_DEBUG_LOW = -1;    /**< errors from the VSCAN API */
        public const Int32 VSCAN_DEBUG_MID = -51;   /**< informations from the VSCAN API */
        public const Int32 VSCAN_DEBUG_HIGH = -101; /**< errors from system functions */
        /** @} */

        /**
         * @name CAN Debug Modes
         * @{ */
        public const Int32 VSCAN_DEBUG_MODE_CONSOLE = 1;    /**< set console as debug output */
        public const Int32 VSCAN_DEBUG_MODE_FILE = 2;       /**< set file as debug output */
        /** @} */

        /**
         * @name CAN Device Types
         * @{ */
        public const Int32 VSCAN_HWTYPE_UNKNOWN = 0;    /**< unkbown device */
        public const Int32 VSCAN_HWTYPE_SERIAL = 1;     /**< SER-CAN */
        public const Int32 VSCAN_HWTYPE_USB = 2;        /**< USB-CAN */
        public const Int32 VSCAN_HWTYPE_NET = 3;        /**< NET-CAN */
        public const Int32 VSCAN_HWTYPE_BUS = 4;        /**< PCI-CAN and OnRISC Alena */
        /** @} */

        /**
         * @name CAN IOCTL Mode On/Off
         * @{ */
        public const Int32 VSCAN_IOCTL_OFF = 0; /**< disable option */
        public const Int32 VSCAN_IOCTL_ON = 1;  /**< enable option */
        /** @{ */

        /**
         * @name CAN Timestamp Mode
         * @{ */
        public const Int32 VSCAN_TIMESTAMP_OFF = VSCAN_IOCTL_OFF; /**< turn time stamp off */
        public const Int32 VSCAN_TIMESTAMP_ON = VSCAN_IOCTL_ON;   /**< turn time stamp on */
        /** @} */

        /**
         * @name CAN Filter Mode
         * @{ */
        public const Int32 VSCAN_FILTER_MODE_SINGLE = 1; /**< single filter mode */
        public const Int32 VSCAN_FILTER_MODE_DUAL = 2;   /**< dual filter mode */
        /** @} */

        /**
         * @name CAN Flags
         * @{ */
        public const byte VSCAN_FLAGS_STANDARD = (1 << 0); /**< standard frame */
        public const byte VSCAN_FLAGS_EXTENDED = (1 << 1); /**< extended frame */
        public const byte VSCAN_FLAGS_REMOTE = (1 << 2); /**< remote frame */
        public const byte VSCAN_FLAGS_TIMESTAMP = (1 << 3); /**< time stamp present */
        /** @} */

        /**
         * @name extended status and error flags
         * @{ */
        public const int VSCAN_IOCTL_FLAG_RX_FIFO_FULL  = (1 << 0); /**< RX FIFO full flag */
        public const int VSCAN_IOCTL_FLAG_TX_FIFO_FULL  = (1 << 1); /**< TX FIFO full flag */
        public const int VSCAN_IOCTL_FLAG_ERR_WARNING = (1 << 2);   /**< error warning flag */
        public const int VSCAN_IOCTL_FLAG_DATA_OVERRUN = (1 << 3);  /**< data overrun flag */
        public const int VSCAN_IOCTL_FLAG_UNUSED  = (1 << 4);       /**< unused flag */
        public const int VSCAN_IOCTL_FLAG_ERR_PASSIVE = (1 << 5);   /**< passive error flag */
        public const int VSCAN_IOCTL_FLAG_ARBIT_LOST = (1 << 6);    /**< arbitration lost flag */
        public const int VSCAN_IOCTL_FLAG_BUS_ERROR = (1 << 7);     /**< bus error flag */
        public const int VSCAN_IOCTL_FLAG_API_RX_FIFO_FULL = (1 << 16); /**< API RX FIFO full flag */
        /** @} */

        private Int32 handle = 0;

        [DllImport("vs_can_api", EntryPoint = "VSCAN_Open", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 VSCAN_Open(IntPtr SerialNrORComPortORNet, UInt32 Mode);
        [DllImport("vs_can_api", EntryPoint = "VSCAN_Write", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 VSCAN_Write(Int32 Handle, VSCAN_MSG[] Buf, UInt32 Size, ref UInt32 Written);
        [DllImport("vs_can_api", EntryPoint = "VSCAN_Read", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 VSCAN_Read(Int32 Handle, IntPtr Buf, UInt32 Size, ref UInt32 Read);
        [DllImport("vs_can_api", EntryPoint = "VSCAN_Flush", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 VSCAN_Flush(Int32 Handle);
        [DllImport("vs_can_api", EntryPoint = "VSCAN_Close", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 VSCAN_Close(Int32 Handle);
        [DllImport("vs_can_api", EntryPoint = "VSCAN_Ioctl", CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 VSCAN_Ioctl(Int32 Handle, UInt32 Ioctl, IntPtr Param);
        [DllImport("vs_can_api", EntryPoint = "VSCAN_GetErrorString", CallingConvention = CallingConvention.Cdecl)]
        private static extern void VSCAN_GetErrorString(Int32 Status, StringBuilder String, UInt32 MaxLen);

        /**
         * @name Original API calls
         * @{ */


        ///<summary>open CAN channel</summary>
        ///<param name="SerialNrORComPortORNet">a char pointer with one of the following values:
        ///<para>- serial number of the specific device</para>
        ///<para>- COM port or CAN name where the device is located</para>
        ///<para>- IP address and port number of the device</para>
        /// </param>
        /// <param name="Mode">modes
        /// <para>- #VSCAN_MODE_NORMAL</para>
        /// <para>- #VSCAN_MODE_LISTEN_ONLY</para>
        /// <para>- #VSCAN_MODE_SELF_RECEPTION</para>
        /// </param>

        public void Open(string SerialNrORComPortORNet, UInt32 Mode)
        {
            if (SerialNrORComPortORNet.Length > 0)
            {
                IntPtr Ptr;
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] tmpStr = encoding.GetBytes(SerialNrORComPortORNet);
                byte[] zsStr = new byte[SerialNrORComPortORNet.Length + 1];

                tmpStr.CopyTo(zsStr, 0);
                zsStr[SerialNrORComPortORNet.Length] = 0;
                Ptr = Marshal.AllocHGlobal(Marshal.SizeOf(tmpStr[0]) * (SerialNrORComPortORNet.Length + 1));
                Marshal.Copy(zsStr, 0, Ptr, zsStr.Length);
                handle = VSCAN_Open(Ptr, Mode);
                Marshal.FreeHGlobal(Ptr);
            }
            else
            {
                handle = VSCAN_Open(IntPtr.Zero, Mode);
            }
            if (handle <= 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(handle, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }
        }

        
         ///<summary>close CAN channel</summary> 
         
        public void Close()
        {
            Int32 rc = VSCAN_Close(handle);
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }
        }

        /**
         <summary>get and set special information and commands of the CAN device</summary>
         * <param name="Ioctl">tells the function which of the following ioctl should be called</param> 
         * <param name="Param">a pointer to the data for the ioctls</param> 
         */
        public void Ioctl(UInt32 Ioctl, IntPtr Param)
        {
            Int32 rc = VSCAN_Ioctl(handle, Ioctl, Param);
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }
        }

        /**
         * <summary>send CAN frame(s)
         *
         * <remarks>The frames will be buffered and send out after some time - this time can grow up to one
         * time slice of the scheduler (Windows = ~16ms and Linux = ~10ms).
         * <para></para>If you want to send the frames immediately, you must call the function Flush()</remarks> 
         * </summary>
         *
         * <param name="Buf"> a pointer to one element or an array of the structure VSCAN_MSG</param>
         * <param name="Size"> the number of the array elements in Buf</param>
         * @<param name="Written"> a pointer to a DWORD that will receive the number of frames written</param>
         */
        public void Write(VSCAN_MSG[] Buf, UInt32 Size, ref UInt32 Written)
        {
            Int32 rc = VSCAN_Write(handle, Buf, Size, ref Written);
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }
        }

        /**
         * <summary>brief read CAN frame(s)
         *
         * <remarks>The read mode of this function is set to non-blocking mode per default. This
         * means that Read will return immediately - even when there are no frames at the
         * moment.<para></para>To make the  VSCAN_Read blocking, use the ioctl #VSCAN_IOCTL_SET_BLOCKING_READ
         * then it will return only if frames were received</remarks>
         * </summary>
         *
         * <param name="Buf"> a pointer to one element or an array of the structure VSCAN_MSG</param>
         * <param name="Size"> the number of the array elements in Buf</param>
         * <param name="Read"> Read a pointer to a DWORD that will receive the real number of the frames read</param>
         */
        public void Read(ref VSCAN_MSG[] Buf, UInt32 Size, ref UInt32 Read)
        {
            IntPtr pBuf = Marshal.AllocHGlobal(Marshal.SizeOf(Buf[0]) * (int)Size);
            Marshal.StructureToPtr(Buf[0], pBuf, false);
            Int32 rc = VSCAN_Read(handle, pBuf, Size, ref Read);
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }

            IntPtr pCurrent = pBuf;
            for (int i = 0; i < Read; i++)
            {
                Buf[i] = (VSCAN_MSG)Marshal.PtrToStructure(pCurrent, typeof(VSCAN_MSG));

                //pCurrent = (IntPtr)((int)pCurrent + Marshal.SizeOf(Buf[0]));
            }

	    	Marshal.FreeHGlobal(pBuf);
        }

        /**
         * <summary>send all frames immediately out to the CAN bus</summary>
         */
        public void Flush()
        {
            Int32 rc = VSCAN_Flush(handle);
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }
        }
        /** @} */

        /**
         * <name>IOCTL Wrapper</name>
         * @{ */

        /**
         * <summary>get hardware parameter</summary> 
         * <param name="hw">pointer to VSCAN_HWPARAM structure</param>
         */
        public void GetHwParams(ref VSCAN_HWPARAM hw)
        {
            IntPtr pHw = Marshal.AllocHGlobal(Marshal.SizeOf(hw));
            Marshal.StructureToPtr(hw, pHw, false);
            Int32 rc = VSCAN_Ioctl(handle, VSCAN_IOCTL_GET_HWPARAM, pHw);
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }

            hw = (VSCAN_HWPARAM)Marshal.PtrToStructure(pHw, typeof(VSCAN_HWPARAM));
	    	Marshal.FreeHGlobal(pHw);
        }

        /**
         * <summary>get VSCAN API version</summary>
         * <param name="ApiVer">pointer to VSCAN_API_VERSION structure</param>
         */
        public void GetApiVersion(ref VSCAN_API_VERSION ApiVer)
        {
            IntPtr pApiVer = Marshal.AllocHGlobal(Marshal.SizeOf(ApiVer));
            Marshal.StructureToPtr(ApiVer, pApiVer, false);
            Int32 rc = VSCAN_Ioctl(handle, VSCAN_IOCTL_GET_API_VERSION, pApiVer);
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }

            ApiVer = (VSCAN_API_VERSION)Marshal.PtrToStructure(pApiVer, typeof(VSCAN_API_VERSION));
	    	Marshal.FreeHGlobal(pApiVer);
        }

        /**
         * <summary>get extended status and error flags</summary>  
         * <param name="Flags">Flags pointer to byte</param>
         */
        public void GetFlags(ref byte Flags)
        {
            IntPtr pFlags = Marshal.AllocHGlobal(Marshal.SizeOf(Flags));
            Marshal.StructureToPtr(Flags, pFlags, false);
            Int32 rc = VSCAN_Ioctl(handle, VSCAN_IOCTL_GET_FLAGS, pFlags);
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }

            Flags = (byte)Marshal.PtrToStructure(pFlags, typeof(byte));
	    	Marshal.FreeHGlobal(pFlags);
        }

        /**
         * <summary>set acceptance code and mask</summary>
         * <param name="Mask">pointer to VSCAN_CODE_MASK structure</param>
         */
        public void SetACCCodeMask(VSCAN_CODE_MASK Mask)
        {
            IntPtr pMask = Marshal.AllocHGlobal(Marshal.SizeOf(Mask));
            Marshal.StructureToPtr(Mask, pMask, false);
            Int32 rc = VSCAN_Ioctl(handle, VSCAN_IOCTL_SET_ACC_CODE_MASK, pMask);
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }
	    	Marshal.FreeHGlobal(pMask);
        }

        /**
         * <summary>set bit-timing register</summary>
         * <param name="Btr">pointer to VSCAN_BTR structure</param>
         */
        public void SetBTR(VSCAN_BTR Btr)
        {
            IntPtr pBtr = Marshal.AllocHGlobal(Marshal.SizeOf(Btr));
            Marshal.StructureToPtr(Btr, pBtr, false);
            Int32 rc = VSCAN_Ioctl(handle, VSCAN_IOCTL_SET_BTR, pBtr);
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }
	    	Marshal.FreeHGlobal(pBtr);
        }

        /**
        * <summary>set CAN speed</summary>
        * <param name="Speed">CAN speed</param>
        */
        public void SetSpeed(int Speed)
        {
            Int32 rc = VSCAN_Ioctl(handle, VSCAN_IOCTL_SET_SPEED, new IntPtr((Int32)Speed));
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }
        }

        /**
        * <summary>set filter mode</summary>
        * <param name="Mode">filter mode:
        *  <para>- #VSCAN_FILTER_MODE_SINGLE</para>
        *  <para>- #VSCAN_FILTER_MODE_DUAL</para>
        *  </param>
        */
        public void SetFilterMode(int Mode)
        {
            Int32 rc = VSCAN_Ioctl(handle, VSCAN_IOCTL_SET_FILTER_MODE, new IntPtr((Int32)Mode));
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }
        }

        /**
         * <summary>set time stamp mode</summary>
         * <param name="Mode">time stamp mode:
         *  <para>- #VSCAN_TIMESTAMP_OFF</para>
         *  <para>- #VSCAN_TIMESTAMP_ON</para>
         *  </param>
         */
        public void SetTimestamp(int Mode)
        {
            Int32 rc = VSCAN_Ioctl(handle, VSCAN_IOCTL_SET_TIMESTAMP, new IntPtr((Int32)Mode));
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }
        }

        /**
         * <summary>set blocking read mode</summary>
         * <param name="Mode">blocking read mode:
         *  <para>- #VSCAN_IOCTL_OFF</para>
         *  <para>- #VSCAN_IOCTL_ON</para>
         *  </param>
         */
        public void SetBlockingRead(int Mode)
        {
            Int32 rc = VSCAN_Ioctl(handle, VSCAN_IOCTL_SET_BLOCKING_READ, new IntPtr((Int32)Mode));
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }
        }

        /**
         * <summary>set debugging verbosity level</summary>
         * <param name="Level">verbosity level:
         *  <para>- #VSCAN_DEBUG_NONE</para>
         *  <para>- #VSCAN_DEBUG_LOW</para>
         *  <para>- #VSCAN_DEBUG_MID</para>
         *  <para>- #VSCAN_DEBUG_HIGH</para>
         *  </param>
         */
        public void SetDebug(int Level)
        {
            Int32 rc = VSCAN_Ioctl(handle, VSCAN_IOCTL_SET_DEBUG, new IntPtr((Int32)Level));
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }
        }

        /**
         * <summary>set debug mode</summary>
         * <param name="FileORConsole">FileORConsole debug mode:
         *  <para>- #VSCAN_DEBUG_MODE_CONSOLE</para>
         *  <para>- #VSCAN_DEBUG_MODE_FILE</para>
         *  </param>
         */
        public void SetDebugMode(int FileORConsole)
        {
            Int32 rc = VSCAN_Ioctl(handle, VSCAN_IOCTL_SET_DEBUG_MODE, new IntPtr((Int32)FileORConsole));
            if (rc != 0)
            {
                StringBuilder err_string = new StringBuilder(64);

                VSCAN_GetErrorString(rc, err_string, 63);
                throw new VSCANException(err_string.ToString());
            }
        }
        /** @} */

    }
}
