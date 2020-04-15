using System.Collections.Generic;
using TestCreatorWebApp.Domain.Models.DTO;
using TestCreatorWebApp.Domain.Queries.Interfaces;

namespace TestCreatorWebApp.Domain.Queries.Results
{
    public class GetAnswersQueryResult : IQueryResult
    {
        public IEnumerable<Answer> Answers { get; set; }
    }
}
