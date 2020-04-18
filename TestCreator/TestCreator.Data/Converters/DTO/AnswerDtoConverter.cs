using AutoMapper;
using TestCreator.Data.Converters.DTO.Interfaces;
using Answer = TestCreator.Data.Models.DTO.Answer;

namespace TestCreator.Data.Converters.DTO
{
    public class AnswerDtoConverter : IAnswerDtoConverter
    {
        private readonly IMapper _mapper;

        public AnswerDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DAO.Answer, Answer>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Answer Convert(Models.DAO.Answer answer)
        {
            return _mapper.Map<Answer>(answer);
        }
    }
}
