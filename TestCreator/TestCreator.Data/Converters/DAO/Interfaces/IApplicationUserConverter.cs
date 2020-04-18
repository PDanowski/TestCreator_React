using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Converters.DAO.Interfaces
{
    public interface IApplicationUserConverter
    {
        ApplicationUser Convert(Models.DTO.ApplicationUser applicationUser);
    }
}
