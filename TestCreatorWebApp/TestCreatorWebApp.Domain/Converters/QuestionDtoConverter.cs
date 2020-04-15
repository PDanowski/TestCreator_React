using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestCreatorWebApp.Domain.Converters.Interfaces;
using TestCreatorWebApp.Domain.Models.DTO;
using DAO = TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Domain.Converters
{
    class QuestionDtoConverter : IQuestionDtoConverter
    {
        private readonly IMapper _mapper;

        public QuestionDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<DAO.Question, Question>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Question Convert(DAO.Question question)
        {
            return _mapper.Map<Question>(question);
        }
    }
}
