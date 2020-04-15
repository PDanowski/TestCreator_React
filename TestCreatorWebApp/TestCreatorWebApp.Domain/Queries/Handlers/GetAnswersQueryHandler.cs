using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCreatorWebApp.Domain.Data;

namespace TestCreatorWebApp.Domain.Queries.Handlers
{
    public class GetAnswersQueryHandler : QueryHandler<GetAnswersQuery, GetAnswersQueryResult>
    {
        public GetAnswersQueryHandler(EfDbContext context) : base(context)
        {
                
        }

        protected override GetAnswersQueryResult Handle(GetAnswersQuery request)
        {
            var answer = DbContext.Answers.FirstOrDefault(t => t.QuestionId.Equals(request.QuestionId));
        }

        protected override async Task<GetAnswersQueryResult> HandleAsync(GetAnswersQuery request)
        {
            var answer = await DbContext.Answers.FirstOrDefaultAsync(t => t.QuestionId.Equals(request.QuestionId));
        }
    }
}
