namespace  ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FLUX_Sales.MDR_Country_Currency")]
    public partial class Sales_MDR_Country_Currency
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

        [StringLength(3)]
        public string CountryCode { get; set; }

        [StringLength(4479)]
        public string EnName { get; set; }

        [StringLength(3)]
        public string CurrencyCode { get; set; }

        [StringLength(21)]
        public string EnName1 { get; set; }
    }
}
