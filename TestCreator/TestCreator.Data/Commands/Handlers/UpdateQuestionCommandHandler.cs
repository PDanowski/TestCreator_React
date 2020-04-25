using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Database;
using TestCreator.Data.Exceptions;

namespace TestCreator.Data.Commands.Handlers
{
    public class UpdateQuestionCommandHandler : CommandHandler<UpdateQuestionCommand>
    {
        public UpdateQuestionCommandHandler(EfDbContext context) : base(context)
        {
        }

        protected override void DoHandle(UpdateQuestionCommand request)
        {
            var question = DbContext.Questions.FirstOrDefault(t => t.Id.Equals(request.Question.Id));

            if (question == null)
            {
                throw new DataLayerException($"Update failed. Entity with Id: {request.Question.Id} not found");
            }

            question.TestId = request.Question.TestId;
            question.Text = request.Question.Text;
            question.Notes = request.Question.Notes;

            question.LastModificationDate = DateTime.Now;

            DbContext.SaveChanges();

        }

        protected override async Task DoHandleAsync(UpdateQuestionCommand request)
        {
            var question = DbContext.Questions.FirstOrDefault(t => t.Id.Equals(request.Question.Id));

            if (question == null)
            {
                throw new DataLayerException($"Update failed. Entity with Id: {request.Question.Id} not found");
            }

            question.TestId = request.Question.TestId;
            question.Text = request.Question.Text;
            question.Notes = request.Question.Notes;

            question.LastModificationDate = DateTime.Now;

            await DbContext.SaveChangesAsync();
        }
    }
}
