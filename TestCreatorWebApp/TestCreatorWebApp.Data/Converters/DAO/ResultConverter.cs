using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestCreatorWebApp.Data.Converters.DAO.Interfaces;
using TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Data.Converters.DAO
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
