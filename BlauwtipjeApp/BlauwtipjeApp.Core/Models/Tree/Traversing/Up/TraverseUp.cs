namespace BlauwtipjeApp.Core.Models.Tree.Traversing.Up
{
    public class TraverseUp : IBackwardsTraversable
    {
        private TreeNode backNode;

        public TraverseUp(TreeNode back)
        {
            backNode = back;
        }

        public TreeNode Back()
        {
            return backNode;
        }
    }
}
