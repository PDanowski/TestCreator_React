using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestCreatorWebApp.Data.Converters.DAO.Interfaces;
using TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Data.Converters.DAO
{
    public class TokenConverter : ITokenConverter
    {
        private readonly IMapper _mapper;

        public TokenConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DTO.Token, Token>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Token Convert(Models.DTO.Token token)
        {
            return _mapper.Map<Token>(token);
        }
    }
}
