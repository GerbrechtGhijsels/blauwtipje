using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;

namespace BlauwtipjeApp.Core.Services.Tree.Converters.Xml.Impl
{
    public class XmlTreeConverter<TResult> : IXmlTreeConverter<TResult> where TResult : Result
    {
        private XmlConverterSettings settings;
        public XmlTreeConverter(XmlConverterSettings settings)
        {
            this.settings = settings;
        }

        public DeterminationTree<TResult> FromXml(string xml)
        {
            var serializer = new XmlSerializer(typeof(DeterminationTree<TResult>));

            xml = RemoveByteOrderMarkCharacter(xml);

            DeterminationTree<TResult> tree;
            using (var stringReader = new StringReader(xml))
            using (var reader = XmlReader.Create(stringReader, settings.ReaderSettings))
            {
                tree = (DeterminationTree<TResult>)serializer.Deserialize(reader);
            }
            return tree;
        }

        /// <summary>
        /// Removes the byte order mark character at the start of the xml.
        /// More info can be found here: https://stackoverflow.com/a/27743515/8633753
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        private string RemoveByteOrderMarkCharacter(string xml)
        {
            var byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());

            if (xml.StartsWith(byteOrderMarkUtf8, StringComparison.Ordinal))
            {
                xml = xml.Remove(0, byteOrderMarkUtf8.Length);
            }

            return xml;
        }

        public void ToXml(DeterminationTree<TResult> tree, out string output)
        {
            var serializer = new XmlSerializer(typeof(DeterminationTree<TResult>));

            using (StringWriterWithEncoding sw = new StringWriterWithEncoding(settings.Encoding))
            using (XmlWriter xw = XmlWriter.Create(sw, settings.WriterSettings))
            {
                serializer.Serialize(xw, tree, settings.Namespaces);
                output = sw.ToString().Replace('\"', '\'');
            }
        }
    }
}
