using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScortelApi.ISS
{
    /// <summary>
    /// ISSO - Fish data
    /// </summary>
    public class APIFish
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
        public string lname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? group_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? family_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string en_name { get; set; }
    }
}
