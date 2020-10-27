using AutoMapper;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.Converters.DTO.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.DTO
{
    public class ResultDtoConverter : IResultDtoConverter
    {
        private readonly IMapper _mapper;

        public ResultDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ResultViewModel, Result>()
                    .ForMember(x => x.Flags, opt => opt.Ignore())
                    .ForMember(x => x.Type, opt => opt.Ignore())
                    .ForMember(x => x.Notes, opt => opt.Ignore());
            });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Result Convert(ResultViewModel viewModel)
        {
            return _mapper.Map<Result>(viewModel);
        }
    }
}
