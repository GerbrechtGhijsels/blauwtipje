using System.Xml.Serialization;
using BlauwtipjeApp.Core.Models.Tree.Determination;

namespace BlauwtipjeApp.Core.Models.Results
{
    [XmlType(TypeName = "slug")]
    public class Slug : Result
    {
        [XmlAttribute(AttributeName = "displayname")]
        public string DisplayName { get; set; }

        [XmlAttribute(AttributeName = "scientificname")]
        public string ScientificName { get; set; }

        [XmlAttribute(AttributeName = "length")]
        public string Length { get; set; }

        [XmlElement(ElementName = "food")]
        public string Food { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Slug);
        }

        public bool Equals(Slug obj)
        {
            return obj != null
                   && base.Equals(obj)
                   && obj.DisplayName == this.DisplayName
                   && obj.ScientificName == this.ScientificName
                   && obj.Length == this.Length
                   && obj.Food == this.Food;
        }
    }
}
