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
    public partial class GetForwardStatusesResult
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// new field
        /// </summary>
        public DateTime Utc { get; set; }

        private int errorIDField;

        private string nextStartUTCField;
        
        private List<ForwardStatus> statusesField;

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
        public string NextStartUTC
        {
            get
            {
                return this.nextStartUTCField;
            }
            set
            {
                this.nextStartUTCField = value;
            }
        }

        /// <remarks/>
        public List<ForwardStatus> Statuses
        {
            get
            {
                return this.statusesField;
            }
            set
            {
                this.statusesField = value;
            }
        }
    }
}
