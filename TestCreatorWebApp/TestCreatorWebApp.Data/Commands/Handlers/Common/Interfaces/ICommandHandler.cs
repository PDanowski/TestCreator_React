using System.Threading.Tasks;
using TestCreatorWebApp.Data.Commands.Interfaces;

namespace TestCreatorWebApp.Data.Commands.Handlers.Common.Interfaces
{
    public interface ICommandHandler<in TParameter> where TParameter : ICommand
    {
        void Handle(TParameter command);

        Task HandleAsync(TParameter command);
    }
}
