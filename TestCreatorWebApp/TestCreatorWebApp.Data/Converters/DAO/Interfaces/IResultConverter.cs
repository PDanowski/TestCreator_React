using System;
using System.Collections.Generic;
using System.Text;
using TestCreatorWebApp.Data.Models.DAO;

namespace TestCreatorWebApp.Data.Converters.DAO.Interfaces
{
    public interface IResultConverter
    {
        Result Convert(Models.DTO.Result result);
    }
}
