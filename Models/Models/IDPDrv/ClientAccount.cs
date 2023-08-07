using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.IDPDrv
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientAccount : IComparable
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accName { get; set; }
        //ICollection<string> userAccs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long dwb_seq_number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accResId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accDwbResId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accDwbResName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userName { get; set; }

        //public string accusername { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accpass { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string token { get; set; }

        //public long dwbb_sn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caa"></param>
        /// <returns></returns>
        public int CompareTo(object caa)
        {
            ClientAccount cla = (ClientAccount)caa;

            if (this.id > cla.id)
            {
                return 1;
            }
            if (this.id < cla.id)
            {
                return -1;
            }
            else
                return 0;
        }
    }
}
