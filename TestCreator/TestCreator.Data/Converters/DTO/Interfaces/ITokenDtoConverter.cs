using Token = TestCreatorWebApp.Data.Models.DTO.Token;

namespace TestCreatorWebApp.Data.Converters.DTO.Interfaces
{
    public interface ITokenDtoConverter
    {
        Token Convert(Models.DAO.Token token);
    }
}
