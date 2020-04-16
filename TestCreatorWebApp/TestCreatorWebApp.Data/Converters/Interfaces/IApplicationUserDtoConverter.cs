using ApplicationUser = TestCreatorWebApp.Data.Models.DTO.ApplicationUser;
using DAO = TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Data.Converters.Interfaces
{
    public interface IApplicationUserDtoConverter
    {
        ApplicationUser Convert(DAO.ApplicationUser applicationUser);
    }
}
