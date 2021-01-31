using System.Collections.Generic;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.ViewModel.Interfaces
{
    public interface ITestAttemptAnswerViewModelConverter
    {
        IEnumerable<TestAttemptAnswerViewModel> Convert(IEnumerable<Answer> answers);
    }
}
