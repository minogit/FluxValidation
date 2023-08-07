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
    /// ISS Fgear model / brought from ISS API/
    /// </summary>
    public class ApiFGear
    {
        private string _marki;
        /// <summary>
        /// Init in FVMS
        /// </summary>        
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Init in FVMS
        /// </summary>
        public DateTime StoredDT { get; set; }
        /// <summary>
        /// Init in FVMS
        /// </summary>
        public DateTime UpdatedDT { get; set; }
        /// <summary>
        /// Vessel CFR
        /// </summary>
        public string VCFR { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// </summary>
        public string rsr { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS fgear id???
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public int id { get; set; }
   
        /// <summary>
        /// FVMS - not in use
        /// ISS Certificate id
        /// </summary>
        public int udo_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS FGear id
        /// </summary>
        public int ured_id { get; set; }

        /// <summary>       
        /// ISS FGear quantity
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// ISS Fgear note
        /// </summary>
        public string zabelejki { get; set; }

        /// <summary>
        /// ISS Fgear eye size
        /// </summary>
    
        public double oko { get; set; }

        /// <summary>
        /// ISS Fgear mark
        /// </summary>
        public string marki {
            get {
                return this._marki;
            }
            set {
                this._marki = value;
                if (value != "")
                {
                    if (this.MarkiList == null)                     
                    {
                        this.MarkiList = new();                        
                    }
                    if (this._marki == null)
                    {
                        this.MarkiList = new();
                    }
                    else
                    {
                        if (this._marki.IndexOf(",") > 0)
                        {
                            this.MarkiList = this._marki.Split(",").ToList();
                        }
                        else
                        {
                            this.MarkiList.Add(this._marki);
                        }
                    }
                }
            } 
        }
        /// <summary>
        /// Init in FVMS
        /// Additinal field for parsing fgear mark field
        /// </summary>
        public List<string> MarkiList { get; set; }

        /// <summary>
        /// Init in FVMS
        /// New ISS integration field        
        /// </summary>
        [NotMapped]
        public string create_date { get; set; }

        /// <summary>
        /// ISS Fgear description code - OTM
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// ISS Fgear description - Пелагични тралове
        /// </summary>
        public string name { get; set; }


        /// <summary>
        /// FVMS - not in use
        /// ISS Certificate number
        /// </summary>
        public string nomer { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS Certificate valid to - datetime
        /// </summary>
        public DateTime valid_to { get; set; }
      
         /// <summary>
         /// Init in FVMS
         /// </summary>
        [NotMapped]
        public bool hitted { get; set; }
        /// <summary>
        /// Init in FVMS
        /// </summary>
        public bool isDeleted { get; set; }
        /// <summary>
        /// Init in FVMS
        /// </summary>
        public ApiCerts ApiCerts { get; set; }
    }
}
