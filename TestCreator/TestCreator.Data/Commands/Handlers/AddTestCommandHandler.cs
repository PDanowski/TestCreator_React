using System;
using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Database;

namespace TestCreator.Data.Commands.Handlers
{
    public class AddTestCommandHandler : CommandHandler<AddTestCommand>
    {
        private readonly ITestConverter _converter;

        public AddTestCommandHandler(EfDbContext context, ITestConverter converter) : base(context)
        {
            _converter = converter;
        }

        protected override void DoHandle(AddTestCommand request)
        {
            var test = _converter.Convert(request.Test);

            test.CreationDate = DateTime.Now;
            test.LastModificationDate = DateTime.Now;

            DbContext.Tests.Add(test);
            DbContext.SaveChanges();
        }

        protected override async Task DoHandleAsync(AddTestCommand request)
        {
            var test = _converter.Convert(request.Test);

            test.CreationDate = DateTime.Now;
            test.LastModificationDate = DateTime.Now;

            DbContext.Tests.Add(test);
            await DbContext.SaveChangesAsync();
        }
    }
}
