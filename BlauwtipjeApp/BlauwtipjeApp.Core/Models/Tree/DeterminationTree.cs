using System.Collections.Generic;
using System.Xml.Serialization;
using BlauwtipjeApp.Core.Models.Tree.Determination;

namespace BlauwtipjeApp.Core.Models.Tree
{
    [XmlRoot(ElementName = "data")]
    public class DeterminationTree<TResult> where TResult : Result
    {
        public DeterminationTree()
        {
            Questions = new List<Question>();
            Results = new List<TResult>();
            Images = new List<Image>();
        }

        [XmlArray("questions")]
        [XmlArrayItem("question")]
        public List<Question> Questions { get; set; }

        [XmlArray("results")]
        public List<TResult> Results { get; set;  }

        [XmlArray("images")]
        [XmlArrayItem("image")]
        public List<Image> Images { get; set; }

        [XmlElement(ElementName = "info")]
        public string Info { get; set; }
    }
}
