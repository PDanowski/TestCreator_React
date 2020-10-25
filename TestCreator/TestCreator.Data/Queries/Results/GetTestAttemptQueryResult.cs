using System.Collections.Generic;
using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries.Results
{
    public class GetTestAttemptQueryResult : IQueryResult
    {
        public Test Test { get; set; }

        public IEnumerable<Answer> Answers { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
}
