using ScortelApi.ISS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// GZQuery 
    /// </summary>
    public class GZQuery
    {
        /// <summary>
        /// Unq udentifier
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Type of geo zone
        /// </summary>
        public int ZType { get; set; }
        /// <summary>
        /// Since in seconds
        /// </summary>
        public int Since { get; set; }

    }
}
