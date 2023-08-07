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

    #region ELB Protocol 2.0.0

    /// <summary>
    /// Fishing gear record
    /// size - 14 bytes
    /// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types) 
    /// </summary>
    public class FGRec
    {
        #region Fields

        /// <summary>
        /// fgear  -seq FDescription - OTM
        /// </summary>
        private byte fgear;
        private FGDescription fgdesc;
        // eye
        private UInt16 eye;
        private int eyelite;
        private double eyedouble;
        /// <summary>
        /// FLUX gear characteristic: GM - Gear dimension by length or width of the gear in meters
        /// (length of beams, trawl - permimeter of opening, seine nets - overall length,
        /// purse seine - length, puse seine - one boar operated - length, width of dredges, gill nets - length)
        /// </summary>
        private UInt16? len;
        private int? lenlite;
        private decimal lendecimal;

        /// <summary>
        /// high FLUX gear characteristic: HE - Height
        /// </summary>
        private UInt16? high;
        private int? highlite;
        private decimal hightdecimal;

        // trademark
        private UInt16 trade;
        //private int tradelite;
        private FGTrademark fgtrademark;
        // model
        private UInt16 model;
        //private int modellite;
        private FGModel fgmodel;
        /// <summary>
        /// Quantity   
        /// </summary>
        private UInt16 count;
        private int countlite;

        /// <summary>
        /// count - NumericCount
        /// FLUX gear characteristic: GN - Gear dimension by number
        /// (number of trawls, beams, dredges, pots, hooks)
        /// </summary>
        private UInt16? numericcount;
        private int? numericcountlite;

        /// <summary>
        /// FLUX gear characteristic: NN - Nuber of nets in the fleet
        /// </summary>
        private UInt16? netscountinfleet;
        private int? netscountinfleetlite;
        /// <summary>
        /// FLUX gear characteristic: NI - Number of lines
        /// </summary>
        private UInt16? linescount;
        private int? linescountlite;
        /// <summary>
        /// FLUX gear characteristic: NL - Nominal length of one net in a fleet
        /// </summary>
        private UInt16 netnominallength;
        private decimal netnominallengthlite;
        private string trawlmodel;

        #region 3.5 Protocol ELB
        ///// <summary>
        ///// FLUX gear characteristic: NN - Nuber of nets in the fleet
        ///// </summary>
        //private int netscountinfleet { get; set; }

        ///// <summary>
        ///// FLUX gear characteristic: MT - Model of trawl
        ///// (eg. side: OTB-1, OTM-1; stern: OTB-2, OTM-2)
        ///// </summary>
        //public string TrawlModel { get; set; }
        ///// <summary>
        ///// FLUX gear characteristic: NI - Number of lines
        ///// </summary>
        //public int? LinesCount { get; set; }

        ///// <summary>
        ///// FLUX gear characteristic: NL - Nominal length of one net in a fleet
        ///// </summary>
        //public decimal NetNominalLength { get; set; }

        ///// <summary>
        ///// FLUX gear characteristic: GN - Gear dimension by number
        ///// (number of trawls, beams, dredges, pots, hooks)
        ///// </summary>
        //public int? NumericCount { get; set; }
        #endregion

        // creation dt         
        private UInt32 crdtl;
        private DateTime crdt;
        // discardedrecdt        
        private UInt32 drdtl;
        private DateTime drdt;
        #endregion

        #region 4.0 Protocol
        // private string note
        // private string marks
        /// <summary>
        /// FLUX gear characteristic: MT - Model of trawl
        /// (eg. side: OTB-1, OTM-1; stern: OTB-2, OTM-2)
        /// </summary>
        // private string TrawlModel
        /// <summary>
        /// FLUX gear characteristic: NN - Nuber of nets in the fleet
        /// </summary>
        // private int NetsCountInFleet
        /// <summary>
        /// FLUX gear characteristic: NI - Number of lines
        /// </summary>
        // private int LinesCount
        /// <summary>
        /// FLUX gear characteristic: NL - Nominal length of one net in a fleet
        /// </summary>
        // private int NetNominalLength
        /// <summary>
        /// ISS FGear quantity
        /// </summary>
        // private int quantity
        #endregion

        #region Bit pos 
        public const byte bit_pos_eye = 7;
        public const byte bit_pos_trademark = 6;
        public const byte bit_pos_model = 5;
        public const byte bit_pos_count = 4;
        public const byte bit_pos_datacorrection = 2;
        public const byte bit_pos_include_disreccrdt = 1;
        #endregion

        #region Properties

        //public virtual ICollection<SEFORec> SEFORecs { get; set; }
        //public virtual ICollection<FGRecSEFORec> FGRecSEFORecs { get; set; }
        public virtual ICollection<FGRecSEFORec> FGRecSEFORecs { get; set; }

        //public virtual ICollection<SFTRec> SFTRecs { get; set; }
        public virtual ICollection<SFTRecFGRec> SFTRecFGRecs { get; set; }

        //public virtual ICollection<FGRecCert> FGRecCerts { get; set; }
 
        /// <summary>
        /// SQLite PK
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        ///Bit 7
        ///•	0 – excluded gear eye field
        ///•	1 – included gear eye field
        ///Bit 6
        ///•	0 – excluded length field
        ///•	1 – included length field
        ///Bit 5
        ///•	0 – excluded height field
        ///•	1 – included height field
        ///Bit 4 
        ///•	0 – excluded trademark field
        ///•	1 – included trademark field
        ///Bit 3
        ///•	0 – excluded model field
        ///•	1 – included model field
        ///Bit 2 
        ///•	0 – excluded count field
        ///•	1 – included count field
        /// </summary>
        public byte Content { get; set; }

        #region Fishing Gear
        /// <summary>
        /// Sequence number from list fishing gears (0-255)
        /// </summary>
        //[Index("IX_FG_Unq", 1, IsUnique = true)]
        public byte FishingGear
        {
            get { return fgear; }
            set
            {
                fgear = value;
            }
        }
 
        public FGDescription FGDesc
        {
            get { return fgdesc; }
            set
            {
                fgdesc = value;
                fgear = (byte)fgdesc.Seq;
            }
        }

        #endregion

        /// <summary>
        /// TrawlModel
        /// </summary>
        public string TrawlModel
        {
            get
            {
                return trawlmodel;
            }
            set
            {
                trawlmodel = value;
            }
        }

        /// <summary>
        /// count - NumericCount
        /// FLUX gear characteristic: GN - Gear dimension by number
        /// (number of trawls, beams, dredges, pots, hooks)
        /// </summary>
        [NotMapped]
        public UInt16? NumericCount
        {
            get
            {
                return numericcount;
            }
            set
            {
                numericcount = value;
                numericcountlite = value;
            }
        }
        /// <summary>
        /// count - NumericCount
        /// FLUX gear characteristic: GN - Gear dimension by number
        /// (number of trawls, beams, dredges, pots, hooks)
        /// </summary>
        public int? NumericCountLite
        {
            get
            {
                return numericcountlite;
            }
            set
            {
                numericcountlite = value;
                try
                {
                    numericcount = (UInt16)value;
                }
                catch (Exception)
                {
                    numericcount = 0;
                }
                
            }
        }

        /// <summary>
        /// FLUX gear characteristic: NL - Nominal length of one net in a fleet
        /// </summary>
        public decimal NetNominalLengthLite
        {
            get
            {
                return netnominallengthlite;
            }
            set
            {
                netnominallengthlite = value;

            }
        }

        /// <summary>
        /// FLUX gear characteristic: NI - Number of lines
        /// </summary>
        [NotMapped]
        public UInt16? LinesCount
        {
            get
            {
                return linescount;
            }
            set
            {
                linescount = value;
                linescountlite = value;
            }
        }
        /// <summary>
        /// FLUX gear characteristic: NI - Number of lines
        /// </summary>
        public int? LinesCountLite
        {
            get
            {
                return linescountlite;
            }
            set
            {
                linescountlite = value;
                try
                {
                    linescount = (UInt16)value;
                }
                catch (Exception)
                {
                    linescount = 0;
                }                
            }
        }

        /// <summary>
        /// FLUX gear characteristic: NN - Nuber of nets in the fleet
        /// </summary>
        [NotMapped]
        public UInt16? NetsCountInFleet
        {
            get
            {
                return netscountinfleet;
            }
            set
            {
                netscountinfleet = value;
                netscountinfleetlite = value;
            }
        }
        /// <summary>
        /// FLUX gear characteristic: NN - Nuber of nets in the fleet
        /// </summary>
        public int? NetsCountInFleetLite
        {
            get
            {
                return netscountinfleetlite;
            }
            set
            {
                netscountinfleetlite = value;
                try
                {
                    netscountinfleet = (UInt16)value;
                }
                catch (Exception)
                {
                    netscountinfleet = 0;
                }                
            }
        }

        /// <summary>
        /// Fishing gear eye
        /// </summary>
        [NotMapped]
        public UInt16 GearEye
        {
            get
            {
                return eye;
            }
            set
            {
                eye = value;
                eyelite = value;
            }
        }

        /// <summary>
        /// Fishing gear eye SQLite
        /// </summary>
        //[Index("IX_FG_Unq", 2, IsUnique = true)]
        public int GearEyeLite
        {
            get { return eyelite; }
            set
            {
                eyelite = value;
                eye = (UInt16)value;
            }
        }

        public double GearEyeDouble
        {
            get { return eyedouble; }
            set { eyedouble = value; }
        }


        /// <summary>
        /// Fishing gear length
        /// </summary>
        [NotMapped]
        public UInt16? GearLength
        {
            get {
                return len;
            }
            set
            {
                len = value;
                lenlite = value;
            }
        }

        /// <summary>
        /// Fishing gear length SQLite
        /// </summary>
        //[Index("IX_FG_Unq", 3, IsUnique = true)]
        public int? GearLengthLite
        {
            get {
                return lenlite;
            }
            set
            {
                lenlite = value;
                try
                {
                    len = (UInt16)value;
                }
                catch (Exception)
                {
                    len = 0;
                }                
            }
        }

        public decimal GearLengthDecimal
        {
            get
            {
                return lendecimal;
            }
            set
            {
                lendecimal = value;
            }
        }

        /// <summary>
        /// Fishing gear height
        /// </summary>
        [NotMapped]
        public UInt16? GearHeight
        {
            get {
                return high; 
            }
            set
            {
                high = value;
                highlite = value;
            }
        }

        /// <summary>
        /// Fishing gear heaight
        /// </summary>
        //[Index("IX_FG_Unq", 4, IsUnique = true)]
        public int? GearHeightLite
        {
            get { 
                return highlite;
            }
            set
            {
                highlite = value;
                try
                {
                    high = (UInt16)value;
                }
                catch (Exception)
                {
                    high = 0;
                }                
            }
        }

        public decimal GearHeightDecimal
        {
            get
            {
                return hightdecimal;
            }
            set
            {
                hightdecimal = value;
            }
        }


        /// <summary>
        /// Fishing gear trademark
        /// </summary>
        [NotMapped]
        public UInt16 Trademark
        {
            get { return trade; }
            set
            {
                trade = value;
                //tradelite = value;
            }
        }

        //[Index("IX_FG_Unq", 5, IsUnique = true)]
        public FGTrademark FGTrademark
        {
            get { return fgtrademark; }
            set
            {
                fgtrademark = value;
                Trademark = (UInt16)fgtrademark.Id;
            }
        }

        /// <summary>
        /// Fishing gear model
        /// </summary>
        [NotMapped]
        public UInt16 Model
        {
            get { return model; }
            set
            {
                model = value;
                //modellite = value;
            }
        }

        //[Index("IX_FG_Unq", 6, IsUnique = true)]
        public FGModel FGModel
        {
            get { return fgmodel; }
            set
            {
                fgmodel = value;
                model = (UInt16)fgmodel.Id;
            }
        }

        /// <summary>
        /// Fishing gear units count
        /// </summary>
        [NotMapped]
        public UInt16 Count
        {
            get { return count; }
            set
            {
                count = value;
                countlite = value;
            }
        }

        /// <summary>
        /// Fishing gear units count SQLite
        /// </summary>
        public int CountLite
        {
            get { return countlite; }
            set
            {
                countlite = value;
                count = (UInt16)value;
            }
        }

        public byte Favorite { get; set; }

        #region CreationDT
        //[Index("IX_DIRecUnq", 1, IsUnique = true)]
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

        #region Content Funcs

        ///Bit 7
        ///•	0 – excluded gear eye field
        ///•	1 – included gear eye field
        public void IncludeFGearEye()
        {
            Content = THelp.SetBits(Content, bit_pos_eye);
        }

        ///Bit 7
        ///•	0 – excluded gear eye field
        ///•	1 – included gear eye field
        public void ExcludeFGearEye()
        {
            Content = THelp.ResetBits(Content, bit_pos_eye);
        }

        #region commented
        /////Bit 6
        /////•	0 – excluded length field
        /////•	1 – included length field
        //public void IncludeFGearLenfth()
        //{
        //    Content = THelp.SetBits(Content, 6);
        //}

        /////Bit 6
        /////•	0 – excluded length field
        /////•	1 – included length field
        //public void ExcludeFGearLength()
        //{
        //    Content = THelp.ResetBits(Content, 6);
        //}

        /////Bit 5
        /////•	0 – excluded height field
        /////•	1 – included height field
        //public void IncludeFGearHeight()
        //{
        //    Content = THelp.SetBits(Content, 5);
        //}

        /////Bit 5
        /////•	0 – excluded height field
        /////•	1 – included height field
        //public void ExcludeFGearHeight()
        //{
        //    Content = THelp.ResetBits(Content, 5);
        //}
        #endregion

        ///Bit 6 
        ///•	0 – excluded trademark field
        ///•	1 – included trademark field
        public void IncludeFGearTrademark()
        {
            Content = THelp.SetBits(Content, bit_pos_trademark);
        }

        ///Bit 6 
        ///•	0 – excluded trademark field
        ///•	1 – included trademark field
        public void ExcludeFGearTrademark()
        {
            Content = THelp.ResetBits(Content, bit_pos_trademark);
        }

        ///Bit 5
        ///•	0 – excluded model field
        ///•	1 – included model field
        public void IncludeFGearModel()
        {
            Content = THelp.SetBits(Content, bit_pos_model);
        }

        ///Bit 5
        ///•	0 – excluded model field
        ///•	1 – included model field
        public void ExcludeFGearModel()
        {
            Content = THelp.ResetBits(Content, bit_pos_model);
        }

        ///Bit 4 
        ///•	0 – excluded count field
        ///•	1 – included count field
        public void IncludeFGearCount()
        {
            Content = THelp.SetBits(Content, bit_pos_count);
        }

        ///Bit 4 
        ///•	0 – excluded count field
        ///•	1 – included count field
        public void ExcludeFGearCount()
        {
            Content = THelp.ResetBits(Content, bit_pos_count);
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

        public byte FGRecSizOf
        {
            get
            {
                // 1 byte
                var size = Marshal.SizeOf(Content);
                // 1 byte
                size += Marshal.SizeOf(FishingGear);
                // 2 bytes
                size += Marshal.SizeOf(GearEye);
                // 2 bytes
                size += Marshal.SizeOf(GearLength);
                // 2 bytes
                size += Marshal.SizeOf(GearHeight);
                // 2 bytes
                size += Marshal.SizeOf(Trademark);
                // 2 bytes
                size += Marshal.SizeOf(Model);
                // 2 bytes
                size += Marshal.SizeOf(Count);
                return (byte)size;
            }
        }
        #endregion

        /// <summary>
        /// Convert fields to byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            int inx = 0;
            byte[] resp = new byte[500];

            // content - 1 byte
            resp[inx] = Content;
            inx += 1;

            // fishing gear - 1 byte
            resp[inx] = FishingGear;
            inx += 1;

            // fishing eye - 2 bytes
            byte[] feye = new byte[2] { 0, 0 };
            if (THelp.CheckBits(Content, 7))
            {
                feye = BitConverter.GetBytes(GearEye);
                Array.Copy(feye, 0, resp, inx, feye.Length);
                inx += Marshal.SizeOf(GearEye);
            }

            // fishing length - 2 bytes
            byte[] flen = new byte[2] { 0, 0 };
            if (THelp.CheckBits(Content, 6))
            {
                ushort tmpgearlen = 0;
                try
                {
                    if (GearLength != null)
                    {
                        tmpgearlen = (ushort)GearLength;
                    }
                    else
                    {
                        tmpgearlen = 0;
                    }
                }
                catch (Exception)
                {
                    tmpgearlen = 0;
                }
                 
                flen = BitConverter.GetBytes(tmpgearlen);
                Array.Copy(flen, 0, resp, inx, flen.Length);
                inx += Marshal.SizeOf(GearLength);
            }

            // fishing height - 2 bytes
            byte[] fhigh = new byte[2] { 0, 0 };
            if (THelp.CheckBits(Content, 5))
            {
                ushort tmpheight = 0;
                try
                {
                    if (GearHeight != null)
                    {
                        tmpheight = (ushort)GearHeight;
                    }
                    else
                    {
                        tmpheight = 0;
                    }
                }
                catch (Exception)
                {
                    tmpheight = 0;
                }

                fhigh = BitConverter.GetBytes(tmpheight);
                Array.Copy(fhigh, 0, resp, inx, fhigh.Length);
                inx += Marshal.SizeOf(GearHeight);
            }

            // fishing trademark - 2 bytes
            byte[] ftrade = new byte[2] { 0, 0 };
            if (THelp.CheckBits(Content, 4))
            {
                ftrade = BitConverter.GetBytes(Trademark);
                Array.Copy(ftrade, 0, resp, inx, ftrade.Length);
                inx += Marshal.SizeOf(Trademark);
            }

            // fishing model - 2 bytes
            byte[] fmodel = new byte[2] { 0, 0 };
            if (THelp.CheckBits(Content, 3))
            {
                fmodel = BitConverter.GetBytes(Model);
                Array.Copy(fmodel, 0, resp, inx, fmodel.Length);
                inx += Marshal.SizeOf(Model);
            }

            // fishing gear count - 2 bytes
            byte[] fcount = new byte[2] { 0, 0 };
            if (THelp.CheckBits(Content, 2))
            {
                fcount = BitConverter.GetBytes(Count);
                Array.Copy(fcount, 0, resp, inx, fcount.Length);
                inx += Marshal.SizeOf(Count);
            }

            Array.Resize(ref resp, inx);

            return resp;
        }

        /// <summary>
        /// Decode FGrec data - 14 bytes
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

                // 1 byte - fishing gear - sequence number from fg list
                FishingGear = Data[inx];
                inx += Marshal.SizeOf(FishingGear);

                //ushort - fishing eye - 2 bytes                
                if (THelp.CheckBits(Content, 7))
                {
                    GearEye = BitConverter.ToUInt16(Data, inx);
                    inx += Marshal.SizeOf(GearEye);
                }

                //ushort fishing length - 2 bytes                 
                if (THelp.CheckBits(Content, 6))
                {
                    GearLength = BitConverter.ToUInt16(Data, inx);
                    inx += Marshal.SizeOf(GearLength);
                }

                //ushort - fishing height - 2 bytes                 
                if (THelp.CheckBits(Content, 5))
                {
                    GearHeight = BitConverter.ToUInt16(Data, inx);
                    inx += Marshal.SizeOf(GearHeight);
                }

                //ushort - fishing trademark - 2 bytes                 
                if (THelp.CheckBits(Content, 4))
                {
                    Trademark = BitConverter.ToUInt16(Data, inx);
                    inx += Marshal.SizeOf(Trademark);
                }

                //ushort - fishing model - 2 bytes                 
                if (THelp.CheckBits(Content, 3))
                {
                    Model = BitConverter.ToUInt16(Data, inx);
                    inx += Marshal.SizeOf(Model);
                }

                //ushort - fishing gear count - 2 bytes                
                if (THelp.CheckBits(Content, 2))
                {
                    Count = BitConverter.ToUInt16(Data, inx);
                    inx += Marshal.SizeOf(Count);
                }
            }
            catch (Exception)
            {
                // TODO: log
            }
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <returns></returns>
        //public FGRecLite PortToFGRecLite()
        //{
        //    FGRecLite rec = new FGRecLite();
        //    rec.Content = this.Content;
        //    rec.FishingGear = this.FishingGear;
        //    rec.GearEye = this.GearEye;
        //    rec.GearLength = this.GearLength;
        //    rec.GearHeight = this.GearHeight;
        //    rec.Trademark = this.Trademark;
        //    rec.Model = this.Model;
        //    rec.Count = this.Count;

        //    return rec;
        //}
    }

    #endregion


    #region ELBProtocol < 2
    ///// <summary>
    ///// Fishing gear record
    ///// size - 14 bytes
    ///// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types) 
    ///// </summary>
    //public class FGRec
    //{
    //    private UInt16 eye;
    //    private int eyelite;
    //    private UInt16 len;
    //    private int lenlite;
    //    private UInt16 high;
    //    private int highlite;
    //    private UInt16 trade;
    //    private int tradelite;
    //    private UInt16 model;
    //    private int modellite;
    //    private UInt16 count;
    //    private int countlite;


    //    /// <summary>
    //    /// SQLite PK
    //    /// </summary>
    //    //[Key]
    //    public int Id { get; set; }

    //    /// <summary>
    //    ///Bit 7
    //    ///•	0 – excluded gear eye field
    //    ///•	1 – included gear eye field
    //    ///Bit 6
    //    ///•	0 – excluded length field
    //    ///•	1 – included length field
    //    ///Bit 5
    //    ///•	0 – excluded height field
    //    ///•	1 – included height field
    //    ///Bit 4 
    //    ///•	0 – excluded trademark field
    //    ///•	1 – included trademark field
    //    ///Bit 3
    //    ///•	0 – excluded model field
    //    ///•	1 – included model field
    //    ///Bit 2 
    //    ///•	0 – excluded count field
    //    ///•	1 – included count field
    //    /// </summary>
    //    public byte Content { get; set; }

    //    /// <summary>
    //    /// Sequence number from list fishing gears (0-255)
    //    /// </summary>
    //    //[Index("IX_FG_Unq", 1, IsUnique = true)]
    //    public byte FishingGear { get; set; }

    //    /// <summary>
    //    /// Fishing gear eye
    //    /// </summary>
    //    //[NotMapped]
    //    public UInt16 GearEye
    //    {
    //        get
    //        {
    //            return eye;
    //        }
    //        set
    //        {
    //            eye = value;
    //            eyelite = value;
    //        }
    //    }

    //    /// <summary>
    //    /// Fishing gear eye SQLite
    //    /// </summary>
    //    //[Index("IX_FG_Unq", 2, IsUnique = true)]
    //    public int GearEyeLite
    //    {
    //        get { return eyelite; }
    //        set
    //        {
    //            eyelite = value;
    //            eye = (UInt16)value;
    //        }
    //    }

    //    /// <summary>
    //    /// Fishing gear length
    //    /// </summary>
    //    //[NotMapped]
    //    public UInt16 GearLength
    //    {
    //        get { return len; }
    //        set
    //        {
    //            len = value;
    //            lenlite = value;
    //        }
    //    }

    //    /// <summary>
    //    /// Fishing gear length SQLite
    //    /// </summary>
    //    //[Index("IX_FG_Unq", 3, IsUnique = true)]
    //    public int GearLengthLite
    //    {
    //        get { return lenlite; }
    //        set
    //        {
    //            lenlite = value;
    //            len = (UInt16)value;
    //        }
    //    }

    //    /// <summary>
    //    /// Fishing gear height
    //    /// </summary>
    //    //[NotMapped]
    //    public UInt16 GearHeight
    //    {
    //        get { return high; }
    //        set
    //        {
    //            high = value;
    //            highlite = value;
    //        }
    //    }

    //    /// <summary>
    //    /// Fishing gear heaight
    //    /// </summary>
    //    //[Index("IX_FG_Unq", 4, IsUnique = true)]
    //    public int GearHeightLite
    //    {
    //        get { return highlite; }
    //        set
    //        {
    //            highlite = value;
    //            high = (UInt16)value;
    //        }
    //    }

    //    /// <summary>
    //    /// Fishing gear trademark
    //    /// </summary>
    //    //[NotMapped]
    //    public UInt16 Trademark
    //    {
    //        get { return trade; }
    //        set
    //        {
    //            trade = value;
    //            tradelite = value;
    //        }
    //    }

    //    /// <summary>
    //    /// Fishing gear trademark SQLite
    //    /// </summary>
    //    //[Index("IX_FG_Unq", 5, IsUnique = true)]
    //    public int TrademarkLite
    //    {
    //        get { return tradelite; }
    //        set
    //        {
    //            tradelite = value;
    //            trade = (UInt16)value;
    //        }
    //    }

    //    /// <summary>
    //    /// Fishing gear model
    //    /// </summary>
    //    //[NotMapped]
    //    public UInt16 Model
    //    {
    //        get { return model; }
    //        set
    //        {
    //            model = value;
    //            modellite = value;
    //        }
    //    }

    //    /// <summary>
    //    /// Fishing gear model SQLite
    //    /// </summary>
    //    //[Index("IX_FG_Unq", 6, IsUnique = true)]
    //    public int ModelLite
    //    {
    //        get { return modellite; }
    //        set
    //        {
    //            modellite = value;
    //            model = (UInt16)value;
    //        }
    //    }

    //    /// <summary>
    //    /// Fishing gear units count
    //    /// </summary>
    //    //[NotMapped]
    //    public UInt16 Count
    //    {
    //        get { return count; }
    //        set
    //        {
    //            count = value;
    //            countlite = value;
    //        }
    //    }

    //    /// <summary>
    //    /// Fishing gear units count SQLite
    //    /// </summary>
    //    public int CountLite
    //    {
    //        get { return countlite; }
    //        set
    //        {
    //            countlite = value;
    //            count = (UInt16)value;
    //        }
    //    }

    //    public byte Favorite { get; set; }

    //    public byte FGRecSizOf
    //    {
    //        get
    //        {
    //            // 1 byte
    //            var size = Marshal.SizeOf(Content);
    //            // 1 byte
    //            size += Marshal.SizeOf(FishingGear);
    //            // 2 bytes
    //            size += Marshal.SizeOf(GearEye);
    //            // 2 bytes
    //            size += Marshal.SizeOf(GearLength);
    //            // 2 bytes
    //            size += Marshal.SizeOf(GearHeight);
    //            // 2 bytes
    //            size += Marshal.SizeOf(Trademark);
    //            // 2 bytes
    //            size += Marshal.SizeOf(Model);
    //            // 2 bytes
    //            size += Marshal.SizeOf(Count);
    //            return (byte)size;
    //        }
    //    }

    //    /// <summary>
    //    /// Convert fields to byte array
    //    /// </summary>
    //    /// <returns></returns>
    //    public byte[] GetData()
    //    {
    //        int inx = 0;
    //        byte[] resp = new byte[500];

    //        // content - 1 byte
    //        resp[inx] = Content;
    //        inx += 1;

    //        // fishing gear - 1 byte
    //        resp[inx] = FishingGear;
    //        inx += 1;

    //        // fishing eye - 2 bytes
    //        byte[] feye = new byte[2] { 0, 0 };
    //        if (THelp.CheckBits(Content, 7))
    //        {
    //            feye = BitConverter.GetBytes(GearEye);
    //            Array.Copy(feye, 0, resp, inx, feye.Length);
    //            inx += Marshal.SizeOf(GearEye);
    //        }

    //        // fishing length - 2 bytes
    //        byte[] flen = new byte[2] { 0, 0 };
    //        if (THelp.CheckBits(Content, 6))
    //        {
    //            flen = BitConverter.GetBytes(GearLength);
    //            Array.Copy(flen, 0, resp, inx, flen.Length);
    //            inx += Marshal.SizeOf(GearLength);
    //        }

    //        // fishing height - 2 bytes
    //        byte[] fhigh = new byte[2] { 0, 0 };
    //        if (THelp.CheckBits(Content, 5))
    //        {
    //            fhigh = BitConverter.GetBytes(GearHeight);
    //            Array.Copy(fhigh, 0, resp, inx, fhigh.Length);
    //            inx += Marshal.SizeOf(GearHeight);
    //        }

    //        // fishing trademark - 2 bytes
    //        byte[] ftrade = new byte[2] { 0, 0 };
    //        if (THelp.CheckBits(Content, 4))
    //        {
    //            ftrade = BitConverter.GetBytes(Trademark);
    //            Array.Copy(ftrade, 0, resp, inx, ftrade.Length);
    //            inx += Marshal.SizeOf(Trademark);
    //        }

    //        // fishing model - 2 bytes
    //        byte[] fmodel = new byte[2] { 0, 0 };
    //        if (THelp.CheckBits(Content, 3))
    //        {
    //            fmodel = BitConverter.GetBytes(Model);
    //            Array.Copy(fmodel, 0, resp, inx, fmodel.Length);
    //            inx += Marshal.SizeOf(Model);
    //        }

    //        // fishing gear count - 2 bytes
    //        byte[] fcount = new byte[2] { 0, 0 };
    //        if (THelp.CheckBits(Content, 2))
    //        {
    //            fcount = BitConverter.GetBytes(Count);
    //            Array.Copy(fcount, 0, resp, inx, fcount.Length);
    //            inx += Marshal.SizeOf(Count);
    //        }

    //        Array.Resize(ref resp, inx);

    //        return resp;
    //    }

    //    /// <summary>
    //    /// Decode FGrec data - 14 bytes
    //    /// </summary>
    //    /// <param name="Data"></param>
    //    public void DecodePacket(byte[] Data)
    //    {
    //        try
    //        {
    //            int inx = 0;
    //            // content - 1 byte
    //            Content = Data[inx];
    //            inx += Marshal.SizeOf(Content);

    //            // 1 byte - fishing gear - sequence number from fg list
    //            FishingGear = Data[inx];
    //            inx += Marshal.SizeOf(FishingGear);

    //            //ushort - fishing eye - 2 bytes                
    //            if (THelp.CheckBits(Content, 7))
    //            {
    //                GearEye = BitConverter.ToUInt16(Data, inx);
    //                inx += Marshal.SizeOf(GearEye);
    //            }

    //            //ushort fishing length - 2 bytes                 
    //            if (THelp.CheckBits(Content, 6))
    //            {
    //                GearLength = BitConverter.ToUInt16(Data, inx);
    //                inx += Marshal.SizeOf(GearLength);
    //            }

    //            //ushort - fishing height - 2 bytes                 
    //            if (THelp.CheckBits(Content, 5))
    //            {
    //                GearHeight = BitConverter.ToUInt16(Data, inx);
    //                inx += Marshal.SizeOf(GearHeight);
    //            }

    //            //ushort - fishing trademark - 2 bytes                 
    //            if (THelp.CheckBits(Content, 4))
    //            {
    //                Trademark = BitConverter.ToUInt16(Data, inx);
    //                inx += Marshal.SizeOf(Trademark);
    //            }

    //            //ushort - fishing model - 2 bytes                 
    //            if (THelp.CheckBits(Content, 3))
    //            {
    //                Model = BitConverter.ToUInt16(Data, inx);
    //                inx += Marshal.SizeOf(Model);
    //            }

    //            //ushort - fishing gear count - 2 bytes                
    //            if (THelp.CheckBits(Content, 2))
    //            {
    //                Count = BitConverter.ToUInt16(Data, inx);
    //                inx += Marshal.SizeOf(Count);
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            // TODO: log
    //        }
    //    }

    //    /// <summary>
    //    /// Not in use
    //    /// </summary>
    //    /// <returns></returns>
    //    //public FGRecLite PortToFGRecLite()
    //    //{
    //    //    FGRecLite rec = new FGRecLite();
    //    //    rec.Content = this.Content;
    //    //    rec.FishingGear = this.FishingGear;
    //    //    rec.GearEye = this.GearEye;
    //    //    rec.GearLength = this.GearLength;
    //    //    rec.GearHeight = this.GearHeight;
    //    //    rec.Trademark = this.Trademark;
    //    //    rec.Model = this.Model;
    //    //    rec.Count = this.Count;

    //    //    return rec;
    //    //}
    //}

    #endregion
}
