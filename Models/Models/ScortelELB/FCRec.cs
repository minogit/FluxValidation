using ScortelApi.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// Fishing catch record
    /// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types) 
    /// Protocol 2.0.0
    /// </summary>
    public class FCRec
    {
        #region fields
        private UInt32 weight;
        private double weightlite;
        private UInt32 discarded;
        private double discardedlite;
        private UInt16 weightcount;
        private int weightcountlite;
        private UInt16 discardedcount;
        private int discardedcountlite;
        // creation dt         
        private UInt32 crdtl;
        private DateTime crdt;
        //   discardedrecdt        
        private UInt32 drdtl;
        private DateTime drdt;
        // presentation
        private byte presentationcode;
        private FPresentation fpresentation;
        // condition
        private byte condition;
        private FCondition fcondition;
        // quantitytype
        private byte qtype;
        private FQuantityType fqtype;
        // discardedtype
        private byte dtype;
        private FDiscardedType fdtype;
        private UInt16? qtcount;
        private int? qtcountlite;
        #endregion


        #region bit pos       
        /// <summary>
        /// adding record at landing declartion time
        /// </summary>
        public const byte bit_pos_ldtime = 7;
        /// <summary>
        /// Set record - priulov
        /// </summary>
        public const byte bit_pos_priulov = 6;
        /// <summary>
        /// Legal fish size
        /// </summary>
        public const byte bit_pos_lms_bms = 5;
        /// <summary>
        /// the record is deleted at client side (fvms-m)
        /// 0 - ok, 1 - deleted
        /// </summary>
        public const byte bit_pos_isrec_Del = 4;
        public const byte bit_pos_discdata = 3;
        public const byte bit_pos_datacorrection = 2;
        public const byte bit_pos_include_disreccrdt = 1;
        #endregion

        #region Properties
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Packet content
        /// Bit 3 – Discarded Data
        ///•	0 – excluded
        ///•	1 – included
        /// Bit 2 – Data Correction
        ///•	0 – no 
        ///•	1 – yes
        /// Bit 1 
        ///•	0 – DiscardedRecCrDT – emptry - exclude
        ///•	1 – DiscardedRecCrDT – with data - include
        /// </summary>
        public byte Content { get; set; }

        #region SRec
        /// <summary>
        /// Species 
        /// </summary>
        public SRec SRec { get; set; }
        #endregion

        #region Presentation
        /// <summary>
        /// Sequence number from list presentations (0-255)
        /// </summary>
        [NotMapped]
        public byte PresentationCode
        {
            get
            {
                return presentationcode;
            }
            set
            {
                presentationcode = value;
            }
        }

        public FPresentation FPresentation
        {
            get { return fpresentation; }
            set
            {
                fpresentation = value;
                try
                {
                    presentationcode = (byte)fpresentation.Id;
                }
                catch (Exception)
                {
                    presentationcode = 0;
                }
            }
        }
        #endregion

        #region Condition
        /// <summary>
        /// Sequence number from list conditions (0-255)
        /// </summary>
        [NotMapped]
        public byte Condition
        {
            get
            {
                return condition;
            }
            set
            {
                condition = value;
            }
        }

        public FCondition FCondition
        {
            get { return fcondition; }
            set
            {
                fcondition = value;
                try
                {
                    condition = (byte)fcondition.Id;
                }
                catch (Exception)
                {
                    condition = 0;
                }
            }
        }
        #endregion

        #region QuantityType
        /// <summary>
        /// Sequence number from list FQuantityType (0-255)
        /// </summary>
        [NotMapped]
        public byte QuantityType
        {
            get
            {
                return qtype;
            }
            set
            {
                qtype = value;
            }
        }

        public FQuantityType FQuantityType
        {
            get
            {
                return fqtype;
            }
            set
            {
                fqtype = value;
                try
                {
                    qtype = (byte)fqtype.Id;
                }
                catch (Exception)
                {
                    qtype = 0;
                }
            }
        }

        #endregion

        #region Discarded
        /// <summary>
        /// Weight or units
        /// </summary>
        [NotMapped]
        public UInt32 Discarded
        {
            get
            {
                return discarded;
            }
            set
            {
                discarded = value;
                discardedlite = value;
            }
        }

        public double DiscardedLite
        {
            get { return discardedlite; }
            set
            {
                discardedlite = value;
                discarded = (UInt32)value;
            }
        }
        #endregion

        #region Discarded Count
        /// <summary>
        /// Discarded units count 
        /// </summary>
        [NotMapped]
        public UInt16 DiscardedCount
        {
            get { return discardedcount; }
            set
            {
                discardedcount = value;
                discardedcountlite = value;
            }
        }

        /// <summary>
        /// Discarded units count SQLite
        /// </summary>
        public int DiscardedCountLite
        {
            get { return discardedcountlite; }
            set
            {
                discardedcountlite = value;
                discardedcount = (UInt16)value;
            }
        }
        #endregion 

        #region Weight
        /// <summary>
        /// Weight in kilograms
        /// </summary>
        [NotMapped]
        public UInt32 Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                weightlite = value;
            }
        }

        /// <summary>
        /// SQLite field 
        /// </summary>
        public double WeightLite
        {
            get { return weightlite; }
            set
            {
                weightlite = value;
                weight = (UInt32)value;
            }
        }
        #endregion

        #region Weight count
        /// <summary>
        /// Fish catch count of units
        /// </summary>
        [NotMapped]
        public UInt16 WeightCount
        {
            get { return weightcount; }
            set
            {
                weightcount = value;
                weightcountlite = value;
            }
        }
        /// <summary>
        /// Fish catch count of units SQLite 
        /// </summary>
        public int WeightCountLite
        {
            get { return weightcountlite; }
            set
            {
                weightcountlite = value;
                weightcount = (UInt16)value;
            }
        }
        #endregion

        #region WeightCalcLite
        /// <summary>
        /// After calculation depending of fish presentation
        /// </summary>
        public double WeightCalcLite { get; set; }
        #endregion

        #region DiscardedType

        [NotMapped]
        public byte DiscardedType
        {
            get
            {
                return dtype;
            }
            set
            {
                dtype = value;
            }
        }

        public FDiscardedType FDiscardedType
        {
            get { return fdtype; }
            set
            {
                fdtype = value;
                try
                {
                    dtype = (byte)fdtype.Id;
                }
                catch (Exception)
                {
                    dtype = 0;
                }
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

        #region SizeGroup
        public byte SizeGroup { get; set; }
        #endregion

        /// <summary>
        /// Containers count
        /// </summary>
        [NotMapped]
        public UInt16? QTCount
        {
            get
            {
                return qtcount;
            }
            set
            {
                qtcount = (UInt16?)value;
                qtcountlite = (int?)value;
            }
        }

        public int? QTCountLight
        {
            get
            {
                return qtcountlite;
            }
            set
            {
                qtcountlite = (int?)value;
                qtcount = (UInt16?)value;
            }
        }

        #endregion

        #region Content Funcs

        /// <summary>
        /// Exclude discarded data, Bit3 = 0
        /// </summary>
        public void ExcludeDiscardedData()
        {
            Content = THelp.ResetBits(Content, bit_pos_discdata);
        }

        /// <summary>
        /// Include discarded data, Bit3 = 1
        /// </summary>
        public void IncludeDiscardedData()
        {
            Content = THelp.SetBits(Content, bit_pos_discdata);
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
        /// Convert fields to byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            byte[] resp = new byte[1000];
            int inx = 0;

            // Content
            resp[inx] = Content;
            inx += 1;

            // Species record
            byte[] sp = SRec.GetData();
            Array.Copy(sp, 0, resp, 0, sp.Length);
            inx += sp.Length;

            // Presentation
            resp[inx] = PresentationCode;
            inx += 1;

            // Condition
            resp[inx] = Condition;
            inx += 1;

            // Storage
            resp[inx] = QuantityType;
            inx += 1;

            if (THelp.CheckBits(Content, bit_pos_discdata))
            {
                //Discarded
                byte[] dc = new byte[4];
                dc = BitConverter.GetBytes(Discarded);
                Array.Copy(dc, 0, resp, inx, dc.Length);
                inx += dc.Length;

                //Discarded count
                byte[] dcc = new byte[2];
                dcc = BitConverter.GetBytes(DiscardedCount);
                Array.Copy(dcc, 0, resp, inx, dcc.Length);
                inx += dcc.Length;

                //Discarded Type - sequence number 0-255
                resp[inx] = dtype;
                inx += 1;
            }

            // Weight
            byte[] wt = new byte[4];
            wt = BitConverter.GetBytes(Weight);
            Array.Copy(wt, 0, resp, inx, wt.Length);
            inx += wt.Length;

            // Weight count
            byte[] wtc = new byte[2];
            wtc = BitConverter.GetBytes(WeightCount);
            Array.Copy(wtc, 0, resp, inx, wtc.Length);
            inx += wtc.Length;

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

            // sizegroup
            resp[inx] = SizeGroup;
            inx++;

            Array.Resize(ref resp, inx);

            return resp;
        }

        /// <summary>
        /// Calculate
        /// </summary>
        /// <returns></returns>
        public double CalcWeight()
        {
            try
            {
                // calculate depend by presentation, species and conversionfactor, condition
                // condition -> fresh
                // species weight * presentaion coefficient => calc weight
                // condition -> frozen
                // calcweight => X
                // condition -> other
                // calcweight = Y
                return 0;
            }
            catch (Exception)
            {
                //TODO: log
                return 0;
            }
        }
    }
}
