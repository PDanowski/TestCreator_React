using System;
using AutoMapper;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.Converters.DTO.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.DTO
{
    public class TestDtoConverter : ITestDtoConverter
    {
        private readonly IMapper _mapper;

        public TestDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TestViewModel, Test>()
                    .ForMember(x => x.Flags, opt => opt.Ignore())
                    .ForMember(x => x.Type, opt => opt.Ignore())
                    .ForMember(x => x.Notes, opt => opt.Ignore());
            });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Test Convert(TestViewModel viewModel)
        {
            return _mapper.Map<Test>(viewModel);
        }
    }
}
