using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Database;

namespace TestCreator.Data.Commands.Handlers
{
    public class AddResultCommandHandler : CommandHandler<AddResultCommand>
    {
        private readonly IResultConverter _converter;

        public AddResultCommandHandler(EfDbContext context, IResultConverter converter) : base(context)
        {
            _converter = converter;
        }

        protected override void DoHandle(AddResultCommand request)
        {
            var result = _converter.Convert(request.Result);

            result.CreationDate = DateTime.Now;
            result.LastModificationDate = DateTime.Now;

            DbContext.Results.Add(result);
            DbContext.SaveChanges();
        }

        protected override async Task DoHandleAsync(AddResultCommand request)
        {
            var result = _converter.Convert(request.Result);

            result.CreationDate = DateTime.Now;
            result.LastModificationDate = DateTime.Now;

            DbContext.Results.Add(result);
            await DbContext.SaveChangesAsync();
        }
    }
}
