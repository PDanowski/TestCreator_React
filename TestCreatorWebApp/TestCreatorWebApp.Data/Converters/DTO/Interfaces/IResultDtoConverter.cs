using Result = TestCreatorWebApp.Data.Models.DTO.Result;

namespace TestCreatorWebApp.Data.Converters.DTO.Interfaces
{
    public interface IResultDtoConverter
    {
        Result Convert(Models.DAO.Result result);
    }
}
