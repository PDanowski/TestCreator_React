using Test = TestCreator.Data.Models.DTO.Test;

namespace TestCreator.Data.Converters.DTO.Interfaces
{
    public interface ITestDtoConverter
    {
        Test Convert(Models.DAO.Test test);
    }
}
