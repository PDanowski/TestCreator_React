using TestCreator.Data.Models.DTO;

namespace TestCreator.Data.Converters.DAO.Interfaces
{
    public interface IAnswerConverter
    {
        Models.DAO.Answer Convert(Answer answer);
    }
}
