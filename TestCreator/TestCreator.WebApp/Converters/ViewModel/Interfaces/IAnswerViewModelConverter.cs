using System.Collections.Generic;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.ViewModel.Interfaces
{
    public interface IAnswerViewModelConverter
    {
        AnswerViewModel Convert(Answer answer);
        IEnumerable<AnswerViewModel> Convert(IEnumerable<Answer> answers);
    }
}
