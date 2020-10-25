using System.Collections.Generic;
using TestCreator.Data.Models.DTO;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Converters.ViewModel.Interfaces
{
    public interface ITestViewModelConverter
    {
        TestViewModel Convert(Test test);
        IEnumerable<TestViewModel> Convert(IEnumerable<Test> tests);
    }
}
