using System;
using System.Linq;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Droid.Components.Image;
using BlauwtipjeApp.Droid.Components.WebView;

namespace BlauwtipjeApp.Droid.Components.RecyclerListView.ViewHolders
{
    public class ChoiceViewHolder : RecyclerView.ViewHolder
    {
        private readonly FrameLayout choiceTextViewContainer;
        private readonly ResourceImageView choiceImageView;
        private readonly TextView choiceImageCountTextView;
        private readonly Button choiceSelectButton;

        public event EventHandler<int> OnChoiceSelected;
        public event EventHandler<int> OnChoiceImageClicked;

        private int choicePosition;

        public ChoiceViewHolder(View itemView) : base(itemView)
        {
            choiceTextViewContainer = ItemView.FindViewById<FrameLayout>(Resource.Id.webViewContainer);
            choiceImageView = ItemView.FindViewById<ResourceImageView>(Resource.Id.rowImage);
            choiceImageCountTextView = ItemView.FindViewById<TextView>(Resource.Id.indication);
            choiceSelectButton = ItemView.FindViewById<Button>(Resource.Id.SelecteerButton);

            choiceSelectButton.Click += (sender, args) =>
            {
                OnChoiceSelected?.Invoke(sender, choicePosition);
            };

            choiceImageView.Click += (sender, args) =>
            {
                OnChoiceImageClicked?.Invoke(sender, choicePosition);
            };
        }

        private CustomWebView tempWebview;
        public void SetChoice(Choice choice, int position)
        {
            choicePosition = position;
            if (tempWebview != null)
            {
                choiceTextViewContainer.RemoveAllViews();
                tempWebview.Destroy();
            }
            tempWebview = new CustomWebView(ItemView.Context);
            choiceTextViewContainer.AddView(tempWebview,
                        new FrameLayout.LayoutParams(
                                         ViewGroup.LayoutParams.MatchParent,
                                         ViewGroup.LayoutParams.WrapContent));
            tempWebview.SetBackgroundColor(Color.Transparent);
            tempWebview.InjectHtml(choice.Text);

            if (choice.ImageList.Count <= 1)
            {
                choiceImageCountTextView.Visibility = ViewStates.Gone;
            }
            else if (choice.ImageList.Count > 1)
            {
                choiceImageCountTextView.Visibility = ViewStates.Visible;
                choiceImageCountTextView.Text = "1/" + choice.ImageList.Count;
            }

            if (choice.ImageList.Any())
                choiceImageView.SetImageFromBytes(choice.ImageList[0].Content);

            var next = choice.TryNext();
            if (next is Question)
            {
                choiceSelectButton.Text = "Selecteer";
                choiceSelectButton.SetTextColor(Color.White);
            }
            else if (next is Slug slug)
            {
                choiceSelectButton.Text = slug.DisplayName;
                choiceSelectButton.SetTextColor(Color.Orange);
            }
        }
    }
}