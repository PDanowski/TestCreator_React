using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.ViewModel.Interfaces
{
    public interface IUserViewModelConverter
    {
        UserViewModel Convert(ApplicationUser user);
    }
}
