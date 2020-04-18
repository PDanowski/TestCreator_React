using System.Collections.Generic;
using TestCreatorWebApp.Data.Models.DTO;
using TestCreatorWebApp.Data.Queries.Interfaces;

namespace TestCreatorWebApp.Data.Queries.Results
{
    public class GetAnswersQueryResult : IQueryResult
    {
        public IEnumerable<Answer> Answers { get; set; }
    }
}
