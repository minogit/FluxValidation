using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcUserAttribute
    {
        public int Userid { get; set; }
        public int Attributeid { get; set; }
        public int Id { get; set; }

        public TcAttributes Attribute { get; set; }
        public TcUsers User { get; set; }
    }
}
