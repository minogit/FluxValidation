using ScortelApi.Models.Interfaces;
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
    /// Fishing operation 
    /// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types)
    /// 
    /// Protocol 2.7
    /// </summary>
    public class FORec : IFORec
    {
        #region fields
        private byte fcrcount;
        private List<FCRec> fclist = new List<FCRec>();
        // creation dt         
        private UInt32 crdtl;
        private DateTime crdt;
        //   discardedrecdt        
        private UInt32 drdtl;
        private DateTime drdt;
        #endregion

        #region bit pos
        public const byte jfrev_bit_pos = 7;
        public const byte isfo_opened_bit_pos = 6;
        public const byte bit_pos_datacorrection = 2;
        public const byte bit_pos_include_disreccrdt = 1;
        #endregion

        /// <summary>
        /// SQLite PK id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        ///Bit 7
        ///•	0 – excluded JFRec(joint fishing record)
        ///•	1 – included JFRec(joint fishing record)
        ///Bit 6 – 25.06.2019 v.1.0.0.8 ->
        ///•	0 – opened
        ///•	1 – closed
        ///Bit 5 
        ///•	0 – excluded EFO(end of fishing operation)
        ///•	1 – included EFO(end of fishing operatin)
        ///Bit 2 – Data correction
        ///•	0 – no correction of data
        ///•	1 – with correction
        ///Bit 1 
        ///•	0 – DiscardedRecCrDT – emptry - exclude
        ///•	1 – DiscardedRecCrDT – with data - include
        /// </summary>
        public byte Content { get; set; } = 0;

        /// <summary>
        /// Start of fishing opeartion
        /// </summary>
        public SEFORec StartFO { get; set; }

        /// <summary>
        /// End of fishing operation
        /// </summary>
        public SEFORec EndFO { get; set; }

        /// <summary>
        /// Joint vessels information
        /// </summary>
        public JFRec JFRec { get; set; }

        /// <summary>
        /// Count of FCRecs records
        /// </summary>
        public byte FCRecsCount
        {
            get { return fcrcount; }
            set
            {
                try
                {
                    fcrcount = (byte)FCRecs.Count;
                }
                catch (Exception)
                {
                    fcrcount = 0;
                }
            }
        }

        /// <summary>
        /// List of fishing catches
        /// </summary>
        public List<FCRec> FCRecs
        {
            get { return fclist; }
            set
            {
                fclist = value;
                fcrcount = (byte)fclist.Count;
            }
        }

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

        /// <summary>
        /// end fishing trip structure - creation date and time
        /// </summary>
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

        /// <summary>
        /// DiscardedRecCrDT
        /// </summary>
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
 
        #region TripNumber
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
        //[Index("IX_DIRecUnq", 1, IsUnique = true)]
        public string TripNumberStr
        {
            get
            {
                if (TripNumber != null)
                {
                    if (TripNumber != null && TripNumber.Length > 1)
                    {
                        return Encoding.UTF8.GetString(TripNumber, 1, TripNumber.Length - 1);
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (value == null)
                {
                    //TripNumber = new byte[] { };
                    TripNumber = System.Array.Empty<byte>();
                }
                else
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
        }
        #endregion

        /// <summary>
        /// IDP Sensors data
        /// </summary>
        public SensRec Sens { get; set; }

        public int? StatusTransISS { get; set; } = 0;
        public int? StatusTransNISS { get; set; } = 0;

        #region Content Funcs

        /// <summary>
        /// Content func, Bit7 = 0
        /// </summary>
        public void IncludeJFRec()
        {
            Content = THelp.SetBits(Content, jfrev_bit_pos);
        }

        /// <summary>
        /// Content func, Bit7 = 1
        /// </summary>
        public void ExcludeJFRec()
        {
            Content = THelp.ResetBits(Content, jfrev_bit_pos);
        }

        /// <summary>
        /// Bit6 = 0
        /// </summary>
        public void OpenFO()
        {
            Content = THelp.ResetBits(Content, isfo_opened_bit_pos);
        }

        /// <summary>
        /// Bit6 = 1
        /// </summary>
        public void CloseFO()
        {
            Content = THelp.SetBits(Content, isfo_opened_bit_pos);
        }

        /// <summary>
        /// Content func, Bit5 = 0
        /// </summary>
        public void IncludeEFO()
        {
            Content = THelp.SetBits(Content, 5);
        }

        /// <summary>
        /// Content func, Bit5 = 1
        /// </summary>
        public void ExcludeEFO()
        {
            Content = THelp.ResetBits(Content, 5);
        }

        /// <summary>
        /// Content func - no correction of data, Bit2 = 0
        /// </summary>
        public void SetNoDataCorrection()
        {
            Content = THelp.ResetBits(Content, bit_pos_datacorrection);
        }

        /// <summary>
        /// Content func - with data correction, Bit2 = 1
        /// </summary>
        public void SetDataCorrection()
        {
            Content = THelp.SetBits(Content, bit_pos_datacorrection);
        }

        /// <summary>
        /// Conten func - exclude DiscardedRecCrDTL, Bit1 = 0
        /// </summary>
        public void ExcludeDisRecCrDTL()
        {
            Content = THelp.ResetBits(Content, bit_pos_include_disreccrdt);
        }

        /// <summary>
        /// Content func - include DiscardedRecCrDTL, Bit1 = 1
        /// </summary>
        public void IncludeDisRecCrDTL()
        {
            Content = THelp.SetBits(Content, bit_pos_include_disreccrdt);
        }

        #endregion

        /// <summary>
        /// Get date in byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            byte[] resp = new byte[2000];
            int inx = 0;

            // Content
            resp[inx] = Content;
            inx += 1;

            // Start of fishing operation
            byte[] sf = StartFO.GetData();
            Array.Copy(sf, 0, resp, inx, sf.Length);
            inx += sf.Length;

            // End of fishing operation
            if (EndFO != null)
            {
                byte[] ef = EndFO.GetData();
                Array.Copy(ef, 0, resp, inx, ef.Length);
                inx += ef.Length;
            }
            else
            {
                ExcludeEFO();
            }
            if (JFRec != null)
            {
                // Joint vessels information
                byte[] jv = JFRec.GetData();
                Array.Copy(jv, 0, resp, inx, jv.Length);
                inx += jv.Length;
            }
            else
            {
                ExcludeJFRec();
            }

            // FCRecs count
            if (FCRecsCount != FCRecs.Count)
            {
                resp[inx] = (byte)FCRecs.Count;
                inx += 1;
            }
            else
            {
                resp[inx] = FCRecsCount;
                inx += 1;
            }

            // fishing catches
            foreach (var rec in FCRecs)
            {
                byte[] fc = rec.GetData();
                Array.Copy(fc, 0, resp, inx, fc.Length);
                inx += fc.Length;
            }

            // CreationDT
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

            #region day rec Trip number               
            Array.Copy(TripNumber, 0, resp, inx, TripNumber.Length);
            inx += TripNumber.Length;
            #endregion

            // IDP sensors data
            var sensarr = Sens.GetBytes();
            Array.Copy(sensarr, 0, resp, inx, sensarr.Length);
            inx += sensarr.Length;

            Array.Resize(ref resp, inx);
            return resp;
        }
    }
}
