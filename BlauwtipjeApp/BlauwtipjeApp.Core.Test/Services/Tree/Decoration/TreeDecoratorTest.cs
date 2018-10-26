using System;
using System.Collections.Generic;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Models.FileManagement;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Services.Tree.Decorators.Exceptions;
using BlauwtipjeApp.Core.Services.Tree.Decorators.Impl;
using BlauwtipjeApp.Core.Test.Fakes.Daos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Services.Tree.Decoration
{
    [TestClass]
    public class TreeDecoratorTest
    {
        private ILocalResourceDAO fakeResourceDatabase;

        private List<Image> imageList;

        private List<Resource> resourceList;

        [TestInitialize]
        public void SetupFakeResourceDatabase()
        {
            imageList = new List<Image>
            {
                new Image
                {
                    XmlId = 1,
                    Name = "img1",
                    Extension = "png"
                },
                new Image
                {
                    XmlId = 2,
                    Name = "img2",
                    Extension = "jpeg"
                }
            };

            resourceList = new List<Resource>
            {
                new Resource()
                {
                    Name = imageList[0].Filename,
                    Etag = "1",
                    Content = new byte[]
                    {
                        1
                    }
                },
                new Resource()
                {
                    Name = imageList[1].Filename,
                    Etag = "2",
                    Content = new byte[]
                    {
                        2
                    }
                },
            };

            fakeResourceDatabase = new FakeLocalResourceDAO(resourceList);
        }

        #region Empty Tree
        [TestMethod]
        public void DecorateEmptyTreeTest()
        {
            // Arrange
            var tree = new DeterminationTree<Result>();
            var decorator = new TreeDecorator<Result>(fakeResourceDatabase);

            // Act
            decorator.DecorateTree(tree);

            // Assert
            Assert.AreEqual(0, tree.Questions.Count);
            Assert.AreEqual(0, tree.Results.Count);
            Assert.AreEqual(0, tree.Images.Count);
        }
        #endregion

        #region OneLayer Tree
        private readonly DeterminationTree<Result> _oneLayerTree = new DeterminationTree<Result>
        {
            Questions = new List<Question>()
            {
                new Question()
                {
                    Id = 1,
                    Choices = new List<Choice>()
                    {
                        new Choice()
                        {
                            NextResultId = 1
                        }
                    }
                }
            },
            Results = new List<Result>()
            {
                new Result
                {
                    Id = 1
                }
            }
        };

        [TestMethod]
        public void DecorateOneLayerTreeTest()
        {
            // Arrange
            var tree = _oneLayerTree;
            var decorator = new TreeDecorator<Result>(fakeResourceDatabase);

            // Act
            decorator.DecorateTree(tree);

            // Assert
            var nextNode = tree.Questions[0].Choices[0].TryNext();
            Assert.IsInstanceOfType(nextNode, typeof(Result));
            Assert.AreEqual(nextNode.Id, 1);

            var backNode = tree.Results[0].TryBack();
            Assert.IsInstanceOfType(backNode, typeof(Question));
            Assert.AreEqual(backNode.Id, 1);
        }
        #endregion

        #region TwoLayer Tree
        private readonly DeterminationTree<Result> _twoLayerTree = new DeterminationTree<Result>()
        {
            Questions = new List<Question>()
            {
                new Question()
                {
                    Id = 1,
                    Choices = new List<Choice>()
                    {
                        new Choice()
                        {
                            NextResultId = 1
                        },
                        new Choice()
                        {
                            NextQuestionId = 2
                        }
                    }
                },
                new Question()
                {
                    Id = 2,
                    Choices = new List<Choice>()
                    {
                        new Choice()
                        {
                            NextResultId = 2
                        },
                    }
                }
            },
            Results = new List<Result>()
            {
                new Result
                {
                    Id = 1
                },
                new Result()
                {
                    Id = 2
                }
            }
        };

        [TestMethod]
        public void DecorateTwoLayerTreeTest()
        {
            // Arrange
            var tree = _twoLayerTree;
            var decorator = new TreeDecorator<Result>(fakeResourceDatabase);

            // Act
            decorator.DecorateTree(tree);

            // Assert
            var result1Node = tree.Questions[0].Choices[0].TryNext();
            Assert.IsInstanceOfType(result1Node, typeof(Result));
            Assert.AreEqual(result1Node.Id, 1);

            var question2Node = tree.Questions[0].Choices[1].TryNext();
            Assert.IsInstanceOfType(question2Node, typeof(Question));
            Assert.AreEqual(question2Node.Id, 2);

            var result2Node = tree.Questions[1].Choices[0].TryNext();
            Assert.IsInstanceOfType(result2Node, typeof(Result));
            Assert.AreEqual(result2Node.Id, 2);
        }
        #endregion

        #region QuestionImages
        [TestMethod]
        public void DecorateQuestionsWithImagesTest()
        {
            // Arrange
            var tree = new DeterminationTree<Result>()
            {
                Questions = new List<Question>()
                {
                    new Question()
                    {
                        ImageIdList = new List<int>
                        {
                            1,
                            2
                        }
                    }
                },
                Images = imageList
            };
            var decorator = new TreeDecorator<Result>(fakeResourceDatabase);

            // Act
            decorator.DecorateTree(tree);

            // Assert
            var questionImageList = tree.Questions[0].ImageList;
            Assert.AreEqual(2, questionImageList.Count);

            Assert.IsTrue(questionImageList[0].IsEquivalentTo(resourceList[0]));
            Assert.IsTrue(questionImageList[1].IsEquivalentTo(resourceList[1]));
        }
        #endregion

        #region ChoiceImages
        [TestMethod]
        public void DecorateChoicesWithImagesTest()
        {
            // Arrange
            var tree = new DeterminationTree<Result>()
            {
                Questions = new List<Question>()
                {
                    new Question()
                    {
                        Choices = new List<Choice>
                        {
                            new Choice
                            {
                                ImageIdList = new List<int>
                                {
                                    1,
                                    2
                                }
                            }
                        }
                    }
                },
                Images = imageList
            };
            var decorator = new TreeDecorator<Result>(fakeResourceDatabase);

            // Act
            decorator.DecorateTree(tree);

            // Assert
            var choiceImageList = tree.Questions[0].Choices[0].ImageList;
            Assert.AreEqual(2, choiceImageList.Count);

            Assert.IsTrue(choiceImageList[0].IsEquivalentTo(resourceList[0]));
            Assert.IsTrue(choiceImageList[1].IsEquivalentTo(resourceList[1]));
        }
        #endregion

        #region ResultImages
        [TestMethod]
        public void DecorateResultsWithImagesTest()
        {
            // Arrange
            var tree = new DeterminationTree<Result>()
            {
                Results = new List<Result>()
                {
                    new Result()
                    {
                        ImageIdList = new List<int>
                        {
                            1,
                            2
                        }
                    }
                },
                Images = imageList
            };
            var decorator = new TreeDecorator<Result>(fakeResourceDatabase);

            // Act
            decorator.DecorateTree(tree);

            // Assert
            var resultImageList = tree.Results[0].ImageList;
            Assert.AreEqual(2, resultImageList.Count);

            Assert.IsTrue(resultImageList[0].IsEquivalentTo(resourceList[0]));
            Assert.IsTrue(resultImageList[1].IsEquivalentTo(resourceList[1]));
        }
        #endregion

        #region ExceptionTests
        [TestMethod]
        public void CannotFindImageIdInImageListExceptionTest()
        {
            // Arrange
            var tree = new DeterminationTree<Result>()
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        ImageIdList = new List<int>
                        {
                            1
                        },
                        Choices = new List<Choice>
                        {
                            new Choice
                            {
                                ImageIdList = new List<int>
                                {
                                    2
                                }
                            }
                        }
                    }
                },
                Results = new List<Result>()
                {
                    new Result()
                    {
                        ImageIdList = new List<int>
                        {
                            3
                        }
                    }
                }
            };
            var decorator = new TreeDecorator<Result>(fakeResourceDatabase);

            // Act
            string result = "";
            try
            {
                decorator.DecorateTree(tree);
            }
            catch (TreeDecorateException ex)
            {
                result = ex.Message;
            }

            // Assert
            Assert.AreEqual("ImageId '1' referenced in Question '0' does not point to an image in the <images> list" + Environment.NewLine +
                            "ImageId '2' referenced in Choice '1' of Question '0' does not point to an image in the <images> list" + Environment.NewLine +
                            "ImageId '3' referenced in Result '0' does not point to an image in the <images> list"
                            , result);
        }

        [TestMethod]
        public void CannotFindImageInLocalDatabaseExceptionTest()
        {
            // Arrange
            var tree = new DeterminationTree<Result>()
            {
                Images = new List<Image>
                {
                    new Image
                    {
                        XmlId = 1,
                        Name = "SomeImage",
                        Extension = "png"
                    }
                }
            };
            var decorator = new TreeDecorator<Result>(fakeResourceDatabase);

            // Act
            string result = "";
            try
            {
                decorator.DecorateTree(tree);
            }
            catch (TreeDecorateException ex)
            {
                result = ex.Message;
            }

            // Assert
            Assert.AreEqual("Image with id '1' and filename 'SomeImage.png' not found in the local dao"
                , result);
        }

        [TestMethod]
        public void CannotLinkChoiceExceptionTest()
        {
            // Arrange
            var tree = new DeterminationTree<Result>()
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = 1,
                        Choices = new List<Choice>
                        {
                            new Choice
                            {
                                NextQuestionId = 11
                            },
                            new Choice
                            {
                                NextResultId = 22
                            },
                            new Choice
                            {
                                Id = 5,
                                NextQuestionId = 33,
                                NextResultId = 44
                            }
                        }
                    }
                }
            };
            var decorator = new TreeDecorator<Result>(fakeResourceDatabase);

            // Act
            string result = "";
            try
            {
                decorator.DecorateTree(tree);
            }
            catch (TreeDecorateException ex)
            {
                result = ex.Message;
            }

            // Assert
            Assert.AreEqual("NextQuestionId '11' in Choice '1' of Question '1' does not point to something" + Environment.NewLine +
                            "NextResultId '22' in Choice '2' of Question '1' does not point to something" + Environment.NewLine +
                            "Cannot have both a nextQuestionId AND a nextResultId in a choice" + Environment.NewLine +
                            "NextQuestionId '33' in Choice '5' of Question '1' does not point to something" + Environment.NewLine +
                            "NextResultId '44' in Choice '5' of Question '1' does not point to something"
                            , result);
        }
        #endregion
    }
}
