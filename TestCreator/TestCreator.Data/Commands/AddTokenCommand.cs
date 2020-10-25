using TestCreator.Data.Commands.Interfaces;
using TestCreator.Data.Models.DTO;

namespace TestCreator.Data.Commands
{
    public class AddTokenCommand : ICommand
    {
        public Token Token { get; set; }
    }
}
