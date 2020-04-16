using AutoMapper;
using TestCreatorWebApp.Data.Converters.Interfaces;
using DAO = TestCreatorWebApp.Data.Models.DAO;
using Token = TestCreatorWebApp.Data.Models.DTO.Token;

namespace TestCreatorWebApp.Data.Converters
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
