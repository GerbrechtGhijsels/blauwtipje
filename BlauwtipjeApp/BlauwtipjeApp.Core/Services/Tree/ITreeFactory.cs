using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;

namespace BlauwtipjeApp.Core.Services.Tree
{
    /// <summary>
    /// Interface for the building a <see cref="DeterminationTree{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface ITreeFactory<TResult> where TResult : Result
    {
        /// <summary>
        /// Builds the tree.
        /// </summary>
        /// <returns><see cref="DeterminationTree{TResult}"/></returns>
        DeterminationTree<TResult> BuildTree();
    }
}
