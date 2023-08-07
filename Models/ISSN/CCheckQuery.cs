using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISSN
{
    /// <summary>
    /// CCheck query
    /// </summary>
    public class CCheckQuery
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Since in seconds
        /// </summary>
        public int Since { get; set; }
    }
}
