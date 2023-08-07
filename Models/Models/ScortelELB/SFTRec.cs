using ScortelApi.Models.Interfaces;
using ScortelApi.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{

    #region ELBProt = 3.3 for FVMS-M 3.0.0.9 and newer
    /// <summary>
    /// start of fishing trip record
    /// full size - 26 + ( 14 * N)
    /// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types) 
    /// 
    /// ELBProtocol 2.7
    /// </summary>
    public class SFTRec //: ISFTRec
    {
        #region fields
        private long timestamp;
        private Int64 timestamplite;
        // port
        private UInt16 portid;
        private int portidlite;
        // econ zone
        private UInt16 econzone;
        private int econzonelite;
        private ZoneDefinition zdefecon;
        // stat zone
        private UInt16 statsozne;
        private int statzonelite;
        // creationdt
        private DateTime creationdt;
        // discardedrecdt        
        private UInt32 drdtl;
        private DateTime drdt;
        // fgrecscount
        private byte fgrcount;
        private List<FGRec> fglist = new List<FGRec>();
        // activity
        private byte activity;

        private byte fcrcount;
        private List<FCRec> fclist = new List<FCRec>();
        #endregion

        #region positions
        private const byte EcoZone_bit_position = 7;
        private const byte StatZone_bit_position = 6;
        private const byte StGen_bit_position = 5;
        private const byte Pos_bit_position = 4;
        //private const byte TimeSt_bit_position = 3;
        private const byte PortId_bit_position = 3;
        private const byte bit_pos_datacorrection = 2;
        private const byte bit_pos_include_disreccrdt = 1;
        #endregion

        /// <summary>
        /// SQLite PK
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// ???
        /// </summary>

        //[NotMapped]
        public string TimestampStr { get; set; }

        #region ScortelELBProtocol fields

        /// <summary>
        /// Comm protocol version
        /// 0 - 3.0.0.8 and older
        /// 1 - 3.0.0.9
        /// </summary>
        public byte ProtocolVersion { get; set; }

        /// <summary>
        ///Bit 7 – Economic zone
        ///•	0 – excluded economicl zone field
        ///•	1 – included economic zone field
        ///Bit 6 – Statistical zone
        ///•	0 – excluded statistical zone field
        ///•	1 – included statistical zone field 
        ///Bit 5 – Status generation of record
        ///•	0 – auto generated
        ///•	1 – manual generated
        ///Bit 4 – Position GPS part
        ///•	0 – excluded
        ///•	1 – included  
        ///Bit 3 – Port ID
        ///•	0 – excluded port Id
        ///•	1 – included port id
        ///Bit 2 – Data correction
        ///•	0 – no correction of data
        ///•	1 – with correction
        ///Bit 1 
        ///•	0 – DiscardedRecCrDT – emptry - exclude
        ///•	1 – DiscardedRecCrDT – with data - include
        /// </summary>
        public byte Content { get; set; }

        /// <summary>
        /// Timestamp since 2018.01.01 00:00:00 UTC in seconds
        /// </summary>      
        public long Timestamp
        {
            get { return timestamp; }
            set
            {
                timestamp = value;
                timestamplite = value;
                creationdt = THelp.DateTimeFromELBTimestamp(timestamp);
            }
        }

        /// <summary>
        /// Timestamp since 2018.01.01 00:00:00 UTC in seconds
        /// SQLite
        /// </summary>  
        [NotMapped]
        public Int64 TimestampLite
        {
            get { return timestamplite; }
            set
            {
                timestamplite = value;
                timestamp = (uint)value;
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

        #region Ports
        public LPorts LPorts { get; set; }

        ///// <summary>
        ///// Sequence number from list of ports (0-65535)
        ///// </summary>
        //[NotMapped]
        //public UInt16 PortId
        //{
        //    get { return portid; }
        //    set
        //    {
        //        portid = value;
        //        portidlite = value;
        //    }
        //}

        ///// <summary>
        ///// Sequence number from list of ports (0-65535)
        ///// SQLite
        ///// </summary>

        //public int PortIdLite
        //{
        //    get { return portidlite; }
        //    set
        //    {
        //        portidlite = value;
        //        portid = (UInt16)value;
        //    }
        //}
        #endregion

        #region FGRecsCount
        private byte FGRecsCount
        {
            get
            {
                fgrcount = (byte)fglist.Count;
                return fgrcount;
            }
            set
            {
                fgrcount = value;
            }
        }
        #endregion

        //public virtual ICollection<SFTRecFGRec> SFTRecFGRecs { get; set; }

        public virtual ICollection<FGRec> FGRecs { get; set; }

        #region fishing activity
        ///// <summary>
        ///// List of fishing gears
        ///// </summary>
        //public List<FGRec> FGRecs { get; set; } = new List<FGRec>();

        ///// <summary>
        ///// Sequence number from list activities (0-255)
        ///// </summary>
        //public byte Activity { get; set; }

        public FishingActivity FishingActivity { get; set; }
        #endregion

        #region Econ Zone

        public ZoneDefinition ZoneDefinitionEcon { get; set; }

        ///// <summary>
        ///// Sequence number from list economic zones (0-65535)
        /////. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
        ///// </summary>
        ////[NotMapped]
        //public UInt16 EconZone
        //{
        //    get { return econzone; }
        //    set
        //    {
        //        econzone = value;
        //        econzonelite = value;
        //    }
        //}

        ///// <summary>
        ///// Sequence number from list economic zones (0-65535)
        /////. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
        ///// SQLite
        ///// </summary>
        //public int EconZoneLite
        //{
        //    get { return econzonelite; }
        //    set
        //    {
        //        econzonelite = value;
        //        econzone = (UInt16)value;
        //    }
        //}
        #endregion

        #region stat zone

        public ZoneDefinition ZoneDefinitionStat { get; set; }

        ///// <summary>
        ///// Sequence number from list statistical or other zones (0-65535)
        /////. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
        ///// </summary>
        ////[NotMapped]
        //public UInt16 StatZone
        //{
        //    get { return statsozne; }
        //    set
        //    {
        //        statsozne = value;
        //        statzonelite = value;
        //    }
        //}

        ///// <summary>
        ///// Sequence number from list statistical or other zones (0-65535)
        /////. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
        /////SQLite
        ///// </summary>
        //public int StatZoneLite
        //{
        //    get { return statzonelite; }
        //    set
        //    {
        //        statzonelite = value;
        //        statsozne = (UInt16)value;
        //    }
        //}
        #endregion

        /// <summary>
        /// Gps position part
        /// </summary>
        public PosRec Pos { get; set; }

        /// <summary>
        /// Start fishing trip structure - creation status
        /// </summary>
        //[NotMapped]
        public string CreationStatus
        {
            get
            {
                if (THelp.CheckBits(Content, StGen_bit_position))
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
        /// Start fishing trip structure - sent status
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
        /// Start fishing trip structure - completion status
        /// </summary>
        public int CompletionStatus { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("DateTime", typeof(System.DateTime))]
        public DateTime CreationDT
        {
            get
            {
                return creationdt;
            }
            set
            {
                creationdt = THelp.DateTimeFromELBTimestamp(timestamp);
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

        #endregion

        public DateTime ReceivedTimestamp { get; set; }

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

        /// <summary>
        /// Count of initial FCRecs records
        /// </summary>
        //[DefaultValue(0)]
        public byte FCRecsCount
        {
            get { return fcrcount; }
            set
            {
                try
                {
                    fcrcount = value;
                }
                catch (Exception)
                {
                    fcrcount = 0;
                }
            }
        }

        /// <summary>
        /// List of initial fishing catches
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

        /// <summary>
        /// Status is sent to old ISS
        /// </summary>
        public int? StatusTransISS { get; set; } = 0;
        /// <summary>
        /// Status is sent to new ISS
        /// </summary>
        public int? StatusTransNISS { get; set; } = 0;

        #region Content func

        /// <summary>
        /// Content func - exclude economic zone
        /// </summary>
        public void ExcludeEconZone()
        {
            Content = THelp.ResetBits(Content, EcoZone_bit_position);
        }

        /// <summary>
        /// Content func - include  economic zone
        /// </summary>
        public void IncludeEconZone()
        {
            Content = THelp.SetBits(Content, EcoZone_bit_position);
        }

        /// <summary>
        /// Content func - exlude statistical zone
        /// </summary>
        public void ExcludeStatZone()
        {
            Content = THelp.ResetBits(Content, StatZone_bit_position);
        }

        /// <summary>
        /// Content func - include statistical zone
        /// </summary>
        public void IncludeStatZone()
        {
            Content = THelp.SetBits(Content, StatZone_bit_position);
        }

        /// <summary>
        /// Content func - exlude gps pos
        /// </summary>
        public void ExcludeGPSPos()
        {
            Content = THelp.ResetBits(Content, Pos_bit_position);
        }

        /// <summary>
        /// Content func - include gps pos
        /// </summary>
        public void IncludeGPSPos()
        {
            Content = THelp.SetBits(Content, Pos_bit_position);
        }

        /// <summary>
        /// Content func - auto generated structure
        /// </summary>
        public void SetAutoGenerated()
        {
            Content = THelp.ResetBits(Content, StGen_bit_position);
        }

        /// <summary>
        /// Content func - manual generated structure
        /// </summary>
        public void SetManualGenerated()
        {
            Content = THelp.SetBits(Content, StGen_bit_position);
        }

        ///// <summary>
        ///// Content func - exlude timestamp field 
        ///// </summary>
        //public void ExcludeTimestamp()
        //{
        //    Content = THelp.ResetBits(Content, TimeSt_bit_position);
        //}

        ///// <summary>
        ///// Content func - include timstamp field
        ///// </summary>
        //public void IncludeTimestamp()
        //{
        //    Content = THelp.SetBits(Content, TimeSt_bit_position);
        //}

        /// <summary>
        /// Content func - exlude port id 
        /// </summary>
        public void ExcludePortId()
        {
            Content = THelp.ResetBits(Content, PortId_bit_position);
        }

        /// <summary>
        /// Content func - include port id
        /// </summary>
        public void IncludePortId()
        {
            Content = THelp.SetBits(Content, PortId_bit_position);
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
        /// Convert data field to byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            int inx = 0;
            byte[] resp = new byte[1000];

            // content - 1 byte
            resp[inx] = Content;
            inx += 1;

            // timestamp - 4 bytes
            byte[] time = new byte[4] { 0, 0, 0, 0 };
            time = BitConverter.GetBytes(Timestamp);
            Array.Copy(time, 0, resp, inx, time.Length);
            inx += Marshal.SizeOf(Timestamp);

            //discardedreccrdt
            // DisRecCrDTL - 4 bytes
            byte[] distime = new byte[4] { 0, 0, 0, 0 };
            if (THelp.CheckBits(Content, bit_pos_include_disreccrdt))
            {
                distime = BitConverter.GetBytes(DisRecCrDTL);
                Array.Copy(distime, 0, resp, inx, distime.Length);
                inx += Marshal.SizeOf(DisRecCrDTL);
            }

            // port id - 2 bytes
            byte[] portid = new byte[2] { 0, 0 };
            if (THelp.CheckBits(Content, PortId_bit_position))
            {
                if (LPorts != null)
                {
                    portid = BitConverter.GetBytes((UInt16)LPorts.Id);
                    Array.Copy(portid, 0, resp, inx, portid.Length);
                    inx += Marshal.SizeOf((UInt16)LPorts.Id);
                }
                else
                {
                    portid = BitConverter.GetBytes((UInt16)0);
                    Array.Copy(portid, 0, resp, inx, portid.Length);
                    inx += Marshal.SizeOf((UInt16)0);
                }
                //portid = BitConverter.GetBytes(PortId);
                //Array.Copy(portid, 0, resp, inx, portid.Length);
                //inx += Marshal.SizeOf(PortId);
            }

            ////////////TODO: use FGRCount change below
            //////////// FGRecs - new version from 23.06.2019
            //////////if (SFTRecFGRecs != null)
            //////////{
            //////////    if (SFTRecFGRecs.Count == 0)
            //////////    {
            //////////        // fishing gear count = 0 -> no fg data next byte is activity
            //////////        resp[inx] = 0;
            //////////        inx += sizeof(byte);
            //////////    }
            //////////    else
            //////////    {
            //////////        // set fgear count 
            //////////        resp[inx] = (byte)SFTRecFGRecs.Count;
            //////////        inx += sizeof(byte);
            //////////        // fishing gear data
            //////////        foreach (var sftfg in SFTRecFGRecs)
            //////////        {
            //////////            using (var context = IoC.ApplicationDbContext)
            //////////            {
            //////////                var foundfg = context.FGRecs.FirstOrDefault(x => x.Id == sftfg.FGRec.Id);
            //////////                if (foundfg != null)
            //////////                {
            //////////                    var fgarr = foundfg.GetData();
            //////////                    Array.Copy(fgarr, 0, resp, inx, fgarr.Length);
            //////////                    inx += fgarr.Length;
            //////////                }
            //////////                else
            //////////                {
            //////////                    //TODO: ?????
            //////////                }
            //////////            }
            //////////        }
            //////////    }
            //////////}
            //////////else
            //////////{
            //////////    // fishing gear count = 0 -> no fg data next byte is activity
            //////////    resp[inx] = 0;
            //////////    inx += sizeof(byte);
            //////////}

            //Activity
            //resp[inx] = Activity;
            if (FishingActivity != null)
            {
                resp[inx] = (byte)FishingActivity.Id;
                inx += 1;
            }
            else
            {
                resp[inx] = 0;
                inx += 1;
            }

            // economic zone
            byte[] econom = new byte[2] { 0, 0 };
            if (THelp.CheckBits(Content, EcoZone_bit_position))
            {
                if (ZoneDefinitionEcon != null)
                {
                    econom = BitConverter.GetBytes((UInt16)ZoneDefinitionEcon.Id);
                    Array.Copy(econom, 0, resp, inx, econom.Length);
                    inx += Marshal.SizeOf((UInt16)ZoneDefinitionEcon.Id);
                }
                else
                {
                    econom = BitConverter.GetBytes((UInt16)0);
                    Array.Copy(econom, 0, resp, inx, econom.Length);
                    inx += Marshal.SizeOf((UInt16)0);
                }
                //econom = BitConverter.GetBytes(EconZone);
                //Array.Copy(econom, 0, resp, inx, econom.Length);
                //inx += Marshal.SizeOf(EconZone);
            }

            // statistical zone
            byte[] stat = new byte[2] { 0, 0 };
            if (THelp.CheckBits(Content, StatZone_bit_position))
            {
                if (ZoneDefinitionStat != null)
                {
                    stat = BitConverter.GetBytes((UInt16)ZoneDefinitionStat.Id);
                    Array.Copy(econom, 0, resp, inx, econom.Length);
                    inx += Marshal.SizeOf((UInt16)ZoneDefinitionStat.Id);
                }
                else
                {
                    stat = BitConverter.GetBytes((UInt16)0);
                    Array.Copy(econom, 0, resp, inx, econom.Length);
                    inx += Marshal.SizeOf((UInt16)0);
                }
                //stat = BitConverter.GetBytes(StatZone);
                //Array.Copy(econom, 0, resp, inx, econom.Length);
                //inx += Marshal.SizeOf(StatZone);
            }

            // position gps
            if (THelp.CheckBits(Content, Pos_bit_position))
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
        /// Decode start of fishing trip data
        /// </summary>
        /// <param name="Data"></param>
        public void DecodePacket(byte[] Data)
        {
            try
            {
               
            }
            catch (Exception)
            {
                //TODO: log error
            }
        }
    }


    #endregion

    #region ELBProt = 2.7
    ///// <summary>
    ///// start of fishing trip record
    ///// full size - 26 + ( 14 * N)
    ///// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types) 
    ///// 
    ///// ELBProtocol 2.7
    ///// </summary>
    //public class SFTRec //: ISFTRec
    //{
    //    #region fields
    //    private long timestamp;
    //    private Int64 timestamplite;
    //    // port
    //    private UInt16 portid;
    //    private int portidlite;
    //    // econ zone
    //    private UInt16 econzone;
    //    private int econzonelite;
    //    private ZoneDefinition zdefecon;
    //    // stat zone
    //    private UInt16 statsozne;
    //    private int statzonelite;
    //    // creationdt
    //    private DateTime creationdt;
    //    // discardedrecdt        
    //    private UInt32 drdtl;
    //    private DateTime drdt;
    //    // fgrecscount
    //    private byte fgrcount;
    //    private List<FGRec> fglist = new List<FGRec>();
    //    // activity
    //    private byte activity;
    //    #endregion

    //    #region positions
    //    private const byte EcoZone_bit_position = 7;
    //    private const byte StatZone_bit_position = 6;
    //    private const byte StGen_bit_position = 5;
    //    private const byte Pos_bit_position = 4;
    //    //private const byte TimeSt_bit_position = 3;
    //    private const byte PortId_bit_position = 3;
    //    private const byte bit_pos_datacorrection = 2;
    //    private const byte bit_pos_include_disreccrdt = 1;
    //    #endregion

    //    /// <summary>
    //    /// SQLite PK
    //    /// </summary>
    //    [Key]
    //    public long Id { get; set; }
    //    /// <summary>
    //    /// ???
    //    /// </summary>

    //    //[NotMapped]
    //    public string TimestampStr { get; set; }

    //    #region ScortelELBProtocol fields
    //    /// <summary>
    //    ///Bit 7 – Economic zone
    //    ///•	0 – excluded economicl zone field
    //    ///•	1 – included economic zone field
    //    ///Bit 6 – Statistical zone
    //    ///•	0 – excluded statistical zone field
    //    ///•	1 – included statistical zone field 
    //    ///Bit 5 – Status generation of record
    //    ///•	0 – auto generated
    //    ///•	1 – manual generated
    //    ///Bit 4 – Position GPS part
    //    ///•	0 – excluded
    //    ///•	1 – included  
    //    ///Bit 3 – Port ID
    //    ///•	0 – excluded port Id
    //    ///•	1 – included port id
    //    ///Bit 2 – Data correction
    //    ///•	0 – no correction of data
    //    ///•	1 – with correction
    //    ///Bit 1 
    //    ///•	0 – DiscardedRecCrDT – emptry - exclude
    //    ///•	1 – DiscardedRecCrDT – with data - include
    //    /// </summary>
    //    public byte Content { get; set; }

    //    /// <summary>
    //    /// Timestamp since 2018.01.01 00:00:00 UTC in seconds
    //    /// </summary>      
    //    public long Timestamp
    //    {
    //        get { return timestamp; }
    //        set
    //        {
    //            timestamp = value;
    //            timestamplite = value;
    //            creationdt = THelp.DateTimeFromELBTimestamp(timestamp);
    //        }
    //    }

    //    /// <summary>
    //    /// Timestamp since 2018.01.01 00:00:00 UTC in seconds
    //    /// SQLite
    //    /// </summary>  
    //    [NotMapped]
    //    public Int64 TimestampLite
    //    {
    //        get { return timestamplite; }
    //        set
    //        {
    //            timestamplite = value;
    //            timestamp = (uint)value;
    //        }
    //    }

    //    #region DiscardedRecCrDTL
    //    [NotMapped]
    //    private UInt32 DisRecCrDTL
    //    {
    //        get
    //        {
    //            return drdtl;
    //        }
    //        set
    //        {
    //            drdtl = value;
    //            drdt = THelp.DateTimeFromELBTimestamp(value);
    //        }
    //    }

    //    #endregion

    //    #region Ports
    //    public LPorts LPorts { get; set; }

    //    ///// <summary>
    //    ///// Sequence number from list of ports (0-65535)
    //    ///// </summary>
    //    //[NotMapped]
    //    //public UInt16 PortId
    //    //{
    //    //    get { return portid; }
    //    //    set
    //    //    {
    //    //        portid = value;
    //    //        portidlite = value;
    //    //    }
    //    //}

    //    ///// <summary>
    //    ///// Sequence number from list of ports (0-65535)
    //    ///// SQLite
    //    ///// </summary>

    //    //public int PortIdLite
    //    //{
    //    //    get { return portidlite; }
    //    //    set
    //    //    {
    //    //        portidlite = value;
    //    //        portid = (UInt16)value;
    //    //    }
    //    //}
    //    #endregion

    //    #region FGRecsCount
    //    private byte FGRecsCount
    //    {
    //        get
    //        {
    //            fgrcount = (byte)fglist.Count;
    //            return fgrcount;
    //        }
    //        set
    //        {
    //            fgrcount = value;
    //        }
    //    }
    //    #endregion

    //    //public virtual ICollection<SFTRecFGRec> SFTRecFGRecs { get; set; }

    //    public virtual ICollection<FGRec> FGRecs { get; set; }

    //    #region fishing activity
    //    ///// <summary>
    //    ///// List of fishing gears
    //    ///// </summary>
    //    //public List<FGRec> FGRecs { get; set; } = new List<FGRec>();

    //    ///// <summary>
    //    ///// Sequence number from list activities (0-255)
    //    ///// </summary>
    //    //public byte Activity { get; set; }

    //    public FishingActivity FishingActivity { get; set; }
    //    #endregion

    //    #region Econ Zone

    //    public ZoneDefinition ZoneDefinitionEcon { get; set; }

    //    ///// <summary>
    //    ///// Sequence number from list economic zones (0-65535)
    //    /////. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
    //    ///// </summary>
    //    ////[NotMapped]
    //    //public UInt16 EconZone
    //    //{
    //    //    get { return econzone; }
    //    //    set
    //    //    {
    //    //        econzone = value;
    //    //        econzonelite = value;
    //    //    }
    //    //}

    //    ///// <summary>
    //    ///// Sequence number from list economic zones (0-65535)
    //    /////. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
    //    ///// SQLite
    //    ///// </summary>
    //    //public int EconZoneLite
    //    //{
    //    //    get { return econzonelite; }
    //    //    set
    //    //    {
    //    //        econzonelite = value;
    //    //        econzone = (UInt16)value;
    //    //    }
    //    //}
    //    #endregion

    //    #region stat zone

    //    public ZoneDefinition ZoneDefinitionStat { get; set; }

    //    ///// <summary>
    //    ///// Sequence number from list statistical or other zones (0-65535)
    //    /////. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
    //    ///// </summary>
    //    ////[NotMapped]
    //    //public UInt16 StatZone
    //    //{
    //    //    get { return statsozne; }
    //    //    set
    //    //    {
    //    //        statsozne = value;
    //    //        statzonelite = value;
    //    //    }
    //    //}

    //    ///// <summary>
    //    ///// Sequence number from list statistical or other zones (0-65535)
    //    /////. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
    //    /////SQLite
    //    ///// </summary>
    //    //public int StatZoneLite
    //    //{
    //    //    get { return statzonelite; }
    //    //    set
    //    //    {
    //    //        statzonelite = value;
    //    //        statsozne = (UInt16)value;
    //    //    }
    //    //}
    //    #endregion

    //    /// <summary>
    //    /// Gps position part
    //    /// </summary>
    //    public PosRec Pos { get; set; } 

    //    /// <summary>
    //    /// Start fishing trip structure - creation status
    //    /// </summary>
    //    //[NotMapped]
    //    public string CreationStatus
    //    {
    //        get
    //        {
    //            if (THelp.CheckBits(Content, StGen_bit_position))
    //            {
    //                return "ръчно";
    //            }
    //            else
    //            {
    //                return "автоматично";
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Start fishing trip structure - sent status
    //    /// </summary>
    //    public int SentStatus { get; set; }
    //    //[NotMapped]
    //    public string SentStatusStr
    //    {
    //        get
    //        {
    //            switch (SentStatus)
    //            {
    //                case 0:
    //                    return "не е изпратено";
    //                case 1:
    //                    return "изпратено";
    //                case 2:
    //                    return "";
    //                case 3:
    //                    return "";
    //                default:
    //                    return "";
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Start fishing trip structure - completion status
    //    /// </summary>
    //    public int CompletionStatus { get; set; }

    //    [System.Xml.Serialization.XmlElementAttribute("DateTime", typeof(System.DateTime))]
    //    public DateTime CreationDT
    //    {
    //        get
    //        {
    //            return creationdt;
    //        }
    //        set
    //        {
    //            creationdt = THelp.DateTimeFromELBTimestamp(timestamp);
    //        }
    //    }

    //    public DateTime DisRecCrDT
    //    {
    //        get
    //        {
    //            return drdt;
    //        }
    //        set
    //        {
    //            drdt = value;
    //            drdtl = THelp.ELBTimeFormat(value);
    //        }
    //    }

    //    //[NotMapped]
    //    public string CompletionStatusStr
    //    {
    //        get
    //        {
    //            switch (CompletionStatus)
    //            {
    //                case 0:
    //                    return "не е завършено";
    //                case 1:
    //                    return "завършено";
    //                case 2:
    //                    return "";
    //                case 3:
    //                    return "";
    //                default:
    //                    return "";
    //            }
    //        }
    //    }

    //    #endregion

    //    public DateTime ReceivedTimestamp { get; set; }

    //    /// <summary>
    //    /// Trip number from information system - number of page of fishing diary
    //    /// first byte is length of the field, if length is 0 - there is no data
    //    /// minimum field size 1 byte, maximum 255
    //    /// Data is encoded in Unicode
    //    /// Needed for data recreation at server side
    //    /// </summary>       
    //    public byte[] TripNumber { get; set; }

    //    public string TripNumberStr
    //    {
    //        get
    //        {
    //            if (TripNumber != null && TripNumber.Length != 1)
    //            {
    //                return Encoding.UTF8.GetString(TripNumber, 1, TripNumber.Length - 1);
    //            }
    //            else
    //            {
    //                return "";
    //            }
    //        }
    //        set
    //        {
    //            //tripnumberstr = value;
    //            byte[] tmp = Encoding.UTF8.GetBytes(value);
    //            if (tmp.Length > 255)
    //            {
    //            }
    //            else
    //            {
    //                byte len = (byte)tmp.Length;
    //                byte[] tnum = new byte[len + 1];
    //                tnum[0] = len;
    //                Array.Copy(tmp, 0, tnum, 1, (int)len);
    //                TripNumber = tnum;
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// IDP Sensors data
    //    /// </summary>
    //    public SensRec Sens { get; set; }

    //    #region Content func

    //    /// <summary>
    //    /// Content func - exclude economic zone
    //    /// </summary>
    //    public void ExcludeEconZone()
    //    {
    //        Content = THelp.ResetBits(Content, EcoZone_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - include  economic zone
    //    /// </summary>
    //    public void IncludeEconZone()
    //    {
    //        Content = THelp.SetBits(Content, EcoZone_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - exlude statistical zone
    //    /// </summary>
    //    public void ExcludeStatZone()
    //    {
    //        Content = THelp.ResetBits(Content, StatZone_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - include statistical zone
    //    /// </summary>
    //    public void IncludeStatZone()
    //    {
    //        Content = THelp.SetBits(Content, StatZone_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - exlude gps pos
    //    /// </summary>
    //    public void ExcludeGPSPos()
    //    {
    //        Content = THelp.ResetBits(Content, Pos_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - include gps pos
    //    /// </summary>
    //    public void IncludeGPSPos()
    //    {
    //        Content = THelp.SetBits(Content, Pos_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - auto generated structure
    //    /// </summary>
    //    public void SetAutoGenerated()
    //    {
    //        Content = THelp.ResetBits(Content, StGen_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - manual generated structure
    //    /// </summary>
    //    public void SetManualGenerated()
    //    {
    //        Content = THelp.SetBits(Content, StGen_bit_position);
    //    }

    //    ///// <summary>
    //    ///// Content func - exlude timestamp field 
    //    ///// </summary>
    //    //public void ExcludeTimestamp()
    //    //{
    //    //    Content = THelp.ResetBits(Content, TimeSt_bit_position);
    //    //}

    //    ///// <summary>
    //    ///// Content func - include timstamp field
    //    ///// </summary>
    //    //public void IncludeTimestamp()
    //    //{
    //    //    Content = THelp.SetBits(Content, TimeSt_bit_position);
    //    //}

    //    /// <summary>
    //    /// Content func - exlude port id 
    //    /// </summary>
    //    public void ExcludePortId()
    //    {
    //        Content = THelp.ResetBits(Content, PortId_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - include port id
    //    /// </summary>
    //    public void IncludePortId()
    //    {
    //        Content = THelp.SetBits(Content, PortId_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - no correction of data, Bit2 = 0
    //    /// </summary>
    //    public void SetNoDataCorrection()
    //    {
    //        Content = THelp.ResetBits(Content, bit_pos_datacorrection);
    //    }

    //    /// <summary>
    //    /// Content func - with data correction, Bit2 = 1
    //    /// </summary>
    //    public void SetDataCorrection()
    //    {
    //        Content = THelp.SetBits(Content, bit_pos_datacorrection);
    //    }

    //    /// <summary>
    //    /// Conten func - exclude DiscardedRecCrDTL, Bit1 = 0
    //    /// </summary>
    //    public void ExcludeDisRecCrDTL()
    //    {
    //        Content = THelp.ResetBits(Content, bit_pos_include_disreccrdt);
    //    }

    //    /// <summary>
    //    /// Content func - include DiscardedRecCrDTL, Bit1 = 1
    //    /// </summary>
    //    public void IncludeDisRecCrDTL()
    //    {
    //        Content = THelp.SetBits(Content, bit_pos_include_disreccrdt);
    //    }
    //    #endregion

    //    /// <summary>
    //    /// Convert data field to byte array
    //    /// </summary>
    //    /// <returns></returns>
    //    public byte[] GetData()
    //    {
    //        int inx = 0;
    //        byte[] resp = new byte[1000];

    //        // content - 1 byte
    //        resp[inx] = Content;
    //        inx += 1;

    //        // timestamp - 4 bytes
    //        byte[] time = new byte[4] { 0, 0, 0, 0 };
    //        time = BitConverter.GetBytes(Timestamp);
    //        Array.Copy(time, 0, resp, inx, time.Length);
    //        inx += Marshal.SizeOf(Timestamp);

    //        //discardedreccrdt
    //        // DisRecCrDTL - 4 bytes
    //        byte[] distime = new byte[4] { 0, 0, 0, 0 };
    //        if (THelp.CheckBits(Content, bit_pos_include_disreccrdt))
    //        {
    //            distime = BitConverter.GetBytes(DisRecCrDTL);
    //            Array.Copy(distime, 0, resp, inx, distime.Length);
    //            inx += Marshal.SizeOf(DisRecCrDTL);
    //        }

    //        // port id - 2 bytes
    //        byte[] portid = new byte[2] { 0, 0 };
    //        if (THelp.CheckBits(Content, PortId_bit_position))
    //        {
    //            if (LPorts != null)
    //            {
    //                portid = BitConverter.GetBytes((UInt16)LPorts.Id);
    //                Array.Copy(portid, 0, resp, inx, portid.Length);
    //                inx += Marshal.SizeOf((UInt16)LPorts.Id);
    //            }
    //            else
    //            {
    //                portid = BitConverter.GetBytes((UInt16)0);
    //                Array.Copy(portid, 0, resp, inx, portid.Length);
    //                inx += Marshal.SizeOf((UInt16)0);
    //            }
    //            //portid = BitConverter.GetBytes(PortId);
    //            //Array.Copy(portid, 0, resp, inx, portid.Length);
    //            //inx += Marshal.SizeOf(PortId);
    //        }

    //        ////////////TODO: use FGRCount change below
    //        //////////// FGRecs - new version from 23.06.2019
    //        //////////if (SFTRecFGRecs != null)
    //        //////////{
    //        //////////    if (SFTRecFGRecs.Count == 0)
    //        //////////    {
    //        //////////        // fishing gear count = 0 -> no fg data next byte is activity
    //        //////////        resp[inx] = 0;
    //        //////////        inx += sizeof(byte);
    //        //////////    }
    //        //////////    else
    //        //////////    {
    //        //////////        // set fgear count 
    //        //////////        resp[inx] = (byte)SFTRecFGRecs.Count;
    //        //////////        inx += sizeof(byte);
    //        //////////        // fishing gear data
    //        //////////        foreach (var sftfg in SFTRecFGRecs)
    //        //////////        {
    //        //////////            using (var context = IoC.ApplicationDbContext)
    //        //////////            {
    //        //////////                var foundfg = context.FGRecs.FirstOrDefault(x => x.Id == sftfg.FGRec.Id);
    //        //////////                if (foundfg != null)
    //        //////////                {
    //        //////////                    var fgarr = foundfg.GetData();
    //        //////////                    Array.Copy(fgarr, 0, resp, inx, fgarr.Length);
    //        //////////                    inx += fgarr.Length;
    //        //////////                }
    //        //////////                else
    //        //////////                {
    //        //////////                    //TODO: ?????
    //        //////////                }
    //        //////////            }
    //        //////////        }
    //        //////////    }
    //        //////////}
    //        //////////else
    //        //////////{
    //        //////////    // fishing gear count = 0 -> no fg data next byte is activity
    //        //////////    resp[inx] = 0;
    //        //////////    inx += sizeof(byte);
    //        //////////}

    //        //Activity
    //        //resp[inx] = Activity;
    //        if (FishingActivity != null)
    //        {
    //            resp[inx] = (byte)FishingActivity.Id;
    //            inx += 1;
    //        }
    //        else
    //        {
    //            resp[inx] = 0;
    //            inx += 1;
    //        }

    //        // economic zone
    //        byte[] econom = new byte[2] { 0, 0 };
    //        if (THelp.CheckBits(Content, EcoZone_bit_position))
    //        {
    //            if (ZoneDefinitionEcon != null)
    //            {
    //                econom = BitConverter.GetBytes((UInt16)ZoneDefinitionEcon.Id);
    //                Array.Copy(econom, 0, resp, inx, econom.Length);
    //                inx += Marshal.SizeOf((UInt16)ZoneDefinitionEcon.Id);
    //            }
    //            else
    //            {
    //                econom = BitConverter.GetBytes((UInt16)0);
    //                Array.Copy(econom, 0, resp, inx, econom.Length);
    //                inx += Marshal.SizeOf((UInt16)0);
    //            }
    //            //econom = BitConverter.GetBytes(EconZone);
    //            //Array.Copy(econom, 0, resp, inx, econom.Length);
    //            //inx += Marshal.SizeOf(EconZone);
    //        }

    //        // statistical zone
    //        byte[] stat = new byte[2] { 0, 0 };
    //        if (THelp.CheckBits(Content, StatZone_bit_position))
    //        {
    //            if (ZoneDefinitionStat != null)
    //            {
    //                stat = BitConverter.GetBytes((UInt16)ZoneDefinitionStat.Id);
    //                Array.Copy(econom, 0, resp, inx, econom.Length);
    //                inx += Marshal.SizeOf((UInt16)ZoneDefinitionStat.Id);
    //            }
    //            else
    //            {
    //                stat = BitConverter.GetBytes((UInt16)0);
    //                Array.Copy(econom, 0, resp, inx, econom.Length);
    //                inx += Marshal.SizeOf((UInt16)0);
    //            }
    //            //stat = BitConverter.GetBytes(StatZone);
    //            //Array.Copy(econom, 0, resp, inx, econom.Length);
    //            //inx += Marshal.SizeOf(StatZone);
    //        }

    //        // position gps
    //        if (THelp.CheckBits(Content, Pos_bit_position))
    //        {
    //            var posarr = Pos.GetData();
    //            Array.Copy(posarr, 0, resp, inx, posarr.Length);
    //            inx += posarr.Length;
    //        }

    //        // IDP sensors data
    //        var sensarr = Sens.GetBytes();
    //        Array.Copy(sensarr, 0, resp, inx, sensarr.Length);
    //        inx += sensarr.Length;

    //        Array.Resize(ref resp, inx);

    //        return resp;
    //    }

    //    /// <summary>
    //    /// Decode start of fishing trip data
    //    /// </summary>
    //    /// <param name="Data"></param>
    //    public void DecodePacket(byte[] Data)
    //    {
    //        try
    //        {
    //            using (var context = IoC.ApplicationDbContext)
    //            {
    //                int inx = 0;
    //                //byte content - 1 byte
    //                var cnt = Data[0];
    //                inx += Marshal.SizeOf(Content); // byte

    //                //uint timestamp - 4 bytes

    //                Timestamp = BitConverter.ToUInt32(Data, inx);
    //                inx += Marshal.SizeOf(Timestamp); // 4 bytes

    //                if (THelp.CheckBits(Content, bit_pos_include_disreccrdt))
    //                {
    //                    DisRecCrDTL = BitConverter.ToUInt32(Data, inx);
    //                    inx += Marshal.SizeOf(DisRecCrDTL); // 4 bytes                     
    //                }

    //                //ushort portid - 2 bytes                 
    //                if (THelp.CheckBits(Content, PortId_bit_position))
    //                {
    //                    var portid = BitConverter.ToUInt16(Data, inx);
    //                    inx += Marshal.SizeOf(portid); // 2 bytes  
    //                }

    //                // FGRec count - 1 byte
    //                var fgCount = Data[inx];
    //                inx += sizeof(byte);

    //                // FGears Data - N*14
    //                if (fgCount > 0)
    //                {

    //                    //FGRecs = new List<FGRec>();

    //                    for (int i = 0; i < fgCount; i++)
    //                    {
    //                        FGRec fGRec = new FGRec();
    //                        // fgrec data - 14 bytes
    //                        var fgsize = fGRec.FGRecSizOf;
    //                        byte[] tmpFGData = new byte[fgsize];
    //                        // copy data from Data array to tmp buffer
    //                        Array.Copy(Data, inx, tmpFGData, 0, fgsize);
    //                        // decode fgrec data
    //                        fGRec.DecodePacket(tmpFGData);
    //                        // add to list
    //                        SFTRecFGRec sdtfgrec = new SFTRecFGRec();
    //                        sdtfgrec.SFTRec = this;
    //                        sdtfgrec.FGRec = fGRec;


    //                        /////SFTRecFGRecs.Add(sdtfgrec);


    //                        //FGRecs.Add(fGRec);

    //                        inx += fgsize;
    //                    }
    //                }

    //                //byte Activity - 1 byte
    //                //Activity = Data[inx];

    //                var foundFA = context.FishingActivities.FirstOrDefault(x => x.Id == Data[inx]);
    //                FishingActivity = foundFA;
    //                inx += 1;



    //                //ushort economic zone - 2 bytes                 
    //                if (THelp.CheckBits(Content, EcoZone_bit_position))
    //                {
    //                    //EconZone = BitConverter.ToUInt16(Data, inx);
    //                    //inx += Marshal.SizeOf(EconZone);

    //                    var ezoneid = BitConverter.ToUInt16(Data, inx);
    //                    var foundez = context.ZoneDefinitions.FirstOrDefault(x => x.Id == ezoneid);
    //                    if (foundez != null)
    //                    {
    //                        ZoneDefinitionEcon = foundez;
    //                        inx += 2;
    //                    }
    //                    else
    //                    {
    //                        //TODO ???
    //                    }
    //                }

    //                //ushort - statistical zone - 2 bytes                 
    //                if (THelp.CheckBits(Content, StatZone_bit_position))
    //                {
    //                    //StatZone = BitConverter.ToUInt16(Data, inx);
    //                    //inx += Marshal.SizeOf(StatZone);

    //                    var szoneid = BitConverter.ToUInt16(Data, inx);
    //                    var foundsz = context.ZoneDefinitions.FirstOrDefault(x => x.Id == szoneid);
    //                    if (foundsz != null)
    //                    {
    //                        ZoneDefinitionStat = foundsz;
    //                        inx += 2;
    //                    }
    //                    else
    //                    {
    //                        //TODO: ?????
    //                    }
    //                }

    //                // position gps
    //                if (THelp.CheckBits(Content, Pos_bit_position))
    //                {
    //                    PosRec tmpPRec = new PosRec();
    //                    byte[] PosArr = new byte[tmpPRec.PosRecSizeOf];
    //                    // copy pos byte array
    //                    Array.Copy(Data, inx, PosArr, 0, tmpPRec.PosRecSizeOf);
    //                    // decode pos data
    //                    tmpPRec.DecodePacket(PosArr);
    //                    Pos = tmpPRec;

    //                    inx += tmpPRec.PosRecSizeOf;
    //                }

    //                // sens data 

    //                //SensRec tmpPRec = new SensRec();
    //                //byte[] PosArr = new byte[tmpPRec.PosRecSizeOf];
    //                //// copy pos byte array
    //                //Array.Copy(Data, inx, PosArr, 0, tmpPRec.PosRecSizeOf);
    //                //// decode pos data
    //                //tmpPRec.DecodePacket(PosArr);
    //                //Pos = tmpPRec;

    //                //inx += tmpPRec.PosRecSizeOf;

    //            }
    //        }
    //        catch (Exception)
    //        {
    //            //TODO: log error
    //        }
    //    }
    //}


    #endregion

    #region ELBProt < 2
    ///// <summary>
    ///// start of fishing trip record
    ///// full size - 26 + ( 14 * N)
    ///// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types) 
    ///// </summary>
    //public class SFTRec
    //{
    //    private uint timestamp;
    //    private Int64 timestamplite;
    //    private UInt16 portid;
    //    private int portidlite;
    //    private UInt16 econzone;
    //    private int econzonelite;
    //    private UInt16 statsozne;
    //    private int statzonelite;

    //    #region positions
    //    private const byte EcoZone_bit_position = 7;
    //    private const byte StatZone_bit_position = 6;
    //    private const byte StGen_bit_position = 5;
    //    private const byte Pos_bit_position = 4;
    //    private const byte TimeSt_bit_position = 3;
    //    private const byte PortId_bit_position = 2;
    //    #endregion

    //    /// <summary>
    //    /// SQLite PK
    //    /// </summary>
    //    //[Key]
    //    public int Id { get; set; }

    //    /// <summary>
    //    /// ???
    //    /// </summary>
    //    //[NotMapped]
    //    public string TimestampStr { get; set; }

    //    #region ScortelELBProtocol fields

    //    /// <summary>
    //    ///Bit 7 – Economic zone
    //    ///•	0 – excluded economicl zone field
    //    ///•	1 – included economic zone field
    //    ///Bit 6 – Statistical zone
    //    ///•	0 – excluded statistical zone field
    //    ///•	1 – included statistical zone field
    //    ///Bit 5 – Status generation of record
    //    ///•	0 – auto generated
    //    ///•	1 – manual generated
    //    ///Bit 4 – Position GPS part
    //    ///•	0 – excluded gps
    //    ///•	1 – included gps
    //    ///Bit 3 – Date and time field
    //    ///•	0 – excluded date and time field
    //    ///•	1 – included date and time field
    //    ///Bit 2 – Port ID
    //    ///•	0 – excluded port Id
    //    ///•	1 – included port id
    //    /// If GPS Position is included Timestamp field have to be excluded 
    //    /// </summary>
    //    public byte Content { get; set; }

    //    /// <summary>
    //    /// Timestamp since 2018.01.01 00:00:00 UTC in seconds
    //    /// </summary>      
    //    //[NotMapped]
    //    public uint Timestamp
    //    {
    //        get { return timestamp; }
    //        set
    //        {
    //            timestamp = value;
    //            timestamplite = value;
    //        }
    //    }

    //    /// <summary>
    //    /// Timestamp since 2018.01.01 00:00:00 UTC in seconds
    //    /// SQLite
    //    /// </summary>  
    //    public Int64 TimestampLite
    //    {
    //        get { return timestamplite; }
    //        set
    //        {
    //            timestamplite = value;
    //            timestamp = (uint)value;
    //        }
    //    }

    //    /// <summary>
    //    /// Sequence number from list of ports (0-65535)
    //    /// </summary>
    //    //[NotMapped]
    //    public UInt16 PortId
    //    {
    //        get { return portid; }
    //        set
    //        {
    //            portid = value;
    //            portidlite = value;
    //        }
    //    }

    //    /// <summary>
    //    /// Sequence number from list of ports (0-65535)
    //    /// SQLite
    //    /// </summary>
    //    public int PortIdLite
    //    {
    //        get { return portidlite; }
    //        set
    //        {
    //            portidlite = value;
    //            portid = (UInt16)value;
    //        }
    //    }

    //    /// <summary>
    //    /// List of fishing gears
    //    /// </summary>
    //    public List<FGRec> FGRecs { get; set; } = new List<FGRec>();

    //    /// <summary>
    //    /// Sequence number from list activities (0-255)
    //    /// </summary>
    //    public byte Activity { get; set; }

    //    /// <summary>
    //    /// Sequence number from list economic zones (0-65535)
    //    ///. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
    //    /// </summary>
    //    //[NotMapped]
    //    public UInt16 EconZone
    //    {
    //        get { return econzone; }
    //        set
    //        {
    //            econzone = value;
    //            econzonelite = value;
    //        }
    //    }

    //    /// <summary>
    //    /// Sequence number from list economic zones (0-65535)
    //    ///. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
    //    /// SQLite
    //    /// </summary>
    //    public int EconZoneLite
    //    {
    //        get { return econzonelite; }
    //        set
    //        {
    //            econzonelite = value;
    //            econzone = (UInt16)value;
    //        }
    //    }

    //    /// <summary>
    //    /// Sequence number from list statistical or other zones (0-65535)
    //    ///. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
    //    /// </summary>
    //    //[NotMapped]
    //    public UInt16 StatZone
    //    {
    //        get { return statsozne; }
    //        set
    //        {
    //            statsozne = value;
    //            statzonelite = value;
    //        }
    //    }

    //    /// <summary>
    //    /// Sequence number from list statistical or other zones (0-65535)
    //    ///. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
    //    ///SQLite
    //    /// </summary>
    //    public int StatZoneLite
    //    {
    //        get { return statzonelite; }
    //        set
    //        {
    //            statzonelite = value;
    //            statsozne = (UInt16)value;
    //        }
    //    }

    //    /// <summary>
    //    /// Gps position part
    //    /// </summary>
    //    public PosRec Pos { get; set; } = new PosRec();

    //    /// <summary>
    //    /// Start fishing trip structure - creation status
    //    /// </summary>
    //    //[NotMapped]
    //    public string CreationStatus
    //    {
    //        get
    //        {
    //            if (THelp.CheckBits(Content, StGen_bit_position))
    //            {
    //                return "ръчно";
    //            }
    //            else
    //            {
    //                return "автоматично";
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Start fishing trip structure - sent status
    //    /// </summary>
    //    public int SentStatus { get; set; }
    //    //[NotMapped]
    //    public string SentStatusStr
    //    {
    //        get
    //        {
    //            switch (SentStatus)
    //            {
    //                case 0:
    //                    return "не е изпратено";
    //                case 1:
    //                    return "изпратено";
    //                case 2:
    //                    return "";
    //                case 3:
    //                    return "";
    //                default:
    //                    return "";
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Start fishing trip structure - completion status
    //    /// </summary>
    //    public int CompletionStatus { get; set; }

    //    public DateTime CreationDT { get; set; }

    //    //[NotMapped]
    //    public string CompletionStatusStr
    //    {
    //        get
    //        {
    //            switch (CompletionStatus)
    //            {
    //                case 0:
    //                    return "не е завършено";
    //                case 1:
    //                    return "завършено";
    //                case 2:
    //                    return "";
    //                case 3:
    //                    return "";
    //                default:
    //                    return "";
    //            }
    //        }
    //    }

    //    #endregion

    //    #region Content func

    //    /// <summary>
    //    /// Content func - exclude economic zone
    //    /// </summary>
    //    public void ExcludeEconZone()
    //    {
    //        Content = THelp.ResetBits(Content, EcoZone_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - include  economic zone
    //    /// </summary>
    //    public void IncludeEconZone()
    //    {
    //        Content = THelp.SetBits(Content, EcoZone_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - exlude statistical zone
    //    /// </summary>
    //    public void ExcludeStatZone()
    //    {
    //        Content = THelp.ResetBits(Content, StatZone_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - include statistical zone
    //    /// </summary>
    //    public void IncludeStatZone()
    //    {
    //        Content = THelp.SetBits(Content, StatZone_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - exlude gps pos
    //    /// </summary>
    //    public void ExcludeGPSPos()
    //    {
    //        Content = THelp.ResetBits(Content, Pos_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - include gps pos
    //    /// </summary>
    //    public void IncludeGPSPos()
    //    {
    //        Content = THelp.SetBits(Content, Pos_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - auto generated structure
    //    /// </summary>
    //    public void SetAutoGenerated()
    //    {
    //        Content = THelp.ResetBits(Content, StGen_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - manual generated structure
    //    /// </summary>
    //    public void SetManualGenerated()
    //    {
    //        Content = THelp.SetBits(Content, StGen_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - exlude timestamp field 
    //    /// </summary>
    //    public void ExcludeTimestamp()
    //    {
    //        Content = THelp.ResetBits(Content, TimeSt_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - include timstamp field
    //    /// </summary>
    //    public void IncludeTimestamp()
    //    {
    //        Content = THelp.SetBits(Content, TimeSt_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - exlude port id 
    //    /// </summary>
    //    public void ExcludePortId()
    //    {
    //        Content = THelp.ResetBits(Content, PortId_bit_position);
    //    }

    //    /// <summary>
    //    /// Content func - include port id
    //    /// </summary>
    //    public void IncludePortId()
    //    {
    //        Content = THelp.SetBits(Content, PortId_bit_position);
    //    }
    //    #endregion

    //    /// <summary>
    //    /// Convert data field to byte array
    //    /// </summary>
    //    /// <returns></returns>
    //    public byte[] GetData()
    //    {
    //        int inx = 0;
    //        byte[] resp = new byte[500];

    //        // content - 1 byte
    //        resp[inx] = Content;
    //        inx += 1;

    //        // timestamp - 4 bytes
    //        byte[] time = new byte[4] { 0, 0, 0, 0 };
    //        if (THelp.CheckBits(Content, TimeSt_bit_position))
    //        {
    //            time = BitConverter.GetBytes(Timestamp);
    //            Array.Copy(time, 0, resp, inx, time.Length);
    //            inx += Marshal.SizeOf(Timestamp);
    //        }

    //        // port id - 2 bytes
    //        byte[] portid = new byte[2] { 0, 0 };
    //        if (THelp.CheckBits(Content, PortId_bit_position))
    //        {
    //            portid = BitConverter.GetBytes(PortId);
    //            Array.Copy(portid, 0, resp, inx, portid.Length);
    //            inx += Marshal.SizeOf(PortId);
    //        }

    //        // FGRecs
    //        if (FGRecs != null)
    //        {
    //            if (FGRecs.Count == 0)
    //            {
    //                // fishing gear count = 0 -> no fg data next byte is activity
    //                resp[inx] = 0;
    //                inx += sizeof(byte);
    //            }
    //            else
    //            {
    //                // set fgear count 
    //                resp[inx] = (byte)FGRecs.Count;
    //                inx += sizeof(byte);
    //                // fishing gear data
    //                foreach (FGRec fg in FGRecs)
    //                {
    //                    var fgarr = fg.GetData();
    //                    Array.Copy(fgarr, 0, resp, inx, fgarr.Length);
    //                    inx += fgarr.Length;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            // fishing gear count = 0 -> no fg data next byte is activity
    //            resp[inx] = 0;
    //            inx += sizeof(byte);
    //        }

    //        //Activity
    //        resp[inx] = Activity;
    //        inx += 1;

    //        // economic zone
    //        byte[] econom = new byte[2] { 0, 0 };
    //        if (THelp.CheckBits(Content, EcoZone_bit_position))
    //        {
    //            econom = BitConverter.GetBytes(EconZone);
    //            Array.Copy(econom, 0, resp, inx, econom.Length);
    //            inx += Marshal.SizeOf(EconZone);
    //        }

    //        // statistical zone
    //        byte[] stat = new byte[2] { 0, 0 };
    //        if (THelp.CheckBits(Content, StatZone_bit_position))
    //        {
    //            stat = BitConverter.GetBytes(StatZone);
    //            Array.Copy(econom, 0, resp, inx, econom.Length);
    //            inx += Marshal.SizeOf(StatZone);
    //        }

    //        // position gps
    //        if (THelp.CheckBits(Content, Pos_bit_position))
    //        {
    //            var posarr = Pos.GetData();
    //            Array.Copy(posarr, 0, resp, inx, posarr.Length);
    //            inx += posarr.Length;
    //        }

    //        Array.Resize(ref resp, inx);

    //        return resp;
    //    }

    //    /// <summary>
    //    /// Decode start of fishing trip data
    //    /// </summary>
    //    /// <param name="Data"></param>
    //    public void DecodePacket(byte[] Data)
    //    {
    //        try
    //        {
    //            int inx = 0;
    //            //byte content - 1 byte
    //            var cnt = Data[0];
    //            inx += Marshal.SizeOf(Content); // byte

    //            //uint timestamp - 4 bytes
    //            if (THelp.CheckBits(Content, TimeSt_bit_position))
    //            {
    //                Timestamp = BitConverter.ToUInt32(Data, inx);
    //                inx += Marshal.SizeOf(Timestamp); // 4 bytes
    //            }

    //            //ushort portid - 2 bytes                 
    //            if (THelp.CheckBits(Content, PortId_bit_position))
    //            {
    //                PortId = BitConverter.ToUInt16(Data, inx);
    //                inx += Marshal.SizeOf(PortId); // 2 bytes
    //            }

    //            // FGRec count - 1 byte
    //            var fgCount = Data[inx];
    //            inx += sizeof(byte);

    //            // FGears Data - N*14
    //            if (fgCount > 0)
    //            {
    //                FGRecs = new List<FGRec>();

    //                for (int i = 0; i < fgCount; i++)
    //                {
    //                    FGRec fGRec = new FGRec();
    //                    // fgrec data - 14 bytes
    //                    var fgsize = fGRec.FGRecSizOf;
    //                    byte[] tmpFGData = new byte[fgsize];
    //                    // copy data from Data array to tmp buffer
    //                    Array.Copy(Data, inx, tmpFGData, 0, fgsize);
    //                    // decode fgrec data
    //                    fGRec.DecodePacket(tmpFGData);
    //                    // add to list
    //                    FGRecs.Add(fGRec);

    //                    inx += fgsize;
    //                }
    //            }

    //            //byte Activity - 1 byte
    //            Activity = Data[inx];
    //            inx += Marshal.SizeOf(Activity);

    //            //ushort economic zone - 2 bytes                 
    //            if (THelp.CheckBits(Content, EcoZone_bit_position))
    //            {
    //                EconZone = BitConverter.ToUInt16(Data, inx);
    //                inx += Marshal.SizeOf(EconZone);
    //            }

    //            //ushort - statistical zone - 2 bytes                 
    //            if (THelp.CheckBits(Content, StatZone_bit_position))
    //            {
    //                StatZone = BitConverter.ToUInt16(Data, inx);
    //                inx += Marshal.SizeOf(StatZone);
    //            }

    //            // position gps
    //            if (THelp.CheckBits(Content, Pos_bit_position))
    //            {
    //                PosRec tmpPRec = new PosRec();
    //                byte[] PosArr = new byte[tmpPRec.PosRecSizeOf];
    //                // copy pos byte array
    //                Array.Copy(Data, inx, PosArr, 0, tmpPRec.PosRecSizeOf);
    //                // decode pos data
    //                tmpPRec.DecodePacket(PosArr);
    //                Pos = tmpPRec;

    //                inx += tmpPRec.PosRecSizeOf;
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            //TODO: log error
    //        }
    //    }
    //}

    #endregion
}
