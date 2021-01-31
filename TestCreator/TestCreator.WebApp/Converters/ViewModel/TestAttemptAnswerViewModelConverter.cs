using System;
using System.Collections.Generic;
using AutoMapper;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.Converters.ViewModel.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.ViewModel
{
    public class TestAttemptAnswerViewModelConverter : ITestAttemptAnswerViewModelConverter
    {
        private readonly IMapper _mapper;

        public TestAttemptAnswerViewModelConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Answer, TestAttemptAnswerViewModel>()
                    .ForMember(x => x.Checked, opt => opt.Ignore());
            });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public IEnumerable<TestAttemptAnswerViewModel> Convert(IEnumerable<Answer> answers)
        {
            return _mapper.Map<IEnumerable<TestAttemptAnswerViewModel>>(answers);
        }
    }
}
