using System;
using System.Threading.Tasks;
using Autofac;
using TestCreator.Data.Commands.Handlers.Common.Interfaces;
using TestCreator.Data.Commands.Interfaces;
using TestCreator.WebApp.Data.Commands.Interfaces;

namespace TestCreator.WebApp.Data.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispatch<TParameter>(TParameter command) where TParameter : ICommand
        {
            var handler = _context.Resolve<ICommandHandler<TParameter>>();
            handler.Handle(command);
        }

        public async Task DispatchAsync<TParameter>(TParameter command) where TParameter : ICommand
        {
            var handler = _context.Resolve<ICommandHandler<TParameter>>();
            await handler.HandleAsync(command);
        }
    }
}
