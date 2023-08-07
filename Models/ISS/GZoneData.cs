using ScortelApi.Models.FLUX.Noms;
using ScortelApi.Models.ScortelELB;
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
    public class GZoneData
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Creation timestamp UTC
        /// </summary>
        public DateTime CrDT { get; set; }
        /// <summary>
        /// Access lvl
        /// </summary>
        public ZAccessLvl AccLvl { get; set; }
        /// <summary>
        /// Zone type
        /// </summary>
        public ZoneType ZType { get; set; }
        /// <summary>
        /// Resricted/ Forbidden/ Allowed species
        /// </summary>
        public List<MDR_FAO_species> Species { get; set; }
        /// <summary>
        /// Target groups
        /// </summary>
        public List<FA_MDR_Target_Species_Group> Groups { get; set; }
        /// <summary>
        /// Restriction/ allowance valid from timestamp UTC
        /// </summary>
        public DateTime FromDT { get; set; }
        /// <summary>
        /// Restriction/ allowance valid to timestamp UTC
        /// </summary>
        public DateTime ToDT { get; set; }
        /// <summary>
        /// Is valid Restriction/ allowance
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// List of geo data/ zones
        /// </summary>
        public List<ISSPolygon> GeoZD { get; set; }
    }
}
