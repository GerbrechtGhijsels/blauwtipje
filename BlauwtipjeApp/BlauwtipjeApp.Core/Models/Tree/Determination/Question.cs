using System.Collections.Generic;
using System.Xml.Serialization;

namespace BlauwtipjeApp.Core.Models.Tree.Determination
{
    public class Question : TreeNode
    {
        public Question()
        {
            Choices = new List<Choice>();
        }

        [XmlArray("choices")]
        [XmlArrayItem("choice")]
        public List<Choice> Choices { get; set; }

        public bool ShouldSerializeChoices()
        {
            return Choices.Count > 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Question);
        }

        public bool Equals(Question obj)
        {
            return obj != null
                   && base.Equals(obj)
                   && obj.Choices.Count == Choices.Count;
        }
    }
}
