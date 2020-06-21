using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.Converters.DTO.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.DTO
{
    public class AnswerDtoConverter : IAnswerDtoConverter
    {
        private readonly IMapper _mapper;

        public AnswerDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AnswerViewModel, Answer>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Answer Convert(AnswerViewModel viewModel)
        {
            return _mapper.Map<Answer>(viewModel);
        }
    }
}
