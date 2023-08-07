using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISS
{
    /// <summary>
    /// ISSO Ports data
    /// </summary>
    public class APIPorts
    {
        /// <summary>
        /// DB id
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// ISSO DB id
        /// </summary>
        [Column("iss_id")]
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int group { get; set; }
    }
}
