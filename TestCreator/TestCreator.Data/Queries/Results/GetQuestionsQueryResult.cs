﻿using System.Collections.Generic;
using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries.Results
{
    public class GetQuestionsQueryResult : IQueryResult
    {
        public IEnumerable<Question> Questions { get; set; }
    }
}
