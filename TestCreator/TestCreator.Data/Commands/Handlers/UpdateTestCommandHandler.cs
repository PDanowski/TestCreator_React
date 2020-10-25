using System;
using System.Linq;
using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Database;
using TestCreator.Data.Exceptions;

namespace TestCreator.Data.Commands.Handlers
{
    public class UpdateTestCommandHandler : CommandHandler<UpdateTestCommand>
    {
        public UpdateTestCommandHandler(EfDbContext context) : base(context)
        {
        }

        protected override void DoHandle(UpdateTestCommand request)
        {
            var test = DbContext.Tests.FirstOrDefault(t => t.Id.Equals(request.Test.Id));

            if (test == null)
            {
                throw new DataLayerException($"Update failed. Entity with Id: {request.Test.Id} not found");
            }

            test.Title = request.Test.Title;
            test.Description = request.Test.Description;
            test.Text = request.Test.Text;
            test.Notes = request.Test.Notes;

            test.LastModificationDate = DateTime.Now;

            DbContext.SaveChanges();

        }

        protected override async Task DoHandleAsync(UpdateTestCommand request)
        {
            var test = DbContext.Tests.FirstOrDefault(t => t.Id.Equals(request.Test.Id));

            if (test == null)
            {
                throw new DataLayerException($"Update failed. Entity with Id: {request.Test.Id} not found");
            }

            test.Title = request.Test.Title;
            test.Description = request.Test.Description;
            test.Text = request.Test.Text;
            test.Notes = request.Test.Notes;

            test.LastModificationDate = DateTime.Now;

            await DbContext.SaveChangesAsync();
        }
    }
}
