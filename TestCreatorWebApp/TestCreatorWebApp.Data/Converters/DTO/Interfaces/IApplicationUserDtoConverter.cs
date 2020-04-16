using ApplicationUser = TestCreatorWebApp.Data.Models.DTO.ApplicationUser;

namespace TestCreatorWebApp.Data.Converters.DTO.Interfaces
{
    public interface IApplicationUserDtoConverter
    {
        ApplicationUser Convert(Models.DAO.ApplicationUser applicationUser);
    }
}
