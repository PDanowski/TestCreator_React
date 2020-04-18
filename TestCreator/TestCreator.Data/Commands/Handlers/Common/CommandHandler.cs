using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common.Interfaces;
using TestCreator.Data.Commands.Interfaces;
using TestCreator.Data.Database;

namespace TestCreator.Data.Commands.Handlers.Common
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
