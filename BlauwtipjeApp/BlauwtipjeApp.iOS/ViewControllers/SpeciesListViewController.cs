using System;
using System.Collections.Generic;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Presenters;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace BlauwtipjeApp.iOS.ViewControllers
{
    public partial class SpeciesListViewController : BaseTableActivity<ResultListPresenter<Slug>>, IResultListView<Slug>
    {   
        private List<Slug> resultList;

        public SpeciesListViewController(IntPtr handle) : base(handle)
        {
        }

        public SpeciesListViewController(){
            
        }

        public override void ViewDidLoad()
        {   
            resultList = new List<Slug>();

            Presenter = ServiceLocator.GetService<IPresenterFactory<Slug>>().GetPresenterFor(this);
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.



        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            NavigationController.PushViewController(new AnimalResultViewController(resultList[indexPath.Row].Id), true);
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return resultList.Count;
        }

        public override nfloat GetHeightForRow(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            //return UITableView.AutomaticDimension;
            return 100;
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(SpeciesViewCell.Key) as SpeciesViewCell;
            if(cell == null){
                cell = new SpeciesViewCell();
                var views = NSBundle.MainBundle.LoadNib(SpeciesViewCell.Key, cell, null);
                cell = Runtime.GetNSObject(views.ValueAt(0)) as SpeciesViewCell;
            }
            cell.PopulateCell(resultList[indexPath.Row]);
            return cell;
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void SetResultList(List<Slug> results)
        {
            resultList.Clear();
            resultList.AddRange(results);


        }

        public void ShowResultDetailView(Slug result)
        {
            NavigationController.PushViewController(new AnimalResultViewController(result.Id), true);
        }
    }
}

