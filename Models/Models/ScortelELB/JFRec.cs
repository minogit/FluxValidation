using ScortelApi.Models.Interfaces;
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
    /// Joint fishing record
    /// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types)
    /// 
    /// Protocol 2.0.0
    /// </summary>
    public class JFRec : IJFRec
    {
        #region Fields

        private byte vrecscount;
        private List<VRec> vrlist;
        // creation dt         
        private UInt32 crdtl;
        private DateTime crdt;
        // discardedrecdt        
        private UInt32 drdtl;
        private DateTime drdt;
        #endregion

        #region Bit pos
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
        /// Content 
        /// 
        /// Bit 2 – Data correction
        ///•	0 – no correction of data
        ///•	1 – with correction
        ///Bit 1 
        ///•	0 – DiscardedRecCrDT – emptry - exclude
        ///•	1 – DiscardedRecCrDT – with data - include
        /// </summary>
        public byte Content { get; set; } = 0;

        #region VRecsCount

        private byte VRecsCount
        {
            get { return vrecscount; }
            set
            {
                vrecscount = value;
            }
        }

        #endregion

        #region VRecs
        /// <summary>
        /// List of vessel 
        /// </summary>
        public List<VRec> VRecs
        {
            get { return vrlist; }
            set
            {
                vrlist = value;
                vrecscount = (byte)vrlist.Count;
            }
        }
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

        #region Pos
        /// <summary>
        /// Gps position part
        /// </summary>
        public PosRec Pos { get; set; } = new PosRec();
        #endregion

        public DateTime ServerDT { get; set; }
         

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
                byte[] resp = new byte[1000];

                // content - 1 byte
                resp[inx] = Content;
                inx += 1;

                if (VRecsCount != VRecs.Count)
                {
                    resp[inx] = (byte)VRecs.Count;
                    inx += 1;
                }
                else
                {
                    resp[inx] = VRecsCount;
                    inx += 1;
                }

                // Vessels data
                foreach (var rec in VRecs)
                {
                    byte[] vr = rec.GetData();
                    Array.Copy(vr, 0, resp, inx, vr.Length);
                    inx += vr.Length;
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

                // Gps position
                byte[] pos = Pos.GetData();
                Array.Copy(pos, 0, resp, inx, pos.Length);
                inx += pos.Length;

                Array.Resize(ref resp, inx);

                return resp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public JFRec()
        {
            VRecs = new List<VRec>();
        }
    }
}
