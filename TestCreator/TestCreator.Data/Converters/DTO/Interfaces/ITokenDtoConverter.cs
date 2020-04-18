using Token = TestCreator.Data.Models.DTO.Token;

namespace TestCreator.Data.Converters.DTO.Interfaces
{
    public interface ITokenDtoConverter
    {
        Token Convert(Models.DAO.Token token);
    }
}
