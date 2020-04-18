using AutoMapper;
using TestCreatorWebApp.Data.Converters.DTO.Interfaces;
using Question = TestCreatorWebApp.Data.Models.DTO.Question;

namespace TestCreatorWebApp.Data.Converters.DTO
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
