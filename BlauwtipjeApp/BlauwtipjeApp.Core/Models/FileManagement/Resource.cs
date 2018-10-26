using System.IO;
using System.Text;
using System.Xml.Serialization;
using SQLite;

namespace BlauwtipjeApp.Core.Models.FileManagement
{
    [Table("resources")]
    public class Resource
    {
        [XmlIgnore]
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [XmlIgnore]
        [Unique, Column("name")]
        public string Name { get; set; }

        [XmlIgnore]
        [Column("content")]
        public byte[] Content { get; set; }

        public string GetContentAsString()
        {
            return Encoding.UTF8.GetString(Content);
        }

        public void SetContentFromString(string content)
        {
            Content = Encoding.UTF8.GetBytes(content);
        }

        public Stream GetContentAsStream()
        {
            return new MemoryStream(Content);
        }

        [XmlIgnore]
        [Column("etag")]
        public string Etag { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Resource);
        }

        public bool Equals(Resource obj)
        {
            return obj != null
                   && base.Equals(obj)
                   && obj.Name == Name
                   && obj.Etag == Etag
                   && obj.Content == Content;
        }


    }
}
