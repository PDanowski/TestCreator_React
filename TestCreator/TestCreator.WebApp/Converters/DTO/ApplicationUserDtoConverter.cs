using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.Converters.DTO.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.DTO
{
    public class ApplicationUserDtoConverter : IApplicationUserDtoConverter
    {
        private readonly IMapper _mapper;

        public ApplicationUserDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserViewModel, ApplicationUser>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public TestCreator.Data.Models.DTO.ApplicationUser Convert(UserViewModel viewModel)
        {
            return _mapper.Map<ApplicationUser>(viewModel);
        }
    }
}
