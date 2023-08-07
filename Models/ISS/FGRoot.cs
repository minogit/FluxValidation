using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISS
{
    /// <summary>
    /// Json parse class for vessel's list of fgears
    /// </summary>
    public class FGRoot
    {
        public List<ApiFGear> ApiFGearList { get; set; }
    }
}

 
