using AutoMapper;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Converters.DAO
{
    public class AnswerConverter : IAnswerConverter
    {
        private readonly IMapper _mapper;

        public AnswerConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DTO.Answer, Answer>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Answer Convert(Models.DTO.Answer answer)
        {
            return _mapper.Map<Answer>(answer);
        }
    }
}
