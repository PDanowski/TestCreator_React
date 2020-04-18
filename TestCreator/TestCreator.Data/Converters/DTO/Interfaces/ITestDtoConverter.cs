using Test = TestCreatorWebApp.Data.Models.DTO.Test;

namespace TestCreatorWebApp.Data.Converters.DTO.Interfaces
{
    public interface ITestDtoConverter
    {
        Test Convert(Models.DAO.Test test);
    }
}
