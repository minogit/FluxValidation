namespace ScortelApi.Models.FLUX
{
    public class TMPVMS
    {
    }


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:xeu:flux-transport:v1")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:xeu:flux-transport:v1", IsNullable = false)]
    public partial class ENV
    {

        private ENVMSG mSGField;

        private System.DateTime dtField;

        private bool tsField;

        /// <remarks/>
        public ENVMSG MSG
        {
            get
            {
                return this.mSGField;
            }
            set
            {
                this.mSGField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime DT
        {
            get
            {
                return this.dtField;
            }
            set
            {
                this.dtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool TS
        {
            get
            {
                return this.tsField;
            }
            set
            {
                this.tsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:xeu:flux-transport:v1")]
    public partial class ENVMSG
    {

        private FLUXVesselPositionMessageType fLUXVesselPositionMessageField;

        private string frField;

        private string adField;

        private string dfField;

        private string onField;

        private bool arField;

        private System.DateTime tODTField;

        private byte toField;

        private string vbField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:un:unece:uncefact:data:standard:FLUXVesselPositionMessage:4")]
        public FLUXVesselPositionMessageType FLUXVesselPositionMessage
        {
            get
            {
                return this.fLUXVesselPositionMessageField;
            }
            set
            {
                this.fLUXVesselPositionMessageField = value;
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AD
        {
            get
            {
                return this.adField;
            }
            set
            {
                this.adField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DF
        {
            get
            {
                return this.dfField;
            }
            set
            {
                this.dfField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ON
        {
            get
            {
                return this.onField;
            }
            set
            {
                this.onField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool AR
        {
            get
            {
                return this.arField;
            }
            set
            {
                this.arField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime TODT
        {
            get
            {
                return this.tODTField;
            }
            set
            {
                this.tODTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte TO
        {
            get
            {
                return this.toField;
            }
            set
            {
                this.toField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string VB
        {
            get
            {
                return this.vbField;
            }
            set
            {
                this.vbField = value;
            }
        }
    }


}
