using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// List of ports
    /// ELBProtocol 2.0.0
    /// </summary>
    public class LPorts
    {
        /// <summary>
        /// SQL PK
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Country or area description
        /// </summary>
        //[Index("IX_LPorts_Unq", 1, IsUnique = true)]
        public string PortName { get; set; }

        public string PortNameBG { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[Index("IX_LPorts_Unq", 2, IsUnique = true)]
        public string Code { get; set; }

        public float Lat { get; set; }

        public float Lng { get; set; }

        public int Seq { get; set; }
        /// <summary>
        /// Default/ favorite home port
        /// </summary>
        public byte IsHome { get; set; }
        /// <summary>
        /// Default / favorite registration port
        /// </summary>
        public byte IsReg { get; set; }
    }
}
