using System;
using System.Collections.Generic;
using System.Text;
using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries.Results
{
    public class GetAllTestDataQueryResult : IQueryResult
    {
        public Test Test { get; set; }

        public IEnumerable<Answer> Answers { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
}
