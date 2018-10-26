using System;
using System.Collections.Generic;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.iOS.Classes;
using Foundation;
using UIKit;

namespace BlauwtipjeApp.iOS.ViewControllers
{
    public partial class DeterminationViewController : BaseActivity<DeterminationPresenter<Slug>>, IDeterminationView<Slug>
    {
        private List<Choice> choices;

        private bool noPictureSelected;
        private DeterminationTree<Slug> tree;
        private Question currentQuestion;
        private UIImage myPicture;
        private List<Question> previousQuestions;
        private UIImagePickerController imagePicker;
        private ChoiceTable choiceTable;
        //private Bundle extras;

        public DeterminationViewController() : base("DeterminationViewController", null)
        {
        }

        public DeterminationViewController(bool useOwnPicture){
            this.noPictureSelected = !useOwnPicture;
        }

        public DeterminationViewController(bool useOwnPicture, UIImage myPicture)
        {
            this.noPictureSelected = !useOwnPicture;
            this.myPicture = myPicture;
        }

        public void OnChoiceClicked(int selectedChoice)
        {
            Presenter.OnChoiceClicked(selectedChoice);
        }

        public override void ViewDidLoad()
        {   
            choices = new List<Choice>();
            Presenter = ServiceLocator.GetService<IPresenterFactory<Slug>>().GetPresenterFor(this);
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.



            QuestionTable.RegisterNibForCellReuse(SpeciesViewCell.Nib, "Cell");
            choiceTable = new ChoiceTable(this);
            QuestionTable.Source = choiceTable;
            choiceTable.SetChoices(choices);
            choiceTable.OnChoiceClicked += (sender, selectedChoice) => OnChoiceClicked(selectedChoice);





        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }





        private void AdjustDeterminationLayout()
        {
            DeterminationImage.Image = null;
            DeterminationImage.Hidden = true;
        }




        protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            // determine what was selected, video or image
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    Console.WriteLine("Image selected");
                    isImage = true;
                    break;
                case "public.video":
                    Console.WriteLine("Video selected");
                    break;
            }

            // get common info (shared between images and video)
            NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceURL")] as NSUrl;
            if (referenceURL != null)
                Console.WriteLine("Url:" + referenceURL.ToString());

            // if it was an image, get the other image info
            if (isImage)
            {
                // get the original image
                UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    // do something with the image
                    Console.WriteLine("got the original image");
                    myPicture = originalImage; // display
                    DeterminationImage.Image = myPicture;
                }
            }
            else
            { // if it's a video
              // get video url
                NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                if (mediaURL != null)
                {
                    Console.WriteLine(mediaURL.ToString());
                }
            }
            // dismiss the picker
            imagePicker.DismissViewController(true,null);

        }

        void Handle_Canceled(object sender, EventArgs e)
        {
            imagePicker.DismissViewController(true, null);
            AdjustDeterminationLayout();
        }

        public void SetCurrentQuestion(Question question)
        {
            currentQuestion = question;
            choices.Clear();
            foreach (var choice in question.Choices)
            {
                choices.Add(choice);
            }

            QuestionTable.ReloadData();


        }

        public Question GetCurrentQuestion()
        {
            return currentQuestion;
        }

        public void SetDeterminationPicture(byte[] picture)
        {
            if (picture == null) return;
            NSData data = NSData.FromArray(picture);
            UIImage image = UIImage.LoadFromData(data);
            DeterminationImage.Image = image;
        }

        public void ShowPhotoSelector()
        {
            imagePicker = new UIImagePickerController();

            imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

            imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
            imagePicker.Canceled += Handle_Canceled;

            NavigationController.PresentModalViewController(imagePicker, true);
        }

        public void ShowResult(Slug result)
        {
            NavigationController.PushViewController(new AnimalResultViewController(result.Id), true);
        }
    }
}

