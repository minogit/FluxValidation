using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISS
{
    /// <summary>
    /// Данни за удостоверения за риболов на дадено РК от API интерфейс на стара ИСС на ИАРА
    /// 
    /// </summary>
    public class ApiCerts
    {
        /// <summary>
        /// Init in FVMS
        /// </summary> 
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Init in FVMS
        /// Време на запис в СНРК
        /// </summary>
        public DateTime StoredDT { get; set; }
        /// <summary>
        /// Init in FVMS
        /// Време на актуализация на данните в СНРК
        /// </summary>
        public DateTime UpdatedDT { get; set; }
        /// <summary>
        /// Vessel name  - БУМЕРАНГ
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Vessel CFR - BGR002031477
        /// </summary>
        public string cfr { get; set; }
        /// <summary>
        /// FVMS - not in use
        /// </summary>
        public int dnevnik_id { get; set; }
        /// <summary>
        /// Д03108381-025-001
        /// </summary>
        public string dnevnik_nomer { get; set; }
        /// <summary>
        /// Valid from 
        /// </summary>
        public DateTime dnevnik_start_date { get; set; }
        /// <summary>
        /// Used in integration with old ISS, and in FVMS process of creation page 
        /// </summary>        
        public long dnevnik_start_page { get; set; }
        /// <summary>
        /// Used in integration with old ISS, and in FVMS process of creation page 
        /// </summary> 
        public long dnevnik_end_page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime dnevnik_create_date { get; set; }
        /// <summary>
        /// 03108381-025
        /// </summary>
        public string udo_nomer { get; set; }
        /// <summary>
        /// Valid from
        /// </summary>
        public DateTime udo_valid_from { get; set; } 
        /// <summary>
        /// Valid to
        /// </summary>
        public DateTime udo_valid_to { get; set; }
        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime udo_create_date { get; set; }
        /// <summary>
        /// Used in integration with old ISS, and in FVMS process of creation page 
        /// </summary>    
        public long current_page { get; set; }
        /// <summary>
        /// Used in integration with old ISS, and in FVMS process of creation page 
        /// </summary>
        public bool next_page { get; set; }


        /// <summary>
        /// Initializing in FVMS      
        /// 1. Get only license/certs from ISS by CFR
        /// 2. Get only gears by cfr
        /// 3. Init Certs with 2. result
        /// </summary>
        public ICollection<ApiFGear> Gears { get; set; }
        /// <summary>
        /// Init in FVMS
        /// </summary>
        public ApiPerm ApiPerm { get; set; }
        /// <summary>
        /// Init in FVMS
        /// </summary>
        [NotMapped]
        public bool hitted { get; set; }
        /// <summary>
        /// Init in FVMS
        /// </summary>
        public bool isDeleted { get; set; }
    }
}
