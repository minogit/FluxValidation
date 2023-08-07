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
    public partial class ForwardMessageRecord
    {

        private int idField;

        private string statusUTCField;

        private string createUTCField;

        private bool isClosedField;

        private int stateField;

        private string destinationIDField;

        private int errorIDField;

        private bool errorIDFieldSpecified;

        private byte[] rawPayloadField;

        private Message payloadField;

        private int referenceNumberField;

        private bool referenceNumberFieldSpecified;

        private string transportField;

        private string scheduledSendUTCField;

        private string regionNameField;

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <remarks/>
        public int ID
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

        /// <remarks/>
        public string StatusUTC
        {
            get
            {
                return this.statusUTCField;
            }
            set
            {
                this.statusUTCField = value;
            }
        }

        /// <remarks/>
        public string CreateUTC
        {
            get
            {
                return this.createUTCField;
            }
            set
            {
                this.createUTCField = value;
            }
        }

        /// <remarks/>
        public bool IsClosed
        {
            get
            {
                return this.isClosedField;
            }
            set
            {
                this.isClosedField = value;
            }
        }

        /// <remarks/>
        public int State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
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
        public int ErrorID
        {
            get
            {
                return this.errorIDField;
            }
            set
            {
                this.errorIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ErrorIDSpecified
        {
            get
            {
                return this.errorIDFieldSpecified;
            }
            set
            {
                this.errorIDFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
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
        public int ReferenceNumber
        {
            get
            {
                return this.referenceNumberField;
            }
            set
            {
                this.referenceNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReferenceNumberSpecified
        {
            get
            {
                return this.referenceNumberFieldSpecified;
            }
            set
            {
                this.referenceNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string Transport
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

        /// <remarks/>
        public string ScheduledSendUTC
        {
            get
            {
                return this.scheduledSendUTCField;
            }
            set
            {
                this.scheduledSendUTCField = value;
            }
        }

        /// <remarks/>
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
    }
}
