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
    public partial class ForwardSubmission
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

        private int forwardMessageIDField;

        private string destinationIDField;

        private int errorIDField;

        private bool errorIDFieldSpecified;

        private long userMessageIDField;

        private bool userMessageIDFieldSpecified;

        private string scheduledSendUTCField;

        private int terminalWakeupPeriodField;

        private bool terminalWakeupPeriodFieldSpecified;

        private System.Nullable<int> oTAMessageSizeField;

        private bool oTAMessageSizeFieldSpecified;

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
        public int TerminalWakeupPeriod
        {
            get
            {
                return this.terminalWakeupPeriodField;
            }
            set
            {
                this.terminalWakeupPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TerminalWakeupPeriodSpecified
        {
            get
            {
                return this.terminalWakeupPeriodFieldSpecified;
            }
            set
            {
                this.terminalWakeupPeriodFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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
    }
}
