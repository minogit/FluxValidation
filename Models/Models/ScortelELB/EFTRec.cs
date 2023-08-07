using ScortelApi.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// end of fishing trip record
    /// full size - 29???
    /// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types) 
    /// Protocol versio 2.7
    /// </summary>
    public class EFTRec
    {
        private uint estimatedtimestamp;
        private Int64 estimatedtimestamplite;
        private UInt16 portarrivalid;
        private int portarrivalidlite;
        private uint endFactivitiestimestamp;
        private Int64 endFactivitiestimestamplite;

        public enum EFTBitPosition
        {
            EstTime_bit_position = 7,
            PortId_bit_position = 6,
            EFATime_bit_position = 5,
            Pos_bit_position = 4,
            StGen_bit_position = 3
        };

        //private const byte EstTime_bit_position = 7;
        //private const byte PortId_bit_position = 6;
        //private const byte EFATime_bit_position = 5;
        //private const byte Pos_bit_position = 4;
        //private const byte StGen_bit_position = 3;

        [Key]
        public long Id { get; set; }

        /// <summary>
        ///Bit 7 – Estimated time of arrival
        ///•	0 – excluded estimated timestamp field
        ///•	1 – included estimated timestamp field
        ///Bit 6 – Port of arrival Id
        ///•	0 – excluded portId field
        ///•	1 – included portidfield
        ///Bit 5 – End of fishing activities timestamp
        ///•	0 – excluded end timestamp field
        ///•	1 – included end timestamp field
        ///Bit 4 – Position GPS part
        ///•	0 – excluded
        ///•	1 – invluded
        ///Bit 3 – Status generation of record
        ///•	0 – auto generated
        ///•	1 – manual generated
        /// </summary>
        public byte Content { get; set; }

        /// <summary>
        /// Estimated time of arrival timestamp since 2018.01.01 00:00:00 UTC in seconds
        /// </summary>
        [NotMapped]
        public uint EstimatedTimestamp
        {
            get { return estimatedtimestamp; }
            set
            {
                estimatedtimestamp = value;
                estimatedtimestamplite = value;
            }
        }

        /// <summary>
        /// Estimated time of arrival timestamp since 2018.01.01 00:00:00 UTC in seconds
        /// SQLite
        /// </summary>
        public Int64 EstimatedTimestampLite
        {
            get { return estimatedtimestamplite; }
            set
            {
                estimatedtimestamplite = value;
                estimatedtimestamp = (uint)value;
            }
        }

        public DateTime EstimatedTimestampDT { get; set; }

        /// <summary>
        /// Sequence number from list of ports (0-65535)
        /// </summary>
        [NotMapped]
        public UInt16 PortArrivalId
        {
            get { return portarrivalid; }
            set
            {
                portarrivalid = value;
                portarrivalidlite = value;
            }
        }

        /// <summary>
        /// Sequence number from list of ports (0-65535) SQLite
        /// </summary>
        public int PortArrivalIdLite
        {
            get { return portarrivalidlite; }
            set
            {
                portarrivalid = (UInt16)value;
                portarrivalidlite = value;
            }
        }

        /// <summary>
        /// End of fishing activities timestamp since 2018.01.01 00:00:00 UTC in seconds
        /// </summary>
        [NotMapped]
        public uint EndFActivitiesTimestamp
        {
            get { return endFactivitiestimestamp; }
            set
            {
                endFactivitiestimestamp = value;
                endFactivitiestimestamplite = value;
            }
        }

        /// <summary>
        /// End of fishing activities timestamp since 2018.01.01 00:00:00 UTC in seconds
        /// SQLite
        /// </summary>
        public Int64 EndFActivitiesTimestampLite
        {
            get { return endFactivitiestimestamplite; }
            set
            {
                endFactivitiestimestamp = (uint)value;
                endFactivitiestimestamplite = value;
            }
        }

        public DateTime EndFActivitiesTimestampDT { get; set; }

        /// <summary>
        /// Gps position part
        /// </summary>
        public PosRec Pos { get; set; }

        /// <summary>
        /// end fishing trip structure - creation status
        /// </summary>
        //[NotMapped]
        public string CreationStatus
        {
            get
            {
                if (THelp.CheckBits(Content, (byte)EFTBitPosition.StGen_bit_position))
                {
                    return "ръчно";
                }
                else
                {
                    return "автоматично";
                }
            }
        }

        /// <summary>
        /// end fishing trip structure - sent status
        /// </summary>
        public int SentStatus { get; set; }
        //[NotMapped]
        public string SentStatusStr
        {
            get
            {
                switch (SentStatus)
                {
                    case 0:
                        return "не е изпратено";
                    case 1:
                        return "изпратено";
                    case 2:
                        return "";
                    case 3:
                        return "";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// end fishing trip structure - completion status
        /// </summary>
        public int CompletionStatus { get; set; }

        //[NotMapped]
        public string CompletionStatusStr
        {
            get
            {
                switch (CompletionStatus)
                {
                    case 0:
                        return "не е завършено";
                    case 1:
                        return "завършено";
                    case 2:
                        return "";
                    case 3:
                        return "";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// end fishing trip structure - creation date and time
        /// </summary>
        public DateTime CreationDT { get; set; }

        /// <summary>
        /// Trip number from information system - number of page of fishing diary
        /// first byte is length of the field, if length is 0 - there is no data
        /// minimum field size 1 byte, maximum 255
        /// Data is encoded in Unicode
        /// Needed for data recreation at server side
        /// </summary>       
        public byte[] TripNumber { get; set; }

        public string TripNumberStr
        {
            get
            {
                if (TripNumber != null && TripNumber.Length != 1)
                {
                    return Encoding.UTF8.GetString(TripNumber, 1, TripNumber.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                //tripnumberstr = value;
                byte[] tmp = Encoding.UTF8.GetBytes(value);
                if (tmp.Length > 255)
                {
                }
                else
                {
                    byte len = (byte)tmp.Length;
                    byte[] tnum = new byte[len + 1];
                    tnum[0] = len;
                    Array.Copy(tmp, 0, tnum, 1, (int)len);
                    TripNumber = tnum;
                }
            }
        }

        /// <summary>
        /// IDP Sensors data
        /// </summary>
        public SensRec Sens { get; set; }

        #region Content func

        /// <summary>
        /// Content func
        ///Bit 7 – Estimated time of arrival
        ///•	0 – excluded estimated timestamp field
        ///•	1 – included estimated timestamp field
        /// </summary>
        public void ExcludeEstimatedTime()
        {
            Content = THelp.ResetBits(Content, (byte)EFTBitPosition.EstTime_bit_position);
        }

        /// <summary>
        /// Content func - include  economic zone
        ///Bit 7 – Estimated time of arrival
        ///•	0 – excluded estimated timestamp field
        ///•	1 – included estimated timestamp field
        /// </summary>
        public void IncludeEstimatedTime()
        {
            Content = THelp.SetBits(Content, (byte)EFTBitPosition.EstTime_bit_position);
        }

        /// <summary>
        /// Content func 
        ///Bit 6 – Port of arrival Id
        ///•	0 – excluded portId field
        ///•	1 – included portidfield
        /// </summary>
        public void ExcludePortId()
        {
            Content = THelp.ResetBits(Content, (byte)EFTBitPosition.PortId_bit_position);
        }

        /// <summary>
        /// Content func - include port id
        /// </summary>
        public void IncludePortId()
        {
            Content = THelp.SetBits(Content, (byte)EFTBitPosition.PortId_bit_position);
        }

        /// <summary>
        /// Content func - exlude
        ///Bit 5 – End of fishing activities timestamp
        ///•	0 – excluded end timestamp field
        ///•	1 – included end timestamp field
        /// </summary>
        public void ExcludeGPSPos()
        {
            Content = THelp.ResetBits(Content, (byte)EFTBitPosition.Pos_bit_position);
        }

        /// <summary>
        /// Content func - include
        ///Bit 4 – Position GPS part
        ///•	0 – excluded
        ///•	1 – invluded
        /// </summary>
        public void IncludeGPSPos()
        {
            Content = THelp.SetBits(Content, (byte)EFTBitPosition.Pos_bit_position);
        }

        /// <summary>
        /// Content func - auto generated structure
        /// </summary>
        public void SetAutoGenerated()
        {
            Content = THelp.ResetBits(Content, (byte)EFTBitPosition.StGen_bit_position);
        }

        /// <summary>
        /// Content func - manual generated structure
        /// </summary>
        public void SetManualGenerated()
        {
            Content = THelp.SetBits(Content, (byte)EFTBitPosition.StGen_bit_position);
        }

        /// <summary>
        /// Content func
        ///Bit 5 – End of fishing activities timestamp
        ///•	0 – excluded end timestamp field
        ///•	1 – included end timestamp field        
        /// </summary>
        public void ExcludeEndFActivitiesTimestamp()
        {
            Content = THelp.ResetBits(Content, (byte)EFTBitPosition.EFATime_bit_position);
        }

        /// <summary>
        /// Content func
        ///Bit 5 – End of fishing activities timestamp
        ///•	0 – excluded end timestamp field
        ///•	1 – included end timestamp field        
        /// </summary>
        public void IncludeEndFActivitiesTimestamp()
        {
            Content = THelp.SetBits(Content, (byte)EFTBitPosition.EFATime_bit_position);
        }


        #endregion

        /// <summary>
        /// Convert data field to byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            int inx = 0;
            byte[] resp = new byte[29];

            // content - 1 byte
            resp[inx] = Content;
            inx += 1;

            // estimated timestamp - 4 bytes
            byte[] estime = new byte[4] { 0, 0, 0, 0 };
            if (THelp.CheckBits(Content, (byte)EFTBitPosition.EstTime_bit_position))
            {
                estime = BitConverter.GetBytes(EstimatedTimestamp);
                Array.Copy(estime, 0, resp, inx, estime.Length);
                inx += Marshal.SizeOf(EstimatedTimestamp);
            }

            // port of arrival id - 2 bytes
            byte[] portid = new byte[2] { 0, 0 };
            if (THelp.CheckBits(Content, (byte)EFTBitPosition.PortId_bit_position))
            {
                portid = BitConverter.GetBytes(PortArrivalId);
                Array.Copy(portid, 0, resp, inx, portid.Length);
                inx += Marshal.SizeOf(PortArrivalId);
            }

            // end of fishing activities timestamp - 4 bytes
            byte[] efatime = new byte[4] { 0, 0, 0, 0 };
            if (THelp.CheckBits(Content, (byte)EFTBitPosition.EFATime_bit_position))
            {
                efatime = BitConverter.GetBytes(EndFActivitiesTimestamp);
                Array.Copy(efatime, 0, resp, inx, efatime.Length);
                inx += Marshal.SizeOf(EndFActivitiesTimestamp);
            }

            // position gps
            if (THelp.CheckBits(Content, (byte)EFTBitPosition.Pos_bit_position))
            {
                var posarr = Pos.GetData();
                Array.Copy(posarr, 0, resp, inx, posarr.Length);
                inx += posarr.Length;
            }

            // IDP sensors data
            var sensarr = Sens.GetBytes();
            Array.Copy(sensarr, 0, resp, inx, sensarr.Length);
            inx += sensarr.Length;

            Array.Resize(ref resp, inx);

            return resp;
        }

        /// <summary>
        /// Decode EFTRec data
        /// </summary>
        /// <param name="Data"></param>
        public void DecodePacket(byte[] Data)
        {
            try
            {
                int inx = 0;

                // content - 1 byte
                Content = Data[inx];
                inx += Marshal.SizeOf(Content);

                //uint - Estimated timestamp - 4 bytes                
                if (THelp.CheckBits(Content, (byte)EFTBitPosition.EstTime_bit_position))
                {
                    EstimatedTimestamp = BitConverter.ToUInt32(Data, inx);
                    inx += Marshal.SizeOf(EstimatedTimestamp);
                }

                //ushort - Port of arrival id - 2 bytes               
                if (THelp.CheckBits(Content, (byte)EFTBitPosition.PortId_bit_position))
                {
                    PortArrivalId = BitConverter.ToUInt16(Data, inx);
                    inx += Marshal.SizeOf(PortArrivalId);
                }

                //uint - End of fishing activities timestamp - 4 bytes                 
                if (THelp.CheckBits(Content, (byte)EFTBitPosition.EFATime_bit_position))
                {
                    EndFActivitiesTimestamp = BitConverter.ToUInt32(Data, inx);
                    inx += Marshal.SizeOf(EndFActivitiesTimestamp);
                }

                //POSRec  position gps
                if (THelp.CheckBits(Content, (byte)EFTBitPosition.Pos_bit_position))
                {
                    PosRec tmpPRec = new PosRec();
                    byte[] PosArr = new byte[tmpPRec.PosRecSizeOf];
                    // copy pos byte array
                    Array.Copy(Data, inx, PosArr, 0, tmpPRec.PosRecSizeOf);
                    // decode pos data
                    tmpPRec.DecodePacket(PosArr);
                    Pos = tmpPRec;

                    inx += tmpPRec.PosRecSizeOf;
                }

                //TODO: decode sens data

            }
            catch (Exception)
            {
                // TODO: log
            }
        }
    }
}
