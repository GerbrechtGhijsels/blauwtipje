using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Models.FileManagement;
using BlauwtipjeApp.Core.Models.Tree.Determination;

namespace BlauwtipjeApp.Core.Services.Update.Impl
{
    public class UpdateService : IUpdateService
    {
        private readonly string XmlFileName;
        private readonly string ChangelogFileName;
        private UpdateHelper updateHelper;
        public event EventHandler OnUpdateCompleted;

        public UpdateService(IAzureResourceDAO remoteDatabase, ILocalResourceDAO localDao, string xmlFileName, string changelogFileName)
        {
            this.updateHelper = new UpdateHelper(remoteDatabase, localDao);
            XmlFileName = xmlFileName;
            ChangelogFileName = changelogFileName;
        }

        public string GetChangelog()
        {
            return updateHelper.GetRemoteResourceAsString(ChangelogFileName);
        }

        public bool AreEssentialsInPlace()
        {
            return updateHelper.GetLocalResource(XmlFileName) != null;
        }

        public bool IsUpdateAvailable()
        {
            return updateHelper.IsResourceChanged(ChangelogFileName);
        }

        public async Task DoUpdate<TResult>(IProgressReporter progressReporter) where TResult : Result
        {
            var updatedXml = updateHelper.GetRemoteResourceAsString(XmlFileName);

            progressReporter.StartNewTask("Update afbeeldingen");
            await Task.Run(() =>
            {
                UpdateImages(updatedXml, progressReporter);
            });

            progressReporter.StartNewTask("Verwijderen onnodige afbeeldingen");
            await Task.Run(() =>
            {
                DeleteUnusedImagesFromDatabase(updatedXml, progressReporter);
            });

            progressReporter.StartNewTask("Update determinatie boom"); 
            await Task.Run(() =>
            {
                UpdateTree(progressReporter);
            });

            progressReporter.StartNewTask("Zoeken voor fouten in determinatieboom");
            await Task.Run(() =>
            {
                CheckXml<TResult>(progressReporter);
            });

            progressReporter.StartNewTask("Bijna klaar!");
            await Task.Run(() =>
            {
                UpdateChangelog(progressReporter);
            });

            OnUpdateCompleted?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateImages(string xml, IProgressReporter reporter)
        {
            var imagesInXml = updateHelper.GetImagesFromXml(xml);

            reporter.SetMax(imagesInXml.Count);
            foreach (var xmlImage in imagesInXml)
            {
                updateHelper.UpdateResourceIfChanged(xmlImage.Filename);
                reporter.IncreaseProgressByOne();
            }
        }

        private void DeleteUnusedImagesFromDatabase(string xml, IProgressReporter reporter)
        {
            reporter.SetMax(4);
            var localImageList = updateHelper.GetImagesFromLocalDatabase();
            reporter.IncreaseProgressByOne();
            var imagesInXml = updateHelper.GetImagesFromXml(xml);
            reporter.IncreaseProgressByOne();

            var unusedImages = new List<Resource>();
            foreach (var image in localImageList)
            {
                var isUsed = imagesInXml.FindAll(i => i.Filename == image.Name).Count > 0;
                if (!isUsed)
                    unusedImages.Add(image);
            }
            reporter.IncreaseProgressByOne();

            foreach (var image in unusedImages)
            {
                updateHelper.RemoveLocalResource(image.Name);
            }
            reporter.IncreaseProgressByOne();
        }

        private void UpdateTree(IProgressReporter reporter)
        {
            reporter.SetMax(1);
            updateHelper.UpdateResourceIfChanged(XmlFileName);
            reporter.IncreaseProgressByOne();
        }

        private void CheckXml<TResult>(IProgressReporter reporter) where TResult : Result
        {
            reporter.SetMax(1);
            TreeManager<TResult>.Reinitialize();
            reporter.IncreaseProgressByOne();
        }

        private void UpdateChangelog(IProgressReporter reporter)
        {
            reporter.SetMax(1);
            updateHelper.UpdateResourceIfChanged(ChangelogFileName);
            reporter.IncreaseProgressByOne();
        }
    }
}
