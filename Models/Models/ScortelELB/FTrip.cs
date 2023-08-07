using ScortelApi.Models.Interfaces;
using ScortelApi.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{

    /// <summary>
    /// Fishing trip structure 
    /// 
    /// Protocol 2.7
    /// </summary>
    public class FTrip : IFTrip
    {
        #region fields
        private string tripnumberstr = "";
        // creation dt         
        private UInt32 crdtl;
        private DateTime crdt;
        // private discardedrecdt        
        private UInt32 drdtl;
        private DateTime drdt;
        // ftname
        private string ftnamel = "";
        #endregion

        #region bit positions
        private static byte bit_pos_autogen = 5;
        private static byte bit_pos_real_tripnumber = 4;
        private static byte bit_pos_data_correction = 2;
        private static byte bit_pos_include_disreccrdt = 1;
        #endregion

        /// <summary>
        /// Used for Code first SQLite
        /// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types)
        /// </summary>
        [Key]
        public long Id { get; set; }

        #region ScortelELBProtocol fields
        /// <summary>
        /// Bit 5 – Status 
        ///•	0 – auto generated record
        ///•	1 – manual generated record
        /// Bit 4 - is Trip number real
        ///•	0 – no
        ///•	1 – yes
        /// Bit 2 – Data correction
        ///•	0 – no correction of data
        ///•	1 – with correction
        /// Bit 1 
        ///•	0 – DiscardedRecCrDT – emptry - exclude
        ///•	1 – DiscardedRecCrDT – with data - include
        /// </summary>
        public byte Content { get; set; }

        /// <summary>
        /// 0 - generated
        /// 1 - verified by ISS
        /// </summary>
        public byte IsTripNumReal { get; set; }

        #region TripNumber vars
        /// <summary>
        /// Trip number from information system - number of page of fishing diary
        /// first byte is length of the field, if length is 0 - there is no data
        /// minimum field size 1 byte, maximum 255
        /// Data is encoded in Unicode
        /// </summary>
        public byte[] TripNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Index("IX_FTripUnq", 1, IsUnique = true)]
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
        #endregion

        #region SFTRec
        /// <summary>
        /// Start fishing trip record
        /// </summary>
        public SFTRec SFTRec { get; set; }
        #endregion

        #region EFTRec
        /// <summary>
        /// End fishing trip record
        /// </summary>
        public EFTRec EFTRec { get; set; }
        #endregion

        #region DIRecsCount
        /// <summary>
        /// Count of DIRecs records
        /// </summary>
        public byte DIRecsCount { get; set; }
        #endregion

        #region DIRecs
        /// <summary>
        /// List of trip days records
        /// </summary>
        public List<DIRec> DIRecs { get; set; }
        //public ICollection<DIRec> DIRecs { get; set; }
        #endregion

        #region CreationDTL
        /// <summary>
        /// Timestamp since 2018.01.01 00:00:00 UTC in seconds
        /// </summary>
        [NotMapped]
        public UInt32 CreationDTL
        {
            get { return crdtl; }
            set
            {
                crdtl = value;
                crdt = THelp.DateTimeFromELBTimestamp((UInt32)value);
            }
        }

        #endregion

        #region DiscardedRecCrDTL
        [NotMapped]
        private UInt32 DisRecCrDTL
        {
            get
            {
                return drdtl;
            }
            set
            {
                drdtl = value;
                drdt = THelp.DateTimeFromELBTimestamp(value);
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// Fishing trip name consists of
        /// SFT timestamp and EFT timestamp
        /// </summary>
        //[Index("IX_FTripUnq", 2, IsUnique = true)]
        public string FTName
        {
            get
            {
                try
                {
                    if (SFTRec != null && EFTRec != null)
                    {
                        if (SFTRec.Pos != null && EFTRec.Pos != null)
                        {
                            ftnamel = THelp.UnixTimeStampToDateTime(SFTRec.Pos.Timestamp).ToString("dd-MM-yy HH:mm:ss") + " - " + THelp.UnixTimeStampToDateTime(EFTRec.Pos.Timestamp).ToString("dd-MM-yy HH:mm:ss");
                        }
                        else
                        {
                            ftnamel = CreationDT.ToString();
                        }
                    }
                    else
                    {
                        ftnamel = CreationDT.ToString();
                    }
                    return ftnamel;
                }
                catch (Exception)
                {
                    ftnamel = this.CreationDT.ToString();
                    return ftnamel;
                }
            }
            set
            {
                ftnamel = value;
            }
        }

        /// <summary>
        /// Fishing trip creation status
        /// </summary>
        public string CreationStatus
        {
            get
            {
                if (THelp.CheckBits(Content, 5))
                {
                    return "ръчно";
                }
                else
                {
                    return "автоматично";
                }
            }
        }

        public int SentStatus { get; set; }

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

        public int CompletionStatus { get; set; }

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

        public DateTime CreationDT
        {
            get
            {
                return crdt;
            }
            set
            {
                crdt = value;
                crdtl = THelp.ELBTimeFormat(value);
            }
        }

        public DateTime DisRecCrDT
        {
            get
            {
                return drdt;
            }
            set
            {
                drdt = value;
                drdtl = THelp.ELBTimeFormat(value);
            }
        }

        public Vessel Ves { get; set; }

        [Required]
        [DefaultValue(0)]
        public byte IsInsp { get; set; }

        /// <summary>
        /// IDP Sensors data
        /// </summary>
        public SensRec Sens { get; set; }


        #region Flux Integration
        /// <summary>
        /// Trup Unq Number for Flux
        /// Can't be used standart Scortel Unq trip number, Flux doesn't allow '_'
        /// </summary>
        public string TNFluxFormat { get; set; }
        #endregion


        #region Content Funcs

        /// <summary>
        /// Set manual generation of fishing trip, Bit 5 = 1
        /// </summary>
        public void SetManualGeneration()
        {
            Content = THelp.SetBits(Content, bit_pos_autogen);
        }

        /// <summary>
        /// Set auto generation of fishing trip, Bit 5 = 0
        /// </summary>
        public void SetAutoGeneration()
        {
            Content = THelp.ResetBits(Content, bit_pos_autogen);
        }

        /// <summary>
        /// Bit 4 = 1
        /// </summary>
        public void SetRealTripNumber()
        {
            Content = THelp.SetBits(Content, bit_pos_real_tripnumber);
        }

        /// <summary>
        /// Bit 4 = 0
        /// </summary>
        public void SetRandomTripNumber()
        {
            Content = THelp.ResetBits(Content, bit_pos_real_tripnumber);
        }

        /// <summary>
        /// Bit 2 = 0
        /// </summary>
        public void SetNoRecDataCorrection()
        {
            Content = THelp.ResetBits(Content, bit_pos_data_correction);
        }

        /// <summary>
        /// Bit 2 = 1
        /// </summary>
        public void SetRecDataCorrection()
        {
            Content = THelp.SetBits(Content, bit_pos_data_correction);
        }

        /// <summary>
        /// Bit 1 = 0
        /// </summary>
        public void ExcludeDisRecCrDTL()
        {
            Content = THelp.ResetBits(Content, bit_pos_include_disreccrdt);
        }

        /// <summary>
        /// Bit 1 = 1
        /// </summary>
        public void IncludeDisRecCrDTL()
        {
            Content = THelp.SetBits(Content, bit_pos_include_disreccrdt);
        }

        #endregion

        /// <summary>
        /// Convert data fields to byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            try
            {
                int inx = 0;
                byte[] resp = new byte[5000];

                // content - 1 byte
                resp[inx] = Content;
                inx += 1;

                // tripnumber  
                Array.Copy(TripNumber, 0, resp, inx, TripNumber.Length);
                inx += TripNumber.Length;

                // SFT
                byte[] sftarr = SFTRec.GetData();
                Array.Copy(sftarr, 0, resp, inx, sftarr.Length);
                inx += sftarr.Length;

                // EFT
                byte[] eftarr = EFTRec.GetData();
                Array.Copy(eftarr, 0, resp, inx, eftarr.Length);
                inx += eftarr.Length;

                // DIRecsCount
                resp[inx] = DIRecsCount;
                inx += 1;

                // DIRecs
                foreach (var di in DIRecs)
                {
                    //TODO da se popravi

                    ////byte[] diarr = di.GetBytes();
                    ////Array.Copy(diarr, 0, resp, inx, diarr.Length);
                    ////inx += diarr.Length;
                }

                // CreationDTL - 4 bytes                 
                byte[] time = new byte[4] { 0, 0, 0, 0 };
                time = BitConverter.GetBytes(CreationDTL);
                Array.Copy(time, 0, resp, inx, time.Length);
                inx += Marshal.SizeOf(CreationDTL);

                // DisRecCrDTL - 4 bytes
                byte[] distime = new byte[4] { 0, 0, 0, 0 };
                if (THelp.CheckBits(Content, bit_pos_include_disreccrdt))
                {
                    distime = BitConverter.GetBytes(DisRecCrDTL);
                    Array.Copy(distime, 0, resp, inx, distime.Length);
                    inx += Marshal.SizeOf(DisRecCrDTL);
                }

                resp[inx] = IsInsp;
                inx++;

                // IDP sensors data
                var sensarr = Sens.GetBytes();
                Array.Copy(sensarr, 0, resp, inx, sensarr.Length);
                inx += sensarr.Length;

                Array.Resize(ref resp, inx);
                return resp;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
