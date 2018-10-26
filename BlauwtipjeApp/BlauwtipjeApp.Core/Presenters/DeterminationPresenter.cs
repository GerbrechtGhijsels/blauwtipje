using System.Threading.Tasks;
using BlauwtipjeApp.Core.Components.Dialog;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;

namespace BlauwtipjeApp.Core.Presenters
{
    public class DeterminationPresenter<T> : BasePresenter<IDeterminationView<T>> where T : Result
    {
        private DeterminationTree<T> tree;
        private IDeterminationInProgressDAO dao;
        private DeterminationInProgress determinationInProgress;

        public DeterminationPresenter(IDeterminationView<T> View, DeterminationTree<T> tree, IDeterminationInProgressDAO dao) : base(View)
        {
            this.tree = tree;
            this.dao = dao;
        }

        public override async Task OnViewCreate()
        {
            View.SetCurrentQuestion(tree.Questions[0]);
            if (!dao.HasDeterminationInProgress())
            {
                determinationInProgress = new DeterminationInProgress();
                return;
            }

            var dialogResult = await View.ShowAlertDialog(new AlertDialogConfig
            {
                Title = "Er is een vorige determinatie beschikbaar",
                Message = "Wilt u verder gaan met deze determinatie?",
                PositiveButtonText = "Ja",
                NegativeButtonText = "Nee"
            });

            switch (dialogResult)
            {
                case DialogResult.Positive:
                    RestorePreviousDetermination();
                    break;
                default:
                    determinationInProgress = new DeterminationInProgress();
                    break;
            }
        }

        private void RestorePreviousDetermination()
        {
            determinationInProgress = dao.GetDeterminationInProgress();

            var previousQuestionId = determinationInProgress.QuestionID;
            if (previousQuestionId != 0)
                View.SetCurrentQuestion(tree.Questions.Find(question => question.Id == previousQuestionId));

            var image = determinationInProgress.DeterminationPicture;
            View.SetDeterminationPicture(image);
        }

        public void OnUserSelectPictureRequest()
        {
            View.ShowPhotoSelector();
        }

        public void OnUserSelectPictureResponse(byte[] picture)
        {
            SetDeterminationPictureToViewAndSaveIt(picture);
        }

        private void SetDeterminationPictureToViewAndSaveIt(byte[] picture)
        {
            View.SetDeterminationPicture(picture);
            determinationInProgress.DeterminationPicture = picture;
            dao.StoreDeterminationInProgress(determinationInProgress);
        }

        public void OnChoiceClicked(int selectedChoice)
        {
            var choice = View.GetCurrentQuestion().Choices[selectedChoice];
            var next = choice.TryNext();

            var nextQuestion = next as Question;
            var nextResult = next as T;
            if (nextQuestion != null)
            {
                SetQuestionToViewAndSaveProgress(nextQuestion);
            }

            if (nextResult != null)
            {
                dao.DeleteDeterminationInProgress();
                View.ShowResult(nextResult);
            }
        }

        public void OnChoiceImageClicked(int selectedChoice)
        {
            var choice = View.GetCurrentQuestion().Choices[selectedChoice];
            View.ShowImageGallery(choice.ImageIdList);
        }

        public override async Task<bool> OnBackButtonClicked()
        {
            var question = View.GetCurrentQuestion().TryBack();
            if (question != null)
            {
                SetQuestionToViewAndSaveProgress((Question)question);
            }
            else
            {
                var dialogResult = await View.ShowAlertDialog(new AlertDialogConfig
                {
                    Title = "Determineren",
                    Message = "Weet u zeker dat u wilt stoppen?",
                    PositiveButtonText = "Ja",
                    NegativeButtonText = "Nee"
                });

                if (dialogResult == DialogResult.Positive)
                {
                    dao.DeleteDeterminationInProgress();
                    return false;
                }
            }
            return true;
        }

        private void SetQuestionToViewAndSaveProgress(Question question)
        {
            if (question.Id != 0)
            {
                determinationInProgress.QuestionID = question.Id;
                dao.StoreDeterminationInProgress(determinationInProgress);
            }
            View.SetCurrentQuestion(question);
        }

        public void OnInfoButtonClicked()
        {
            View.ShowAlertDialog(new AlertDialogConfig
            {
                Title = "Info",
                Message = View.GetCurrentQuestion().Note,
                PositiveButtonText = "Ok"
            });
        }
    }

    public interface IDeterminationView<TResult> : IView where TResult : Result
    {
        void SetDeterminationPicture(byte[] picture);
        void SetCurrentQuestion(Question question);
        Question GetCurrentQuestion();
        void ShowPhotoSelector();
        void ShowResult(TResult result);
    }
}
