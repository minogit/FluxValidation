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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:un:unece:uncefact:data:standard:FLUXFLAPQueryMessage:1")]
    [System.Xml.Serialization.XmlRootAttribute("FLUXFLAPQueryMessage", Namespace="urn:un:unece:uncefact:data:standard:FLUXFLAPQueryMessage:1", IsNullable=false)]
    public partial class FLUXFLAPQueryMessageType {
        
        private FLAPQueryType fLAPQueryField;
        
        /// <remarks/>
        public FLAPQueryType FLAPQuery {
            get {
                return this.fLAPQueryField;
            }
            set {
                this.fLAPQueryField = value;
            }
        }
    }
     
}
