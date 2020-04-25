using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Database;

namespace TestCreator.Data.Commands.Handlers
{
    public class AddAnswerCommandHandler : CommandHandler<AddAnswerCommand>
    {
        private readonly IAnswerConverter _converter;

        public AddAnswerCommandHandler(EfDbContext context, IAnswerConverter converter) : base(context)
        {
            _converter = converter;
        }

        protected override void DoHandle(AddAnswerCommand request)
        {
            var answer = _converter.Convert(request.Answer);

            answer.CreationDate = DateTime.Now;
            answer.LastModificationDate = DateTime.Now;

            DbContext.Answers.Add(answer);
            DbContext.SaveChanges();
        }

        protected override async Task DoHandleAsync(AddAnswerCommand request)
        {
            var answer = _converter.Convert(request.Answer);

            answer.CreationDate = DateTime.Now;
            answer.LastModificationDate = DateTime.Now;

            DbContext.Answers.Add(answer);
            await DbContext.SaveChangesAsync();
        }
    }
}
