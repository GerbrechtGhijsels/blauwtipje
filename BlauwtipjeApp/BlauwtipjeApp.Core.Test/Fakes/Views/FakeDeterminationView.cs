using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Presenters;

namespace BlauwtipjeApp.Core.Test.Fakes.Views
{
    public class FakeDeterminationView : FakeBaseView, IDeterminationView<Result>
    {

        public FakeDeterminationView()
        {
        }

        public byte[] DeterminationPicture { get; private set; }
        public void SetDeterminationPicture(byte[] picture)
        {
            DeterminationPicture = picture;
        }

        public byte[] GetDeterminationPicture()
        {
            return DeterminationPicture;
        }

        public Question CurrentQuestion { get; private set; }
        public void SetCurrentQuestion(Question question)
        {
            CurrentQuestion = question;
        }

        public Question GetCurrentQuestion()
        {
            return CurrentQuestion;
        }

        public bool PhotoSelectorShown { get; private set; }
        public void ShowPhotoSelector()
        {
            PhotoSelectorShown = true;
        }

        public Result ResultShown { get; private set; }
        public void ShowResult(Result result)
        {
            ResultShown = result;
        }

        public enum ZoomStatus
        {
            ZoomedIn,
            ZoomedOut
        }

        public ZoomStatus OwnPictureZoomStatus { get; private set; }
        public void ZoomOwnPictureIn()
        {
            OwnPictureZoomStatus = ZoomStatus.ZoomedIn;
        }
        public void ZoomOwnPictureOut()
        {
            OwnPictureZoomStatus = ZoomStatus.ZoomedOut;
        }
    }
}