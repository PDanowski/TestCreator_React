using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestCreatorWebApp.Domain.Converters.Interfaces;
using TestCreatorWebApp.Domain.Models.DTO;
using DAO = TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Domain.Converters
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
