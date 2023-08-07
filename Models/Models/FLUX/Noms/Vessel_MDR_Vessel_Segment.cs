namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FLUX_Vessel.MDR_Vessel_Segment")]
    public partial class Vessel_MDR_Vessel_Segment
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

        [StringLength(5)]
        public string vesselpgm { get; set; }

        [StringLength(3)]
        public string country { get; set; }

        [StringLength(4)]
        public string segclass { get; set; }

        [StringLength(1)]
        public string outerind { get; set; }
    }
}
