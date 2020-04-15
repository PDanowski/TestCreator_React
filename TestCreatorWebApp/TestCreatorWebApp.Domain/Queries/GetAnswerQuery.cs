using System;
using System.Collections.Generic;
using System.Text;
using TestCreatorWebApp.Domain.Queries.Interfaces;

namespace TestCreatorWebApp.Domain.Queries
{
    public class GetAnswerQuery : IQuery
    {
        public int Id { get; set; }
    }
}
