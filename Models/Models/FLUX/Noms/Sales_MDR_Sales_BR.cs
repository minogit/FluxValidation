namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FLUX_Sales.MDR_Sales_BR")]
    public partial class Sales_MDR_Sales_BR
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

        [StringLength(2)]
        public string Context { get; set; }

        [Column("BR Def")]
        [StringLength(16)]
        public string BR_Def { get; set; }

        [StringLength(1)]
        public string Country { get; set; }

        [StringLength(3)]
        public string Result { get; set; }

        [Column("Active Ind")]
        [StringLength(1)]
        public string Active_Ind { get; set; }
    }
}
