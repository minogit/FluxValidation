﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 
namespace ScortelApi.Models.FLUX {
    using System.Xml.Serialization;
  
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:FLUXFAReportMessage:3")]
    [System.Xml.Serialization.XmlRootAttribute("FLUXFAReportMessage", Namespace = "urn:un:unece:uncefact:data:standard:FLUXFAReportMessage:3", IsNullable = false)]
    public partial class FLUXFAReportMessageType
    {

        private FLUXReportDocumentType fLUXReportDocumentField;

        private FAReportDocumentType[] fAReportDocumentField;

        /// <remarks/>
        public FLUXReportDocumentType FLUXReportDocument
        {
            get
            {
                return this.fLUXReportDocumentField;
            }
            set
            {
                this.fLUXReportDocumentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FAReportDocument")]
        public FAReportDocumentType[] FAReportDocument
        {
            get
            {
                return this.fAReportDocumentField;
            }
            set
            {
                this.fAReportDocumentField = value;
            }
        }
    }

}
