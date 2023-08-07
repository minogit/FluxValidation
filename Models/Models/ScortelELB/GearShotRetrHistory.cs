using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public class GearShotRetrHistory
    {
        [Key]
        public long Id { get; set; }
        public long vessel_id { get; set; }

        public DateTime Occurance { get; set; }      

        /// <summary>
        /// Trip id with fishing operatioin gear_shot
        /// </summary>
        public string? StartFTripStr { get; set; }
        /// <summary>
        /// Fishing operation with gear_shot
        /// </summary>
        public long? StartFO_id { get; set; }
        /// <summary>
        /// Start fishing operation with gear_shot
        /// </summary>
        public long? StartFO_StartSEFO_id { get; set; }
        /// <summary>
        /// End fishing operation with gear_shot
        /// </summary>
        public long? StartFO_EndSEFO_id { get; set; }

        /// <summary>
        /// Can be the same trip when gear_shot or other trip with fishing operation with gear_retrieve bit7 = 1        
        /// </summary>
        public string? EndFTripStr { get; set; }
        /// <summary>
        /// Fishing operation with gear retrieval
        /// </summary>
        public long? EndFO_id { get; set; }
        /// <summary>
        /// Start fishing operation with gear_retrieval
        /// </summary>
        public long? EndFO_StartSEFO_id { get; set; }
        /// <summary>
        /// End fishing operation with gear_retrieval
        /// </summary>
        public long? EndFO_EndSEFO_id { get; set; }

        public string? FGCode { get; set; }
        public int? FGGearEyeLight { get; set; }

        /// <summary>
        /// Status of operation
        /// 0 - Gear_shot
        /// 1 - Gear_retrieval
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Status if gear was retrieved or not
        /// 0 - not completed - there is no fishing operation that matching previous fishing 
        ///     operation with startfishing operation with gear shot activity 
        /// 1 - completed - there is fishing operation that match previous fishing operation
        /// </summary>
        public int Completed { get; set; }

        public string? StartUUID { get; set; }
        public string? EndUUID { get; set; }
        public DateTime? CompletedDT { get; set; }

    }
}
