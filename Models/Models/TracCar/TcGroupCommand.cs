using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcGroupCommand
    {
        public int Groupid { get; set; }
        public int Commandid { get; set; }
        public int Id { get; set; }

        public TcCommands Command { get; set; }
        public TcGroups Group { get; set; }
    }
}
