using System;
using System.Collections.Generic;
using System.Text;
using TestCreatorWebApp.Data.Models.DAO;
using TestCreatorWebApp.Domain.Queries.Interfaces;

namespace TestCreatorWebApp.Domain.Queries.Results
{
    public class GetAnswerQueryResult : IQueryResult
    {
        public Answer Answer { get; }
    }
}
