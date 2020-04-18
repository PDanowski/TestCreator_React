using System;
using System.Threading.Tasks;
using TestCreator.Data.Database;
using TestCreator.Data.Exceptions;
using TestCreator.Data.Queries.Handlers.Common.Interfaces;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries.Handlers.Common
{
    public abstract class QueryHandler<TParameter, TResult> : IQueryHandler<TParameter, TResult>
        where TResult : IQueryResult, new()
        where TParameter : IQuery, new()
    {
        protected EfDbContext DbContext;

        protected QueryHandler(EfDbContext applicationDbContext)
        {
            DbContext = applicationDbContext;
        }

        public TResult Retrieve(TParameter query)
        {
            try
            {
                return Handle(query);
            }
            catch (Exception ex)
            {
                throw new DataLayerException(ex.Message, ex.InnerException);
            }
            
        }

        public async Task<TResult> RetrieveAsync(TParameter query)
        {
            try
            {
                return await HandleAsync(query);
            }
            catch (Exception ex)
            {
                throw new DataLayerException(ex.Message, ex.InnerException);
            }
        }

        protected abstract TResult Handle(TParameter request);

        protected abstract Task<TResult> HandleAsync(TParameter request);
    }
}
