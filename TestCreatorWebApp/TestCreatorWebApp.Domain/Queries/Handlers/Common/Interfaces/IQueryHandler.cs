using System.Threading.Tasks;
using TestCreatorWebApp.Domain.Queries.Interfaces;

namespace TestCreatorWebApp.Domain.Queries.Handlers
{
    public interface IQueryHandler<in TParameter, TResult> where TResult : IQueryResult where TParameter : IQuery
    {
        TResult Retrieve(TParameter query);

        Task<TResult> RetrieveAsync(TParameter query);
    }
}