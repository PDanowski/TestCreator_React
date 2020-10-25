using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.ViewModel.Interfaces
{
    public interface IUserViewModelConverter
    {
        UserViewModel Convert(ApplicationUser user);
    }
}
