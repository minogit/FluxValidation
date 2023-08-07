using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISS
{





    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Placemark
    {

        private string nameField;

        private object descriptionField;

        private PlacemarkStyle styleField;

        private PlacemarkPolygon polygonField;

        /// <remarks/>
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
        public object description
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
        public PlacemarkStyle Style
        {
            get
            {
                return this.styleField;
            }
            set
            {
                this.styleField = value;
            }
        }

        /// <remarks/>
        public PlacemarkPolygon Polygon
        {
            get
            {
                return this.polygonField;
            }
            set
            {
                this.polygonField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlacemarkStyle
    {

        private PlacemarkStyleLineStyle lineStyleField;

        private PlacemarkStylePolyStyle polyStyleField;

        /// <remarks/>
        public PlacemarkStyleLineStyle LineStyle
        {
            get
            {
                return this.lineStyleField;
            }
            set
            {
                this.lineStyleField = value;
            }
        }

        /// <remarks/>
        public PlacemarkStylePolyStyle PolyStyle
        {
            get
            {
                return this.polyStyleField;
            }
            set
            {
                this.polyStyleField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlacemarkStyleLineStyle
    {

        private string colorField;

        private decimal widthField;

        /// <remarks/>
        public string color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }

        /// <remarks/>
        public decimal width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlacemarkStylePolyStyle
    {

        private string colorField;

        /// <remarks/>
        public string color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlacemarkPolygon
    {

        private PlacemarkPolygonOuterBoundaryIs outerBoundaryIsField;

        /// <remarks/>
        public PlacemarkPolygonOuterBoundaryIs outerBoundaryIs
        {
            get
            {
                return this.outerBoundaryIsField;
            }
            set
            {
                this.outerBoundaryIsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlacemarkPolygonOuterBoundaryIs
    {

        private PlacemarkPolygonOuterBoundaryIsLinearRing linearRingField;

        /// <remarks/>
        public PlacemarkPolygonOuterBoundaryIsLinearRing LinearRing
        {
            get
            {
                return this.linearRingField;
            }
            set
            {
                this.linearRingField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlacemarkPolygonOuterBoundaryIsLinearRing
    {

        private string coordinatesField;

        /// <remarks/>
        public string coordinates
        {
            get
            {
                return this.coordinatesField;
            }
            set
            {
                this.coordinatesField = value;
            }
        }
    }
}
