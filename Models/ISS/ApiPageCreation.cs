using Newtonsoft.Json;
using ScortelApi.Models.ISS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISS
{
    /// <summary>
    /// ISSO - FVMS->ISSo Api Page Creation
    /// </summary>
    [Serializable]
    public class ApiPageCreation
    {
        /// <summary>
        /// DB id
        /// </summary>
        [JsonIgnore]
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Creation id = -1
        /// Mandatory
        /// </summary>
        [Column("iss_id")]
        public int id { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        //public int page_num { get; set; }
        public long page_num { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string get_date { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string razreshitelno { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string start_date { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public int start_port { get; set; }
        /// <summary>
        /// @@@@@@@@@@@@@@@@@@@@
        /// </summary>
        public int udo_id { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public int ured_id { get; set; }
        /// <summary>
        /// @@@@@@@@@@@@@@@@@@@@
        /// </summary>
        public int dekl_id { get; set; }
        public List<int> fishs { get; set; }

        [NotMapped]
        public List<Page_Ulov> page_ulov { get; set; }

        public DateTime CreationDT { get; set; }

        // public ELBook ELBook { get; set; } 
        [JsonIgnore]
        public int ELBook_Id { get; set; }
        [JsonIgnore]
        public List<int> foids { get; set; }


        public ApiPageCreation()
        {
            id = -1;
            fishs = new List<int>();
            //page_ulov = new List<Page_Ulov>();
        }

        public string ToStr()
        {
            try
            {
                string tmp = "";

                tmp = JsonConvert.SerializeObject(this);

                return tmp;
            }
            catch (Exception ex)
            {
                return " ex: " + ex.Message;
            }
        }
    }

    [Serializable]
    public class Page_Ulov
    {
        [JsonIgnore]
        [Key]
        public long Id { get; set; }
        [Column("iss_id")]
        public int id { get; set; }
        public string pravoagalnik { get; set; }
        public string zona { get; set; }
        public string data { get; set; }
        public string operacii { get; set; }
        public string dni { get; set; }
        public List<Fish> fishs { get; set; }
        

        public Page_Ulov()
        {
            fishs = new List<Fish>();
        }
    }

    [Serializable]
    public class Fish
    {
        [JsonIgnore]
        [Key]
        public long Id { get; set; }
        [Column("iss_id")]
        public int id { get; set; }
        public double qty { get; set; }
        public int status { get; set; }
        //[JsonIgnore]
        [NotMapped]
        public string Species { get; set; }
        //[JsonIgnore]
        [NotMapped]
        public string SpeciesCode { get; set; }
        //[JsonIgnore]
        [NotMapped]
        public string Present { get; set; }
    }
}
