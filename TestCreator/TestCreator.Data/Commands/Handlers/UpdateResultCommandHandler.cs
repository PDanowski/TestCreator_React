using System;
using System.Linq;
using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Database;
using TestCreator.Data.Exceptions;

namespace TestCreator.Data.Commands.Handlers
{
    public class UpdateResultCommandHandler : CommandHandler<UpdateResultCommand>
    {
        public UpdateResultCommandHandler(EfDbContext context) : base(context)
        {
        }

        protected override void DoHandle(UpdateResultCommand request)
        {
            var result = DbContext.Results.FirstOrDefault(t => t.Id.Equals(request.Result.Id));

            if (result == null)
            {
                throw new DataLayerException($"Update failed. Entity with Id: {request.Result.Id} not found");
            }

            result.TestId = request.Result.TestId;
            result.Text = request.Result.Text;
            result.Notes = request.Result.Notes;
            result.MinValue = request.Result.MinValue;
            result.MaxValue = request.Result.MaxValue;

            result.LastModificationDate = DateTime.Now;

            DbContext.SaveChanges();
        }

        protected override async Task DoHandleAsync(UpdateResultCommand request)
        {
            var result = DbContext.Results.FirstOrDefault(t => t.Id.Equals(request.Result.Id));

            if (result == null)
            {
                throw new DataLayerException($"Update failed. Entity with Id: {request.Result.Id} not found");
            }

            result.TestId = request.Result.TestId;
            result.Text = request.Result.Text;
            result.Notes = request.Result.Notes;
            result.MinValue = request.Result.MinValue;
            result.MaxValue = request.Result.MaxValue;

            result.LastModificationDate = DateTime.Now;

            await DbContext.SaveChangesAsync();
        }
    }
}
