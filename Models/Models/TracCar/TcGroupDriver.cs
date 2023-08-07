using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcGroupDriver
    {
        public int Groupid { get; set; }
        public int Driverid { get; set; }
        public int Id { get; set; }

        public TcDrivers Driver { get; set; }
        public TcGroups Group { get; set; }
    }
}
