using System.Collections.Generic;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Models.FileManagement;
using BlauwtipjeApp.Core.Services.Update.Impl;
using BlauwtipjeApp.Core.Test.Fakes.Daos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Services.Update
{
    [TestClass]
    public class UpdateHelperTest
    {
        private ILocalResourceDAO fakeLocalDatabase;

        [TestInitialize]
        public void TestInitialize()
        {
            fakeLocalDatabase = new FakeLocalResourceDAO(new List<Resource>
            {
                new Resource
                {
                    Name = "test1.txt",
                    Etag = "123"
                },
                new Resource
                {
                    Name = "test2.txt",
                    Etag = "102030"
                }
            });
        }

        [TestMethod]
        public void IsResourceUpdatedTest()
        {
            // Arrange
            var remoteResources = new List<Resource>
            {
                new Resource
                {
                    Name = "test1.txt",
                    Etag = "123"
                }
            };
            var fakeAzureResourceDao = new FakeAzureResourceDAO(remoteResources);
            var updateHelper = new UpdateHelper(fakeAzureResourceDao, fakeLocalDatabase);

            //Act
            var result1 = updateHelper.IsResourceChanged("test1.txt");
            remoteResources[0].Etag = "456";
            var result2 = updateHelper.IsResourceChanged("test1.txt");

            // Assert
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void UpdateResourceTest()
        {
            // Arrange
            var remoteResources = new List<Resource>
            {
                new Resource
                {
                    Name = "test1.txt",
                    Etag = "456"
                },
                new Resource
                {
                    Name = "test2.txt",
                    Etag = "102030"
                }
            };
            var fakeRemoteDatabase = new FakeAzureResourceDAO(remoteResources);
            var updateHelper = new UpdateHelper(fakeRemoteDatabase, fakeLocalDatabase);

            //Act
            var isUpdated1 = updateHelper.UpdateResourceIfChanged("test1.txt");
            var isUpdated2 = updateHelper.UpdateResourceIfChanged("test2.txt");
            var updatedResource = fakeLocalDatabase.Get("test1.txt");
            var notUpdatedResource = fakeLocalDatabase.Get("test2.txt");

            // Assert
            Assert.IsTrue(isUpdated1);
            Assert.IsFalse(isUpdated2);
            Assert.AreEqual("456", updatedResource.Etag);
            Assert.AreEqual("102030", notUpdatedResource.Etag);
        }
    }
}
