using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.DTO.Interfaces
{
    public interface IApplicationUserDtoConverter
    {
        ApplicationUser Convert(UserViewModel viewModel);
    }
}
