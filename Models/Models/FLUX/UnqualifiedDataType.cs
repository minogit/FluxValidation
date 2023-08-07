using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.FLUX
{
    #region ACDR

    #endregion

    #region FLUX FA Query

    /// <remarks from FLAP/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:20")]
    public partial class IDType
    {

        private string schemeIDField;

        private string schemeNameField;

        private string schemeAgencyIDField;

        private string schemeAgencyNameField;

        private string schemeVersionIDField;

        private string schemeDataURIField;

        private string schemeURIField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string schemeID
        {
            get
            {
                return this.schemeIDField;
            }
            set
            {
                this.schemeIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string schemeName
        {
            get
            {
                return this.schemeNameField;
            }
            set
            {
                this.schemeNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string schemeAgencyID
        {
            get
            {
                return this.schemeAgencyIDField;
            }
            set
            {
                this.schemeAgencyIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string schemeAgencyName
        {
            get
            {
                return this.schemeAgencyNameField;
            }
            set
            {
                this.schemeAgencyNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string schemeVersionID
        {
            get
            {
                return this.schemeVersionIDField;
            }
            set
            {
                this.schemeVersionIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string schemeDataURI
        {
            get
            {
                return this.schemeDataURIField;
            }
            set
            {
                this.schemeDataURIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string schemeURI
        {
            get
            {
                return this.schemeURIField;
            }
            set
            {
                this.schemeURIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute(DataType = "token")]
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
    }

    /// <remarks from FLAP/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:20")]
    public partial class CodeType
    {

        private string listIDField;

        private string listAgencyIDField;

        private string listAgencyNameField;

        private string listNameField;

        private string listVersionIDField;

        private string nameField;

        private string languageIDField;

        private string listURIField;

        private string listSchemeURIField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string listID
        {
            get
            {
                return this.listIDField;
            }
            set
            {
                this.listIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string listAgencyID
        {
            get
            {
                return this.listAgencyIDField;
            }
            set
            {
                this.listAgencyIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string listAgencyName
        {
            get
            {
                return this.listAgencyNameField;
            }
            set
            {
                this.listAgencyNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string listName
        {
            get
            {
                return this.listNameField;
            }
            set
            {
                this.listNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string listVersionID
        {
            get
            {
                return this.listVersionIDField;
            }
            set
            {
                this.listVersionIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
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
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string languageID
        {
            get
            {
                return this.languageIDField;
            }
            set
            {
                this.languageIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string listURI
        {
            get
            {
                return this.listURIField;
            }
            set
            {
                this.listURIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string listSchemeURI
        {
            get
            {
                return this.listSchemeURIField;
            }
            set
            {
                this.listSchemeURIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute(DataType = "token")]
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
    }

    /// <remarks from FLAP/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:20")]
    public partial class DateTimeType
    {

        //private object itemField;

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("DateTime", typeof(System.DateTime))]
        //[System.Xml.Serialization.XmlElementAttribute("DateTimeString", typeof(DateTimeTypeDateTimeString))]
        //public object Item
        //{
        //    get
        //    {
        //        return this.itemField;
        //    }
        //    set
        //    {
        //        this.itemField = value;
        //    }
        //}

        private DateTime itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DateTime", typeof(System.DateTime))]
        //[System.Xml.Serialization.XmlElementAttribute("DateTimeString", typeof(DateTimeTypeDateTimeString))]
        public DateTime Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks from FLAP/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:20")]
    public partial class DateTimeTypeDateTimeString
    {

        private string formatField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string format
        {
            get
            {
           
                return this.formatField;
            }
            set
            {
                this.formatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                //int posP = this.valueField.IndexOf(".");
                //int posZ = this.valueField.IndexOf("Z");
                //string res = "";
                //if (posP > 0 && posZ > 0)
                //{
                //    res = this.valueField.Remove(posP, 3);
                //}
                //return res;
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks from FLAP/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:20")]
    public partial class TextType
    {

        private string languageIDField;

        private string languageLocaleIDField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string languageID
        {
            get
            {
                return this.languageIDField;
            }
            set
            {
                this.languageIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string languageLocaleID
        {
            get
            {
                return this.languageLocaleIDField;
            }
            set
            {
                this.languageLocaleIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
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
    }

    /// <remarks from FLAP/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:20")]
    public partial class MeasureType
    {

        private string unitCodeField;

        private string unitCodeListVersionIDField;

        private decimal valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string unitCode
        {
            get
            {
                return this.unitCodeField;
            }
            set
            {
                this.unitCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string unitCodeListVersionID
        {
            get
            {
                return this.unitCodeListVersionIDField;
            }
            set
            {
                this.unitCodeListVersionIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public decimal Value
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
    }


    #endregion

    #region FLUX FA Response Message

    ///////// <remarks from FLAP/>
    //////[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    //////[System.SerializableAttribute()]
    //////[System.Diagnostics.DebuggerStepThroughAttribute()]
    //////[System.ComponentModel.DesignerCategoryAttribute("code")]
    //////[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:20")]
    //////public partial class IndicatorType
    //////{

    //////    private object itemField;

    //////    /// <remarks/>
    //////    [System.Xml.Serialization.XmlElementAttribute("Indicator", typeof(bool))]
    //////    [System.Xml.Serialization.XmlElementAttribute("IndicatorString", typeof(IndicatorTypeIndicatorString))]
    //////    public object Item
    //////    {
    //////        get
    //////        {
    //////            return this.itemField;
    //////        }
    //////        set
    //////        {
    //////            this.itemField = value;
    //////        }
    //////    }
    //////}
    ///

    /// <remarks from FLAP/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:20")]
    public partial class IndicatorType
    {

        private string itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }




    /// <remarks from FLAP/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:20")]
    public partial class IndicatorTypeIndicatorString
    {

        private string formatField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string format
        {
            get
            {
                return this.formatField;
            }
            set
            {
                this.formatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
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
    }

    /// <remarks from FLAP/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:20")]
    public partial class QuantityType
    {

        private string unitCodeField;

        private string unitCodeListIDField;

        private string unitCodeListAgencyIDField;

        private string unitCodeListAgencyNameField;

        private decimal valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string unitCode
        {
            get
            {
                return this.unitCodeField;
            }
            set
            {
                this.unitCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string unitCodeListID
        {
            get
            {
                return this.unitCodeListIDField;
            }
            set
            {
                this.unitCodeListIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string unitCodeListAgencyID
        {
            get
            {
                return this.unitCodeListAgencyIDField;
            }
            set
            {
                this.unitCodeListAgencyIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string unitCodeListAgencyName
        {
            get
            {
                return this.unitCodeListAgencyNameField;
            }
            set
            {
                this.unitCodeListAgencyNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public decimal Value
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:20")]
    public partial class BinaryObjectType
    {

        private string formatField;

        private string mimeCodeField;

        private string encodingCodeField;

        private string characterSetCodeField;

        private string uriField;

        private string filenameField;

        private byte[] valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string format
        {
            get
            {
                return this.formatField;
            }
            set
            {
                this.formatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string mimeCode
        {
            get
            {
                return this.mimeCodeField;
            }
            set
            {
                this.mimeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string encodingCode
        {
            get
            {
                return this.encodingCodeField;
            }
            set
            {
                this.encodingCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string characterSetCode
        {
            get
            {
                return this.characterSetCodeField;
            }
            set
            {
                this.characterSetCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string uri
        {
            get
            {
                return this.uriField;
            }
            set
            {
                this.uriField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string filename
        {
            get
            {
                return this.filenameField;
            }
            set
            {
                this.filenameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute(DataType = "base64Binary")]
        public byte[] Value
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
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:20")]
    public partial class NumericType
    {

        private string formatField;

        private decimal valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string format
        {
            get
            {
                return this.formatField;
            }
            set
            {
                this.formatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public decimal Value
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:UnqualifiedDataType:20")]
    public partial class AmountType
    {

        private string currencyIDField;

        private string currencyCodeListVersionIDField;

        private decimal valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string currencyID
        {
            get
            {
                return this.currencyIDField;
            }
            set
            {
                this.currencyIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token")]
        public string currencyCodeListVersionID
        {
            get
            {
                return this.currencyCodeListVersionIDField;
            }
            set
            {
                this.currencyCodeListVersionIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public decimal Value
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
    }

    #endregion
}
