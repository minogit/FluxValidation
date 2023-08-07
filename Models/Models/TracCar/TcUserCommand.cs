using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcUserCommand
    {
        public int Userid { get; set; }
        public int Commandid { get; set; }
        public int Id { get; set; }

        public TcCommands Command { get; set; }
        public TcUsers User { get; set; }
    }
}
