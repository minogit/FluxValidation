using ScortelApi.ISS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public class GZRepEntity
    {
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// DateTime of report creation/ UTC
        /// </summary>         
        public DateTime CrDT { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public virtual List<GZoneData> GZones { get; set; }
    }
}
