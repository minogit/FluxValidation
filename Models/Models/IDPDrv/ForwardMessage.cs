using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.IDPDrv
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ForwardMessage
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

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
        /// If packet is sent to IDP gateway 
        /// Not sent = 0
        /// Sent = 1
        /// </summary>
        public byte IsSentIDPGate { get; set; }

        /// <summary>
        ///  Additional field FVMS-Server apps
        ///  Timestamp recorded to DB
        /// </summary> 
        //[Index("IX_FMsgUnq", 1, IsUnique = true)]
        public DateTime RecordedUTC { get; set; }

        /// <summary>
        /// Send packet SIN if there is system packet
        /// </summary>
        public int PacketSIN { get; set; }
        /// <summary>
        /// Send packet MIN if there is system packet
        /// </summary>
        public int PacketMIN { get; set; }
        /// <summary>
        /// Packet type
        /// - PollRequest
        /// - TxtMsg
        /// - CMsg
        /// - CCmd
        /// </summary>
        public string PacketType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string GatewayError { get; set; }

        /// <summary>
        /// Recorded from app
        /// 1. Wialon
        /// 2. Server apps
        /// 3. IS
        /// 4. FLUX
        /// 5. other
        /// </summary> 
        public int? RecordedBySw { get; set; } = 0;

        /// <summary>
        /// Scortel ELB Protocol sequence number of packet
        /// </summary>
        public byte ELBSeq { get; set; }

        public int ForwardMessageID { get; set; }

        ///// <summary>
        ///// if there is received message data             -> timestamp of msg
        ///// if the packet is data and no received message -> default timestamp
        ///// </summary>
        //public DateTime ReceivedMsgTimestamp { get; set; }

        ///// <summary>
        /////  
        ///// </summary>
        ////[Index("IX_FMsgUnq", 2, IsUnique = true)]
        //public byte ELBPacketDesc { get; set; }

        private string destinationIDField;

        private long userMessageIDField;

        private bool userMessageIDFieldSpecified;

        private byte[] rawPayloadField;
        //private List<byte> rawPayloadField;

        private Message payloadField;

        private string transportTypeField;

        private DelayedSend sendOptionsField;

        /// <remarks/>
        //[Index("IX_FMsgUnq", 2, IsUnique = true)]
        public string DestinationID
        {
            get
            {
                return this.destinationIDField;
            }
            set
            {
                this.destinationIDField = value;
            }
        }

        /// <remarks/>
        public long UserMessageID
        {
            get
            {
                return this.userMessageIDField;
            }
            set
            {
                this.userMessageIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UserMessageIDSpecified
        {
            get
            {
                return this.userMessageIDFieldSpecified;
            }
            set
            {
                this.userMessageIDFieldSpecified = value;
            }
        }

        /// <remarks/>
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

        /// <remarks/>
        public Message Payload
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

        /// <remarks/>
        public string TransportType
        {
            get
            {
                return this.transportTypeField;
            }
            set
            {
                this.transportTypeField = value;
            }
        }

        /// <remarks/>
        public DelayedSend SendOptions
        {
            get
            {
                return this.sendOptionsField;
            }
            set
            {
                this.sendOptionsField = value;
            }
        }
    }
}
