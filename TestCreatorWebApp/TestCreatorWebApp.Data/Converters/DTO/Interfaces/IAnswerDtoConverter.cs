using Answer = TestCreatorWebApp.Data.Models.DTO.Answer;

namespace TestCreatorWebApp.Data.Converters.DTO.Interfaces
{
    public interface IAnswerDtoConverter
    {
        Answer Convert(Models.DAO.Answer answer);
    }
}
