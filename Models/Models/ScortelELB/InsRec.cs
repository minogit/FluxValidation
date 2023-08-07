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
    /// Protocol 2.6
    /// </summary>
    public class InsRec : IInsRec
    {
        #region Fields

        // creation dt         
        private UInt32 crdtl;
        private DateTime crdt;
        // discardedrecdt        
        private UInt32 drdtl;
        private DateTime drdt;
        // country
        private byte country;
        // CountryCodeM49
        private CountryCodeM49 CCodeM49;
        // inspectors id type
        private byte instype;
        // 
        private InsIdTypeRec insidtyperes;
        // inspdatastr
        private string inspdatastr;
        #endregion

        #region Bit pos
        public const byte bit_pos_trip = 3;
        public const byte bit_pos_datacorrection = 2;
        public const byte bit_pos_include_disreccrdt = 1;
        #endregion

        #region Properties
        /// <summary>
        /// SQLite PK id
        /// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types)
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Content 
        /// 
        /// Bit 2 – Data correction
        ///•	0 – no correction of data
        ///•	1 – with correction
        /// Bit 1 
        ///•	0 – DiscardedRecCrDT – emptry - exclude
        ///•	1 – DiscardedRecCrDT – with data - include
        /// </summary>
        public byte Content { get; set; }

        #region Country
        /// <summary>
        /// Country 3 symbols abbreviation from list (0-255) 
        /// </summary>
        [NotMapped]
        public byte Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }

        public CountryCodeM49 Countries
        {
            get
            {
                return CCodeM49;
            }
            set
            {
                CCodeM49 = value;
                country = (byte)CCodeM49.Id;
            }
        }
        #endregion

        #region Inspectors Id Type
        /// <summary>
        /// inspector identification. (1. Names, 2 – Identification card Number etc.)
        /// </summary>
        [NotMapped]
        public byte InspIdType
        {
            get { return instype; }
            set { instype = value; }
        }

        public InsIdTypeRec InsIdTypeRec
        {
            get
            {
                return insidtyperes;
            }
            set
            {
                insidtyperes = value;
                instype = (byte)insidtyperes.Code; // Id
            }
        }
        #endregion

        #region InspData
        /// <summary>
        /// Inspector name or other identification number or name. 
        /// First byte is length of the field.  
        ///The data is encoded in Unicode.
        /// </summary>
        public byte[] InspData { get; set; }

        /// <summary>
        /// Inspection identification in string format
        /// </summary>
        //[Index("IX_InsRecUnq", 1, IsUnique = true)]
        public string InspDataStr
        {
            get
            {
                if (InspData != null)
                {

                    return Encoding.UTF8.GetString(InspData, 1, InspData.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {

                //InspDataStr = value;
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
                    InspData = tnum;
                }
            }
        }
        #endregion

        #region Pos
        /// <summary>
        /// GPS position record
        /// </summary>
        public PosRec Pos { get; set; } = new PosRec();
        #endregion

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

        #endregion

        #region Content Funcs
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

        /// <summary>
        /// Exclude trip number data bit3 = 0
        /// </summary>
        public void ExcludeTripData()
        {
            Content = THelp.ResetBits(Content, bit_pos_trip);
        }

        /// <summary>
        /// Include trip number data bit3 = 0
        /// </summary>
        public void IncludeTripData()
        {
            Content = THelp.SetBits(Content, bit_pos_trip);
        }
        #endregion

        /// <summary>
        /// Convert fields to byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            try
            {
                byte[] resp = new byte[25 + InspData.Length];
                int inx = 0;

                // Country
                if (Country != Countries.Id)
                {
                    resp[inx] = (byte)Countries.Id;
                    inx += 1;
                }
                else
                {
                    resp[inx] = Country;
                    inx += 1;
                }

                // InspIdType
                if (InspIdType != InsIdTypeRec.Id)
                {
                    resp[inx] = (byte)InsIdTypeRec.Id;
                    inx += 1;
                }
                else
                {
                    resp[inx] = InspIdType;
                    inx += 1;
                }

                // Inspector data
                Array.Copy(InspData, 0, resp, inx, InspData.Length);
                inx += InspData.Length;

                byte[] pos = Pos.GetData();
                Array.Copy(pos, 0, resp, inx, pos.Length);
                inx += 1;

                // CreationDT - 4 bytes          
                byte[] time = BitConverter.GetBytes(CreationDTL);
                Array.Copy(time, 0, resp, inx, time.Length);
                inx += Marshal.SizeOf(CreationDTL);

                // DisRecCrDTL - 4 bytes         
                if (THelp.CheckBits(Content, bit_pos_include_disreccrdt))
                {
                    byte[] distime = BitConverter.GetBytes(DisRecCrDTL);
                    Array.Copy(distime, 0, resp, inx, distime.Length);
                    inx += Marshal.SizeOf(DisRecCrDTL);
                }

                #region Day rec Trip number               
                if (THelp.CheckBits(this.Content, bit_pos_trip))
                {
                    Array.Copy(TripNumber, 0, resp, inx, TripNumber.Length);
                    inx += TripNumber.Length;
                }
                #endregion

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
