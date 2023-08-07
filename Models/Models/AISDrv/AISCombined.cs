using ScortelApi.Models.ScortelELB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.AISDrv
{
    /// <summary>
    /// 
    /// </summary>
    public class AISCombined  
    {
        /// <summary>
        /// 
        /// </summary>
        public AIS_MsgType_1_3 n { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AIS_MsgType_5 k { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Vessel m { get; set; }      
    }
}
