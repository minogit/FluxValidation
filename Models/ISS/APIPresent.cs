using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScortelApi.ISS
{
    /// <summary>
    /// ISSO Fish Presentation data
    /// </summary>
    public class APIPresent
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
        public float transform { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int measure_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string measure { get; set; }
    }
}
