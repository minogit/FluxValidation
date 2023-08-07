using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISSN
{
    /// <summary>
    /// 
    /// </summary>
    public class CCheckEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 
        /// 
        /// </summary>
        public string Grid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TrDS { get; set; }   
        /// <summary>
        /// 
        /// </summary>
        public string TrDF { get; set; }  
        /// <summary>
        /// 
        /// </summary>
        public string ChDS { get; set; }    
        /// <summary>
        /// 
        /// </summary>
        public string ChDF { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MDReq { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string ChDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Purpose { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Precond { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte Level { get; set; } 
        /// <summary>
        /// /
        /// </summary>
        public byte Necessity { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string LegalRef { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Remarks { get; set; }  
        /// <summary>
        /// 
        /// </summary>
        public DateTime DAdd { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public bool CCRes { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public DateTime LUpd { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string VesselCFR { get; set; } 
    }
}
