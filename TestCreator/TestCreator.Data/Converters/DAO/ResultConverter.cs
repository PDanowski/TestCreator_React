using AutoMapper;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Converters.DAO
{
    public class ResultConverter : IResultConverter
    {
        private readonly IMapper _mapper;

        public ResultConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DTO.Result, Result>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Result Convert(Models.DTO.Result result)
        {
            return _mapper.Map<Result>(result);
        }
    }
}
