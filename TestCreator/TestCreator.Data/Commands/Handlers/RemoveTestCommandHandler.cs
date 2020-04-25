using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Database;
using TestCreator.Data.Exceptions;
using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Commands.Handlers
{
    public class RemoveTestCommandHandler : CommandHandler<RemoveTestCommand>
    {
        public RemoveTestCommandHandler(EfDbContext context) : base(context)
        {
        }

        protected override void DoHandle(RemoveTestCommand request)
        {
            var test = new Test
            {
                Id = request.Id
            };

            if (test == null)
            {
                throw new DataLayerException($"Deletion failed. Entity with Id: {request.Id} not found");
            }

            DbContext.Tests.Remove(test);
            DbContext.SaveChanges();
        }

        protected override async Task DoHandleAsync(RemoveTestCommand request)
        {
            var test = new Test
            {
                Id = request.Id
            };

            if (test == null)
            {
                throw new DataLayerException($"Deletion failed. Entity with Id: {request.Id} not found");
            }

            DbContext.Tests.Remove(test);
            await DbContext.SaveChangesAsync();
        }
    }
}
