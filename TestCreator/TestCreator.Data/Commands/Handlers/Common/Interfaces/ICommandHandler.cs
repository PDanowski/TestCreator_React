using System.Threading.Tasks;
using TestCreator.Data.Commands.Interfaces;

namespace TestCreator.Data.Commands.Handlers.Common.Interfaces
{
    public interface ICommandHandler<in TParameter> where TParameter : ICommand
    {
        void Handle(TParameter command);

        Task HandleAsync(TParameter command);
    }
}
