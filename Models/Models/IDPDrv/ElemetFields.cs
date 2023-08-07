using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.IDPDrv
{
    /// <summary>
    /// https://blog.oneunicorn.com/2017/09/25/many-to-many-relationships-in-ef-core-2-0-part-1-the-basics/
    /// </summary>
    public class ElemetFields
    {
        /// <summary>
        /// 
        /// </summary>
        public int ElementId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public   Element Element { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int FieldId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public   Field Field{ get; set; }
    }
}
