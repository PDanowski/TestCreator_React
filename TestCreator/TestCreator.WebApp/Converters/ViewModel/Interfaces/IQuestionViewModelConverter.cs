using System.Collections.Generic;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.ViewModel.Interfaces
{
    public interface IQuestionViewModelConverter
    {
        QuestionViewModel Convert(Question question);
        IEnumerable<QuestionViewModel> Convert(IEnumerable<Question> questions);
    }
}
