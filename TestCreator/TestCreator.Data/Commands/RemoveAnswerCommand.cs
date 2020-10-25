using TestCreator.Data.Commands.Interfaces;

namespace TestCreator.Data.Commands
{
    public class RemoveAnswerCommand : ICommand
    {
        public int Id { get; set; }
    }
}
