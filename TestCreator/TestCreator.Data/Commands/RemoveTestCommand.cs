using TestCreator.Data.Commands.Interfaces;

namespace TestCreator.Data.Commands
{
    public class RemoveTestCommand : ICommand
    {
        public int Id { get; set; }
    }
}
