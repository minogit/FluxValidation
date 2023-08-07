using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISSN
{
    /// <summary>
    /// 
    /// </summary>
    public class TelQuery
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid UUID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VCFR { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public uint Since { get; set; }
    }
}
