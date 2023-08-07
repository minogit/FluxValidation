using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScortelApi.ISS
{
 
     /// <summary>
     /// 
     /// </summary>
    public class APIFGearNameList
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 
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
        public int? group { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? gear_p { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? gear_d { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gear_features { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gear_ename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gear_fopers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gear_vesseltype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gear_where { get; set; }
    }
}
