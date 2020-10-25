using TestCreator.Data.Commands.Interfaces;

namespace TestCreator.Data.Commands
{
    public class RemoveTokenCommand : ICommand
    {
        public int Id { get; set; }
    }
}
