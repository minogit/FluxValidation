using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ISS
{
    public class PageGen :IPageGen
    {
        public long Id { get; set; }

        public DateTime Timestamp { get; set; }
        public UInt32 CPage { get; set; }

        public string ELBookNum { get; set; }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <returns></returns>
        public UInt32 GetNPage()
        {
            return 0;
        }

        //public   UInt32 GetNPage()
        //{
        //    //using (var context = new ApplicationDbContext())
        //    //{
        //    //    try
        //    //    {
        //    //        byte tmpseq = 1;
        //    //        var cslist = context.PackSeqs.OrderByDescending(x => x.CSeq).Take(1).ToList();
        //    //        if (cslist != null && cslist.Count > 0)
        //    //        {
        //    //            var ccseq = (byte)(cslist[0].CSeq + 1);
        //    //            if (ccseq < 255)
        //    //            {
        //    //                //ELBPortArrive.Seq = ccseq;
        //    //                tmpseq = ccseq;
        //    //            }
        //    //            else
        //    //            {
        //    //                //ELBPortArrive.Seq = 1;
        //    //                tmpseq = 1;
        //    //            }
        //    //            cslist[0].CSeq = tmpseq;
        //    //            cslist[0].Timestamp = DateTime.Now;
        //    //            context.Entry(cslist[0]).State = EntityState.Modified;
        //    //            var rescs = context.SaveChanges();
        //    //            //if (rescs > 0)
        //    //            //{
        //    //            //    return tmpseq;
        //    //            //}
        //    //            return tmpseq;
        //    //        }
        //    //        else
        //    //        {
        //    //            //ELBPortArrive.Seq = 1;
        //    //            tmpseq = 1;
        //    //            PackSeq pseq = new PackSeq();
        //    //            pseq.CSeq = 1;
        //    //            pseq.Timestamp = DateTime.Now;
        //    //            context.PackSeqs.Add(pseq);
        //    //            var cnseq = context.SaveChanges();
        //    //            //if (cnseq > 0)
        //    //            //{
        //    //            //    return tmpseq;
        //    //            //}
        //    //            return tmpseq;
        //    //        }
        //    //    }
        //    //    catch (Exception)
        //    //    {
        //    //        // have to write to DB ???
        //    //        return 1;
        //    //    }
        //    //}
        //}
    }
}
