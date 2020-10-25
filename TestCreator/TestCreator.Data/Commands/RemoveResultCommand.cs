using TestCreator.Data.Commands.Interfaces;

namespace TestCreator.Data.Commands
{
    public class RemoveResultCommand : ICommand
    {
        public int Id { get; set; }
    }
}
