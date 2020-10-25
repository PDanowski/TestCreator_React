using TestCreator.Data.Commands.Interfaces;

namespace TestCreator.Data.Commands
{
    public class RemoveQuestionCommand : ICommand
    {
        public int Id { get; set; }
    }
}
