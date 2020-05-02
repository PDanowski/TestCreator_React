using System.Collections.Generic;
using Answer = TestCreator.Data.Models.DTO.Answer;

namespace TestCreator.Data.Converters.DTO.Interfaces
{
    public interface IAnswerDtoConverter
    {
        Answer Convert(Models.DAO.Answer answer);
        IEnumerable<Answer> Convert(IEnumerable<Models.DAO.Answer> answers);
    }
}
