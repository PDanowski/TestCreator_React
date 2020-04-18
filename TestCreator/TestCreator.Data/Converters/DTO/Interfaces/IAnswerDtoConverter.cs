using Answer = TestCreator.Data.Models.DTO.Answer;

namespace TestCreator.Data.Converters.DTO.Interfaces
{
    public interface IAnswerDtoConverter
    {
        Answer Convert(Models.DAO.Answer answer);
    }
}
