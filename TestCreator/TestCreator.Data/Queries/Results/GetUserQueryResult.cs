using System;
using System.Collections.Generic;
using System.Text;
using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries.Results
{
    public class GetUserQueryResult : IQueryResult
    {
        public ApplicationUser User { get; set; }
    }
}
