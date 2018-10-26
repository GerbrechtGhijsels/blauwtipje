using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;

namespace BlauwtipjeApp.Core.Services.Tree.Converters.Xml
{
    /// <summary>
    /// Interface for converting a <see cref="DeterminationTree{TResult}"/> to and from an xml string.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IXmlTreeConverter<TResult> where TResult : Result
    {
        /// <summary>
        /// Converts a xml string to a <see cref="DeterminationTree{TResult}"/>.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns>The tree.</returns>
        DeterminationTree<TResult> FromXml(string xml);
        /// <summary>
        /// Converts a <see cref="DeterminationTree{TResult}"/> to a xml string.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <param name="output">The xml output.</param>
        void ToXml(DeterminationTree<TResult> tree, out string output);
    }
}
