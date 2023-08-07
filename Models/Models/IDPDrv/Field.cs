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
    public partial class Field
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

        //private Element[] elementsField;
        //private List<Element> elementsField;
        private List<ElemetFields> elementsField;

        private Message messageField;

        private string nameField;

        private string valueField;

        private string typeField;
        /// <summary>
        /// 
        /// </summary>
        public List<ElemetFields> Elements
        {
            get
            {
                return this.elementsField;
            }
            set
            {
                this.elementsField = value;
            }
        }

        ///// <remarks/>
        //public List<Element> Elements
        //{
        //    get
        //    {
        //        return this.elementsField;
        //    }
        //    set
        //    {
        //        this.elementsField = value;
        //    }
        //}

        ///// <remarks/>
        //public Element[] Elements
        //{
        //    get
        //    {
        //        return this.elementsField;
        //    }
        //    set
        //    {
        //        this.elementsField = value;
        //    }
        //}

        /// <remarks/>
        public Message Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }
}
