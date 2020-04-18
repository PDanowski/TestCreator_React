using Question = TestCreatorWebApp.Data.Models.DTO.Question;

namespace TestCreatorWebApp.Data.Converters.DTO.Interfaces
{
    public interface IQuestionDtoConverter
    {
        Question Convert(Models.DAO.Question question);
    }
}
