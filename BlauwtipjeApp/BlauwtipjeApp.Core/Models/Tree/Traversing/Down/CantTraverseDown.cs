using System;

namespace BlauwtipjeApp.Core.Models.Tree.Traversing.Down
{
    public class CantTraverseDown : IForwardsTraversable
    {
        public TreeNode Next()
        {
            throw new InvalidOperationException("Cannot traverse further down.");
        }
    }
}
