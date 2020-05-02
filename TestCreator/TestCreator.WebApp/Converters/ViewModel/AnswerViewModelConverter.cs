using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.Converters.ViewModel.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.ViewModel
{
    public class AnswerViewModelConverter : IAnswerViewModelConverter
    {
        private readonly IMapper _mapper;

        public AnswerViewModelConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Answer, AnswerViewModel>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public AnswerViewModel Convert(Answer answer)
        {
            return _mapper.Map<AnswerViewModel>(answer);
        }

        public IEnumerable<AnswerViewModel> Convert(IEnumerable<Answer> answers)
        {
            return _mapper.Map<IEnumerable<AnswerViewModel>>(answers);
        }
    }
}
