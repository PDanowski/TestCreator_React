using AutoMapper;
using TestCreatorWebApp.Data.Converters.Interfaces;
using DAO = TestCreatorWebApp.Data.Models.DAO;
using Test = TestCreatorWebApp.Data.Models.DTO.Test;

namespace TestCreatorWebApp.Data.Converters
{
    class TestDtoConverter : ITestDtoConverter
    {
        private readonly IMapper _mapper;

        public TestDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<DAO.Test, Test>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Test Convert(DAO.Test test)
        {
            return _mapper.Map<Test>(test);
        }
    }
}
