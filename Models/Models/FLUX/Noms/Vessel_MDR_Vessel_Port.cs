namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FLUX_Vessel.MDR_Vessel_Port")]
    public partial class Vessel_MDR_Vessel_Port
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(500)]
        public string EnDescription { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        [StringLength(500)]
        public string CreatedBy { get; set; }

        [StringLength(500)]
        public string UpdatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [StringLength(3)]
        public string Country { get; set; }

        [StringLength(1)]
        public string OuterInd { get; set; }

        [StringLength(5)]
        public string NUTs { get; set; }
    }
}
