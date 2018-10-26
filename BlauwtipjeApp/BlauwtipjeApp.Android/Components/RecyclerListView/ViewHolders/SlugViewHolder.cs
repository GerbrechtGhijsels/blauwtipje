using System;
using System.Linq;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using BlauwtipjeApp.Core.Models.Results;

using BlauwtipjeApp.Droid.Components.Image;

namespace BlauwtipjeApp.Droid.Components.RecyclerListView.ViewHolders
{
    public class SlugViewHolder : RecyclerView.ViewHolder
    {
        private TextView slugNameTextView;
        private TextView slugScientificNameTextView;
        private ResourceImageView slugImageView;

        private Slug slug;
        public event EventHandler<int> OnSlugClick;

        public SlugViewHolder(View view) : base(view)
        {
            slugNameTextView = view.FindViewById<TextView>(Resource.Id.SpecieslistAnimalName);
            slugScientificNameTextView = view.FindViewById<TextView>(Resource.Id.SpecieslistScientificName);
            slugImageView = view.FindViewById<ResourceImageView>(Resource.Id.SpecieslistImage);
            ItemView.Click += (sender, e) => OnSlugClick?.Invoke(sender, slug.Id);
        }

        public void SetSlug(Slug slg)
        {
            slug = slg;
            slugNameTextView.Text = slug.DisplayName;
            slugScientificNameTextView.Text = slug.ScientificName;

            var slugImage = slug.ImageList.First();
            
            slugImageView.SetImageFromBytes(slugImage?.Content);
            
        }
    }
}