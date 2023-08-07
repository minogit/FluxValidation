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
    public partial class IGWSInformationResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

        private int errorIDField;

        private string uTCField;

        private string versionField;

        //private ErrorInfo[] errorCodesField;
        private List<ErrorInfo> errorCodesField;

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
        public string UTC
        {
            get
            {
                return this.uTCField;
            }
            set
            {
                this.uTCField = value;
            }
        }

        /// <remarks/>
        //[Index("IX_IGWSInformationResponseUnq", 1, IsUnique = true)]
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        public List<ErrorInfo> ErrorCodes
        {
            get
            {
                return this.errorCodesField;
            }
            set
            {
                this.errorCodesField = value;
            }
        }

        ///// <remarks/>
        //public ErrorInfo[] ErrorCodes
        //{
        //    get
        //    {
        //        return this.errorCodesField;
        //    }
        //    set
        //    {
        //        this.errorCodesField = value;
        //    }
        //}
    }
}
