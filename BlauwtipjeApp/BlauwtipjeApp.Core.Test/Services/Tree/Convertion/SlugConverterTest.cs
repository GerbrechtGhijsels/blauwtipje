using System;
using System.Collections.Generic;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Services.Tree.Converters.Xml;
using BlauwtipjeApp.Core.Services.Tree.Converters.Xml.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Services.Tree.Convertion
{
    [TestClass]
    public class SlugConverterTest
    {
        private Slug _testSlug;
        private string _testSlugXml;
        private DeterminationTree<Slug> _testSlugTree;

        [TestInitialize]
        public void TestInitialize()
        {
            _testSlug = new Slug
            {
                DisplayName = "Slak 1",
                ScientificName = "Cochlea unum",
                Length = "1 mm",
                Food = "Zeewier"
            };

            _testSlugXml = "<?xml version='1.0' encoding='utf-8'?>" + Environment.NewLine +
                            "<data>" + Environment.NewLine +
                            "\t<questions />" + Environment.NewLine +
                            "\t<results>" + Environment.NewLine +
                            "\t\t<slug " + 
                            "displayname='" + _testSlug.DisplayName + "' " + 
                            "scientificname='" + _testSlug.ScientificName + "' " +
                            "length='" + _testSlug.Length + 
                            "'>" + Environment.NewLine +
                            "\t\t\t<food>" + _testSlug.Food + "</food>" + Environment.NewLine +
                            "\t\t</slug>" + Environment.NewLine +
                            "\t</results>" + Environment.NewLine +
                            "\t<images />" + Environment.NewLine +
                            "</data>";

            _testSlugTree = new DeterminationTree<Slug>()
            {
                Results = new List<Slug>()
                {
                    _testSlug
                }
            };
        }

        [TestMethod]
        public void ReadSlugTest()
        {
            // Arrange
            var xml = _testSlugXml;
            var converter = new XmlTreeConverter<Slug>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings());

            //Act
            var tree = converter.FromXml(xml);

            // Assert
            Assert.AreEqual(1, tree.Results.Count);
            Assert.IsTrue(tree.Results[0].Equals(_testSlug));
        }

        

        [TestMethod]
        public void WriteSlugTest()
        {
            // Arrange
            var filledTree = _testSlugTree;
            var expected = _testSlugXml;
            var converter = new XmlTreeConverter<Slug>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings());

            //Act
            converter.ToXml(filledTree, out var actual);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
