using System;
using System.Collections.Generic;
using BlauwtipjeApp.Core.Models.FileManagement;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Services.Tree.Decorators.Impl;
using BlauwtipjeApp.Core.Test.Fakes.Daos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Services.Tree.Decoration
{
    [TestClass]
    public class HtmlContentPreparation
    {
        [TestMethod]
        public void DecorateInfoText()
        {
            // Arrange
            var tree = new DeterminationTree<Result>();
            tree.Info = "blabla {img:1} something";
            tree.Images.Add(new Image
            {
                XmlId = 1,
                Name = "test",
                Extension = "png"
            });
            var dao = new FakeLocalResourceDAO(new List<Resource>
            {
                new Resource
                {
                    Name = "test.png",
                    Content = new byte[] {1,2,3}
                }
            });
            var decorator = new TreeDecorator<Result>(dao);

            // Act
            decorator.DecorateTree(tree);

            // Assert
            Assert.AreEqual("blabla " + "base64," + Convert.ToBase64String(new byte[] { 1, 2, 3 }) + " something", tree.Info);
        }

        [TestMethod]
        public void DecorateResult()
        {
            // Arrange
            var tree = new DeterminationTree<Result>();
            tree.Results.Add(new Result
            {
                Id = 5,
                Text = "start {img:1} end"
            });
            tree.Images.Add(new Image
            {
                XmlId = 1,
                Name = "test",
                Extension = "png"
            });
            var dao = new FakeLocalResourceDAO(new List<Resource>
            {
                new Resource
                {
                    Name = "test.png",
                    Content = new byte[] {1,2,3}
                }
            });
            var decorator = new TreeDecorator<Result>(dao);

            // Act
            decorator.DecorateTree(tree);

            // Assert
            Assert.AreEqual("start " + "base64," + Convert.ToBase64String(new byte[] { 1, 2, 3 }) + " end", tree.Results[0].Text);
        }
    }
}
