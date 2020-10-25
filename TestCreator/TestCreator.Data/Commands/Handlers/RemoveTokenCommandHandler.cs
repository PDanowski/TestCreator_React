using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Database;
using TestCreator.Data.Exceptions;
using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Commands.Handlers
{
    public class RemoveTokenCommandHandler : CommandHandler<RemoveTokenCommand>
    {
        public RemoveTokenCommandHandler(EfDbContext context) : base(context)
        {
        }

        protected override void DoHandle(RemoveTokenCommand request)
        {
            var token = new Token
            {
                Id = request.Id
            };

            if (token == null)
            {
                throw new DataLayerException($"Deletion failed. Entity with Id: {request.Id} not found");
            }

            DbContext.Tokens.Remove(token);
            DbContext.SaveChanges();
        }

        protected override async Task DoHandleAsync(RemoveTokenCommand request)
        {
            var token = new Token
            {
                Id = request.Id
            };

            if (token == null)
            {
                throw new DataLayerException($"Deletion failed. Entity with Id: {request.Id} not found");
            }

            DbContext.Tokens.Remove(token);
            await DbContext.SaveChangesAsync();
        }
    }
}
