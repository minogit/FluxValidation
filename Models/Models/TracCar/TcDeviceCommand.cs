using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcDeviceCommand
    {
        public int Deviceid { get; set; }
        public int Commandid { get; set; }
        public int Id { get; set; }

        public TcCommands Command { get; set; }
        public TcDevices Device { get; set; }
    }
}
