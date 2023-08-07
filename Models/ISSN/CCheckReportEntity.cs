using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISSN
{
    /// <summary>
    /// /
    /// </summary>
    public class CCheckReportEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CrDT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CCheckEntity CCheck { get; set; }
    }
}
