using AutoMapper;
using TestCreatorWebApp.Data.Converters.DTO.Interfaces;
using ApplicationUser = TestCreatorWebApp.Data.Models.DTO.ApplicationUser;

namespace TestCreatorWebApp.Data.Converters.DTO
{
    public class ApplicationUserDtoConverter : IApplicationUserDtoConverter
    {
        private readonly IMapper _mapper;

        public ApplicationUserDtoConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Models.DAO.ApplicationUser, ApplicationUser>(); });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public ApplicationUser Convert(Models.DAO.ApplicationUser applicationUser)
        {
            return _mapper.Map<ApplicationUser>(applicationUser);
        }
    }
}
