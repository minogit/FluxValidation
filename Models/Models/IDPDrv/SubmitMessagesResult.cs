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
    public partial class SubmitMessagesResult
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

        private int errorIDField;

        //private ForwardSubmission[] submissionsField;
        private List<ForwardSubmission> submissionsField;

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
        public List<ForwardSubmission> Submissions
        {
            get
            {
                return this.submissionsField;
            }
            set
            {
                this.submissionsField = value;
            }
        }

        ///// <remarks/>
        //public ForwardSubmission[] Submissions
        //{
        //    get
        //    {
        //        return this.submissionsField;
        //    }
        //    set
        //    {
        //        this.submissionsField = value;
        //    }
        //}
    }
}
