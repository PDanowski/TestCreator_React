using AutoMapper;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Converters.DAO
{
    public class ApplicationUserConverter : IApplicationUserConverter
    {
        private readonly IMapper _mapper;

        public ApplicationUserConverter()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.DTO.ApplicationUser, ApplicationUser>()
                    .ForMember(x => x.Notes, opt => opt.Ignore())
                    .ForMember(x => x.Type, opt => opt.Ignore())
                    .ForMember(x => x.Flags, opt => opt.Ignore())
                    .ForMember(x => x.LastModificationDate, opt => opt.Ignore())
                    .ForMember(x => x.Tests, opt => opt.Ignore())
                    .ForMember(x => x.Tokens, opt => opt.Ignore())
                    .ForMember(x => x.NormalizedUserName, opt => opt.Ignore())
                    .ForMember(x => x.NormalizedEmail, opt => opt.Ignore())
                    .ForMember(x => x.EmailConfirmed, opt => opt.Ignore())
                    .ForMember(x => x.PasswordHash, opt => opt.Ignore())
                    .ForMember(x => x.SecurityStamp, opt => opt.Ignore())
                    .ForMember(x => x.ConcurrencyStamp, opt => opt.Ignore())
                    .ForMember(x => x.PhoneNumber, opt => opt.Ignore())
                    .ForMember(x => x.PhoneNumberConfirmed, opt => opt.Ignore())
                    .ForMember(x => x.TwoFactorEnabled, opt => opt.Ignore())
                    .ForMember(x => x.LockoutEnd, opt => opt.Ignore())
                    .ForMember(x => x.LockoutEnabled, opt => opt.Ignore())
                    .ForMember(x => x.AccessFailedCount, opt => opt.Ignore()); 
            });

            _mapper = mapperConfig.CreateMapper();
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public ApplicationUser Convert(Models.DTO.ApplicationUser applicationUser)
        {
            return _mapper.Map<ApplicationUser>(applicationUser);
        }
    }
}
