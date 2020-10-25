using TestCreator.Data.Commands.Interfaces;
using TestCreator.Data.Models.DTO;

namespace TestCreator.Data.Commands
{
    public class AddQuestionCommand : ICommand
    {
        public Question Question { get; set; }
    }
}
