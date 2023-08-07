//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using ScortelApi.Tools;
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
    /// Start/ end fishing operation record
    /// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types) 
    /// ELBProtocol 2.7
    /// </summary>
    public class SEFORec
    {
        #region fields
        private uint timestamp;
        private Int64 timstamplite;
        // econ zone
        private UInt16 econzone;
        private int econzonelite;
        // stat zone
        private UInt16 statzone;
        private int statzonelite;
        private byte fgcount;
        private List<FGRec> fglist = new List<FGRec>();
        // creationdt
        private DateTime creationdt;
        // discardedrecdt        
        private UInt32 drdtl;
        private DateTime drdt;
        #endregion

        #region bit pos
        private const byte IsSetFG_bit_position = 7;
        private const byte StatandEcoZone_bit_position = 6;
        private const byte StGen_bit_position = 5;
        private const byte Pos_bit_position = 4;
        private const byte SEFO_bit_position = 3;
        public const byte bit_pos_datacorrection = 2;
        public const byte bit_pos_include_disreccrdt = 1;
        #endregion

        /// <summary>
        /// SQLite PK id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Comm protocol version
        /// 0 - 3.0.0.8 and older
        /// 1 - 3.0.0.9
        /// </summary>
        public byte ProtocolVersion { get; set; }

        /// <summary>
        ///Bit 7
        ///•	0 - normal fishing operation with catch
        ///•	1 – set only fishing gear and go back to port without catch
        ///Bit 6
        ///•	0 – excluded statistical and economic zone field
        ///•	1 – included statistical and economic zone field
        ///Bit 5 – Status generation of record
        ///•	0 – auto generated
        ///•	1 – manual generated
        ///Bit 4 – Position GPS part
        ///•	0 – excluded
        ///•	1 – included
        ///Bit 3 – Start end FO
        ///•	0 – start FO
        ///•	1 – end FO
        ///Bit 2 – Data Correction
        ///•	0 – no
        ///•	1 – yes
        ///Bit 1 
        ///•	0 – DiscardedRecCrDT – emptry - exclude
        ///•	1 – DiscardedRecCrDT – with data - include
        /// </summary>
        //[Index("IX_SEFORecUnq", 1, IsUnique = true)]
        public byte Content { get; set; }

        /// <summary>
        ///  Size of fishing gear list
        /// </summary>
        public byte FGCount
        {
            get { return fgcount; }
            set
            {
                try
                {
                    //fgcount = (byte)FGRecs.Count;
                    if (FGRecSEFORecs != null)
                    {
                        fgcount = (byte)FGRecSEFORecs.Count;
                    }
                    else
                    {
                        fgcount = 0;
                    }
                }
                catch (Exception)
                {
                    fgcount = 0;
                }
            }
        }

        public virtual ICollection<FGRecSEFORec> FGRecSEFORecs { get; set; }
  
        #region Econ Zone
        /// <summary>
        /// Sequence number from list economic zones (0-65535)
        ///. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
        /// </summary>
        [NotMapped]
        public UInt16 EconZone
        {
            get { return econzone; }
            set
            {
                try
                {
                    if (ZoneDefinitionEcon != null)
                    {
                        if (ZoneDefinitionEcon.ZoneType.Code == 1)
                        {
                            econzone = (UInt16)ZoneDefinitionEcon.Id;
                        }
                        else
                        {
                            econzone = 0;
                        }
                    }
                }
                catch (Exception)
                {
                    econzone = 0;
                }
            }
        }

        public ZoneDefinition ZoneDefinitionEcon { get; set; }

        ////[NotMapped]
        ////public UInt16 EconZone
        ////{
        ////    get { return econzone; }
        ////    set
        ////    {
        ////        econzone = value;
        ////        econzonelite = value;
        ////    }
        ////}

        /////// <summary>
        /////// Sequence number from list economic zones (0-65535)
        ///////. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
        ///////SQLite
        /////// </summary>
        ////public int EconZoneLite
        ////{
        ////    get { return econzonelite; }
        ////    set
        ////    {
        ////        econzonelite = value;
        ////        econzone = (UInt16)value;
        ////    }
        ////}

        #endregion

        #region Stat zone
        /// <summary>
        /// Sequence number from list statistical or other zones (0-65535)
        ///. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
        /// </summary>

        [NotMapped]
        public UInt16 StatZone
        {
            get { return statzone; }
            set
            {
                try
                {
                    if (ZoneDefinitionStat != null)
                    {
                        if (ZoneDefinitionStat.ZoneType.Code == 2)
                        {
                            statzone = (UInt16)ZoneDefinitionStat.Id;
                        }
                        else
                        {
                            statzone = 0;
                        }
                    }
                }
                catch (Exception)
                {
                    statzone = 0;
                }
            }
        }

        public ZoneDefinition ZoneDefinitionStat { get; set; }

        ////[NotMapped]
        ////public UInt16 StatZone
        ////{
        ////    get { return statzone; }
        ////    set
        ////    {
        ////        statzone = value;
        ////        statzonelite = value;
        ////    }
        ////}

        /////// <summary>
        /////// Sequence number from list statistical or other zones (0-65535)
        ///////. Can be omit the and the calculation to be performed on the server based on geographic coordinates.
        ///////SQLite
        /////// </summary>
        ////public int StatZoneLite
        ////{
        ////    get { return statzonelite; }
        ////    set
        ////    {
        ////        statzonelite = value;
        ////        statzone = (UInt16)value;
        ////    }
        ////}
        #endregion

        /// <summary>
        /// Gps position part
        /// </summary>
        public PosRec Pos { get; set; }

        /// <summary>
        /// Timestamp since 2018.01.01 00:00:00 UTC in seconds
        /// </summary>
        [NotMapped]
        public uint Timestamp
        {
            get { return timestamp; }
            set
            {
                timestamp = value;
                timstamplite = value;
                creationdt = THelp.DateTimeFromELBTimestamp(timestamp);
            }
        }

        /// <summary>
        /// Timestamp since 2018.01.01 00:00:00 UTC in seconds
        /// </summary>
        //[Index("IX_SEFORecUnq", 2, IsUnique = true)]
        public Int64 TimestampLite
        {
            get { return timstamplite; }
            set
            {
                timstamplite = value;
                timestamp = (uint)value;
            }
        }

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

        /// <summary>
        /// Fishing operation depth
        /// meters * 10
        /// </summary>
        public byte FODepth { get; set; }

        /// <summary>
        /// Start/End fishing operation structure - creation status
        /// </summary>
        [NotMapped]
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
        /// Start/ End fishing operation structure - sent status
        /// </summary>
        public int SentStatus { get; set; }
        [NotMapped]
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
        /// Start/End fishing operation structure - completion status
        /// </summary>
        public int CompletionStatus { get; set; }
        [NotMapped]
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
        /// Received to server
        /// </summary>
        public DateTime ReceivedTimestamp { get; set; }

        /// <summary>
        /// IDP Sensors data
        /// </summary>
        public SensRec Sens { get; set; }

        #region Content Funcs

        /// <summary>
        /// Include economic zone in record
        /// </summary>
        public void IncludeEconZone()
        {
            Content = THelp.SetBits(Content, StatandEcoZone_bit_position);
        }

        /// <summary>
        /// Exclude economic zone from record
        /// </summary>
        public void ExcludeEconZone()
        {
            Content = THelp.ResetBits(Content, StatandEcoZone_bit_position);
        }

        /// <summary>
        /// Include statistical zone in record
        /// </summary>
        public void IncludeStatZone()
        {
            Content = THelp.SetBits(Content, StatandEcoZone_bit_position);
        }

        /// <summary>
        /// Exclude statistical zone from record
        /// </summary>
        public void ExcludeStatZone()
        {
            Content = THelp.ResetBits(Content, StatandEcoZone_bit_position);
        }

        /// <summary>
        /// Include Pos in record
        /// </summary>
        public void IncludePos()
        {
            Content = THelp.SetBits(Content, Pos_bit_position);
        }

        /// <summary>
        /// Exclude Pos from record
        /// </summary>
        public void ExcludePos()
        {
            Content = THelp.ResetBits(Content, Pos_bit_position);
        }

        /// <summary>
        /// Set record as start fishing operation
        /// </summary>
        public void SetStartFOperation()
        {
            Content = THelp.ResetBits(Content, SEFO_bit_position);
        }

        /// <summary>
        /// Set record as end of fishing operation
        /// </summary>
        public void SetEndFOperation()
        {
            Content = THelp.SetBits(Content, SEFO_bit_position);
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
            try
            {
                int inx = 0;
                byte[] resp = new byte[500];

                // content - 1 byte
                resp[inx] = Content;
                inx += 1;

                // fishing gear list count
                if (fgcount != FGRecSEFORecs.Count)
                {
                    resp[inx] = (byte)FGRecSEFORecs.Count;
                    inx += 1;
                }
                else
                {
                    resp[inx] = fgcount;
                    inx += 1;
                }

                // fishing gear
                if (THelp.CheckBits(Content, SEFO_bit_position))
                {
                    // end of fishing operation
                    // no fishing gears data
                }
                else
                {
                    foreach (var fg in FGRecSEFORecs)
                    {
                        
                         
                        //var fgarr = fg.GetData();
                        //Array.Copy(fgarr, 0, resp, inx, fgarr.Length);
                        //inx += fgarr.Length;
                    }
                }

                // economic zone
                byte[] econom = new byte[2] { 0, 0 };
                if (THelp.CheckBits(Content, StatandEcoZone_bit_position))
                {
                    econom = BitConverter.GetBytes(EconZone);
                    Array.Copy(econom, 0, resp, inx, econom.Length);
                    inx += Marshal.SizeOf(EconZone);
                  
                    // statistical zone
                    byte[] stat = new byte[2] { 0, 0 };
                 
                    stat = BitConverter.GetBytes(StatZone);
                    Array.Copy(econom, 0, resp, inx, econom.Length);
                    inx += Marshal.SizeOf(StatZone);
                }

                // position gps
                if (THelp.CheckBits(Content, Pos_bit_position))
                {
                    var posarr = Pos.GetData();
                    Array.Copy(posarr, 0, resp, inx, posarr.Length);
                    inx += posarr.Length;
                }

                //depth
                resp[inx] = FODepth;
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
