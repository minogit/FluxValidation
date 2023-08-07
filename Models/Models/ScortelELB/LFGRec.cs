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
    public class LFGRec
    {
        #region fields
        // creation dt         
        private UInt32 crdtl;
        private DateTime crdt;
        // lost dt         
        private UInt32 lostdtl;
        private DateTime lostdt;

        // eye
        private UInt16 eye;
        private int eyelite;
        #endregion

        /// <summary>
        /// SQLite PK id
        /// </summary>
        [Key]
        public int Id { get; set; }

        #region Content
        public byte Content { get; set; }
        #endregion

        #region CreationDT
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

        #region LostDT
        /// <summary>
        /// end fishing trip structure - creation date and time
        /// </summary>
        public DateTime LostDT
        {
            get
            {
                return lostdt;
            }
            set
            {
                lostdt = value;
                lostdtl = THelp.ELBTimeFormat(value);
            }
        }

        #region LostDTL
        /// <summary>
        /// Timestamp since 2018.01.01 00:00:00 UTC in seconds
        /// </summary>
        [NotMapped]
        public UInt32 LostDTL
        {
            get { return lostdtl; }
            set
            {
                lostdtl = value;
                lostdt = THelp.DateTimeFromELBTimestamp((UInt32)value);
            }
        }

        #endregion
        #endregion

        #region Marks

        public byte[] Marks { get; set; }

        public string MarksStr
        {
            get
            {
                if (Marks != null && Marks.Length != 1)
                {
                    return Encoding.UTF8.GetString(Marks, 1, Marks.Length - 1);
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
                    Marks = tnum;
                }
            }
        }
        #endregion


        /// <summary>
        /// Sequence number from list fishing gears (0-255)
        /// </summary>     
        public byte FishingGear
        {
            get;
            set;
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
        public int GearEyeLite
        {
            get { return eyelite; }
            set
            {
                eyelite = value;
                eye = (UInt16)value;
            }
        }

        #region 
        #endregion


        /// <summary>
        /// Convert fields to byte array
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

                // fishing gear - 1 byte
                resp[inx] = FishingGear;
                inx += 1;

                // fishing eye - 2 bytes
                byte[] feye = new byte[2] { 0, 0 };
                feye = BitConverter.GetBytes(GearEye);
                Array.Copy(feye, 0, resp, inx, feye.Length);
                inx += Marshal.SizeOf(GearEye);

                // CreationDT - 4 bytes          
                byte[] time = BitConverter.GetBytes(CreationDTL);
                Array.Copy(time, 0, resp, inx, time.Length);
                inx += Marshal.SizeOf(CreationDTL);

                // LostDT - 4 bytes          
                byte[] losttime = BitConverter.GetBytes(LostDTL);
                Array.Copy(losttime, 0, resp, inx, losttime.Length);
                inx += Marshal.SizeOf(LostDTL);

                // marks
                // tripnumber  
                Array.Copy(Marks, 0, resp, inx, Marks.Length);
                inx += Marks.Length;

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
