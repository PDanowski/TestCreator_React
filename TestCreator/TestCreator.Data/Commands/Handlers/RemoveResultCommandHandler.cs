using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Database;
using TestCreator.Data.Exceptions;
using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Commands.Handlers
{
    public class RemoveResultCommandHandler : CommandHandler<RemoveResultCommand>
    {
        public RemoveResultCommandHandler(EfDbContext context) : base(context)
        {
        }

        protected override void DoHandle(RemoveResultCommand request)
        {
            var result = new Result
            {
                Id = request.Id
            };

            if (result == null)
            {
                throw new DataLayerException($"Deletion failed. Entity with Id: {request.Id} not found");
            }

            DbContext.Results.Remove(result);
            DbContext.SaveChanges();
        }

        protected override async Task DoHandleAsync(RemoveResultCommand request)
        {
            var result = new Result
            {
                Id = request.Id
            };

            if (result == null)
            {
                throw new DataLayerException($"Deletion failed. Entity with Id: {request.Id} not found");
            }

            DbContext.Results.Remove(result);
            await DbContext.SaveChangesAsync();
        }
    }
}
