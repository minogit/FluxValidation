namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("MDR_Location")]
    public partial class MDR_Location
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

        [StringLength(3)]
        public string iso3 { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public float? Latitude { get; set; }

        public float? Longitude { get; set; }

        [StringLength(5)]
        public string FishingPort { get; set; }

        [StringLength(5)]
        public string LandingPlace { get; set; }

        [StringLength(5)]
        public string CommercialPort { get; set; }

        [StringLength(5)]
        public string UNLoCode { get; set; }

        [StringLength(12)]
        public string Coordinates { get; set; }

        [StringLength(8)]
        public string Function { get; set; }
    }
}
