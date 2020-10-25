using System.Collections.Generic;
using AutoMapper;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.Converters.ViewModel.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.ViewModel
{
    public class QuestionViewModelConverter : IQuestionViewModelConverter
    {
        private readonly IMapper _mapper;

        public QuestionViewModelConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Question, QuestionViewModel>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public QuestionViewModel Convert(Question question)
        {
            return _mapper.Map<QuestionViewModel>(question);
        }

        public IEnumerable<QuestionViewModel> Convert(IEnumerable<Question> questions)
        {
            return _mapper.Map<IEnumerable<QuestionViewModel>>(questions);
        }
    }
}
