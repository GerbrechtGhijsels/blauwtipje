using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BlauwtipjeApp.Core.Services.Tree.Converters.Xml
{
    public class XmlConverterSettings
    {
        public XmlSerializerNamespaces Namespaces { get; set; }
        public XmlReaderSettings ReaderSettings { get; set; }
        public XmlWriterSettings WriterSettings { get; set; }
        public Encoding Encoding { get; set; }

        public static XmlConverterSettings GetBlauwtipjeXmlConverterSettings()
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);
            return new XmlConverterSettings
            {
                Namespaces = namespaces,
                ReaderSettings = new XmlReaderSettings()
                {
                    IgnoreWhitespace = true
                },
                WriterSettings = new XmlWriterSettings
                {
                    IndentChars = "\t",
                    Indent = true
                },
                Encoding = Encoding.UTF8
            };
        }
    }
}
