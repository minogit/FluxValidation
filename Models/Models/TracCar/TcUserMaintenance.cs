using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcUserMaintenance
    {
        public int Userid { get; set; }
        public int Maintenanceid { get; set; }
        public int Id { get; set; }

        public TcMaintenances Maintenance { get; set; }
        public TcUsers User { get; set; }
    }
}
