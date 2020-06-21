using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.DTO.Interfaces
{
    public interface IAnswerDtoConverter
    {
        Answer Convert(AnswerViewModel viewModel);
    }
}
