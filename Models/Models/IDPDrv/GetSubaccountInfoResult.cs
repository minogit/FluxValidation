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
    public partial class GetSubaccountInfoResult
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

        //private SubaccountInfo[] subaccountsField;
        private List<SubaccountInfo> subaccountsField;

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
        public List<SubaccountInfo> Subaccounts
        {
            get
            {
                return this.subaccountsField;
            }
            set
            {
                this.subaccountsField = value;
            }
        }
    }
}
