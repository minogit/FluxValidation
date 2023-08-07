using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.FLUX
{

    #region ACDR
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:18" +
        "")]
    public partial class FLUXACDRReportType
    {

        private CodeType regionalAreaCodeField;

        private CodeType regionalSpeciesCodeField;

        private CodeType fishingCategoryCodeField;

        private CodeType fAOFishingGearCodeField;

        private CodeType typeCodeField;

        private VesselTransportMeansType specifiedVesselTransportMeansField;

        private ACDRReportedAreaType[] specifiedACDRReportedAreaField;

        /// <remarks/>
        public CodeType RegionalAreaCode
        {
            get
            {
                return this.regionalAreaCodeField;
            }
            set
            {
                this.regionalAreaCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType RegionalSpeciesCode
        {
            get
            {
                return this.regionalSpeciesCodeField;
            }
            set
            {
                this.regionalSpeciesCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType FishingCategoryCode
        {
            get
            {
                return this.fishingCategoryCodeField;
            }
            set
            {
                this.fishingCategoryCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType FAOFishingGearCode
        {
            get
            {
                return this.fAOFishingGearCodeField;
            }
            set
            {
                this.fAOFishingGearCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public VesselTransportMeansType SpecifiedVesselTransportMeans
        {
            get
            {
                return this.specifiedVesselTransportMeansField;
            }
            set
            {
                this.specifiedVesselTransportMeansField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedACDRReportedArea")]
        public ACDRReportedAreaType[] SpecifiedACDRReportedArea
        {
            get
            {
                return this.specifiedACDRReportedAreaField;
            }
            set
            {
                this.specifiedACDRReportedAreaField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:18" +
        "")]
    public partial class AggregatedCatchReportDocumentType
    {

        private DelimitedPeriodType effectiveDelimitedPeriodField;

        private FLUXPartyType ownerFLUXPartyField;

        /// <remarks/>
        public DelimitedPeriodType EffectiveDelimitedPeriod
        {
            get
            {
                return this.effectiveDelimitedPeriodField;
            }
            set
            {
                this.effectiveDelimitedPeriodField = value;
            }
        }

        /// <remarks/>
        public FLUXPartyType OwnerFLUXParty
        {
            get
            {
                return this.ownerFLUXPartyField;
            }
            set
            {
                this.ownerFLUXPartyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:18" +
        "")]
    public partial class ACDRCatchType
    {

        private CodeType fAOSpeciesCodeField;

        private QuantityType unitQuantityField;

        private MeasureType weightMeasureField;

        private CodeType usageCodeField;

        /// <remarks/>
        public CodeType FAOSpeciesCode
        {
            get
            {
                return this.fAOSpeciesCodeField;
            }
            set
            {
                this.fAOSpeciesCodeField = value;
            }
        }

        /// <remarks/>
        public QuantityType UnitQuantity
        {
            get
            {
                return this.unitQuantityField;
            }
            set
            {
                this.unitQuantityField = value;
            }
        }

        /// <remarks/>
        public MeasureType WeightMeasure
        {
            get
            {
                return this.weightMeasureField;
            }
            set
            {
                this.weightMeasureField = value;
            }
        }

        /// <remarks/>
        public CodeType UsageCode
        {
            get
            {
                return this.usageCodeField;
            }
            set
            {
                this.usageCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:18" +
        "")]
    public partial class ACDRReportedAreaType
    {

        private CodeType fAOIdentificationCodeField;

        private CodeType sovereigntyWaterCodeField;

        private CodeType landingCountryCodeField;

        private CodeType catchStatusCodeField;

        private ACDRCatchType[] specifiedACDRCatchField;

        /// <remarks/>
        public CodeType FAOIdentificationCode
        {
            get
            {
                return this.fAOIdentificationCodeField;
            }
            set
            {
                this.fAOIdentificationCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType SovereigntyWaterCode
        {
            get
            {
                return this.sovereigntyWaterCodeField;
            }
            set
            {
                this.sovereigntyWaterCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType LandingCountryCode
        {
            get
            {
                return this.landingCountryCodeField;
            }
            set
            {
                this.landingCountryCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType CatchStatusCode
        {
            get
            {
                return this.catchStatusCodeField;
            }
            set
            {
                this.catchStatusCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedACDRCatch")]
        public ACDRCatchType[] SpecifiedACDRCatch
        {
            get
            {
                return this.specifiedACDRCatchField;
            }
            set
            {
                this.specifiedACDRCatchField = value;
            }
        }
    }

    #endregion

    #region FA 

    #region FA Query
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FAQueryType
    {

        private IDType idField;

        private DateTimeType submittedDateTimeField;

        private CodeType typeCodeField;

        private DelimitedPeriodType specifiedDelimitedPeriodField;

        private FLUXPartyType submitterFLUXPartyField;

        private FAQueryParameterType[] simpleFAQueryParameterField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public DateTimeType SubmittedDateTime
        {
            get
            {
                return this.submittedDateTimeField;
            }
            set
            {
                this.submittedDateTimeField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public DelimitedPeriodType SpecifiedDelimitedPeriod
        {
            get
            {
                return this.specifiedDelimitedPeriodField;
            }
            set
            {
                this.specifiedDelimitedPeriodField = value;
            }
        }

        /// <remarks/>
        public FLUXPartyType SubmitterFLUXParty
        {
            get
            {
                return this.submitterFLUXPartyField;
            }
            set
            {
                this.submitterFLUXPartyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SimpleFAQueryParameter")]
        public FAQueryParameterType[] SimpleFAQueryParameter
        {
            get
            {
                return this.simpleFAQueryParameterField;
            }
            set
            {
                this.simpleFAQueryParameterField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FAQueryParameterType
    {

        private CodeType typeCodeField;

        private CodeType valueCodeField;

        private DateTimeType valueDateTimeField;

        private IDType valueIDField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType ValueCode
        {
            get
            {
                return this.valueCodeField;
            }
            set
            {
                this.valueCodeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType ValueDateTime
        {
            get
            {
                return this.valueDateTimeField;
            }
            set
            {
                this.valueDateTimeField = value;
            }
        }

        /// <remarks/>
        public IDType ValueID
        {
            get
            {
                return this.valueIDField;
            }
            set
            {
                this.valueIDField = value;
            }
        }
    }

    #endregion

    #region FA Response

    #endregion

    #endregion

    #region FLAP

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:18" +
        "")]
    public partial class FLAPQueryType
    {

        private DateTimeType submittedDateTimeField;

        private CodeType typeCodeField;

        private DelimitedPeriodType specifiedDelimitedPeriodField;

        private FLAPIdentityType[] subjectFLAPIdentityField;

        private VesselIdentityType[] subjectVesselIdentityField;

        private FLUXPartyType submitterFLUXPartyField;

        /// <remarks/>
        public DateTimeType SubmittedDateTime
        {
            get
            {
                return this.submittedDateTimeField;
            }
            set
            {
                this.submittedDateTimeField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public DelimitedPeriodType SpecifiedDelimitedPeriod
        {
            get
            {
                return this.specifiedDelimitedPeriodField;
            }
            set
            {
                this.specifiedDelimitedPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SubjectFLAPIdentity")]
        public FLAPIdentityType[] SubjectFLAPIdentity
        {
            get
            {
                return this.subjectFLAPIdentityField;
            }
            set
            {
                this.subjectFLAPIdentityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SubjectVesselIdentity")]
        public VesselIdentityType[] SubjectVesselIdentity
        {
            get
            {
                return this.subjectVesselIdentityField;
            }
            set
            {
                this.subjectVesselIdentityField = value;
            }
        }

        /// <remarks/>
        public FLUXPartyType SubmitterFLUXParty
        {
            get
            {
                return this.submitterFLUXPartyField;
            }
            set
            {
                this.submitterFLUXPartyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:18" +
        "")]
    public partial class FLAPIdentityType
    {

        private IDType idField;

        private IDType requestIDField;

        private CodeType fADATypeCodeField;

        private CodeType fCTypeCodeField;

        private CodeType[] statusCodeField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public IDType RequestID
        {
            get
            {
                return this.requestIDField;
            }
            set
            {
                this.requestIDField = value;
            }
        }

        /// <remarks/>
        public CodeType FADATypeCode
        {
            get
            {
                return this.fADATypeCodeField;
            }
            set
            {
                this.fADATypeCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType FCTypeCode
        {
            get
            {
                return this.fCTypeCodeField;
            }
            set
            {
                this.fCTypeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("StatusCode")]
        public CodeType[] StatusCode
        {
            get
            {
                return this.statusCodeField;
            }
            set
            {
                this.statusCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:18" +
        "")]
    public partial class VesselIdentityType
    {

        private IDType[] vesselIDField;

        private TextType[] vesselNameField;

        private IDType vesselRegistrationCountryIDField;

        private CodeType vesselTypeCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("VesselID")]
        public IDType[] VesselID
        {
            get
            {
                return this.vesselIDField;
            }
            set
            {
                this.vesselIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("VesselName")]
        public TextType[] VesselName
        {
            get
            {
                return this.vesselNameField;
            }
            set
            {
                this.vesselNameField = value;
            }
        }

        /// <remarks/>
        public IDType VesselRegistrationCountryID
        {
            get
            {
                return this.vesselRegistrationCountryIDField;
            }
            set
            {
                this.vesselRegistrationCountryIDField = value;
            }
        }

        /// <remarks/>
        public CodeType VesselTypeCode
        {
            get
            {
                return this.vesselTypeCodeField;
            }
            set
            {
                this.vesselTypeCodeField = value;
            }
        }
    }

    #endregion

    #region MDR

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class MDRQueryType
    {

        private IDType idField;

        private DateTimeType submittedDateTimeField;

        private CodeType typeCodeField;

        private CodeType contractualLanguageCodeField;

        private FLUXPartyType submitterFLUXPartyField;

        private MDRQueryIdentityType subjectMDRQueryIdentityField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public DateTimeType SubmittedDateTime
        {
            get
            {
                return this.submittedDateTimeField;
            }
            set
            {
                this.submittedDateTimeField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType ContractualLanguageCode
        {
            get
            {
                return this.contractualLanguageCodeField;
            }
            set
            {
                this.contractualLanguageCodeField = value;
            }
        }

        /// <remarks/>
        public FLUXPartyType SubmitterFLUXParty
        {
            get
            {
                return this.submitterFLUXPartyField;
            }
            set
            {
                this.submitterFLUXPartyField = value;
            }
        }

        /// <remarks/>
        public MDRQueryIdentityType SubjectMDRQueryIdentity
        {
            get
            {
                return this.subjectMDRQueryIdentityField;
            }
            set
            {
                this.subjectMDRQueryIdentityField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class MDRQueryIdentityType
    {

        private IDType idField;

        private DateTimeType[] validFromDateTimeField;

        private IDType versionIDField;

        private DelimitedPeriodType validityDelimitedPeriodField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ValidFromDateTime")]
        public DateTimeType[] ValidFromDateTime
        {
            get
            {
                return this.validFromDateTimeField;
            }
            set
            {
                this.validFromDateTimeField = value;
            }
        }

        /// <remarks/>
        public IDType VersionID
        {
            get
            {
                return this.versionIDField;
            }
            set
            {
                this.versionIDField = value;
            }
        }

        /// <remarks/>
        public DelimitedPeriodType ValidityDelimitedPeriod
        {
            get
            {
                return this.validityDelimitedPeriodField;
            }
            set
            {
                this.validityDelimitedPeriodField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class MDRElementDataNodeType
    {

        private TextType nameField;

        private TextType valueField;

        private CodeType typeCodeField;

        private FLUXBinaryFileType valueFLUXBinaryFileField;

        /// <remarks/>
        public TextType Name
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
        public TextType Value
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
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public FLUXBinaryFileType ValueFLUXBinaryFile
        {
            get
            {
                return this.valueFLUXBinaryFileField;
            }
            set
            {
                this.valueFLUXBinaryFileField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class MDRDataNodeType
    {

        private IDType idField;

        private IDType parentIDField;

        private NumericType hierarchicalLevelNumericField;

        private DelimitedPeriodType effectiveDelimitedPeriodField;

        private MDRElementDataNodeType[] subordinateMDRElementDataNodeField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public IDType ParentID
        {
            get
            {
                return this.parentIDField;
            }
            set
            {
                this.parentIDField = value;
            }
        }

        /// <remarks/>
        public NumericType HierarchicalLevelNumeric
        {
            get
            {
                return this.hierarchicalLevelNumericField;
            }
            set
            {
                this.hierarchicalLevelNumericField = value;
            }
        }

        /// <remarks/>
        public DelimitedPeriodType EffectiveDelimitedPeriod
        {
            get
            {
                return this.effectiveDelimitedPeriodField;
            }
            set
            {
                this.effectiveDelimitedPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SubordinateMDRElementDataNode")]
        public MDRElementDataNodeType[] SubordinateMDRElementDataNode
        {
            get
            {
                return this.subordinateMDRElementDataNodeField;
            }
            set
            {
                this.subordinateMDRElementDataNodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class DataSetVersionType
    {

        private IDType idField;

        private TextType nameField;

        private DateTimeType validityStartDateTimeField;

        private DateTimeType validityEndDateTimeField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public TextType Name
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
        public DateTimeType ValidityStartDateTime
        {
            get
            {
                return this.validityStartDateTimeField;
            }
            set
            {
                this.validityStartDateTimeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType ValidityEndDateTime
        {
            get
            {
                return this.validityEndDateTimeField;
            }
            set
            {
                this.validityEndDateTimeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class MDRDataSetType
    {

        private IDType idField;

        private TextType descriptionField;

        private TextType originField;

        private TextType nameField;

        private DataSetVersionType[] specifiedDataSetVersionField;

        private MDRDataNodeType[] containedMDRDataNodeField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public TextType Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public TextType Origin
        {
            get
            {
                return this.originField;
            }
            set
            {
                this.originField = value;
            }
        }

        /// <remarks/>
        public TextType Name
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
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedDataSetVersion")]
        public DataSetVersionType[] SpecifiedDataSetVersion
        {
            get
            {
                return this.specifiedDataSetVersionField;
            }
            set
            {
                this.specifiedDataSetVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ContainedMDRDataNode")]
        public MDRDataNodeType[] ContainedMDRDataNode
        {
            get
            {
                return this.containedMDRDataNodeField;
            }
            set
            {
                this.containedMDRDataNodeField = value;
            }
        }
    }

    #endregion

    #region Sales

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SalesQueryType
    {

        private IDType idField;

        private DateTimeType submittedDateTimeField;

        private CodeType typeCodeField;

        private DelimitedPeriodType specifiedDelimitedPeriodField;

        private FLUXPartyType submitterFLUXPartyField;

        private SalesQueryParameterType[] simpleSalesQueryParameterField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public DateTimeType SubmittedDateTime
        {
            get
            {
                return this.submittedDateTimeField;
            }
            set
            {
                this.submittedDateTimeField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public DelimitedPeriodType SpecifiedDelimitedPeriod
        {
            get
            {
                return this.specifiedDelimitedPeriodField;
            }
            set
            {
                this.specifiedDelimitedPeriodField = value;
            }
        }

        /// <remarks/>
        public FLUXPartyType SubmitterFLUXParty
        {
            get
            {
                return this.submitterFLUXPartyField;
            }
            set
            {
                this.submitterFLUXPartyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SimpleSalesQueryParameter")]
        public SalesQueryParameterType[] SimpleSalesQueryParameter
        {
            get
            {
                return this.simpleSalesQueryParameterField;
            }
            set
            {
                this.simpleSalesQueryParameterField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SalesQueryParameterType
    {

        private CodeType typeCodeField;

        private CodeType valueCodeField;

        private DateTimeType valueDateTimeField;

        private IDType valueIDField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType ValueCode
        {
            get
            {
                return this.valueCodeField;
            }
            set
            {
                this.valueCodeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType ValueDateTime
        {
            get
            {
                return this.valueDateTimeField;
            }
            set
            {
                this.valueDateTimeField = value;
            }
        }

        /// <remarks/>
        public IDType ValueID
        {
            get
            {
                return this.valueIDField;
            }
            set
            {
                this.valueIDField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VehicleTransportMeansType
    {

        private IDType idField;

        private IDType registrationCountryIDField;

        private TextType nameField;

        private CodeType typeCodeField;

        private SalesPartyType ownerSalesPartyField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public IDType RegistrationCountryID
        {
            get
            {
                return this.registrationCountryIDField;
            }
            set
            {
                this.registrationCountryIDField = value;
            }
        }

        /// <remarks/>
        public TextType Name
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
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public SalesPartyType OwnerSalesParty
        {
            get
            {
                return this.ownerSalesPartyField;
            }
            set
            {
                this.ownerSalesPartyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SalesPartyType
    {

        private IDType idField;

        private TextType nameField;

        private CodeType typeCodeField;

        private IDType countryIDField;

        private CodeType[] roleCodeField;

        private StructuredAddressType[] specifiedStructuredAddressField;

        private FLUXOrganizationType specifiedFLUXOrganizationField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public TextType Name
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
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public IDType CountryID
        {
            get
            {
                return this.countryIDField;
            }
            set
            {
                this.countryIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RoleCode")]
        public CodeType[] RoleCode
        {
            get
            {
                return this.roleCodeField;
            }
            set
            {
                this.roleCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedStructuredAddress")]
        public StructuredAddressType[] SpecifiedStructuredAddress
        {
            get
            {
                return this.specifiedStructuredAddressField;
            }
            set
            {
                this.specifiedStructuredAddressField = value;
            }
        }

        /// <remarks/>
        public FLUXOrganizationType SpecifiedFLUXOrganization
        {
            get
            {
                return this.specifiedFLUXOrganizationField;
            }
            set
            {
                this.specifiedFLUXOrganizationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FLUXOrganizationType
    {

        private TextType nameField;

        private StructuredAddressType[] postalStructuredAddressField;

        /// <remarks/>
        public TextType Name
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
        [System.Xml.Serialization.XmlElementAttribute("PostalStructuredAddress")]
        public StructuredAddressType[] PostalStructuredAddress
        {
            get
            {
                return this.postalStructuredAddressField;
            }
            set
            {
                this.postalStructuredAddressField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SalesEventType
    {

        private DateTimeType occurrenceDateTimeField;

        private TextType sellerNameField;

        private TextType buyerNameField;

        private SalesBatchType[] relatedSalesBatchField;

        /// <remarks/>
        public DateTimeType OccurrenceDateTime
        {
            get
            {
                return this.occurrenceDateTimeField;
            }
            set
            {
                this.occurrenceDateTimeField = value;
            }
        }

        /// <remarks/>
        public TextType SellerName
        {
            get
            {
                return this.sellerNameField;
            }
            set
            {
                this.sellerNameField = value;
            }
        }

        /// <remarks/>
        public TextType BuyerName
        {
            get
            {
                return this.buyerNameField;
            }
            set
            {
                this.buyerNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedSalesBatch")]
        public SalesBatchType[] RelatedSalesBatch
        {
            get
            {
                return this.relatedSalesBatchField;
            }
            set
            {
                this.relatedSalesBatchField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SalesDocumentType
    {

        private IDType[] idField;

        private CodeType currencyCodeField;

        private IDType[] transportDocumentIDField;

        private IDType[] salesNoteIDField;

        private IDType[] takeoverDocumentIDField;

        private SalesBatchType[] specifiedSalesBatchField;

        private SalesEventType[] specifiedSalesEventField;

        private FishingActivityType[] specifiedFishingActivityField;

        private FLUXLocationType[] specifiedFLUXLocationField;

        private SalesPartyType[] specifiedSalesPartyField;

        private VehicleTransportMeansType specifiedVehicleTransportMeansField;

        private ValidationResultDocumentType[] relatedValidationResultDocumentField;

        private AmountType[] totalSalesPriceField;

        private FLUXLocationType departureSpecifiedFLUXLocationField;

        private FLUXLocationType arrivalSpecifiedFLUXLocationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public CodeType CurrencyCode
        {
            get
            {
                return this.currencyCodeField;
            }
            set
            {
                this.currencyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TransportDocumentID")]
        public IDType[] TransportDocumentID
        {
            get
            {
                return this.transportDocumentIDField;
            }
            set
            {
                this.transportDocumentIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SalesNoteID")]
        public IDType[] SalesNoteID
        {
            get
            {
                return this.salesNoteIDField;
            }
            set
            {
                this.salesNoteIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TakeoverDocumentID")]
        public IDType[] TakeoverDocumentID
        {
            get
            {
                return this.takeoverDocumentIDField;
            }
            set
            {
                this.takeoverDocumentIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedSalesBatch")]
        public SalesBatchType[] SpecifiedSalesBatch
        {
            get
            {
                return this.specifiedSalesBatchField;
            }
            set
            {
                this.specifiedSalesBatchField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedSalesEvent")]
        public SalesEventType[] SpecifiedSalesEvent
        {
            get
            {
                return this.specifiedSalesEventField;
            }
            set
            {
                this.specifiedSalesEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFishingActivity")]
        public FishingActivityType[] SpecifiedFishingActivity
        {
            get
            {
                return this.specifiedFishingActivityField;
            }
            set
            {
                this.specifiedFishingActivityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFLUXLocation")]
        public FLUXLocationType[] SpecifiedFLUXLocation
        {
            get
            {
                return this.specifiedFLUXLocationField;
            }
            set
            {
                this.specifiedFLUXLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedSalesParty")]
        public SalesPartyType[] SpecifiedSalesParty
        {
            get
            {
                return this.specifiedSalesPartyField;
            }
            set
            {
                this.specifiedSalesPartyField = value;
            }
        }

        /// <remarks/>
        public VehicleTransportMeansType SpecifiedVehicleTransportMeans
        {
            get
            {
                return this.specifiedVehicleTransportMeansField;
            }
            set
            {
                this.specifiedVehicleTransportMeansField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedValidationResultDocument")]
        public ValidationResultDocumentType[] RelatedValidationResultDocument
        {
            get
            {
                return this.relatedValidationResultDocumentField;
            }
            set
            {
                this.relatedValidationResultDocumentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ChargeAmount", IsNullable = false)]
        public AmountType[] TotalSalesPrice
        {
            get
            {
                return this.totalSalesPriceField;
            }
            set
            {
                this.totalSalesPriceField = value;
            }
        }

        /// <remarks/>
        public FLUXLocationType DepartureSpecifiedFLUXLocation
        {
            get
            {
                return this.departureSpecifiedFLUXLocationField;
            }
            set
            {
                this.departureSpecifiedFLUXLocationField = value;
            }
        }

        /// <remarks/>
        public FLUXLocationType ArrivalSpecifiedFLUXLocation
        {
            get
            {
                return this.arrivalSpecifiedFLUXLocationField;
            }
            set
            {
                this.arrivalSpecifiedFLUXLocationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SalesReportType
    {

        private IDType idField;

        private CodeType itemTypeCodeField;

        private SalesDocumentType[] includedSalesDocumentField;

        private ValidationResultDocumentType[] includedValidationResultDocumentField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public CodeType ItemTypeCode
        {
            get
            {
                return this.itemTypeCodeField;
            }
            set
            {
                this.itemTypeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IncludedSalesDocument")]
        public SalesDocumentType[] IncludedSalesDocument
        {
            get
            {
                return this.includedSalesDocumentField;
            }
            set
            {
                this.includedSalesDocumentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IncludedValidationResultDocument")]
        public ValidationResultDocumentType[] IncludedValidationResultDocument
        {
            get
            {
                return this.includedValidationResultDocumentField;
            }
            set
            {
                this.includedValidationResultDocumentField = value;
            }
        }
    }


    #endregion

    #region Vessel Pos

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselQueryType
    {

        private DateTimeType submittedDateTimeField;

        private CodeType typeCodeField;

        private IDType idField;

        private FLUXPartyType submitterFLUXPartyField;

        private VesselQueryParameterType[] simpleVesselQueryParameterField;

        private LogicalQueryParameterType[] compoundLogicalQueryParameterField;

        private VesselIdentityType subjectVesselIdentityField;

        private DelimitedPeriodType specifiedDelimitedPeriodField;

        /// <remarks/>
        public DateTimeType SubmittedDateTime
        {
            get
            {
                return this.submittedDateTimeField;
            }
            set
            {
                this.submittedDateTimeField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public FLUXPartyType SubmitterFLUXParty
        {
            get
            {
                return this.submitterFLUXPartyField;
            }
            set
            {
                this.submitterFLUXPartyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SimpleVesselQueryParameter")]
        public VesselQueryParameterType[] SimpleVesselQueryParameter
        {
            get
            {
                return this.simpleVesselQueryParameterField;
            }
            set
            {
                this.simpleVesselQueryParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CompoundLogicalQueryParameter")]
        public LogicalQueryParameterType[] CompoundLogicalQueryParameter
        {
            get
            {
                return this.compoundLogicalQueryParameterField;
            }
            set
            {
                this.compoundLogicalQueryParameterField = value;
            }
        }

        /// <remarks/>
        public VesselIdentityType SubjectVesselIdentity
        {
            get
            {
                return this.subjectVesselIdentityField;
            }
            set
            {
                this.subjectVesselIdentityField = value;
            }
        }

        /// <remarks/>
        public DelimitedPeriodType SpecifiedDelimitedPeriod
        {
            get
            {
                return this.specifiedDelimitedPeriodField;
            }
            set
            {
                this.specifiedDelimitedPeriodField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class PrimitiveQueryParameterType
    {

        private IndicatorType applicableIndicatorField;

        private TextType comparatorField;

        private DateTimeType documentPublicationDateTimeField;

        private TextType scopeField;

        private NumericType realNumberNumericField;

        private NumericType integerNumberNumericField;

        private IndicatorType negationIndicatorField;

        private TextType keywordField;

        private CodeType typeCodeField;

        /// <remarks/>
        public IndicatorType ApplicableIndicator
        {
            get
            {
                return this.applicableIndicatorField;
            }
            set
            {
                this.applicableIndicatorField = value;
            }
        }

        /// <remarks/>
        public TextType Comparator
        {
            get
            {
                return this.comparatorField;
            }
            set
            {
                this.comparatorField = value;
            }
        }

        /// <remarks/>
        public DateTimeType DocumentPublicationDateTime
        {
            get
            {
                return this.documentPublicationDateTimeField;
            }
            set
            {
                this.documentPublicationDateTimeField = value;
            }
        }

        /// <remarks/>
        public TextType Scope
        {
            get
            {
                return this.scopeField;
            }
            set
            {
                this.scopeField = value;
            }
        }

        /// <remarks/>
        public NumericType RealNumberNumeric
        {
            get
            {
                return this.realNumberNumericField;
            }
            set
            {
                this.realNumberNumericField = value;
            }
        }

        /// <remarks/>
        public NumericType IntegerNumberNumeric
        {
            get
            {
                return this.integerNumberNumericField;
            }
            set
            {
                this.integerNumberNumericField = value;
            }
        }

        /// <remarks/>
        public IndicatorType NegationIndicator
        {
            get
            {
                return this.negationIndicatorField;
            }
            set
            {
                this.negationIndicatorField = value;
            }
        }

        /// <remarks/>
        public TextType Keyword
        {
            get
            {
                return this.keywordField;
            }
            set
            {
                this.keywordField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class LogicalQueryParameterType
    {

        private TextType logicalOperatorField;

        private IndicatorType negationIndicatorField;

        private LogicalQueryParameterType subordinateLogicalQueryParameterField;

        private PrimitiveQueryParameterType firstSubordinatePrimitiveQueryParameterField;

        private PrimitiveQueryParameterType secondSubordinatePrimitiveQueryParameterField;

        /// <remarks/>
        public TextType LogicalOperator
        {
            get
            {
                return this.logicalOperatorField;
            }
            set
            {
                this.logicalOperatorField = value;
            }
        }

        /// <remarks/>
        public IndicatorType NegationIndicator
        {
            get
            {
                return this.negationIndicatorField;
            }
            set
            {
                this.negationIndicatorField = value;
            }
        }

        /// <remarks/>
        public LogicalQueryParameterType SubordinateLogicalQueryParameter
        {
            get
            {
                return this.subordinateLogicalQueryParameterField;
            }
            set
            {
                this.subordinateLogicalQueryParameterField = value;
            }
        }

        /// <remarks/>
        public PrimitiveQueryParameterType FirstSubordinatePrimitiveQueryParameter
        {
            get
            {
                return this.firstSubordinatePrimitiveQueryParameterField;
            }
            set
            {
                this.firstSubordinatePrimitiveQueryParameterField = value;
            }
        }

        /// <remarks/>
        public PrimitiveQueryParameterType SecondSubordinatePrimitiveQueryParameter
        {
            get
            {
                return this.secondSubordinatePrimitiveQueryParameterField;
            }
            set
            {
                this.secondSubordinatePrimitiveQueryParameterField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselQueryParameterType
    {

        private CodeType searchTypeCodeField;

        /// <remarks/>
        public CodeType SearchTypeCode
        {
            get
            {
                return this.searchTypeCodeField;
            }
            set
            {
                this.searchTypeCodeField = value;
            }
        }
    }
    #endregion

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FLUXPartyType
    {

        private IDType[] idField;

        private TextType[] nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Name")]
        public TextType[] Name
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class DelimitedPeriodType
    {

        private DateTimeType startDateTimeField;

        private DateTimeType endDateTimeField;

        private MeasureType durationMeasureField;

        /// <remarks/>
        public DateTimeType StartDateTime
        {
            get
            {
                return this.startDateTimeField;
            }
            set
            {
                this.startDateTimeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType EndDateTime
        {
            get
            {
                return this.endDateTimeField;
            }
            set
            {
                this.endDateTimeField = value;
            }
        }

        /// <remarks/>
        public MeasureType DurationMeasure
        {
            get
            {
                return this.durationMeasureField;
            }
            set
            {
                this.durationMeasureField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FLUXResponseDocumentType
    {

        private IDType[] idField;

        private IDType referencedIDField;

        private DateTimeType creationDateTimeField;

        private CodeType responseCodeField;

        private TextType remarksField;

        private TextType rejectionReasonField;

        private CodeType typeCodeField;

        private ValidationResultDocumentType[] relatedValidationResultDocumentField;

        private FLUXPartyType respondentFLUXPartyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public IDType ReferencedID
        {
            get
            {
                return this.referencedIDField;
            }
            set
            {
                this.referencedIDField = value;
            }
        }

        /// <remarks/>
        public DateTimeType CreationDateTime
        {
            get
            {
                return this.creationDateTimeField;
            }
            set
            {
                this.creationDateTimeField = value;
            }
        }

        /// <remarks/>
        public CodeType ResponseCode
        {
            get
            {
                return this.responseCodeField;
            }
            set
            {
                this.responseCodeField = value;
            }
        }

        /// <remarks/>
        public TextType Remarks
        {
            get
            {
                return this.remarksField;
            }
            set
            {
                this.remarksField = value;
            }
        }

        /// <remarks/>
        public TextType RejectionReason
        {
            get
            {
                return this.rejectionReasonField;
            }
            set
            {
                this.rejectionReasonField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedValidationResultDocument")]
        public ValidationResultDocumentType[] RelatedValidationResultDocument
        {
            get
            {
                return this.relatedValidationResultDocumentField;
            }
            set
            {
                this.relatedValidationResultDocumentField = value;
            }
        }

        /// <remarks/>
        public FLUXPartyType RespondentFLUXParty
        {
            get
            {
                return this.respondentFLUXPartyField;
            }
            set
            {
                this.respondentFLUXPartyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselEquipmentCharacteristicType
    {

        private CodeType typeCodeField;

        private TextType[] descriptionField;

        private MeasureType valueMeasureField;

        private TextType[] valueField;

        private CodeType[] valueCodeField;

        private DateTimeType valueDateTimeField;

        private IndicatorType valueIndicatorField;

        private QuantityType valueQuantityField;

        private FLUXBinaryFileType valueFLUXBinaryFileField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public MeasureType ValueMeasure
        {
            get
            {
                return this.valueMeasureField;
            }
            set
            {
                this.valueMeasureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Value")]
        public TextType[] Value
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
        [System.Xml.Serialization.XmlElementAttribute("ValueCode")]
        public CodeType[] ValueCode
        {
            get
            {
                return this.valueCodeField;
            }
            set
            {
                this.valueCodeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType ValueDateTime
        {
            get
            {
                return this.valueDateTimeField;
            }
            set
            {
                this.valueDateTimeField = value;
            }
        }

        /// <remarks/>
        public IndicatorType ValueIndicator
        {
            get
            {
                return this.valueIndicatorField;
            }
            set
            {
                this.valueIndicatorField = value;
            }
        }

        /// <remarks/>
        public QuantityType ValueQuantity
        {
            get
            {
                return this.valueQuantityField;
            }
            set
            {
                this.valueQuantityField = value;
            }
        }

        /// <remarks/>
        public FLUXBinaryFileType ValueFLUXBinaryFile
        {
            get
            {
                return this.valueFLUXBinaryFileField;
            }
            set
            {
                this.valueFLUXBinaryFileField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselAdministrativeCharacteristicType
    {

        private CodeType typeCodeField;

        private TextType[] descriptionField;

        private MeasureType valueMeasureField;

        private TextType valueField;

        private CodeType valueCodeField;

        private DateTimeType valueDateTimeField;

        private IndicatorType valueIndicatorField;

        private QuantityType valueQuantityField;

        private FLUXBinaryFileType valueFLUXBinaryFileField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public MeasureType ValueMeasure
        {
            get
            {
                return this.valueMeasureField;
            }
            set
            {
                this.valueMeasureField = value;
            }
        }

        /// <remarks/>
        public TextType Value
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
        public CodeType ValueCode
        {
            get
            {
                return this.valueCodeField;
            }
            set
            {
                this.valueCodeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType ValueDateTime
        {
            get
            {
                return this.valueDateTimeField;
            }
            set
            {
                this.valueDateTimeField = value;
            }
        }

        /// <remarks/>
        public IndicatorType ValueIndicator
        {
            get
            {
                return this.valueIndicatorField;
            }
            set
            {
                this.valueIndicatorField = value;
            }
        }

        /// <remarks/>
        public QuantityType ValueQuantity
        {
            get
            {
                return this.valueQuantityField;
            }
            set
            {
                this.valueQuantityField = value;
            }
        }

        /// <remarks/>
        public FLUXBinaryFileType ValueFLUXBinaryFile
        {
            get
            {
                return this.valueFLUXBinaryFileField;
            }
            set
            {
                this.valueFLUXBinaryFileField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselStorageCharacteristicType
    {

        private CodeType[] typeCodeField;

        private CodeType methodTypeCodeField;

        private MeasureType[] capacityValueMeasureField;

        private MeasureType[] temperatureValueMeasureField;

        private QuantityType unitValueQuantityField;

        private IDType idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TypeCode")]
        public CodeType[] TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType MethodTypeCode
        {
            get
            {
                return this.methodTypeCodeField;
            }
            set
            {
                this.methodTypeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CapacityValueMeasure")]
        public MeasureType[] CapacityValueMeasure
        {
            get
            {
                return this.capacityValueMeasureField;
            }
            set
            {
                this.capacityValueMeasureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TemperatureValueMeasure")]
        public MeasureType[] TemperatureValueMeasure
        {
            get
            {
                return this.temperatureValueMeasureField;
            }
            set
            {
                this.temperatureValueMeasureField = value;
            }
        }

        /// <remarks/>
        public QuantityType UnitValueQuantity
        {
            get
            {
                return this.unitValueQuantityField;
            }
            set
            {
                this.unitValueQuantityField = value;
            }
        }

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselTechnicalCharacteristicType
    {

        private CodeType typeCodeField;

        private TextType[] descriptionField;

        private MeasureType valueMeasureField;

        private TextType valueField;

        private CodeType valueCodeField;

        private DateTimeType valueDateTimeField;

        private IndicatorType valueIndicatorField;

        private QuantityType valueQuantityField;

        private FLUXBinaryFileType valueFLUXBinaryFileField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public MeasureType ValueMeasure
        {
            get
            {
                return this.valueMeasureField;
            }
            set
            {
                this.valueMeasureField = value;
            }
        }

        /// <remarks/>
        public TextType Value
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
        public CodeType ValueCode
        {
            get
            {
                return this.valueCodeField;
            }
            set
            {
                this.valueCodeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType ValueDateTime
        {
            get
            {
                return this.valueDateTimeField;
            }
            set
            {
                this.valueDateTimeField = value;
            }
        }

        /// <remarks/>
        public IndicatorType ValueIndicator
        {
            get
            {
                return this.valueIndicatorField;
            }
            set
            {
                this.valueIndicatorField = value;
            }
        }

        /// <remarks/>
        public QuantityType ValueQuantity
        {
            get
            {
                return this.valueQuantityField;
            }
            set
            {
                this.valueQuantityField = value;
            }
        }

        /// <remarks/>
        public FLUXBinaryFileType ValueFLUXBinaryFile
        {
            get
            {
                return this.valueFLUXBinaryFileField;
            }
            set
            {
                this.valueFLUXBinaryFileField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SpecifiedPeriodType
    {

        private MeasureType[] durationMeasureField;

        private IndicatorType inclusiveIndicatorField;

        private TextType[] descriptionField;

        private DateTimeType startDateTimeField;

        private DateTimeType endDateTimeField;

        private DateTimeType completeDateTimeField;

        private IndicatorType openIndicatorField;

        private CodeType seasonCodeField;

        private IDType idField;

        private TextType[] nameField;

        private NumericType[] sequenceNumericField;

        private CodeType startDateFlexibilityCodeField;

        private IndicatorType continuousIndicatorField;

        private CodeType purposeCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DurationMeasure")]
        public MeasureType[] DurationMeasure
        {
            get
            {
                return this.durationMeasureField;
            }
            set
            {
                this.durationMeasureField = value;
            }
        }

        /// <remarks/>
        public IndicatorType InclusiveIndicator
        {
            get
            {
                return this.inclusiveIndicatorField;
            }
            set
            {
                this.inclusiveIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public DateTimeType StartDateTime
        {
            get
            {
                return this.startDateTimeField;
            }
            set
            {
                this.startDateTimeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType EndDateTime
        {
            get
            {
                return this.endDateTimeField;
            }
            set
            {
                this.endDateTimeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType CompleteDateTime
        {
            get
            {
                return this.completeDateTimeField;
            }
            set
            {
                this.completeDateTimeField = value;
            }
        }

        /// <remarks/>
        public IndicatorType OpenIndicator
        {
            get
            {
                return this.openIndicatorField;
            }
            set
            {
                this.openIndicatorField = value;
            }
        }

        /// <remarks/>
        public CodeType SeasonCode
        {
            get
            {
                return this.seasonCodeField;
            }
            set
            {
                this.seasonCodeField = value;
            }
        }

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Name")]
        public TextType[] Name
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
        [System.Xml.Serialization.XmlElementAttribute("SequenceNumeric")]
        public NumericType[] SequenceNumeric
        {
            get
            {
                return this.sequenceNumericField;
            }
            set
            {
                this.sequenceNumericField = value;
            }
        }

        /// <remarks/>
        public CodeType StartDateFlexibilityCode
        {
            get
            {
                return this.startDateFlexibilityCodeField;
            }
            set
            {
                this.startDateFlexibilityCodeField = value;
            }
        }

        /// <remarks/>
        public IndicatorType ContinuousIndicator
        {
            get
            {
                return this.continuousIndicatorField;
            }
            set
            {
                this.continuousIndicatorField = value;
            }
        }

        /// <remarks/>
        public CodeType PurposeCode
        {
            get
            {
                return this.purposeCodeField;
            }
            set
            {
                this.purposeCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class EmailCommunicationType
    {

        private IDType uRIIDField;

        /// <remarks/>
        public IDType URIID
        {
            get
            {
                return this.uRIIDField;
            }
            set
            {
                this.uRIIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class WebsiteCommunicationType
    {

        private IDType websiteURIIDField;

        /// <remarks/>
        public IDType WebsiteURIID
        {
            get
            {
                return this.websiteURIIDField;
            }
            set
            {
                this.websiteURIIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class UniversalCommunicationType
    {

        private IDType uRIIDField;

        private CommunicationChannelCodeType channelCodeField;

        private TextType localNumberField;

        private TextType completeNumberField;

        private CodeType countryNumberCodeField;

        private TextType extensionNumberField;

        private CodeType areaNumberCodeField;

        private TextType[] accessField;

        private CodeType useCodeField;

        private IndicatorType hTMLPreferredIndicatorField;

        private SpecifiedPreferenceType usageSpecifiedPreferenceField;

        /// <remarks/>
        public IDType URIID
        {
            get
            {
                return this.uRIIDField;
            }
            set
            {
                this.uRIIDField = value;
            }
        }

        /// <remarks/>
        public CommunicationChannelCodeType ChannelCode
        {
            get
            {
                return this.channelCodeField;
            }
            set
            {
                this.channelCodeField = value;
            }
        }

        /// <remarks/>
        public TextType LocalNumber
        {
            get
            {
                return this.localNumberField;
            }
            set
            {
                this.localNumberField = value;
            }
        }

        /// <remarks/>
        public TextType CompleteNumber
        {
            get
            {
                return this.completeNumberField;
            }
            set
            {
                this.completeNumberField = value;
            }
        }

        /// <remarks/>
        public CodeType CountryNumberCode
        {
            get
            {
                return this.countryNumberCodeField;
            }
            set
            {
                this.countryNumberCodeField = value;
            }
        }

        /// <remarks/>
        public TextType ExtensionNumber
        {
            get
            {
                return this.extensionNumberField;
            }
            set
            {
                this.extensionNumberField = value;
            }
        }

        /// <remarks/>
        public CodeType AreaNumberCode
        {
            get
            {
                return this.areaNumberCodeField;
            }
            set
            {
                this.areaNumberCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Access")]
        public TextType[] Access
        {
            get
            {
                return this.accessField;
            }
            set
            {
                this.accessField = value;
            }
        }

        /// <remarks/>
        public CodeType UseCode
        {
            get
            {
                return this.useCodeField;
            }
            set
            {
                this.useCodeField = value;
            }
        }

        /// <remarks/>
        public IndicatorType HTMLPreferredIndicator
        {
            get
            {
                return this.hTMLPreferredIndicatorField;
            }
            set
            {
                this.hTMLPreferredIndicatorField = value;
            }
        }

        /// <remarks/>
        public SpecifiedPreferenceType UsageSpecifiedPreference
        {
            get
            {
                return this.usageSpecifiedPreferenceField;
            }
            set
            {
                this.usageSpecifiedPreferenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:Standard:QualifiedDataType:20")]
    public partial class CommunicationChannelCodeType
    {

        private string listIDField;

        private CommunicationChannelCodeListAgencyIDContentType listAgencyIDField;

        private bool listAgencyIDFieldSpecified;

        private string listVersionIDField;

        private string listURIField;

        private CommunicationMeansTypeCodeContentType valueField;

        public CommunicationChannelCodeType()
        {
            this.listIDField = "3155_CommunicationChannelCode";
            this.listAgencyIDField = CommunicationChannelCodeListAgencyIDContentType.Item6;
            this.listVersionIDField = "D18B";
        }

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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public CommunicationChannelCodeListAgencyIDContentType listAgencyID
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool listAgencyIDSpecified
        {
            get
            {
                return this.listAgencyIDFieldSpecified;
            }
            set
            {
                this.listAgencyIDFieldSpecified = value;
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
        [System.Xml.Serialization.XmlTextAttribute()]
        public CommunicationMeansTypeCodeContentType Value
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:Standard:QualifiedDataType:20")]
    public enum CommunicationChannelCodeListAgencyIDContentType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:codelist:standard:UNECE:CommunicationMeansTypeCode:D18B")]
    public enum CommunicationMeansTypeCodeContentType
    {

        /// <remarks/>
        AA,

        /// <remarks/>
        AB,

        /// <remarks/>
        AC,

        /// <remarks/>
        AD,

        /// <remarks/>
        AE,

        /// <remarks/>
        AF,

        /// <remarks/>
        AG,

        /// <remarks/>
        AH,

        /// <remarks/>
        AI,

        /// <remarks/>
        AJ,

        /// <remarks/>
        AK,

        /// <remarks/>
        AL,

        /// <remarks/>
        AM,

        /// <remarks/>
        AN,

        /// <remarks/>
        AO,

        /// <remarks/>
        AP,

        /// <remarks/>
        AQ,

        /// <remarks/>
        AR,

        /// <remarks/>
        AS,

        /// <remarks/>
        AT,

        /// <remarks/>
        AU,

        /// <remarks/>
        AV,

        /// <remarks/>
        AW,

        /// <remarks/>
        CA,

        /// <remarks/>
        EI,

        /// <remarks/>
        EM,

        /// <remarks/>
        EX,

        /// <remarks/>
        FT,

        /// <remarks/>
        FX,

        /// <remarks/>
        GM,

        /// <remarks/>
        IE,

        /// <remarks/>
        IM,

        /// <remarks/>
        MA,

        /// <remarks/>
        PB,

        /// <remarks/>
        PS,

        /// <remarks/>
        SW,

        /// <remarks/>
        TE,

        /// <remarks/>
        TG,

        /// <remarks/>
        TL,

        /// <remarks/>
        TM,

        /// <remarks/>
        TT,

        /// <remarks/>
        TX,

        /// <remarks/>
        XF,

        /// <remarks/>
        XG,

        /// <remarks/>
        XH,

        /// <remarks/>
        XI,

        /// <remarks/>
        XJ,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselEngineType
    {

        private IDType serialNumberIDField;

        private CodeType typeCodeField;

        private CodeType roleCodeField;

        private CodeType propulsionTypeCodeField;

        private MeasureType[] powerMeasureField;

        private CodeType powerMeasurementMethodCodeField;

        private CodeType manufacturerCodeField;

        private TextType modelField;

        private TextType manufacturerField;

        private FLUXPictureType[] illustrateFLUXPictureField;

        /// <remarks/>
        public IDType SerialNumberID
        {
            get
            {
                return this.serialNumberIDField;
            }
            set
            {
                this.serialNumberIDField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType RoleCode
        {
            get
            {
                return this.roleCodeField;
            }
            set
            {
                this.roleCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType PropulsionTypeCode
        {
            get
            {
                return this.propulsionTypeCodeField;
            }
            set
            {
                this.propulsionTypeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PowerMeasure")]
        public MeasureType[] PowerMeasure
        {
            get
            {
                return this.powerMeasureField;
            }
            set
            {
                this.powerMeasureField = value;
            }
        }

        /// <remarks/>
        public CodeType PowerMeasurementMethodCode
        {
            get
            {
                return this.powerMeasurementMethodCodeField;
            }
            set
            {
                this.powerMeasurementMethodCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType ManufacturerCode
        {
            get
            {
                return this.manufacturerCodeField;
            }
            set
            {
                this.manufacturerCodeField = value;
            }
        }

        /// <remarks/>
        public TextType Model
        {
            get
            {
                return this.modelField;
            }
            set
            {
                this.modelField = value;
            }
        }

        /// <remarks/>
        public TextType Manufacturer
        {
            get
            {
                return this.manufacturerField;
            }
            set
            {
                this.manufacturerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IllustrateFLUXPicture")]
        public FLUXPictureType[] IllustrateFLUXPicture
        {
            get
            {
                return this.illustrateFLUXPictureField;
            }
            set
            {
                this.illustrateFLUXPictureField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FLUXPictureType
    {

        private IDType idField;

        private CodeType typeCodeField;

        private DateTimeType takenDateTimeField;

        private IDType areaIncludedIDField;

        private TextType descriptionField;

        private BinaryObjectType digitalImageBinaryObjectField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType TakenDateTime
        {
            get
            {
                return this.takenDateTimeField;
            }
            set
            {
                this.takenDateTimeField = value;
            }
        }

        /// <remarks/>
        public IDType AreaIncludedID
        {
            get
            {
                return this.areaIncludedIDField;
            }
            set
            {
                this.areaIncludedIDField = value;
            }
        }

        /// <remarks/>
        public TextType Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public BinaryObjectType DigitalImageBinaryObject
        {
            get
            {
                return this.digitalImageBinaryObjectField;
            }
            set
            {
                this.digitalImageBinaryObjectField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class ConstructionEventType
    {

        private TextType[] descriptionField;

        private DateTimeType occurrenceDateTimeField;

        private ConstructionLocationType relatedConstructionLocationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public DateTimeType OccurrenceDateTime
        {
            get
            {
                return this.occurrenceDateTimeField;
            }
            set
            {
                this.occurrenceDateTimeField = value;
            }
        }

        /// <remarks/>
        public ConstructionLocationType RelatedConstructionLocation
        {
            get
            {
                return this.relatedConstructionLocationField;
            }
            set
            {
                this.relatedConstructionLocationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class ConstructionLocationType
    {

        private IDType countryIDField;

        private TextType[] descriptionField;

        private CodeType geopoliticalRegionCodeField;

        private IDType[] idField;

        private TextType[] nameField;

        private CodeType typeCodeField;

        private ContactPartyType[] specifiedContactPartyField;

        private StructuredAddressType physicalStructuredAddressField;

        /// <remarks/>
        public IDType CountryID
        {
            get
            {
                return this.countryIDField;
            }
            set
            {
                this.countryIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public CodeType GeopoliticalRegionCode
        {
            get
            {
                return this.geopoliticalRegionCodeField;
            }
            set
            {
                this.geopoliticalRegionCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Name")]
        public TextType[] Name
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
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedContactParty")]
        public ContactPartyType[] SpecifiedContactParty
        {
            get
            {
                return this.specifiedContactPartyField;
            }
            set
            {
                this.specifiedContactPartyField = value;
            }
        }

        /// <remarks/>
        public StructuredAddressType PhysicalStructuredAddress
        {
            get
            {
                return this.physicalStructuredAddressField;
            }
            set
            {
                this.physicalStructuredAddressField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselPositionEventType
    {

        private DateTimeType obtainedOccurrenceDateTimeField;

        private CodeType typeCodeField;

        private MeasureType speedValueMeasureField;

        private MeasureType courseValueMeasureField;

        private CodeType activityTypeCodeField;

        private VesselGeographicalCoordinateType specifiedVesselGeographicalCoordinateField;

        /// <remarks/>
        public DateTimeType ObtainedOccurrenceDateTime
        {
            get
            {
                return this.obtainedOccurrenceDateTimeField;
            }
            set
            {
                this.obtainedOccurrenceDateTimeField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public MeasureType SpeedValueMeasure
        {
            get
            {
                return this.speedValueMeasureField;
            }
            set
            {
                this.speedValueMeasureField = value;
            }
        }

        /// <remarks/>
        public MeasureType CourseValueMeasure
        {
            get
            {
                return this.courseValueMeasureField;
            }
            set
            {
                this.courseValueMeasureField = value;
            }
        }

        /// <remarks/>
        public CodeType ActivityTypeCode
        {
            get
            {
                return this.activityTypeCodeField;
            }
            set
            {
                this.activityTypeCodeField = value;
            }
        }

        /// <remarks/>
        public VesselGeographicalCoordinateType SpecifiedVesselGeographicalCoordinate
        {
            get
            {
                return this.specifiedVesselGeographicalCoordinateField;
            }
            set
            {
                this.specifiedVesselGeographicalCoordinateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselGeographicalCoordinateType
    {

        private MeasureType altitudeMeasureField;

        private MeasureType latitudeMeasureField;

        private MeasureType longitudeMeasureField;

        private IDType systemIDField;

        /// <remarks/>
        public MeasureType AltitudeMeasure
        {
            get
            {
                return this.altitudeMeasureField;
            }
            set
            {
                this.altitudeMeasureField = value;
            }
        }

        /// <remarks/>
        public MeasureType LatitudeMeasure
        {
            get
            {
                return this.latitudeMeasureField;
            }
            set
            {
                this.latitudeMeasureField = value;
            }
        }

        /// <remarks/>
        public MeasureType LongitudeMeasure
        {
            get
            {
                return this.longitudeMeasureField;
            }
            set
            {
                this.longitudeMeasureField = value;
            }
        }

        /// <remarks/>
        public IDType SystemID
        {
            get
            {
                return this.systemIDField;
            }
            set
            {
                this.systemIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class RegistrationEventType
    {

        private TextType[] descriptionField;

        private DateTimeType occurrenceDateTimeField;

        private RegistrationLocationType relatedRegistrationLocationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public DateTimeType OccurrenceDateTime
        {
            get
            {
                return this.occurrenceDateTimeField;
            }
            set
            {
                this.occurrenceDateTimeField = value;
            }
        }

        /// <remarks/>
        public RegistrationLocationType RelatedRegistrationLocation
        {
            get
            {
                return this.relatedRegistrationLocationField;
            }
            set
            {
                this.relatedRegistrationLocationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class RegistrationLocationType
    {

        private IDType countryIDField;

        private TextType[] descriptionField;

        private CodeType geopoliticalRegionCodeField;

        private IDType[] idField;

        private TextType[] nameField;

        private CodeType typeCodeField;

        private StructuredAddressType physicalStructuredAddressField;

        /// <remarks/>
        public IDType CountryID
        {
            get
            {
                return this.countryIDField;
            }
            set
            {
                this.countryIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public CodeType GeopoliticalRegionCode
        {
            get
            {
                return this.geopoliticalRegionCodeField;
            }
            set
            {
                this.geopoliticalRegionCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Name")]
        public TextType[] Name
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
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public StructuredAddressType PhysicalStructuredAddress
        {
            get
            {
                return this.physicalStructuredAddressField;
            }
            set
            {
                this.physicalStructuredAddressField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselTransportMeansType
    {

        private IDType[] idField;

        private TextType[] nameField;

        private CodeType[] typeCodeField;

        private DateTimeType commissioningDateTimeField;

        private CodeType operationalStatusCodeField;

        private CodeType hullMaterialCodeField;

        private MeasureType draughtMeasureField;

        private MeasureType speedMeasureField;

        private MeasureType trawlingSpeedMeasureField;

        private CodeType roleCodeField;

        private VesselCountryType registrationVesselCountryField;

        private VesselPositionEventType[] specifiedVesselPositionEventField;

        private RegistrationEventType[] specifiedRegistrationEventField;

        private ConstructionEventType specifiedConstructionEventField;

        private VesselEngineType[] attachedVesselEngineField;

        private VesselDimensionType[] specifiedVesselDimensionField;

        private FishingGearType[] onBoardFishingGearField;

        private VesselEquipmentCharacteristicType[] applicableVesselEquipmentCharacteristicField;

        private VesselAdministrativeCharacteristicType[] applicableVesselAdministrativeCharacteristicField;

        private FLUXPictureType[] illustrateFLUXPictureField;

        private ContactPartyType[] specifiedContactPartyField;

        private VesselCrewType specifiedVesselCrewField;

        private VesselStorageCharacteristicType[] applicableVesselStorageCharacteristicField;

        private VesselTechnicalCharacteristicType[] applicableVesselTechnicalCharacteristicField;

        private FLAPDocumentType[] grantedFLAPDocumentField;

        private FLUXLocationType[] specifiedFLUXLocationField;

        private FACatchType[] specifiedFACatchField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Name")]
        public TextType[] Name
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
        [System.Xml.Serialization.XmlElementAttribute("TypeCode")]
        public CodeType[] TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType CommissioningDateTime
        {
            get
            {
                return this.commissioningDateTimeField;
            }
            set
            {
                this.commissioningDateTimeField = value;
            }
        }

        /// <remarks/>
        public CodeType OperationalStatusCode
        {
            get
            {
                return this.operationalStatusCodeField;
            }
            set
            {
                this.operationalStatusCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType HullMaterialCode
        {
            get
            {
                return this.hullMaterialCodeField;
            }
            set
            {
                this.hullMaterialCodeField = value;
            }
        }

        /// <remarks/>
        public MeasureType DraughtMeasure
        {
            get
            {
                return this.draughtMeasureField;
            }
            set
            {
                this.draughtMeasureField = value;
            }
        }

        /// <remarks/>
        public MeasureType SpeedMeasure
        {
            get
            {
                return this.speedMeasureField;
            }
            set
            {
                this.speedMeasureField = value;
            }
        }

        /// <remarks/>
        public MeasureType TrawlingSpeedMeasure
        {
            get
            {
                return this.trawlingSpeedMeasureField;
            }
            set
            {
                this.trawlingSpeedMeasureField = value;
            }
        }

        /// <remarks/>
        public CodeType RoleCode
        {
            get
            {
                return this.roleCodeField;
            }
            set
            {
                this.roleCodeField = value;
            }
        }

        /// <remarks/>
        public VesselCountryType RegistrationVesselCountry
        {
            get
            {
                return this.registrationVesselCountryField;
            }
            set
            {
                this.registrationVesselCountryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedVesselPositionEvent")]
        public VesselPositionEventType[] SpecifiedVesselPositionEvent
        {
            get
            {
                return this.specifiedVesselPositionEventField;
            }
            set
            {
                this.specifiedVesselPositionEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedRegistrationEvent")]
        public RegistrationEventType[] SpecifiedRegistrationEvent
        {
            get
            {
                return this.specifiedRegistrationEventField;
            }
            set
            {
                this.specifiedRegistrationEventField = value;
            }
        }

        /// <remarks/>
        public ConstructionEventType SpecifiedConstructionEvent
        {
            get
            {
                return this.specifiedConstructionEventField;
            }
            set
            {
                this.specifiedConstructionEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AttachedVesselEngine")]
        public VesselEngineType[] AttachedVesselEngine
        {
            get
            {
                return this.attachedVesselEngineField;
            }
            set
            {
                this.attachedVesselEngineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedVesselDimension")]
        public VesselDimensionType[] SpecifiedVesselDimension
        {
            get
            {
                return this.specifiedVesselDimensionField;
            }
            set
            {
                this.specifiedVesselDimensionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("OnBoardFishingGear")]
        public FishingGearType[] OnBoardFishingGear
        {
            get
            {
                return this.onBoardFishingGearField;
            }
            set
            {
                this.onBoardFishingGearField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ApplicableVesselEquipmentCharacteristic")]
        public VesselEquipmentCharacteristicType[] ApplicableVesselEquipmentCharacteristic
        {
            get
            {
                return this.applicableVesselEquipmentCharacteristicField;
            }
            set
            {
                this.applicableVesselEquipmentCharacteristicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ApplicableVesselAdministrativeCharacteristic")]
        public VesselAdministrativeCharacteristicType[] ApplicableVesselAdministrativeCharacteristic
        {
            get
            {
                return this.applicableVesselAdministrativeCharacteristicField;
            }
            set
            {
                this.applicableVesselAdministrativeCharacteristicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IllustrateFLUXPicture")]
        public FLUXPictureType[] IllustrateFLUXPicture
        {
            get
            {
                return this.illustrateFLUXPictureField;
            }
            set
            {
                this.illustrateFLUXPictureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedContactParty")]
        public ContactPartyType[] SpecifiedContactParty
        {
            get
            {
                return this.specifiedContactPartyField;
            }
            set
            {
                this.specifiedContactPartyField = value;
            }
        }

        /// <remarks/>
        public VesselCrewType SpecifiedVesselCrew
        {
            get
            {
                return this.specifiedVesselCrewField;
            }
            set
            {
                this.specifiedVesselCrewField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ApplicableVesselStorageCharacteristic")]
        public VesselStorageCharacteristicType[] ApplicableVesselStorageCharacteristic
        {
            get
            {
                return this.applicableVesselStorageCharacteristicField;
            }
            set
            {
                this.applicableVesselStorageCharacteristicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ApplicableVesselTechnicalCharacteristic")]
        public VesselTechnicalCharacteristicType[] ApplicableVesselTechnicalCharacteristic
        {
            get
            {
                return this.applicableVesselTechnicalCharacteristicField;
            }
            set
            {
                this.applicableVesselTechnicalCharacteristicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GrantedFLAPDocument")]
        public FLAPDocumentType[] GrantedFLAPDocument
        {
            get
            {
                return this.grantedFLAPDocumentField;
            }
            set
            {
                this.grantedFLAPDocumentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFLUXLocation")]
        public FLUXLocationType[] SpecifiedFLUXLocation
        {
            get
            {
                return this.specifiedFLUXLocationField;
            }
            set
            {
                this.specifiedFLUXLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFACatch")]
        public FACatchType[] SpecifiedFACatch
        {
            get
            {
                return this.specifiedFACatchField;
            }
            set
            {
                this.specifiedFACatchField = value;
            }
        }
    }

    /// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public  class VesselTransportMeansType1
    {

        private IDType[] idField;

        private TextType[] nameField;

        private CodeType[] typeCodeField;

        private DateTimeType commissioningDateTimeField;

        private CodeType operationalStatusCodeField;

        private CodeType hullMaterialCodeField;

        private MeasureType draughtMeasureField;

        private MeasureType speedMeasureField;

        private MeasureType trawlingSpeedMeasureField;

        private CodeType roleCodeField;

        private VesselCountryType registrationVesselCountryField;

        private VesselPositionEventType[] specifiedVesselPositionEventField;

        private RegistrationEventType[] specifiedRegistrationEventField;

        private ConstructionEventType specifiedConstructionEventField;

        private VesselEngineType[] attachedVesselEngineField;

        private VesselDimensionType[] specifiedVesselDimensionField;

        private FishingGearType1[] onBoardFishingGearField;

        //private VesselEquipmentCharacteristicType[] applicableVesselEquipmentCharacteristicField;

        //private VesselAdministrativeCharacteristicType[] applicableVesselAdministrativeCharacteristicField;

        //private FLUXPictureType[] illustrateFLUXPictureField;

        //private ContactPartyType[] specifiedContactPartyField;

        //private VesselCrewType specifiedVesselCrewField;

        //private VesselStorageCharacteristicType[] applicableVesselStorageCharacteristicField;

        //private VesselTechnicalCharacteristicType[] applicableVesselTechnicalCharacteristicField;

        //private FLAPDocumentType[] grantedFLAPDocumentField;

        //private FLUXLocationType[] specifiedFLUXLocationField;

        //private FACatchType[] specifiedFACatchField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Name")]
        public TextType[] Name
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
        [System.Xml.Serialization.XmlElementAttribute("TypeCode")]
        public CodeType[] TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType CommissioningDateTime
        {
            get
            {
                return this.commissioningDateTimeField;
            }
            set
            {
                this.commissioningDateTimeField = value;
            }
        }

        /// <remarks/>
        public CodeType OperationalStatusCode
        {
            get
            {
                return this.operationalStatusCodeField;
            }
            set
            {
                this.operationalStatusCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType HullMaterialCode
        {
            get
            {
                return this.hullMaterialCodeField;
            }
            set
            {
                this.hullMaterialCodeField = value;
            }
        }

        /// <remarks/>
        public MeasureType DraughtMeasure
        {
            get
            {
                return this.draughtMeasureField;
            }
            set
            {
                this.draughtMeasureField = value;
            }
        }

        /// <remarks/>
        public MeasureType SpeedMeasure
        {
            get
            {
                return this.speedMeasureField;
            }
            set
            {
                this.speedMeasureField = value;
            }
        }

        /// <remarks/>
        public MeasureType TrawlingSpeedMeasure
        {
            get
            {
                return this.trawlingSpeedMeasureField;
            }
            set
            {
                this.trawlingSpeedMeasureField = value;
            }
        }

        /// <remarks/>
        public CodeType RoleCode
        {
            get
            {
                return this.roleCodeField;
            }
            set
            {
                this.roleCodeField = value;
            }
        }

        /// <remarks/>
        public VesselCountryType RegistrationVesselCountry
        {
            get
            {
                return this.registrationVesselCountryField;
            }
            set
            {
                this.registrationVesselCountryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedVesselPositionEvent")]
        public VesselPositionEventType[] SpecifiedVesselPositionEvent
        {
            get
            {
                return this.specifiedVesselPositionEventField;
            }
            set
            {
                this.specifiedVesselPositionEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedRegistrationEvent")]
        public RegistrationEventType[] SpecifiedRegistrationEvent
        {
            get
            {
                return this.specifiedRegistrationEventField;
            }
            set
            {
                this.specifiedRegistrationEventField = value;
            }
        }

        /// <remarks/>
        public ConstructionEventType SpecifiedConstructionEvent
        {
            get
            {
                return this.specifiedConstructionEventField;
            }
            set
            {
                this.specifiedConstructionEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AttachedVesselEngine")]
        public VesselEngineType[] AttachedVesselEngine
        {
            get
            {
                return this.attachedVesselEngineField;
            }
            set
            {
                this.attachedVesselEngineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedVesselDimension")]
        public VesselDimensionType[] SpecifiedVesselDimension
        {
            get
            {
                return this.specifiedVesselDimensionField;
            }
            set
            {
                this.specifiedVesselDimensionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("OnBoardFishingGear")]
        public FishingGearType1[] OnBoardFishingGear
        {
            get
            {
                return this.onBoardFishingGearField;
            }
            set
            {
                this.onBoardFishingGearField = value;
            }
        }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("ApplicableVesselEquipmentCharacteristic")]
        //public VesselEquipmentCharacteristicType[] ApplicableVesselEquipmentCharacteristic
        //{
        //    get
        //    {
        //        return this.applicableVesselEquipmentCharacteristicField;
        //    }
        //    set
        //    {
        //        this.applicableVesselEquipmentCharacteristicField = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("ApplicableVesselAdministrativeCharacteristic")]
        //public VesselAdministrativeCharacteristicType[] ApplicableVesselAdministrativeCharacteristic
        //{
        //    get
        //    {
        //        return this.applicableVesselAdministrativeCharacteristicField;
        //    }
        //    set
        //    {
        //        this.applicableVesselAdministrativeCharacteristicField = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("IllustrateFLUXPicture")]
        //public FLUXPictureType[] IllustrateFLUXPicture
        //{
        //    get
        //    {
        //        return this.illustrateFLUXPictureField;
        //    }
        //    set
        //    {
        //        this.illustrateFLUXPictureField = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("SpecifiedContactParty")]
        //public ContactPartyType[] SpecifiedContactParty
        //{
        //    get
        //    {
        //        return this.specifiedContactPartyField;
        //    }
        //    set
        //    {
        //        this.specifiedContactPartyField = value;
        //    }
        //}

        ///// <remarks/>
        //public VesselCrewType SpecifiedVesselCrew
        //{
        //    get
        //    {
        //        return this.specifiedVesselCrewField;
        //    }
        //    set
        //    {
        //        this.specifiedVesselCrewField = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("ApplicableVesselStorageCharacteristic")]
        //public VesselStorageCharacteristicType[] ApplicableVesselStorageCharacteristic
        //{
        //    get
        //    {
        //        return this.applicableVesselStorageCharacteristicField;
        //    }
        //    set
        //    {
        //        this.applicableVesselStorageCharacteristicField = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("ApplicableVesselTechnicalCharacteristic")]
        //public VesselTechnicalCharacteristicType[] ApplicableVesselTechnicalCharacteristic
        //{
        //    get
        //    {
        //        return this.applicableVesselTechnicalCharacteristicField;
        //    }
        //    set
        //    {
        //        this.applicableVesselTechnicalCharacteristicField = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("GrantedFLAPDocument")]
        //public FLAPDocumentType[] GrantedFLAPDocument
        //{
        //    get
        //    {
        //        return this.grantedFLAPDocumentField;
        //    }
        //    set
        //    {
        //        this.grantedFLAPDocumentField = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("SpecifiedFLUXLocation")]
        //public FLUXLocationType[] SpecifiedFLUXLocation
        //{
        //    get
        //    {
        //        return this.specifiedFLUXLocationField;
        //    }
        //    set
        //    {
        //        this.specifiedFLUXLocationField = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("SpecifiedFACatch")]
        //public FACatchType[] SpecifiedFACatch
        //{
        //    get
        //    {
        //        return this.specifiedFACatchField;
        //    }
        //    set
        //    {
        //        this.specifiedFACatchField = value;
        //    }
        //}
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselCountryType
    {

        private IDType idField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
 

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class StructuredAddressType
    {

        private IDType idField;

        private CodeType postcodeCodeField;

        private TextType buildingNameField;

        private TextType streetNameField;

        private TextType cityNameField;

        private IDType countryIDField;

        private TextType citySubDivisionNameField;

        private TextType countryNameField;

        private TextType countrySubDivisionNameField;

        private TextType blockNameField;

        private TextType plotIdentificationField;

        private TextType postOfficeBoxField;

        private TextType buildingNumberField;

        private TextType staircaseNumberField;

        private TextType floorIdentificationField;

        private TextType roomIdentificationField;

        private TextType postalAreaField;

        private TextType postcodeField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public CodeType PostcodeCode
        {
            get
            {
                return this.postcodeCodeField;
            }
            set
            {
                this.postcodeCodeField = value;
            }
        }

        /// <remarks/>
        public TextType BuildingName
        {
            get
            {
                return this.buildingNameField;
            }
            set
            {
                this.buildingNameField = value;
            }
        }

        /// <remarks/>
        public TextType StreetName
        {
            get
            {
                return this.streetNameField;
            }
            set
            {
                this.streetNameField = value;
            }
        }

        /// <remarks/>
        public TextType CityName
        {
            get
            {
                return this.cityNameField;
            }
            set
            {
                this.cityNameField = value;
            }
        }

        /// <remarks/>
        public IDType CountryID
        {
            get
            {
                return this.countryIDField;
            }
            set
            {
                this.countryIDField = value;
            }
        }

        /// <remarks/>
        public TextType CitySubDivisionName
        {
            get
            {
                return this.citySubDivisionNameField;
            }
            set
            {
                this.citySubDivisionNameField = value;
            }
        }

        /// <remarks/>
        public TextType CountryName
        {
            get
            {
                return this.countryNameField;
            }
            set
            {
                this.countryNameField = value;
            }
        }

        /// <remarks/>
        public TextType CountrySubDivisionName
        {
            get
            {
                return this.countrySubDivisionNameField;
            }
            set
            {
                this.countrySubDivisionNameField = value;
            }
        }

        /// <remarks/>
        public TextType BlockName
        {
            get
            {
                return this.blockNameField;
            }
            set
            {
                this.blockNameField = value;
            }
        }

        /// <remarks/>
        public TextType PlotIdentification
        {
            get
            {
                return this.plotIdentificationField;
            }
            set
            {
                this.plotIdentificationField = value;
            }
        }

        /// <remarks/>
        public TextType PostOfficeBox
        {
            get
            {
                return this.postOfficeBoxField;
            }
            set
            {
                this.postOfficeBoxField = value;
            }
        }

        /// <remarks/>
        public TextType BuildingNumber
        {
            get
            {
                return this.buildingNumberField;
            }
            set
            {
                this.buildingNumberField = value;
            }
        }

        /// <remarks/>
        public TextType StaircaseNumber
        {
            get
            {
                return this.staircaseNumberField;
            }
            set
            {
                this.staircaseNumberField = value;
            }
        }

        /// <remarks/>
        public TextType FloorIdentification
        {
            get
            {
                return this.floorIdentificationField;
            }
            set
            {
                this.floorIdentificationField = value;
            }
        }

        /// <remarks/>
        public TextType RoomIdentification
        {
            get
            {
                return this.roomIdentificationField;
            }
            set
            {
                this.roomIdentificationField = value;
            }
        }

        /// <remarks/>
        public TextType PostalArea
        {
            get
            {
                return this.postalAreaField;
            }
            set
            {
                this.postalAreaField = value;
            }
        }

        /// <remarks/>
        public TextType Postcode
        {
            get
            {
                return this.postcodeField;
            }
            set
            {
                this.postcodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class ContactPartyType
    {

        private IDType[] idField;

        private TextType nameField;

        private TextType[] descriptionField;

        private IDType[] nationalityCountryIDField;

        private CodeType[] languageCodeField;

        private IDType residenceCountryIDField;

        private CodeType[] roleCodeField;

        private StructuredAddressType[] specifiedStructuredAddressField;

        private ContactPersonType[] specifiedContactPersonField;

        private TelecommunicationCommunicationType[] telephoneTelecommunicationCommunicationField;

        private TelecommunicationCommunicationType[] faxTelecommunicationCommunicationField;

        private EmailCommunicationType[] uRIEmailCommunicationField;

        private UniversalCommunicationType[] specifiedUniversalCommunicationField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public TextType Name
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
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NationalityCountryID")]
        public IDType[] NationalityCountryID
        {
            get
            {
                return this.nationalityCountryIDField;
            }
            set
            {
                this.nationalityCountryIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("LanguageCode")]
        public CodeType[] LanguageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        /// <remarks/>
        public IDType ResidenceCountryID
        {
            get
            {
                return this.residenceCountryIDField;
            }
            set
            {
                this.residenceCountryIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RoleCode")]
        public CodeType[] RoleCode
        {
            get
            {
                return this.roleCodeField;
            }
            set
            {
                this.roleCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedStructuredAddress")]
        public StructuredAddressType[] SpecifiedStructuredAddress
        {
            get
            {
                return this.specifiedStructuredAddressField;
            }
            set
            {
                this.specifiedStructuredAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedContactPerson")]
        public ContactPersonType[] SpecifiedContactPerson
        {
            get
            {
                return this.specifiedContactPersonField;
            }
            set
            {
                this.specifiedContactPersonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TelephoneTelecommunicationCommunication")]
        public TelecommunicationCommunicationType[] TelephoneTelecommunicationCommunication
        {
            get
            {
                return this.telephoneTelecommunicationCommunicationField;
            }
            set
            {
                this.telephoneTelecommunicationCommunicationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FaxTelecommunicationCommunication")]
        public TelecommunicationCommunicationType[] FaxTelecommunicationCommunication
        {
            get
            {
                return this.faxTelecommunicationCommunicationField;
            }
            set
            {
                this.faxTelecommunicationCommunicationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("URIEmailCommunication")]
        public EmailCommunicationType[] URIEmailCommunication
        {
            get
            {
                return this.uRIEmailCommunicationField;
            }
            set
            {
                this.uRIEmailCommunicationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedUniversalCommunication")]
        public UniversalCommunicationType[] SpecifiedUniversalCommunication
        {
            get
            {
                return this.specifiedUniversalCommunicationField;
            }
            set
            {
                this.specifiedUniversalCommunicationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class ContactPersonType
    {

        private TextType titleField;

        private TextType givenNameField;

        private TextType middleNameField;

        private TextType familyNamePrefixField;

        private TextType familyNameField;

        private TextType nameSuffixField;

        private CodeType genderCodeField;

        private TextType aliasField;

        private DateTimeType birthDateTimeField;

        private TextType birthplaceNameField;

        private TelecommunicationCommunicationType telephoneTelecommunicationCommunicationField;

        private TelecommunicationCommunicationType faxTelecommunicationCommunicationField;

        private EmailCommunicationType emailURIEmailCommunicationField;

        private WebsiteCommunicationType websiteURIWebsiteCommunicationField;

        private UniversalCommunicationType[] specifiedUniversalCommunicationField;

        /// <remarks/>
        public TextType Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public TextType GivenName
        {
            get
            {
                return this.givenNameField;
            }
            set
            {
                this.givenNameField = value;
            }
        }

        /// <remarks/>
        public TextType MiddleName
        {
            get
            {
                return this.middleNameField;
            }
            set
            {
                this.middleNameField = value;
            }
        }

        /// <remarks/>
        public TextType FamilyNamePrefix
        {
            get
            {
                return this.familyNamePrefixField;
            }
            set
            {
                this.familyNamePrefixField = value;
            }
        }

        /// <remarks/>
        public TextType FamilyName
        {
            get
            {
                return this.familyNameField;
            }
            set
            {
                this.familyNameField = value;
            }
        }

        /// <remarks/>
        public TextType NameSuffix
        {
            get
            {
                return this.nameSuffixField;
            }
            set
            {
                this.nameSuffixField = value;
            }
        }

        /// <remarks/>
        public CodeType GenderCode
        {
            get
            {
                return this.genderCodeField;
            }
            set
            {
                this.genderCodeField = value;
            }
        }

        /// <remarks/>
        public TextType Alias
        {
            get
            {
                return this.aliasField;
            }
            set
            {
                this.aliasField = value;
            }
        }

        /// <remarks/>
        public DateTimeType BirthDateTime
        {
            get
            {
                return this.birthDateTimeField;
            }
            set
            {
                this.birthDateTimeField = value;
            }
        }

        /// <remarks/>
        public TextType BirthplaceName
        {
            get
            {
                return this.birthplaceNameField;
            }
            set
            {
                this.birthplaceNameField = value;
            }
        }

        /// <remarks/>
        public TelecommunicationCommunicationType TelephoneTelecommunicationCommunication
        {
            get
            {
                return this.telephoneTelecommunicationCommunicationField;
            }
            set
            {
                this.telephoneTelecommunicationCommunicationField = value;
            }
        }

        /// <remarks/>
        public TelecommunicationCommunicationType FaxTelecommunicationCommunication
        {
            get
            {
                return this.faxTelecommunicationCommunicationField;
            }
            set
            {
                this.faxTelecommunicationCommunicationField = value;
            }
        }

        /// <remarks/>
        public EmailCommunicationType EmailURIEmailCommunication
        {
            get
            {
                return this.emailURIEmailCommunicationField;
            }
            set
            {
                this.emailURIEmailCommunicationField = value;
            }
        }

        /// <remarks/>
        public WebsiteCommunicationType WebsiteURIWebsiteCommunication
        {
            get
            {
                return this.websiteURIWebsiteCommunicationField;
            }
            set
            {
                this.websiteURIWebsiteCommunicationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedUniversalCommunication")]
        public UniversalCommunicationType[] SpecifiedUniversalCommunication
        {
            get
            {
                return this.specifiedUniversalCommunicationField;
            }
            set
            {
                this.specifiedUniversalCommunicationField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class TelecommunicationCommunicationType
    {

        private TextType localNumberField;

        private TextType completeNumberField;

        private CodeType countryNumberCodeField;

        private TextType extensionNumberField;

        private CodeType areaNumberCodeField;

        private TextType internalAccessField;

        private CodeType useCodeField;

        private TextType specialDeviceTypeField;

        private SpecifiedPreferenceType usageSpecifiedPreferenceField;

        /// <remarks/>
        public TextType LocalNumber
        {
            get
            {
                return this.localNumberField;
            }
            set
            {
                this.localNumberField = value;
            }
        }

        /// <remarks/>
        public TextType CompleteNumber
        {
            get
            {
                return this.completeNumberField;
            }
            set
            {
                this.completeNumberField = value;
            }
        }

        /// <remarks/>
        public CodeType CountryNumberCode
        {
            get
            {
                return this.countryNumberCodeField;
            }
            set
            {
                this.countryNumberCodeField = value;
            }
        }

        /// <remarks/>
        public TextType ExtensionNumber
        {
            get
            {
                return this.extensionNumberField;
            }
            set
            {
                this.extensionNumberField = value;
            }
        }

        /// <remarks/>
        public CodeType AreaNumberCode
        {
            get
            {
                return this.areaNumberCodeField;
            }
            set
            {
                this.areaNumberCodeField = value;
            }
        }

        /// <remarks/>
        public TextType InternalAccess
        {
            get
            {
                return this.internalAccessField;
            }
            set
            {
                this.internalAccessField = value;
            }
        }

        /// <remarks/>
        public CodeType UseCode
        {
            get
            {
                return this.useCodeField;
            }
            set
            {
                this.useCodeField = value;
            }
        }

        /// <remarks/>
        public TextType SpecialDeviceType
        {
            get
            {
                return this.specialDeviceTypeField;
            }
            set
            {
                this.specialDeviceTypeField = value;
            }
        }

        /// <remarks/>
        public SpecifiedPreferenceType UsageSpecifiedPreference
        {
            get
            {
                return this.usageSpecifiedPreferenceField;
            }
            set
            {
                this.usageSpecifiedPreferenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FLUXBinaryFileType
    {

        private TextType titleField;

        private BinaryObjectType includedBinaryObjectField;

        private TextType descriptionField;

        private MeasureType sizeMeasureField;

        private CodeType typeCodeField;

        private TextType nameField;

        /// <remarks/>
        public TextType Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public BinaryObjectType IncludedBinaryObject
        {
            get
            {
                return this.includedBinaryObjectField;
            }
            set
            {
                this.includedBinaryObjectField = value;
            }
        }

        /// <remarks/>
        public TextType Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public MeasureType SizeMeasure
        {
            get
            {
                return this.sizeMeasureField;
            }
            set
            {
                this.sizeMeasureField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public TextType Name
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
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SpecifiedPreferenceType
    {

        private NumericType priorityRankingNumericField;

        private IndicatorType preferredIndicatorField;

        private SpecifiedPeriodType unavailableSpecifiedPeriodField;

        private SpecifiedPeriodType availableSpecifiedPeriodField;

        /// <remarks/>
        public NumericType PriorityRankingNumeric
        {
            get
            {
                return this.priorityRankingNumericField;
            }
            set
            {
                this.priorityRankingNumericField = value;
            }
        }

        /// <remarks/>
        public IndicatorType PreferredIndicator
        {
            get
            {
                return this.preferredIndicatorField;
            }
            set
            {
                this.preferredIndicatorField = value;
            }
        }

        /// <remarks/>
        public SpecifiedPeriodType UnavailableSpecifiedPeriod
        {
            get
            {
                return this.unavailableSpecifiedPeriodField;
            }
            set
            {
                this.unavailableSpecifiedPeriodField = value;
            }
        }

        /// <remarks/>
        public SpecifiedPeriodType AvailableSpecifiedPeriod
        {
            get
            {
                return this.availableSpecifiedPeriodField;
            }
            set
            {
                this.availableSpecifiedPeriodField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselDimensionType
    {

        private CodeType typeCodeField;

        private MeasureType valueMeasureField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public MeasureType ValueMeasure
        {
            get
            {
                return this.valueMeasureField;
            }
            set
            {
                this.valueMeasureField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FishingGearType
    {

        private CodeType typeCodeField;

        private CodeType[] roleCodeField;

        private FLUXPictureType[] illustrateFLUXPictureField;

        private GearCharacteristicType[] applicableGearCharacteristicField;

        private GearInspectionEventType[] relatedGearInspectionEventField;

        private FishingGearEquipmentType[] attachedFishingGearEquipmentField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RoleCode")]
        public CodeType[] RoleCode
        {
            get
            {
                return this.roleCodeField;
            }
            set
            {
                this.roleCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IllustrateFLUXPicture")]
        public FLUXPictureType[] IllustrateFLUXPicture
        {
            get
            {
                return this.illustrateFLUXPictureField;
            }
            set
            {
                this.illustrateFLUXPictureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ApplicableGearCharacteristic")]
        public GearCharacteristicType[] ApplicableGearCharacteristic
        {
            get
            {
                return this.applicableGearCharacteristicField;
            }
            set
            {
                this.applicableGearCharacteristicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedGearInspectionEvent")]
        public GearInspectionEventType[] RelatedGearInspectionEvent
        {
            get
            {
                return this.relatedGearInspectionEventField;
            }
            set
            {
                this.relatedGearInspectionEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AttachedFishingGearEquipment")]
        public FishingGearEquipmentType[] AttachedFishingGearEquipment
        {
            get
            {
                return this.attachedFishingGearEquipmentField;
            }
            set
            {
                this.attachedFishingGearEquipmentField = value;
            }
        }
    }

    /// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FishingGearType1
    {

        private CodeType typeCodeField;

        private CodeType[] roleCodeField;

        private FLUXPictureType[] illustrateFLUXPictureField;

        //private GearCharacteristicType[] applicableGearCharacteristicField;

        //private GearInspectionEventType[] relatedGearInspectionEventField;

        //private FishingGearEquipmentType[] attachedFishingGearEquipmentField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RoleCode")]
        public CodeType[] RoleCode
        {
            get
            {
                return this.roleCodeField;
            }
            set
            {
                this.roleCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IllustrateFLUXPicture")]
        public FLUXPictureType[] IllustrateFLUXPicture
        {
            get
            {
                return this.illustrateFLUXPictureField;
            }
            set
            {
                this.illustrateFLUXPictureField = value;
            }
        }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("ApplicableGearCharacteristic")]
        //public GearCharacteristicType[] ApplicableGearCharacteristic
        //{
        //    get
        //    {
        //        return this.applicableGearCharacteristicField;
        //    }
        //    set
        //    {
        //        this.applicableGearCharacteristicField = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("RelatedGearInspectionEvent")]
        //public GearInspectionEventType[] RelatedGearInspectionEvent
        //{
        //    get
        //    {
        //        return this.relatedGearInspectionEventField;
        //    }
        //    set
        //    {
        //        this.relatedGearInspectionEventField = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("AttachedFishingGearEquipment")]
        //public FishingGearEquipmentType[] AttachedFishingGearEquipment
        //{
        //    get
        //    {
        //        return this.attachedFishingGearEquipmentField;
        //    }
        //    set
        //    {
        //        this.attachedFishingGearEquipmentField = value;
        //    }
        //}
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FishingGearEquipmentType
    {

        private CodeType typeCodeField;

        private FLUXCharacteristicType[] applicableFLUXCharacteristicField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ApplicableFLUXCharacteristic")]
        public FLUXCharacteristicType[] ApplicableFLUXCharacteristic
        {
            get
            {
                return this.applicableFLUXCharacteristicField;
            }
            set
            {
                this.applicableFLUXCharacteristicField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class GearCharacteristicType
    {

        private CodeType typeCodeField;

        private TextType[] descriptionField;

        private MeasureType valueMeasureField;

        private DateTimeType valueDateTimeField;

        private IndicatorType valueIndicatorField;

        private CodeType valueCodeField;

        private TextType valueField;

        private QuantityType valueQuantityField;

        private FLUXLocationType[] specifiedFLUXLocationField;

        private FLUXBinaryFileType valueFLUXBinaryFileField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public MeasureType ValueMeasure
        {
            get
            {
                return this.valueMeasureField;
            }
            set
            {
                this.valueMeasureField = value;
            }
        }

        /// <remarks/>
        public DateTimeType ValueDateTime
        {
            get
            {
                return this.valueDateTimeField;
            }
            set
            {
                this.valueDateTimeField = value;
            }
        }

        /// <remarks/>
        public IndicatorType ValueIndicator
        {
            get
            {
                return this.valueIndicatorField;
            }
            set
            {
                this.valueIndicatorField = value;
            }
        }

        /// <remarks/>
        public CodeType ValueCode
        {
            get
            {
                return this.valueCodeField;
            }
            set
            {
                this.valueCodeField = value;
            }
        }

        /// <remarks/>
        public TextType Value
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
        public QuantityType ValueQuantity
        {
            get
            {
                return this.valueQuantityField;
            }
            set
            {
                this.valueQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFLUXLocation")]
        public FLUXLocationType[] SpecifiedFLUXLocation
        {
            get
            {
                return this.specifiedFLUXLocationField;
            }
            set
            {
                this.specifiedFLUXLocationField = value;
            }
        }

        /// <remarks/>
        public FLUXBinaryFileType ValueFLUXBinaryFile
        {
            get
            {
                return this.valueFLUXBinaryFileField;
            }
            set
            {
                this.valueFLUXBinaryFileField = value;
            }
        }
    }
     
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FLUXLocationType
    {

        private CodeType typeCodeField;

        private IDType countryIDField;

        private IDType idField;

        private CodeType geopoliticalRegionCodeField;

        private TextType[] nameField;

        private IDType sovereignRightsCountryIDField;

        private IDType jurisdictionCountryIDField;

        private CodeType regionalFisheriesManagementOrganizationCodeField;

        private FLUXGeographicalCoordinateType specifiedPhysicalFLUXGeographicalCoordinateField;

        private StructuredAddressType[] postalStructuredAddressField;

        private StructuredAddressType physicalStructuredAddressField;

        private SpecifiedPolygonType[] boundarySpecifiedPolygonField;

        private FLUXCharacteristicType[] applicableFLUXCharacteristicField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public IDType CountryID
        {
            get
            {
                return this.countryIDField;
            }
            set
            {
                this.countryIDField = value;
            }
        }

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public CodeType GeopoliticalRegionCode
        {
            get
            {
                return this.geopoliticalRegionCodeField;
            }
            set
            {
                this.geopoliticalRegionCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Name")]
        public TextType[] Name
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
        public IDType SovereignRightsCountryID
        {
            get
            {
                return this.sovereignRightsCountryIDField;
            }
            set
            {
                this.sovereignRightsCountryIDField = value;
            }
        }

        /// <remarks/>
        public IDType JurisdictionCountryID
        {
            get
            {
                return this.jurisdictionCountryIDField;
            }
            set
            {
                this.jurisdictionCountryIDField = value;
            }
        }

        /// <remarks/>
        public CodeType RegionalFisheriesManagementOrganizationCode
        {
            get
            {
                return this.regionalFisheriesManagementOrganizationCodeField;
            }
            set
            {
                this.regionalFisheriesManagementOrganizationCodeField = value;
            }
        }

        /// <remarks/>
        public FLUXGeographicalCoordinateType SpecifiedPhysicalFLUXGeographicalCoordinate
        {
            get
            {
                return this.specifiedPhysicalFLUXGeographicalCoordinateField;
            }
            set
            {
                this.specifiedPhysicalFLUXGeographicalCoordinateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PostalStructuredAddress")]
        public StructuredAddressType[] PostalStructuredAddress
        {
            get
            {
                return this.postalStructuredAddressField;
            }
            set
            {
                this.postalStructuredAddressField = value;
            }
        }

        /// <remarks/>
        public StructuredAddressType PhysicalStructuredAddress
        {
            get
            {
                return this.physicalStructuredAddressField;
            }
            set
            {
                this.physicalStructuredAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("BoundarySpecifiedPolygon")]
        public SpecifiedPolygonType[] BoundarySpecifiedPolygon
        {
            get
            {
                return this.boundarySpecifiedPolygonField;
            }
            set
            {
                this.boundarySpecifiedPolygonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ApplicableFLUXCharacteristic")]
        public FLUXCharacteristicType[] ApplicableFLUXCharacteristic
        {
            get
            {
                return this.applicableFLUXCharacteristicField;
            }
            set
            {
                this.applicableFLUXCharacteristicField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FLUXGeographicalCoordinateType
    {

        private MeasureType longitudeMeasureField;

        private MeasureType latitudeMeasureField;

        private MeasureType altitudeMeasureField;

        private IDType systemIDField;

        /// <remarks/>
        public MeasureType LongitudeMeasure
        {
            get
            {
                return this.longitudeMeasureField;
            }
            set
            {
                this.longitudeMeasureField = value;
            }
        }

        /// <remarks/>
        public MeasureType LatitudeMeasure
        {
            get
            {
                return this.latitudeMeasureField;
            }
            set
            {
                this.latitudeMeasureField = value;
            }
        }

        /// <remarks/>
        public MeasureType AltitudeMeasure
        {
            get
            {
                return this.altitudeMeasureField;
            }
            set
            {
                this.altitudeMeasureField = value;
            }
        }

        /// <remarks/>
        public IDType SystemID
        {
            get
            {
                return this.systemIDField;
            }
            set
            {
                this.systemIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SpecifiedPolygonType
    {

        private SpecifiedLinearRingType[] interiorSpecifiedLinearRingField;

        private SpecifiedLinearRingType exteriorSpecifiedLinearRingField;

        private SpecifiedGeographicalObjectCharacteristicType associatedSpecifiedGeographicalObjectCharacteristicField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("InteriorSpecifiedLinearRing")]
        public SpecifiedLinearRingType[] InteriorSpecifiedLinearRing
        {
            get
            {
                return this.interiorSpecifiedLinearRingField;
            }
            set
            {
                this.interiorSpecifiedLinearRingField = value;
            }
        }

        /// <remarks/>
        public SpecifiedLinearRingType ExteriorSpecifiedLinearRing
        {
            get
            {
                return this.exteriorSpecifiedLinearRingField;
            }
            set
            {
                this.exteriorSpecifiedLinearRingField = value;
            }
        }

        /// <remarks/>
        public SpecifiedGeographicalObjectCharacteristicType AssociatedSpecifiedGeographicalObjectCharacteristic
        {
            get
            {
                return this.associatedSpecifiedGeographicalObjectCharacteristicField;
            }
            set
            {
                this.associatedSpecifiedGeographicalObjectCharacteristicField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SpecifiedLinearRingType
    {

        private TextType[] coordinateField;

        private SpecifiedDirectPositionType coordinateSpecifiedDirectPositionField;

        private FLUXGeographicalCoordinateType[] specifiedFLUXGeographicalCoordinateField;

        private SpecifiedGeographicalObjectCharacteristicType associatedSpecifiedGeographicalObjectCharacteristicField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Coordinate")]
        public TextType[] Coordinate
        {
            get
            {
                return this.coordinateField;
            }
            set
            {
                this.coordinateField = value;
            }
        }

        /// <remarks/>
        public SpecifiedDirectPositionType CoordinateSpecifiedDirectPosition
        {
            get
            {
                return this.coordinateSpecifiedDirectPositionField;
            }
            set
            {
                this.coordinateSpecifiedDirectPositionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFLUXGeographicalCoordinate")]
        public FLUXGeographicalCoordinateType[] SpecifiedFLUXGeographicalCoordinate
        {
            get
            {
                return this.specifiedFLUXGeographicalCoordinateField;
            }
            set
            {
                this.specifiedFLUXGeographicalCoordinateField = value;
            }
        }

        /// <remarks/>
        public SpecifiedGeographicalObjectCharacteristicType AssociatedSpecifiedGeographicalObjectCharacteristic
        {
            get
            {
                return this.associatedSpecifiedGeographicalObjectCharacteristicField;
            }
            set
            {
                this.associatedSpecifiedGeographicalObjectCharacteristicField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SpecifiedDirectPositionType
    {

        private TextType nameField;

        private TextType[] coordinateReferenceDimensionField;

        private TextType[] axisLabelListField;

        private TextType[] uOMLabelListField;

        private NumericType[] countNumericField;

        /// <remarks/>
        public TextType Name
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
        [System.Xml.Serialization.XmlElementAttribute("CoordinateReferenceDimension")]
        public TextType[] CoordinateReferenceDimension
        {
            get
            {
                return this.coordinateReferenceDimensionField;
            }
            set
            {
                this.coordinateReferenceDimensionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AxisLabelList")]
        public TextType[] AxisLabelList
        {
            get
            {
                return this.axisLabelListField;
            }
            set
            {
                this.axisLabelListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("UOMLabelList")]
        public TextType[] UOMLabelList
        {
            get
            {
                return this.uOMLabelListField;
            }
            set
            {
                this.uOMLabelListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CountNumeric")]
        public NumericType[] CountNumeric
        {
            get
            {
                return this.countNumericField;
            }
            set
            {
                this.countNumericField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SpecifiedGeographicalObjectCharacteristicType
    {

        private IDType idField;

        private TextType descriptionField;

        private TextType descriptionReferenceField;

        private TextType nameField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public TextType Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public TextType DescriptionReference
        {
            get
            {
                return this.descriptionReferenceField;
            }
            set
            {
                this.descriptionReferenceField = value;
            }
        }

        /// <remarks/>
        public TextType Name
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FLUXCharacteristicType
    {

        private CodeType typeCodeField;

        private TextType[] descriptionField;

        private MeasureType valueMeasureField;

        private DateTimeType valueDateTimeField;

        private IndicatorType valueIndicatorField;

        private CodeType valueCodeField;

        private TextType[] valueField;

        private QuantityType valueQuantityField;

        private FLUXLocationType[] specifiedFLUXLocationField;

        private FLAPDocumentType[] relatedFLAPDocumentField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public MeasureType ValueMeasure
        {
            get
            {
                return this.valueMeasureField;
            }
            set
            {
                this.valueMeasureField = value;
            }
        }

        /// <remarks/>
        public DateTimeType ValueDateTime
        {
            get
            {
                return this.valueDateTimeField;
            }
            set
            {
                this.valueDateTimeField = value;
            }
        }

        /// <remarks/>
        public IndicatorType ValueIndicator
        {
            get
            {
                return this.valueIndicatorField;
            }
            set
            {
                this.valueIndicatorField = value;
            }
        }

        /// <remarks/>
        public CodeType ValueCode
        {
            get
            {
                return this.valueCodeField;
            }
            set
            {
                this.valueCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Value")]
        public TextType[] Value
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
        public QuantityType ValueQuantity
        {
            get
            {
                return this.valueQuantityField;
            }
            set
            {
                this.valueQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFLUXLocation")]
        public FLUXLocationType[] SpecifiedFLUXLocation
        {
            get
            {
                return this.specifiedFLUXLocationField;
            }
            set
            {
                this.specifiedFLUXLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedFLAPDocument")]
        public FLAPDocumentType[] RelatedFLAPDocument
        {
            get
            {
                return this.relatedFLAPDocumentField;
            }
            set
            {
                this.relatedFLAPDocumentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FLAPDocumentType
    {

        private IDType idField;

        private IDType[] vesselIDField;

        private IDType[] joinedVesselIDField;

        private TextType nameField;

        private IndicatorType firstApplicationIndicatorField;

        private TextType remarksField;

        private DateTimeType decisionDateTimeField;

        private CodeType typeCodeField;

        private FLUXCharacteristicType[] specifiedFLUXCharacteristicField;

        private VesselCrewType[] specifiedVesselCrewField;

        private DelimitedPeriodType[] applicableDelimitedPeriodField;

        private DelimitedPeriodType entryIntoForceDelimitedPeriodField;

        private VesselTransportCharterType[] specifiedVesselTransportCharterField;

        private ContactPartyType[] specifiedContactPartyField;

        private TargetedQuotaType[] specifiedTargetedQuotaField;

        private FLUXBinaryFileType[] attachedFLUXBinaryFileField;

        private FLUXLocationType[] specifiedFLUXLocationField;

        private ValidationResultDocumentType[] relatedValidationResultDocumentField;

        private FLAPRequestDocumentType[] relatedFLAPRequestDocumentField;

        private AuthorizationStatusType[] specifiedAuthorizationStatusField;

        private VesselTransportMeansType[] specifiedVesselTransportMeansField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("VesselID")]
        public IDType[] VesselID
        {
            get
            {
                return this.vesselIDField;
            }
            set
            {
                this.vesselIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("JoinedVesselID")]
        public IDType[] JoinedVesselID
        {
            get
            {
                return this.joinedVesselIDField;
            }
            set
            {
                this.joinedVesselIDField = value;
            }
        }

        /// <remarks/>
        public TextType Name
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
        public IndicatorType FirstApplicationIndicator
        {
            get
            {
                return this.firstApplicationIndicatorField;
            }
            set
            {
                this.firstApplicationIndicatorField = value;
            }
        }

        /// <remarks/>
        public TextType Remarks
        {
            get
            {
                return this.remarksField;
            }
            set
            {
                this.remarksField = value;
            }
        }

        /// <remarks/>
        public DateTimeType DecisionDateTime
        {
            get
            {
                return this.decisionDateTimeField;
            }
            set
            {
                this.decisionDateTimeField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFLUXCharacteristic")]
        public FLUXCharacteristicType[] SpecifiedFLUXCharacteristic
        {
            get
            {
                return this.specifiedFLUXCharacteristicField;
            }
            set
            {
                this.specifiedFLUXCharacteristicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedVesselCrew")]
        public VesselCrewType[] SpecifiedVesselCrew
        {
            get
            {
                return this.specifiedVesselCrewField;
            }
            set
            {
                this.specifiedVesselCrewField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ApplicableDelimitedPeriod")]
        public DelimitedPeriodType[] ApplicableDelimitedPeriod
        {
            get
            {
                return this.applicableDelimitedPeriodField;
            }
            set
            {
                this.applicableDelimitedPeriodField = value;
            }
        }

        /// <remarks/>
        public DelimitedPeriodType EntryIntoForceDelimitedPeriod
        {
            get
            {
                return this.entryIntoForceDelimitedPeriodField;
            }
            set
            {
                this.entryIntoForceDelimitedPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedVesselTransportCharter")]
        public VesselTransportCharterType[] SpecifiedVesselTransportCharter
        {
            get
            {
                return this.specifiedVesselTransportCharterField;
            }
            set
            {
                this.specifiedVesselTransportCharterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedContactParty")]
        public ContactPartyType[] SpecifiedContactParty
        {
            get
            {
                return this.specifiedContactPartyField;
            }
            set
            {
                this.specifiedContactPartyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedTargetedQuota")]
        public TargetedQuotaType[] SpecifiedTargetedQuota
        {
            get
            {
                return this.specifiedTargetedQuotaField;
            }
            set
            {
                this.specifiedTargetedQuotaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AttachedFLUXBinaryFile")]
        public FLUXBinaryFileType[] AttachedFLUXBinaryFile
        {
            get
            {
                return this.attachedFLUXBinaryFileField;
            }
            set
            {
                this.attachedFLUXBinaryFileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFLUXLocation")]
        public FLUXLocationType[] SpecifiedFLUXLocation
        {
            get
            {
                return this.specifiedFLUXLocationField;
            }
            set
            {
                this.specifiedFLUXLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedValidationResultDocument")]
        public ValidationResultDocumentType[] RelatedValidationResultDocument
        {
            get
            {
                return this.relatedValidationResultDocumentField;
            }
            set
            {
                this.relatedValidationResultDocumentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedFLAPRequestDocument")]
        public FLAPRequestDocumentType[] RelatedFLAPRequestDocument
        {
            get
            {
                return this.relatedFLAPRequestDocumentField;
            }
            set
            {
                this.relatedFLAPRequestDocumentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedAuthorizationStatus")]
        public AuthorizationStatusType[] SpecifiedAuthorizationStatus
        {
            get
            {
                return this.specifiedAuthorizationStatusField;
            }
            set
            {
                this.specifiedAuthorizationStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedVesselTransportMeans")]
        public VesselTransportMeansType[] SpecifiedVesselTransportMeans
        {
            get
            {
                return this.specifiedVesselTransportMeansField;
            }
            set
            {
                this.specifiedVesselTransportMeansField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselCrewType
    {

        private CodeType typeCodeField;

        private QuantityType memberQuantityField;

        private QuantityType minimumSizeQuantityField;

        private QuantityType maximumSizeQuantityField;

        private QuantityType onDeckSizeQuantityField;

        private QuantityType aboveDeckSizeQuantityField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public QuantityType MemberQuantity
        {
            get
            {
                return this.memberQuantityField;
            }
            set
            {
                this.memberQuantityField = value;
            }
        }

        /// <remarks/>
        public QuantityType MinimumSizeQuantity
        {
            get
            {
                return this.minimumSizeQuantityField;
            }
            set
            {
                this.minimumSizeQuantityField = value;
            }
        }

        /// <remarks/>
        public QuantityType MaximumSizeQuantity
        {
            get
            {
                return this.maximumSizeQuantityField;
            }
            set
            {
                this.maximumSizeQuantityField = value;
            }
        }

        /// <remarks/>
        public QuantityType OnDeckSizeQuantity
        {
            get
            {
                return this.onDeckSizeQuantityField;
            }
            set
            {
                this.onDeckSizeQuantityField = value;
            }
        }

        /// <remarks/>
        public QuantityType AboveDeckSizeQuantity
        {
            get
            {
                return this.aboveDeckSizeQuantityField;
            }
            set
            {
                this.aboveDeckSizeQuantityField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselTransportCharterType
    {

        private CodeType typeCodeField;

        private DelimitedPeriodType[] applicableDelimitedPeriodField;

        private ContactPartyType[] specifiedContactPartyField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ApplicableDelimitedPeriod")]
        public DelimitedPeriodType[] ApplicableDelimitedPeriod
        {
            get
            {
                return this.applicableDelimitedPeriodField;
            }
            set
            {
                this.applicableDelimitedPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedContactParty")]
        public ContactPartyType[] SpecifiedContactParty
        {
            get
            {
                return this.specifiedContactPartyField;
            }
            set
            {
                this.specifiedContactPartyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class TargetedQuotaType
    {

        private CodeType typeCodeField;

        private CodeType objectCodeField;

        private MeasureType[] weightMeasureField;

        private QuantityType[] specifiedQuantityField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType ObjectCode
        {
            get
            {
                return this.objectCodeField;
            }
            set
            {
                this.objectCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("WeightMeasure")]
        public MeasureType[] WeightMeasure
        {
            get
            {
                return this.weightMeasureField;
            }
            set
            {
                this.weightMeasureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedQuantity")]
        public QuantityType[] SpecifiedQuantity
        {
            get
            {
                return this.specifiedQuantityField;
            }
            set
            {
                this.specifiedQuantityField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class ValidationResultDocumentType
    {

        private IDType validatorIDField;

        private DateTimeType creationDateTimeField;

        private ValidationQualityAnalysisType[] relatedValidationQualityAnalysisField;

        /// <remarks/>
        public IDType ValidatorID
        {
            get
            {
                return this.validatorIDField;
            }
            set
            {
                this.validatorIDField = value;
            }
        }

        /// <remarks/>
        public DateTimeType CreationDateTime
        {
            get
            {
                return this.creationDateTimeField;
            }
            set
            {
                this.creationDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedValidationQualityAnalysis")]
        public ValidationQualityAnalysisType[] RelatedValidationQualityAnalysis
        {
            get
            {
                return this.relatedValidationQualityAnalysisField;
            }
            set
            {
                this.relatedValidationQualityAnalysisField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class ValidationQualityAnalysisType
    {

        private CodeType levelCodeField;

        private CodeType typeCodeField;

        private TextType[] resultField;

        private IDType idField;

        private TextType descriptionField;

        private TextType[] referencedItemField;

        /// <remarks/>
        public CodeType LevelCode
        {
            get
            {
                return this.levelCodeField;
            }
            set
            {
                this.levelCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Result")]
        public TextType[] Result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public TextType Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReferencedItem")]
        public TextType[] ReferencedItem
        {
            get
            {
                return this.referencedItemField;
            }
            set
            {
                this.referencedItemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FLAPRequestDocumentType
    {

        private IDType idField;

        private CodeType typeCodeField;

        private CodeType fADATypeCodeField;

        private CodeType purposeCodeField;

        private TextType purposeField;

        private FishingCategoryType relatedFishingCategoryField;

        private ValidationResultDocumentType[] relatedValidationResultDocumentField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType FADATypeCode
        {
            get
            {
                return this.fADATypeCodeField;
            }
            set
            {
                this.fADATypeCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType PurposeCode
        {
            get
            {
                return this.purposeCodeField;
            }
            set
            {
                this.purposeCodeField = value;
            }
        }

        /// <remarks/>
        public TextType Purpose
        {
            get
            {
                return this.purposeField;
            }
            set
            {
                this.purposeField = value;
            }
        }

        /// <remarks/>
        public FishingCategoryType RelatedFishingCategory
        {
            get
            {
                return this.relatedFishingCategoryField;
            }
            set
            {
                this.relatedFishingCategoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedValidationResultDocument")]
        public ValidationResultDocumentType[] RelatedValidationResultDocument
        {
            get
            {
                return this.relatedValidationResultDocumentField;
            }
            set
            {
                this.relatedValidationResultDocumentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FishingCategoryType
    {

        private CodeType typeCodeField;

        private CodeType[] fishingMethodCodeField;

        private TextType[] fishingMethodField;

        private CodeType[] fishingAreaCodeField;

        private TextType[] fishingAreaField;

        private TextType specialConditionField;

        private FishingGearType[] authorizedFishingGearField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FishingMethodCode")]
        public CodeType[] FishingMethodCode
        {
            get
            {
                return this.fishingMethodCodeField;
            }
            set
            {
                this.fishingMethodCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FishingMethod")]
        public TextType[] FishingMethod
        {
            get
            {
                return this.fishingMethodField;
            }
            set
            {
                this.fishingMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FishingAreaCode")]
        public CodeType[] FishingAreaCode
        {
            get
            {
                return this.fishingAreaCodeField;
            }
            set
            {
                this.fishingAreaCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FishingArea")]
        public TextType[] FishingArea
        {
            get
            {
                return this.fishingAreaField;
            }
            set
            {
                this.fishingAreaField = value;
            }
        }

        /// <remarks/>
        public TextType SpecialCondition
        {
            get
            {
                return this.specialConditionField;
            }
            set
            {
                this.specialConditionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AuthorizedFishingGear")]
        public FishingGearType[] AuthorizedFishingGear
        {
            get
            {
                return this.authorizedFishingGearField;
            }
            set
            {
                this.authorizedFishingGearField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class AuthorizationStatusType
    {

        private CodeType conditionCodeField;

        private DateTimeType changedDateTimeField;

        private TextType[] descriptionField;

        /// <remarks/>
        public CodeType ConditionCode
        {
            get
            {
                return this.conditionCodeField;
            }
            set
            {
                this.conditionCodeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType ChangedDateTime
        {
            get
            {
                return this.changedDateTimeField;
            }
            set
            {
                this.changedDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class GearInspectionEventType
    {

        private IDType[] idField;

        private TextType[] descriptionField;

        private DateTimeType occurrenceDateTimeField;

        private DelimitedPeriodType occurrenceDelimitedPeriodField;

        private GearCharacteristicType[] relatedGearCharacteristicField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public DateTimeType OccurrenceDateTime
        {
            get
            {
                return this.occurrenceDateTimeField;
            }
            set
            {
                this.occurrenceDateTimeField = value;
            }
        }

        /// <remarks/>
        public DelimitedPeriodType OccurrenceDelimitedPeriod
        {
            get
            {
                return this.occurrenceDelimitedPeriodField;
            }
            set
            {
                this.occurrenceDelimitedPeriodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedGearCharacteristic")]
        public GearCharacteristicType[] RelatedGearCharacteristic
        {
            get
            {
                return this.relatedGearCharacteristicField;
            }
            set
            {
                this.relatedGearCharacteristicField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class GearProblemType
    {

        private CodeType typeCodeField;

        private QuantityType affectedQuantityField;

        private CodeType[] recoveryMeasureCodeField;

        private FLUXLocationType[] specifiedFLUXLocationField;

        private FishingGearType[] relatedFishingGearField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public QuantityType AffectedQuantity
        {
            get
            {
                return this.affectedQuantityField;
            }
            set
            {
                this.affectedQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RecoveryMeasureCode")]
        public CodeType[] RecoveryMeasureCode
        {
            get
            {
                return this.recoveryMeasureCodeField;
            }
            set
            {
                this.recoveryMeasureCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFLUXLocation")]
        public FLUXLocationType[] SpecifiedFLUXLocation
        {
            get
            {
                return this.specifiedFLUXLocationField;
            }
            set
            {
                this.specifiedFLUXLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedFishingGear")]
        public FishingGearType[] RelatedFishingGear
        {
            get
            {
                return this.relatedFishingGearField;
            }
            set
            {
                this.relatedFishingGearField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SalesBatchType
    {

        private IDType[] idField;

        private AAPProductType[] specifiedAAPProductField;

        private AmountType[] totalSalesPriceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedAAPProduct")]
        public AAPProductType[] SpecifiedAAPProduct
        {
            get
            {
                return this.specifiedAAPProductField;
            }
            set
            {
                this.specifiedAAPProductField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ChargeAmount", IsNullable = false)]
        public AmountType[] TotalSalesPrice
        {
            get
            {
                return this.totalSalesPriceField;
            }
            set
            {
                this.totalSalesPriceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FACatchType
    {

        private CodeType speciesCodeField;

        private QuantityType unitQuantityField;

        private MeasureType weightMeasureField;

        private CodeType weighingMeansCodeField;

        private CodeType usageCodeField;

        private CodeType typeCodeField;

        private NumericType toleranceMarginNumericField;

        private FishingTripType[] relatedFishingTripField;

        private SizeDistributionType specifiedSizeDistributionField;

        private AAPStockType[] relatedAAPStockField;

        private AAPProcessType[] appliedAAPProcessField;

        private SalesBatchType[] relatedSalesBatchField;

        private FLUXLocationType[] specifiedFLUXLocationField;

        private FishingGearType[] usedFishingGearField;

        private FLUXCharacteristicType[] applicableFLUXCharacteristicField;

        private FLUXLocationType[] destinationFLUXLocationField;

        private DelimitedPeriodType[] specifiedDelimitedPeriodField;

        /// <remarks/>
        public CodeType SpeciesCode
        {
            get
            {
                return this.speciesCodeField;
            }
            set
            {
                this.speciesCodeField = value;
            }
        }

        /// <remarks/>
        public QuantityType UnitQuantity
        {
            get
            {
                return this.unitQuantityField;
            }
            set
            {
                this.unitQuantityField = value;
            }
        }

        /// <remarks/>
        public MeasureType WeightMeasure
        {
            get
            {
                return this.weightMeasureField;
            }
            set
            {
                this.weightMeasureField = value;
            }
        }

        /// <remarks/>
        public CodeType WeighingMeansCode
        {
            get
            {
                return this.weighingMeansCodeField;
            }
            set
            {
                this.weighingMeansCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType UsageCode
        {
            get
            {
                return this.usageCodeField;
            }
            set
            {
                this.usageCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public NumericType ToleranceMarginNumeric
        {
            get
            {
                return this.toleranceMarginNumericField;
            }
            set
            {
                this.toleranceMarginNumericField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedFishingTrip")]
        public FishingTripType[] RelatedFishingTrip
        {
            get
            {
                return this.relatedFishingTripField;
            }
            set
            {
                this.relatedFishingTripField = value;
            }
        }

        /// <remarks/>
        public SizeDistributionType SpecifiedSizeDistribution
        {
            get
            {
                return this.specifiedSizeDistributionField;
            }
            set
            {
                this.specifiedSizeDistributionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedAAPStock")]
        public AAPStockType[] RelatedAAPStock
        {
            get
            {
                return this.relatedAAPStockField;
            }
            set
            {
                this.relatedAAPStockField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AppliedAAPProcess")]
        public AAPProcessType[] AppliedAAPProcess
        {
            get
            {
                return this.appliedAAPProcessField;
            }
            set
            {
                this.appliedAAPProcessField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedSalesBatch")]
        public SalesBatchType[] RelatedSalesBatch
        {
            get
            {
                return this.relatedSalesBatchField;
            }
            set
            {
                this.relatedSalesBatchField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFLUXLocation")]
        public FLUXLocationType[] SpecifiedFLUXLocation
        {
            get
            {
                return this.specifiedFLUXLocationField;
            }
            set
            {
                this.specifiedFLUXLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("UsedFishingGear")]
        public FishingGearType[] UsedFishingGear
        {
            get
            {
                return this.usedFishingGearField;
            }
            set
            {
                this.usedFishingGearField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ApplicableFLUXCharacteristic")]
        public FLUXCharacteristicType[] ApplicableFLUXCharacteristic
        {
            get
            {
                return this.applicableFLUXCharacteristicField;
            }
            set
            {
                this.applicableFLUXCharacteristicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DestinationFLUXLocation")]
        public FLUXLocationType[] DestinationFLUXLocation
        {
            get
            {
                return this.destinationFLUXLocationField;
            }
            set
            {
                this.destinationFLUXLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedDelimitedPeriod")]
        public DelimitedPeriodType[] SpecifiedDelimitedPeriod
        {
            get
            {
                return this.specifiedDelimitedPeriodField;
            }
            set
            {
                this.specifiedDelimitedPeriodField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FishingTripType
    {

        private IDType[] idField;

        private CodeType typeCodeField;

        private DelimitedPeriodType[] specifiedDelimitedPeriodField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedDelimitedPeriod")]
        public DelimitedPeriodType[] SpecifiedDelimitedPeriod
        {
            get
            {
                return this.specifiedDelimitedPeriodField;
            }
            set
            {
                this.specifiedDelimitedPeriodField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class SizeDistributionType
    {

        private CodeType categoryCodeField;

        private CodeType[] classCodeField;

        /// <remarks/>
        public CodeType CategoryCode
        {
            get
            {
                return this.categoryCodeField;
            }
            set
            {
                this.categoryCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ClassCode")]
        public CodeType[] ClassCode
        {
            get
            {
                return this.classCodeField;
            }
            set
            {
                this.classCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class AAPStockType
    {

        private IDType idField;

        /// <remarks/>
        public IDType ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class AAPProcessType
    {

        private CodeType[] typeCodeField;

        private NumericType conversionFactorNumericField;

        private FACatchType[] usedFACatchField;

        private AAPProductType[] resultAAPProductField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TypeCode")]
        public CodeType[] TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public NumericType ConversionFactorNumeric
        {
            get
            {
                return this.conversionFactorNumericField;
            }
            set
            {
                this.conversionFactorNumericField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("UsedFACatch")]
        public FACatchType[] UsedFACatch
        {
            get
            {
                return this.usedFACatchField;
            }
            set
            {
                this.usedFACatchField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ResultAAPProduct")]
        public AAPProductType[] ResultAAPProduct
        {
            get
            {
                return this.resultAAPProductField;
            }
            set
            {
                this.resultAAPProductField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class AAPProductType
    {

        private CodeType speciesCodeField;

        private QuantityType unitQuantityField;

        private MeasureType weightMeasureField;

        private CodeType weighingMeansCodeField;

        private CodeType usageCodeField;

        private QuantityType packagingUnitQuantityField;

        private CodeType packagingTypeCodeField;

        private MeasureType packagingUnitAverageWeightMeasureField;

        private AAPProcessType[] appliedAAPProcessField;

        private AmountType[] totalSalesPriceField;

        private SizeDistributionType specifiedSizeDistributionField;

        private FLUXLocationType[] originFLUXLocationField;

        private FishingActivityType originFishingActivityField;

        /// <remarks/>
        public CodeType SpeciesCode
        {
            get
            {
                return this.speciesCodeField;
            }
            set
            {
                this.speciesCodeField = value;
            }
        }

        /// <remarks/>
        public QuantityType UnitQuantity
        {
            get
            {
                return this.unitQuantityField;
            }
            set
            {
                this.unitQuantityField = value;
            }
        }

        /// <remarks/>
        public MeasureType WeightMeasure
        {
            get
            {
                return this.weightMeasureField;
            }
            set
            {
                this.weightMeasureField = value;
            }
        }

        /// <remarks/>
        public CodeType WeighingMeansCode
        {
            get
            {
                return this.weighingMeansCodeField;
            }
            set
            {
                this.weighingMeansCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType UsageCode
        {
            get
            {
                return this.usageCodeField;
            }
            set
            {
                this.usageCodeField = value;
            }
        }

        /// <remarks/>
        public QuantityType PackagingUnitQuantity
        {
            get
            {
                return this.packagingUnitQuantityField;
            }
            set
            {
                this.packagingUnitQuantityField = value;
            }
        }

        /// <remarks/>
        public CodeType PackagingTypeCode
        {
            get
            {
                return this.packagingTypeCodeField;
            }
            set
            {
                this.packagingTypeCodeField = value;
            }
        }

        /// <remarks/>
        public MeasureType PackagingUnitAverageWeightMeasure
        {
            get
            {
                return this.packagingUnitAverageWeightMeasureField;
            }
            set
            {
                this.packagingUnitAverageWeightMeasureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AppliedAAPProcess")]
        public AAPProcessType[] AppliedAAPProcess
        {
            get
            {
                return this.appliedAAPProcessField;
            }
            set
            {
                this.appliedAAPProcessField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ChargeAmount", IsNullable = false)]
        public AmountType[] TotalSalesPrice
        {
            get
            {
                return this.totalSalesPriceField;
            }
            set
            {
                this.totalSalesPriceField = value;
            }
        }

        /// <remarks/>
        public SizeDistributionType SpecifiedSizeDistribution
        {
            get
            {
                return this.specifiedSizeDistributionField;
            }
            set
            {
                this.specifiedSizeDistributionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("OriginFLUXLocation")]
        public FLUXLocationType[] OriginFLUXLocation
        {
            get
            {
                return this.originFLUXLocationField;
            }
            set
            {
                this.originFLUXLocationField = value;
            }
        }

        /// <remarks/>
        public FishingActivityType OriginFishingActivity
        {
            get
            {
                return this.originFishingActivityField;
            }
            set
            {
                this.originFishingActivityField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FishingActivityType
    {

        private IDType[] idField;

        private CodeType typeCodeField;

        private DateTimeType occurrenceDateTimeField;

        private CodeType reasonCodeField;

        private CodeType vesselRelatedActivityCodeField;

        private CodeType fisheryTypeCodeField;

        private CodeType speciesTargetCodeField;

        private QuantityType operationsQuantityField;

        private MeasureType fishingDurationMeasureField;

        private FACatchType[] specifiedFACatchField;

        private FLUXLocationType[] relatedFLUXLocationField;

        private GearProblemType[] specifiedGearProblemField;

        private FLUXCharacteristicType[] specifiedFLUXCharacteristicField;

        private FishingGearType[] specifiedFishingGearField;

        private VesselStorageCharacteristicType sourceVesselStorageCharacteristicField;

        private VesselStorageCharacteristicType destinationVesselStorageCharacteristicField;

        private FishingActivityType[] relatedFishingActivityField;

        private FLAPDocumentType[] specifiedFLAPDocumentField;

        private DelimitedPeriodType[] specifiedDelimitedPeriodField;

        private FishingTripType specifiedFishingTripField;

        private VesselTransportMeansType[] relatedVesselTransportMeansField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType OccurrenceDateTime
        {
            get
            {
                return this.occurrenceDateTimeField;
            }
            set
            {
                this.occurrenceDateTimeField = value;
            }
        }

        /// <remarks/>
        public CodeType ReasonCode
        {
            get
            {
                return this.reasonCodeField;
            }
            set
            {
                this.reasonCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType VesselRelatedActivityCode
        {
            get
            {
                return this.vesselRelatedActivityCodeField;
            }
            set
            {
                this.vesselRelatedActivityCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType FisheryTypeCode
        {
            get
            {
                return this.fisheryTypeCodeField;
            }
            set
            {
                this.fisheryTypeCodeField = value;
            }
        }

        /// <remarks/>
        public CodeType SpeciesTargetCode
        {
            get
            {
                return this.speciesTargetCodeField;
            }
            set
            {
                this.speciesTargetCodeField = value;
            }
        }

        /// <remarks/>
        public QuantityType OperationsQuantity
        {
            get
            {
                return this.operationsQuantityField;
            }
            set
            {
                this.operationsQuantityField = value;
            }
        }

        /// <remarks/>
        public MeasureType FishingDurationMeasure
        {
            get
            {
                return this.fishingDurationMeasureField;
            }
            set
            {
                this.fishingDurationMeasureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFACatch")]
        public FACatchType[] SpecifiedFACatch
        {
            get
            {
                return this.specifiedFACatchField;
            }
            set
            {
                this.specifiedFACatchField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedFLUXLocation")]
        public FLUXLocationType[] RelatedFLUXLocation
        {
            get
            {
                return this.relatedFLUXLocationField;
            }
            set
            {
                this.relatedFLUXLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedGearProblem")]
        public GearProblemType[] SpecifiedGearProblem
        {
            get
            {
                return this.specifiedGearProblemField;
            }
            set
            {
                this.specifiedGearProblemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFLUXCharacteristic")]
        public FLUXCharacteristicType[] SpecifiedFLUXCharacteristic
        {
            get
            {
                return this.specifiedFLUXCharacteristicField;
            }
            set
            {
                this.specifiedFLUXCharacteristicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFishingGear")]
        public FishingGearType[] SpecifiedFishingGear
        {
            get
            {
                return this.specifiedFishingGearField;
            }
            set
            {
                this.specifiedFishingGearField = value;
            }
        }

        /// <remarks/>
        public VesselStorageCharacteristicType SourceVesselStorageCharacteristic
        {
            get
            {
                return this.sourceVesselStorageCharacteristicField;
            }
            set
            {
                this.sourceVesselStorageCharacteristicField = value;
            }
        }

        /// <remarks/>
        public VesselStorageCharacteristicType DestinationVesselStorageCharacteristic
        {
            get
            {
                return this.destinationVesselStorageCharacteristicField;
            }
            set
            {
                this.destinationVesselStorageCharacteristicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedFishingActivity")]
        public FishingActivityType[] RelatedFishingActivity
        {
            get
            {
                return this.relatedFishingActivityField;
            }
            set
            {
                this.relatedFishingActivityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFLAPDocument")]
        public FLAPDocumentType[] SpecifiedFLAPDocument
        {
            get
            {
                return this.specifiedFLAPDocumentField;
            }
            set
            {
                this.specifiedFLAPDocumentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedDelimitedPeriod")]
        public DelimitedPeriodType[] SpecifiedDelimitedPeriod
        {
            get
            {
                return this.specifiedDelimitedPeriodField;
            }
            set
            {
                this.specifiedDelimitedPeriodField = value;
            }
        }

        /// <remarks/>
        public FishingTripType SpecifiedFishingTrip
        {
            get
            {
                return this.specifiedFishingTripField;
            }
            set
            {
                this.specifiedFishingTripField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedVesselTransportMeans")]
        public VesselTransportMeansType[] RelatedVesselTransportMeans
        {
            get
            {
                return this.relatedVesselTransportMeansField;
            }
            set
            {
                this.relatedVesselTransportMeansField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FLUXReportDocumentType
    {

        private IDType[] idField;

        private IDType referencedIDField;

        private DateTimeType creationDateTimeField;

        private CodeType purposeCodeField;

        private TextType purposeField;

        private CodeType typeCodeField;

        private FLUXPartyType ownerFLUXPartyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public IDType ReferencedID
        {
            get
            {
                return this.referencedIDField;
            }
            set
            {
                this.referencedIDField = value;
            }
        }

        /// <remarks/>
        public DateTimeType CreationDateTime
        {
            get
            {
                return this.creationDateTimeField;
            }
            set
            {
                this.creationDateTimeField = value;
            }
        }

        /// <remarks/>
        public CodeType PurposeCode
        {
            get
            {
                return this.purposeCodeField;
            }
            set
            {
                this.purposeCodeField = value;
            }
        }

        /// <remarks/>
        public TextType Purpose
        {
            get
            {
                return this.purposeField;
            }
            set
            {
                this.purposeField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public FLUXPartyType OwnerFLUXParty
        {
            get
            {
                return this.ownerFLUXPartyField;
            }
            set
            {
                this.ownerFLUXPartyField = value;
            }
        }
    }



    /// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public  class FLUXReportDocumentType1
    {

        private IDType[] idField;

        private IDType referencedIDField;

        private DateTimeType creationDateTimeField;

        private CodeType purposeCodeField;

        private TextType purposeField;

        private CodeType typeCodeField;

        private FLUXPartyType ownerFLUXPartyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public IDType ReferencedID
        {
            get
            {
                return this.referencedIDField;
            }
            set
            {
                this.referencedIDField = value;
            }
        }

        /// <remarks/>
        public DateTimeType CreationDateTime
        {
            get
            {
                return this.creationDateTimeField;
            }
            set
            {
                this.creationDateTimeField = value;
            }
        }

        /// <remarks/>
        public CodeType PurposeCode
        {
            get
            {
                return this.purposeCodeField;
            }
            set
            {
                this.purposeCodeField = value;
            }
        }

        /// <remarks/>
        public TextType Purpose
        {
            get
            {
                return this.purposeField;
            }
            set
            {
                this.purposeField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        public FLUXPartyType OwnerFLUXParty
        {
            get
            {
                return this.ownerFLUXPartyField;
            }
            set
            {
                this.ownerFLUXPartyField = value;
            }
        }
    }


    /// <summary>
    /// FA Report Document
    /// </summary>
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class FAReportDocumentType
    {
        #region fields
        private CodeType typeCodeField;

        private CodeType fMCMarkerCodeField;

        private IDType[] relatedReportIDField;

        private DateTimeType acceptanceDateTimeField;

        private FLUXReportDocumentType relatedFLUXReportDocumentField;

        private FishingActivityType[] specifiedFishingActivityField;

        private VesselTransportMeansType specifiedVesselTransportMeansField;
        #endregion

        /// <summary>
        /// Type - FLUX_FA_REPORT_TYPE
        /// <ram:TypeCode listID="FLUX_FA_REPORT_TYPE">DECLARATION</ram:TypeCode>
        /// </summary>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }
         
        /// <summary>
        /// FMC_Marker - FLUX_FA_FMC - optional
        /// </summary>
        public CodeType FMCMarkerCode
        {
            get
            {
                return this.fMCMarkerCodeField;
            }
            set
            {
                this.fMCMarkerCodeField = value;
            }
        }

        /// <summary>
        /// Related_Report Identification - optional
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("RelatedReportID")]
        public IDType[] RelatedReportID
        {
            get
            {
                return this.relatedReportIDField;
            }
            set
            {
                this.relatedReportIDField = value;
            }
        }

        /// <summary>
        /// Acceptance - DateTime - mandatory
        /// <ram:AcceptanceDateTime>
        ///     <udt:DateTime>2020-05-06T13:39:33.176Z</udt:DateTime>
        /// </ram:AcceptanceDateTime>
        /// </summary>
        public DateTimeType AcceptanceDateTime
        {
            get
            {
                return this.acceptanceDateTimeField;
            }
            set
            {
                this.acceptanceDateTimeField = value;
            }
        }

        /// <summary>
        /// RelatedFluxReport_Document - mandatory 
        /// </summary>
        public FLUXReportDocumentType RelatedFLUXReportDocument
        {
            get
            {
                return this.relatedFLUXReportDocumentField;
            }
            set
            {
                this.relatedFLUXReportDocumentField = value;
            }
        }

        /// <summary>
        /// SpecifiedFishing_Activity
        /// Departure etc.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("SpecifiedFishingActivity")]
        public FishingActivityType[] SpecifiedFishingActivity
        {
            get
            {
                return this.specifiedFishingActivityField;
            }
            set
            {
                this.specifiedFishingActivityField = value;
            }
        }

        /// <summary>
        /// SpecifiedVessel_TransportMeans - optional in case of deletion or cancellation report, otherwise mandatory
        /// </summary>
        public VesselTransportMeansType SpecifiedVesselTransportMeans
        {
            get
            {
                return this.specifiedVesselTransportMeansField;
            }
            set
            {
                this.specifiedVesselTransportMeansField = value;
            }
        }
    }

    
    /// <summary>
    /// 
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselHistoricalCharacteristicType
    {

        private CodeType typeCodeField;

        private TextType[] descriptionField;

        private TextType valueField;

        private IndicatorType valueIndicatorField;

        private CodeType valueCodeField;

        private DateTimeType valueDateTimeField;

        private MeasureType valueMeasureField;

        private QuantityType valueQuantityField;

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public TextType Value
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
        public IndicatorType ValueIndicator
        {
            get
            {
                return this.valueIndicatorField;
            }
            set
            {
                this.valueIndicatorField = value;
            }
        }

        /// <remarks/>
        public CodeType ValueCode
        {
            get
            {
                return this.valueCodeField;
            }
            set
            {
                this.valueCodeField = value;
            }
        }

        /// <remarks/>
        public DateTimeType ValueDateTime
        {
            get
            {
                return this.valueDateTimeField;
            }
            set
            {
                this.valueDateTimeField = value;
            }
        }

        /// <remarks/>
        public MeasureType ValueMeasure
        {
            get
            {
                return this.valueMeasureField;
            }
            set
            {
                this.valueMeasureField = value;
            }
        }

        /// <remarks/>
        public QuantityType ValueQuantity
        {
            get
            {
                return this.valueQuantityField;
            }
            set
            {
                this.valueQuantityField = value;
            }
        }
    }

    /// <summary>
    /// remarks from FLAP
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20" +
        "")]
    public partial class VesselEventType
    {

        private IDType vesselIDField;

        private CodeType typeCodeField;

        private TextType[] descriptionField;

        private DateTimeType occurrenceDateTimeField;

        private IDType[] idField;

        private DelimitedPeriodType discreteDelimitedPeriodField;

        private ValidationResultDocumentType relatedValidationResultDocumentField;

        private VesselHistoricalCharacteristicType[] relatedVesselHistoricalCharacteristicField;

        private VesselTransportMeansType relatedVesselTransportMeansField;

        private ValidationQualityAnalysisType[] relatedValidationQualityAnalysisField;

        /// <remarks/>
        public IDType VesselID
        {
            get
            {
                return this.vesselIDField;
            }
            set
            {
                this.vesselIDField = value;
            }
        }

        /// <remarks/>
        public CodeType TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public TextType[] Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public DateTimeType OccurrenceDateTime
        {
            get
            {
                return this.occurrenceDateTimeField;
            }
            set
            {
                this.occurrenceDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ID")]
        public IDType[] ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public DelimitedPeriodType DiscreteDelimitedPeriod
        {
            get
            {
                return this.discreteDelimitedPeriodField;
            }
            set
            {
                this.discreteDelimitedPeriodField = value;
            }
        }

        /// <remarks/>
        public ValidationResultDocumentType RelatedValidationResultDocument
        {
            get
            {
                return this.relatedValidationResultDocumentField;
            }
            set
            {
                this.relatedValidationResultDocumentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedVesselHistoricalCharacteristic")]
        public VesselHistoricalCharacteristicType[] RelatedVesselHistoricalCharacteristic
        {
            get
            {
                return this.relatedVesselHistoricalCharacteristicField;
            }
            set
            {
                this.relatedVesselHistoricalCharacteristicField = value;
            }
        }

        /// <remarks/>
        public VesselTransportMeansType RelatedVesselTransportMeans
        {
            get
            {
                return this.relatedVesselTransportMeansField;
            }
            set
            {
                this.relatedVesselTransportMeansField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RelatedValidationQualityAnalysis")]
        public ValidationQualityAnalysisType[] RelatedValidationQualityAnalysis
        {
            get
            {
                return this.relatedValidationQualityAnalysisField;
            }
            set
            {
                this.relatedValidationQualityAnalysisField = value;
            }
        }
    }

 
}
