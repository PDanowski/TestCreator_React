using TestCreator.Data.Queries.Results;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Mappers.Interfaces
{
    public interface ITestAttemptViewModelMapper
    {
        TestAttemptViewModel Convert(GetTestAttemptQueryResult testAttempt);
    }
}
