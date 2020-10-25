using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Database;
using TestCreator.Data.Exceptions;
using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Commands.Handlers
{
    public class RemoveQuestionCommandHandler : CommandHandler<RemoveQuestionCommand>
    {
        public RemoveQuestionCommandHandler(EfDbContext context) : base(context)
        {
        }

        protected override void DoHandle(RemoveQuestionCommand request)
        {
            var question = new Question
            {
                Id = request.Id
            };

            if (question == null)
            {
                throw new DataLayerException($"Deletion failed. Entity with Id: {request.Id} not found");
            }

            DbContext.Questions.Remove(question);
            DbContext.SaveChanges();
        }

        protected override async Task DoHandleAsync(RemoveQuestionCommand request)
        {
            var question = new Question
            {
                Id = request.Id
            };

            if (question == null)
            {
                throw new DataLayerException($"Deletion failed. Entity with Id: {request.Id} not found");
            }

            DbContext.Questions.Remove(question);
            await DbContext.SaveChangesAsync();
        }
    }
}
