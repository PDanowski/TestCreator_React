using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestCreatorWebApp.Data.Converters.DAO.Interfaces;
using TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Data.Converters.DAO
{
    public class AnswerConverter : IAnswerConverter
    {
        private readonly IMapper _mapper;

        public AnswerConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DTO.Answer, Answer>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Answer Convert(Models.DTO.Answer answer)
        {
            return _mapper.Map<Answer>(answer);
        }
    }
}
