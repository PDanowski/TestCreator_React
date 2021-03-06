﻿using System;
using AutoMapper;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.Converters.DTO.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.DTO
{
    public class QuestionDtoConverter : IQuestionDtoConverter
    {
        private readonly IMapper _mapper;

        public QuestionDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuestionViewModel, Question>()
                    .ForMember(x => x.Flags, opt => opt.Ignore())
                    .ForMember(x => x.Type, opt => opt.Ignore())
                    .ForMember(x => x.Notes, opt => opt.Ignore());
            });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Question Convert(QuestionViewModel viewModel)
        {
            return _mapper.Map<Question>(viewModel);
        }
    }
}
