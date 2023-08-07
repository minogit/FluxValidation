using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISS
{  
    /// <summary>
    /// 
    /// </summary>
    public class ISSError
    {
        /// <summary>
        /// 
        /// </summary>
        public int error { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Description description { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Description
    {
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string _class { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }

}
