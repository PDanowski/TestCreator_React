using AutoMapper;
using TestCreatorWebApp.Data.Converters.Interfaces;
using Answer = TestCreatorWebApp.Data.Models.DTO.Answer;
using DAO = TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Data.Converters
{
    class AnswerDtoConverter : IAnswerDtoConverter
    {
        private readonly IMapper _mapper;

        public AnswerDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<DAO.Answer, Answer>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Answer Convert(DAO.Answer answer)
        {
            return _mapper.Map<Answer>(answer);
        }
    }
}
