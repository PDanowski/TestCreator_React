using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestCreatorWebApp.Domain.Converters.Interfaces;
using TestCreatorWebApp.Domain.Models.DTO;
using DAO = TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Domain.Converters
{
    class TokenDtoConverter : ITokenDtoConverter
    {
        private readonly IMapper _mapper;

        public TokenDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<DAO.Token, Token>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Token Convert(DAO.Token token)
        {
            return _mapper.Map<Token>(token);
        }
    }
}
