using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.Components.Dialog;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.iOS.Dialogs;
using Foundation;
using UIKit;

namespace BlauwtipjeApp.iOS.ViewControllers
{
    public partial class MainViewController : BaseActivity<MainPresenter>, IMainView
    {
        private UIAlertController progressDialog;
        private ProgressDialogBuilder progressDialogBuilder;
        private UIImagePickerController imagePicker;
        private UIImage myPicture;


        public MainViewController(IntPtr handle) : base("MainViewController", null)
        {

        }

        public MainViewController() : base("MainViewController", null)
        {

        }

        public override void ViewDidLoad()
        {
            Presenter = ServiceLocator.GetService<IPresenterFactory<Slug>>().GetPresenterFor(this);
            base.ViewDidLoad();


            //AppDelegate.Self.GetUpdateService();
            //var updateService = AppDelegate.Self.GetUpdateService();
            //var networkHelper = AppDelegate.Self.GetNetworkHelper();
            //viewModel = new MainViewModel(this, updateService, networkHelper);
           
            BtnDetermination.TouchUpInside += (sender, e) => Presenter.OnDeterminationButtonClicked();
            BtnSpeciesList.TouchUpInside += (sender, e) =>  Presenter.OnSpeciesListButtonClicked();
            BtnInfo.TouchUpInside += (sender, e) =>  Presenter.OnInfoButtonClicked();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);



        }




        public void DismissProgressDialog()
        {
            progressDialogBuilder.Hide();
        }


        public void OpenUpdateScreen()
        {
            NavigationController.PushViewController(new UpdateViewController(), true);
        }

        public void SetRandomPicture(byte[] pictrue)
        {
            throw new NotImplementedException();
        }

        public List<Image> GetRandomPicturesForOnMainScreen()
        {
            throw new NotImplementedException();
        }
    }
}

