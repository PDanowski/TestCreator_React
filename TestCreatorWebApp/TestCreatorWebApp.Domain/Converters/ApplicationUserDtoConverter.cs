using AutoMapper;
using TestCreatorWebApp.Data.Converters.Interfaces;
using ApplicationUser = TestCreatorWebApp.Data.Models.DTO.ApplicationUser;
using DAO = TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Data.Converters
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
