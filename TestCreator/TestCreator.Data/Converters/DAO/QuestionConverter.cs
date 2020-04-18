using AutoMapper;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Converters.DAO
{
    public class QuestionConverter : IQuestionConverter
    {
        private readonly IMapper _mapper;

        public QuestionConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DTO.Question, Question>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Question Convert(Models.DTO.Question question)
        {
            return _mapper.Map<Question>(question);
        }
    }
}
