using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{ /// <summary>
  /// Packet structure
  /// ------------------------------
  /// Field           |  Size
  /// ------------------------------
  /// STH             |  1
  /// Sequence        |  1
  /// Description     |  1
  /// Data            |  0 – 6.136
  /// CRC             |  2
  /// ETH             |  1
  /// ------------------------------
  /// 0x00, 0x02, 0x03, 0x10 - not in use and must be escaped
  /// </summary>
    public class ELBPacket
    {
        #region Const Frame Description

        public const byte STX = 2;
        public const byte ETX = 3;
        private const byte PreEscapeChar = 0x10;
        private const byte PreEscapeDelta = 0x20;

        public const byte FD_ACKNOWLEDGE = 0x01;

        public const byte FD_TEXT = 0x06;
        public const byte FD_LONG_TEXT = 0x07;
        public const byte FD_FILE = 0x08;
        public const byte FD_TEXT_WITH_GPS = 0x09;
        public const byte FD_LIVE_UPDATE = 0x0E;

        public const byte FD_GET_SOFTWARE_VERSION = 0x11;
        public const byte FD_GET_SHELL_VERSION = 0x12;
        public const byte FD_GET_CREW_INFO = 0x13;
        public const byte FD_GET_VESSEL_INFO = 0x14;
        public const byte FD_GET_CURRENT_FISHING_TRIP = 0x15;
        public const byte FD_GET_CURRENT_FISHING_DAY_INFO = 0x16;

        public const byte FD_SET_CONFIGURATION = 0x1D;
        public const byte FD_GET_CONFIGURATION = 0x1E;

        public const byte FD_SOFTWARE_VERSION_RESP = 0x21;
        public const byte FD_SHELL_VERSION_RESP = 0x22;
        public const byte FD_CREW_INFO_RESP = 0x23;
        public const byte FD_VESSEL_INFO_RESP = 0x24;
        public const byte FD_FISHING_TRIP_RESP = 0x25;
        public const byte FD_FISHING_DAY_INFO_RESP = 0x26;

        public const byte FD_CURRENT_CONFIGURATION = 0x2E;

        public const byte FD_START_FISHING_TRIP = 0x30;
        public const byte FD_END_FISHING_TRIP = 0x31;
        public const byte FD_PORT_LEAVE = 0x32;
        public const byte FD_PORT_ARRIVE = 0x33;
        public const byte FD_START_FISHING_OPERATION = 0x34;
        public const byte FD_END_FISHING_OPERATION = 0x35;
        public const byte FD_FISHIN_OPERATION_DATA = 0x37;
        public const byte FD_24_DAY_REPORT = 0x36;
        public const byte FD_LAND_REP = 0x40;
        public const byte FD_INSRECS_DATA = 0x41;

        public const byte FD_GET_METEO_DATA = 0x50;
        public const byte FD_METEO_DATA_RESP = 0x51;

        public const byte FD_UPDATE_FROM_OISS_PAGENUM = 0x55;

        public const byte FD_GET_DEVICE_UNIVERSAL_TIME = 0x60;
        public const byte FD_DEVICE_UNIVERSAL_TIME_RESP = 0x61;
        public const byte FD_SET_DEVICE_UNIVERSAL_TIME = 0x62;
         
        public const byte FD_CRCERROR = 0x75;

        public const byte FD_IGNORED = 0x76;

        public const byte FD_ERROR = 0x7f;

        #endregion

        //колко е старо, кога е пратено за първи път
        public int wait_sec_old = 0;
        //колко време е чакало след последното пращане
        public int wait_sec_send = 0;

        private byte[] fFrame;
        private byte[] fFramePreEscape;

        public struct ProtoRec
        {
            //   flags  bit 7 - Sent     (0 - No, 1 - Yes) (bits:76543210)
            //                             bit 6 - GPS Valid(0 - No, 1 - Yes)
            //{1} bit 5 - GPRS     (0 - No, 1 - Yes)
            public byte flags;
            //{4} UTC time (seconds since 1, Jan, 1970)
            public Int32 utc;
            //{1}
            public byte lat_deg;
            //{4} latitude (positive value - North, negative - South)
            public float lat_min;
            //{1}
            public byte lon_deg;
            //{4} longitude (positive value - East, negative - West)
            public float lon_min;
            //{2} course (0-359)
            public Int16 course;
            //{2} speed (km/h) {km * 10 - e.g. 56.21 km/h = 5621}
            public Int16 speed;
            //{1} GSM signal level (0-31)
            public byte gsmsig;
            //{1} digital inputs state
            public byte inputs;
            //{2} analog input state
            public Int16 input1;
            //{2} analog input state
            public Int16 input2;
            //{2} analog input state
            public Int16 input3;
            //{1} outputs state
            public byte output;
            //{4} fuel in milliliters (ml)
            public Int32 fuel;
            //{4} engine on minutes
            public Int32 engine;
            //{4} RfID
            public Int32 rfid;
            //{2} 0x0000
            public Int16 reserve;
        }

        public struct TRec_0x09
        {
            //{4} UTC time (seconds since 1, Jan, 1970)
            public Int32 utc;
            //{1}
            public byte lat_deg;
            //{4} latitude (positive value - North, negative - South)
            public float lat_min;
            //{1}
            public byte lon_deg;
            //{4} longitude (positive value - East, negative - West)
            public float lon_min;
        }

        public ELBPacket()
            : base()
        {
            byte[] m = {
                    STX,
                    0,
                    FD_ACKNOWLEDGE,
                    0,
                    0,
                    0,
                    ETX
                };
            fFrame = m;
            //fFramePreEscape = fFrame
            MakeCRC();
        }
        private void RemovePreEscape()
        {
            int i = 0;
            int j = 0;
            int n = fFramePreEscape.Length - 2;
            byte[] buf = new byte[n + 1];
            buf[0] = STX;
            while (i < n)
            {
                i += 1;
                j += 1;
                if (fFramePreEscape[i] == PreEscapeChar)
                {
                    i += 1;
                    buf[j] = (byte)(fFramePreEscape[i] - PreEscapeDelta);
                }
                else
                {
                    buf[j] = fFramePreEscape[i];
                }
            }
            j += 1;
            Array.Resize(ref buf, j + 1);
            buf[j] = ETX;
            fFrame = buf;
        }

        private void SetPreEscape()
        {
            int i = 0;
            int j = 0;
            int n = fFrame.Length - 2;
            byte b = 0;
            byte[] buf = new byte[n * 2 + 1];
            buf[0] = STX;
            while (i < n)
            {
                i += 1;
                j += 1;
                b = fFrame[i];
                if ((b == 0) | (b == STX) | (b == ETX) | (b == PreEscapeChar))
                {
                    buf[j] = PreEscapeChar;
                    j += 1;
                    buf[j] = (byte)(b + PreEscapeDelta);
                }
                else
                {
                    buf[j] = b;
                }
            }
            j += 1;
            Array.Resize(ref buf, j + 1);
            buf[j] = ETX;
            fFramePreEscape = buf;
        }

        private UInt16 CRC16(byte[] buf)
        {
            int[] crc_tbl = {
        0x0, 0x1021, 0x2042, 0x3063, 0x4084, 0x50a5, 0x60c6, 0x70e7, 0x8108,
        0x9129, 0xa14a, 0xb16b, 0xc18c, 0xd1ad, 0xe1ce, 0xf1ef, 0x1231, 0x210,
        0x3273, 0x2252, 0x52b5, 0x4294, 0x72f7, 0x62d6, 0x9339, 0x8318, 0xb37b,
        0xa35a, 0xd3bd, 0xc39c, 0xf3ff, 0xe3de, 0x2462, 0x3443, 0x420,  0x1401,
        0x64e6, 0x74c7, 0x44a4, 0x5485, 0xa56a, 0xb54b, 0x8528, 0x9509, 0xe5ee,
        0xf5cf, 0xc5ac, 0xd58d, 0x3653, 0x2672, 0x1611, 0x630,  0x76d7, 0x66f6,
        0x5695, 0x46b4, 0xb75b, 0xa77a, 0x9719, 0x8738, 0xf7df, 0xe7fe, 0xd79d,
        0xc7bc, 0x48c4, 0x58e5, 0x6886, 0x78a7, 0x840,  0x1861, 0x2802, 0x3823,
        0xc9cc, 0xd9ed, 0xe98e, 0xf9af, 0x8948, 0x9969, 0xa90a, 0xb92b, 0x5af5,
        0x4ad4, 0x7ab7, 0x6a96, 0x1a71, 0xa50,  0x3a33, 0x2a12, 0xdbfd, 0xcbdc,
        0xfbbf, 0xeb9e, 0x9b79, 0x8b58, 0xbb3b, 0xab1a, 0x6ca6, 0x7c87, 0x4ce4,
        0x5cc5, 0x2c22, 0x3c03, 0xc60,  0x1c41, 0xedae, 0xfd8f, 0xcdec, 0xddcd,
        0xad2a, 0xbd0b, 0x8d68, 0x9d49, 0x7e97, 0x6eb6, 0x5ed5, 0x4ef4, 0x3e13,
        0x2e32, 0x1e51, 0xe70,  0xff9f, 0xefbe, 0xdfdd, 0xcffc, 0xbf1b, 0xaf3a,
        0x9f59, 0x8f78, 0x9188, 0x81a9, 0xb1ca, 0xa1eb, 0xd10c, 0xc12d, 0xf14e,
        0xe16f, 0x1080, 0xa1,   0x30c2, 0x20e3, 0x5004, 0x4025, 0x7046, 0x6067,
        0x83b9, 0x9398, 0xa3fb, 0xb3da, 0xc33d, 0xd31c, 0xe37f, 0xf35e, 0x2b1,
        0x1290, 0x22f3, 0x32d2, 0x4235, 0x5214, 0x6277, 0x7256, 0xb5ea, 0xa5cb,
        0x95a8, 0x8589, 0xf56e, 0xe54f, 0xd52c, 0xc50d, 0x34e2, 0x24c3, 0x14a0,
        0x481,  0x7466, 0x6447, 0x5424, 0x4405, 0xa7db, 0xb7fa, 0x8799, 0x97b8,
        0xe75f, 0xf77e, 0xc71d, 0xd73c, 0x26d3, 0x36f2, 0x691,  0x16b0, 0x6657,
        0x7676, 0x4615, 0x5634, 0xd94c, 0xc96d, 0xf90e, 0xe92f, 0x99c8, 0x89e9,
        0xb98a, 0xa9ab, 0x5844, 0x4865, 0x7806, 0x6827, 0x18c0, 0x8e1,  0x3882,
        0x28a3, 0xcb7d, 0xdb5c, 0xeb3f, 0xfb1e, 0x8bf9, 0x9bd8, 0xabbb, 0xbb9a,
        0x4a75, 0x5a54, 0x6a37, 0x7a16, 0xaf1,  0x1ad0, 0x2ab3, 0x3a92, 0xfd2e,
        0xed0f, 0xdd6c, 0xcd4d, 0xbdaa, 0xad8b, 0x9de8, 0x8dc9, 0x7c26, 0x6c07,
        0x5c64, 0x4c45, 0x3ca2, 0x2c83, 0x1ce0, 0xcc1,  0xef1f, 0xff3e, 0xcf5d,
        0xdf7c, 0xaf9b, 0xbfba, 0x8fd9, 0x9ff8, 0x6e17, 0x7e36, 0x4e55, 0x5e74,
        0x2e93, 0x3eb2, 0xed1,  0x1ef0
    };
            int i = 0;
            int crc = 0;
            int j = 0;

            for (i = 0; i < buf.Length; i++)
            {
                j = ((crc >> 8) ^ buf[i]) & 0xff;
                crc = ((crc << 8) ^ crc_tbl[j]);
                crc = crc & 0xffff;
            }

            //'за да няма 0x03 в чексумата, (ETX = 0x03)
            //Dim Lo As Integer = crc And &HFF
            //Dim Hi As Integer = (crc >> 8) And &HFF

            //If Lo = 3 Then Lo = 4
            //If Hi = 3 Then Hi = 4
            //crc = Hi * 256 + Lo

            return Convert.ToUInt16(crc);
        }

        private void MakeCRC()
        {
            UInt16 crc = default(UInt16);
            //buf = Seq + Description + Data
            byte[] buf = new byte[fFrame.Length - 4];
            Array.Copy(fFrame, 1, buf, 0, buf.Length);

            crc = CRC16(buf);
            Buffer.BlockCopy(BitConverter.GetBytes(crc), 0, fFrame, fFrame.Length - 3, 2);
            SetPreEscape();
        }

        public bool CRCisValid()
        {
            byte[] buf = new byte[fFrame.Length - 4];
            Array.Copy(fFrame, 1, buf, 0, buf.Length);
            UInt16 crc = CRC16(buf);
            Int32 fcrc = fFrame[fFrame.Length - 2] * 256 + fFrame[fFrame.Length - 3];
            return fcrc == Convert.ToInt32(crc);
        }

        #region Properties
        public byte[] Frame
        {
            get { return fFrame; }
            set
            {
                fFrame = value;
                SetPreEscape();
            }
        }
        public byte[] FramePreEscape
        {
            get { return fFramePreEscape; }
            set
            {
                fFramePreEscape = value;
                RemovePreEscape();
            }
        }
        public byte Seq
        {
            get { return fFrame[1]; }
            set
            {
                fFrame[1] = value;
                MakeCRC();
            }
        }
        public byte Description
        {
            get { return fFrame[2]; }
            set
            {
                fFrame[2] = value;
                MakeCRC();
            }
        }
        public byte[] Data
        {
            get
            {
                byte[] buf = new byte[fFrame.Length - 6];
                Array.Copy(fFrame, 3, buf, 0, buf.Length);
                return buf;
            }
            set
            {
                int len = value.Length;
                byte[] m = new byte[len + 6];
                m[0] = STX;
                m[1] = fFrame[1];
                m[2] = fFrame[2];
                Array.Copy(value, 0, m, 3, len);
                m[len + 5] = ETX;
                //  fFrame := copy(fFrame,1,3)+value+'CS'+chr(&H03
                fFrame = m;
                MakeCRC();
            }
        }
        #endregion


        #region Packets

        public byte[] PortLeavePacket(byte[] Data)
        {
            try
            {

                ELBPacket ELB = new ELBPacket();
                ELB.Seq = 1;
                ELB.Description = FD_PORT_LEAVE;
                ELB.Data = Data;
                return ELB.Frame;
            }
            catch (Exception)
            {
                //TODO: error log
                return null;
            }
        }

        #endregion

        #region Analizer

        //public void ELBPacketAnalizer(byte[] ELBRawData)
        //{
        //    try
        //    {
        //        ELBPacket eLBPacket = new ELBPacket();
        //        eLBPacket.FramePreEscape = ELBRawData;

        //        switch (eLBPacket.Description)
        //        {
        //            case ELBPacket.FD_ACKNOWLEDGE: // 0x01
        //                break;
        //            case ELBPacket.FD_TEXT: // 0x06
        //                break;
        //            case ELBPacket.FD_LONG_TEXT: // 0x07
        //                break;
        //            case ELBPacket.FD_FILE: // 0x08
        //                break;
        //            case ELBPacket.FD_TEXT_WITH_GPS: // 0x09
        //                break;
        //            case ELBPacket.FD_LIVE_UPDATE: // 0x0E
        //                break;
        //            case ELBPacket.FD_GET_SOFTWARE_VERSION: //0x11
        //                break;
        //            case ELBPacket.fd
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        //TODO: error log
        //    }
        //}

        #endregion
    }
}
