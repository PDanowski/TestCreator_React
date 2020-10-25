using System.Threading.Tasks;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Database;

namespace TestCreator.Data.Commands.Handlers
{
    public class AddTokenCommandHandler : CommandHandler<AddTokenCommand>
    {
        private readonly ITokenConverter _converter;

        public AddTokenCommandHandler(EfDbContext context, ITokenConverter converter) : base(context)
        {
            _converter = converter;
        }

        protected override void DoHandle(AddTokenCommand request)
        {
            var token = _converter.Convert(request.Token);

            DbContext.Tokens.Add(token);
            DbContext.SaveChanges();
        }

        protected override async Task DoHandleAsync(AddTokenCommand request)
        {
            var token = _converter.Convert(request.Token);

            DbContext.Tokens.Add(token);
            await DbContext.SaveChangesAsync();
        }
    }
}
