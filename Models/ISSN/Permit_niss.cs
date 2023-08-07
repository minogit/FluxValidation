using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScortelApi.ISSN
{
    /// <summary>
    /// ISSM Permit model
    /// </summary>
    public class Permit_niss
    {
        /// <summary>
        /// DB Id
        /// </summary>
        [JsonIgnore]
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Permit number - 03108281
        /// </summary>
        [JsonPropertyName("nomer")]
        public string Number { get; set; }

        /// <summary>
        /// Permit creation datetime
        /// </summary>
        [JsonPropertyName("create_date")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Permit valid if revoke_flag = 0 or revoke_flg = ""
        /// </summary>
        [JsonPropertyName("revoke_flag")]
        public string revoke_flag { get; set; }

        [JsonIgnore]
        public bool IsRevoked
        {
            get
            {
                return revoke_flag == "0" ? true : !string.IsNullOrEmpty(revoke_flag);
            }
            set
            {
                if (value)
                {
                    revoke_flag = "0";
                }
                else
                {
                    revoke_flag = "";
                }
            }
        }

        /// <summary>
        /// Vessel CFR
        /// </summary>
        [JsonPropertyName("VCFR")]
        public string CFR { get; set; }

        /// <summary>
        /// Initializing in FVMS 
        /// 1. Get only permits from ISS by CFR
        /// 2. Get only license/certs from ISS by CFR
        /// 3. Init Certs with 2. result
        /// </summary>
        [JsonPropertyName("Certs")]
        public ICollection<Certificate_niss> Certificates { get; set; }

        //************************************************************

        /// <summary>
        /// Initializing in FVMS
        /// </summary>
        [JsonIgnore]
        public DateTime StoredDT { get; set; }

        /// <summary>
        /// Initializing in FVMS
        /// </summary>
        [JsonIgnore]
        public DateTime UpdatedDT { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS DB Id - sequence number
        /// ИД в БД на ИСС
        /// </summary>
        [JsonIgnore]
        public int rsrid { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// Type of permit
        /// </summary>
        [JsonIgnore]
        public int rsr_type { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// Type of permit
        /// </summary>
        [JsonIgnore]
        public int sto_type { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// Permit validity
        /// </summary>
        [JsonIgnore]
        public string data { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS DB Id of owner
        /// </summary>
        [JsonIgnore]
        public string tituliar_id { get; set; }

        /// <summary>    
        /// FVMS - not in use
        /// ISS DB Id of user
        /// int type
        /// </summary>
        [JsonIgnore]
        public string ribar_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS DB ship Id
        /// int type
        /// </summary>
        [JsonIgnore]
        public string ship_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// int type
        /// </summary>
        [JsonIgnore]
        public string ship_doc_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS DB Id dalqn if exist
        /// int type
        /// </summary>
        [JsonIgnore]
        public string dalian_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// int type
        /// </summary>
        [JsonIgnore]
        public string dalian_doc_doc_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// float type
        /// </summary>    
        [JsonIgnore]
        public string dalian_tax { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// object type
        /// </summary>
        [JsonIgnore]
        public string dalian_tax_date { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// int type
        /// </summary>
        [JsonIgnore]
        public string stopanstvo_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        ///  int type
        /// </summary>
        [JsonIgnore]
        public string sto_doc_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// int type
        /// </summary>
        [JsonIgnore]
        public string zveno_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// </summary>
        [JsonIgnore]
        public string ctrl_code { get; set; }

        /// <summary>
        /// FVMS - Not in use 
        /// object type
        /// </summary>
        [JsonIgnore]
        public string revoke_date { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// object type
        /// </summary>
        [JsonIgnore]
        public string revoke_reason { get; set; }


        /// <summary>
        /// Initializing in FVMS
        /// </summary>
        [JsonIgnore]
        public bool hitted { get; set; }

        /// <summary>
        /// Initializing in FVMS
        /// </summary>
        [JsonIgnore]
        public bool isDeleted { get; set; }
    }
}
