using System.Xml.Serialization;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.FileManagement;

namespace BlauwtipjeApp.Core.Models.Tree
{
    public class Image : IEquivalence<Resource>, IEquivalence<Image>
    {
        [XmlAttribute(AttributeName = "id")]
        public int XmlId { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "extension")]
        public string Extension { get; set; }

        [XmlIgnore]
        public string Filename => Name + "." + Extension;

        public Image()
        {
        }

        [XmlIgnore]
        public byte[] Content { get; set; }


        public bool IsEquivalentTo(Resource other)
        {
            if (other == null) return false;
            return (Filename.Equals(other.Name) && Content == other.Content);
        }

        public bool IsEquivalentTo(Image other)
        {
            if (object.ReferenceEquals(this, other)) return true;
            if (other == null) return false;
            return (XmlId == other.XmlId && Filename.Equals(other.Filename) && Content == other.Content);
        }
    }
}
