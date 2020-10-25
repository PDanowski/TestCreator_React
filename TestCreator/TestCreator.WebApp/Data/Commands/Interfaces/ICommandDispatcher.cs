using System.Threading.Tasks;
using TestCreator.Data.Commands.Interfaces;

namespace TestCreator.WebApp.Data.Commands.Interfaces
{
    public interface ICommandDispatcher
    {
        void Dispatch<TParameter>(TParameter command) where TParameter : ICommand;

        Task DispatchAsync<TParameter>(TParameter command) where TParameter : ICommand;
    }
}
