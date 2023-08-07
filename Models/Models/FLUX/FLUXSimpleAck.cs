using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ScortelApi.Models.FLUX
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:xeu:flux-transport:wsdl:v1")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:xeu:flux-transport:wsdl:v1", IsNullable = false, ElementName = "ACK")]   
    public partial class FLUXSimpleAck
    {
        private byte rsField;

        private string reField;

        private string ctField;

        private string frField;

        public long ID { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte RS
        {
            get
            {
                return this.rsField;
            }
            set
            {
                this.rsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RE
        {
            get
            {
                return this.reField;
            }
            set
            {
                this.reField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CT
        {
            get
            {
                return this.ctField;
            }
            set
            {
                this.ctField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FR
        {
            get
            {
                return this.frField;
            }
            set
            {
                this.frField = value;
            }
        }

        public string ON { get; set; }
    }
}
