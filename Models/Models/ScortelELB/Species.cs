using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// Aquatic organisms / species
    /// ELBProt 2.0.0
    /// </summary>
    public class Species
    {
        /// <summary>
        /// SQLite PK
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Bulgarina name of given species
        /// </summary>
        //[Index("IX_Species_Unq", 1, IsUnique = true)]
        public string NameBG { get; set; }
        /// <summary>
        /// English name of given species
        /// </summary>
        //[Index("IX_Species_Unq", 2, IsUnique = true)]
        public string NameEN { get; set; }
        /// <summary>
        /// Latin name of given species
        /// </summary>
        //[Index("IX_Species_Unq", 3, IsUnique = true)]
        public string NameLatin { get; set; }
        /// <summary>
        /// FOA code - 3 symbols
        /// </summary>
        //[Index("IX_Species_Unq", 4, IsUnique = true)]
        public string FAOCode3 { get; set; }
        /// <summary>
        /// Type of species
        /// 1 - freshwater fish
        /// 2 - progressive fish ???? Проходни риби
        /// 3 - sea fish
        /// 4 - other aquatic organisms
        /// </summary>
        public byte Type { get; set; }


        public virtual ICollection<ConversionFactor> ConversionFactors { get; set; }
    }
}
