using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;

namespace BlauwtipjeApp.Core.Services.Tree.Decorators
{
    /// <summary>
    /// Interface for decorating a <see cref="DeterminationTree{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface ITreeDecorator<TResult> where TResult : Result
    {
        /// <summary>
        /// Decorates the tree.
        /// </summary>
        /// <param name="simpleTree">The undecorated tree.</param>
        void DecorateTree(DeterminationTree<TResult> simpleTree);
    }
}
