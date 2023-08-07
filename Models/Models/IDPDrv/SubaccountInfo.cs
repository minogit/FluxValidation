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
    public partial class SubaccountInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

        private string accountIDField;

        private string nameField;

        /// <remarks/>
        //[Index("IX_SubAccInfoUnq", 1, IsUnique = true)]
        public string AccountID
        {
            get
            {
                return this.accountIDField;
            }
            set
            {
                this.accountIDField = value;
            }
        }

        /// <remarks/>
        //[Index("IX_SubAccInfoUnq", 2, IsUnique = true)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }
}
