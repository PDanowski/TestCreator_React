using System.Threading.Tasks;
using TestCreatorWebApp.Domain.Commands.Interfaces;

namespace TestCreatorWebApp.Domain.Commands.Handlers
{
    public interface ICommandHandler<in TParameter> where TParameter : ICommand
    {
        void Handle(TParameter command);

        Task HandleAsync(TParameter command);
    }
}
