using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ScortelApi.Models.FLUX
{
    /// <summary>
    /// 
    /// </summary>
    public class FluxLocalResp
    {

        public FluxLocalResp()
        {
            Hdrs = new Dictionary<string, string>();
        }

        /// <summary>
        /// PK, ignored in XML Serialization
        /// </summary>
        [XmlIgnoreAttribute]
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public POSTMSGOUT PMO { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public WebHeaderCollection Headers { get; set; }

        /// <summary>
        /// No direct mapping for DB
        /// </summary>
        [NotMapped]
        public Dictionary<string, string> Hdrs { get; set; }

        /// <summary>
        /// Stored field to DB
        /// </summary>
        public string DictionaryAsJSON
        {
            get
            {
                return JsonConvert.SerializeObject(Hdrs);
            }
            set
            {
                Hdrs = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
            }
        }
    }
}
