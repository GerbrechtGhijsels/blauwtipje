using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Views.Animations;
using BlauwtipjeApp.Droid.Helpers;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using Result = Android.App.Result;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Droid.Components.Image;
using BlauwtipjeApp.Droid.Components.RecyclerListView.Adapters;
using FFImageLoading.Transformations;

namespace BlauwtipjeApp.Droid.Activities
{
    [Activity(Label = "", ParentActivity = typeof(MainActivity),
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class DeterminationActivity : BaseActivity<DeterminationPresenter<Slug>>, IDeterminationView<Slug>
    {
        private RecyclerAdapterDetermination adapter;

        private FrameLayout imageContainer;
        private ResourceImageView ownPictureView;
        private ExpandImageHelper expandImageHelper;
        private RecyclerView recyclerView;

        private Question currentQuestion;
        private List<Choice> choices;

        private const int PictureChooserRequestCode = 20;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Presenter = ServiceLocator.GetService<IPresenterFactory<Slug>>().GetPresenterFor(this);
            CustomLayoutId = Resource.Layout.activity_determination;
            CanNavigateTo = new List<NavigableScreen>
            {
                NavigableScreen.Main,
                NavigableScreen.SpeciesList,
                NavigableScreen.Info
            };
            CurrentActivity = NavigableScreen.Determination;
            base.OnCreate(savedInstanceState);

            ownPictureView = FindViewById<ResourceImageView>(Resource.Id.ownPicture);
            imageContainer = FindViewById<FrameLayout>(Resource.Id.imageContainer);
            imageContainer.Click += OwnPictureOnClick;
            imageContainer.LongClick += OwnPictureOnLongClick;

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
         
            choices = new List<Choice>();

            var layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);

            var expandedImageView = FindViewById<ImageView>(Resource.Id.expanded_image);
            expandImageHelper = new ExpandImageHelper(expandedImageView);
            expandImageHelper.SetZoomInAnimation(AnimationUtils.LoadAnimation(this, Resource.Animation.zoom_in));
            expandImageHelper.SetZoomOutAnimation(AnimationUtils.LoadAnimation(this, Resource.Animation.zoom_out));

            adapter = new RecyclerAdapterDetermination(choices);
            adapter.OnChoiceClicked += (sender, selectedChoice) => OnChoiceClicked(selectedChoice);
            adapter.OnChoiceImageClicked += (sender, selectedChoice) => OnChoiceImageClicked(selectedChoice);
            recyclerView.SetAdapter(adapter);
        }

        private void OwnPictureOnClick(object o, EventArgs eventArgs)
        {
            if (!ownPictureView.IsImageSet)
                Presenter.OnUserSelectPictureRequest();
            else
                expandImageHelper.ZoomImageIn();
        }

        private void OwnPictureOnLongClick(object sender, View.LongClickEventArgs e)
        {
            if (ownPictureView.IsImageSet)
                Presenter.OnUserSelectPictureRequest();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == PictureChooserRequestCode && resultCode == Result.Ok)
            {
                var stream = ContentResolver.OpenInputStream(data.Data);
                Presenter.OnUserSelectPictureResponse(ImageUtils.StreamToBytes(stream));
            }
            else
            {
                base.OnActivityResult(requestCode, resultCode, data);
            }
        }

        public void SetCurrentQuestion(Question question)
        {
            currentQuestion = question;
            choices.Clear();
            foreach (var choice in question.Choices)
                choices.Add(choice);

            if (currentQuestion.Text != null)
                FormatHeaderTitle(currentQuestion.Text);

            adapter?.NotifyDataSetChanged();
            recyclerView.GetLayoutManager().ScrollToPosition(0);
        }
        public Question GetCurrentQuestion()
        {
            return currentQuestion;
        }

        public void SetDeterminationPicture(byte[] picture)
        {
            if (picture == null) return;
            expandImageHelper.SetPicture(picture);
            ownPictureView.SetImageFromBytes(picture, new ImageTransformation
            {
                Width = 300,
                Height = 300,
                Rounded = new RoundedTransformation(20)
            });
            ChangeOwnImageToRecyclerViewVerticalProportions(40, 60);
        }

        public void ChangeOwnImageToRecyclerViewVerticalProportions(int ownImageProportion, int recyclerViewProportion)
        {
            imageContainer.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 0, ownImageProportion);
            recyclerView.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 0, recyclerViewProportion);
            recyclerView.GetLayoutManager().ScrollToPosition(0);
        }

        /// <summary>
        /// Formats the header title.
        /// </summary>
        /// <param name="headerTitle">The header title.</param>
        public void FormatHeaderTitle(string headerTitle)
        {
            //Set Max length for header string
            const int MaxLength = 22;
            char[] MyChar = { '\n', ' ', '.' };

            headerTitle = headerTitle.Trim(MyChar);

            if (IsTablet() == false && headerTitle.Length > MaxLength)
                headerTitle = headerTitle.Substring(0, MaxLength) + "...";

            ActivityTitle.Text = headerTitle;
        }

        public void ShowPhotoSelector()
        {
            var imageIntent = new Intent();
            imageIntent.SetType("image/*");
            imageIntent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(imageIntent, "Selecteer Foto"), PictureChooserRequestCode);
        }

        public void ShowResult(Slug result)
        {
            var intent = new SlugResultActivity.SlugResultIntent(this) {ResultId = result.Id};
            StartActivity(intent);
        }

        public void OnChoiceClicked(int selectedChoice)
        {
            Presenter.OnChoiceClicked(selectedChoice);
        }

        public void OnChoiceImageClicked(int selectedChoice)
        {
            Presenter.OnChoiceImageClicked(selectedChoice);
        }

        protected override void OnPause()
        {
            if (expandImageHelper.IsVisible())
                expandImageHelper.ZoomImageOut();
            base.OnPause();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.determinateRecapInfo)
                Presenter.OnInfoButtonClicked();
            return base.OnOptionsItemSelected(item);
        }

        //inflate toolbar 
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}
