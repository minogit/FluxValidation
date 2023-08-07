using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcDrivers
    {
        public TcDrivers()
        {
            TcDeviceDriver = new HashSet<TcDeviceDriver>();
            TcGroupDriver = new HashSet<TcGroupDriver>();
            TcUserDriver = new HashSet<TcUserDriver>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Uniqueid { get; set; }
        public string Attributes { get; set; }

        public ICollection<TcDeviceDriver> TcDeviceDriver { get; set; }
        public ICollection<TcGroupDriver> TcGroupDriver { get; set; }
        public ICollection<TcUserDriver> TcUserDriver { get; set; }
    }
}
