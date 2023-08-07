namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("MDR_Conversion_Factor")]
    public partial class MDR_Conversion_Factor
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        [StringLength(500)]
        public string CreatedBy { get; set; }

        [StringLength(500)]
        public string UpdatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [StringLength(3)]
        public string SpeciesCode { get; set; }

        [StringLength(3)]
        public string State { get; set; }

        [StringLength(7)]
        public string Presentation { get; set; }

        public float? Factor { get; set; }

        [StringLength(5)]
        public string Country { get; set; }

        [StringLength(17)]
        public string LegalSource { get; set; }

        [StringLength(1)]
        public string Collective { get; set; }

        [StringLength(18)]
        public string Comment { get; set; }
    }
}
