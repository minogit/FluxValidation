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
    /// Crew member record
    /// ELB Protocol 2.0.0
    /// </summary>
    public class CMRec : ICMRec
    {
        #region Fields
        private string namestr;
        private string addressstr;
        private UInt16 zipcode;
        private int zipcodelite;
        private string positionstr;
        private string usernamestr;
        private string userpassstr;
        private string cellphonestr;
        private string faxstr;
        private string emailstr;
        private string medicalinfostr;
        private string notestr;
        // creation dt         
        private UInt32 crdtl;
        private DateTime crdt;
        // discardedrecdt        
        private UInt32 drdtl;
        private DateTime drdt;
        #endregion

        #region Bit pos - content
        public const byte min_bit_pos = 7;
        public const byte zipcode_bit_pos = 6;
        public const byte access_bit_pos = 5;
        public const byte fax_bit_pos = 4;
        public const byte email_bit_pos = 3;
        public const byte nat_bit_pos = 2;
        public const byte med_bit_pos = 1;
        public const byte notes_bit_pos = 0;
        #endregion

        #region Bit pos content 2
        public const byte bit_pos_datacorrection = 2;
        public const byte bit_pos_include_disreccrdt = 1;
        #endregion

        #region Properties

        /// <summary>
        /// SQLite PK id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Bit 7
        ///•	0 – minimal information(Name; Crew position; Cell phone)
        ///•	1 – full information
        /// Bit 6
        ///•	0 – excluded zipcode
        ///•	1 – included zipcode 
        /// Bit 5
        ///•	0 – excluded username and password
        ///•	1 – included username and password
        /// Bit 4
        ///•	0 – excluded fax
        ///•	1 – included fax
        /// Bit 3
        ///•	0 – excluded email
        ///•	1 – included email
        /// Bit 2
        ///•	0 – excluded nationality
        ///•	1 – included nationality
        /// Bit 1
        ///•	0 – excluded medical info
        ///•	1 – included medical info
        /// Bit 0
        ///•	0 – excluded notes
        ///•	1 – included notes
        /// </summary>
        public byte Content { get; set; }

        /// <summary>    
        /// Bit 2
        ///•	0 – excluded nationality
        ///•	1 – included nationality
        /// Bit 1
        ///•	0 – excluded medical info
        ///•	1 – included medical info
        /// Bit 0
        ///•	0 – excluded notes
        ///•	1 – included notes
        /// </summary>
        public byte Content_2 { get; set; }

        /// <summary>
        /// Crew member names in byte format
        /// First byte is the length of data
        /// </summary>
        public byte[] Name
        {
            get; set;
        }

        /// <summary>
        /// Crew member name in string format UTF8
        /// </summary>
        public string NameStr
        {
            get
            {
                if (Name != null)
                {
                    return Encoding.UTF8.GetString(Name, 1, Name.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                namestr = value;
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
                    Name = tnum;
                }
            }
        }

        /// <summary>
        /// Crew member address in byte format
        /// First byte is the length of data
        /// </summary>
        public byte[] Address { get; set; }

        /// <summary>
        /// Crew member address in string format UTF8
        /// </summary>
        public string AddressStr
        {
            get
            {
                if (Address != null)
                {
                    return Encoding.UTF8.GetString(Address, 1, Address.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                addressstr = value;
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
                    Address = tnum;
                }
            }
        }

        /// <summary>
        /// Address zip code
        /// </summary>
        [NotMapped]
        public UInt16 ZipCode
        {
            get { return zipcode; }
            set
            {
                zipcode = value;
                zipcodelite = value;
            }
        }

        /// <summary>
        /// Address zip code
        /// SQLite
        /// </summary>
        public int ZipCodeLite
        {
            get { return zipcodelite; }
            set
            {
                zipcodelite = value;
                zipcode = (UInt16)value;
            }
        }

        /// <summary>
        /// Crew member position in byte format
        /// First byte is the length of data
        /// </summary>
        public byte[] Position { get; set; }

        /// <summary>
        /// Crew member position in string format UTF8
        /// </summary>
        public string PositionStr
        {
            get
            {
                if (Position != null)
                {
                    return Encoding.UTF8.GetString(Position, 1, Position.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                positionstr = value;
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
                    Position = tnum;
                }
            }
        }

        /// <summary>
        /// Crew member username in byte format
        /// First byte is the length of data
        /// </summary>
        public byte[] Username { get; set; }

        /// <summary>
        /// Crew member username in string format UTF8
        /// </summary>
        public string UsernameStr
        {
            get
            {
                if (Username != null)
                {
                    return Encoding.UTF8.GetString(Username, 1, Username.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                usernamestr = value;
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
                    Username = tnum;
                }
            }
        }

        /// <summary>
        /// Crew member userpass in byte format
        /// First byte is the length of data
        /// </summary>
        public byte[] Userpass { get; set; }

        /// <summary>
        /// Crew member userpass in string format UTF8
        /// </summary>
        public string UserpassStr
        {
            get
            {
                if (Userpass != null)
                {
                    return Encoding.UTF8.GetString(Userpass, 1, Userpass.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                userpassstr = value;
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
                    Userpass = tnum;
                }
            }
        }

        /// <summary>
        /// Crew member cellphone in byte format
        /// First byte is the length of data
        /// </summary>
        public byte[] CellPhone { get; set; }

        /// <summary>
        /// Crew member cellphone in string format UTF8
        /// </summary>
        public string CellPhoneStr
        {
            get
            {
                if (CellPhone != null)
                {
                    return Encoding.UTF8.GetString(CellPhone, 1, CellPhone.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                cellphonestr = value;
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
                    CellPhone = tnum;
                }
            }
        }

        /// <summary>
        /// Crew member fax in byte format
        /// First byte is the length of data
        /// </summary>
        public byte[] Fax { get; set; }

        /// <summary>
        /// Crew member fax in string format UTF8
        /// </summary>
        public string FaxStr
        {
            get
            {
                if (Fax != null)
                {
                    return Encoding.UTF8.GetString(Fax, 1, Fax.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                faxstr = value;
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
                    Fax = tnum;
                }
            }
        }

        /// <summary>
        /// Crew member email in byte format
        /// First byte is the length of data
        /// </summary>
        public byte[] EMail { get; set; }

        /// <summary>
        /// Crew member email in string format UTF8
        /// </summary>
        public string EMailStr
        {
            get
            {
                if (EMail != null)
                {
                    return Encoding.UTF8.GetString(EMail, 1, EMail.Length - 1);
                }
                else
                    return "";
            }
            set
            {
                emailstr = value;
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
                    EMail = tnum;
                }
            }
        }

        /// <summary>
        /// Sequence number from list nationalities
        /// </summary>
        public byte Nationality { get; set; }

        /// <summary>
        /// Crew member medical info in byte format
        /// First byte is the length of data
        /// </summary>
        public byte[] MedicalInfo { get; set; }

        /// <summary>
        /// Crew member medical info in string format UTF8
        /// </summary>
        public string MedicalInfoStr
        {
            get
            {
                if (MedicalInfo != null)
                {
                    return Encoding.UTF8.GetString(MedicalInfo, 1, MedicalInfo.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                medicalinfostr = value;
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
                    MedicalInfo = tnum;
                }
            }
        }

        /// <summary>
        /// Crew member notes in byte format
        /// First byte is the length of data
        /// </summary>
        public byte[] Notes { get; set; }

        /// <summary>
        /// Crew member mnotes in string format UTF8
        /// </summary>
        public string NotesStr
        {
            get
            {
                if (Notes != null)
                {
                    return Encoding.UTF8.GetString(Notes, 1, Notes.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                notestr = value;
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
                    Notes = tnum;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Favorite { get; set; }

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

        #endregion

        #region Content funcs

        /// <summary>
        /// Set minimal info
        /// Name, Address, Position, CellPhone
        /// </summary>
        public void SetMinimalInfo()
        {
            Content = THelp.ResetBits(Content, min_bit_pos);
        }

        /// <summary>
        /// Set full data
        /// </summary>
        public void SetFullData()
        {
            Content = THelp.SetBits(Content, min_bit_pos);
        }

        /// <summary>
        /// Exclude zip code data
        /// </summary>
        public void ExcludeZipCode()
        {
            Content = THelp.ResetBits(Content, zipcode_bit_pos);
        }

        /// <summary>
        /// Include zip code data
        /// </summary>
        public void IncludeZipCode()
        {
            Content = THelp.SetBits(Content, zipcode_bit_pos);
        }

        /// <summary>
        /// Exclude access data
        /// </summary>
        public void ExcludeAccessData()
        {
            Content = THelp.ResetBits(Content, access_bit_pos);
        }

        /// <summary>
        /// Include access data
        /// </summary>
        public void IncludeAccessData()
        {
            Content = THelp.SetBits(Content, access_bit_pos);
        }

        /// <summary>
        /// Exclude fax data
        /// </summary>
        public void EcludeFax()
        {
            Content = THelp.ResetBits(Content, fax_bit_pos);
        }

        /// <summary>
        /// Include fax data
        /// </summary>
        public void IncludeFax()
        {
            Content = THelp.SetBits(Content, fax_bit_pos);
        }

        /// <summary>
        /// Exclude email data
        /// </summary>
        public void ExcludeEmail()
        {
            Content = THelp.ResetBits(Content, email_bit_pos);
        }

        /// <summary>
        /// Include email data
        /// </summary>
        public void IncludeEmail()
        {
            Content = THelp.SetBits(Content, email_bit_pos);
        }

        /// <summary>
        /// Exclude nationality data
        /// </summary>
        public void ExcludeNationality()
        {
            Content = THelp.ResetBits(Content, nat_bit_pos);
        }

        /// <summary>
        /// Include nationality data
        /// </summary>
        public void IncludeNationality()
        {
            Content = THelp.SetBits(Content, nat_bit_pos);
        }

        /// <summary>
        /// Exclude medical info data
        /// </summary>
        public void ExcludeMedicalInfo()
        {
            Content = THelp.ResetBits(Content, med_bit_pos);
        }

        /// <summary>
        /// Include medical info data
        /// </summary>
        public void IncludeMedicalInfo()
        {
            Content = THelp.SetBits(Content, med_bit_pos);
        }

        /// <summary>
        /// Exclude notes data
        /// </summary>
        public void ExcludeNotes()
        {
            Content = THelp.ResetBits(Content, notes_bit_pos);
        }

        /// <summary>
        /// Include notes data
        /// </summary>
        public void IncludeNotes()
        {
            Content = THelp.SetBits(Content, notes_bit_pos);
        }

        /// <summary>
        /// Content func - no correction of data, Bit2 = 0
        /// </summary>
        public void SetNoDataCorrection()
        {
            Content_2 = THelp.ResetBits(Content_2, bit_pos_datacorrection);
        }

        /// <summary>
        /// Content func - with data correction, Bit2 = 1
        /// </summary>
        public void SetDataCorrection()
        {
            Content_2 = THelp.SetBits(Content_2, bit_pos_datacorrection);
        }

        /// <summary>
        /// Conten func - exclude DiscardedRecCrDTL, Bit1 = 0
        /// </summary>
        public void ExcludeDisRecCrDTL()
        {
            Content_2 = THelp.ResetBits(Content_2, bit_pos_include_disreccrdt);
        }

        /// <summary>
        /// Content func - include DiscardedRecCrDTL, Bit1 = 1
        /// </summary>
        public void IncludeDisRecCrDTL()
        {
            Content_2 = THelp.SetBits(Content_2, bit_pos_include_disreccrdt);
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
                byte[] resp = new byte[3000];
                int inx = 0;

                //Content
                resp[inx] = Content;
                inx += 1;

                // Name
                Array.Copy(Name, 0, resp, inx, Name.Length);
                inx += Name.Length;

                // Address
                Array.Copy(Address, 0, resp, inx, Address.Length);
                inx += Address.Length;

                // Zipcode
                if (THelp.CheckBits(Content, zipcode_bit_pos))
                {
                    byte[] zip = BitConverter.GetBytes(ZipCode);
                    Array.Copy(zip, 0, resp, inx, zip.Length);
                    inx += zip.Length;
                }

                // Position
                Array.Copy(Position, 0, resp, inx, Position.Length);
                inx += Position.Length;

                // Username and password
                if (THelp.CheckBits(Content, access_bit_pos))
                {
                    // username
                    Array.Copy(Username, 0, resp, inx, Username.Length);
                    inx += Username.Length;

                    // password
                    Array.Copy(Userpass, 0, resp, inx, Userpass.Length);
                    inx += Userpass.Length;
                }

                // Fax
                if (THelp.CheckBits(Content, fax_bit_pos))
                {
                    Array.Copy(Fax, 0, resp, inx, Fax.Length);
                    inx += Fax.Length;
                }

                // Email
                if (THelp.CheckBits(Content, email_bit_pos))
                {
                    Array.Copy(EMail, 0, resp, inx, EMail.Length);
                    inx += EMail.Length;
                }

                // Nationality
                if (THelp.CheckBits(Content, nat_bit_pos))
                {
                    resp[inx] = Nationality;
                    inx += 1;
                }

                // Medical info
                if (THelp.CheckBits(Content, med_bit_pos))
                {
                    Array.Copy(MedicalInfo, 0, resp, inx, MedicalInfo.Length);
                    inx += MedicalInfo.Length;
                }

                // Notes
                if (THelp.CheckBits(Content, notes_bit_pos))
                {
                    Array.Copy(Notes, 0, resp, inx, Notes.Length);
                    inx += Notes.Length;
                }

                // CreationDT - 4 bytes          
                byte[] time = BitConverter.GetBytes(CreationDTL);
                Array.Copy(time, 0, resp, inx, time.Length);
                inx += Marshal.SizeOf(CreationDTL);

                // DisRecCrDTL - 4 bytes         
                if (THelp.CheckBits(Content_2, bit_pos_include_disreccrdt))
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
