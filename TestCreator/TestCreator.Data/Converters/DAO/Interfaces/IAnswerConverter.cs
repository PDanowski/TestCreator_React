using TestCreatorWebApp.Data.Models.DTO;

namespace TestCreatorWebApp.Data.Converters.DAO.Interfaces
{
    public interface IAnswerConverter
    {
        Models.DAO.Answer Convert(Answer answer);
    }
}
