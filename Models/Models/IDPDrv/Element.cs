using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.IDPDrv
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Element
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

        //private Field[] fieldsField;
        // private List<Field> fieldsField;
        private List<ElemetFields> fieldsField;

        private int indexField;
        /// <summary>
        /// 
        /// </summary>
        public Element()
        {
            this.indexField = -1;
        }

        /// <remarks/>


        /// <remarks/>
        public List<ElemetFields> Fields
        {
            get
            {
                return this.fieldsField;
            }
            set
            {
                this.fieldsField = value;
            }
        }


        ///// <remarks/>
        //public List<Field> Fields
        //{
        //    get
        //    {
        //        return this.fieldsField;
        //    }
        //    set
        //    {
        //        this.fieldsField = value;
        //    }
        //}

        ///// <remarks/>
        //public Field[] Fields
        //{
        //    get
        //    {
        //        return this.fieldsField;
        //    }
        //    set
        //    {
        //        this.fieldsField = value;
        //    }
        //}

         
        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(-1)]
        public int Index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }
    }
}
