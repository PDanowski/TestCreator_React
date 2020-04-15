using DAO = TestCreatorWebApp.Data.Models.DAO;
using Question = TestCreatorWebApp.Data.Models.DTO.Question;

namespace TestCreatorWebApp.Data.Converters.Interfaces
{
    public interface IQuestionDtoConverter
    {
        Question Convert(DAO.Question question);
    }
}
