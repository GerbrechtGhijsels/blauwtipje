using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Content.PM;
using Android.Support.V7.Widget;
using BlauwtipjeApp.Core.Helpers;
using Android.Support.V4.View;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Droid.Components.RecyclerListView.Adapters;
using System.Linq;

namespace BlauwtipjeApp.Droid.Activities
{
    [Activity(Label = "", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SpeciesListActivity : BaseActivity<ResultListPresenter<Slug>>, IResultListView<Slug>, MenuItemCompat.IOnActionExpandListener
    {
        private RecyclerAdapterSpecies adapter;
        private List<Slug> resultList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Presenter = ServiceLocator.GetService<IPresenterFactory<Slug>>().GetPresenterFor(this);
            CustomLayoutId = Resource.Layout.activity_specieslist;
            CurrentActivity = NavigableScreen.SpeciesList;
            CanNavigateTo = new List<NavigableScreen>
            {
                NavigableScreen.Main,
                NavigableScreen.Determination,
                NavigableScreen.Info
            };
            base.OnCreate(savedInstanceState);
            ActivityTitle.Text = "Soortenlijst";

            var mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewSpecies);
            mRecyclerView.HasFixedSize = true;

            resultList = new List<Slug>();

            //create layout manager
            var mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            adapter = new RecyclerAdapterSpecies(resultList);
            adapter.OnSlugClick += (sender, slugId) => Presenter.OnResultClicked(slugId);
            mRecyclerView.SetAdapter(adapter);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.search_menu, menu);

            var searchItem = menu.FindItem(Resource.Id.search);
            var searchView = (SearchView)MenuItemCompat.GetActionView(searchItem);

            searchView = searchView.JavaCast<SearchView>();

            searchView.QueryTextChange += (s, e) => adapter.Filter.InvokeFilter(e.NewText);

            searchView.QueryTextSubmit += (s, e) =>
            {
                // Handle enter/search button on keyboard here
                e.Handled = true;
                (menu.FindItem(Resource.Id.search)).ActionView.ClearFocus();
            };

            MenuItemCompat.SetOnActionExpandListener(searchItem, this);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // Handle item selection
            switch (item.ItemId)
            {
                case Resource.Id.Name_sort:
                    adapter.SortByDisplayNameAtoZ();
                    break;
                case Resource.Id.ScientifficName_sort:
                    adapter.SortByScientificNameAtoZ();
                    break;
                default:
                    return base.OnOptionsItemSelected(item);
            }
            return true;
        }

        public bool OnMenuItemActionCollapse(IMenuItem item)
        {
            adapter.Filter.InvokeFilter("");
            return true;
        }

        public bool OnMenuItemActionExpand(IMenuItem item)
        {
            return true;
        }

        public void SetResultList(List<Slug> results)
        {
            resultList.Clear();
            resultList.AddRange(results.OrderBy(o => o.DisplayName).ToList());
            adapter.NotifyDataSetChanged();
        }

        public void ShowResultDetailView(Slug result)
        {
            var intent = new SlugResultActivity.SlugResultIntent(this) { ResultId = result.Id };
            StartActivity(intent);
        }
    }
}