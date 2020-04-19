using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using TestCreator.Data.Queries.Handlers.Common.Interfaces;
using TestCreator.Data.Queries.Interfaces;
using TestCreator.WebApp.Data.Commands.Interfaces;

namespace TestCreator.WebApp.Data.Commands
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext _componentContext;

        public QueryDispatcher(IComponentContext componentContext)
        {
            _componentContext = componentContext ?? throw new ArgumentNullException(nameof(componentContext));
        }

        public TResult Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            var handler = _componentContext.Resolve<IQueryHandler<TParameter, TResult>>();
            return handler.Retrieve(query);
        }

        public async Task<TResult> DispatchAsync<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            var handler = _componentContext.Resolve<IQueryHandler<TParameter, TResult>>();
            return await handler.RetrieveAsync(query);
        }
    }
}
