using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.IDPDrv
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ReturnMessage
    {

        private long idField;

        private string messageUTCField;

        private string receiveUTCField;

        private int sINField;

        private string mobileIDField;

        private byte[] rawPayloadField;

        private Message payloadField;

        private string regionNameField;

        private System.Nullable<int> oTAMessageSizeField;

        private bool oTAMessageSizeFieldSpecified;

        private System.Nullable<int> customerIDField;

        private bool customerIDFieldSpecified;

        private int transportField;

        private System.Nullable<int> mobileOwnerIDField;

        private bool mobileOwnerIDFieldSpecified;
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime RecordedUTC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? StatusTransWialonIPS { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public DateTime SentWialonIPS { get; set; }

        /// <summary>         
        /// 1.if packet is standard IDP packet (SIN X) => NeedAck = false/ 0 ; IsAcked = false/0
        ///   
        /// 2.if packet is ScortelFishery (SIN = 239) check its description
        ///     a. the packet is Ack => NeedAck = false/0; IsAcked = false/0
        ///        Update ForwardMessages packet who waiting this ack, NeedAck = false/0; IsAcked = true/1
        ///     b. the packet is Data => NeedAck = true/1; IsAcked = false/0 
        ///        Generate Ack and add new row to ForwardMessages => NeedAck = false/0; IsAcked = false/0
        /// </summary>
        public byte NeedAck { get; set; }

        /// <summary>         
        /// 1.if packet is standard IDP packet (SIN X) => NeedAck = false/ 0 ; IsAcked = false/0
        ///   
        /// 2.if packet is ScortelFishery (SIN = 239) check its description
        ///     a. the packet is Ack => NeedAck = false/0; IsAcked = false/0
        ///        Update ForwardMessages packet who waiting this ack, NeedAck = false/0; IsAcked = true/1
        ///     b. the packet is Data => NeedAck = true/1; IsAcked = false/0 
        ///        Generate Ack and add new row to ForwardMessages => NeedAck = false/0; IsAcked = false/0
        /// </summary>
        public byte IsAcked { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("ID")]
        public long ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MessageUTC
        {
            get
            {
                return this.messageUTCField;
            }
            set
            {
                this.messageUTCField = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReceiveUTC
        {
            get
            {
                return this.receiveUTCField;
            }
            set
            {
                this.receiveUTCField = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SIN
        {
            get
            {
                return this.sINField;
            }
            set
            {
                this.sINField = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MobileID
        {
            get
            {
                return this.mobileIDField;
            }
            set
            {
                this.mobileIDField = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] RawPayload
        {
            get
            {
                return this.rawPayloadField;
            }
            set
            {
                this.rawPayloadField = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual Message Payload
        {
            get
            {
                return this.payloadField;
            }
            set
            {
                this.payloadField = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RegionName
        {
            get
            {
                return this.regionNameField;
            }
            set
            {
                this.regionNameField = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Nullable<int> OTAMessageSize
        {
            get
            {
                return this.oTAMessageSizeField;
            }
            set
            {
                this.oTAMessageSizeField = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool OTAMessageSizeSpecified
        {
            get
            {
                return this.oTAMessageSizeFieldSpecified;
            }
            set
            {
                this.oTAMessageSizeFieldSpecified = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Nullable<int> CustomerID
        {
            get
            {
                return this.customerIDField;
            }
            set
            {
                this.customerIDField = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool CustomerIDSpecified
        {
            get
            {
                return this.customerIDFieldSpecified;
            }
            set
            {
                this.customerIDFieldSpecified = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Transport
        {
            get
            {
                return this.transportField;
            }
            set
            {
                this.transportField = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Nullable<int> MobileOwnerID
        {
            get
            {
                return this.mobileOwnerIDField;
            }
            set
            {
                this.mobileOwnerIDField = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool MobileOwnerIDSpecified
        {
            get
            {
                return this.mobileOwnerIDFieldSpecified;
            }
            set
            {
                this.mobileOwnerIDFieldSpecified = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>        
        public int? StatusTransISS { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public int? StatusTransNISS { get; set; } = 0;
    }
}
