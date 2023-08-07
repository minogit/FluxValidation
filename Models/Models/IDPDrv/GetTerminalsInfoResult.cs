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
    public partial class GetTerminalsInfoResult
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

        //private TerminalInfo[] terminalsField;
        private List<TerminalInfo> terminalsField;

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
        public List<TerminalInfo> Terminals
        {
            get
            {
                return this.terminalsField;
            }
            set
            {
                this.terminalsField = value;
            }
        }
    }
}
