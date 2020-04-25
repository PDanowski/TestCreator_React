using System.Threading.Tasks;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.WebApp.Data.Queries.Interfaces
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult;

        Task<TResult> DispatchAsync<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult;
    }
}
