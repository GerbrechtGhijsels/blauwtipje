using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Models.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BlauwtipjeApp.Core.Test.Helpers
{
    [TestClass]
    public class HtmlContentPreparerTest
    {
        [TestMethod]
        public void NullImageList()
        {
            // Arrange
            var htmlContentPreparer = new HtmlContentPreparer(null);

            // Act
            var result = htmlContentPreparer.InjectImages("start {img:1} end");

            // Assert
            Assert.AreEqual("start {img:1} end", result);
        }

        [TestMethod]
        public void EmptyString()
        {
            // Arrange
            var images = new List<Image>
            {
                new Image
                {
                    XmlId = 1,
                    Content = new byte[] {1, 2, 3}
                }
            };
            var htmlContentPreparer = new HtmlContentPreparer(images);

            // Act
            var result = htmlContentPreparer.InjectImages(string.Empty);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void NullString()
        {
            // Arrange
            var images = new List<Image>
            {
                new Image
                {
                    XmlId = 1,
                    Content = new byte[] {1, 2, 3}
                }
            };
            var htmlContentPreparer = new HtmlContentPreparer(images);

            // Act
            var result = htmlContentPreparer.InjectImages(null);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void OneImage()
        {
            // Arrange
            var images = new List<Image>
            {
                new Image
                {
                    XmlId = 1,
                    Content = new byte[] {1, 2, 3}
                }
            };
            var htmlContentPreparer = new HtmlContentPreparer(images);

            // Act
            var result = htmlContentPreparer.InjectImages("start {img:1} end");

            // Assert
            Assert.AreEqual("start " + "base64," + Convert.ToBase64String(images[0].Content) + " end", result);
        }

        [TestMethod]
        public void TwoImages()
        {
            // Arrange
            var images = new List<Image>
            {
                new Image
                {
                    XmlId = 1,
                    Content = new byte[] {1, 2, 3}
                },
                new Image
                {
                    XmlId = 2,
                    Content = new byte[] {4, 5, 6}
                }
            };
            var htmlContentPreparer = new HtmlContentPreparer(images);

            // Act
            var result = htmlContentPreparer.InjectImages("start {img:1} mid {img:2} end");

            // Assert
            Assert.AreEqual("start " + 
                            "base64," + Convert.ToBase64String(images[0].Content) + 
                            " mid " +
                            "base64," + Convert.ToBase64String(images[1].Content) +
                            " end", result);
        }

        [TestMethod]
        public void NoImageMarkers()
        {
            // Arrange
            var images = new List<Image>
            {
                new Image
                {
                    XmlId = 1,
                    Content = new byte[] {1, 2, 3}
                }
            };
            var htmlContentPreparer = new HtmlContentPreparer(images);

            // Act
            var result = htmlContentPreparer.InjectImages("start {umg:1} end");

            // Assert
            Assert.AreEqual("start {umg:1} end", result);
        }

        [TestMethod]
        public void UnknownImageReference()
        {
            // Arrange
            var images = new List<Image>
            {
                new Image
                {
                    XmlId = 1,
                    Content = new byte[] {1, 2, 3}
                }
            };
            var htmlContentPreparer = new HtmlContentPreparer(images);

            // Act
            var result = htmlContentPreparer.InjectImages("start {img:2} end");

            // Assert
            Assert.AreEqual("start {img:2} end", result);
        }
    }
}
