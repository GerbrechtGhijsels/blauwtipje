using System;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Services.Tree;

namespace BlauwtipjeApp.Core
{
    public static class TreeManager<TResult> where TResult : Result
    {
        private static DeterminationTree<TResult> tree;

        public static void Initialize()
        {
            if (Settings.UpdateWasNotCompleted) return;
            tree = ServiceLocator.GetService<ITreeFactory<TResult>>().BuildTree();
        }

        public static void Reinitialize()
        {
            tree = ServiceLocator.GetService<ITreeFactory<TResult>>().BuildTree();
            ServiceLocator.GetService<IDeterminationInProgressDAO>().DeleteDeterminationInProgress();
        }

        public static DeterminationTree<TResult> GetTree()
        {
            return tree;
        }
    }
}
