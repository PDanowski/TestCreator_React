using Answer = TestCreatorWebApp.Data.Models.DTO.Answer;
using DAO = TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Data.Converters.Interfaces
{
    public interface IAnswerDtoConverter
    {
        Answer Convert(DAO.Answer answer);
    }
}
