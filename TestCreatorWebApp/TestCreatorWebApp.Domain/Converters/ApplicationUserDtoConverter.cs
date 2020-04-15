using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestCreatorWebApp.Domain.Converters.Interfaces;
using TestCreatorWebApp.Domain.Models.DTO;
using DAO = TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Domain.Converters
{
    class ApplicationUserDtoConverter : IApplicationUserDtoConverter
    {
        private readonly IMapper _mapper;

        public ApplicationUserDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<DAO.ApplicationUser, ApplicationUser>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public ApplicationUser Convert(DAO.ApplicationUser applicationUser)
        {
            return _mapper.Map<ApplicationUser>(applicationUser);
        }
    }
}
