namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FLUX_Vessel.MDR_Vessel_BR")]
    public partial class Vessel_MDR_Vessel_BR
    {
        public int ID { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        [StringLength(500)]
        public string CreatedBy { get; set; }

        [StringLength(500)]
        public string UpdatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [StringLength(2)]
        public string Context { get; set; }

        [StringLength(18)]
        public string BRReference { get; set; }

        [StringLength(3)]
        public string Country { get; set; }

        [StringLength(3)]
        public string BRRes { get; set; }

        [StringLength(1)]
        public string ActiveInd { get; set; }
    }
}
