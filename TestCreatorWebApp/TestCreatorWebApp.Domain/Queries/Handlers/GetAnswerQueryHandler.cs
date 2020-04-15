using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreatorWebApp.Domain.Converters.Interfaces;
using TestCreatorWebApp.Domain.Data;
using TestCreatorWebApp.Domain.Queries.Results;

namespace TestCreatorWebApp.Domain.Queries.Handlers
{
    public class GetAnswerQueryHandler : QueryHandler<GetAnswerQuery, GetAnswerQueryResult>
    {
        private IAnswerDtoConverter _dtoConverter;

        public GetAnswerQueryHandler(EfDbContext context, IAnswerDtoConverter dtoConverter) : base(context)
        {
            _dtoConverter = dtoConverter;
        }

        protected override GetAnswerQueryResult Handle(GetAnswerQuery request)
        {
            var answer = DbContext.Answers.FirstOrDefault(t => t.Id.Equals(request.Id));

            return new GetAnswerQueryResult
            {
                Answer = _dtoConverter.Convert(answer)
            };
        }

        protected override async Task<GetAnswerQueryResult> HandleAsync(GetAnswerQuery request)
        {
            var answer = await DbContext.Answers.FirstOrDefaultAsync(t => t.Id.Equals(request.Id));

            return new GetAnswerQueryResult
            {
                Answer = _dtoConverter.Convert(answer)
            };
        }
    }
}
