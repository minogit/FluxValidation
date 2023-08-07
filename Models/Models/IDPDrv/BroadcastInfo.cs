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
    public partial class BroadcastInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

        private string idField;

        private string descriptionField;

        /// <remarks/>
        //[Index("IX_BrCastInfoUnq", 1, IsUnique = true)]
        public string ID
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
        //[Index("IX_BrCastInfoUnq", 2, IsUnique = true)]
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
    }
}
