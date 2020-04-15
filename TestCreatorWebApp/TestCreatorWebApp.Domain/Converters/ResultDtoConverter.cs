using AutoMapper;
using TestCreatorWebApp.Data.Converters.Interfaces;
using DAO = TestCreatorWebApp.Data.Models.DAO;
using Result = TestCreatorWebApp.Data.Models.DTO.Result;

namespace TestCreatorWebApp.Data.Converters
{
    class ResultDtoConverter : IResultDtoConverter
    {
        private readonly IMapper _mapper;

        public ResultDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<DAO.Result, Result>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Result Convert(DAO.Result result)
        {
            return _mapper.Map<Result>(result);
        }
    }
}
