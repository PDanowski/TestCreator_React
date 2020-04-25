using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Exceptions;

namespace TestCreator.Data.Commands.Handlers
{
    public class UpdateAnswerCommandHandler : CommandHandler<UpdateAnswerCommand>
    {
        public UpdateAnswerCommandHandler(EfDbContext context) : base(context)
        {
        }

        protected override void DoHandle(UpdateAnswerCommand request)
        {
            var answer = DbContext.Answers.FirstOrDefault(t => t.Id.Equals(request.Answer.Id));

            if (answer == null)
            {
                throw new DataLayerException($"Update failed. Entity with Id: {request.Answer.Id} not found");
            }

            answer.QuestionId = request.Answer.QuestionId;
            answer.Text = request.Answer.Text;
            answer.Notes = request.Answer.Notes;
            answer.Value = request.Answer.Value;

            answer.LastModificationDate = DateTime.Now;

            DbContext.SaveChanges();
        }

        protected override async Task DoHandleAsync(UpdateAnswerCommand request)
        {
            var answer = DbContext.Answers.FirstOrDefault(t => t.Id.Equals(request.Answer.Id));

            if (answer == null)
            {
                throw new DataLayerException($"Update failed. Entity with Id: {request.Answer.Id} not found");
            }

            answer.QuestionId = request.Answer.QuestionId;
            answer.Text = request.Answer.Text;
            answer.Notes = request.Answer.Notes;
            answer.Value = request.Answer.Value;

            answer.LastModificationDate = DateTime.Now;

            await DbContext.SaveChangesAsync();
        }
    }
}
