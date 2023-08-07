using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ISS
{
    public interface IPageGen
    {
        long Id { get; set; }

        DateTime Timestamp { get; set; }
        UInt32 CPage { get; set; }

        UInt32 GetNPage();

        string ELBookNum { get; set; }

    }
}
