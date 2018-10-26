using System;
using System.Collections.Generic;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.iOS.ViewControllers;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace BlauwtipjeApp.iOS.Classes
{
    public class ChoiceTable :UITableViewSource
    {
        private List<Choice> TableItems;
        private DeterminationViewController context;
        public event EventHandler<int> OnChoiceClicked;
        string CellIdentifier = "TableCell";

        public ChoiceTable(List<Choice> items,DeterminationViewController context)
        {
            TableItems = items;
            this.context = context;

        }

        public ChoiceTable(DeterminationViewController context)
        {
            TableItems =  new List<Choice>();
            this.context = context;
        }

        public void SetChoices(List<Choice> items){
            TableItems = items;
        }



        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //NavigationController.PushViewController(new AnimalResultViewController(TableItems[indexPath.Row]), true);
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return TableItems.Count;
        }

        public override nfloat GetHeightForRow(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            //return UITableView.AutomaticDimension;
            return 600;
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(DeterminationViewCell.Key) as DeterminationViewCell;
            if (cell == null)
            {   
                
                cell = new DeterminationViewCell(this);
                var views = NSBundle.MainBundle.LoadNib(DeterminationViewCell.Key, cell, null);
                cell = Runtime.GetNSObject(views.ValueAt(0)) as DeterminationViewCell;
                cell.OnChoiceSelected += OnChoiceClicked;
            }
            cell.PopulateCell(TableItems[indexPath.Row],indexPath,this);
            return cell;
        }
    }
}
