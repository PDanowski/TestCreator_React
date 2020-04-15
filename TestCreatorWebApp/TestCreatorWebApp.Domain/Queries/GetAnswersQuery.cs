using System;
using System.Collections.Generic;
using System.Text;
using TestCreatorWebApp.Domain.Queries.Interfaces;

namespace TestCreatorWebApp.Domain.Queries
{
    public class GetAnswersQuery : IQuery
    {
        public int QuestionId { get; set; }
    }
}
