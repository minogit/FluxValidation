using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcGroupMaintenance
    {
        public int Groupid { get; set; }
        public int Maintenanceid { get; set; }
        public int Id { get; set; }

        public TcGroups Group { get; set; }
        public TcMaintenances Maintenance { get; set; }
    }
}
