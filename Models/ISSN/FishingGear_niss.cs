using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScortelApi.ISSN
{
    /// <summary>
    /// ISSM Fishing gear model
    /// </summary>
    public class FishingGear_niss
    {
        /// <summary>
        /// DB Id
        /// </summary>
        [JsonIgnore]
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Vessel CFR
        /// </summary>
        [JsonPropertyName("VCFR")]
        public string CFR { get; set; }

        /// <summary>
        /// ISS FGear quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public int Quantity { get; set; }

        /// <summary>
        /// ISS Fgear note
        /// </summary>
        [JsonPropertyName("zabelejki")]
        public string Notes { get; set; }

        /// <summary>
        /// ISS Fgear eye size
        /// </summary>
        [JsonPropertyName("oko")]
        public double Eye { get; set; }

        /// <summary>
        /// ISS Fgear mark
        /// </summary>
        [JsonPropertyName("marki")]
        public string Marks { get; set; }

        /// <summary>
        /// ISS Fgear description code - OTM
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// ISS Fgear description - Пелагични тралове
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// FLUX gear characteristic: HE - Height
        /// </summary>
        [JsonPropertyName("height")]
        public decimal? Height { get; set; }

        /// <summary>
        /// FLUX gear characteristic: GM - Gear dimension by length or width of the gear in meters
        /// (length of beams, trawl - permimeter of opening, seine nets - overall length,
        /// purse seine - length, puse seine - one boar operated - length, width of dredges, gill nets - length)
        /// </summary>
        [JsonPropertyName("lengthOrWidth")]
        public decimal? LengthOrWidth { get; set; }

        /// <summary>
        /// FLUX gear characteristic: MT - Model of trawl
        /// (eg. side: OTB-1, OTM-1; stern: OTB-2, OTM-2)
        /// </summary>
        [JsonPropertyName("trawlModel")]
        public string TrawlModel { get; set; }

        /// <summary>
        /// FLUX gear characteristic: NN - Nuber of nets in the fleet
        /// </summary>
        [JsonPropertyName("netsCountInFleet")]
        public int? NetsCountInFleet { get; set; }

        /// <summary>
        /// FLUX gear characteristic: NI - Number of lines
        /// </summary>
        [JsonPropertyName("linesCount")]
        public int? LinesCount { get; set; }

        /// <summary>
        /// FLUX gear characteristic: NL - Nominal length of one net in a fleet
        /// </summary>
        [JsonPropertyName("netNominalLength")]
        public decimal NetNominalLength { get; set; }

        /// <summary>
        /// FLUX gear characteristic: GN - Gear dimension by number
        /// (number of trawls, beams, dredges, pots, hooks)
        /// </summary>
        [JsonPropertyName("numericCount")]
        public int? NumericCount { get; set; }


        // ***************************************************
        ///// <summary>
        ///// Init in FVMS
        ///// </summary>       
        //public long Id { get; set; }
        /// <summary>
        /// Init in FVMS
        /// </summary>
        [JsonIgnore]
        public DateTime StoredDT { get; set; }
        /// <summary>
        /// Init in FVMS
        /// </summary>
        [JsonIgnore]
        public DateTime UpdatedDT { get; set; }


        /// <summary>
        /// FVMS - not in use
        /// </summary>
        [JsonIgnore]
        public string rsr { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS fgear id???
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS Certificate id
        /// </summary>
        [JsonIgnore]
        public int udo_id { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS FGear id
        /// </summary>
        [JsonIgnore]
        public int ured_id { get; set; }

        /// <summary>
        /// Init in FVMS
        /// Additinal field for parsing fgear mark field
        /// </summary>
        [JsonIgnore]
        public List<string> MarkiList { get; set; }

        /// <summary>
        /// Init in FVMS
        /// New ISS integration field        
        /// </summary>
        [JsonIgnore]
        public string create_date { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS Certificate number
        /// </summary>
        [JsonIgnore]
        public string nomer { get; set; }

        /// <summary>
        /// FVMS - not in use
        /// ISS Certificate valid to - datetime
        /// </summary>
        [JsonIgnore]
        public DateTime valid_to { get; set; }

        /// <summary>
        /// Init in FVMS
        /// </summary>
        [JsonIgnore]
        public bool hitted { get; set; }
        /// <summary>
        /// Init in FVMS
        /// </summary>
        [JsonIgnore]
        public bool isDeleted { get; set; }

        /// <summary>
        /// Init in FVMS
        /// </summary>
        [JsonIgnore]
        public virtual Certificate_niss Certificate_niss { get; set; }
    }
 }
