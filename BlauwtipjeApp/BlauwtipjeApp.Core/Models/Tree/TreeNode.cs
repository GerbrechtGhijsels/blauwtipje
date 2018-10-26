using System.Collections.Generic;
using System.Xml.Serialization;
using BlauwtipjeApp.Core.Models.Tree.Traversing;

namespace BlauwtipjeApp.Core.Models.Tree
{
    public class TreeNode
    {
        public TreeNode()
        {
            ImageList = new List<Image>();
        }

        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        public bool ShouldSerializeId()
        {
            return Id != default(int);
        }

        [XmlElement(ElementName = "text")]
        public string Text { get; set; }

        [XmlElement(ElementName = "note")]
        public string Note { get; set; }

        [XmlArray("images")]
        [XmlArrayItem("image")]
        public List<int> ImageIdList { get; set; }

        [XmlIgnore]
        public List<Image> ImageList { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as TreeNode);
        }

        public bool Equals(TreeNode obj)
        {
            return obj != null
                   && obj.Id == this.Id
                   && obj.Text == this.Text
                   && obj.Note == this.Note;
        }

        #region Tree traversal stuff
        [XmlIgnore]
        public IForwardsTraversable ForwardsTraversableBehavior { get; set; }

        public TreeNode TryNext()
        {
            return ForwardsTraversableBehavior.Next();
        }

        [XmlIgnore]
        public IBackwardsTraversable BackwardsTraversableBehavior { get; set; }

        public TreeNode TryBack()
        {
            return BackwardsTraversableBehavior?.Back();
        }
        #endregion
    }
}
