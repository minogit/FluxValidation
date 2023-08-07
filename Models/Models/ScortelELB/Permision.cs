using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public class Permision
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Certificate uniqe number
        /// </summary>
        public string Num { get; set; }
        /// <summary>
        /// Is valid certificate
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        /// Certificate valid from datatime
        /// </summary>
        public DateTime ValidFrom { get; set; }
        /// <summary>
        /// Certificate valid to datetime
        /// </summary>
        public DateTime ValidTo { get; set; }
        /// <summary>
        /// Last successful ELB update 
        /// </summary>
        public DateTime LUELB { get; set; }
        /// <summary>
        /// Last update from iss
        /// </summary>
        public DateTime LUFromISS { get; set; }
        /// <summary>
        /// Have to update data       
        /// </summary>
        public bool MustUpdate { get; set; }

        /// <summary>
        /// 0 - ok 
        /// 1 - history
        /// </summary>
        public byte IsHistory { get; set; }

        public List<Certificate> Certificates { get; set; }

         
 
    }
}
