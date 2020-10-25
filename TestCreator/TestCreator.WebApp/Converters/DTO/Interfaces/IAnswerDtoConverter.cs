using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.DTO.Interfaces
{
    public interface IAnswerDtoConverter
    {
        Answer Convert(AnswerViewModel viewModel);
    }
}
