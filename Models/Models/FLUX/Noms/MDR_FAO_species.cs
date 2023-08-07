namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("MDR_FAO_species")]
    public partial class MDR_FAO_species
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

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

        [StringLength(13)]
        public string TaxCode { get; set; }

        [Column("Scientific name")]
        [StringLength(50)]
        public string Scientific_name { get; set; }

        [Column("English name")]
        [StringLength(50)]
        public string English_name { get; set; }

        [Column("French name")]
        [StringLength(50)]
        public string French_name { get; set; }

        [Column("Spanish name")]
        [StringLength(50)]
        public string Spanish_name { get; set; }

        [StringLength(30)]
        public string Family { get; set; }

        [StringLength(50)]
        public string Order { get; set; }
    }
}
