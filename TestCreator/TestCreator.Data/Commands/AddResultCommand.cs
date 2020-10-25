using TestCreator.Data.Commands.Interfaces;
using TestCreator.Data.Models.DTO;

namespace TestCreator.Data.Commands
{
    public class AddResultCommand : ICommand
    {
        public Result Result { get; set; }
    }
}
