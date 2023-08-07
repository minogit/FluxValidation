using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcGroups
    {
    
        public TcGroups()
        {
            InverseGroup = new HashSet<TcGroups>();
            TcDevices = new HashSet<TcDevices>();
            TcGroupCommand = new HashSet<TcGroupCommand>();
            TcGroupDriver = new HashSet<TcGroupDriver>();
            TcGroupGeofence = new HashSet<TcGroupGeofence>();
            TcGroupMaintenance = new HashSet<TcGroupMaintenance>();
            TcGroupNotification = new HashSet<TcGroupNotification>();
            TcUserGroup = new HashSet<TcUserGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Groupid { get; set; }
        public string Attributes { get; set; }

        public TcGroups Group { get; set; }
        public ICollection<TcGroups> InverseGroup { get; set; }
        public ICollection<TcDevices> TcDevices { get; set; }
        public ICollection<TcGroupCommand> TcGroupCommand { get; set; }
        public ICollection<TcGroupDriver> TcGroupDriver { get; set; }
        public ICollection<TcGroupGeofence> TcGroupGeofence { get; set; }
        public ICollection<TcGroupMaintenance> TcGroupMaintenance { get; set; }
        public ICollection<TcGroupNotification> TcGroupNotification { get; set; }
        public ICollection<TcUserGroup> TcUserGroup { get; set; }
    }
}
