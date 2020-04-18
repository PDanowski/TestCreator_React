using AutoMapper;
using TestCreator.Data.Converters.DTO.Interfaces;
using Token = TestCreator.Data.Models.DTO.Token;

namespace TestCreator.Data.Converters.DTO
{
    public class TokenDtoConverter : ITokenDtoConverter
    {
        private readonly IMapper _mapper;

        public TokenDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DAO.Token, Token>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Token Convert(Models.DAO.Token token)
        {
            return _mapper.Map<Token>(token);
        }
    }
}
