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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:FLUXFLAPResponseMessage:7")]
    [System.Xml.Serialization.XmlRootAttribute("FLUXFLAPResponseMessage", Namespace = "urn:un:unece:uncefact:data:standard:FLUXFLAPResponseMessage:7", IsNullable = false)]
    public partial class FLUXFLAPResponseMessageType
    {

        private FLUXResponseDocumentType fLUXResponseDocumentField;

        private FLAPRequestDocumentType fLAPRequestDocumentField;

        private FLAPDocumentType fLAPDocumentField;

        /// <remarks/>
        public FLUXResponseDocumentType FLUXResponseDocument
        {
            get
            {
                return this.fLUXResponseDocumentField;
            }
            set
            {
                this.fLUXResponseDocumentField = value;
            }
        }

        /// <remarks/>
        public FLAPRequestDocumentType FLAPRequestDocument
        {
            get
            {
                return this.fLAPRequestDocumentField;
            }
            set
            {
                this.fLAPRequestDocumentField = value;
            }
        }

        /// <remarks/>
        public FLAPDocumentType FLAPDocument
        {
            get
            {
                return this.fLAPDocumentField;
            }
            set
            {
                this.fLAPDocumentField = value;
            }
        }
    }

}
