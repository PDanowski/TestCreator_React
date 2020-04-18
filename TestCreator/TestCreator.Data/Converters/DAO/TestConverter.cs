using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestCreatorWebApp.Data.Converters.DAO.Interfaces;
using TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Data.Converters.DAO
{
    public class TestConverter : ITestConverter
    {
        private readonly IMapper _mapper;

        public TestConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DTO.Test, Test>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Test Convert(Models.DTO.Test test)
        {
            return _mapper.Map<Test>(test);
        }
    }
}
