using System.Xml.Serialization;
using BlauwtipjeApp.Core.Models.Tree.Traversing.Down;

namespace BlauwtipjeApp.Core.Models.Tree.Determination
{
    [XmlType(TypeName = "result")]
    public class Result : TreeNode
    {
        public Result()
        {
            ForwardsTraversableBehavior = new CantTraverseDown();
        }
    }
}
