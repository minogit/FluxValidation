using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISS
{
    /// <summary>
    /// 
    /// </summary>
    public class InspData
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Names { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Cardnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Sector { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Code { get; set; }
    }
}
