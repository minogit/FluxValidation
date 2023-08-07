using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISS
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiVessel
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_restrict { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string flic_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long shipid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_done { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_last { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ctrl_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string event_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string event_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reg_nom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ext_mark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pozivna { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mmsi { get; set; }
        /// <summary>
        ///  object
        /// </summary>
        public string uvi { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ais { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cfr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string flot_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reg_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string owner_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string slic_nom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string slic_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string slic_publisher { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pub_tom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pub_page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pub_nom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string enter_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string build_year { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string build_place { get; set; }
        /// <summary>
        /// object
        /// </summary>        
        public string admin_nom { get; set; }
        /// <summary>
        /// object
        /// </summary>
        public string admin_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pomosht_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string segment_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string port_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string stay_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rajon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string has_rsr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string length { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tonnage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extra_tonnage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string oslo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string depth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string perpen_length { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string me_kw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string se_kw { get; set; }
        /// <summary>
        /// object
        /// </summary>
        public string me_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string me_marka { get; set; }  
        /// <summary>
        /// 
        /// </summary>
        public string me_fuel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string main_ured { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extra_ured { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ship_mat_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ship_segm_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string has_vms { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string per_cap { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string crew_cap { get; set; }
        /// <summary>
        /// object
        /// </summary>
        public string talon_nom { get; set; }
        /// <summary>
        /// object
        /// </summary>
        public string talon_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string svid_nom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string svid_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string last_zav_date { get; set; }
        /// <summary>
        /// object
        /// </summary>
        public string exp_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string exp_country_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string exp_type_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string has_zh_lic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zh_nom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zh_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string has_calcan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string has_calcan_year { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string calcan_qty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zabelejki { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string des_reason_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string create_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime StoredDT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdatedDT { get; set; }
    }
}
