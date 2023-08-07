using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcMaintenances
    {
        public TcMaintenances()
        {
            TcGroupMaintenance = new HashSet<TcGroupMaintenance>();
            TcUserMaintenance = new HashSet<TcUserMaintenance>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Start { get; set; }
        public double Period { get; set; }
        public string Attributes { get; set; }

        public ICollection<TcGroupMaintenance> TcGroupMaintenance { get; set; }
        public ICollection<TcUserMaintenance> TcUserMaintenance { get; set; }
    }
}
