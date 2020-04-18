using System.Threading.Tasks;
using TestCreatorWebApp.Data.Commands.Handlers.Common.Interfaces;
using TestCreatorWebApp.Data.Commands.Interfaces;
using TestCreatorWebApp.Data.Database;

namespace TestCreatorWebApp.Data.Commands.Handlers
{
    public abstract class CommandHandler<TRequest> : ICommandHandler<TRequest>
            where TRequest : ICommand
    {
        protected EfDbContext DbContext;

        protected CommandHandler(EfDbContext context)
        {
            DbContext = context;
        }

        public void Handle(TRequest command)
        {
            DoHandle(command);
        }

        public async Task HandleAsync(TRequest command)
        {
            await DoHandleAsync(command);
        }

        // Protected methods
        protected abstract void DoHandle(TRequest request);

        protected abstract Task DoHandleAsync(TRequest request);
    }
}
