using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ScortelApi.ISS
{

    /// <summary>
    /// 
    /// </summary>
    public class Rootobject
    {
        /// <summary>
        /// 
        /// </summary>
        public int page_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string get_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int anulirana { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ured_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ured_descr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int capitan_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string capitan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string razreshitelno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extmark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ship_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ship_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float ship_power { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float ship_length { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string start_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int start_port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object end_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object end_port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object unload_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object unload_port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int has_partship { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object partship_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object partship_extmark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int has_trans { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object trans_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object trans_ship { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object trans_port_target { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object trans_pozivna { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object trans_nacionalnost { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fishs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int tz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ribar_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rsr_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int udo_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int dekl_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object del_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ctrl_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trip_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Page_Ulov[] page_ulov { get; set; }
    }
 
    /// <summary>
    /// 
    /// </summary>
    public class PageRspRoot
    {
        public int status { get; set; }
        public string description { get; set; }
        public List<PageCreationResp> page { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PageCreationResp
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("iss_id")]
        public int id { get; set; }
        //public int page_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long page_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string get_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int anulirana { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ured_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ured_descr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int capitan_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string capitan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string razreshitelno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extmark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ship_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ship_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float ship_power { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float ship_length { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string start_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int start_port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public object end_date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string end_date { get; set; }
        //public object end_port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? end_port { get; set; }
        //public object unload_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string unload_date { get; set; }
        //public object unload_port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string unload_port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int has_partship { get; set; }
        //public object partship_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string partship_name { get; set; }
        //public object partship_extmark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string partship_extmark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int has_trans { get; set; }
        //public object trans_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trans_date { get; set; }
        //public object trans_ship { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trans_ship { get; set; }
        //public object trans_port_target { get; set; }
        /// <summary>
        /// /
        /// </summary>
        public string trans_port_target { get; set; }
        //public object trans_pozivna { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trans_pozivna { get; set; }
        //public object trans_nacionalnost { get; set;}
        /// <summary>
        /// 
        /// </summary>
        public string trans_nacionalnost { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<int> fishs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int tz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ribar_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rsr_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int udo_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int dekl_id { get; set; }
        //public object del_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string del_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ctrl_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trip_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Page_Ulov> page_ulov { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public DateTime CreationDT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string StartPort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string EndPort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string FGCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    public class Robject
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long page_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string get_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int anulirana { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ured_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ured_descr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int capitan_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string capitan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string razreshitelno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extmark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ship_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ship_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float ship_power { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float ship_length { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string start_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int start_port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object end_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? end_port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object unload_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object unload_port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int has_partship { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object partship_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object partship_extmark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int has_trans { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object trans_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object trans_ship { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object trans_port_target { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object trans_pozivna { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object trans_nacionalnost { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int[] fishs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int tz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ribar_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rsr_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int udo_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int dekl_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object del_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ctrl_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trip_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Page_Ulov[] page_ulov { get; set; }
    }

 

}
