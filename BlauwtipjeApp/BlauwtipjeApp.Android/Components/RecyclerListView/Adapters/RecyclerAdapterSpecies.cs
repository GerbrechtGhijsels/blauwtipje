using System;
using System.Collections.Generic;
using System.Linq;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Droid.Classes;
using BlauwtipjeApp.Droid.Components.RecyclerListView.ViewHolders;
using Java.Lang;

namespace BlauwtipjeApp.Droid.Components.RecyclerListView.Adapters
{
    public class RecyclerAdapterSpecies : RecyclerView.Adapter, IFilterable
    {
        private List<Slug> mAnimals;
        private List<Slug> copyAnimals;
        public event EventHandler<int> OnSlugClick;

        public RecyclerAdapterSpecies(List<Slug> animals)
        {
            mAnimals = animals;
            Filter = new SpeciesFilter(this);
        }

        public override int GetItemViewType(int position)
        {
            return Resource.Layout.item_species;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_species, parent, false);
            var viewHolder = new SlugViewHolder(row);
            viewHolder.OnSlugClick += (sender, slugId) => OnSlugClick?.Invoke(sender, slugId);
            return viewHolder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (!(holder is SlugViewHolder slugViewHolder)) return;

            var slug = mAnimals[position];
            slugViewHolder.SetSlug(slug);
        }

        public override int ItemCount
        {
            get { return mAnimals.Count; }
        }

        public Filter Filter { get; private set; }

        private class SpeciesFilter : Filter
        {
            private readonly RecyclerAdapterSpecies _adapter;
            public SpeciesFilter(RecyclerAdapterSpecies adapter)
            {
                _adapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                if (constraint == null) return new FilterResults();

                var returnObj = new FilterResults();

                var results = new List<Slug>();
                if (_adapter.copyAnimals == null)
                    _adapter.copyAnimals = _adapter.mAnimals;


                if (_adapter.copyAnimals != null && _adapter.copyAnimals.Any())
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.
                    results.AddRange(
                        _adapter.copyAnimals.Where(
                            species => species.DisplayName.ToLower().Contains(constraint.ToString()) || species.ScientificName.ToLower().Contains(constraint.ToString())));
                }

                // Nasty piece of .NET to Java wrapping, be careful with this!
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                constraint.Dispose();

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    _adapter.mAnimals = values.ToArray<Java.Lang.Object>()
                        .Select(r => r.ToNetObject<Slug>()).ToList();

                _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }
        }

        public void SortByDisplayNameAtoZ(){
            mAnimals = mAnimals.OrderBy(o => o.DisplayName).ToList();
            NotifyDataSetChanged();
        }
        public void SortByScientificNameAtoZ()
        {
            mAnimals = mAnimals.OrderBy(o => o.ScientificName).ToList();
            NotifyDataSetChanged();
        }

    }
}