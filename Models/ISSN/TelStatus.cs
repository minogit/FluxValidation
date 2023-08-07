using ScortelApi.Models.ScortelELB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISSN
{
    /// <summary>
    /// Telemtric data
    /// </summary>
    public class TelStatus
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string GType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VCFR { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PosRec Pos { get; set; }         
    }
     
}
