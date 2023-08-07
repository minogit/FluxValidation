using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
 
//using GeoAPI;
 

namespace ScortelApi.ISS
{
    /// <summary>
    /// 
    /// </summary>
    public class PGISSPorts
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TimeStamp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Begin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? End { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Altitudemode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Tessellate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Extrude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Visibility { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Draworder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public Polygon WkbGeometry { get; set; }
    }
}
