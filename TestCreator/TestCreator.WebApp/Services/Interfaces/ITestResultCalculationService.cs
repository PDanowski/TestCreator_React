using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Services.Interfaces
{
    public interface ITestResultCalculationService
    {
        TestAttemptResultViewModel CalculateResult(TestAttemptViewModel viewModel);
    }
}
