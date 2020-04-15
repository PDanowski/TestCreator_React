using DAO = TestCreatorWebApp.Data.Models.DAO;
using Result = TestCreatorWebApp.Data.Models.DTO.Result;

namespace TestCreatorWebApp.Data.Converters.Interfaces
{
    public interface IResultDtoConverter
    {
        Result Convert(DAO.Result result);
    }
}
