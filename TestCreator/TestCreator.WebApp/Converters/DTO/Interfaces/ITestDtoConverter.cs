using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.DTO.Interfaces
{
    public interface ITestDtoConverter
    {
        Test Convert(TestViewModel viewModel);
    }
}
