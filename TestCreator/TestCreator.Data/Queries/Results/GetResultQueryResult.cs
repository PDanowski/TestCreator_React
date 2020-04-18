using System;
using System.Collections.Generic;
using System.Text;
using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries.Results
{
    public class GetResultQueryResult : IQueryResult
    {
        public Result Result { get; set; }
    }
}
