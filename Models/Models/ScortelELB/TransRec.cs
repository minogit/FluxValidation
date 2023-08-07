using ScortelApi.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ScortelApi.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ScortelApi.Models.ScortelELB
{
    

    /// <summary>
    /// Protocol 2.0.1
    /// </summary>
    public class TransRec : ITransRec
    {
        #region Fields
        private UInt16 econzone;
        private int econzonelite;
        private UInt16 statzone;
        private int statzonelite;
        // fcrecscount
        private byte fcrecscount;
        private List<FCRec> fclist;
        // discardedrecdt        
        private UInt32 drdtl;
        private DateTime drdt;
        // creation dt         
        private UInt32 crdtl;
        private DateTime crdt;
        private byte vreccount = 0;
        private VRec vrec;
        #endregion

        #region Bit pos
        public static byte bit_pos_econ = 7;
        public static byte bit_pos_stat = 6;
        public static byte bit_pos_ves_role = 5;
        public static byte bit_pos_position = 4;
        public const byte bit_pos_datacorrection = 2;
        public const byte bit_pos_include_disreccrdt = 1;
        #endregion

        #region Properties
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Bit 7
        ///•	0 – excluded economic zone 
        ///•	1 – included economic zone
        ///Bit 6 
        ///•	0 – excluded statistical or other zone
        ///•	1 – included statistical or other zone
        ///Bit 5 Vessel role
        ///•	0 – donor
        ///•	1 – receiving
        ///Bit 4 – Position GPS part
        ///•	0 – excluded
        ///•	1 – invluded
        /// Bit 2 – Data correction
        ///•	0 – no correction of data
        ///•	1 – with correction
        /// Bit 1 
        ///•	0 – DiscardedRecCrDT – emptry - exclude
        ///•	1 – DiscardedRecCrDT – with data - include
        /// </summary>
        //[Index("IX_TransRecUnq", 1, IsUnique = true)]
        public byte Content { get; set; }

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
        #endregion

        #region FCRecsCount
        private byte FCRecsCount
        {
            get { return fcrecscount; }
            set { fcrecscount = value; }
        }
        #endregion

        #region FCRecs
        /// <summary>
        /// List of fishing catch
        /// </summary>
        public List<FCRec> FCRecs
        {
            get { return fclist; }
            set
            {
                fclist = value;
                fcrecscount = (byte)fclist.Count;
            }
        }
        #endregion

        #region Pos
        /// <summary>
        /// Gps position part
        /// </summary>
        //[Index("IX_TransRecUnq", 2, IsUnique = true)]
        public PosRec Pos { get; set; } = new PosRec();
        #endregion

        #region CreationDT
        //[Index("IX_TransRecUnq", 3, IsUnique = true)]
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

        #region VRecCount
        [NotMapped]
        public int VRCount
        {
            get { return vreccount; }
        }
        #endregion

        #region VRecs
        /// <summary>
        /// List of vessel 
        /// </summary>
        public VRec VRec
        {
            get { return vrec; }
            set
            {
                vrec = value;
                vreccount = 1;
            }
        }
        #endregion

        #endregion

        #region Content Funcs

        /// <summary>
        /// Bit7 = 0
        /// </summary>
        public void ExcludeEconZone()
        {
            Content = THelp.ResetBits(Content, bit_pos_econ);
        }

        /// <summary>
        /// Bit7 = 1
        /// </summary>
        public void IncludeEconZone()
        {
            Content = THelp.SetBits(Content, bit_pos_econ);
        }

        /// <summary>
        /// Bit6 = 0
        /// </summary>
        public void ExcludeStatZone()
        {
            Content = THelp.ResetBits(Content, bit_pos_stat);
        }

        /// <summary>
        /// Bit6 = 1
        /// </summary>
        public void IncludeStatZone()
        {
            Content = THelp.SetBits(Content, bit_pos_stat);
        }

        /// <summary>
        /// Set vessel role to donor, Bit5 = 0
        /// </summary>
        public void VesselRoleDonor()
        {
            Content = THelp.ResetBits(Content, bit_pos_ves_role);
        }

        /// <summary>
        /// Set vessel role to receiving catch, Bit5 = 1
        /// </summary>
        public void VesselRoleReceiving()
        {
            Content = THelp.SetBits(Content, bit_pos_ves_role);
        }

        /// <summary>
        /// Bit4 = 0
        /// </summary>
        public void ExcludePos()
        {
            Content = THelp.ResetBits(Content, bit_pos_position);
        }

        /// <summary>
        /// Bit4 = 1
        /// </summary>
        public void IncludePos()
        {
            Content = THelp.SetBits(Content, bit_pos_position);
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
            try
            {
                byte[] resp = new byte[1000];
                int inx = 0;

                // Content
                resp[inx] = Content;
                inx += 1;

                // Economic zone
                if (THelp.CheckBits(Content, bit_pos_econ))
                {
                    byte[] eco = new byte[2];
                    eco = BitConverter.GetBytes(EconZone);
                    Array.Copy(eco, 0, resp, inx, eco.Length);
                    inx += eco.Length;
                }

                //Statistical zone
                if (THelp.CheckBits(Content, bit_pos_stat))
                {
                    byte[] st = new byte[2];
                    st = BitConverter.GetBytes(StatZone);
                    Array.Copy(st, 0, resp, inx, st.Length);
                    inx += st.Length;
                }

                // FCRecsCount
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

                // Fishing catch recs
                foreach (var fc in FCRecs)
                {
                    byte[] fcarr = fc.GetData();
                    Array.Copy(fcarr, 0, resp, inx, fcarr.Length);
                    inx += fcarr.Length;
                }

                // GPS Position
                if (THelp.CheckBits(Content, bit_pos_position))
                {
                    var posarr = Pos.GetData();
                    Array.Copy(posarr, 0, resp, inx, posarr.Length);
                    inx += posarr.Length;
                }

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
