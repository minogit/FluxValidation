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
    /// Vessel information record 
    /// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types)
    /// 
    /// Protocol 2.0.0
    /// </summary>
    public class VRec : IVRec
    {
        #region fields
        private string vesselnamestr;
        private string descriptionstr;
        private string externalmarkstr;
        private string mmsistr;
        private string cfrstr;
        private string callsignstr;
        private string licensestr;
        private string radiolicstr;
        private string rssnrstr;
        private string vesselownerstr;
        private string vesselcompanyaddr_EIKstr;
        private UInt16 homeport;
        private int homeportlite;
        private UInt16 regport;
        private int regportlite;
        // creation dt         
        private UInt32 crdtl;
        private DateTime crdt;
        // discardedrecdt        
        private UInt32 drdtl;
        private DateTime drdt;
        #endregion

        #region content bit positions
        // Content 1
        private const byte vname_bit_pos = 7;
        private const byte vdesc_bit_pos = 6;
        private const byte country_bit_pos = 5;
        private const byte ext_mark_bit_pos = 4;
        private const byte mmsi_bit_pos = 3;
        private const byte cfr_bit_pos = 2;
        private const byte rcallsign_bit_pos = 1;
        private const byte license_bit_pos = 0;
        // Content 2
        private const byte radiolic_bit_pos = 7;
        private const byte rss_bit_pos = 6;
        private const byte hport_bit_pos = 5;
        private const byte rport_bit_pos = 4;
        private const byte vlen_bit_pos = 3;
        private const byte vwidth_bit_pos = 2;
        private const byte draft_bit_pos = 1;
        private const byte tonage_bit_pos = 0;
        // Content 3
        private const byte owner_bit_pos = 7;
        private const byte company_bit_pos = 6;
        public const byte bit_pos_datacorrection = 2;
        public const byte bit_pos_include_disreccrdt = 1;
        #endregion

        /// <summary>
        /// SQLite PK id
        /// </summary>    
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Bit 7
        ///•	0 – excluded vessel name
        ///•	1 – included vessel name
        ///Bit 6
        ///•	0 – excluded vessel description
        ///•	1 – included vessel description
        ///Bit 5
        ///•	0 – excluded vessel country
        ///•	1 – included vessel country
        ///Bit 4
        ///•	0 – exluded vessel external mark
        ///•	1 – included vessel external mark
        ///Bit 3
        ///•	0 – excluded vessel MMSI
        ///•	1 – included vessel MMSI
        ///Bit 2
        ///•	0 – excluded vessel radio call sign
        ///•	1 – included vessel radio call sign
        ///Bit 1
        ///•	0 – excluded RSS number
        ///•	1 – included RSS number
        ///Bit 0  - Home port
        ///•	0 – excluded home port
        ///•	1 – included home port
        /// </summary>
        public byte Content_1 { get; set; } = 0;

        /// <summary>
        ///Bit 7
        ///•	0 – excluded Radio License
        ///•	1 – included Radio License
        ///Bit 6
        ///•	0 – excluded RSS number
        ///•	1 – included RSS number
        ///Bit 5
        ///•	0 – excluded home port
        ///•	1 – included home port
        ///Bit 4
        ///•	0 – excluded reg port
        ///•	1 – included reg port
        ///Bit 3
        ///•	0 – excluded vessel length
        ///•	1 – included vessel length
        ///Bit 2
        ///•	0 – excluded vessel width
        ///•	1 – included vessel width
        ///Bit 1
        ///•	0 – excluded vessel draft
        ///•	1 – included vessel draft
        ///Bit 0
        ///•	0 – excluded vessel tonage
        ///•	1 – included vessel tonage
        /// </summary>
        public byte Content_2 { get; set; } = 0;

        /// <summary>
        ///Bit 7
        ///•	0 – excluded Vessel Owner
        ///•	1 – included Vessel Owner
        ///Bit 6
        ///•	0 – excluded Vessel address
        ///•	1 – included Vessel address
        ///Bit 2 – Data correction
        ///•	0 – no correction of data
        ///•	1 – with correction
        ///Bit 1 
        ///•	0 – DiscardedRecCrDT – emptry - exclude
        ///•	1 – DiscardedRecCrDT – with data - include
        /// </summary>
        public byte Content_3 { get; set; } = 0;

        /// <summary>
        /// Vessel name in byte format
        /// First byte is the length of data
        /// </summary>
        [NotMapped]
        public byte[] VesselName { get; set; }

        /// <summary>
        /// Vessel name in string format
        /// </summary>
        //[Index("IX_VesselUnq", 1, IsUnique = true)]
        public string VesselNameStr
        {
            get
            {
                if (VesselName != null)
                {
                    return Encoding.UTF8.GetString(VesselName, 1, VesselName.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                vesselnamestr = value;
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
                    VesselName = tnum;
                }
            }
        }

        /// <summary>
        /// Vessel description in byte format
        /// First byte is length of data
        /// </summary>
        [NotMapped]
        public byte[] Description { get; set; }

        /// <summary>
        /// Vessel description in string format
        /// </summary>
        public string DescriptionStr
        {
            get
            {
                if (Description != null)
                {
                    return Encoding.UTF8.GetString(Description, 1, Description.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                descriptionstr = value;
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
                    Description = tnum;
                }
            }
        }

        /// <summary>
        /// Vessel country
        /// </summary>
        public byte Country { get; set; }

        /// <summary>
        /// External mark in byte format
        /// First byte is the length of the field
        /// </summary>
        [NotMapped]
        public byte[] ExternalMark { get; set; }

        /// <summary>
        /// External mark in string format
        /// </summary>
        //[Index("IX_VesselUnq", 2, IsUnique = true)]
        public string ExternalMarkStr
        {
            get
            {
                if (ExternalMark != null)
                {
                    return Encoding.UTF8.GetString(ExternalMark, 1, ExternalMark.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                externalmarkstr = value;
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
                    ExternalMark = tnum;
                }
            }
        }

        /// <summary>
        /// Vessel MMSI in byte format
        /// First byte is the length of data
        /// </summary>
        [NotMapped]
        public byte[] MMSI { get; set; }

        /// <summary>
        /// Vessel MMSI in string format
        /// </summary>
        public string MMSIStr
        {
            get
            {
                if (MMSI != null)
                {
                    return Encoding.UTF8.GetString(MMSI, 1, MMSI.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                mmsistr = value;
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
                    MMSI = tnum;
                }
            }
        }

        /// <summary>
        /// Vessel CFR in byte format
        /// First byte is the length of data
        /// </summary>
        [NotMapped]
        public byte[] CFR { get; set; }

        /// <summary>
        /// Vessel CFR in string format
        /// </summary>
        public string CFRStr
        {
            get
            {
                if (CFR != null)
                {
                    return Encoding.UTF8.GetString(CFR, 1, CFR.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                cfrstr = value;
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
                    CFR = tnum;
                }
            }
        }

        /// <summary>
        /// Vessel CallSign in byte format
        /// First byte is the length of data
        /// </summary>
        [NotMapped]
        public byte[] CallSign { get; set; }

        /// <summary>
        /// Vessel CallSign in string format
        /// </summary>
        public string CallSignStr
        {
            get
            {
                if (CallSign != null)
                {
                    return Encoding.UTF8.GetString(CallSign, 1, CallSign.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                callsignstr = value;
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
                    CallSign = tnum;
                }
            }
        }

        /// <summary>
        /// Vessel License
        /// </summary>
        [NotMapped]
        public byte[] License { get; set; }

        /// <summary>
        /// Vessel License in string format
        /// </summary>
        public string LicenseStr
        {
            get
            {
                if (License != null)
                {
                    return Encoding.UTF8.GetString(License, 1, License.Length - 1);
                }
                else
                { return ""; }
            }
            set
            {
                licensestr = value;
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
                    License = tnum;
                }
            }
        }

        /// <summary>
        /// Vessel radio license in byte format
        /// </summary>
        [NotMapped]
        public byte[] RadioLic { get; set; }

        /// <summary>
        /// Vessel radio license in string format
        /// </summary>
        public string RadioLicStr
        {
            get
            {
                if (RadioLic != null)
                {
                    return Encoding.UTF8.GetString(RadioLic, 1, RadioLic.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                radiolicstr = value;
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
                    RadioLic = tnum;
                }
            }
        }

        /// <summary>
        /// Vessel RSSNr for UK and Norway in byte format
        /// </summary>
        [NotMapped]
        public byte[] RSSNr { get; set; }

        /// <summary>
        /// Vessel RSSNr for UK and Norway in string format
        /// </summary>
        public string RSSNrStr
        {
            get
            {
                if (RSSNr != null)
                {
                    return Encoding.UTF8.GetString(RSSNr, 1, RSSNr.Length - 1);
                }
                else
                {
                    return "";
                }

            }
            set
            {
                rssnrstr = value;
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
                    RSSNr = tnum;
                }
            }
        }

        #region Home port

        public LPorts HPort { get; set; }
        #endregion

        #region Reg port

        public LPorts RPort { get; set; }

        #endregion

        /// <summary>
        /// Vessel Length
        /// </summary>
        public float VesselLen { get; set; }

        /// <summary>
        /// Vessel width
        /// </summary>
        public float VesselWidth { get; set; }

        /// <summary>
        /// Vessel draft
        /// </summary>
        public float VesselDraft { get; set; }

        /// <summary>
        /// Vessel gross tonage
        /// </summary>
        public float VesselGrossTonage { get; set; }

        /// <summary>
        /// Vessel owner
        /// </summary>
        [NotMapped]
        public byte[] VesselOwner
        {
            get; set;
        }

        /// <summary>
        /// Vessel owner SQLite
        /// </summary>
        public string VesselOwnerStr
        {
            get
            {
                if (VesselOwner != null)
                {
                    return Encoding.UTF8.GetString(VesselOwner, 1, VesselOwner.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                vesselownerstr = value;
                if (value != null)
                {
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
                        VesselOwner = tnum;
                    }
                }
            }
        }

        /// <summary>
        /// Vessel company address eik
        /// </summary>
        [NotMapped]
        public byte[] VesselCompanyAddr_EIK { get; set; }

        /// <summary>
        /// Vessel company address/ eik SQLite
        /// </summary>
        public string VesselCompanyAddr_EIKStr
        {
            get
            {
                if (VesselCompanyAddr_EIK != null)
                {
                    return Encoding.UTF8.GetString(VesselCompanyAddr_EIK, 1, VesselCompanyAddr_EIK.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                vesselcompanyaddr_EIKstr = value;
                if (value != null)
                {
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
                        VesselCompanyAddr_EIK = tnum;
                    }
                }
            }
        }

        /// <summary>
        /// List of crew members
        /// </summary>
        public List<CMRec> CMRecs { get; set; } = new List<CMRec>();

        public byte Favorite { get; set; }

        public byte Own { get; set; }

        #region CreationDT

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
        public DateTime? DisRecCrDT
        {
            get
            {
                return drdt;
            }
            set
            {
                if (value == null)
                {
                    //drdt = null;
                    drdtl = 0;
                }
                else
                {
                    drdt = (DateTime)value;
                    drdtl = THelp.ELBTimeFormat((DateTime)value);
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

        #endregion

        public DateTime ServerDT { get; set; }

        #region Content funcs

        /// <summary>
        /// Content 1 - bit 7 - vessel name
        /// </summary>
        public void IncludeVesselName()
        {
            Content_1 = THelp.SetBits(Content_1, vname_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void ExcludeVesselName()
        {
            Content_1 = THelp.ResetBits(Content_1, vname_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void IncludedVesselDescription()
        {
            Content_1 = THelp.SetBits(Content_1, vdesc_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void ExcludedVesselDescription()
        {
            Content_1 = THelp.ResetBits(Content_1, vdesc_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void IncludedVesselCountry()
        {
            Content_1 = THelp.SetBits(Content_1, country_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void ExcludedVesselCountry()
        {
            Content_1 = THelp.ResetBits(Content_1, country_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void IncludedExternalMark()
        {
            Content_1 = THelp.SetBits(Content_1, ext_mark_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void ExcludedExternalMark()
        {
            Content_1 = THelp.ResetBits(Content_1, ext_mark_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void IncludedMMSI()
        {
            Content_1 = THelp.SetBits(Content_1, mmsi_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void ExcludedMMSI()
        {
            Content_1 = THelp.ResetBits(Content_1, mmsi_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void IncludedCFR()
        {
            Content_1 = THelp.SetBits(Content_1, cfr_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void ExcludedCFR()
        {
            Content_1 = THelp.ResetBits(Content_1, cfr_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void IncludedRCallSign()
        {
            Content_1 = THelp.SetBits(Content_1, rcallsign_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void ExcludedRCallSign()
        {
            Content_1 = THelp.ResetBits(Content_1, rcallsign_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void IncludedLicense()
        {
            Content_1 = THelp.SetBits(Content_1, license_bit_pos);
        }

        /// <summary>
        /// Content 1 func
        /// </summary>
        public void ExcludedLicense()
        {
            Content_1 = THelp.ResetBits(Content_1, license_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void IncludeRadioLicense()
        {
            Content_2 = THelp.SetBits(Content_2, radiolic_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void ExcludedRadioLicense()
        {
            Content_2 = THelp.ResetBits(Content_2, radiolic_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void IncludedRSS()
        {
            Content_2 = THelp.SetBits(Content_2, rss_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void ExcludedRSS()
        {
            Content_2 = THelp.ResetBits(Content_2, rss_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void IncludedHomePort()
        {
            Content_2 = THelp.SetBits(Content_2, hport_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void ExcludedHomePort()
        {
            Content_2 = THelp.ResetBits(Content_2, hport_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void IncludedRegPort()
        {
            Content_2 = THelp.SetBits(Content_2, rport_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void ExcludedRegPort()
        {
            Content_2 = THelp.ResetBits(Content_2, rport_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void IncludedLength()
        {
            Content_2 = THelp.SetBits(Content_2, vlen_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void ExcludedLength()
        {
            Content_2 = THelp.ResetBits(Content_2, vlen_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void IncludedWidth()
        {
            Content_2 = THelp.SetBits(Content_2, vwidth_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void ExcludedWidth()
        {
            Content_2 = THelp.ResetBits(Content_2, vwidth_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void IncludedDraft()
        {
            Content_2 = THelp.SetBits(Content_2, draft_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void ExcludedDraft()
        {
            Content_2 = THelp.ResetBits(Content_2, draft_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void IncludedTonage()
        {
            Content_2 = THelp.SetBits(Content_2, tonage_bit_pos);
        }

        /// <summary>
        /// Content 2 func
        /// </summary>
        public void ExcludedTonage()
        {
            Content_2 = THelp.ResetBits(Content_2, tonage_bit_pos);
        }

        /// <summary>
        /// Content 3 func
        /// </summary>
        public void IncludedOwner()
        {
            Content_3 = THelp.SetBits(Content_3, owner_bit_pos);
        }

        /// <summary>
        /// Content 3 func
        /// </summary>
        public void ExcludedOwner()
        {
            Content_3 = THelp.ResetBits(Content_3
                , owner_bit_pos);
        }

        /// <summary>
        /// Content 3 func
        /// </summary>
        public void IncludedCompanyAddress()
        {
            Content_3 = THelp.SetBits(Content_3, company_bit_pos);
        }

        /// <summary>
        /// Content 3 func
        /// </summary>
        public void ExcludedCompanyAddress()
        {
            Content_3 = THelp.ResetBits(Content_3, company_bit_pos);
        }

        /// <summary>
        /// Content func - no correction of data, Bit2 = 0
        /// </summary>
        public void SetNoDataCorrection()
        {
            Content_3 = THelp.ResetBits(Content_3, bit_pos_datacorrection);
        }

        /// <summary>
        /// Content func - with data correction, Bit2 = 1
        /// </summary>
        public void SetDataCorrection()
        {
            Content_3 = THelp.SetBits(Content_3, bit_pos_datacorrection);
        }

        /// <summary>
        /// Conten func - exclude DiscardedRecCrDTL, Bit1 = 0
        /// </summary>
        public void ExcludeDisRecCrDTL()
        {
            Content_3 = THelp.ResetBits(Content_3, bit_pos_include_disreccrdt);
        }

        /// <summary>
        /// Content func - include DiscardedRecCrDTL, Bit1 = 1
        /// </summary>
        public void IncludeDisRecCrDTL()
        {
            Content_3 = THelp.SetBits(Content_3, bit_pos_include_disreccrdt);
        }
        #endregion

        /// <summary>
        /// Convert class fields to byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            try
            {
                byte[] resp = new byte[2000];
                int inx = 0;

                // Content 1
                resp[inx] = Content_1;
                inx += 1;
                // Content 2
                resp[inx] = Content_2;
                inx += 1;
                // Content 3
                resp[inx] = Content_3;
                inx += 1;

                // Vessel Name
                if (THelp.CheckBits(Content_1, vname_bit_pos))
                {
                    Array.Copy(VesselName, 0, resp, inx, VesselName.Length);
                    inx += VesselName.Length;
                }

                // Vessel Description
                if (THelp.CheckBits(Content_1, vdesc_bit_pos))
                {
                    Array.Copy(Description, 0, resp, inx, Description.Length);
                    inx += Description.Length;
                }

                // Vessel Country
                if (THelp.CheckBits(Content_1, country_bit_pos))
                {
                    resp[inx] = Country;
                    inx += 1;
                }

                // Vessel external mark
                if (THelp.CheckBits(Content_1, ext_mark_bit_pos))
                {
                    Array.Copy(ExternalMark, 0, resp, inx, ExternalMark.Length);
                    inx += ExternalMark.Length;
                }

                // Vessel MMSI
                if (THelp.CheckBits(Content_1, mmsi_bit_pos))
                {
                    Array.Copy(MMSI, 0, resp, inx, MMSI.Length);
                    inx += MMSI.Length;
                }

                // Vessel CFR
                if (THelp.CheckBits(Content_1, cfr_bit_pos))
                {
                    Array.Copy(CFR, 0, resp, inx, CFR.Length);
                    inx += CFR.Length;
                }

                // Vessel radio call sign
                if (THelp.CheckBits(Content_1, rcallsign_bit_pos))
                {
                    Array.Copy(CallSign, 0, resp, inx, CallSign.Length);
                    inx += CallSign.Length;
                }

                // Vessel license
                if (THelp.CheckBits(Content_1, license_bit_pos))
                {
                    Array.Copy(License, 0, resp, inx, License.Length);
                    inx += License.Length;
                }

                // Vessel radion license
                if (THelp.CheckBits(Content_2, radiolic_bit_pos))
                {
                    Array.Copy(RadioLic, 0, resp, inx, RadioLic.Length);
                    inx += RadioLic.Length;
                }

                // Vessel RSS
                if (THelp.CheckBits(Content_2, rss_bit_pos))
                {
                    Array.Copy(RSSNr, 0, resp, inx, RSSNr.Length);
                    inx += RSSNr.Length;
                }

                // Vessel home port
                if (THelp.CheckBits(Content_2, hport_bit_pos))
                {
                    if (HPort != null)
                    {
                        byte[] hp = new byte[2];
                        hp = BitConverter.GetBytes((UInt16)HPort.Id);
                        Array.Copy(hp, 0, resp, inx, hp.Length);
                        inx += hp.Length;
                    }
                    else
                    {
                        byte[] hp = new byte[2];
                        hp = BitConverter.GetBytes((UInt16)0);
                        Array.Copy(hp, 0, resp, inx, hp.Length);
                        inx += hp.Length;
                    }
                    //byte[] hp = new byte[2];
                    //hp = BitConverter.GetBytes(HomePort);
                    //Array.Copy(hp, 0, resp, inx, hp.Length);
                    //inx += hp.Length;
                }

                // Vessel reg port
                if (THelp.CheckBits(Content_2, rport_bit_pos))
                {
                    if (RPort != null)
                    {
                        byte[] rp = new byte[2];
                        rp = BitConverter.GetBytes((UInt16)RPort.Id);
                        Array.Copy(rp, 0, resp, inx, rp.Length);
                        inx += rp.Length;
                    }
                    else
                    {
                        byte[] rp = new byte[2];
                        rp = BitConverter.GetBytes((UInt16)0);
                        Array.Copy(rp, 0, resp, inx, rp.Length);
                        inx += rp.Length;
                    }
                    //byte[] rp = new byte[2];
                    //rp = BitConverter.GetBytes(RegPort);
                    //Array.Copy(rp, 0, resp, inx, rp.Length);
                    //inx += rp.Length;
                }

                // Vessel length
                if (THelp.CheckBits(Content_2, vlen_bit_pos))
                {
                    byte[] len = new byte[4];
                    len = BitConverter.GetBytes(VesselLen);
                    Array.Copy(len, 0, resp, inx, len.Length);
                    inx += len.Length;
                }

                // Vessel width
                if (THelp.CheckBits(Content_2, vwidth_bit_pos))
                {
                    byte[] vw = new byte[4];
                    vw = BitConverter.GetBytes(VesselWidth);
                    Array.Copy(vw, 0, resp, inx, vw.Length);
                    inx += vw.Length;
                }

                // Vessel draft
                if (THelp.CheckBits(Content_2, draft_bit_pos))
                {
                    byte[] draft = new byte[4];
                    draft = BitConverter.GetBytes(VesselDraft);
                    Array.Copy(draft, 0, resp, inx, draft.Length);
                    inx += draft.Length;
                }

                // Vessel tonage
                if (THelp.CheckBits(Content_2, tonage_bit_pos))
                {
                    byte[] ton = new byte[4];
                    ton = BitConverter.GetBytes(VesselGrossTonage);
                    Array.Copy(ton, 0, resp, inx, ton.Length);
                    inx += ton.Length;
                }

                // Vessel owner
                if (THelp.CheckBits(Content_3, owner_bit_pos))
                {
                    Array.Copy(VesselOwner, 0, resp, inx, VesselOwner.Length);
                    inx += VesselOwner.Length;
                }

                // Vessel company address
                if (THelp.CheckBits(Content_3, company_bit_pos))
                {
                    Array.Copy(VesselCompanyAddr_EIK, 0, resp, inx, VesselCompanyAddr_EIK.Length);
                    inx += VesselCompanyAddr_EIK.Length;
                }

                // CreationDT - 4 bytes          
                byte[] time = BitConverter.GetBytes(CreationDTL);
                Array.Copy(time, 0, resp, inx, time.Length);
                inx += Marshal.SizeOf(CreationDTL);

                // DisRecCrDTL - 4 bytes         
                if (THelp.CheckBits(Content_3, bit_pos_include_disreccrdt))
                {
                    byte[] distime = BitConverter.GetBytes(DisRecCrDTL);
                    Array.Copy(distime, 0, resp, inx, distime.Length);
                    inx += Marshal.SizeOf(DisRecCrDTL);
                }

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
