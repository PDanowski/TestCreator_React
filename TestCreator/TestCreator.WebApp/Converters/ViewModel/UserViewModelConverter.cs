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
    public class UserViewModelConverter : IUserViewModelConverter
    {
        private readonly IMapper _mapper;

        public UserViewModelConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserViewModel>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public UserViewModel Convert(ApplicationUser user)
        {
            return _mapper.Map<UserViewModel>(user);
        }
    }
}
