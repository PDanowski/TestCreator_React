using DAO = TestCreatorWebApp.Data.Models.DAO;
using Token = TestCreatorWebApp.Data.Models.DTO.Token;

namespace TestCreatorWebApp.Data.Converters.Interfaces
{
    public interface ITokenDtoConverter
    {
        Token Convert(DAO.Token token);
    }
}
