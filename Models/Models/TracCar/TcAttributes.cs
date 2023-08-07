using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcAttributes
    {
        public TcAttributes()
        {
            TcDeviceAttribute = new HashSet<TcDeviceAttribute>();
            TcUserAttribute = new HashSet<TcUserAttribute>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Attribute { get; set; }
        public string Expression { get; set; }

        public ICollection<TcDeviceAttribute> TcDeviceAttribute { get; set; }
        public ICollection<TcUserAttribute> TcUserAttribute { get; set; }
    }
}
