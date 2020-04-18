using System;
using System.Collections.Generic;
using System.Text;
using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries.Results
{
    public class GetTestsQueryResult : IQueryResult
    {
        public IEnumerable<Test> Tests { get; set; }
    }
}
