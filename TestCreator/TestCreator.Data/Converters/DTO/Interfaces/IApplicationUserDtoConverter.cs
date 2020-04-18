using ApplicationUser = TestCreator.Data.Models.DTO.ApplicationUser;

namespace TestCreator.Data.Converters.DTO.Interfaces
{
    public interface IApplicationUserDtoConverter
    {
        ApplicationUser Convert(Models.DAO.ApplicationUser applicationUser);
    }
}
