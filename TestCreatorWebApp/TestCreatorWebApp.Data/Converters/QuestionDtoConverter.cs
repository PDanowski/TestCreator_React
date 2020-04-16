using AutoMapper;
using TestCreatorWebApp.Data.Converters.Interfaces;
using DAO = TestCreatorWebApp.Data.Models.DAO;
using Question = TestCreatorWebApp.Data.Models.DTO.Question;

namespace TestCreatorWebApp.Data.Converters
{
    class QuestionDtoConverter : IQuestionDtoConverter
    {
        private readonly IMapper _mapper;

        public QuestionDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<DAO.Question, Question>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Question Convert(DAO.Question question)
        {
            return _mapper.Map<Question>(question);
        }
    }
}
