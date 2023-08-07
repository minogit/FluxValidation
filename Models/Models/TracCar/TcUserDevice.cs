using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcUserDevice
    {
        public int Userid { get; set; }
        public int Deviceid { get; set; }
        public int Id { get; set; }

        public TcDevices Device { get; set; }
        public TcUsers User { get; set; }
    }
}
