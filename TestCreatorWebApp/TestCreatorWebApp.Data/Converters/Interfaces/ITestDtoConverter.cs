using DAO = TestCreatorWebApp.Data.Models.DAO;
using Test = TestCreatorWebApp.Data.Models.DTO.Test;

namespace TestCreatorWebApp.Data.Converters.Interfaces
{
    public interface ITestDtoConverter
    {
        Test Convert(DAO.Test test);
    }
}
