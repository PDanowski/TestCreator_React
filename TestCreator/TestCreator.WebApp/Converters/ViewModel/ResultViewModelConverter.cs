using System.Collections.Generic;
using AutoMapper;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.Converters.ViewModel.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.ViewModel
{
    public class ResultViewModelConverter : IResultViewModelConverter
    {
        private readonly IMapper _mapper;

        public ResultViewModelConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Result, ResultViewModel>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public ResultViewModel Convert(Result result)
        {
            return _mapper.Map<ResultViewModel>(result);
        }

        public IEnumerable<ResultViewModel> Convert(IEnumerable<Result> results)
        {
            return _mapper.Map<IEnumerable<ResultViewModel>>(results);
        }
    }
}
