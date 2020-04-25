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
    public class RemoveAnswerCommandHandler : CommandHandler<RemoveAnswerCommand>
    {
        public RemoveAnswerCommandHandler(EfDbContext context) : base(context)
        {
        }

        protected override void DoHandle(RemoveAnswerCommand request)
        {
            var answer = new Answer
            {
                Id = request.Id
            };

            if (answer == null)
            {
                throw new DataLayerException($"Deletion failed. Entity with Id: {request.Id} not found");
            }

            DbContext.Answers.Remove(answer);
            DbContext.SaveChanges();
        }

        protected override async Task DoHandleAsync(RemoveAnswerCommand request)
        {
            var answer = new Answer
            {
                Id = request.Id
            };

            if (answer == null)
            {
                throw new DataLayerException($"Deletion failed. Entity with Id: {request.Id} not found");
            }

            DbContext.Answers.Remove(answer);
            await DbContext.SaveChangesAsync();
        }
    }
}
