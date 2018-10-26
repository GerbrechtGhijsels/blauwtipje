using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Droid.Components.RecyclerListView.ViewHolders;

namespace BlauwtipjeApp.Droid.Components.RecyclerListView.Adapters
{
    public class RecyclerAdapterDetermination : RecyclerView.Adapter
    {
        private readonly List<Choice> choiceList;
        public event EventHandler<int> OnChoiceClicked;
        public event EventHandler<int> OnChoiceImageClicked;

        public RecyclerAdapterDetermination(List<Choice> choiceList)
        {
            this.choiceList = choiceList;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_determinationquestion, parent, false);
            var viewHolder = new ChoiceViewHolder(row);
            viewHolder.OnChoiceSelected += OnChoiceClicked;
            viewHolder.OnChoiceImageClicked += OnChoiceImageClicked;
            return viewHolder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var choice = choiceList[position];
            if (holder is ChoiceViewHolder choiceViewHolder)
            {
                choiceViewHolder.SetChoice(choice, position);
            }
        }

        public override int ItemCount
        {
            get { return choiceList.Count; }
        }
    }
}