﻿using System.Collections.Generic;
using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries.Results
{
    public class GetResultsQueryResult : IQueryResult
    {
        public IEnumerable<Result> Results { get; set; }
    }
}
