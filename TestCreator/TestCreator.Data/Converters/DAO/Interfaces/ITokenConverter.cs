using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Converters.DAO.Interfaces
{
    public interface ITokenConverter
    {
        Token Convert(Models.DTO.Token token);
    }
}
