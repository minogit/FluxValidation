using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public class CertFGCert
    {
        /// <summary>
        /// needed to create junction table
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// FGear
        /// </summary>
        public CertFG CertFG { get; set; }
        /// <summary>
        /// Certificate 
        /// </summary>
        public Certificate Certificate { get; set; }
    }
}
