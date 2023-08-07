namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FLUX_FA.MDR_FA_BR_EU")]
    public partial class FA_MDR_FA_BR_EU
    {
        public int ID { get; set; }

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

        [StringLength(14)]
        public string BRDef { get; set; }

        [StringLength(14)]
        public string Country { get; set; }

        [StringLength(3)]
        public string Result { get; set; }

        [StringLength(3)]
        public string ActiveInd { get; set; }
    }
}
