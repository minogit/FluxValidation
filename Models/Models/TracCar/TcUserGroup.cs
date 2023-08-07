using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcUserGroup
    {
        public int Userid { get; set; }
        public int Groupid { get; set; }
        public int Id { get; set; }

        public TcGroups Group { get; set; }
        public TcUsers User { get; set; }
    }
}
