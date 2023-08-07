using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public class ZoneDefinition
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        ///  zone name
        /// </summary>
        //[Index("IX_ZoneDef_Unq", 1, IsUnique = true)]
        public string Name { get; set; }

        public string NameBG { get; set; }

        public string CodeStr { get; set; }

        /// <summary>
        ///  zone identification code
        /// </summary>
        //[Index("IX_ZoneDef_Unq", 2, IsUnique = true)]
        public int Code { get; set; }

        /// <summary>
        /// zone figure type
        /// 0 - point
        /// 1 - cirecle
        /// 2 - polygone
        /// </summary>
        public int FirgureType { get; set; }

        /// <summary>
        /// Count of figure points
        /// point - 1 without radius
        /// cirecle - 1 + radius
        /// polygone - N without radius 
        /// </summary>
        public int PointsCount { get; set; }

        /// <summary>
        /// Radius describing corcle 
        /// </summary>
        public int Radius { get; set; }

        /// <summary>
        /// List of points
        /// Sample: 
        /// lat + space + lng + , + lat + space + lng
        /// 42.34 23.33, 44.22 21.356, 47.32 24.22, 42.34 23.33
        /// </summary>
        public string Coords { get; set; }

        /// <summary>
        /// Fishing activities access level
        /// ????
        /// 0 - allowed
        /// 1 - limited/ restricted for certain period or species
        /// 2 - forbidden for certain period or species
        /// </summary>
        //public int FishingAccessLvl { get; set; }
        public ZAccessLvl FishingAccessLvl { get; set; }

        /// <summary>
        /// Start of period
        /// </summary>
        public DateTime StartPeriod { get; set; }

        /// <summary>
        /// end of period
        /// </summary>
        public DateTime EndPeriod { get; set; }

        /// <summary>
        /// Allowed species for fishing
        /// </summary>
        public List<Species> AllowedSpecies { get; set; } = new List<Species>();

        /// <summary>
        /// Forbidden species for fishing
        /// </summary>
        public List<Species> RestrictedSpecies { get; set; } = new List<Species>();

        /// <summary>
        /// Zone type
        /// 0 - reserved
        /// 1 - economic zone
        /// 2 - statistical zone
        /// 3 - other
        /// </summary>
        //TODO:        
        public ZoneType ZoneType { get; set; }

    }
}
