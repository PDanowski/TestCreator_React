using AutoMapper;
using TestCreatorWebApp.Data.Converters.DTO.Interfaces;
using Test = TestCreatorWebApp.Data.Models.DTO.Test;

namespace TestCreatorWebApp.Data.Converters.DTO
{
    public class TestDtoConverter : ITestDtoConverter
    {
        private readonly IMapper _mapper;

        public TestDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DAO.Test, Test>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Test Convert(Models.DAO.Test test)
        {
            return _mapper.Map<Test>(test);
        }
    }
}
