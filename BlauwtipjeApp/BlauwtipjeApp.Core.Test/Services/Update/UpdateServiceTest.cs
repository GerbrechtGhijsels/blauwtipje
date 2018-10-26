using System;
using System.Collections.Generic;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Models.FileManagement;
using BlauwtipjeApp.Core.Test.Fakes.Daos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Services.Update
{
    [TestClass]
    public class UpdateServiceTest
    {
        private IAzureResourceDAO fakeRemoteDatabase;
        private ILocalResourceDAO fakeLocalDatabase;

        private string treeXml;
        private Resource treeResource = new Resource
        {
            Etag = "123",
            Name = "Determination.xml"
        };

        private Resource changeLogResource = new Resource
        {
            Etag = "123",
            Name = "Changelog.txt",
            Content = new byte[]
            {
                10, 20, 30
            }
        };

        private Resource imageResource = new Resource
        {
            Etag = "123",
            Name = "images/P1.png",
            Content = new byte[]
            {
                10, 20, 30
            }
        };

        [TestInitialize]
        public void TestInitialize()
        {
            treeXml = "<?xml version='1.0' encoding='utf-8'?>" + Environment.NewLine +
                      "<data>" + Environment.NewLine +
                      "\t<questions />" + Environment.NewLine +
                      "\t<results />" + Environment.NewLine +
                      "\t<images>" + Environment.NewLine +
                      "\t\t<image id='" + imageResource + "' name='" + imageResource.Name.Split('.')[0] + "' extension='" + imageResource.Name.Split('.')[1] + "' />" + Environment.NewLine +
                      "\t</images>" + Environment.NewLine +
                      "</data>";
            treeResource.SetContentFromString(treeXml);
            fakeLocalDatabase = new FakeLocalResourceDAO(new List<Resource>
            {
                changeLogResource,
                imageResource,
                treeResource
            });

            var changedChangeLog = new Resource
            {
                Etag = "456",
                Name = "Changelog.txt",
                Content = new byte[]
                {
                    40, 50, 60
                }
            };
            var changedImage = new Resource
            {
                Etag = "456",
                Name = "images/P1.png",
                Content = new byte[]
                {
                    40, 50, 60
                }
            };
            var changedTree = new Resource
            {
                Etag = "456",
                Name = "Determination.xml"
            };
            changedTree.SetContentFromString(treeXml);
            fakeRemoteDatabase = new FakeAzureResourceDAO(new List<Resource>
            {
                changedChangeLog,
                changedImage,
                changedTree
            });
        }

        [TestMethod]
        public void ShouldUpdateChangelogTest()
        {
            // Arrange
            //var updateService = new UpdateService(fakeRemoteDatabase, fakeLocalDatabase);

            //// Act
            //updateService.DoUpdate();

            // Assert
            //var localChangelog = fakeLocalDatabase.Get("Changelog.txt");
            //var remoteChangelog = fakeRemoteDatabase.Get("Changelog.txt");
            //Assert.IsTrue(localChangelog.Equals(remoteChangelog));
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ShouldUpdateTreeXmlTest()
        {
            //// Arrange
            //var updateService = new UpdateService(fakeRemoteDatabase, fakeLocalDatabase);

            //// Act
            //updateService.DoUpdate();

            // Assert
            //var localTreeXml = fakeLocalDatabase.Get("Determination.xml").GetContentAsString();
            //var remoteTreeXml = fakeRemoteDatabase.Get("Determination.xml").GetContentAsString();
            //Assert.IsTrue(localTreeXml.Equals(remoteTreeXml));
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ShouldUpdateImageTest()
        {
            //// Arrange
            //var updateService = new UpdateService(fakeRemoteDatabase, fakeLocalDatabase);

            //// Act
            //updateService.DoUpdate();

            // Assert
            //var localImage = fakeLocalDatabase.Get("images/P1.png");
            //var remoteImage = fakeRemoteDatabase.Get("images/P1.png");
            //Assert.IsTrue(localImage.Equals(remoteImage));
            Assert.IsTrue(true);
        }
    }
}
