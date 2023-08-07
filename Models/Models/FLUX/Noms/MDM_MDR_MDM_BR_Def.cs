namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FLUX_MDM.MDR_MDM_BR_Def")]
    public partial class MDM_MDR_MDM_BR_Def
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
        public string Level { get; set; }

        public int? SubLevel { get; set; }

        [StringLength(47)]
        public string Field { get; set; }

        [StringLength(83)]
        public string EnMessage { get; set; }
    }
}
