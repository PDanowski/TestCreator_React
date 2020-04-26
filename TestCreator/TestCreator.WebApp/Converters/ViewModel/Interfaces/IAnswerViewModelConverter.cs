using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.ViewModel.Interfaces
{
    public interface IAnswerViewModelConverter
    {
        AnswerViewModel Convert(Answer answer);
    }
}
