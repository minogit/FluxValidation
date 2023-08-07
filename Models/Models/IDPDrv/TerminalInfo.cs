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
    public partial class TerminalInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

        private string primeIDField;

        private string descriptionField;

        private string lastRegistrationUTCField;

        private string regionNameField;

        private string mTSNField;

        private string iMEIField;

        private string mEIDField;

        private string mACField;

        private string pairedTerminalPrimeIDField;

        private string lastMTBPUTCField;

        private string lastMTWSUTCField;

        /// <remarks/>
        public string PrimeID
        {
            get
            {
                return this.primeIDField;
            }
            set
            {
                this.primeIDField = value;
            }
        }

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string LastRegistrationUTC
        {
            get
            {
                return this.lastRegistrationUTCField;
            }
            set
            {
                this.lastRegistrationUTCField = value;
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

        /// <remarks/>
        public string MTSN
        {
            get
            {
                return this.mTSNField;
            }
            set
            {
                this.mTSNField = value;
            }
        }

        /// <remarks/>
        public string IMEI
        {
            get
            {
                return this.iMEIField;
            }
            set
            {
                this.iMEIField = value;
            }
        }

        /// <remarks/>
        public string MEID
        {
            get
            {
                return this.mEIDField;
            }
            set
            {
                this.mEIDField = value;
            }
        }

        /// <remarks/>
        public string MAC
        {
            get
            {
                return this.mACField;
            }
            set
            {
                this.mACField = value;
            }
        }

        /// <remarks/>
        public string PairedTerminalPrimeID
        {
            get
            {
                return this.pairedTerminalPrimeIDField;
            }
            set
            {
                this.pairedTerminalPrimeIDField = value;
            }
        }

        /// <remarks/>
        public string LastMTBPUTC
        {
            get
            {
                return this.lastMTBPUTCField;
            }
            set
            {
                this.lastMTBPUTCField = value;
            }
        }

        /// <remarks/>
        public string LastMTWSUTC
        {
            get
            {
                return this.lastMTWSUTCField;
            }
            set
            {
                this.lastMTWSUTCField = value;
            }
        }
    }
}
