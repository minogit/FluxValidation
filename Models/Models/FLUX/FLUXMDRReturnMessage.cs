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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:FLUXMDRReturnMessage:5")]
    [System.Xml.Serialization.XmlRootAttribute("FLUXMDRReturnMessage", Namespace = "urn:un:unece:uncefact:data:standard:FLUXMDRReturnMessage:5", IsNullable = false)]
    public partial class FLUXMDRReturnMessageType
    {

        private FLUXResponseDocumentType fLUXResponseDocumentField;

        private MDRDataSetType mDRDataSetField;

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
        public MDRDataSetType MDRDataSet
        {
            get
            {
                return this.mDRDataSetField;
            }
            set
            {
                this.mDRDataSetField = value;
            }
        }
    }

}
