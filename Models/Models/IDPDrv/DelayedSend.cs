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
    public partial class DelayedSend
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

        private bool satelliteSendOnReceiveField;

        private string messageExpireUTCField;

        /// <remarks/>
        public bool SatelliteSendOnReceive
        {
            get
            {
                return this.satelliteSendOnReceiveField;
            }
            set
            {
                this.satelliteSendOnReceiveField = value;
            }
        }

        /// <remarks/>
        public string MessageExpireUTC
        {
            get
            {
                return this.messageExpireUTCField;
            }
            set
            {
                this.messageExpireUTCField = value;
            }
        }
    }
}
