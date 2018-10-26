using System.Xml.Serialization;
using BlauwtipjeApp.Core.Models.Tree.Traversing.Up;

namespace BlauwtipjeApp.Core.Models.Tree.Determination
{
    public class Choice : TreeNode
    {
        [XmlAttribute(AttributeName = "nextQuestionId")]
        public int NextQuestionId { get; set; }

        [XmlAttribute(AttributeName = "nextResultId")]
        public int NextResultId { get; set; }

        public bool ShouldSerializeNextQuestionId()
        {
            return NextQuestionId != default(int);
        }

        public bool ShouldSerializeNextResultId()
        {
            return NextResultId != default(int);
        }

        public new TreeNode TryNext()
        {
            var node = base.TryNext();
            if (node != null)
                node.BackwardsTraversableBehavior = new TraverseUp(TryBack());
            return node;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Choice);
        }

        public bool Equals(Choice obj)
        {
            return obj != null
                   && base.Equals(obj)
                   && obj.NextQuestionId == NextQuestionId
                   && obj.NextResultId == NextResultId;
        }
    }
}
