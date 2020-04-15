using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCreatorWebApp.Domain.Data;
using TestCreatorWebApp.Domain.Queries.Interfaces;

namespace TestCreatorWebApp.Domain.Queries.Handlers
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
            return Handle(query);
        }

        public async Task<TResult> RetrieveAsync(TParameter query)
        {
            return await HandleAsync(query); 
        }

        protected abstract TResult Handle(TParameter request);

        protected abstract Task<TResult> HandleAsync(TParameter request);
    }
}
