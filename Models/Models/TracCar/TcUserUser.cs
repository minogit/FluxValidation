using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcUserUser
    {
        public int Userid { get; set; }
        public int Manageduserid { get; set; }
        public int Id { get; set; }

        public TcUsers Manageduser { get; set; }
        public TcUsers User { get; set; }
    }
}
