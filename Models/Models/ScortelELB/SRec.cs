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
    /// Species record
    /// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types) 
    /// 
    /// Protocol 2.0.0
    /// </summary>
    public class SRec
    {
        #region Fields
        // species
        private Species species;
        private UInt16 speciesCode;
        private int specieslite;
        // creation dt         
        private UInt32 crdtl;
        private DateTime crdt;
        //   discardedrecdt        
        private UInt32 drdtl;
        private DateTime drdt;
        // quantitytype
        private byte qtype;
        // currency
        private byte currency;
        private FCurrency fcurrency;

        #endregion

        #region Bit pos
        public enum SRecBitPosition
        {
            FullPacketData_bit_position = 7,
            //QType_bit_pos = 6,
            bit_pos_datacorrection = 2,
            bit_pos_include_disreccrdt = 1
        };

        #endregion

        #region Properties
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Bit 7
        ///•	0 – minimal information(Without Price; Currency)
        ///•	1 – full information
        /// //////////////// Bit 6
        /// ////////////////•	0 – excluded QuantityType
        /// ////////////////•	1 – included QuantityType
        /// Bit 2 – Data correction
        ///•	0 – no correction of data
        ///•	1 – with correction
        /// Bit 1 
        ///•	0 – DiscardedRecCrDT – emptry - exclude
        ///•	1 – DiscardedRecCrDT – with data - include
        /// </summary>
        public byte Content { get; set; }
 
        #region Species
        /// <summary>
        /// Sequence number from list species (0-255)
        /// </summary>
        [NotMapped]
        public UInt16 SpeciesCode
        {
            get
            {
                return speciesCode;
            }
            set
            {
                speciesCode = value;
            }
        }

        public Species Species
        {
            get { return species; }
            set
            {
                species = value;
                try
                {
                    speciesCode = (byte)species.Id;
                }
                catch (Exception)
                {
                    speciesCode = 0;
                }
            }
        }
        #endregion

        #region Price
        /// <summary>
        /// Price for what?
        /// </summary>
        public float Price { get; set; }
        #endregion
 
        #region Currency
        /// <summary>
        /// Sequence number from list currencies (0-255)
        /// </summary>
        [NotMapped]
        public byte Currency
        {
            get { return currency; }
            set
            {
                currency = value;
            }
        }

        public FCurrency FCurrency
        {
            get { return fcurrency; }
            set
            {
                fcurrency = value;
                try
                {
                    currency = (byte)fcurrency.Id;
                }
                catch (Exception)
                {
                    currency = 0;
                }
            }
        }
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

        #region DiscardedRecCrDT
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

        #region Content func
        /// <summary>
        /// Content func
        /// Bit 7
        ///•	0 – minimal information(Without Price; Currency)
        ///•	1 – full information
        /// </summary>
        public void MinimalContent()
        {
            Content = THelp.ResetBits(Content, (byte)SRecBitPosition.FullPacketData_bit_position);
        }

        public void FullContent()
        {
            Content = THelp.SetBits(Content, (byte)SRecBitPosition.FullPacketData_bit_position);
        }

        #region commented
        ///// <summary>
        ///// Exclude QuantityType, Bit6 = 0
        ///// </summary>
        //public void ExcludeQuantityType()
        //{
        //    Content = THelp.ResetBits(Content, (byte)SRecBitPosition.QType_bit_pos);
        //}

        ///// <summary>
        ///// Include QuantityType, Bit6 = 1
        ///// </summary>
        //public void IncludeQuantityType()
        //{
        //    Content = THelp.SetBits(Content, (byte)SRecBitPosition.QType_bit_pos);
        //}
        #endregion

        /// <summary>
        /// Content func - no correction of data, Bit2 = 0
        /// </summary>
        public void SetNoDataCorrection()
        {
            Content = THelp.ResetBits(Content, (byte)SRecBitPosition.bit_pos_datacorrection);
        }

        /// <summary>
        /// Content func - with data correction, Bit2 = 1
        /// </summary>
        public void SetDataCorrection()
        {
            Content = THelp.SetBits(Content, (byte)SRecBitPosition.bit_pos_datacorrection);
        }

        /// <summary>
        /// Conten func - exclude DiscardedRecCrDTL, Bit1 = 0
        /// </summary>
        public void ExcludeDisRecCrDTL()
        {
            Content = THelp.ResetBits(Content, (byte)SRecBitPosition.bit_pos_include_disreccrdt);
        }

        /// <summary>
        /// Content func - include DiscardedRecCrDTL, Bit1 = 1
        /// </summary>
        public void IncludeDisRecCrDTL()
        {
            Content = THelp.SetBits(Content, (byte)SRecBitPosition.bit_pos_include_disreccrdt);
        }

        #endregion

        /// <summary>
        /// Convert fields to byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            int inx = 0;
            byte[] resp = new byte[50];

            // content - 1 byte
            resp[inx] = Content;
            inx += 1;

            // species - 2 bytes
            byte[] sp = new byte[2] { 0, 0 };
            sp = BitConverter.GetBytes(SpeciesCode);
            Array.Copy(sp, 0, resp, inx, sp.Length);
            inx += Marshal.SizeOf(SpeciesCode);

            // check bit 7 if = 1 full information
            // including Price, QuantityType, Currency
            if (THelp.CheckBits(Content, (byte)SRecBitPosition.FullPacketData_bit_position))
            {
                // Price
                byte[] pr = new byte[4];
                pr = BitConverter.GetBytes(Price);
                Array.Copy(pr, 0, resp, inx, pr.Length);
                inx += Marshal.SizeOf(Price);

                // Currency
                resp[inx] = Currency;
                inx += 1;
            }

            #region commented
            //if (THelp.CheckBits(Content, (byte)SRecBitPosition.QType_bit_pos))
            //{
            //    //Quantity Type
            //    resp[inx] = QuantityType;
            //    inx += 1;
            //}
            #endregion

            // CreationDT
            byte[] time = new byte[4] { 0, 0, 0, 0 };
            time = BitConverter.GetBytes(CreationDTL);
            Array.Copy(time, 0, resp, inx, time.Length);
            inx += Marshal.SizeOf(CreationDTL);

            // DisRecCrDTL - 4 bytes
            byte[] distime = new byte[4] { 0, 0, 0, 0 };
            if (THelp.CheckBits(Content, (byte)SRecBitPosition.bit_pos_include_disreccrdt))
            {
                distime = BitConverter.GetBytes(DisRecCrDTL);
                Array.Copy(distime, 0, resp, inx, distime.Length);
                inx += Marshal.SizeOf(DisRecCrDTL);
            }

            Array.Resize(ref resp, inx);

            return resp;
        }

    }
}
