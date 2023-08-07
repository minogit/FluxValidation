namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("MDR_Gear_Type")]
    public partial class MDR_Gear_Type
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

        [StringLength(28)]
        public string Category { get; set; }

        [StringLength(15)]
        public string SubCategory { get; set; }

        [StringLength(3)]
        public string ParentGear { get; set; }

        [StringLength(8)]
        public string GearActivity { get; set; }

        [StringLength(16)]
        public string Target { get; set; }

        [StringLength(45)]
        public string Description { get; set; }

        [StringLength(6)]
        public string ISSCFGCode { get; set; }
    }
}
