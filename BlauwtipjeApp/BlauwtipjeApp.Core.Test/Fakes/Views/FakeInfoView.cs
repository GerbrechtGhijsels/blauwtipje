using BlauwtipjeApp.Core.Presenters;

namespace BlauwtipjeApp.Core.Test.Fakes.Views
{
    public class FakeInfoView : FakeBaseView, IInfoView
    {
        public FakeInfoView()
        {
        }

        public string InfoText { get; private set; }
        public void SetInfoText(string text)
        {
            InfoText = text;
        }
    }
}