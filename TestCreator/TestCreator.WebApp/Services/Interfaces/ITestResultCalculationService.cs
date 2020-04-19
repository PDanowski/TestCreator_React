using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Services.Interfaces
{
    public interface ITestResultCalculationService
    {
        TestAttemptResultViewModel CalculateResult(TestAttemptViewModel viewModel);
    }
}
