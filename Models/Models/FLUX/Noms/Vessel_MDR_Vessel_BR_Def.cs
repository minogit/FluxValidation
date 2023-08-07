namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FLUX_Vessel.MDR_Vessel_BR_Def")]
    public partial class Vessel_MDR_Vessel_BR_Def
    {
        public int ID { get; set; }

        [Required]
        [StringLength(500)]
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

        [StringLength(10)]
        public string startdate { get; set; }

        [StringLength(10)]
        public string enddate { get; set; }

        [StringLength(26)]
        public string Brievel { get; set; }

        [StringLength(104)]
        public string BRSubLevel { get; set; }

        [StringLength(123)]
        public string Field { get; set; }

        [StringLength(8408)]
        public string EnMessage { get; set; }

        [StringLength(26)]
        public string brlevel { get; set; }
    }
}
