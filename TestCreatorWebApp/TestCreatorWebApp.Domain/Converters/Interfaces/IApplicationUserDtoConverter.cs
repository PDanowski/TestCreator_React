using System;
using System.Collections.Generic;
using System.Text;
using TestCreatorWebApp.Domain.Models.DTO;
using DAO = TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Domain.Converters.Interfaces
{
    public interface IApplicationUserDtoConverter
    {
        ApplicationUser Convert(DAO.ApplicationUser applicationUser);
    }
}
