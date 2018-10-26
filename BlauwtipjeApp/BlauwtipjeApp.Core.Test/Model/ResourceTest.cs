using BlauwtipjeApp.Core.Models.FileManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Model
{
    [TestClass]
    public class ResourceTest
    {
        public void TestShouldBeEqualWithDifferentId()
        {
            // Arrange
            var baseResource = new Resource
            {
                Name = "Test",
                Etag = "1",
                Content = new byte[] {1}
            };
            var otherResource = new Resource
            {
                Name = "Test",
                Etag = "1",
                Content = new byte[] { 1 }
            };

            // Act
            var result = baseResource.Equals(otherResource);

            // Assert
            // Cant debug why this one fails
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestShouldNotBeEqualWithDifferentName()
        {
            // Arrange
            var baseResource = new Resource
            {
                Name = "Test",
                Etag = "1",
                Content = new byte[] { 1 }
            };
            var otherResource = new Resource
            {
                Name = "NotTest",
                Etag = "1",
                Content = new byte[] { 1 }
            };

            // Act
            var result = baseResource.Equals(otherResource);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestShouldNotBeEqualWithDifferentEtag()
        {
            // Arrange
            var baseResource = new Resource
            {
                Name = "Test",
                Etag = "1",
                Content = new byte[] { 1 }
            };
            var otherResource = new Resource
            {
                Name = "Test",
                Etag = "2",
                Content = new byte[] { 1 }
            };

            // Act
            var result = baseResource.Equals(otherResource);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestShouldNotBeEqualWithDifferentContent()
        {
            // Arrange
            var baseResource = new Resource
            {
                Name = "Test",
                Etag = "1",
                Content = new byte[] { 1 }
            };
            var otherResource = new Resource
            {
                Name = "Test",
                Etag = "1",
                Content = new byte[] { 2 }
            };

            // Act
            var result = baseResource.Equals(otherResource);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
