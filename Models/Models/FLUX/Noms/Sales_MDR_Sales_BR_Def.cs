namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FLUX_Sales.MDR_Sales_BR_Def")]
    public partial class Sales_MDR_Sales_BR_Def
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

        [StringLength(10)]
        public string startdate { get; set; }

        [StringLength(10)]
        public string enddate { get; set; }

        [StringLength(41)]
        public string BR { get; set; }

        [StringLength(208)]
        public string Level { get; set; }

        [StringLength(96)]
        public string SubLevel { get; set; }

        [StringLength(112)]
        public string Field { get; set; }

        [StringLength(134)]
        public string EnMessage { get; set; }
    }
}
