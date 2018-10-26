using System.Collections.Generic;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Presenters;

namespace BlauwtipjeApp.Core.Test.Fakes.Views
{
    public class FakeMainView : FakeBaseView, IMainView
    {
        public FakeMainView()
        {
        }

        public bool UpdateScreenOpened { get; private set; }
        public void OpenUpdateScreen()
        {
            UpdateScreenOpened = true;
        }

        public byte[] PictureSet { get; private set; }
        public void SetRandomPicture(byte[] pictrue)
        {
            PictureSet = pictrue;
        }

        public List<Image> RandomPictures { get; set; }
        public List<Image> GetRandomPicturesForOnMainScreen()
        {
            return new List<Image>();
        }
    }
}