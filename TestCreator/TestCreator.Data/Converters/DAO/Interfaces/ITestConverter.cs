using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Converters.DAO.Interfaces
{
    public interface ITestConverter
    {
        Test Convert(Models.DTO.Test test);
    }
}
