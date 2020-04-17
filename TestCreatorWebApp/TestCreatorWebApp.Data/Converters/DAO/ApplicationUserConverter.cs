using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestCreatorWebApp.Data.Converters.DAO.Interfaces;
using TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Data.Converters.DAO
{
    public class ApplicationUserConverter : IApplicationUserConverter
    {
        private readonly IMapper _mapper;

        public ApplicationUserConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DTO.ApplicationUser, ApplicationUser>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public ApplicationUser Convert(Models.DTO.ApplicationUser applicationUser)
        {
            return _mapper.Map<ApplicationUser>(applicationUser);
        }
    }
}
