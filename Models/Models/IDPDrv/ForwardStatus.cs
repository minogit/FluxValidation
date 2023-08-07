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
    public partial class ForwardStatus
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

        private int forwardMessageIDField;

        private bool isClosedField;

        private int stateField;

        private string stateUTCField;

        private int errorIDField;

        private bool errorIDFieldSpecified;

        private int referenceNumberField;

        private bool referenceNumberFieldSpecified;

        private string transportField;

        private string scheduledSendUTCField;

        private string regionNameField;

        /// <remarks/>
        public int ForwardMessageID
        {
            get
            {
                return this.forwardMessageIDField;
            }
            set
            {
                this.forwardMessageIDField = value;
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
        public string StateUTC
        {
            get
            {
                return this.stateUTCField;
            }
            set
            {
                this.stateUTCField = value;
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
