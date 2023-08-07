using Newtonsoft.Json;
using ScortelApi.Models.ScortelELB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISS
{
    /// <summary>
    /// ISS Fishing Permits
    /// 
    /// Checked with new ISS Integration documentation - 24.09.2020
    /// </summary>
    public class ApiPerm
    {
      
        /// <summary>
        /// Initializing in FVMS
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Initializing in FVMS
        /// </summary>
        public DateTime StoredDT { get; set; }
        /// <summary>
        /// Initializing in FVMS
        /// </summary>
        public DateTime UpdatedDT { get; set; }
        /// <summary>
        /// Vessel CFR
        /// </summary>
        public string VCFR { get; set; }

        #region Fields from api json
        /// <summary>
        /// FVMS - not in use
        /// ISS DB Id - sequence number
        /// ИД в БД на ИСС
        /// </summary>
        [JsonProperty("rsrid")]
        [System.Text.Json.Serialization.JsonIgnore]
        public int id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// Type of permit
        /// </summary>
        public int rsr_type { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// Type of permit
        /// </summary>
        public int sto_type { get; set; }

        /// <summary>
        /// Permit number - 03108281
        /// </summary>
        public string nomer { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// Permit validity
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS DB Id of owner
        /// </summary>
        public string tituliar_id { get; set; }

        /// <summary>    
        /// FVMS - not in use
        /// ISS DB Id of user
        /// int type
        /// </summary>
        public string ribar_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS DB ship Id
        /// int type
        /// </summary>
        public string ship_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// int type
        /// </summary>
        public string ship_doc_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS DB Id dalqn if exist
        /// int type
        /// </summary>
        public string dalian_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// int type
        /// </summary>
        public string dalian_doc_doc_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// float type
        /// </summary>        
        public string dalian_tax { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// object type
        /// </summary>
        public string dalian_tax_date { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// int type
        /// </summary>
        public string stopanstvo_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        ///  int type
        /// </summary>
        public string sto_doc_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// int type
        /// </summary>
        public string zveno_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// </summary>
        public string ctrl_code { get; set; }

        /// <summary>
        /// Permit creation datetime
        /// </summary>
        public DateTime? create_date { get; set; }
 
        /// <summary>
        /// Permit valid if revoke_flag = 0 or revoke_flg = ""
        /// </summary>
        public string revoke_flag { get; set; }

        /// <summary>
        /// FVMS - Not in use 
        /// object type
        /// </summary>
        public string revoke_date { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// object type
        /// </summary>
        public string revoke_reason { get; set; }
        #endregion
        /// <summary>
        /// Initializing in FVMS 
        /// 1. Get only permits from ISS by CFR
        /// 2. Get only license/certs from ISS by CFR
        /// 3. Init Certs with 2. result
        /// </summary>
        public ICollection<ApiCerts> Certs { get; set; }
        /// <summary>
        /// Initializing in FVMS
        /// </summary>
        [NotMapped]
        public bool hitted { get; set; }
        /// <summary>
        /// Initializing in FVMS
        /// </summary>
        public bool isDeleted { get; set; }
    }
}
