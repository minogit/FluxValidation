using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcUserDriver
    {
        public int Userid { get; set; }
        public int Driverid { get; set; }
        public int Id { get; set; }

        public TcDrivers Driver { get; set; }
        public TcUsers User { get; set; }
    }
}
