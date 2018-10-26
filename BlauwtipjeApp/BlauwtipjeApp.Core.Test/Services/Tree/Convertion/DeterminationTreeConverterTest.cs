using System;
using System.Collections.Generic;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Services.Tree.Converters.Xml;
using BlauwtipjeApp.Core.Services.Tree.Converters.Xml.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Services.Tree.Convertion
{
    [TestClass]
    public class DeterminationTreeConverterTest
    {
        #region Empty Tree
        private readonly string EmptyTreeXml =
            "<?xml version='1.0' encoding='utf-8'?>" + Environment.NewLine +
            "<data>" + Environment.NewLine +
            "\t<questions />" + Environment.NewLine +
            "\t<results />" + Environment.NewLine +
            "\t<images />" + Environment.NewLine +
            "</data>";

        [TestMethod]
        public void ReadEmptyTreeTest()
        {
            // Arrange
            var xml = EmptyTreeXml;
            var converter = new XmlTreeConverter<Result>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings());

            //Act
            var tree = converter.FromXml(xml);

            // Assert
            Assert.AreEqual(tree.Questions.Count, 0);
            Assert.AreEqual(tree.Results.Count, 0);
        }

        [TestMethod]
        public void WriteEmptyTreeTest()
        {
            // Arrange
            var filledTree = new DeterminationTree<Result>();
            var expected = EmptyTreeXml;
            var converter = new XmlTreeConverter<Result>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings());

            //Act
            converter.ToXml(filledTree, out var actual);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Question
        private Question _question;
        private string _questionXml;
        private DeterminationTree<Result> _questionTree;

        private void SetupQuestionTestData()
        {
            _question = new Question
            {
                Id = 1,
                Text = "TestText",
                Note = "TestNote",
                ImageIdList = new List<int>()
                {
                    1
                },
                Choices = new List<Choice>()
                {
                    new Choice()
                }
            };

            _questionXml = "<?xml version='1.0' encoding='utf-8'?>" + Environment.NewLine +
                           "<data>" + Environment.NewLine +
                           "\t<questions>" + Environment.NewLine +
                           "\t\t<question id='" + _question.Id + "'>" + Environment.NewLine +
                           "\t\t\t<text>" + _question.Text + "</text>" + Environment.NewLine +
                           "\t\t\t<note>" + _question.Note + "</note>" + Environment.NewLine +
                           "\t\t\t<images>" + Environment.NewLine +
                           "\t\t\t\t<image>" + _question.ImageIdList[0] + "</image>" + Environment.NewLine +
                           "\t\t\t</images>" + Environment.NewLine +
                           "\t\t\t<choices>" + Environment.NewLine +
                           "\t\t\t\t<choice />" + Environment.NewLine +
                           "\t\t\t</choices>" + Environment.NewLine +
                           "\t\t</question>" + Environment.NewLine +
                           "\t</questions>" + Environment.NewLine +
                           "\t<results />" + Environment.NewLine +
                           "\t<images />" + Environment.NewLine +
                           "</data>";

            _questionTree = new DeterminationTree<Result>()
            {
                Questions = new List<Question>
                {
                    _question,
                }
            };
        }

        [TestMethod]
        public void ReadQuestionTest()
        {
            // Arrange
            SetupQuestionTestData();
            var xml = _questionXml;
            var converter = new XmlTreeConverter<Result>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings());

            //Act
            var tree = converter.FromXml(xml);

            // Assert
            Assert.AreEqual(1, tree.Questions.Count);
            Assert.IsTrue(_question.Equals(tree.Questions[0]));
        }

        [TestMethod]
        public void WriteQuestionTest()
        {
            // Arrange
            SetupQuestionTestData();
            var filledTree = _questionTree;
            var expected = _questionXml;
            var converter = new XmlTreeConverter<Result>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings());

            //Act
            converter.ToXml(filledTree, out var actual);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Choice
        private Choice _choice;
        private string _choiceXml;
        private DeterminationTree<Result> _choiceTree;

        private void SetupChoiceTestData()
        {
            _choice = new Choice
            {
                Id = 1,
                Text = "TestText",
                Note = "TestNote",
                ImageIdList = new List<int>()
                {
                    1
                },
                NextQuestionId = 1,
                NextResultId = 1
            };

            _choiceXml = "<?xml version='1.0' encoding='utf-8'?>" + Environment.NewLine +
                           "<data>" + Environment.NewLine +
                           "\t<questions>" + Environment.NewLine +
                           "\t\t<question>" + Environment.NewLine +
                           "\t\t\t<choices>" + Environment.NewLine +
                           "\t\t\t\t<choice id='" + _choice.Id + "' nextQuestionId='" + _choice.NextQuestionId + "' nextResultId='" + _choice.NextResultId + "'>" + Environment.NewLine +
                           "\t\t\t\t\t<text>" + _choice.Text + "</text>" + Environment.NewLine +
                           "\t\t\t\t\t<note>" + _choice.Note + "</note>" + Environment.NewLine +
                           "\t\t\t\t\t<images>" + Environment.NewLine +
                           "\t\t\t\t\t\t<image>" + _choice.ImageIdList[0] + "</image>" + Environment.NewLine +
                           "\t\t\t\t\t</images>" + Environment.NewLine +
                           "\t\t\t\t</choice>" + Environment.NewLine +
                           "\t\t\t</choices>" + Environment.NewLine +
                           "\t\t</question>" + Environment.NewLine +
                           "\t</questions>" + Environment.NewLine +
                           "\t<results />" + Environment.NewLine +
                           "\t<images />" + Environment.NewLine +
                           "</data>";

            _choiceTree = new DeterminationTree<Result>()
            {
                Questions = new List<Question>
                {
                    new Question()
                    {
                        Choices = new List<Choice>
                        {
                            _choice
                        }
                    }
                }
            };
        }

        [TestMethod]
        public void ReadChoiceTest()
        {
            // Arrange
            SetupChoiceTestData();
            var xml = _choiceXml;
            var converter = new XmlTreeConverter<Result>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings());

            //Act
            var tree = converter.FromXml(xml);

            // Assert
            Assert.AreEqual(1, tree.Questions[0].Choices.Count);
            Assert.IsTrue(_choice.Equals(tree.Questions[0].Choices[0]));
        }

        [TestMethod]
        public void WriteChoiceTest()
        {
            // Arrange
            SetupChoiceTestData();
            var filledTree = _choiceTree;
            var expected = _choiceXml;
            var converter = new XmlTreeConverter<Result>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings());

            //Act
            converter.ToXml(filledTree, out var actual);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Result
        private Result _result;
        private string _resultXml;
        private DeterminationTree<Result> _resultTree;

        private void SetupResultTestData()
        {
            _result = new Result
            {
                Id = 1,
                Text = "TestText",
                Note = "TestNote",
                ImageIdList = new List<int>()
                {
                    1
                }
            };

            _resultXml = "<?xml version='1.0' encoding='utf-8'?>" + Environment.NewLine +
                           "<data>" + Environment.NewLine +
                           "\t<questions />" + Environment.NewLine +
                           "\t<results>" + Environment.NewLine +
                           "\t\t<result id='" + _result.Id + "'>" + Environment.NewLine +
                           "\t\t\t<text>" + _result.Text + "</text>" + Environment.NewLine +
                           "\t\t\t<note>" + _result.Note + "</note>" + Environment.NewLine +
                           "\t\t\t<images>" + Environment.NewLine +
                           "\t\t\t\t<image>" + _result.ImageIdList[0] + "</image>" + Environment.NewLine +
                           "\t\t\t</images>" + Environment.NewLine +
                           "\t\t</result>" + Environment.NewLine +
                           "\t</results>" + Environment.NewLine +
                           "\t<images />" + Environment.NewLine +
                           "</data>";

            _resultTree = new DeterminationTree<Result>()
            {
                Results = new List<Result>
                {
                    _result
                }
            };
        }

        [TestMethod]
        public void ReadResultTest()
        {
            // Arrange
            SetupResultTestData();
            var xml = _resultXml;
            var converter = new XmlTreeConverter<Result>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings());

            //Act
            var tree = converter.FromXml(xml);

            // Assert
            Assert.AreEqual(1, tree.Results.Count);
            Assert.IsTrue(_result.Equals(tree.Results[0]));
        }

        [TestMethod]
        public void WriteResultTest()
        {
            // Arrange
            SetupResultTestData();
            var filledTree = _resultTree;
            var expected = _resultXml;
            var converter = new XmlTreeConverter<Result>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings());

            //Act
            converter.ToXml(filledTree, out var actual);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Images
        private Image _image;
        private string _imageXml;
        private DeterminationTree<Result> _imageTree;

        private void SetupImageTestData()
        {
            _image = new Image()
            {
                XmlId = 1,
                Name = "test",
                Extension = "png"
            };

            _imageXml = "<?xml version='1.0' encoding='utf-8'?>" + Environment.NewLine +
                         "<data>" + Environment.NewLine +
                         "\t<questions />" + Environment.NewLine +
                         "\t<results />" + Environment.NewLine +
                         "\t<images>" + Environment.NewLine +
                         "\t\t<image id='" + _image.XmlId + "' name='" + _image.Name + "' extension='" + _image.Extension + "' />" + Environment.NewLine +
                         "\t</images>" + Environment.NewLine +
                         "</data>";

            _imageTree = new DeterminationTree<Result>()
            {
                Images = new List<Image>
                {
                    _image
                }
            };
        }

        [TestMethod]
        public void ReadImageTest()
        {
            // Arrange
            SetupImageTestData();
            var xml = _imageXml;
            var converter = new XmlTreeConverter<Result>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings());

            //Act
            var tree = converter.FromXml(xml);

            // Assert
            var treeImages = tree.Images;
            Assert.AreEqual(1, treeImages.Count);
            Assert.IsTrue(_image.IsEquivalentTo(treeImages[0]));
        }

        [TestMethod]
        public void WriteImageTest()
        {
            // Arrange
            SetupImageTestData();
            var filledTree = _imageTree;
            var expected = _imageXml;
            var parser = new XmlTreeConverter<Result>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings());

            //Act
            parser.ToXml(filledTree, out var actual);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
