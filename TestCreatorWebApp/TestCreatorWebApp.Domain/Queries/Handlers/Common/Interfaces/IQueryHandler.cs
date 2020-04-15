﻿using System.Threading.Tasks;
using TestCreatorWebApp.Data.Queries.Interfaces;

namespace TestCreatorWebApp.Data.Queries.Handlers.Common.Interfaces
{
    public interface IQueryHandler<in TParameter, TResult> where TResult : IQueryResult where TParameter : IQuery
    {
        TResult Retrieve(TParameter query);

        Task<TResult> RetrieveAsync(TParameter query);
    }
}