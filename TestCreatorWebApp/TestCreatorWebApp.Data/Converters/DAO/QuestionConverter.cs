using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestCreatorWebApp.Data.Converters.DAO.Interfaces;
using TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Data.Converters.DAO
{
    public class QuestionConverter : IQuestionConverter
    {
        private readonly IMapper _mapper;

        public QuestionConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DTO.Question, Question>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Question Convert(Models.DTO.Question question)
        {
            return _mapper.Map<Question>(question);
        }
    }
}
