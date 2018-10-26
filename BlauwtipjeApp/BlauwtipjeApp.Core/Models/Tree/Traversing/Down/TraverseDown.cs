namespace BlauwtipjeApp.Core.Models.Tree.Traversing.Down
{
    public class TraverseDown : IForwardsTraversable
    {
        private TreeNode next;

        public TraverseDown(TreeNode next)
        {
            this.next = next;
        }

        public TreeNode Next()
        {
            return next;
        }
    }
}
