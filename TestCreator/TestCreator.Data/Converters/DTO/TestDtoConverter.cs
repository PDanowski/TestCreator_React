using AutoMapper;
using TestCreator.Data.Converters.DTO.Interfaces;
using Test = TestCreator.Data.Models.DTO.Test;

namespace TestCreator.Data.Converters.DTO
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
