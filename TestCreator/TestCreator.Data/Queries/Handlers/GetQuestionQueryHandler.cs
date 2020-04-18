﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetQuestionQueryHandler : QueryHandler<GetQuestionQuery, GetQuestionQueryResult>
    {
        private readonly IQuestionDtoConverter _dtoConverter;

        public GetQuestionQueryHandler(EfDbContext dbContext) : base(dbContext)
        {

        }

        protected override GetQuestionQueryResult Handle(GetQuestionQuery request)
        {
            var question = DbContext.Questions.FirstOrDefault(q => q.Id.Equals(request.Id));

            return new GetQuestionQueryResult
            {
                Question = _dtoConverter.Convert(question)
            };
        }

        protected override async Task<GetQuestionQueryResult> HandleAsync(GetQuestionQuery request)
        {
            var question = await DbContext.Questions.FirstOrDefaultAsync(q => q.Id.Equals(request.Id));

            return new GetQuestionQueryResult
            {
                Question = _dtoConverter.Convert(question)
            };
        }
    }
}
