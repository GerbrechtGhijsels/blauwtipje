using System;
using System.Collections.Generic;
using System.Linq;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Models.Tree.Traversing.Down;
using BlauwtipjeApp.Core.Models.Tree.Traversing.Up;
using BlauwtipjeApp.Core.Services.Tree.Decorators.Exceptions;

namespace BlauwtipjeApp.Core.Services.Tree.Decorators.Impl
{
    public class TreeDecorator<TResult> : ITreeDecorator<TResult> where TResult : Result
    {
        private readonly ILocalResourceDAO _dao;
        private IHtmlContentPreparer _htmlContentPreparer;
        private DeterminationTree<TResult> tree;
        private List<string> errors;

        public TreeDecorator(ILocalResourceDAO dao)
        {
            this._dao = dao;
        }

        /// <summary>
        /// Links every <see cref="Choice"/> to a <see cref="Question"/> or <see cref="Result"/>.
        /// Fills the <see cref="TreeNode.ImageList"/> of every <see cref="TreeNode"/> with content from the <see cref="ILocalResourceDAO"/>.
        /// </summary>
        /// <param name="simpleTree">The undecorated tree.</param>
        public void DecorateTree(DeterminationTree<TResult> simpleTree)
        {
            errors = new List<string>();
            tree = simpleTree;
            Decorate();
        }

        private void Decorate()
        {
            FillImages();
            _htmlContentPreparer = new HtmlContentPreparer(tree.Images);
            FillInfo();
            FillQuestions();
            FillResults();

            if (errors.Count > 0)
            {
                throw new TreeDecorateException(string.Join(Environment.NewLine, errors));
            }
        }

        private void FillInfo()
        {
            if (string.IsNullOrWhiteSpace(tree.Info)) return;
            tree.Info = _htmlContentPreparer.InjectImages(tree.Info);
        }

        #region Images
        private void FillImages()
        {
            foreach (var image in tree.Images)
            {
                var resource = _dao.Get(image.Filename);
                if (resource == null)
                    errors.Add($"Image with id '{image.XmlId}' and filename '{image.Filename}' not found in the local dao");
                else
                    image.Content = resource.Content;
            }
        }
        #endregion

        #region Questions
        private void FillQuestions()
        {
            foreach (var question in tree.Questions)
            {
                var faultyIds = DecorateNode(question);
                foreach (var id in faultyIds)
                    errors.Add($"ImageId '{id}' referenced in Question '{question.Id}' does not point to an image in the <images> list");
                FillChoices(question);
            }
        }
        #endregion

        #region Choices
        private void FillChoices(Question question)
        {
            foreach (var choice in question.Choices ?? Enumerable.Empty<Choice>())
            {
                var linkErrors = LinkChoice(choice);
                foreach (var error in linkErrors)
                    errors.Add($"{error} in Choice '{GetIdOfChoice(question, choice)}' of Question '{question.Id}' does not point to something");
                choice.BackwardsTraversableBehavior = new TraverseUp(question);

                if (tree.Images == null) continue;

                var faultyIds = DecorateNode(choice);
                foreach (var id in faultyIds)
                    errors.Add($"ImageId '{id}' referenced in Choice '{GetIdOfChoice(question, choice)}' of Question '{question.Id}' does not point to an image in the <images> list");
            }
        }

        private List<string> LinkChoice(Choice choice)
        {
            var linkErrors = new List<string>();
            var nextQuestionId = choice.NextQuestionId;
            var nextResultId = choice.NextResultId;

            if (nextQuestionId != default(int) && nextResultId != default(int))
                errors.Add("Cannot have both a nextQuestionId AND a nextResultId in a choice");

            if (choice.NextQuestionId != default(int))
            {
                var question = tree.Questions.Find(q => q.Id == choice.NextQuestionId);
                if (question == null)
                    linkErrors.Add("NextQuestionId '" + choice.NextQuestionId + "'");
                choice.ForwardsTraversableBehavior = new TraverseDown(question);
            }

            if (choice.NextResultId != default(int))
            {
                var result = tree.Results.Find(a => a.Id == choice.NextResultId);
                if (result == null)
                    linkErrors.Add("NextResultId '" + choice.NextResultId + "'");
                choice.ForwardsTraversableBehavior = new TraverseDown(result);
            }
            return linkErrors;
        }

        /// <summary>
        /// Gets the id of the choice.
        /// When it does not have one, give it a number regarding its position in the Choices list of the question.
        /// Only used to give meaningful error messages.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <param name="choice">The choice.</param>
        /// <returns></returns>
        private int GetIdOfChoice(Question question, Choice choice)
        {
            return (choice.Id == default(int) ? question.Choices.IndexOf(choice) + 1 : choice.Id);
        }
        #endregion

        #region Results
        private void FillResults()
        {
            foreach (var result in tree.Results)
            {
                if (tree.Images == null) continue;

                var faultyIds = DecorateNode(result);
                foreach (var id in faultyIds)
                    errors.Add("ImageId '" + id + "' referenced in " + typeof(TResult).Name + " '" + result.Id + "' does not point to an image in the <images> list");
            }
        }
        #endregion


        /// <summary>
        /// Decorates a <see cref="TreeNode"/>.
        /// This function loads the node's images using ids in its ImageIdList into its ImageList.
        /// It also injects Base64 strings of the images that the node has referenced in its Text field.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private List<int> DecorateNode(TreeNode node)
        {
            var faultyIds = new List<int>();

            foreach (var id in node.ImageIdList ?? Enumerable.Empty<int>())
            {
                var image = tree.Images.Find(i => i.XmlId == id);
                if (image == null)
                    faultyIds.Add(id);
                node.ImageList.Add(image);
            }

            if (!string.IsNullOrWhiteSpace(node.Text))
                node.Text = _htmlContentPreparer.InjectImages(node.Text);

            return faultyIds;
        }
    }
}
