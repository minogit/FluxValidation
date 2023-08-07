using ScortelApi.Models.Maitenance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public partial class Vessel : IComparable
    {
        #region Fields

        private int mid;
        private string mregnum;
        private long mvehicle_type_id;
        private long mvehicle_model_id;
        private long mclient_id;
        private long muser_id;
        private long mowner_id;

        // additional
        private long mmoindex;
        private string mtypecode;

        // additional
        /// <summary>
        /// селектиран ли е в програмата за прехвърляне на данни
        /// </summary>
        private bool mSelected;
        /// <summary>
        /// ид на ред от НФ БД, до който сме стигнали с прехвърлянето успешно
        /// </summary>
        private int mNFRowID;

        private DateTime mLastTime;
        /// <summary>
        /// Последно състояние на двигателя
        /// </summary>
        private int mEngState;
        /// <summary>
        /// Време на предходен запис. 
        /// Необходим за намиране състоянието на двигателя
        /// при обработка на закъсняли записи.
        /// </summary>
        private DateTime mPrevRecDT;

        private double mMoLastLat;
        private double mMoLastLong;

        private string mWialonId;

        
        #endregion

        #region Properties

        [Key]
        public long Id { get; set; }
 
        public string Regnum
        { get { return mregnum; } set { mregnum = value; } }

        public string RegnumEn { get; set; }

        public long vehicle_type_id
        { get { return mvehicle_type_id; } set { mvehicle_type_id = value; } }

        public long vehicle_model_id
        { get { return mvehicle_model_id; } set { mvehicle_model_id = value; } }

        public long client_id
        { get { return mclient_id; } set { mclient_id = value; } }

        public long user_id
        { get { return muser_id; } set { muser_id = value; } }

        public long owner_id
        { get { return mowner_id; } set { mowner_id = value; } }

        public long moindex
        { get { return mmoindex; } set { mmoindex = value; } }

        public string typecode
        { get { return mtypecode; } set { mtypecode = value; } }

        public bool Selected
        {
            get { return mSelected; }
            set { mSelected = value; }
        }

        public int NFRowID
        {
            get { return mNFRowID; }
            set { mNFRowID = value; }
        }

        public DateTime LastTime
        {
            get
            {
                return mLastTime;
            }
            set
            {
                mLastTime = value;
            }
        }

        public int EngState
        {
            get { return mEngState; }
            set { mEngState = value; }
        }

        public DateTime PrevRecDT
        {
            get { return mPrevRecDT; }
            set { mPrevRecDT = value; }
        }

        public double MoLastLat
        {
            get { return mMoLastLat; }
            set { mMoLastLat = value; }
        }

        public double MoLastLong
        {
            get { return mMoLastLong; }
            set { mMoLastLong = value; }
        }

        public string WialonId
        {
            get { return mWialonId; }
            set { mWialonId = value; }
        }

        
        #region additional

        public string AISClass { get; set; }
        public string CallSign { get; set; }
        public string CFR { get; set; }
        public string MMSI { get; set; }
        public string GrossT { get; set; }
        public string OMark { get; set; }
        public string OMarkEn { get; set; }
        public string Draft { get; set; }
        public string Size { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public string VOwner { get; set; }     
        public string VOwnerAddr { get; set; }
        public string VOwnerName { get; set; }
        public string CapPhone { get; set; }
        public string Contacts { get; set; }
        public string HomePort { get; set; }
        public string RegPort { get; set; }
        public string FisheryPermission { get; set; }
        public string RLicense { get; set; } 
        public string NsPosition { get; set; }

        //public BordSet BordSet { get; set; }
        #endregion

        public List<Permision> Permits { get; set; }

        #endregion

        public int CompareTo(object obj)
        {
            Vessel other = (Vessel)obj;
            if (this.Id < other.Id)
                return -1;
            else if (this.Id > other.Id)
                return 1;
            else
                return 0;
        }

        public static Comparison<Vessel> Compare_regnum = delegate (Vessel object1, Vessel object2)
        {
            return object1.Regnum.CompareTo(object2.Regnum);
        };

        public Vessel()
        {
            //mid = -1;
            mregnum = "";
            mvehicle_type_id = -1;
            mvehicle_model_id = -1;
            mclient_id = -1;
            muser_id = -1;
            mowner_id = -1;
            mmoindex = -1;
            mtypecode = "";
            mSelected = false;
            mNFRowID = -1;
            mWialonId = "";
       
        }

    }
}
