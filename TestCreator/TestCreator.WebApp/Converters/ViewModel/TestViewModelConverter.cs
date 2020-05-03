using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.Converters.ViewModel.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.ViewModel
{
    public class TestViewModelConverter : ITestViewModelConverter
    {
        private readonly IMapper _mapper;

        public TestViewModelConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Test, TestViewModel>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public TestViewModel Convert(Test test)
        {
            return _mapper.Map<TestViewModel>(test);
        }

        public IEnumerable<TestViewModel> Convert(IEnumerable<Test> tests)
        {
            return _mapper.Map<IEnumerable<TestViewModel>>(tests);
        }
    }
}
