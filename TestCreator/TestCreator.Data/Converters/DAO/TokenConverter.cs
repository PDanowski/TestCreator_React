using AutoMapper;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Converters.DAO
{
    public class TokenConverter : ITokenConverter
    {
        private readonly IMapper _mapper;

        public TokenConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DTO.Token, Token>()
                .ForMember(x => x.User, opt => opt.Ignore()); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Token Convert(Models.DTO.Token token)
        {
            return _mapper.Map<Token>(token);
        }
    }
}
