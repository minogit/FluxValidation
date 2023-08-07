namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("MDR_Stat_Rect")]
    public partial class MDR_Stat_Rect
    {
        public int ID { get; set; }

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

        [StringLength(5)]
        public string ICESNAME { get; set; }

        public float? SOUTH { get; set; }

        public float? WEST { get; set; }

        public float? NORTH { get; set; }

        public float? EAST { get; set; }

        [StringLength(4)]
        public string SOURCE { get; set; }
    }
}
