using System;
using System.Collections.Generic;
using System.Text;
using TestCreatorWebApp.Data.Models.DTO;
using TestCreatorWebApp.Domain.Queries.Interfaces;

namespace TestCreatorWebApp.Domain.Queries
{
    public class GetAnswersQueryResult : IQueryResult
    {
        IEnumerable<Answer> Answers { get; }
    }
}
