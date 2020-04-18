using AutoMapper;
using TestCreator.Data.Converters.DTO.Interfaces;
using Question = TestCreator.Data.Models.DTO.Question;

namespace TestCreator.Data.Converters.DTO
{
    public class QuestionDtoConverter : IQuestionDtoConverter
    {
        private readonly IMapper _mapper;

        public QuestionDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DAO.Question, Question>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Question Convert(Models.DAO.Question question)
        {
            return _mapper.Map<Question>(question);
        }
    }
}
