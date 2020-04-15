using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DAO = TestCreatorWebApp.Data.Models.DAO;
using TestCreatorWebApp.Domain.Converters.Interfaces;
using TestCreatorWebApp.Domain.Models.DTO;

namespace TestCreatorWebApp.Domain.Converters
{
    class AnswerDtoConverter : IAnswerDtoConverter
    {
        private readonly IMapper _mapper;

        public AnswerDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<DAO.Answer, Answer>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Answer Convert(DAO.Answer answer)
        {
            return _mapper.Map<Answer>(answer);
        }
    }
}
