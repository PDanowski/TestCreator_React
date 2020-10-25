using System;
using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Database;

namespace TestCreator.Data.Commands.Handlers
{
    public class AddQuestionCommandHandler : CommandHandler<AddQuestionCommand>
    {
        private readonly IQuestionConverter _converter;

        public AddQuestionCommandHandler(EfDbContext context, IQuestionConverter converter) : base(context)
        {
            _converter = converter;
        }

        protected override void DoHandle(AddQuestionCommand request)
        {
            var question = _converter.Convert(request.Question);

            question.CreationDate = DateTime.Now;
            question.LastModificationDate = DateTime.Now;

            DbContext.Questions.Add(question);
            DbContext.SaveChanges();
        }

        protected override async Task DoHandleAsync(AddQuestionCommand request)
        {
            var question = _converter.Convert(request.Question);

            question.CreationDate = DateTime.Now;
            question.LastModificationDate = DateTime.Now;

            DbContext.Questions.Add(question);
            await DbContext.SaveChangesAsync();
        }
    }
}
