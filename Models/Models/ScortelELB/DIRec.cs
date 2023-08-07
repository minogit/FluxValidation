using ScortelApi.Models.Interfaces;
using ScortelApi.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{    

    /// <summary>
    /// Day information
    /// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types) 
    /// 
    /// Protocol 2.7
    /// </summary>
    public class DIRec : IDIRec
    {
        #region Fields
        //private string tripnumberstr = "";
        // discardedrecdt        
        private UInt32 drdtl;
        private DateTime drdt;
        // creation dt         
        private UInt32 crdtl;
        private DateTime crdt;
        // transreccount
        private byte transreccount;
        private List<TransRec> trlist;
        // insreccount
        private byte insreccount;
        private List<InsRec> inslist;
        // forecscount
        private byte forecscount;
        private List<FORec> folist;
        #endregion

        #region Bit pos
        public const byte tripnum_bit_pos = 7;
        public const byte transrec_bit_pos = 6;
        public const byte insp_bit_pos = 5;
        public const byte auto_bit_pos = 4;
        public const byte bit_pos_real_tripnumber = 3;
        public const byte bit_pos_datacorrection = 2;
        public const byte bit_pos_include_disreccrdt = 1;
        #endregion

        #region Properties
        /// <summary>
        /// SQLite PK
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Bit 7
        ///•	0 – excluded Trip number 
        ///•	1 – included Trip number
        /// Bit 6 
        ///•	0 – excluded transbording records
        ///•	1 – included transbording records
        /// Bit 5
        ///•	0 – excluded Inspection record
        ///•	1 – included Inspection record
        /// Bit 4 – Status 
        ///•	0 – auto generated record
        ///•	1 – manual generated record
        /// Bit 3 - is Trip number real
        ///•	0 – no
        ///•	1 – yes
        ///Bit 2 – Data Correction
        ///•	0 – no 
        ///•	1 – yes
        ///Bit 1 
        ///•	0 – DiscardedRecCrDT – emptry - exclude
        ///•	1 – DiscardedRecCrDT – with data - include
        /// </summary>
        public byte Content { get; set; }

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

        #region CreationDT
        //[Index("IX_DIRecUnq", 2, IsUnique = true)]
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

        #endregion

        #region DisRecCrDT
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

        #region TransRecCount
        public byte TransRecCount
        {
            get { return transreccount; }
            set
            {
                transreccount = value;
            }
        }
        #endregion

        #region TransRecs
        /// <summary>
        /// 
        /// </summary>         
        public List<TransRec> TransRecs
        {
            get { return trlist; }
            set
            {
                trlist = value;
                transreccount = (byte)trlist.Count;
            }
        }
        #endregion

        #region InsRecCount
        public byte InsRecCount
        {
            get
            {
                return insreccount;
            }
            set
            {
                insreccount = value;
            }
        }
        #endregion

        #region InsRecs
        /// <summary>
        /// Inspection data record
        /// </summary>
        public List<InsRec> InsRecs
        {
            get { return inslist; }
            set
            {
                inslist = value;
                insreccount = (byte)inslist.Count;
            }
        }

        //public InsRec InsRec { get; set; }

        #endregion

        #region FORecsCount 
        public byte FORecsCount
        {
            get { return forecscount; }
            set { forecscount = value; }
        }
        #endregion

        #region FORecs
        /// <summary>
        /// List of day fishing opearations
        /// </summary>
        public List<FORec> FORecs
        {
            get { return folist; }
            set
            {
                folist = value;
                forecscount = (byte)folist.Count;
            }
        }
        #endregion


        /// <summary>
        /// Name of direc - datetime.now -> to string
        /// </summary>
        public string DIRecName { get; set; }

        /// <summary>
        /// Fishing trip creation status
        /// </summary>
        public string CreationStatus
        {
            get
            {
                if (THelp.CheckBits(Content, 4))
                {
                    return "manual";
                }
                else
                {
                    return "auto";
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

        ///// <summary>
        ///// Need coordinates to direc structure to allow visualization in Wialon
        ///// </summary>
        ////[NotMapped]
        //public int Lat { get; set; }

        ///// <summary>
        ///// Need coordinates to direc structure to allow visualization in Wialon
        ///// </summary>
        ////[NotMapped]
        //public int Lng { get; set; }

        public PosRec Pos { get; set; }

        /// <summary>
        /// IDP Sensors data
        /// </summary>
        public SensRec Sens { get; set; }

        /// <summary>
        /// Status is sent to ISS
        /// </summary>
        public int? StatusTransISS { get; set; } = 0;
        /// <summary>
        /// Status is sent to NISS
        /// </summary>
        public int? StatusTransNISS { get; set; } = 0;

        #endregion

        #region Content Func
        /// <summary>
        /// Content func, Bit7 = 1
        /// </summary>
        public void IncludeTripNuber()
        {
            Content = THelp.SetBits(Content, transrec_bit_pos);
        }

        /// <summary>
        /// Content func, Bit7 = 0
        /// </summary>
        public void ExcludeTripNumber()
        {
            Content = THelp.ResetBits(Content, tripnum_bit_pos);
        }

        /// <summary>
        /// Content func, Bit6 = 1
        /// </summary>
        public void IncludeTransbordingRec()
        {
            Content = THelp.SetBits(Content, transrec_bit_pos);
        }

        /// <summary>
        /// Content func, Bit6 = 0
        /// </summary>
        public void ExcludedTransboardingRec()
        {
            Content = THelp.ResetBits(Content, transrec_bit_pos);
        }

        /// <summary>
        /// Content func, Bit5 = 1
        /// </summary>
        public void IncludeInspRec()
        {
            Content = THelp.SetBits(Content, insp_bit_pos);
        }

        /// <summary>
        /// Content func, Bit5 = 0
        /// </summary>
        public void ExcludedInspRec()
        {
            Content = THelp.ResetBits(Content, insp_bit_pos);
        }

        /// <summary>
        /// Content func, Bit4 = 1
        /// </summary>
        public void SetManualGenerated()
        {
            Content = THelp.SetBits(Content, auto_bit_pos);
        }

        /// <summary>
        /// Content func, Bit4 = 0
        /// </summary>
        public void SetAutoGenerated()
        {
            Content = THelp.ResetBits(Content, auto_bit_pos);
        }

        /// <summary>
        /// Bit3 = 1
        /// </summary>
        public void SetRealTripNumber()
        {
            Content = THelp.SetBits(Content, bit_pos_real_tripnumber);
        }

        /// <summary>
        /// Bit3 = 0 
        /// </summary>
        public void SetRandomTripNumber()
        {
            Content = THelp.ResetBits(Content, bit_pos_real_tripnumber);
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

        public byte[] GetBytes(PosRec pos)
        {
            try
            {
                byte[] resp = new byte[5000];
                int inx = 0;

                if (pos != null)
                {
                    var posdata = pos.GetData();
                    Array.Copy(posdata, 0, resp, 0, posdata.Length);
                    inx += posdata.Length;
                }

                ////// Content
                ////resp[inx] = Content;
                ////inx += 1;

                ////if (THelp.CheckBits(Content, tripnum_bit_pos))
                ////{
                ////    // Trip number
                ////    Array.Copy(TripNumber, 0, resp, inx, TripNumber.Length);
                ////    inx += TripNumber.Length;
                ////}

                ////// CreationDT - 4 bytes          
                ////byte[] time = BitConverter.GetBytes(CreationDTL);
                ////Array.Copy(time, 0, resp, inx, time.Length);
                ////inx += Marshal.SizeOf(CreationDTL);

                ////// DisRecCrDTL - 4 bytes         
                ////if (THelp.CheckBits(Content, bit_pos_include_disreccrdt))
                ////{
                ////    byte[] distime = BitConverter.GetBytes(DisRecCrDTL);
                ////    Array.Copy(distime, 0, resp, inx, distime.Length);
                ////    inx += Marshal.SizeOf(DisRecCrDTL);
                ////}

                ////if (THelp.CheckBits(Content, transrec_bit_pos))
                ////{
                ////    // TransrecsCount
                ////    if (TransRecCount != TransRecs.Count)
                ////    {
                ////        resp[inx] = (byte)TransRecs.Count;
                ////    }
                ////    else
                ////    {
                ////        resp[inx] = TransRecCount;
                ////        inx += 1;
                ////    }

                ////    // Transboarding records
                ////    foreach (var trec in TransRecs)
                ////    {
                ////        byte[] tr = trec.GetData();
                ////        Array.Copy(tr, 0, resp, inx, tr.Length);
                ////        inx += tr.Length;
                ////    }
                ////}

                ////if (THelp.CheckBits(Content, insp_bit_pos))
                ////{
                ////    // InsRecsCount
                ////    if (InsRecCount != InsRecs.Count)
                ////    {
                ////        resp[inx] = (byte)InsRecs.Count;
                ////    }
                ////    else
                ////    {
                ////        resp[inx] = InsRecCount;
                ////    }

                ////    // InsRecs
                ////    foreach (var ins in InsRecs)
                ////    {
                ////        byte[] insarr = ins.GetData();
                ////        Array.Copy(insarr, 0, resp, inx, insarr.Length);
                ////        inx += insarr.Length;
                ////    }
                ////}

                ////// FO Count
                ////if (FORecsCount != FORecs.Count)
                ////{
                ////    resp[inx] = (byte)FORecs.Count;
                ////}
                ////else
                ////{
                ////    resp[inx] = FORecsCount;
                ////}

                ////// Fishing operations
                ////foreach (var fo in FORecs)
                ////{
                ////    //using (var context = new DatabaseContext())
                ////    //{
                ////    //    var fotmp = context.FORecs.FirstOrDefault(x => x.Id == fo.Id);
                ////    //    fotmp.GetData();

                ////        byte[] fr = fo.GetData();
                ////        Array.Copy(fr, 0, resp, inx, fr.Length);
                ////        inx += fr.Length;
                ////    //}                    
                ////}



                Array.Resize(ref resp, inx);
                return resp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
