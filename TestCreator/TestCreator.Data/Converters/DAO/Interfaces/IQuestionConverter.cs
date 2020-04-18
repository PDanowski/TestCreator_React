using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Converters.DAO.Interfaces
{
    public interface IQuestionConverter
    {
        Question Convert(Models.DTO.Question question);
    }
}
