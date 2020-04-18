using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Converters.DAO.Interfaces
{
    public interface IResultConverter
    {
        Result Convert(Models.DTO.Result result);
    }
}
