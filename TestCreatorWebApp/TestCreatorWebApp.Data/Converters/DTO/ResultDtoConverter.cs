using AutoMapper;
using TestCreatorWebApp.Data.Converters.DTO.Interfaces;
using Result = TestCreatorWebApp.Data.Models.DTO.Result;

namespace TestCreatorWebApp.Data.Converters.DTO
{
    public class ResultDtoConverter : IResultDtoConverter
    {
        private readonly IMapper _mapper;

        public ResultDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DAO.Result, Result>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Result Convert(Models.DAO.Result result)
        {
            return _mapper.Map<Result>(result);
        }
    }
}
