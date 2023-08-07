namespace ScortelApi.Models.FLUX.Noms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    /// <summary>
    /// 
    /// </summary>
    [Table("FLUX_ACDR.MDR_ACDR_Catch_Status")]
    public partial class ACDR_MDR_ACDR_Catch_Status
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(500)]
        public string EnDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ValidFrom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ValidTo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(500)]
        public string CreatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(500)]
        public string UpdatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdatedOn { get; set; }
    }
}
