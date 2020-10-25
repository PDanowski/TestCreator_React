using System.Collections.Generic;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.ViewModel.Interfaces
{
    public interface IResultViewModelConverter
    {
        ResultViewModel Convert(Result result);
        IEnumerable<ResultViewModel> Convert(IEnumerable<Result> results);
    }
}
