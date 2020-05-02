using System.Collections.Generic;
using Question = TestCreator.Data.Models.DTO.Question;

namespace TestCreator.Data.Converters.DTO.Interfaces
{
    public interface IQuestionDtoConverter
    {
        Question Convert(Models.DAO.Question question);
        IEnumerable<Question> Convert(IEnumerable<Models.DAO.Question> questions);
    }
}
